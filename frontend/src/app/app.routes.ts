import { Routes } from '@angular/router';
import { Inicio } from './pages/inicio/inicio';
import { Propiedades } from './pages/propiedades/propiedades';
import { PropiedadDetalle } from './pages/propiedad-detalle/propiedad-detalle';
import { LaEmpresa } from './pages/la-empresa/la-empresa';
import { Tasaciones } from './pages/tasaciones/tasaciones';
import { Contacto } from './pages/contacto/contacto';
import { Login } from './pages/admin/login/login';
import { Panel } from './pages/admin/panel/panel';
import { PropiedadForm } from './pages/admin/propiedad-form/propiedad-form';
import { authGuard } from './core/auth.guard';

export const routes: Routes = [
  { path: '', component: Inicio },
  { path: 'propiedades', component: Propiedades },
  { path: 'propiedades/:id', component: PropiedadDetalle },
  { path: 'la-empresa', component: LaEmpresa },
  { path: 'tasaciones', component: Tasaciones },
  { path: 'contacto', component: Contacto },
  { path: 'admin', component: Login },
  { path: 'admin/panel', component: Panel, canActivate: [authGuard] },
  { path: 'admin/panel/propiedades/nueva', component: PropiedadForm, canActivate: [authGuard] },
  { path: 'admin/panel/propiedades/:id/editar', component: PropiedadForm, canActivate: [authGuard] },
  { path: '**', redirectTo: '' },
];
