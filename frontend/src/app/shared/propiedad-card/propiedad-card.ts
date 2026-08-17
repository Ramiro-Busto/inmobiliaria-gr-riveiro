import { DecimalPipe } from '@angular/common';
import { Component, Input, computed, inject, signal } from '@angular/core';
import { RouterLink } from '@angular/router';
import { FormBuilder, ReactiveFormsModule, Validators } from '@angular/forms';
import { Propiedad } from '../../core/models/propiedad';
import { imagenUrl } from '../../core/api-config';
import { TIPO_LABELS, etiquetaOpcion, etiquetaMoneda } from '../../core/property-form-config';
import { ConsultasService } from '../../core/consultas.service';
import { linkWhatsAppPropiedad } from '../whatsapp';
import { Icon, IconName } from '../icon/icon';

interface Caracteristica {
  icono: IconName;
  texto: string;
}

const CANTIDAD_MAXIMA_CARACTERISTICAS = 3;

// El listado de "candidatas" a mostrar como ícono en la tarjeta, en orden de prioridad.
// Cada propiedad muestra las primeras 3 de esta lista que realmente tenga cargadas
// (varía según el tipo: un Terreno no tiene dormitorios, una Casa no tiene FOT, etc).
function calcularCaracteristicas(propiedad: Propiedad): Caracteristica[] {
  const superficieTotal = propiedad['superficieTotal'] as number | undefined;
  const superficieCubierta = propiedad['superficieCubierta'] as number | undefined;
  const dormitorios = propiedad['cantidadDormitorios'] as number | undefined;
  const banos = propiedad['cantidadBanos'] as number | undefined;
  const esAEstrenar = propiedad['esAEstrenar'] as boolean | undefined;
  const tipoUsoTerreno = propiedad['tipoUsoTerreno'] as string | undefined;
  const fotPercent = propiedad['fotPercent'] as number | undefined;

  const candidatas: (Caracteristica | null)[] = [
    superficieTotal ? { icono: 'maximize', texto: `${superficieTotal} m²` } : null,
    dormitorios ? { icono: 'home', texto: `${dormitorios} dorm${dormitorios === 1 ? '' : 's'}.` } : null,
    banos ? { icono: 'droplet', texto: `${banos} baño${banos === 1 ? '' : 's'}` } : null,
    esAEstrenar ? { icono: 'star', texto: 'A estrenar' } : null,
    fotPercent ? { icono: 'maximize', texto: `FOT ${fotPercent}` } : null,
    tipoUsoTerreno ? { icono: 'home', texto: tipoUsoTerreno } : null,
    superficieCubierta ? { icono: 'maximize', texto: `${superficieCubierta} m² cub.` } : null,
  ];

  return candidatas.filter((c): c is Caracteristica => c !== null).slice(0, CANTIDAD_MAXIMA_CARACTERISTICAS);
}

@Component({
  selector: 'app-propiedad-card',
  imports: [RouterLink, DecimalPipe, ReactiveFormsModule, Icon],
  templateUrl: './propiedad-card.html',
  styleUrl: './propiedad-card.scss',
})
export class PropiedadCard {
  private readonly fb = inject(FormBuilder);
  private readonly consultasService = inject(ConsultasService);

  @Input({ required: true }) propiedad!: Propiedad;

  protected readonly imagenUrl = imagenUrl;
  protected readonly TIPO_LABELS = TIPO_LABELS;
  protected readonly etiquetaOpcion = etiquetaOpcion;
  protected readonly etiquetaMoneda = etiquetaMoneda;

  protected readonly linkWhatsapp = computed(() =>
    linkWhatsAppPropiedad(`${window.location.origin}/propiedades/${this.propiedad.id}`),
  );

  protected readonly caracteristicas = computed(() => calcularCaracteristicas(this.propiedad));

  protected readonly expensas = computed(() => {
    const monto = this.propiedad['expensasMonto'] as number | undefined;
    return monto ? monto : null;
  });

  protected readonly modalAbierto = signal(false);
  protected readonly enviando = signal(false);
  protected readonly enviado = signal(false);
  protected readonly error = signal(false);

  protected readonly form = this.fb.nonNullable.group({
    nombre: ['', Validators.required],
    telefono: ['', Validators.required],
    email: ['', [Validators.required, Validators.email]],
    mensaje: ['', Validators.required],
  });

  abrirContacto(event: Event): void {
    event.preventDefault();
    event.stopPropagation();

    this.enviado.set(false);
    this.error.set(false);
    this.form.reset({
      nombre: '',
      telefono: '',
      email: '',
      mensaje: `Hola, vi esta propiedad ("${this.propiedad.titulo}") en el sitio web de la inmobiliaria y me gustaría que me contacten. Gracias.`,
    });
    this.modalAbierto.set(true);
  }

  cerrarContacto(): void {
    this.modalAbierto.set(false);
  }

  enviarContacto(): void {
    if (this.form.invalid) {
      this.form.markAllAsTouched();
      return;
    }

    this.enviando.set(true);
    this.error.set(false);

    this.consultasService
      .create({ ...this.form.getRawValue(), propiedadId: this.propiedad.id, tipo: 'Contacto' })
      .subscribe({
        next: () => {
          this.enviando.set(false);
          this.enviado.set(true);
        },
        error: () => {
          this.enviando.set(false);
          this.error.set(true);
        },
      });
  }
}
