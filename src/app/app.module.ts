import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { HttpModule } from '@angular/http';
import { HttpClientModule } from '@angular/common/http';
import { FormsModule } from '@angular/forms';
import { AppRoutingModule } from './modules/route-module/route-module.module';
import { SharedModule } from './modules/shared/shared.module';

import { AppComponent } from './app.component';

import { AuthService } from './shared/service/auth/auth.service';
import { LoaderService } from './shared/service/inner/loader.service';
import { ModalTempService } from './shared/service/inner/modal-temp.service';
import { ModalConfirmService } from './shared/service/inner/modal-confirm.service';
import { CardService } from './shared/service/inner/card.service';

@NgModule({
  declarations: [
    AppComponent
  ],
  imports: [
    HttpModule,
    HttpClientModule,
    FormsModule,
    BrowserModule,
    AppRoutingModule,
    SharedModule
  ],
  providers: [
    AuthService,
    LoaderService,
    ModalTempService,
    ModalConfirmService,
    CardService
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
