import { Component,OnInit, OnDestroy } from '@angular/core';
import { AuthService } from '../auth.service';
import { PermissionService } from '../permission.service';

import { NavigationEnd, Router } from '@angular/router';
import { Subscription } from 'rxjs';

@Component({
  selector: 'app-custom-header',
  templateUrl: './header.component.html',
  styleUrls: ['./header.component.css']
})
export class HeaderComponent implements OnInit, OnDestroy {
  private routeSubscription: Subscription | undefined;
  public hide: boolean = false;
  public currentUser: string | null = '';
  constructor(
    private authService: AuthService,
    private router: Router,
    private prermisionService: PermissionService,
  ) {}
  canViewJobs(): boolean { return this.prermisionService.isAdmin() || this.prermisionService.isSuperAdmin() || this.prermisionService.isProductionWorker()
    || this.prermisionService.isCarrier() || this.prermisionService.isOwner()
   }

   canViewManageUsers(): boolean {
    return this.prermisionService.isAdmin() || this.prermisionService.isSuperAdmin() || this.prermisionService.isOwner();
   }

   canViewOrders(): boolean{
    return this.prermisionService.isAdmin() || this.prermisionService.isSuperAdmin() || this.prermisionService.isOwner() || this.prermisionService.isProcurement() || this.prermisionService.isEngineer() || this.prermisionService.isProductionWorker();
   }

   canViewDocuments(): boolean{
    return this.prermisionService.isAdmin() || this.prermisionService.isSuperAdmin() || this.prermisionService.isOwner()
   }
  ngOnInit(): void {
    this.currentUser = this.authService.getUserName();
    this.routeSubscription = this.router.events.subscribe(event => {
      if (event instanceof NavigationEnd) {
        this.checkLoginRoute();
      }
    });
  }
  ngOnDestroy(): void {
    if (this.routeSubscription) {
      this.routeSubscription.unsubscribe();
    }
  }

  checkLoginRoute(): void {
    const route = this.router.url.slice(1);
    if (route === 'login' || route === 'register') {
      this.hide = true;
    }
    else {
      this.hide = false;
    }
  }
  
  logoutUser() {
    this.authService.logoutUser();
    this.router.navigate(['/login']);
  }
}
