import { Component, OnDestroy, OnInit } from '@angular/core';
import { Subscription } from 'rxjs';
import { NavigationEnd, Router } from '@angular/router';

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
    private router: Router
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
}
