import { Moneda, TipoPropiedad } from './models/propiedad';
import { ZONAS_GEOGRAFICAS } from './geografia-argentina';

export type FieldType = 'text' | 'textarea' | 'number' | 'select' | 'checkbox';

export interface FieldConfig {
  key: string;
  label: string;
  type: FieldType;
  opciones?: string[]; // solo para type: 'select'
  requerido?: boolean;
}

// Para mostrar más lindo un código interno cuando no coincide con lo que se ve en
// pantalla (ej. el valor real es "AlquilerTemporario" pero se muestra "Alquiler
// Temporario"). Si un valor no está acá, se muestra tal cual.
export const ETIQUETAS_OPCION: Record<string, string> = {
  AlquilerTemporario: 'Alquiler Temporario',
  Pesos: 'Pesos (ARS)',
  Dolares: 'Dólares (U$D)',
};

export function etiquetaOpcion(valor: string): string {
  return ETIQUETAS_OPCION[valor] ?? valor;
}

// Símbolo de moneda para mostrar junto al precio (pesos argentinos = ARS, no "$" a secas).
export function etiquetaMoneda(moneda: Moneda | undefined): string {
  return moneda === 'Dolares' ? 'U$D' : 'ARS';
}

const texto = (key: string, label: string, requerido = false): FieldConfig => ({
  key,
  label,
  type: 'text',
  requerido,
});
const textoArea = (key: string, label: string, requerido = false): FieldConfig => ({
  key,
  label,
  type: 'textarea',
  requerido,
});
const numero = (key: string, label: string, requerido = false): FieldConfig => ({
  key,
  label,
  type: 'number',
  requerido,
});
const check = (key: string, label: string): FieldConfig => ({ key, label, type: 'checkbox' });
const select = (key: string, label: string, opciones: string[], requerido = false): FieldConfig => ({
  key,
  label,
  type: 'select',
  opciones,
  requerido,
});

// Campos comunes a cualquier tipo de propiedad (los que vive en la tabla base "Propiedad").
export const CAMPOS_COMUNES: FieldConfig[] = [
  texto('titulo', 'Título', true),
  select('operacion', 'Operación', ['Venta', 'Alquiler', 'AlquilerTemporario', 'Remate'], true),
  select(
    'estado',
    'Estado de la publicación',
    ['Vigente', 'Reservado', 'Suspendido', 'Historico', 'EnTasacion', 'Alquilado', 'Vendido', 'Borrador'],
    true,
  ),
  numero('monto', 'Monto', true),
  select('moneda', 'Moneda', ['Pesos', 'Dolares'], true),
  check('noPublicarPrecio', 'No publicar el precio'),
  numero('expensasMonto', 'Expensas (monto)'),
  select('expensasTipo', 'Expensas (tipo)', [
    'No incluidas',
    'Incluidas',
    'A consultar',
    'No paga expensas',
    'Fijos',
    'Aproximados',
  ]),
  check('aptoProfesional', 'Apto profesional'),
  check('aceptaMascotas', 'Acepta mascotas'),
  select('zonaGeografica', 'Zona geográfica', ZONAS_GEOGRAFICAS, true),
  texto('partidoLocalidad', 'Partido / Localidad', true),
  texto('barrioCiudad', 'Barrio / Ciudad', true),
  texto('calle', 'Calle', true),
  texto('nroCalle', 'Número'),
  texto('piso', 'Piso'),
  texto('depto', 'Depto'),
  texto('entreCalle1', 'Entre calle 1'),
  texto('entreCalle2', 'Entre calle 2'),
  numero('latitud', 'Latitud (opcional)'),
  numero('longitud', 'Longitud (opcional)'),
  textoArea('descripcion', 'Descripción', true),
];

const ORIENTACION = [
  'Este',
  'Oeste',
  'Norte',
  'Sur',
  'Sudeste',
  'Sudoeste',
  'Noreste',
  'Noroeste',
];
const LUMINOSIDAD = ['Muy luminoso', 'Luminoso', 'Poco luminoso'];

const camposCasa: FieldConfig[] = [
  select('edificacion', 'Edificación', ['Casa', 'Chalet', 'Duplex', 'Triplex', 'Departamento', 'PH'], true),
  numero('superficieTerreno', 'Superficie del terreno (m²)'),
  numero('superficieTotal', 'Superficie total (m²)'),
  numero('superficieCubierta', 'Superficie cubierta (m²)'),
  numero('superficieDescubierta', 'Superficie descubierta (m²)'),
  select('orientacion', 'Orientación', ORIENTACION),
  select('luminosidad', 'Luminosidad', LUMINOSIDAD),
  texto('tipoTecho', 'Tipo de techo'),
  numero('cocherasCubiertas', 'Cocheras cubiertas'),
  numero('cocherasDescubiertas', 'Cocheras descubiertas'),
  numero('cantidadDormitorios', 'Dormitorios'),
  numero('cantidadBanos', 'Baños'),
  texto('servicios', 'Servicios (separados por coma)'),
  texto('instalaciones', 'Instalaciones (separadas por coma)'),
];

const camposDepartamento: FieldConfig[] = [
  select(
    'edificacion',
    'Edificación',
    ['Duplex', 'Triplex', 'Departamento', 'Penthouse', 'Piso', 'Semipiso', 'Loft'],
    true,
  ),
  numero('superficieTotal', 'Superficie total (m²)'),
  numero('superficieCubierta', 'Superficie cubierta (m²)'),
  numero('superficieDescubierta', 'Superficie descubierta (m²)'),
  select('orientacion', 'Orientación', ORIENTACION),
  select('disposicion', 'Disposición', ['Al frente', 'Contrafrente', 'Interno', 'Lateral']),
  select('luminosidad', 'Luminosidad', LUMINOSIDAD),
  select('tipoBalcon', 'Tipo de balcón', ['Francés', 'Corrido', 'Terraza']),
  numero('cantidadPisosEdificio', 'Pisos del edificio'),
  numero('cocherasCubiertas', 'Cocheras cubiertas'),
  numero('cocherasDescubiertas', 'Cocheras descubiertas'),
  numero('cantidadDormitorios', 'Dormitorios'),
  numero('cantidadBanos', 'Baños'),
  texto('serviciosPropiedad', 'Servicios de la propiedad (separados por coma)'),
  texto('amenitiesEdificio', 'Amenities del edificio (separados por coma)'),
];

const camposGalpon: FieldConfig[] = [
  numero('superficieTerreno', 'Superficie del terreno (m²)'),
  numero('superficieTotal', 'Superficie total (m²)'),
  numero('superficieCubierta', 'Superficie cubierta (m²)'),
  numero('alturaEntrada', 'Altura de entrada (m)'),
  numero('alturaTecho', 'Altura del techo (m)'),
  select('tipoGalpon', 'Tipo de galpón', ['Almacén / depósito', 'Bodega comercial', 'Nave industrial']),
  select('tipoPorton', 'Tipo de portón', ['Levadizo', 'Corredizo']),
  select('tipoGas', 'Tipo de gas', ['De Red', 'Estacionario', 'De Cilindros']),
  texto('servicios', 'Servicios (separados por coma)'),
];

const camposLocal: FieldConfig[] = [
  numero('superficieTotal', 'Superficie total (m²)'),
  numero('superficieCubierta', 'Superficie cubierta (m²)'),
  numero('superficieLocal', 'Superficie del local (m²)'),
  numero('alturaInterior', 'Altura interior (m)'),
  select('situado', 'Situado en', ['Vía pública', 'Shopping', 'Galería']),
  select('orientacion', 'Orientación', ORIENTACION),
  numero('cantidadBanos', 'Baños'),
  texto('serviciosPropiedad', 'Servicios (separados por coma)'),
];

const camposQuinta: FieldConfig[] = [
  select('edificacion', 'Edificación', ['Casa', 'Chalet'], true),
  numero('superficieTerreno', 'Superficie del terreno'),
  numero('superficieTotal', 'Superficie total'),
  select('unidadSuperficieTotal', 'Unidad de superficie', ['m²', 'ha']),
  numero('superficieCubierta', 'Superficie cubierta (m²)'),
  select('detalleAcceso', 'Acceso', ['A consultar', 'Arena', 'Asfalto', 'Ripio', 'Tierra']),
  numero('cantidadDormitorios', 'Dormitorios'),
  numero('cantidadBanos', 'Baños'),
  texto('servicios', 'Servicios (separados por coma)'),
];

const camposTerreno: FieldConfig[] = [
  numero('superficieTotal', 'Superficie total', true),
  select('unidadSuperficieTotal', 'Unidad de superficie', ['m²', 'ha'], true),
  select('tipoLote', 'Tipo de lote', [
    'Perimetral',
    'Interno',
    'Al golf',
    'A la laguna',
    'Al río',
    'Otro',
  ]),
  select('tipoUsoTerreno', 'Uso permitido', ['Residencial', 'Comercial', 'Industrial']),
  select('formaTerreno', 'Forma del terreno', ['Regular', 'Irregular', 'Plano']),
  select('detalleAcceso', 'Acceso', ['A consultar', 'Arena', 'Asfalto', 'Ripio', 'Tierra']),
  texto('servicios', 'Servicios (separados por coma)'),
];

const camposPH: FieldConfig[] = [
  select('edificacion', 'Edificación', ['Casa', 'Chalet', 'Duplex', 'Triplex', 'PH'], true),
  numero('superficieTerreno', 'Superficie del terreno (m²)'),
  numero('superficieTotal', 'Superficie total (m²)'),
  numero('superficieCubierta', 'Superficie cubierta (m²)'),
  select('orientacion', 'Orientación', ORIENTACION),
  select('luminosidad', 'Luminosidad', LUMINOSIDAD),
  numero('cocherasCubiertas', 'Cocheras cubiertas'),
  numero('cocherasDescubiertas', 'Cocheras descubiertas'),
  numero('cantidadDormitorios', 'Dormitorios'),
  numero('cantidadBanos', 'Baños'),
  texto('serviciosPropiedad', 'Servicios (separados por coma)'),
];

const camposEmprendimiento: FieldConfig[] = [
  texto('nombre', 'Nombre del emprendimiento', true),
  texto('leyendaComercial', 'Leyenda comercial'),
  select('etapaObra', 'Etapa de obra', ['En Pozo', 'En construcción', 'Terminado'], true),
  texto('fechaEntrega', 'Fecha de entrega (MM/YYYY)'),
  check('conFinanciacion', 'Con financiación'),
  numero('cantidadPisos', 'Cantidad de pisos'),
  numero('cantidadDepartamentos', 'Cantidad de departamentos'),
  texto('serviciosPropiedad', 'Servicios (separados por coma)'),
];

export const CAMPOS_POR_TIPO: Record<TipoPropiedad, FieldConfig[]> = {
  Casa: camposCasa,
  Departamento: camposDepartamento,
  Galpon: camposGalpon,
  Local: camposLocal,
  Quinta: camposQuinta,
  Terreno: camposTerreno,
  PH: camposPH,
  Emprendimiento: camposEmprendimiento,
};

export const TIPOS_PROPIEDAD: TipoPropiedad[] = [
  'Casa',
  'Departamento',
  'Quinta',
  'Terreno',
  'PH',
  'Galpon',
  'Local',
  'Emprendimiento',
];

// Nombre "lindo" para mostrar en pantalla (el código interno, ej. "PH" o "Galpon",
// no cambia: esto es solo para lo que ve el usuario).
export const TIPO_LABELS: Record<TipoPropiedad, string> = {
  Casa: 'Casa',
  Departamento: 'Departamento',
  Quinta: 'Quinta',
  Terreno: 'Terreno',
  PH: 'Casa PH',
  Galpon: 'Galpón',
  Local: 'Local',
  Emprendimiento: 'Emprendimiento',
};

// --- Datos para los 4 menús desplegables de la ficha de propiedad ---
// En vez de armar el contenido a mano para cada uno de los 8 tipos, reutilizamos
// la misma configuración de campos del formulario de carga: mostramos únicamente
// los campos que esa propiedad puntual tiene cargados.

export interface DatoDetalle {
  label: string;
  valor: string;
}

const EXCLUIR_DATOS_BASICOS = new Set([
  'titulo',
  'operacion',
  'estado',
  'monto',
  'moneda',
  'noPublicarPrecio',
  'descripcion',
  'latitud',
  'longitud',
]);

const CLAVES_SERVICIOS = new Set(['servicios', 'serviciosPropiedad']);
const CLAVES_AMENITIES = new Set(['amenitiesEdificio', 'serviciosEdificio', 'instalaciones', 'instalacionesPropiedad']);

function tieneValor(valor: unknown): boolean {
  return valor !== null && valor !== undefined && valor !== '';
}

function formatearValor(campo: FieldConfig, valor: unknown): string {
  if (campo.type === 'checkbox') return valor ? 'Sí' : 'No';
  if (campo.type === 'select') return etiquetaOpcion(String(valor));
  return String(valor);
}

// "Datos básicos": los campos comunes (ubicación, expensas, etc.) que no se muestran
// ya arriba en el título/precio de la ficha.
export function datosBasicos(propiedad: Record<string, unknown>): DatoDetalle[] {
  return CAMPOS_COMUNES.filter((c) => !EXCLUIR_DATOS_BASICOS.has(c.key) && tieneValor(propiedad[c.key])).map(
    (c) => ({ label: c.label, valor: formatearValor(c, propiedad[c.key]) }),
  );
}

// "Características de la propiedad": los campos propios del tipo (superficie,
// ambientes, orientación, etc.), sin las listas de servicios/amenities.
export function caracteristicasEspecificas(tipo: TipoPropiedad, propiedad: Record<string, unknown>): DatoDetalle[] {
  return CAMPOS_POR_TIPO[tipo]
    .filter((c) => !CLAVES_SERVICIOS.has(c.key) && !CLAVES_AMENITIES.has(c.key) && tieneValor(propiedad[c.key]))
    .map((c) => ({ label: c.label, valor: formatearValor(c, propiedad[c.key]) }));
}

function combinarListas(tipo: TipoPropiedad, propiedad: Record<string, unknown>, claves: Set<string>): string[] {
  const items: string[] = [];
  for (const campo of CAMPOS_POR_TIPO[tipo]) {
    if (!claves.has(campo.key)) continue;
    const valor = propiedad[campo.key];
    if (typeof valor === 'string' && valor.trim()) {
      items.push(...valor.split(',').map((s) => s.trim()).filter(Boolean));
    }
  }
  return [...new Set(items)];
}

export function listaServicios(tipo: TipoPropiedad, propiedad: Record<string, unknown>): string[] {
  return combinarListas(tipo, propiedad, CLAVES_SERVICIOS);
}

export function listaAmenities(tipo: TipoPropiedad, propiedad: Record<string, unknown>): string[] {
  return combinarListas(tipo, propiedad, CLAVES_AMENITIES);
}
