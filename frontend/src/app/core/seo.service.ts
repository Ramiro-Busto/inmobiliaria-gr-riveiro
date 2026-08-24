import { Injectable, inject } from '@angular/core';
import { Meta, Title } from '@angular/platform-browser';

const SUFIJO = ' | GR Riveiro Negocios Inmobiliarios';
const TITULO_BASE = 'GR Riveiro Negocios Inmobiliarios | Inmobiliaria en Quilmes, Zona Sur';

@Injectable({ providedIn: 'root' })
export class SeoService {
  private readonly title = inject(Title);
  private readonly meta = inject(Meta);

  actualizar(titulo: string | null, descripcion: string, imagen?: string): void {
    const tituloCompleto = titulo ? `${titulo}${SUFIJO}` : TITULO_BASE;

    this.title.setTitle(tituloCompleto);
    this.meta.updateTag({ name: 'description', content: descripcion });
    this.meta.updateTag({ property: 'og:title', content: tituloCompleto });
    this.meta.updateTag({ property: 'og:description', content: descripcion });
    this.meta.updateTag({ property: 'og:url', content: window.location.href });
    if (imagen) {
      this.meta.updateTag({ property: 'og:image', content: imagen });
    }
  }
}
