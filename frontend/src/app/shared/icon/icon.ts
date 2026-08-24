import { Component, Input } from '@angular/core';

export type IconName =
  | 'whatsapp'
  | 'phone'
  | 'mail'
  | 'mapPin'
  | 'clock'
  | 'instagram'
  | 'facebook'
  | 'home'
  | 'droplet'
  | 'maximize'
  | 'close'
  | 'star'
  | 'key'
  | 'chart'
  | 'menu'
  | 'upload'
  | 'badge'
  | 'compass'
  | 'check';

const ICONOS_RELLENOS: IconName[] = ['whatsapp', 'facebook'];

@Component({
  selector: 'app-icon',
  imports: [],
  templateUrl: './icon.html',
  styleUrl: './icon.scss',
})
export class Icon {
  @Input({ required: true }) name!: IconName;
  @Input() size = 20;

  protected get esRelleno(): boolean {
    return ICONOS_RELLENOS.includes(this.name);
  }
}
