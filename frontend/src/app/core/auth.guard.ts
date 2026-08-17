import { inject } from '@angular/core';
import { CanActivateFn, Router } from '@angular/router';
import { AuthService } from './auth.service';

// Protege las rutas del panel: si no hay sesión iniciada, manda al login.
export const authGuard: CanActivateFn = () => {
  const auth = inject(AuthService);
  if (auth.isLoggedIn()) return true;

  inject(Router).navigate(['/admin']);
  return false;
};
