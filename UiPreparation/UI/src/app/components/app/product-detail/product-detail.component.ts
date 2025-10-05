import { Component, ChangeDetectionStrategy, OnInit, Signal, WritableSignal, signal, DestroyRef } from '@angular/core';
import { CommonModule } from '@angular/common';
import { TranslateModule } from '@ngx-translate/core';
import {ActivatedRoute, Router, RouterLink} from '@angular/router';
import {HttpEntityRepositoryService} from '../../../core/services/http-entity-repository.service';
import {TitleService} from '../../../core/services/title.service';
import {Product} from '../models/product';

@Component({
    standalone: true,
    selector: 'app-product-detail',
    templateUrl: './product-detail.component.html',
    styleUrls: ['./product-detail.component.scss'],
    changeDetection: ChangeDetectionStrategy.OnPush,
    imports: [
        CommonModule,
        TranslateModule,
        RouterLink
    ]
})
export class ProductDetailComponent implements OnInit {
    productId!: number;
    private readonly _product: WritableSignal<Product | undefined> = signal(undefined);
    public readonly product: Signal<Product | undefined> = this._product.asReadonly();
    constructor(private productRepo: HttpEntityRepositoryService<Product>,
                private route: ActivatedRoute,
                private router: Router,
                private destroyRef: DestroyRef,
                private titleService: TitleService,
                ) {}
    ngOnInit(): void {
        this.productId = Number(this.route.snapshot.paramMap.get('id'));
        this.fetchProductDetails();
        this.titleService.setPageTitle('Product Detail');
    }

    fetchProductDetails(): void {
        if (!this.productId) {
            this.router.navigate(['/app/products']);
            return;
        }

        this.productRepo.get('/Products/', this.productId, this.destroyRef)
            .subscribe({
                next: (product) => {
                    if (!product) {
                        this.router.navigate(['/app/products']);
                        return;
                    }
                    console.log(product);
                    this._product.set(product);
                    console.log('Ürün detayları yüklendi.');
                },
                error: (err) => {
                    console.error('Ürün detayları çekilirken hata oluştu:', err);
                }
            });
    }
}
