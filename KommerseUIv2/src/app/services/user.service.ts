import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

export interface UserRegisterDto {
  tc: number;
  email: string;
  password: string;
  rePassword: string;
}

export interface UserLoginDto {
  tc: number;
  email: string;
  password: string;
}

@Injectable({
  providedIn: 'root'
})
export class UserService {

  private apiUrl = 'https://localhost:7208/api/Users';

  constructor(private http: HttpClient) {}

  register(user: UserRegisterDto): Observable<any> {
    return this.http.post<any>(`${this.apiUrl}/register`, user);
  }

  login(user: UserLoginDto): Observable<any> {
    return this.http.post<any>(`${this.apiUrl}/login`, user);
  }



}
