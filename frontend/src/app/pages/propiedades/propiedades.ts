import { Component, OnInit, computed, inject, signal } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { FormBuilder, ReactiveFormsModule } from '@angular/forms';
import { debounceTime } from 'rxjs';
import { FiltrosPropiedades, PropiedadesService } from '../../core/propiedades.service';
import { Moneda, Propiedad, TipoPropiedad, Operacion } from '../../core/models/propiedad';
import { PropiedadCard } from '../../shared/propiedad-card/propiedad-card';
import { TIPOS_PROPIEDAD, TIPO_LABELS, etiquetaOpcion } from '../../core/property-form-config';
import { SeoService } from '../../core/seo.service';

const OPERACIONES: Operacion[] = ['Venta', 'Alquiler', 'AlquilerTemporario', 'Remate'];
const MONEDAS: { valor: Moneda; etiqueta: string }[] = [
  { valor: 'Pesos', etiqueta: 'Pesos (ARS)' },
  { valor: 'Dolares', etiqueta: 'Dólares (U$D)' },
];
const POR_PAGINA = 10;
type Orden = 'recientes' | 'precioAsc' | 'precioDesc';

@Component({
  selector: 'app-propiedades',
  imports: [PropiedadCard, ReactiveFormsModule],
  templateUrl: './propiedades.html',
  styleUrl: './propiedades.scss',
})
export class Propiedades implements OnInit {
  private readonly propiedadesService = inject(PropiedadesService);
  private readonly route = inject(ActivatedRoute);
  private readonly fb = inject(FormBuilder);
  private readonly seo = inject(SeoService);

  protected readonly TIPOS_PROPIEDAD = TIPOS_PROPIEDAD;
  protected readonly TIPO_LABELS = TIPO_LABELS;
  protected readonly OPERACIONES = OPERACIONES;
  protected readonly MONEDAS = MONEDAS;
  protected readonly etiquetaOpcion = etiquetaOpcion;

  protected readonly cargando = signal(true);
  protected readonly error = signal(false);
  protected readonly orden = signal<Orden>('recientes');
  protected readonly paginaActual = signal(1);
  // Solo se usa en mobile: en escritorio el sidebar de filtros siempre está visible.
  protected readonly filtrosAbiertos = signal(false);

  private readonly resultado = signal<Propiedad[]>([]);
  protected readonly propiedadesOrdenadas = computed(() => {
    const lista = [...this.resultado()];
    switch (this.orden()) {
      case 'precioAsc':
        return lista.sort((a, b) => (a.monto ?? Infinity) - (b.monto ?? Infinity));
      case 'precioDesc':
        return lista.sort((a, b) => (b.monto ?? -Infinity) - (a.monto ?? -Infinity));
      default:
        return lista.sort((a, b) => b.id - a.id);
    }
  });

  protected readonly totalPaginas = computed(() =>
    Math.max(1, Math.ceil(this.propiedadesOrdenadas().length / POR_PAGINA)),
  );

  protected readonly paginas = computed(() => Array.from({ length: this.totalPaginas() }, (_, i) => i + 1));

  protected readonly propiedades = computed(() => {
    const inicio = (this.paginaActual() - 1) * POR_PAGINA;
    return this.propiedadesOrdenadas().slice(inicio, inicio + POR_PAGINA);
  });

  protected readonly form = this.fb.nonNullable.group({
    tipo: [''],
    operacion: [''],
    moneda: [''],
    ubicacion: [''],
    precioMin: [''],
    precioMax: [''],
  });

  ngOnInit(): void {
    this.seo.actualizar(
      'Propiedades',
      'Explorá casas, departamentos y locales en venta y alquiler en Quilmes y Zona Sur.',
    );

    const queryParams = this.route.snapshot.queryParamMap;
    this.form.patchValue({
      tipo: queryParams.get('tipo') ?? '',
      operacion: queryParams.get('operacion') ?? '',
      moneda: queryParams.get('moneda') ?? '',
      ubicacion: queryParams.get('ubicacion') ?? '',
      precioMin: queryParams.get('precioMin') ?? '',
      precioMax: queryParams.get('precioMax') ?? '',
    });

    this.buscar();

    // Los radios (tipo/operación/moneda) filtran al toque; ubicación y precio esperan
    // un momento a que la persona termine de tipear, para no buscar en cada letra.
    this.form.valueChanges.pipe(debounceTime(350)).subscribe(() => this.buscar());
  }

  private buscar(): void {
    this.cargando.set(true);
    this.error.set(false);
    this.paginaActual.set(1);

    const valores = this.form.getRawValue();
    const filtros: FiltrosPropiedades = {
      tipo: (valores.tipo || undefined) as TipoPropiedad | undefined,
      operacion: (valores.operacion || undefined) as Operacion | undefined,
      moneda: (valores.moneda || undefined) as Moneda | undefined,
      ubicacion: valores.ubicacion || undefined,
      precioMin: valores.precioMin ? Number(valores.precioMin) : undefined,
      precioMax: valores.precioMax ? Number(valores.precioMax) : undefined,
    };

    this.propiedadesService.getAll(filtros).subscribe({
      next: (propiedades) => {
        this.resultado.set(propiedades);
        this.cargando.set(false);
      },
      error: () => {
        this.error.set(true);
        this.cargando.set(false);
      },
    });
  }

  limpiarFiltros(): void {
    this.form.reset({ tipo: '', operacion: '', moneda: '', ubicacion: '', precioMin: '', precioMax: '' });
  }

  alternarFiltros(): void {
    this.filtrosAbiertos.update((abiertos) => !abiertos);
  }

  cambiarOrden(event: Event): void {
    this.orden.set((event.target as HTMLSelectElement).value as Orden);
    this.paginaActual.set(1);
  }

  irAPagina(pagina: number): void {
    this.paginaActual.set(pagina);
    window.scrollTo({ top: 0, behavior: 'smooth' });
  }
}
