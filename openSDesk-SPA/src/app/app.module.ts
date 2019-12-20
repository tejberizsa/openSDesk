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
import { NgcCookieConsentModule, NgcCookieConsentConfig } from 'ngx-cookieconsent';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { FacebookModule } from 'ngx-facebook';

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
import { MemberDetailComponent } from './member/member-detail/member-detail.component';
import { UserService } from './_services/user.service';
import { MemberEditComponent } from './member/member-edit/member-edit.component';
import { MessagesResolver } from './_resolvers/messages.resolver';
import { MemberMessagesComponent } from './member/member-messages/member-messages.component';
import { MemberPhotoEditorComponent } from './member/member-photo-editor/member-photo-editor.component';
import { PolicyComponent } from './policy/policy.component';
import { environment } from 'src/environments/environment';
import { MemberConfirmComponent } from './member/member-confirm/member-confirm.component';


const cookieConfig: NgcCookieConsentConfig = {
   cookie: {
     domain: environment.domain
   },
   palette: {
     popup: {
       background: '#000'
     },
     button: {
       background: '#f1d600'
     }
   },
   theme: 'classic',
   type: 'info',
   'content': {
      'message': 'A weboldalon cookie-kat használok annak érdekében, hogy a lehető legjobb felhasználói élményt nyújtsam.',
      'dismiss': 'OK, elfogadom',
      'deny': 'Refuse cookies',
      'link': 'Bővebben az adatvédelemről',
      'href': 'http://openSDesk.hu/policy',
      'policy': 'Adatvédelmi irányelvek' }
 };

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
         MemberDetailComponent,
         MemberEditComponent,
         MemberMessagesComponent,
         MemberPhotoEditorComponent,
         PolicyComponent,
         MemberConfirmComponent
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
         FacebookModule.forRoot(),
         NgcCookieConsentModule.forRoot(cookieConfig),
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
         MessagesResolver
      ],
      bootstrap: [
         AppComponent
      ]
   })
export class AppModule { }
