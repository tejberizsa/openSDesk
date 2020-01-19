import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { HttpClientModule } from '@angular/common/http';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { BsDropdownModule, CollapseModule, TabsModule, PaginationModule } from 'ngx-bootstrap';
import { RouterModule } from '@angular/router';
import { JwtModule } from '@auth0/angular-jwt';
import { NgxGalleryModule } from 'ngx-gallery';
import { TimeagoModule, TimeagoFormatter, TimeagoIntl, TimeagoCustomFormatter } from 'ngx-timeago';
import { FileUploadModule } from 'ng2-file-upload';
import { NgxEditorModule } from 'ngx-editor';
// import { NgcCookieConsentModule, NgcCookieConsentConfig } from 'ngx-cookieconsent';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';

import { AppComponent } from './app.component';
import { NavComponent } from './nav/nav.component';
import { AuthService } from './_services/auth.service';
import { ErrorInterceptorProvider } from './_services/error.interceptor';
import { HomeComponent } from './home/home.component';
import { RegisterComponent } from './register/register.component';
import { AlertifyService } from './_services/alertify.service';
import { MessagesComponent } from './messages/messages.component';
import { appRoutes } from './routes';
import { AuthGuard } from './_guards/auth.guard';
import { RegisterGuard } from './_guards/register.guard';
import { UserService } from './_services/user.service';
import { UserEditComponent } from './user/user-edit/user-edit.component';
import { MessagesResolver } from './_resolvers/messages.resolver';
import { UserMessagesComponent } from './user/user-messages/user-messages.component';
import { UserPhotoEditorComponent } from './user/user-photo-editor/user-photo-editor.component';
import { PolicyComponent } from './policy/policy.component';
import { environment } from 'src/environments/environment';
import { UserConfirmComponent } from './user/user-confirm/user-confirm.component';
import { TicketAddComponent } from './ticket/ticketAdd/ticketAdd.component';
import { TicketDetailComponent } from './ticket/ticketDetail/ticketDetail.component';
import { UserDetailResolver } from './_resolvers/user-detail.resolver';
import { UserEditResolver } from './_resolvers/user-edit.resolver';
import { UserDetailComponent } from './user/user-detail/user-detail.component';

// to use cookie banner uncomment implementation is app.module.ts and app.component.ts
// const cookieConfig: NgcCookieConsentConfig = {
//    cookie: {
//      domain: environment.domain
//    },
//    palette: {
//      popup: {
//        background: '#000'
//      },
//      button: {
//        background: '#f1d600'
//      }
//    },
//    theme: 'classic',
//    type: 'info',
//    'content': {
//       'message': 'We use cookies and other tracking technologies to improve your browsing experience on our website.',
//       'dismiss': 'I agree',
//       'deny': 'Refuse',
//       'link': 'Learn more',
//       'href': 'http://openSDesk.hu/policy',
//       'policy': 'Privacy policies' }
//  };

export function tokenGetter() {
   return localStorage.getItem('token');
}
export class MyIntl extends TimeagoIntl {
   // do extra stuff here...
   }

   @NgModule({
      declarations: [
         AppComponent,
         NavComponent,
         HomeComponent,
         RegisterComponent,
         MessagesComponent,
         UserEditComponent,
         UserMessagesComponent,
         UserPhotoEditorComponent,
         PolicyComponent,
         UserConfirmComponent,
         TicketAddComponent,
         TicketDetailComponent,
         UserDetailComponent
      ],
      imports: [
         [ BrowserModule, CollapseModule.forRoot()],
         HttpClientModule,
         BrowserAnimationsModule,
         ReactiveFormsModule,
         FormsModule,
         BsDropdownModule.forRoot(),
         TabsModule.forRoot(),
         RouterModule.forRoot(appRoutes),
         NgxGalleryModule,
         FileUploadModule,
         NgxEditorModule,
         PaginationModule.forRoot(),
         // NgcCookieConsentModule.forRoot(cookieConfig),
         TimeagoModule.forRoot({
            intl: { provide: TimeagoIntl, useClass: MyIntl },
            formatter: { provide: TimeagoFormatter, useClass: TimeagoCustomFormatter },
          }),
         JwtModule.forRoot({
            config: {
               tokenGetter: tokenGetter,
               whitelistedDomains: ['localhost:5000'],
               blacklistedRoutes: ['localhost:5000/api/auth']
            }
         })
      ],
      providers: [
         AuthService,
         ErrorInterceptorProvider,
         AlertifyService,
         AuthGuard,
         RegisterGuard,
         UserService,
         MessagesResolver,
         UserDetailResolver,
         UserEditResolver
      ],
      bootstrap: [
         AppComponent
      ]
   })
export class AppModule { }
