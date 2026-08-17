import { TipoPropiedad } from './propiedad';

export type TipoConsulta = 'Contacto' | 'Tasacion';

export interface ConsultaCreate {
  propiedadId: number | null;
  nombre: string;
  apellido?: string;
  email: string;
  telefono?: string;
  mensaje?: string;
  tipoPropiedad?: TipoPropiedad;
  tipo: TipoConsulta;
}

export interface Consulta extends ConsultaCreate {
  id: number;
  fechaCreacion: string;
  leida: boolean;
}
