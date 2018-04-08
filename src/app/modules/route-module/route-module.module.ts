import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { HttpModule } from '@angular/http';
import { RouterModule, Routes, PreloadAllModules } from '@angular/router';
import { ModuleWithProviders } from '@angular/core/core';

import { SharedModule } from '../shared/shared.module';

import { AuthGuard } from '../../shared/service/auth/auth-guard.service';
import { AuthAdminGuard } from '../../shared/service/auth/auth-admin-guard.service';
import { AuthDealerGuard } from '../../shared/service/auth/auth-dealer-guard.service';

import { TestComponent } from '../../components/test/test.component';
import { LoginComponent } from '../../components/external/login/login.component';
import { HomeComponent } from '../../components/external/home/home.component';
import { SignUpComponent } from '../../components/external/sign-up/sign-up.component';
import { SignUpDealerComponent } from '../../components/external/sign-up-dealer/sign-up-dealer.component';
import { ResetPasswordComponent } from '../../components/external/reset-password/reset-password.component';
import { AboutComponent } from '../../components/external/about/about.component';

export const routes: Routes = [
    { path: '', redirectTo: 'home', pathMatch: 'full' },
    { path: 'test', component: TestComponent },
    { path: 'login', component: LoginComponent },
    { path: 'home', component: HomeComponent },
    { path: 'sign-up', component: SignUpComponent },
    { path: 'sign-up-dealer', component: SignUpDealerComponent },
    { path: 'reset-password', component: ResetPasswordComponent },
    { path: 'about', component: AboutComponent },
    {
        path: 'admin',
        loadChildren: 'app/modules/admin/admin.module#AdminModule',
        canActivate: [AuthAdminGuard]
    },
    {
        path: 'dealer',
        loadChildren: 'app/modules/dealer/dealer.module#DealerModule',
        canActivate: [AuthDealerGuard]
    },
    {
        path: 'customer',
        loadChildren: 'app/modules/customer/customer.module#CustomerModule',
        canActivate: [AuthGuard]
    },
    { path: '**', redirectTo: '' }
];

@NgModule({
    imports: [
        CommonModule,
		BrowserModule,
		FormsModule,
		HttpModule,
        SharedModule,
		RouterModule.forRoot(
			routes,
			{ 
                preloadingStrategy: PreloadAllModules, 
                enableTracing: false, 
                useHash: true 
            }
		)
    ],
    declarations: [
		TestComponent,
        LoginComponent,
        HomeComponent,
        SignUpComponent,
        SignUpDealerComponent,
        ResetPasswordComponent,
        AboutComponent
    ],
    providers: [
		AuthGuard,
        AuthAdminGuard,
        AuthDealerGuard
    ],
	exports: [
		RouterModule
	]
})

export class AppRoutingModule { }
