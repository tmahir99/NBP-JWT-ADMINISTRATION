import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';
import { AuthService } from '../auth.service';

interface Order {
  id: number;
  productName: string;
  quantity: number;
  price: number;
  orderDate: Date;
  orderedBy: string;
}

@Injectable({
  providedIn: 'root',
})
export class OrderService {
  private apiUrl = 'https://naprednebaze.azurewebsites.net//api/Procurement/orders';

  constructor(private http: HttpClient, private authService: AuthService) {}

  private getAuthHeaders(): HttpHeaders {
    const token = this.authService.getToken();
    return new HttpHeaders({
      'Content-Type': 'application/json',
      'Authorization': `Bearer ${token}`
    });
  }

  getOrders(): Observable<Order[]> {
    const headers = this.getAuthHeaders();
    return this.http.get<Order[]>(this.apiUrl, { headers });
  }

  getOrder(id: number): Observable<Order> {
    const headers = this.getAuthHeaders();
    return this.http.get<Order>(`${this.apiUrl}/${id}`, { headers });
  }

  createOrder(order: Order): Observable<Order> {
    const headers = this.getAuthHeaders();
    return this.http.post<Order>(this.apiUrl, order, { headers });
  }

  updateOrder(id: number, order: Order): Observable<Order> {
    const headers = this.getAuthHeaders();
    return this.http.put<Order>(`${this.apiUrl}/${id}`, order, { headers });
  }

  deleteOrder(id: number): Observable<void> {
    const headers = this.getAuthHeaders();
    return this.http.delete<void>(`${this.apiUrl}/${id}`, { headers });
  }

  getTotalOrderValueByProduct(productName: string): Observable<any> {
    const headers = this.getAuthHeaders();
    return this.http.get<any>(`${this.apiUrl}/total?productName=${productName}`, { headers });
  }
}
