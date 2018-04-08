import { Base } from '../base.model';

export class Product extends Base {
    constructor() {
        super();

        this.Name = '';
        this.Description = '';
        this.Price = null;
        this.CategoryName = '';
        this.CategoryId = null;
        this.PhotoPath = '';
    }

    public CategoryId: number;
    public Name: string;
    public Description: string;
    public Enable: boolean;
    public EnableStr: string;
    public Price: number;
    public CategoryName: string;
    public PhotoPath: string;
}
