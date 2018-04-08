import { Base } from '../base.model';

export class Category extends Base {
    constructor() {
        super();

        this.Name = '';
    }

    public CompanyId: number;
    public Name: string;
}
