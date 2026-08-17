import { DecimalPipe } from '@angular/common';
import { Component, OnInit, computed, inject, signal } from '@angular/core';
import { DomSanitizer, SafeResourceUrl } from '@angular/platform-browser';
import { ActivatedRoute, RouterLink } from '@angular/router';
import { FormBuilder, ReactiveFormsModule, Validators } from '@angular/forms';
import { PropiedadesService } from '../../core/propiedades.service';
import { ConsultasService } from '../../core/consultas.service';
import { Propiedad } from '../../core/models/propiedad';
import { linkWhatsAppPropiedad } from '../../shared/whatsapp';
import { imagenUrl } from '../../core/api-config';
import { Icon, IconName } from '../../shared/icon/icon';
import { PropiedadCard } from '../../shared/propiedad-card/propiedad-card';
import {
  TIPO_LABELS,
  etiquetaOpcion,
  etiquetaMoneda,
  datosBasicos,
  caracteristicasEspecificas,
  listaServicios,
  listaAmenities,
} from '../../core/property-form-config';

type SeccionAcordeon = 'basicos' | 'caracteristicas' | 'servicios' | 'amenities';

interface TarjetaCaracteristica {
  icono: IconName;
  valor: string;
  etiqueta: string;
}

const MAXIMO_TARJETAS = 4;

// Igual idea que en la tarjeta de propiedad (lista de candidatas en orden de
// prioridad, se muestran las que esa propiedad realmente tenga cargadas) pero acá
// separamos valor y etiqueta porque se dibujan en dos líneas dentro de cada tarjeta.
function calcularTarjetas(propiedad: Propiedad): TarjetaCaracteristica[] {
  const dormitorios = propiedad['cantidadDormitorios'] as number | undefined;
  const banos = propiedad['cantidadBanos'] as number | undefined;
  const superficieTotal = propiedad['superficieTotal'] as number | undefined;
  const superficieCubierta = propiedad['superficieCubierta'] as number | undefined;

  const candidatas: (TarjetaCaracteristica | null)[] = [
    dormitorios ? { icono: 'home', valor: String(dormitorios), etiqueta: dormitorios === 1 ? 'Dormitorio' : 'Dormitorios' } : null,
    banos ? { icono: 'droplet', valor: String(banos), etiqueta: banos === 1 ? 'Baño' : 'Baños' } : null,
    superficieTotal ? { icono: 'maximize', valor: `${superficieTotal} m²`, etiqueta: 'Superficie total' } : null,
    superficieCubierta ? { icono: 'maximize', valor: `${superficieCubierta} m²`, etiqueta: 'Sup. cubierta' } : null,
  ];

  return candidatas.filter((c): c is TarjetaCaracteristica => c !== null).slice(0, MAXIMO_TARJETAS);
}

const CANTIDAD_SIMILARES = 3;

// Puntaje de "qué tan parecida" es una propiedad a la que se está viendo: mismo
// tipo pesa más, después la operación, la zona y que el precio ronde lo mismo.
function calcularSimilitud(base: Propiedad, candidata: Propiedad): number {
  let puntaje = 0;

  if (candidata.tipo === base.tipo) puntaje += 4;
  if (candidata.operacion && candidata.operacion === base.operacion) puntaje += 2;
  if (candidata.partidoLocalidad === base.partidoLocalidad) puntaje += 2;
  if (candidata.barrioCiudad === base.barrioCiudad) puntaje += 1;

  if (base.monto && candidata.monto && base.moneda === candidata.moneda) {
    const diferencia = Math.abs(candidata.monto - base.monto) / base.monto;
    if (diferencia <= 0.25) puntaje += 2;
    else if (diferencia <= 0.5) puntaje += 1;
  }

  return puntaje;
}

@Component({
  selector: 'app-propiedad-detalle',
  imports: [DecimalPipe, RouterLink, Icon, ReactiveFormsModule, PropiedadCard],
  templateUrl: './propiedad-detalle.html',
  styleUrl: './propiedad-detalle.scss',
})
export class PropiedadDetalle implements OnInit {
  private readonly route = inject(ActivatedRoute);
  private readonly propiedadesService = inject(PropiedadesService);
  private readonly consultasService = inject(ConsultasService);
  private readonly sanitizer = inject(DomSanitizer);
  private readonly fb = inject(FormBuilder);

  protected readonly imagenUrl = imagenUrl;
  protected readonly TIPO_LABELS = TIPO_LABELS;
  protected readonly etiquetaOpcion = etiquetaOpcion;
  protected readonly etiquetaMoneda = etiquetaMoneda;

  protected readonly propiedad = signal<Propiedad | null>(null);
  protected readonly similares = signal<Propiedad[]>([]);
  protected readonly cargando = signal(true);
  protected readonly error = signal(false);

  protected readonly indiceFoto = signal(0);

  protected readonly linkWhatsapp = computed(() => {
    const id = this.propiedad()?.id;
    const urlPropiedad = `${window.location.origin}/propiedades/${id}`;
    return linkWhatsAppPropiedad(urlPropiedad);
  });

  protected readonly tarjetas = computed(() => {
    const p = this.propiedad();
    return p ? calcularTarjetas(p) : [];
  });

  // Dirección completa para el renglón de arriba del mapa: solo suma lo que esté cargado
  // (piso/depto, entre calles), sin dejar huecos raros si la propiedad no los tiene.
  protected readonly direccionCompleta = computed(() => {
    const p = this.propiedad();
    if (!p) return '';

    const calleYNro = [p.calle, p['nroCalle']].filter(Boolean).join(' ');
    const pisoDepto = [p['piso'] ? `Piso ${p['piso']}` : null, p['depto'] ? `Depto ${p['depto']}` : null]
      .filter(Boolean)
      .join(', ');

    return [calleYNro, pisoDepto, p.barrioCiudad, p.partidoLocalidad].filter(Boolean).join(', ');
  });

  protected readonly entreCalles = computed(() => {
    const p = this.propiedad();
    if (!p) return null;

    const calle1 = p['entreCalle1'] as string | undefined;
    const calle2 = p['entreCalle2'] as string | undefined;
    if (calle1 && calle2) return `entre ${calle1} y ${calle2}`;
    return null;
  });

  protected readonly datosBasicos = computed(() => {
    const p = this.propiedad();
    return p ? datosBasicos(p) : [];
  });

  protected readonly caracteristicas = computed(() => {
    const p = this.propiedad();
    return p ? caracteristicasEspecificas(p.tipo, p) : [];
  });

  protected readonly servicios = computed(() => {
    const p = this.propiedad();
    return p ? listaServicios(p.tipo, p) : [];
  });

  protected readonly amenities = computed(() => {
    const p = this.propiedad();
    return p ? listaAmenities(p.tipo, p) : [];
  });

  protected readonly expensas = computed(() => {
    const p = this.propiedad();
    const monto = p?.['expensasMonto'] as number | undefined;
    return monto ? monto : null;
  });

  protected readonly tieneMapa = computed(() => {
    const p = this.propiedad();
    return !!(p && p['latitud'] && p['longitud']);
  });

  protected readonly mapaSrc = computed<SafeResourceUrl>(() => {
    const p = this.propiedad();
    if (!p) return '';
    const lat = p['latitud'] as number;
    const lon = p['longitud'] as number;
    const delta = 0.006;
    const bbox = `${lon - delta}%2C${lat - delta}%2C${lon + delta}%2C${lat + delta}`;
    const url = `https://www.openstreetmap.org/export/embed.html?bbox=${bbox}&layer=mapnik&marker=${lat}%2C${lon}`;
    return this.sanitizer.bypassSecurityTrustResourceUrl(url);
  });

  protected readonly mapaLinkGrande = computed(() => {
    const p = this.propiedad();
    if (!p) return '';
    const lat = p['latitud'] as number;
    const lon = p['longitud'] as number;
    return `https://www.openstreetmap.org/?mlat=${lat}&mlon=${lon}#map=16/${lat}/${lon}`;
  });

  private readonly seccionesAbiertas = signal<Set<SeccionAcordeon>>(new Set(['basicos']));

  protected readonly enviandoConsulta = signal(false);
  protected readonly consultaEnviada = signal(false);
  protected readonly errorConsulta = signal(false);

  protected readonly formConsulta = this.fb.nonNullable.group({
    nombre: ['', Validators.required],
    email: ['', [Validators.required, Validators.email]],
    telefono: ['', Validators.required],
    mensaje: ['', Validators.required],
  });

  ngOnInit(): void {
    const id = Number(this.route.snapshot.paramMap.get('id'));

    this.propiedadesService.getById(id).subscribe({
      next: (propiedad) => {
        this.propiedad.set(propiedad);
        this.cargando.set(false);
        this.formConsulta.patchValue({
          mensaje: `Hola, me interesa la propiedad "${propiedad.titulo}" en ${propiedad.calle} ${propiedad['nroCalle'] ?? ''}.`,
        });
        this.cargarSimilares(propiedad);
      },
      error: () => {
        this.error.set(true);
        this.cargando.set(false);
      },
    });
  }

  private cargarSimilares(propiedad: Propiedad): void {
    this.propiedadesService.getAll().subscribe((todas) => {
      const ordenadas = todas
        .filter((candidata) => candidata.id !== propiedad.id)
        .map((candidata) => ({ candidata, puntaje: calcularSimilitud(propiedad, candidata) }))
        .sort((a, b) => b.puntaje - a.puntaje)
        .slice(0, CANTIDAD_SIMILARES)
        .map((entrada) => entrada.candidata);

      this.similares.set(ordenadas);
    });
  }

  fotoAnterior(): void {
    const total = this.propiedad()?.imagenes.length ?? 0;
    this.indiceFoto.update((i) => (i - 1 + total) % total);
  }

  fotoSiguiente(): void {
    const total = this.propiedad()?.imagenes.length ?? 0;
    this.indiceFoto.update((i) => (i + 1) % total);
  }

  irAFoto(indice: number): void {
    this.indiceFoto.set(indice);
  }

  estaAbierta(seccion: SeccionAcordeon): boolean {
    return this.seccionesAbiertas().has(seccion);
  }

  alternarSeccion(seccion: SeccionAcordeon): void {
    this.seccionesAbiertas.update((actuales) => {
      const nuevas = new Set(actuales);
      if (nuevas.has(seccion)) {
        nuevas.delete(seccion);
      } else {
        nuevas.add(seccion);
      }
      return nuevas;
    });
  }

  enviarConsulta(): void {
    if (this.formConsulta.invalid) {
      this.formConsulta.markAllAsTouched();
      return;
    }

    this.enviandoConsulta.set(true);
    this.errorConsulta.set(false);

    this.consultasService
      .create({ ...this.formConsulta.getRawValue(), propiedadId: this.propiedad()!.id, tipo: 'Contacto' })
      .subscribe({
        next: () => {
          this.enviandoConsulta.set(false);
          this.consultaEnviada.set(true);
        },
        error: () => {
          this.enviandoConsulta.set(false);
          this.errorConsulta.set(true);
        },
      });
  }
}
