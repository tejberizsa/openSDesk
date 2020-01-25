import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { AlertifyService } from '../_services/alertify.service';
import { Pagination, PaginatedResult } from '../_models/pagination';
import { AuthService } from '../_services/auth.service';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent implements OnInit {
  model: any = {};
  registerMode: boolean;
  pagination: Pagination;
  queryString: string;
  opened: boolean;
  filterByTopic: number;

  constructor(private route: ActivatedRoute, private alertify: AlertifyService, public authService: AuthService, private router: Router) { }

  ngOnInit() {
      this.route.data.subscribe(data => {
    });
  }

  loggedIn() {
    return this.authService.loggedIn();
  }

  login() {
    this.authService.login(this.model).subscribe(next => {
      this.alertify.success('Successfully logged in');
      this.router.navigate(['/home']);
    }, error => {
      this.alertify.error(error);
    });
  }

  pageChanged(event: any): void {
    this.pagination.currentPage = event.page;
    window.scrollTo(0, 0);
  }

}
