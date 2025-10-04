// src/app/admin/product-list/product-list.component.ts

import { Component, ChangeDetectionStrategy, inject, OnInit, Signal, DestroyRef, WritableSignal, signal } from '@angular/core';
import { CommonModule } from '@angular/common';
import { TranslateModule } from '@ngx-translate/core';
import { DxDataGridModule } from 'devextreme-angular';
import {HttpEntityRepositoryService} from '../../../../services/http-entity-repository.service';

interface Product {
    id: number;
    name: string;
    unitPrice: number;
    unitsInStock: number;
    colors?: string[];
}

@Component({
    standalone: true,
    selector: 'app-product-list',
    templateUrl: './product-list.component.html',
    styleUrls: ['./product-list.component.scss'],
    changeDetection: ChangeDetectionStrategy.OnPush,
    imports: [
        CommonModule,
        TranslateModule,
        DxDataGridModule,
    ]
})
export class ProductListComponent implements OnInit {
    private productRepo = inject(HttpEntityRepositoryService<Product>);
    private destroyRef = inject(DestroyRef);

    private readonly _products: WritableSignal<Product[]> = signal([]);
    public readonly products: Signal<Product[]> = this._products.asReadonly();

    ngOnInit(): void {
        this.fetchProducts();
    }

    fetchProducts(): void {
        this.productRepo.getAll('Products', this.destroyRef)
            .subscribe({
                next: (data) => {
                    this._products.set(data);
                    console.log('Ürünler DevExtreme için yüklendi.');
                },
                error: (err) => {
                    // İstek iptal edildiğinde buraya düşebilir.
                    console.error('Ürünler yüklenirken hata oluştu:', err);
                }
            });
    }

    //TODO:
    goToDetail(id: any) {}
}
