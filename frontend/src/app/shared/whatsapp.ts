// Número fijo de WhatsApp de la inmobiliaria (siempre el mismo, sin importar la propiedad).
const WHATSAPP_NUMERO = '5491150372799';

export function linkWhatsApp(mensaje: string): string {
  return `https://wa.me/${WHATSAPP_NUMERO}?text=${encodeURIComponent(mensaje)}`;
}

// Mensaje default para el botón de WhatsApp dentro de la ficha de una propiedad.
export function linkWhatsAppPropiedad(urlPropiedad: string): string {
  const mensaje =
    `Hola, vi esta propiedad en el sitio web de la inmobiliaria y quiero que me contacten. ` +
    `Gracias. '${urlPropiedad}'`;

  return linkWhatsApp(mensaje);
}
