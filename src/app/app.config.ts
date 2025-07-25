import { ApplicationConfig, importProvidersFrom } from '@angular/core';
import { provideRouter } from '@angular/router';
import { provideHttpClient, withInterceptors } from '@angular/common/http';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { routes } from './app.routes';
import { authInterceptor } from './interceptors/auth.interceptor';
import { AuthService, MockAuthService } from './services/auth.service';
import { ApiService, MockApiService } from './services/api.service';
import { CommentService, MockCommentService } from './services/comment.service';
import { MockTicketService, TicketService } from './services/ticket.service';

export const appConfig: ApplicationConfig = {
  providers: [
    provideRouter(routes),
    provideHttpClient(withInterceptors([authInterceptor])),
    importProvidersFrom(BrowserAnimationsModule),
    { provide: AuthService, useClass: MockAuthService },
    { provide: ApiService, useClass: MockApiService },
    { provide: CommentService, useClass: MockCommentService },
    { provide: TicketService, useClass: MockTicketService },
  ]
};
