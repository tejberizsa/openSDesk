import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { AlertifyService } from '../_services/alertify.service';
import { Pagination, PaginatedResult } from '../_models/pagination';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent implements OnInit {
  registerMode: boolean;
  pagination: Pagination;
  queryString: string;
  opened: boolean;
  filterByTopic: number;

  constructor(private route: ActivatedRoute, private alertify: AlertifyService) { }

  ngOnInit() {
      this.route.data.subscribe(data => {
    });
  }

  pageChanged(event: any): void {
    this.pagination.currentPage = event.page;
    window.scrollTo(0, 0);
  }

}
