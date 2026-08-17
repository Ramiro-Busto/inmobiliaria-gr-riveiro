import { Component, OnInit, computed, inject, signal } from '@angular/core';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { ActivatedRoute, Router, RouterLink } from '@angular/router';
import { CdkDragDrop, DragDropModule, moveItemInArray } from '@angular/cdk/drag-drop';
import { PropiedadesService } from '../../../core/propiedades.service';
import { ImagenesService } from '../../../core/imagenes.service';
import { Icon } from '../../../shared/icon/icon';
import { imagenUrl } from '../../../core/api-config';
import { Propiedad, TipoPropiedad } from '../../../core/models/propiedad';
import { ZONAS_GEOGRAFICAS, PARTIDOS_POR_ZONA, BARRIOS_POR_PARTIDO_ZONA_SUR } from '../../../core/geografia-argentina';

const ZONA_SUR = 'Bs.As. G.B.A. Zona Sur';
import {
  CAMPOS_COMUNES,
  CAMPOS_POR_TIPO,
  FieldConfig,
  TIPOS_PROPIEDAD,
  TIPO_LABELS,
  etiquetaOpcion,
} from '../../../core/property-form-config';

interface ImagenCargada {
  url: string;
}

// Todos los campos de ubicación se muestran juntos en su propia sección (entre "Fotos" y
// "Datos básicos"), no en el loop genérico de "Datos básicos". Zona y Partido además van en
// combos encadenados en vez del <select>/<input> genérico.
const CAMPOS_UBICACION_CASCADA = new Set(['zonaGeografica', 'partidoLocalidad', 'barrioCiudad']);
const CLAVES_UBICACION = new Set([
  'zonaGeografica',
  'partidoLocalidad',
  'barrioCiudad',
  'calle',
  'nroCalle',
  'piso',
  'depto',
  'entreCalle1',
  'entreCalle2',
  'latitud',
  'longitud',
]);

@Component({
  selector: 'app-propiedad-form',
  imports: [ReactiveFormsModule, DragDropModule, RouterLink, Icon],
  templateUrl: './propiedad-form.html',
  styleUrl: './propiedad-form.scss',
})
export class PropiedadForm implements OnInit {
  private readonly fb = inject(FormBuilder);
  private readonly propiedadesService = inject(PropiedadesService);
  private readonly imagenesService = inject(ImagenesService);
  private readonly route = inject(ActivatedRoute);
  private readonly router = inject(Router);

  protected readonly CAMPOS_COMUNES = CAMPOS_COMUNES;
  protected readonly camposDatosBasicos = CAMPOS_COMUNES.filter((c) => !CLAVES_UBICACION.has(c.key));
  // Barrio/Ciudad, Calle, Nro, Piso, Depto, Latitud y Longitud (Zona y Partido van aparte,
  // en combos encadenados).
  protected readonly camposUbicacion = CAMPOS_COMUNES.filter(
    (c) => CLAVES_UBICACION.has(c.key) && !CAMPOS_UBICACION_CASCADA.has(c.key),
  );
  protected readonly TIPOS_PROPIEDAD = TIPOS_PROPIEDAD;
  protected readonly TIPO_LABELS = TIPO_LABELS;
  protected readonly etiquetaOpcion = etiquetaOpcion;
  protected readonly ZONAS_GEOGRAFICAS = ZONAS_GEOGRAFICAS;

  protected readonly tipoSeleccionado = signal<TipoPropiedad | null>(null);
  protected readonly camposEspecificos = computed<FieldConfig[]>(() => {
    const tipo = this.tipoSeleccionado();
    return tipo ? CAMPOS_POR_TIPO[tipo] : [];
  });

  protected readonly esEdicion = signal(false);
  protected readonly guardando = signal(false);
  protected readonly error = signal(false);
  protected readonly camposFaltantes = signal<string[]>([]);
  private propiedadId: number | null = null;

  protected readonly imagenes = signal<ImagenCargada[]>([]);
  protected readonly subiendoImagen = signal(false);
  protected readonly errorImagen = signal<string | null>(null);
  protected readonly arrastrandoImagen = signal(false);
  protected readonly imagenUrl = imagenUrl;

  protected readonly form: FormGroup = this.fb.group({});

  ngOnInit(): void {
    for (const campo of CAMPOS_COMUNES) {
      this.form.addControl(campo.key, this.crearControl(campo));
    }

    const idParam = this.route.snapshot.paramMap.get('id');
    if (idParam) {
      this.esEdicion.set(true);
      this.propiedadId = Number(idParam);
      this.propiedadesService
        .getByIdParaAdmin(this.propiedadId)
        .subscribe((propiedad) => this.cargarPropiedad(propiedad));
    } else {
      this.form.patchValue({ estado: 'Vigente' });
    }
  }

  private crearControl(campo: FieldConfig) {
    const valorInicial = campo.type === 'checkbox' ? false : '';
    return this.fb.control(valorInicial, campo.requerido ? Validators.required : []);
  }

  seleccionarTipo(tipo: TipoPropiedad): void {
    const tipoAnterior = this.tipoSeleccionado();
    if (tipoAnterior) {
      for (const campo of CAMPOS_POR_TIPO[tipoAnterior]) {
        if (this.form.contains(campo.key)) this.form.removeControl(campo.key);
      }
    }

    this.tipoSeleccionado.set(tipo);

    for (const campo of CAMPOS_POR_TIPO[tipo]) {
      this.form.addControl(campo.key, this.crearControl(campo));
    }
  }

  private cargarPropiedad(propiedad: Propiedad): void {
    this.seleccionarTipo(propiedad.tipo);
    this.form.patchValue(propiedad);
    this.imagenes.set([...propiedad.imagenes].sort((a, b) => a.orden - b.orden).map((img) => ({ url: img.url })));
  }

  protected campoInvalido(key: string): boolean {
    const control = this.form.get(key);
    if (!control) return false;
    if (control.invalid && control.touched) return true;

    // "Entre calle 1" y "Entre calle 2": no son obligatorias por sí solas, pero si se
    // completa una, la otra pasa a serlo (no tiene sentido cargar solo una de las dos).
    if (key === 'entreCalle1' || key === 'entreCalle2') {
      const otraClave = key === 'entreCalle1' ? 'entreCalle2' : 'entreCalle1';
      const otraCompleta = !!this.form.get(otraClave)?.value;
      return control.touched && !control.value && otraCompleta;
    }

    return false;
  }

  protected get partidosDisponibles(): string[] {
    const zona = this.form.get('zonaGeografica')?.value;
    return zona ? (PARTIDOS_POR_ZONA[zona] ?? []) : [];
  }

  // Solo tenemos barrios cargados para Zona Sur (donde opera la inmobiliaria). Para el
  // resto de las zonas, o un partido sin datos todavía, Barrio/Ciudad sigue siendo texto libre.
  protected get barriosDisponibles(): string[] {
    if (this.form.get('zonaGeografica')?.value !== ZONA_SUR) return [];
    const partido = this.form.get('partidoLocalidad')?.value;
    return partido ? (BARRIOS_POR_PARTIDO_ZONA_SUR[partido] ?? []) : [];
  }

  cambiarZona(valor: string): void {
    this.form.get('zonaGeografica')?.setValue(valor);
    this.form.get('zonaGeografica')?.markAsTouched();

    // Si el partido que estaba elegido no existe en la zona nueva, se limpia (y con él, el barrio).
    const partidoActual = this.form.get('partidoLocalidad')?.value;
    if (partidoActual && !this.partidosDisponibles.includes(partidoActual)) {
      this.form.get('partidoLocalidad')?.setValue('');
      this.form.get('barrioCiudad')?.setValue('');
    }
  }

  cambiarPartido(valor: string): void {
    this.form.get('partidoLocalidad')?.setValue(valor);
    this.form.get('partidoLocalidad')?.markAsTouched();

    // Si el barrio que estaba elegido no existe para el partido nuevo, se limpia.
    const barrioActual = this.form.get('barrioCiudad')?.value;
    if (barrioActual && !this.barriosDisponibles.includes(barrioActual)) {
      this.form.get('barrioCiudad')?.setValue('');
    }
  }

  subirImagenes(event: Event): void {
    const input = event.target as HTMLInputElement;
    const archivos = input.files ? Array.from(input.files) : [];
    this.subirArchivos(archivos);
    input.value = '';
  }

  soltarImagenes(event: DragEvent): void {
    event.preventDefault();
    this.arrastrandoImagen.set(false);

    const archivos = event.dataTransfer?.files ? Array.from(event.dataTransfer.files) : [];
    const imagenes = archivos.filter((archivo) => archivo.type.startsWith('image/'));
    this.subirArchivos(imagenes);
  }

  arrastreSobreZona(event: DragEvent): void {
    event.preventDefault();
    this.arrastrandoImagen.set(true);
  }

  arrastreFueraDeZona(): void {
    this.arrastrandoImagen.set(false);
  }

  private subirArchivos(archivos: File[]): void {
    if (archivos.length === 0) return;

    this.subiendoImagen.set(true);
    this.errorImagen.set(null);

    let pendientes = archivos.length;
    for (const archivo of archivos) {
      this.imagenesService.subir(archivo).subscribe({
        next: ({ url }) => {
          this.imagenes.update((actuales) => [...actuales, { url }]);
          pendientes--;
          if (pendientes === 0) this.subiendoImagen.set(false);
        },
        error: () => {
          this.errorImagen.set('No se pudo subir alguna imagen. Probá de nuevo.');
          pendientes--;
          if (pendientes === 0) this.subiendoImagen.set(false);
        },
      });
    }
  }

  quitarImagen(index: number): void {
    this.imagenes.update((actuales) => actuales.filter((_, i) => i !== index));
  }

  // La foto que queda primera (más a la izquierda) es la portada.
  reordenarImagenes(event: CdkDragDrop<ImagenCargada[]>): void {
    const actuales = [...this.imagenes()];
    moveItemInArray(actuales, event.previousIndex, event.currentIndex);
    this.imagenes.set(actuales);
  }

  // Un campo numérico vacío llega del formulario como "" (string), no como null, y el
  // backend no puede convertir eso a número — hay que pasarlo a null antes de mandarlo.
  // eslint-disable-next-line @typescript-eslint/no-explicit-any
  private normalizarNumeros(campos: any): any {
    const camposNumericos = [...CAMPOS_COMUNES, ...this.camposEspecificos()]
      .filter((campo) => campo.type === 'number')
      .map((campo) => campo.key);

    const normalizados = { ...campos };
    for (const key of camposNumericos) {
      if (normalizados[key] === '') normalizados[key] = null;
    }
    return normalizados;
  }

  private calcularCamposFaltantes(): string[] {
    const todos = [...CAMPOS_COMUNES, ...this.camposEspecificos()];
    const faltantes = todos.filter((campo) => this.form.get(campo.key)?.invalid).map((campo) => campo.label);

    if (!this.tipoSeleccionado()) faltantes.unshift('Tipo de propiedad');

    const entreCalle1 = this.form.get('entreCalle1')?.value;
    const entreCalle2 = this.form.get('entreCalle2')?.value;
    if (entreCalle1 && !entreCalle2) faltantes.push('Entre calle 2');
    if (entreCalle2 && !entreCalle1) faltantes.push('Entre calle 1');

    return faltantes;
  }

  guardar(): void {
    const faltantes = this.calcularCamposFaltantes();

    if (faltantes.length > 0) {
      this.camposFaltantes.set(faltantes);
      this.form.markAllAsTouched();
      return;
    }

    this.camposFaltantes.set([]);
    this.guardando.set(true);
    this.error.set(false);

    const campos = this.normalizarNumeros(this.form.getRawValue());
    const imagenes = this.imagenes().map((img, orden) => ({ url: img.url, orden }));

    // "tipo" tiene que ir primero en el objeto: el backend lo necesita como la primera
    // propiedad del JSON para reconocer de qué subtipo es la propiedad (Casa, Depto, etc).
    const propiedad = { tipo: this.tipoSeleccionado(), ...campos, imagenes };
    const alTerminar = {
      next: () => this.router.navigate(['/admin/panel']),
      error: () => {
        this.guardando.set(false);
        this.error.set(true);
      },
    };

    if (this.esEdicion() && this.propiedadId) {
      this.propiedadesService.update(this.propiedadId, { ...propiedad, id: this.propiedadId }).subscribe(alTerminar);
    } else {
      this.propiedadesService.create(propiedad).subscribe(alTerminar);
    }
  }
}
