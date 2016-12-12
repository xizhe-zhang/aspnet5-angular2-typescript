import { Component, OnInit } from '@angular/core';
import { Product } from '../core/domain/product';
import { Paginated } from '../core/common/paginated';
import { DataService } from '../core/services/data.service';
import { UtilityService } from '../core/services/utility.service';
import { FeedService } from '../core/services/feed.service';

@Component({
    selector: 'customers',
    templateUrl: './app/components/customers.component.html'
})
export class CustomersComponent extends Paginated implements OnInit {
    private _productAPI: string = 'api/products/';
    private _products: Array<Product>;
    private cust: any;
    private connectionID: string;

    constructor(public productsService: DataService, public utilityService: UtilityService, public feedService: FeedService, ) {
        super(0, 0, 0);
        this.cust = new Cust();
        let lines = new OrderLine();
        this.cust.AddCust(1, "1", "1", lines);
        this.cust.setActiveCust(1);
    }

    ngOnInit() {
        this.feedService.addFeed.subscribe(
            feed => {
                console.log(feed);
                this.connectionID = this.utilityService.getConnectionId();
                if (feed.SessionKey === this.connectionID) {
                    this.getCustomer();
                }
            }
        )
    }

    getCustomer(): void {
        this.productsService.set(this._productAPI, 12);
        this.productsService.getProducts("1", 0)
            .subscribe(res => {
                var data: any = res.json();
                this._products = [];
                this.cust = new Cust();
                let lines = new OrderLine();
                for (let product of data.Items) {
                    this._products.push(new Product(product.Id, product.Code, product.Name, product.Price, product.Barcode, product.StoreId, product.StoreName, product.PicUrl));
                }
                let dat = new Date()
                let sec = dat.getSeconds();
                let index = 0;
                for (let product of this._products) {
                    if (index <= sec % 5)
                        lines.AddOrderLine(product.SkuName, Math.floor(Math.random() * (5 - 1) + 1), product.SkuPrice, product.SkuCode);
                    index = index + 1;
                }
                let feed = this.utilityService.getFeed();
                this.cust.AddCust(feed.Id, "1", feed.WechatName, lines)
                this.cust.setActiveCust(feed.Id);
            },
            error => console.error('Error: ' + error));
    }

}