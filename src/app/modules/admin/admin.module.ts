import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { HttpModule } from '@angular/http';
import { RouterModule, Routes } from '@angular/router';

import { SharedModule } from '../shared/shared.module';
import { DealersComponent } from '../../components/admin/dealers/dealers.component';
import { CustomersComponent } from '../../components/admin/customers/customers.component';
import { OrdersComponent } from '../../components/admin/orders/orders.component';

const adminZoneRoutes: Routes = [
	{ path: '', redirectTo: '/dealers', pathMatch: 'full' },
	{ path: 'dealers', component: DealersComponent },
	{ path: 'customers', component: CustomersComponent },
	{ path: 'orders', component: OrdersComponent }
];

@NgModule({
	imports: [
		CommonModule,
		FormsModule,
		HttpModule,
		SharedModule,
		RouterModule.forChild(adminZoneRoutes)
	],
	declarations: [
		DealersComponent,
		CustomersComponent,
		OrdersComponent
	],
	providers: [
	],
	exports: [
		RouterModule
	]
})

export class AdminModule { }
