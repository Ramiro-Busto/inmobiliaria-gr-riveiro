import { Component, OnInit, inject, signal } from '@angular/core';
import { Router, RouterLink } from '@angular/router';
import { AuthService } from '../../../core/auth.service';
import { PropiedadesService } from '../../../core/propiedades.service';
import { ConsultasService } from '../../../core/consultas.service';
import { Propiedad } from '../../../core/models/propiedad';
import { Consulta } from '../../../core/models/consulta';
import { TIPO_LABELS } from '../../../core/property-form-config';
import { imagenUrl } from '../../../core/api-config';

@Component({
  selector: 'app-panel',
  imports: [RouterLink],
  templateUrl: './panel.html',
  styleUrl: './panel.scss',
})
export class Panel implements OnInit {
  private readonly authService = inject(AuthService);
  private readonly router = inject(Router);
  private readonly propiedadesService = inject(PropiedadesService);
  private readonly consultasService = inject(ConsultasService);

  protected readonly TIPO_LABELS = TIPO_LABELS;
  protected readonly propiedades = signal<Propiedad[]>([]);
  protected readonly consultas = signal<Consulta[]>([]);

  ngOnInit(): void {
    this.cargarPropiedades();
    this.cargarConsultas();
  }

  private cargarPropiedades(): void {
    this.propiedadesService.getAllParaAdmin().subscribe((propiedades) => this.propiedades.set(propiedades));
  }

  private cargarConsultas(): void {
    this.consultasService.getAll().subscribe((consultas) => this.consultas.set(consultas));
  }

  // Portada = la foto de menor "orden" (la que se ve primero en la ficha pública).
  protected portada(p: Propiedad): string | null {
    if (p.imagenes.length === 0) return null;
    const primera = [...p.imagenes].sort((a, b) => a.orden - b.orden)[0];
    return imagenUrl(primera.url);
  }

  eliminarPropiedad(id: number): void {
    if (!confirm('¿Eliminar esta propiedad?')) return;

    this.propiedadesService.delete(id).subscribe(() => this.cargarPropiedades());
  }

  marcarConsultaLeida(id: number): void {
    this.consultasService.marcarLeida(id).subscribe(() => this.cargarConsultas());
  }

  cerrarSesion(): void {
    this.authService.logout();
    this.router.navigate(['/admin']);
  }
}
