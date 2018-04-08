import { Component } from '@angular/core';
import { BaseComponent } from '../../../shared/base.component';
import { Router, NavigationStart } from '@angular/router';
import { AuthService } from '../../../shared/service/auth/auth.service';

@Component({
    selector: 'app-footer',
    templateUrl: './footer.component.html',
    styleUrls: ['./footer.component.less']
})
export class FooterComponent extends BaseComponent {
    private sub: any;
    public IsExternalMode = false;

    constructor(
        private router: Router,
        private authService: AuthService
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
        this.IsExternalMode = false;

        if (!this.authService.isAuthenticated) {
            this.IsExternalMode = true;
        }
    }
}
