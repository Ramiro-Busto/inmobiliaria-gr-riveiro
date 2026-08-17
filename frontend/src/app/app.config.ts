import { ApplicationConfig, LOCALE_ID, provideBrowserGlobalErrorListeners } from '@angular/core';
import { provideRouter, withInMemoryScrolling } from '@angular/router';
import { provideHttpClient, withInterceptors } from '@angular/common/http';
import { registerLocaleData } from '@angular/common';
import localeEsAr from '@angular/common/locales/es-AR';

import { routes } from './app.routes';
import { authInterceptor } from './core/auth.interceptor';

// Así los números (precios, m², etc.) se muestran con el formato argentino:
// punto para los miles, coma para los decimales (ej. 150.000).
registerLocaleData(localeEsAr);

export const appConfig: ApplicationConfig = {
  providers: [
    provideBrowserGlobalErrorListeners(),
    // Al navegar a otra página (ej. volver al panel después de guardar), arrancar
    // siempre desde arriba en vez de mantener el scroll de la página anterior.
    provideRouter(routes, withInMemoryScrolling({ scrollPositionRestoration: 'top', anchorScrolling: 'enabled' })),
    provideHttpClient(withInterceptors([authInterceptor])),
    { provide: LOCALE_ID, useValue: 'es-AR' },
  ],
};
