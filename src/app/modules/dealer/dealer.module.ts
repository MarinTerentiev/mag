import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { HttpModule } from '@angular/http';
import { RouterModule, Routes } from '@angular/router';

import { SharedModule } from '../shared/shared.module';
import { CompaniesComponent } from '../../components/dealer/companies/companies.component';
import { ProductsComponent } from '../../components/dealer/products/products.component';
import { OrdersDealerComponent } from '../../components/dealer/orders/orders.component';

const dealerZoneRoutes: Routes = [
	{ path: '', redirectTo: '/companies', pathMatch: 'full' },
	{ path: 'companies', component: CompaniesComponent },
	{ path: 'products/:id', component: ProductsComponent },
	{ path: 'orders', component: OrdersDealerComponent }
];

@NgModule({
	imports: [
		CommonModule,
		FormsModule,
		HttpModule,
		SharedModule,
		RouterModule.forChild(dealerZoneRoutes)
	],
	declarations: [
		CompaniesComponent,
		ProductsComponent,
		OrdersDealerComponent
	],
	providers: [
	],
	exports: [
		RouterModule
	]
})

export class DealerModule { }
