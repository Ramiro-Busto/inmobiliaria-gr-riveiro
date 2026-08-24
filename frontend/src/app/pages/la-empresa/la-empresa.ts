import { Component, inject } from '@angular/core';
import { Icon } from '../../shared/icon/icon';
import { SeoService } from '../../core/seo.service';

@Component({
  selector: 'app-la-empresa',
  imports: [Icon],
  templateUrl: './la-empresa.html',
  styleUrl: './la-empresa.scss',
})
export class LaEmpresa {
  constructor() {
    inject(SeoService).actualizar(
      'La Empresa',
      'Conocé a GR Riveiro Negocios Inmobiliarios: acompañamiento profesional en la venta y alquiler de propiedades en Quilmes, Zona Sur.',
    );
  }
}
