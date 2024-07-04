import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';
import { AuthService } from '../auth.service';

export interface Document {
  id: number;
  title: string;
  description: string;
  dateCreated: string;
  filePath: string;
  employeeUserName: string;
}

@Injectable({
  providedIn: 'root'
})
export class DocumentService {
  private apiUrl = 'https://naprednebaze.azurewebsites.net//api/Document';

  constructor(private http: HttpClient, private authService: AuthService) { }

  private getAuthHeaders(): HttpHeaders {
    const token = this.authService.getToken();
    return new HttpHeaders({
      'Content-Type': 'application/json',
      'Authorization': `Bearer ${token}`
    });
  }

  getDocuments(): Observable<Document[]> {
    const headers = this.getAuthHeaders();
    return this.http.get<Document[]>(this.apiUrl, { headers });
  }

  addDocument(document: Document): Observable<Document> {
    const headers = this.getAuthHeaders();
    return this.http.post<Document>(this.apiUrl, document, { headers });
  }

  updateDocument(document: Document): Observable<Document> {
    const headers = this.getAuthHeaders();
    return this.http.put<Document>(`${this.apiUrl}/${document.id}`, document, { headers });
  }

  deleteDocument(id: number): Observable<void> {
    const headers = this.getAuthHeaders();
    return this.http.delete<void>(`${this.apiUrl}/${id}`, { headers });
  }
}
