import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable, inject } from '@angular/core';
import { Observable } from 'rxjs';
import { API_BASE_URL } from './api-config';
import { Moneda, Operacion, Propiedad, TipoPropiedad } from './models/propiedad';

export interface FiltrosPropiedades {
  tipo?: TipoPropiedad;
  operacion?: Operacion;
  moneda?: Moneda;
  ubicacion?: string;
  precioMin?: number;
  precioMax?: number;
}

@Injectable({ providedIn: 'root' })
export class PropiedadesService {
  private readonly http = inject(HttpClient);
  private readonly url = `${API_BASE_URL}/propiedades`;

  getAll(filtros: FiltrosPropiedades = {}): Observable<Propiedad[]> {
    let params = new HttpParams();
    for (const [clave, valor] of Object.entries(filtros)) {
      if (valor) params = params.set(clave, valor);
    }
    return this.http.get<Propiedad[]>(this.url, { params });
  }

  getAllParaAdmin(): Observable<Propiedad[]> {
    return this.http.get<Propiedad[]>(`${this.url}/admin`);
  }

  getById(id: number): Observable<Propiedad> {
    return this.http.get<Propiedad>(`${this.url}/${id}`);
  }

  // Igual que getById, pero también trae propiedades en Borrador/Suspendido/etc.
  // (las que no se muestran en el sitio público). La usa el panel para editar.
  getByIdParaAdmin(id: number): Observable<Propiedad> {
    return this.http.get<Propiedad>(`${this.url}/admin/${id}`);
  }

  create(propiedad: Partial<Propiedad>): Observable<Propiedad> {
    return this.http.post<Propiedad>(this.url, propiedad);
  }

  update(id: number, propiedad: Partial<Propiedad>): Observable<void> {
    return this.http.put<void>(`${this.url}/${id}`, propiedad);
  }

  delete(id: number): Observable<void> {
    return this.http.delete<void>(`${this.url}/${id}`);
  }
}
