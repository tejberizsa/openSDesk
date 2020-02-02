import { Injectable } from '@angular/core';
import { TicketService } from '../_services/ticket.service';
import { Router, ActivatedRouteSnapshot, Resolve } from '@angular/router';
import { AlertifyService } from '../_services/alertify.service';
import { Observable, of } from 'rxjs';
import { catchError } from 'rxjs/operators';
import { Ticket } from '../_models/ticket';

@Injectable()
export class TicketDetailResolver implements Resolve<Ticket> {
    constructor(private ticketService: TicketService, private router: Router, private alertify: AlertifyService) {}

    resolve(route: ActivatedRouteSnapshot): Observable<Ticket> {
        return this.ticketService.getTicket(route.params['id']).pipe(
            catchError(error => {
                this.alertify.error('Failed to load data');
                this.router.navigate(['/home']);
                return of(null);
            })
        );
    }
}
