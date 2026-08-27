// Los bots que generan la vista previa de un link (WhatsApp, Facebook, etc.) no
// ejecutan JavaScript: solo leen el HTML crudo que devuelve el servidor. Como el
// sitio es una SPA de Angular, esos bots siempre veían las meta tags fijas de
// index.html (la misma foto para cualquier propiedad). Esta función intercepta
// esos pedidos puntuales y devuelve un HTML mínimo con las meta tags de la
// propiedad correspondiente; a los navegadores normales los deja pasar tal cual.
import type { Context } from '@netlify/edge-functions';

const API_ORIGIN = 'https://inmobiliaria-gr-riveiro.onrender.com';
const NOMBRE_SITIO = 'GR Riveiro Negocios Inmobiliarios';

const BOT_UA =
  /whatsapp|facebookexternalhit|twitterbot|telegrambot|linkedinbot|slackbot|discordbot|skypeuripreview|pinterest/i;

const ETIQUETAS_OPCION: Record<string, string> = {
  AlquilerTemporario: 'Alquiler Temporario',
};

const TIPO_LABELS: Record<string, string> = {
  Casa: 'Casa',
  Departamento: 'Departamento',
  Quinta: 'Quinta',
  Terreno: 'Terreno',
  PH: 'Casa PH',
  Galpon: 'Galpón',
  Local: 'Local',
  Emprendimiento: 'Emprendimiento',
};

function escapeHtml(texto: string): string {
  return texto
    .replace(/&/g, '&amp;')
    .replace(/</g, '&lt;')
    .replace(/>/g, '&gt;')
    .replace(/"/g, '&quot;');
}

export default async (request: Request, context: Context) => {
  const userAgent = request.headers.get('user-agent') ?? '';
  if (!BOT_UA.test(userAgent)) {
    return context.next();
  }

  const url = new URL(request.url);
  const match = url.pathname.match(/^\/propiedades\/(\d+)/);
  if (!match) {
    return context.next();
  }

  try {
    const respuesta = await fetch(`${API_ORIGIN}/api/propiedades/${match[1]}`);
    if (!respuesta.ok) return context.next();

    const propiedad = await respuesta.json();

    const titulo = `${propiedad.titulo} | ${NOMBRE_SITIO}`;
    const prefijoOperacion = propiedad.operacion ? `${ETIQUETAS_OPCION[propiedad.operacion] ?? propiedad.operacion} - ` : '';
    const tipoLabel = TIPO_LABELS[propiedad.tipo] ?? propiedad.tipo;
    const descripcion = `${prefijoOperacion}${tipoLabel} en ${propiedad.barrioCiudad}, ${propiedad.partidoLocalidad}.`;

    const primeraImagen = propiedad.imagenes?.[0]?.url as string | undefined;
    const imagen = primeraImagen
      ? primeraImagen.startsWith('http')
        ? primeraImagen
        : `${API_ORIGIN}${primeraImagen}`
      : `${url.origin}/hero.jpg`;

    const html = `<!doctype html>
<html lang="es">
<head>
<meta charset="utf-8">
<title>${escapeHtml(titulo)}</title>
<meta name="description" content="${escapeHtml(descripcion)}">
<meta property="og:type" content="website">
<meta property="og:site_name" content="${escapeHtml(NOMBRE_SITIO)}">
<meta property="og:title" content="${escapeHtml(titulo)}">
<meta property="og:description" content="${escapeHtml(descripcion)}">
<meta property="og:image" content="${escapeHtml(imagen)}">
<meta property="og:url" content="${escapeHtml(url.href)}">
</head>
<body></body>
</html>`;

    return new Response(html, { headers: { 'content-type': 'text/html; charset=utf-8' } });
  } catch {
    return context.next();
  }
};

export const config = { path: '/propiedades/*' };
