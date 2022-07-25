import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { HeaderComponent } from './components/header/header.component';
import { LoginComponent } from './components/auth/login/login.component';
import { SignupComponent } from './components/auth/signup/signup.component';
import { HomeComponent } from './components/home/home.component';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { BuyComponent } from './components/buy/buy.component';
import { InterceptorService } from './services/interceptor.service';
import { MyWeaponsComponent } from './components/my-weapons/my-weapons.component';
import { EditComponentComponent } from './components/edit-component/edit-component.component';
import { CreateComponentComponent } from './components/create-component/create-component.component';
import { SpinnerComponentComponent } from './components/spinner-component/spinner-component.component';
import { UsersComponent } from './components/users/users.component';

@NgModule({
  declarations: [
    AppComponent,
    HeaderComponent,
    LoginComponent,
    SignupComponent,
    HomeComponent,
    BuyComponent,
    MyWeaponsComponent,
    EditComponentComponent,
    CreateComponentComponent,
    SpinnerComponentComponent,
    UsersComponent,
  ],
  imports: [
    FormsModule,
    HttpClientModule,
    BrowserModule,
    AppRoutingModule,
    ReactiveFormsModule,
  ],
  providers: [
    {
      provide: HTTP_INTERCEPTORS,
      useClass: InterceptorService,
      multi: true,
    },
  ],
  bootstrap: [AppComponent],
})
export class AppModule {}
