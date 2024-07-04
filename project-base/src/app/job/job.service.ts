import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';
import { AuthService } from '../auth.service';

@Injectable({
  providedIn: 'root'
})
export class JobService {
  private apiUrl = 'https://naprednebaze.azurewebsites.net//api/Job';

  constructor(private http: HttpClient, private authService: AuthService) { }

  private getAuthHeaders(): HttpHeaders {
    const token = this.authService.getToken();
    return new HttpHeaders({
      'Authorization': `Bearer ${token}`
    });
  }

  getJobs(): Observable<any> {
    const headers = this.getAuthHeaders();
    return this.http.get<any>(this.apiUrl, { headers });
  }

  getJob(id: number): Observable<any> {
    const headers = this.getAuthHeaders();
    return this.http.get<any>(`${this.apiUrl}/${id}`, { headers });
  }

  createJob(job: FormData): Observable<any> {
    const headers = this.getAuthHeaders();
    return this.http.post<any>(this.apiUrl, job, { headers });
  }

  updateJob(id: number, job: FormData): Observable<any> {
    const headers = this.getAuthHeaders();
    return this.http.put<any>(`${this.apiUrl}/${id}`, job, { headers });
  }

  deleteJob(id: number): Observable<any> {
    const headers = this.getAuthHeaders();
    return this.http.delete<any>(`${this.apiUrl}/${id}`, { headers });
  }

  markJobAsDone(id: number, editedBy: string): Observable<any> {
    const headers = this.getAuthHeaders();
    return this.http.post<any>(`${this.apiUrl}/${id}/mark-done`, `"${editedBy}"`, { headers }); // Send editedBy as JSON string
  }
}
