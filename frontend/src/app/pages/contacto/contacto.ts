import { Component, inject, signal } from '@angular/core';
import { DomSanitizer, SafeResourceUrl } from '@angular/platform-browser';
import { FormBuilder, ReactiveFormsModule, Validators } from '@angular/forms';
import { ConsultasService } from '../../core/consultas.service';
import { Icon } from '../../shared/icon/icon';
import { linkWhatsApp } from '../../shared/whatsapp';
import { MAIL_CONTACTO, TELEFONO_DISPLAY, TELEFONO_TEL } from '../../shared/redes';
import { SeoService } from '../../core/seo.service';

// Coordenadas reales de "Av. 12 de Octubre N° 463, Quilmes" (geocodificadas con OpenStreetMap).
const OFICINA_LAT = -34.7291798;
const OFICINA_LON = -58.2631071;

@Component({
  selector: 'app-contacto',
  imports: [ReactiveFormsModule, Icon],
  templateUrl: './contacto.html',
  styleUrl: './contacto.scss',
})
export class Contacto {
  private readonly fb = inject(FormBuilder);
  private readonly consultasService = inject(ConsultasService);
  private readonly sanitizer = inject(DomSanitizer);

  protected readonly mail = MAIL_CONTACTO;
  protected readonly telefonoDisplay = TELEFONO_DISPLAY;
  protected readonly linkTelefono = `tel:${TELEFONO_TEL}`;
  protected readonly linkMail = `mailto:${MAIL_CONTACTO}`;
  protected readonly linkWhatsapp = linkWhatsApp('Hola, quisiera contactarme con GR Riveiro Negocios Inmobiliarios.');

  protected readonly mapaSrc: SafeResourceUrl = (() => {
    const delta = 0.006;
    const bbox = `${OFICINA_LON - delta}%2C${OFICINA_LAT - delta}%2C${OFICINA_LON + delta}%2C${OFICINA_LAT + delta}`;
    const url = `https://www.openstreetmap.org/export/embed.html?bbox=${bbox}&layer=mapnik&marker=${OFICINA_LAT}%2C${OFICINA_LON}`;
    return this.sanitizer.bypassSecurityTrustResourceUrl(url);
  })();

  protected readonly mapaLinkGrande = `https://www.openstreetmap.org/?mlat=${OFICINA_LAT}&mlon=${OFICINA_LON}#map=17/${OFICINA_LAT}/${OFICINA_LON}`;

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
    mensaje: ['Hola, quiero que me contacten. Gracias.', Validators.required],
  });

  constructor() {
    inject(SeoService).actualizar(
      'Contacto',
      'Comunicate con GR Riveiro Negocios Inmobiliarios en Quilmes, Zona Sur. Av. 12 de Octubre N° 463.',
    );
  }

  // Alto automático: crece con el texto en vez de dejar que se arrastre a mano.
  autoAjustarAltura(textarea: HTMLTextAreaElement): void {
    textarea.style.height = 'auto';
    textarea.style.height = `${textarea.scrollHeight}px`;
  }

  enviar(): void {
    this.intentoEnviar.set(true);

    if (this.form.invalid) {
      return;
    }

    this.enviando.set(true);
    this.error.set(false);

    this.consultasService
      .create({ ...this.form.getRawValue(), propiedadId: null, tipo: 'Contacto' })
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
