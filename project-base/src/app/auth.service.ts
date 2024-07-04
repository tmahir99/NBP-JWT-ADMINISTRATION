import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Router } from '@angular/router';

@Injectable({
  providedIn: 'root'
})
export class AuthService {

  private apiUrl = 'https://naprednebaze.azurewebsites.net//api/Auth';

  constructor(private http: HttpClient, private router: Router) { }

  login(username: string, password: string): Observable<any> {
    const headers = new HttpHeaders({
      'Content-Type': 'application/json',
      'Accept': '*/*'
    });

    const body = {
      userName: username,
      password: password
    };

    return this.http.post<any>(`${this.apiUrl}/login`, body, { headers: headers });
  }

  getAllUserNames(): Observable<string[]> {
    return this.http.get<string[]>(`${this.apiUrl}/GetAllUserNames`);
  }
  
  register(user: any): Observable<any> {
    const headers = new HttpHeaders({
      'Content-Type': 'application/json',
      'Accept': '*/*'
    });

    return this.http.post<any>(`${this.apiUrl}/register`, user, { headers: headers });
  }

  storeToken(token: string): void {
    localStorage.setItem('token', token); // Store token in local storage
  }

  getToken(): string | null {
    return localStorage.getItem('token'); // Retrieve token from local storage
  }

  clearToken(): void {
    localStorage.removeItem('token'); // Clear token from local storage
  }

  storeUserName(userName: string): void {
    localStorage.setItem('userName', userName);
  }

  getUserName(): string | null {
    return localStorage.getItem('userName');
  }

  deleteUserName(): void {
    localStorage.removeItem('userName');
  }

  storeUserSurname(userSurname: string): void {
    localStorage.setItem('userSurname', userSurname);
  }

  getUserSurname(): string | null {
    return localStorage.getItem('userSurname');
  }

  deleteUserSurname(): void {
    localStorage.removeItem('userSurname');
  }

  storeUserRoles(userRoles: string[]): void {
    localStorage.setItem('userRoles', JSON.stringify(userRoles));
  }

  getUserRoles(): string[] {
    const rolesString = localStorage.getItem('userRoles');
    return rolesString ? JSON.parse(rolesString) : null;
  }

  deleteUserRoles(): void {
    localStorage.removeItem('userRoles');
  }

  storeUserUsername(username: string): void {
    localStorage.setItem('userUsername', username);
  }

  getUserUsername(): string | null {
    return localStorage.getItem('userUsername');
  }

  deleteUserUsername(): void {
    localStorage.removeItem('userUsername');
  }

  isLoggedIn(): boolean {
    const token = this.getToken();
    return !!token;
  }

  logoutUser(): void {
    localStorage.clear();
    this.router.navigate(['/login']);
  }
}
