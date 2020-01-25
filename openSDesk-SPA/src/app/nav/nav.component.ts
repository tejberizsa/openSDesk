import { Component, OnInit, ElementRef } from '@angular/core';
import { AuthService } from '../_services/auth.service';
import { AlertifyService } from '../_services/alertify.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-nav',
  templateUrl: './nav.component.html',
  styleUrls: ['./nav.component.css']
})
export class NavComponent implements OnInit {
  model: any = {};
  isCollapsed = true;

  constructor(public authService: AuthService, private alertify: AlertifyService,
    private router: Router, private elem: ElementRef) { }

  ngOnInit() {}

  onClick(e) {
    const elements = Array.from(document.querySelectorAll('a.nav-link'));
    elements.forEach(elem => elem.classList.remove('text-info'));
    e.target.classList.add('text-info');
  }

  logout() {
    localStorage.removeItem('token');
    this.alertify.message('Signed out');
    this.router.navigate(['/home']);
  }
}
