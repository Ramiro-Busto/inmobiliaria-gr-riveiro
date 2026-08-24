import { Component, OnInit, computed, inject, signal } from '@angular/core';
import { Router, RouterLink } from '@angular/router';
import { FiltrosPropiedadesComponent } from '../../shared/filtros-propiedades/filtros-propiedades';
import { PropiedadCard } from '../../shared/propiedad-card/propiedad-card';
import { Icon } from '../../shared/icon/icon';
import { FiltrosPropiedades, PropiedadesService } from '../../core/propiedades.service';
import { Propiedad } from '../../core/models/propiedad';
import { SeoService } from '../../core/seo.service';

const CANTIDAD_POR_TANDA = 9;

@Component({
  selector: 'app-inicio',
  imports: [FiltrosPropiedadesComponent, PropiedadCard, Icon, RouterLink],
  templateUrl: './inicio.html',
  styleUrl: './inicio.scss',
})
export class Inicio implements OnInit {
  private readonly router = inject(Router);
  private readonly propiedadesService = inject(PropiedadesService);
  private readonly seo = inject(SeoService);

  private readonly todas = signal<Propiedad[]>([]);
  protected readonly cantidadMostrada = signal(CANTIDAD_POR_TANDA);
  protected readonly cargandoMas = signal(false);

  protected readonly propiedades = computed(() => this.todas().slice(0, this.cantidadMostrada()));
  protected readonly hayMas = computed(() => this.cantidadMostrada() < this.todas().length);

  ngOnInit(): void {
    this.seo.actualizar(
      null,
      'Inmobiliaria en Quilmes, Zona Sur. Venta y alquiler de casas, departamentos y locales, con acompañamiento profesional en todo el proceso.',
    );
    this.propiedadesService.getAll().subscribe((propiedades) => this.todas.set(propiedades));
  }

  verMas(): void {
    this.cargandoMas.set(true);

    // Ya tenemos todas las propiedades cargadas del pedido inicial; el "cargando" es
    // solo para que se sienta como que trae una tanda nueva, tal como en el sitio actual.
    setTimeout(() => {
      this.cantidadMostrada.update((n) => n + CANTIDAD_POR_TANDA);
      this.cargandoMas.set(false);
    }, 400);
  }

  buscar(filtros: FiltrosPropiedades): void {
    this.router.navigate(['/propiedades'], { queryParams: filtros });
  }
}
