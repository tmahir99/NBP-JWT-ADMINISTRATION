import { Injectable, OnInit } from '@angular/core';
import { AuthService } from "./auth.service";

@Injectable({
  providedIn: 'root'
})
export class PermissionService implements OnInit {

  constructor(
    private authService: AuthService
  ) { }

  ngOnInit(): void {
    this.authService.getUserRoles();
  }

  hasRole(role: string): boolean {
    return this.authService.getUserRoles().includes(role);
  }

  isAdmin(): boolean {
    return this.hasRole('ADMIN');
  }

  isSuperAdmin(): boolean {
    return this.hasRole('SUPERADMIN');
  }

  isOwner(): boolean {
    return this.hasRole('OWNER');
  }

  isCarrier(): boolean {
    return this.hasRole('CARRIER');
  }

  isEngineer(): boolean {
    return this.hasRole('ENGINEER');
  }

  isUser(): boolean {
    return this.hasRole('USER');
  }

  isProductionWorker(): boolean {
    return this.hasRole('PRODUCTION_WORKER');
  }

  isGuest(): boolean {
    return this.hasRole('GUEST');
  }

  isProcurement(): boolean {
    return this.hasRole('PROCUREMENT');
  }
}
