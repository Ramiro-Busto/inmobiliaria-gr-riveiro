import { Component, inject, signal } from '@angular/core';
import { FormBuilder, ReactiveFormsModule, Validators } from '@angular/forms';
import { ConsultasService } from '../../core/consultas.service';
import { Icon } from '../../shared/icon/icon';
import { TIPOS_PROPIEDAD, TIPO_LABELS } from '../../core/property-form-config';
import { TipoPropiedad } from '../../core/models/propiedad';
import { SeoService } from '../../core/seo.service';

@Component({
  selector: 'app-tasaciones',
  imports: [ReactiveFormsModule, Icon],
  templateUrl: './tasaciones.html',
  styleUrl: './tasaciones.scss',
})
export class Tasaciones {
  private readonly fb = inject(FormBuilder);
  private readonly consultasService = inject(ConsultasService);

  protected readonly tiposPropiedad = TIPOS_PROPIEDAD;
  protected readonly TIPO_LABELS = TIPO_LABELS;

  protected readonly enviando = signal(false);
  protected readonly enviado = signal(false);
  protected readonly error = signal(false);
  // Solo mostramos errores de validación después de intentar enviar (no al salir de un campo).
  protected readonly intentoEnviar = signal(false);

  protected readonly form = this.fb.nonNullable.group({
    nombre: ['', Validators.required],
    apellido: ['', Validators.required],
    telefono: ['', Validators.required],
    email: ['', [Validators.required, Validators.email]],
    tipoPropiedad: ['', Validators.required],
  });

  constructor() {
    inject(SeoService).actualizar(
      'Tasaciones',
      'Solicitá una tasación gratuita de tu propiedad en Quilmes y Zona Sur con GR Riveiro Negocios Inmobiliarios.',
    );
  }

  enviar(): void {
    this.intentoEnviar.set(true);

    if (this.form.invalid) {
      return;
    }

    this.enviando.set(true);
    this.error.set(false);

    const { nombre, apellido, telefono, email, tipoPropiedad } = this.form.getRawValue();

    this.consultasService
      .create({
        propiedadId: null,
        nombre,
        apellido,
        telefono,
        email,
        tipoPropiedad: tipoPropiedad as TipoPropiedad,
        tipo: 'Tasacion',
      })
      .subscribe({
        next: () => {
          this.enviando.set(false);
          this.enviado.set(true);
          this.form.reset();
        },
        error: () => {
          this.enviando.set(false);
          this.error.set(true);
        },
      });
  }
}
