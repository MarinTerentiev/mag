import { Component } from '@angular/core';
import { LoaderService } from '../../../shared/service/inner/loader.service';

@Component({
    selector: 'app-loader',
    templateUrl: './loader.component.html',
    styleUrls: ['./loader.component.less']
})
export class LoaderComponent {

    constructor(
        public loaderService: LoaderService
    ) { }

}
