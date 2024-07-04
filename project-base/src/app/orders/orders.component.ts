import { Component, OnInit } from '@angular/core';
import { OrderService } from './order.service';
import { AuthService } from '../auth.service';
import { PermissionService } from '../permission.service';

interface Order {
  id: number;
  productName: string;
  quantity: number;
  price: number;
  orderDate: Date;
  orderedBy: string;
}

@Component({
  selector: 'app-orders',
  templateUrl: './orders.component.html',
  styleUrls: ['./orders.component.css'],
})
export class OrdersComponent implements OnInit {
  displayOrderCreation = false;
  orders: Order[] = [];
  newOrder: Order = { id: 0, productName: '', quantity: 0, price: 0, orderDate: new Date(), orderedBy: '' };
  isEditMode: boolean = false;
  totalValue: any;

  constructor(private orderService: OrderService, public authService: AuthService, public prermisionService: PermissionService) {}

  ngOnInit() {
    this.fetchOrders();
  }

  fetchOrders() {
    this.orderService.getOrders().subscribe((data) => {
      this.orders = data;
    });
  }

  addOrder() {
    if (this.isEditMode) {
      this.orderService.updateOrder(this.newOrder.id, this.newOrder).subscribe((data) => {
        const index = this.orders.findIndex(order => order.id === data.id);
        this.orders[index] = data;
        this.isEditMode = false;
        this.newOrder = { id: 0, productName: '', quantity: 0, price: 0, orderDate: new Date(), orderedBy: '' };
      });
    } else {
      this.orderService.createOrder(this.newOrder).subscribe((data) => {
        this.orders.push(data);
        this.newOrder = { id: 0, productName: '', quantity: 0, price: 0, orderDate: new Date(), orderedBy: '' };
      });
    }
  }

  editOrder(order: Order) {
    this.newOrder = { ...order };
    this.isEditMode = true;
  }

  deleteOrder(id: number) {
    this.orderService.deleteOrder(id).subscribe(() => {
      this.orders = this.orders.filter(order => order.id !== id);
    });
  }

  getTotalValue(productName: string) {
    this.orderService.getTotalOrderValueByProduct(productName).subscribe((data) => {
      this.totalValue = data;
    });
  }

  canCreateOrders(): boolean{
    return this.prermisionService.isAdmin() || this.prermisionService.isSuperAdmin() || this.prermisionService.isOwner() || this.prermisionService.isProcurement() || this.prermisionService.isEngineer() || this.prermisionService.isProductionWorker();
   }
}
