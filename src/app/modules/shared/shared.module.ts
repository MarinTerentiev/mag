import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';

import { LoaderComponent } from '../../components/infrastructure/loader/loader.component';
import { HeaderComponent } from '../../components/infrastructure/header/header.component';
import { FooterComponent } from '../../components/infrastructure/footer/footer.component';
import { ModalTempComponent } from '../../components/infrastructure/modal-temp/modal-temp.component';
import { ModalConfirmComponent } from '../../components/infrastructure/modal-confirm/modal-confirm.component';

@NgModule({
	imports: [
		CommonModule,
		RouterModule
	],
	declarations: [
		LoaderComponent,
		HeaderComponent,
		FooterComponent,
		ModalTempComponent,
		ModalConfirmComponent
	],
	providers: [
	],
	exports: [
		LoaderComponent,
		HeaderComponent,
		FooterComponent,
		ModalTempComponent,
		ModalConfirmComponent
	]
})

export class SharedModule { }
