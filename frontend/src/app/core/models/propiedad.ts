export type TipoPropiedad =
  | 'Casa'
  | 'Departamento'
  | 'Galpon'
  | 'Local'
  | 'Quinta'
  | 'Terreno'
  | 'PH'
  | 'Emprendimiento';

export type Operacion = 'Venta' | 'Alquiler' | 'AlquilerTemporario' | 'Remate';

export type EstadoPublicacion =
  | 'Vigente'
  | 'Reservado'
  | 'Suspendido'
  | 'Historico'
  | 'EnTasacion'
  | 'Alquilado'
  | 'Vendido'
  | 'Borrador';

export type Moneda = 'Pesos' | 'Dolares';

export interface ImagenPropiedad {
  id: number;
  propiedadId: number;
  url: string;
  orden: number;
}

// Campos comunes a los 12 tipos. Los campos específicos de cada tipo (edificacion,
// superficieTotal, cantidadDormitorios, etc.) llegan igual en el JSON pero no están
// tipados acá uno por uno: se acceden como propiedades sueltas del objeto.
export interface Propiedad {
  id: number;
  tipo: TipoPropiedad;
  titulo: string;
  operacion?: Operacion;
  estado: EstadoPublicacion;
  monto?: number;
  moneda?: Moneda;
  noPublicarPrecio: boolean;
  zonaGeografica: string;
  partidoLocalidad: string;
  barrioCiudad: string;
  calle: string;
  descripcion: string;
  imagenes: ImagenPropiedad[];

  // Campos específicos según el tipo (edificacion, superficieTotal, cantidadDormitorios, etc.)
  [campoEspecifico: string]: unknown;
}
