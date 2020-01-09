import { Routes } from '@angular/router';
import { HomeComponent } from './home/home.component';
import { MessagesComponent } from './messages/messages.component';
import { AuthGuard } from './_guards/auth.guard';
import { RegisterComponent } from './register/register.component';
import { RegisterGuard } from './_guards/register.guard';
import { MessagesResolver } from './_resolvers/messages.resolver';
import { UserConfirmComponent } from './user/user-confirm/user-confirm.component';
import { PolicyComponent } from './policy/policy.component';

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
            { path: 'messages', component: MessagesComponent, resolve: { messages: MessagesResolver } }
        ]
    },
    { path: '**', redirectTo: '', pathMatch: 'full'}
];
