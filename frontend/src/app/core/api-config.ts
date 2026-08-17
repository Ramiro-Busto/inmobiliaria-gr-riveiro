// URL base de la API backend. Cuando se despliegue a producción, cambiar este valor
// (o pasarlo a un archivo de entorno) por la URL real del servidor.
export const API_ORIGIN = 'http://localhost:5080';
export const API_BASE_URL = `${API_ORIGIN}/api`;

// Las fotos de las propiedades se guardan en el backend y llegan como rutas relativas
// (ej. "/uploads/propiedades/xxx.jpg"). Como el frontend corre en otro puerto/dominio,
// hay que anteponerles la URL del backend para que el navegador las encuentre.
export function imagenUrl(ruta: string): string {
  return ruta.startsWith('http') ? ruta : `${API_ORIGIN}${ruta}`;
}
