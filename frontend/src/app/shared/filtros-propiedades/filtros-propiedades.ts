import { Component, EventEmitter, Input, OnChanges, Output, inject } from '@angular/core';
import { FormBuilder, ReactiveFormsModule } from '@angular/forms';
import { FiltrosPropiedades } from '../../core/propiedades.service';
import { TIPOS_PROPIEDAD, TIPO_LABELS, etiquetaOpcion } from '../../core/property-form-config';

const OPERACIONES = ['Venta', 'Alquiler', 'AlquilerTemporario', 'Remate'] as const;

// Buscador de propiedades: se usa tal cual tanto en Inicio como en el listado de
// Propiedades, para que el filtro se comporte igual en los dos lugares.
@Component({
  selector: 'app-filtros-propiedades',
  imports: [ReactiveFormsModule],
  templateUrl: './filtros-propiedades.html',
  styleUrl: './filtros-propiedades.scss',
})
export class FiltrosPropiedadesComponent implements OnChanges {
  private readonly fb = inject(FormBuilder);

  @Input() valoresIniciales: Partial<FiltrosPropiedades> = {};
  @Output() readonly buscar = new EventEmitter<FiltrosPropiedades>();

  protected readonly TIPOS_PROPIEDAD = TIPOS_PROPIEDAD;
  protected readonly TIPO_LABELS = TIPO_LABELS;
  protected readonly OPERACIONES = OPERACIONES;
  protected readonly etiquetaOpcion = etiquetaOpcion;

  protected readonly form = this.fb.nonNullable.group({
    tipo: [''],
    operacion: [''],
    ubicacion: [''],
    precioMin: [''],
    precioMax: [''],
  });

  ngOnChanges(): void {
    this.form.patchValue({
      tipo: this.valoresIniciales.tipo ?? '',
      operacion: this.valoresIniciales.operacion ?? '',
      ubicacion: this.valoresIniciales.ubicacion ?? '',
      precioMin: this.valoresIniciales.precioMin?.toString() ?? '',
      precioMax: this.valoresIniciales.precioMax?.toString() ?? '',
    });
  }

  emitirBusqueda(): void {
    const valores = this.form.getRawValue();

    this.buscar.emit({
      tipo: (valores.tipo || undefined) as FiltrosPropiedades['tipo'],
      operacion: (valores.operacion || undefined) as FiltrosPropiedades['operacion'],
      ubicacion: valores.ubicacion || undefined,
      precioMin: valores.precioMin ? Number(valores.precioMin) : undefined,
      precioMax: valores.precioMax ? Number(valores.precioMax) : undefined,
    });
  }

  limpiar(): void {
    this.form.reset({ tipo: '', operacion: '', ubicacion: '', precioMin: '', precioMax: '' });
    this.emitirBusqueda();
  }
}
