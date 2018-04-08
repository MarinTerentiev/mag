import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { BaseComponent } from '../../../shared/base.component';
import { Auth } from '../../../shared/model/data/auth.model';
import { AuthService } from '../../../shared/service/auth/auth.service';
import { ModalTempService } from '../../../shared/service/inner/modal-temp.service';
import { ModalTempConfig } from '../../../shared/model/inner/modal-temp-config.model';
import { ModalTempType } from '../../../shared/enum';

@Component({
    selector: 'app-login',
    templateUrl: './login.component.html',
    styleUrls: ['./login.component.less']
})
export class LoginComponent extends BaseComponent {
    public auth: Auth;

    constructor(
        private authService: AuthService,
        private router: Router,
        private modalTempService: ModalTempService
    ) {
        super();

        this.auth = new Auth;
    }

    public login(): void {
        if (this.auth.UserName === '') {
            this.modalTempService.show(new ModalTempConfig('Login id empty', ModalTempType.danger));
            return;
        }
        if (this.auth.Password === '') {
            this.modalTempService.show(new ModalTempConfig('Password id empty', ModalTempType.danger));
            return;
        }

        this.authService.login(this.auth).then((result) => {
            if (result !== true) {
                this.modalTempService.show(new ModalTempConfig('Login is failed', ModalTempType.danger));
            } else {
                if (this.authService.isAdmin) {
                    this.router.navigateByUrl('/admin/dealers');
                    return;
                }
                if (this.authService.isDealer) {
                    this.router.navigateByUrl('/dealer/companies');
                    return;
                }
                this.router.navigateByUrl('/customer/catalog');
                return;
            }
        }).catch(error => {
            console.log(error);
        });
    }
}
