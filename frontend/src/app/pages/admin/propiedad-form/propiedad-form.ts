import { Component, OnInit, computed, inject, signal } from '@angular/core';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { ActivatedRoute, Router, RouterLink } from '@angular/router';
import { CdkDragDrop, DragDropModule, moveItemInArray } from '@angular/cdk/drag-drop';
import { PropiedadesService } from '../../../core/propiedades.service';
import { ImagenesService } from '../../../core/imagenes.service';
import { imagenUrl } from '../../../core/api-config';
import { Propiedad, TipoPropiedad } from '../../../core/models/propiedad';
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

@Component({
  selector: 'app-propiedad-form',
  imports: [ReactiveFormsModule, DragDropModule, RouterLink],
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
  protected readonly TIPOS_PROPIEDAD = TIPOS_PROPIEDAD;
  protected readonly TIPO_LABELS = TIPO_LABELS;
  protected readonly etiquetaOpcion = etiquetaOpcion;

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
    return !!control && control.invalid && control.touched;
  }

  subirImagenes(event: Event): void {
    const input = event.target as HTMLInputElement;
    const archivos = input.files ? Array.from(input.files) : [];
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

    input.value = '';
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
