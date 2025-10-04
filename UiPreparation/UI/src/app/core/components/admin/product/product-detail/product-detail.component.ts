import { Component, ChangeDetectionStrategy, inject, OnInit, Signal, WritableSignal, signal, DestroyRef } from '@angular/core';
import { CommonModule } from '@angular/common';
import { TranslateModule } from '@ngx-translate/core';
import {ActivatedRoute, Router, RouterLink} from '@angular/router';
import {HttpEntityRepositoryService} from '../../../../services/http-entity-repository.service';

interface Product {
    id: number;
    name: string;
    unitPrice: number;
    unitsInStock: number;
    colors: { colorName: string }[];
}

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
    private productRepo = inject(HttpEntityRepositoryService<Product>);
    private route = inject(ActivatedRoute);
    private router = inject(Router);
    private destroyRef = inject(DestroyRef);

    private readonly _product: WritableSignal<Product | undefined> = signal(undefined);
    public readonly product: Signal<Product | undefined> = this._product.asReadonly();

    productId!: number;

    ngOnInit(): void {
        this.productId = Number(this.route.snapshot.paramMap.get('id'));
        this.fetchProductDetails();
    }

    fetchProductDetails(): void {
        if (!this.productId) {
            this.router.navigate(['/admin/products']);
            return;
        }

        this.productRepo.get('Products/', this.productId, this.destroyRef)
            .subscribe({
                next: (product) => {
                    this._product.set(product);
                    console.log('Ürün detayları yüklendi.');
                },
                error: (err) => {
                    console.error('Ürün detayları çekilirken hata oluştu:', err);
                }
            });
    }
}
