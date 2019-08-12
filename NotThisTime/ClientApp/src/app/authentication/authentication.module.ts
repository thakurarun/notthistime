import { NgModule } from "@angular/core";
import { CommonModule } from "@angular/common";
import { AuthenticationRoutingModule } from "./authentication-routing.module";
import { LoginComponent } from "./login/login.component";
import { ReactiveFormsModule, FormsModule } from "@angular/forms";
import {
  MatFormFieldModule,
  MatDividerModule,
  MatInputModule,
  MatButtonModule
} from "@angular/material";
import { FlexLayoutModule } from "@angular/flex-layout";
import { RegisterComponent } from "./register/register.component";
import { HttpClientModule } from "@angular/common/http";

@NgModule({
  declarations: [LoginComponent, RegisterComponent],
  imports: [
    CommonModule,
    MatFormFieldModule,
    MatInputModule,
    MatButtonModule,
    MatDividerModule,
    AuthenticationRoutingModule,
    ReactiveFormsModule,
    FlexLayoutModule,
    FormsModule,
    HttpClientModule
  ],
  exports: [LoginComponent]
})
export class AuthenticationModule {}
