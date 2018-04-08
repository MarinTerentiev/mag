import { Component, OnDestroy } from '@angular/core';
import { BaseComponent } from '../../../shared/base.component';
import { Router, NavigationStart } from '@angular/router';
import { AuthService } from '../../../shared/service/auth/auth.service';
import { CardService } from '../../../shared/service/inner/card.service';

@Component({
    selector: 'app-header',
    templateUrl: './header.component.html',
    styleUrls: ['./header.component.less']
})
export class HeaderComponent extends BaseComponent implements OnDestroy  {
    private sub: any;

    public IsExternalMode = false;
    public IsCustomerMode = false;
    public IsDealerMode = false;
    public IsAdminMode = false;

    public IsAdminDealerActive = false;
    public IsAdminCustomerActive = false;
    public IsAdminOrderActive = false;
    public IsDealerCompanyActive = false;
    public IsDealerOrderActive = false;
    public IsCustomerCatalogActive = false;

    public userName = '';

    constructor(
        private router: Router,
        private authService: AuthService,
        private cardService: CardService
    ) {
        super();

        this.sub = router.events.subscribe(event => {
            if (event instanceof NavigationStart) {
                this.urlChange(event.url);
            }
        });
    }

    ngOnDestroy() {
        this.sub.unsubscribe();
    }

    private urlChange(url: string) {
        // console.log(url);

        this.IsAdminDealerActive = url.indexOf('/admin/dealers') > -1;
        this.IsAdminCustomerActive = url.indexOf('/admin/customers') > -1;
        this.IsAdminOrderActive = url.indexOf('/admin/orders') > -1;
        this.IsDealerCompanyActive = url.indexOf('/dealer/companies') > -1 || url.indexOf('/dealer/products') > -1;
        this.IsDealerOrderActive = url.indexOf('/dealer/orders') > -1;;
        this.IsCustomerCatalogActive = url.indexOf('/customer/catalog') > -1;

        this.IsAdminMode = false;
        this.IsDealerMode = false;
        this.IsCustomerMode = false;
        this.IsExternalMode = false;
        
        if (this.authService.isAdmin) {
            this.IsAdminMode = true;
        } else if (this.authService.isDealer) {
            this.IsDealerMode = true;
        } else if (this.authService.isAuthenticated) {
            this.IsCustomerMode = true;
        } else {
            this.IsExternalMode = true;
        }
    }

    logout() {
        this.authService.logout();
    }

    getTotal(): number {
        return this.cardService.getTotal();
    }
}
