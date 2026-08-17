import { environment } from '../../environments/environment';

// URL base de la API backend. Sale del archivo de entorno: en desarrollo es localhost,
// en el build de producción Angular lo reemplaza por environments/environment.prod.ts.
export const API_ORIGIN = environment.apiOrigin;
export const API_BASE_URL = `${API_ORIGIN}/api`;

// Las fotos de las propiedades se guardan en el backend y llegan como rutas relativas
// (ej. "/uploads/propiedades/xxx.jpg"). Como el frontend corre en otro puerto/dominio,
// hay que anteponerles la URL del backend para que el navegador las encuentre.
export function imagenUrl(ruta: string): string {
  return ruta.startsWith('http') ? ruta : `${API_ORIGIN}${ruta}`;
}
