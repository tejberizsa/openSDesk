import { Injectable } from '@angular/core';
import { User } from '../_models/user';
import { Resolve, Router, ActivatedRouteSnapshot } from '@angular/router';
import { AuthService } from '../_services/auth.service';
import { AlertifyService } from '../_services/alertify.service';
import { Observable, of } from 'rxjs';
import { catchError } from 'rxjs/operators';
import { TicketService } from '../_services/ticket.service';

@Injectable()
export class UserSelectResolver implements Resolve<User[]> {
    constructor(private ticketService: TicketService, private router: Router,
        private authService: AuthService, private alertify: AlertifyService) {}

    resolve(route: ActivatedRouteSnapshot): Observable<User[]> {
        return this.ticketService.getUserList().pipe(
            catchError(error => {
                this.alertify.error('Failed to load data');
                this.router.navigate(['/home']);
                return of(null);
            })
        );
    }
}
