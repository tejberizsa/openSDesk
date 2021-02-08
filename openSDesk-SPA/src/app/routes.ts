import { Routes } from '@angular/router';
import { HomeComponent } from './home/home.component';
import { MessagesComponent } from './messages/messages.component';
import { AuthGuard } from './_guards/auth.guard';
import { RegisterComponent } from './register/register.component';
import { RegisterGuard } from './_guards/register.guard';
import { MessagesResolver } from './_resolvers/messages.resolver';
import { UserConfirmComponent } from './user/user-confirm/user-confirm.component';
import { PolicyComponent } from './policy/policy.component';
import { UserEditComponent } from './user/user-edit/user-edit.component';
import { UserEditResolver } from './_resolvers/user-edit.resolver';
import { UserDetailResolver } from './_resolvers/user-detail.resolver';
import { UserDetailComponent } from './user/user-detail/user-detail.component';
import { TicketAddComponent } from './ticket/ticketAdd/ticketAdd.component';
import { TicketDetailComponent } from './ticket/ticketDetail/ticketDetail.component';
import { UserSelectResolver } from './_resolvers/user-select.resolver';
import { CategoryListResolver } from './_resolvers/category-list.resolver';
import { TicketDetailResolver } from './_resolvers/ticket-detail.resolver';
import { TicketListComponent } from './ticket/ticket-list/ticket-list.component';

export const appRoutes: Routes = [
    { path: '', component: HomeComponent },
    { path: 'confirm/:id/:ckey', component: UserConfirmComponent },
    { path: 'register', component: RegisterComponent, canActivate: [RegisterGuard] },
    { path: 'policy', component: PolicyComponent },
    {
        path: '',
        runGuardsAndResolvers: 'always',
        canActivate: [AuthGuard],
        children: [
            { path: 'messages', component: MessagesComponent, resolve: { messages: MessagesResolver } },
            { path: 'user/edit', component: UserEditComponent, resolve: { user: UserEditResolver } },
            { path: 'user/:id', component: UserDetailComponent, resolve: { user: UserDetailResolver } },
            { path: 'addTicket', component: TicketAddComponent,
                                resolve: { userlist: UserSelectResolver, categories: CategoryListResolver }  },
            { path: 'ticket/:id', component: TicketDetailComponent, resolve: { ticket: TicketDetailResolver } },
            { path: 'list', component: TicketListComponent, data: {roles: ['Admin', 'Clerk']}}
        ]
    },
    { path: '**', redirectTo: '', pathMatch: 'full'}
];
