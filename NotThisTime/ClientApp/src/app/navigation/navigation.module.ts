import { NgModule } from "@angular/core";
import { CommonModule } from "@angular/common";
import { NavigationRoutingModule } from "./navigation-routing.module";
import { MainComponent } from "./main/main.component";
import {
  MatMenuModule,
  MatButtonModule,
  MatDividerModule
} from "@angular/material";
import { FlexLayoutModule } from "@angular/flex-layout";
import { HttpClientModule } from "@angular/common/http";

@NgModule({
  declarations: [MainComponent],
  imports: [
    CommonModule,
    NavigationRoutingModule,
    MatMenuModule,
    MatButtonModule,
    MatDividerModule,
    FlexLayoutModule,
    HttpClientModule
  ],
  exports: [MainComponent]
})
export class NavigationModule {}
