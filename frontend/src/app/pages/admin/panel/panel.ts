import { Component, OnInit, inject, signal } from '@angular/core';
import { Router, RouterLink } from '@angular/router';
import { FormBuilder, ReactiveFormsModule, Validators } from '@angular/forms';
import { AuthService } from '../../../core/auth.service';
import { PropiedadesService } from '../../../core/propiedades.service';
import { ConsultasService } from '../../../core/consultas.service';
import { Propiedad } from '../../../core/models/propiedad';
import { Consulta } from '../../../core/models/consulta';
import { TIPO_LABELS } from '../../../core/property-form-config';
import { imagenUrl } from '../../../core/api-config';
import { Icon } from '../../../shared/icon/icon';

@Component({
  selector: 'app-panel',
  imports: [RouterLink, ReactiveFormsModule, Icon],
  templateUrl: './panel.html',
  styleUrl: './panel.scss',
})
export class Panel implements OnInit {
  private readonly authService = inject(AuthService);
  private readonly router = inject(Router);
  private readonly propiedadesService = inject(PropiedadesService);
  private readonly consultasService = inject(ConsultasService);
  private readonly fb = inject(FormBuilder);

  protected readonly TIPO_LABELS = TIPO_LABELS;
  protected readonly propiedades = signal<Propiedad[]>([]);
  protected readonly consultas = signal<Consulta[]>([]);

  protected readonly formPassword = this.fb.nonNullable.group({
    passwordActual: ['', Validators.required],
    passwordNueva: ['', [Validators.required, Validators.minLength(6)]],
    passwordConfirmar: ['', Validators.required],
  });
  protected readonly cambiandoPassword = signal(false);
  protected readonly passwordExito = signal(false);
  // Solo se muestran errores después de intentar enviar (mismo criterio que el resto del sitio).
  protected readonly intentoCambiarPassword = signal(false);
  // Este solo lo sabemos después de que el backend responde, a diferencia del resto
  // de las validaciones (que son locales al formulario).
  protected readonly passwordActualIncorrecta = signal(false);

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

  // Los 3 mensajes de campo se recalculan en cada ciclo de detección de cambios (se llaman
  // directo desde el template), así que reaccionan tanto a lo que se va tipeando como al
  // intento de envío, sin necesidad de effects ni de un validador cruzado en el FormGroup.
  protected errorPasswordActual(): string | null {
    if (!this.intentoCambiarPassword()) return null;
    if (!this.formPassword.controls.passwordActual.value) return 'Este campo es requerido.';
    if (this.passwordActualIncorrecta()) return 'La contraseña actual no es correcta.';
    return null;
  }

  protected errorPasswordNueva(): string | null {
    if (!this.intentoCambiarPassword()) return null;
    const { passwordActual, passwordNueva } = this.formPassword.getRawValue();
    if (!passwordNueva) return 'Este campo es requerido.';
    if (passwordNueva.length < 6) return 'Tiene que tener al menos 6 caracteres.';
    if (passwordActual && passwordNueva === passwordActual) return 'Tiene que ser distinta de la contraseña actual.';
    return null;
  }

  protected errorPasswordConfirmar(): string | null {
    if (!this.intentoCambiarPassword()) return null;
    const { passwordNueva, passwordConfirmar } = this.formPassword.getRawValue();
    if (!passwordConfirmar) return 'Este campo es requerido.';
    if (passwordConfirmar !== passwordNueva) return 'Las contraseñas nuevas no coinciden.';
    return null;
  }

  cambiarPassword(): void {
    this.intentoCambiarPassword.set(true);

    if (this.errorPasswordActual() || this.errorPasswordNueva() || this.errorPasswordConfirmar()) {
      return;
    }

    const { passwordActual, passwordNueva } = this.formPassword.getRawValue();
    this.cambiandoPassword.set(true);

    this.authService.cambiarPassword(passwordActual, passwordNueva).subscribe({
      next: () => {
        this.cambiandoPassword.set(false);
        this.intentoCambiarPassword.set(false);
        this.passwordExito.set(true);
        this.formPassword.reset();
      },
      error: (err) => {
        this.cambiandoPassword.set(false);
        this.passwordActualIncorrecta.set(err.status === 400);
      },
    });
  }
}
