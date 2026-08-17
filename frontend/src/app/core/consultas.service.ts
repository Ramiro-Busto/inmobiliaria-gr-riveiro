import { HttpClient } from '@angular/common/http';
import { Injectable, inject } from '@angular/core';
import { Observable } from 'rxjs';
import { API_BASE_URL } from './api-config';
import { Consulta, ConsultaCreate } from './models/consulta';

@Injectable({ providedIn: 'root' })
export class ConsultasService {
  private readonly http = inject(HttpClient);
  private readonly url = `${API_BASE_URL}/consultas`;

  create(consulta: ConsultaCreate): Observable<Consulta> {
    return this.http.post<Consulta>(this.url, consulta);
  }

  getAll(): Observable<Consulta[]> {
    return this.http.get<Consulta[]>(this.url);
  }

  marcarLeida(id: number): Observable<void> {
    return this.http.patch<void>(`${this.url}/${id}/leida`, {});
  }
}
