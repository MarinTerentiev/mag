import { ProductCard } from '../entity/product-card.model';
import { Base } from '../base.model';

export class Order extends Base {
    constructor () {
        super();

        this.ProductCards = [];
        this.Details = '';
        this.Address = '';
    }

    public Amount: number;
    public ProductCards: ProductCard[];
    public Address: string;
    public Details: string;
    public UserName: string;
    public UserId: number;
}
