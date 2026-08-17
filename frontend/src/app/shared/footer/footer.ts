import { Component } from '@angular/core';
import { RouterLink } from '@angular/router';
import { linkWhatsApp } from '../whatsapp';
import { Icon } from '../icon/icon';
import {
  FACEBOOK_HANDLE,
  FACEBOOK_URL,
  INSTAGRAM_HANDLE,
  INSTAGRAM_URL,
  MAIL_CONTACTO,
  TELEFONO_DISPLAY,
  TELEFONO_TEL,
} from '../redes';

@Component({
  selector: 'app-footer',
  imports: [RouterLink, Icon],
  templateUrl: './footer.html',
  styleUrl: './footer.scss',
})
export class Footer {
  protected readonly linkWhatsapp = linkWhatsApp('Hola! Quería hacerte una consulta.');
  protected readonly anio = new Date().getFullYear();

  protected readonly instagramUrl = INSTAGRAM_URL;
  protected readonly instagramHandle = INSTAGRAM_HANDLE;
  protected readonly facebookUrl = FACEBOOK_URL;
  protected readonly facebookHandle = FACEBOOK_HANDLE;
  protected readonly mail = MAIL_CONTACTO;
  protected readonly telefonoDisplay = TELEFONO_DISPLAY;
  protected readonly linkTelefono = `tel:${TELEFONO_TEL}`;
  protected readonly linkMail = `mailto:${MAIL_CONTACTO}`;
}
