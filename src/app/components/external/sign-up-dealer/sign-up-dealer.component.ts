import { Component } from '@angular/core';
import { BaseComponent } from '../../../shared/base.component';
import { SignUpDealer } from '../../../shared/model/data/sign-up-dealer.model';
import { Router } from '@angular/router';
import { ModalTempService } from '../../../shared/service/inner/modal-temp.service';
import { DealerService } from '../../../shared/service/api/dealer.service';
import { ModalTempConfig } from '../../../shared/model/inner/modal-temp-config.model';
import { ModalTempType } from '../../../shared/enum';
import { UserService } from '../../../shared/service/api/user.service';
import { DealerExists } from '../../../shared/model/data/dealer-exists.model';

@Component({
    selector: 'app-sign-up-dealer',
    templateUrl: './sign-up-dealer.component.html',
    styleUrls: ['./sign-up-dealer.component.less'],
    providers: [DealerService,UserService]
})
export class SignUpDealerComponent extends BaseComponent {
    public signUpDealer: SignUpDealer;
    public isSent = false;

    constructor(
        private router: Router,
        private modalTempService: ModalTempService,
        private dealerService: DealerService,
        private userService: UserService
    ) {
        super();

        this.signUpDealer = new SignUpDealer();
    }

    public signUp(): void {
        let errorMessage = '';
        let hasError = false;
        if (this.signUpDealer.DealerName.length < 4) {
            errorMessage += 'Dealer name must be longer than 4 letter.<br>';
            hasError = true;
        }
        if (this.signUpDealer.Email.length < 4) {
            errorMessage += 'Email must be longer than 4 letter.<br>';
            hasError = true;
        }
        if (this.signUpDealer.Name.length < 4) {
            errorMessage += 'Name must be longer than 4 letter.<br>';
            hasError = true;
        }
        if (this.signUpDealer.UserName.length < 4) {
            errorMessage += 'Login must be longer than 4 letter.<br>';
            hasError = true;
        }
        let config = new ModalTempConfig(errorMessage, ModalTempType.danger);
        if (hasError) {       
            config.Time = 5000;
            this.modalTempService.show(config);
            return;
        }

        const dealerExist = new DealerExists(this.signUpDealer.UserName, this.signUpDealer.Email, this.signUpDealer.Name);
        this.dealerService.existData(dealerExist).then(data1 => {         
            if (data1.Exist) {
                errorMessage = data1.Message;
                this.modalTempService.show(config);
                return;
            }

            this.dealerService.signUpDealer(this.signUpDealer).then(data => {
                this.isSent = true;
            }).catch((error) => {
                this.isSent = false;
                config = new ModalTempConfig("error", ModalTempType.danger)
                this.modalTempService.show(config);
            }); 
  
        }).catch((error) => {
            config = new ModalTempConfig("error", ModalTempType.danger)
            this.modalTempService.show(config);
        });
    }
}
