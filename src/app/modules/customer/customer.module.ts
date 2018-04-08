import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { HttpModule } from '@angular/http';
import { RouterModule, Routes } from '@angular/router';

import { SharedModule } from '../shared/shared.module';
import { CatalogComponent } from '../../components/customer/catalog/catalog.component';
import { CartComponent } from '../../components/customer/cart/cart.component';
import { CheckoutComponent } from '../../components/customer/checkout/checkout.component';
import { OrderComponent } from '../../components/customer/order/order.component';
import { ProductComponent } from '../../components/customer/product/product.component';

const customerZoneRoutes: Routes = [
	{ path: '', redirectTo: '/catalog', pathMatch: 'full' },
	{ path: 'catalog', component: CatalogComponent },
	{ path: 'cart', component: CartComponent },
	{ path: 'checkout', component: CheckoutComponent },
	{ path: 'order', component: OrderComponent },
	{ path: 'product/:id', component: ProductComponent }
];

@NgModule({
	imports: [
		CommonModule,
		FormsModule,
		HttpModule,
		SharedModule,
		RouterModule.forChild(customerZoneRoutes)
	],
	declarations: [
		CatalogComponent,
		CartComponent,
		CheckoutComponent,
		OrderComponent,
		ProductComponent
	],
	providers: [
	],
	exports: [
		RouterModule
	]
})

export class CustomerModule { }
