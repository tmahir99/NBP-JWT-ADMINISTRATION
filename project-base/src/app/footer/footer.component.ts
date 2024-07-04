import { Component, OnDestroy, OnInit } from '@angular/core';
import { Subscription } from 'rxjs';
import { NavigationEnd, Router } from '@angular/router';
import { PermissionService } from '../permission.service';

@Component({
  selector: 'app-footer',
  templateUrl: './footer.component.html',
  styleUrls: ['./footer.component.css']
})
export class FooterComponent implements OnInit, OnDestroy{
  private routeSubscription: Subscription | undefined;
  year = new Date().getFullYear();
  public hide: boolean = false;
  constructor(
    private router: Router,
    private prermisionService: PermissionService,
  ) {}
  ngOnInit(): void {
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
}
