import { Component } from '@angular/core';
import { BaseComponent } from '../../../shared/base.component';
import { ResetPassword } from '../../../shared/model/data/reset-password.model';
import { UserService } from '../../../shared/service/api/user.service';
import { Router } from '@angular/router';

@Component({
    selector: 'app-reset-password',
    templateUrl: './reset-password.component.html',
    styleUrls: ['./reset-password.component.less'],
    providers: [UserService]
})
export class ResetPasswordComponent extends BaseComponent {
    public resetPassword: ResetPassword = new ResetPassword();

    constructor(
        private userService: UserService,
        private router: Router
    ) {
        super();
    }

    public reset(): void {
        if (this.resetPassword.Password !== this.resetPassword.PasswordConfirm) {
            alert('Password is not matched');
            return;
        }

        this.userService.resetPassword(this.resetPassword).then((result) => {
            this.router.navigateByUrl('/login');
            return;
        });
    }

    public isValid(): boolean {
        if (this.resetPassword.Key.length > 2 && 
            this.resetPassword.Password.length > 2 &&
            this.resetPassword.PasswordConfirm.length > 2 && 
            this.resetPassword.UserName.length > 2
        ) {
            return true;
        }
        return false;
    }
}