import { HttpClient } from '@angular/common/http';
import { Injectable, inject, signal } from '@angular/core';
import { Observable, tap } from 'rxjs';
import { API_BASE_URL } from './api-config';

interface LoginResponse {
  token: string;
  nombre: string;
  email: string;
}

const STORAGE_KEY = 'inmobiliaria_token';

@Injectable({ providedIn: 'root' })
export class AuthService {
  private readonly http = inject(HttpClient);

  // Signal para que la navbar/guards puedan reaccionar al login/logout al instante.
  readonly token = signal<string | null>(localStorage.getItem(STORAGE_KEY));

  login(email: string, password: string): Observable<LoginResponse> {
    return this.http.post<LoginResponse>(`${API_BASE_URL}/auth/login`, { email, password }).pipe(
      tap((respuesta) => {
        localStorage.setItem(STORAGE_KEY, respuesta.token);
        this.token.set(respuesta.token);
      }),
    );
  }

  logout(): void {
    localStorage.removeItem(STORAGE_KEY);
    this.token.set(null);
  }

  isLoggedIn(): boolean {
    return this.token() !== null;
  }
}
