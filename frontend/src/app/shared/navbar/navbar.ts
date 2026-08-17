import { Component, signal } from '@angular/core';
import { RouterLink, RouterLinkActive } from '@angular/router';
import { Icon } from '../icon/icon';
import { FACEBOOK_URL, INSTAGRAM_URL } from '../redes';

@Component({
  selector: 'app-navbar',
  imports: [RouterLink, RouterLinkActive, Icon],
  templateUrl: './navbar.html',
  styleUrl: './navbar.scss',
})
export class Navbar {
  protected readonly instagramUrl = INSTAGRAM_URL;
  protected readonly facebookUrl = FACEBOOK_URL;

  protected readonly menuAbierto = signal(false);

  alternarMenu(): void {
    this.menuAbierto.update((abierto) => !abierto);
  }

  cerrarMenu(): void {
    this.menuAbierto.set(false);
  }
}
