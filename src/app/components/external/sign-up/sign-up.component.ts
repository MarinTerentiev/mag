import { Component } from '@angular/core';
import { BaseComponent } from '../../../shared/base.component';
import { SignUp } from '../../../shared/model/data/sign-up.model';
import { AuthService } from '../../../shared/service/auth/auth.service';
import { Router } from '@angular/router';
import { ModalTempService } from '../../../shared/service/inner/modal-temp.service';
import { UserService } from '../../../shared/service/api/user.service';
import { ModalTempConfig } from '../../../shared/model/inner/modal-temp-config.model';
import { ModalTempType } from '../../../shared/enum';
import { UserExists } from '../../../shared/model/data/user-exists.model';

@Component({
    selector: 'app-sign-up',
    templateUrl: './sign-up.component.html',
    styleUrls: ['./sign-up.component.less'],
    providers: [UserService]
})
export class SignUpComponent extends BaseComponent {
    public signUpModel: SignUp;

    constructor(
        private router: Router,
        private modalTempService: ModalTempService,
        private userService: UserService
    ) {
        super();

        this.signUpModel = new SignUp();
    }

    public signUp(): void {
        let errorMessage = '';
        let hasError = false;
        if (this.signUpModel.UserName.length < 4) {
            errorMessage += 'User name must be longer than 4 letter.<br>';
            hasError = true;
        }
        if (this.signUpModel.Email.length < 4) {
            errorMessage += 'Email must be longer than 4 letter.<br>';
            hasError = true;
        }
        if (this.signUpModel.Name.length < 4) {
            errorMessage += 'Name must be longer than 4 letter.<br>';
            hasError = true;
        }
        if (this.signUpModel.Password.length < 4) {
            errorMessage += 'Password must be longer than 4 letter.<br>';
            hasError = true;
        }
        if (this.signUpModel.Password != this.signUpModel.PasswordConfirm) {
            errorMessage += 'Password is not match.<br>';
        }
        let config = new ModalTempConfig(errorMessage, ModalTempType.danger);
        if (hasError) {       
            config.Time = 5000;
            this.modalTempService.show(config);
            return;
        }

        const userExist = new UserExists(this.signUpModel.UserName, this.signUpModel.Email);
        this.userService.existData(userExist).then(data1 => {       
            if (data1.Exist) {
                errorMessage = data1.Message;
                this.modalTempService.show(config);
                return;
            }

            this.userService.signUp(this.signUpModel).then(data => {
                this.router.navigateByUrl('/login');

            }).catch((error) => {
                config = new ModalTempConfig("error", ModalTempType.danger)
                this.modalTempService.show(config);
            });  
        }).catch((error) => {
            config = new ModalTempConfig("error", ModalTempType.danger)
            this.modalTempService.show(config);
        });   
    }
}
