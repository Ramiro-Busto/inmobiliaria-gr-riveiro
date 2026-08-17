import { HttpClient } from '@angular/common/http';
import { Injectable, inject } from '@angular/core';
import { Observable } from 'rxjs';
import { API_BASE_URL } from './api-config';

@Injectable({ providedIn: 'root' })
export class ImagenesService {
  private readonly http = inject(HttpClient);

  subir(archivo: File): Observable<{ url: string }> {
    const formData = new FormData();
    formData.append('archivo', archivo);
    return this.http.post<{ url: string }>(`${API_BASE_URL}/imagenes/upload`, formData);
  }
}
