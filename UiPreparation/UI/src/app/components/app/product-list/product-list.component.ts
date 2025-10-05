import { Component, ChangeDetectionStrategy, OnInit, Signal, DestroyRef, WritableSignal, signal } from '@angular/core';
import { CommonModule } from '@angular/common';
import { TranslateModule } from '@ngx-translate/core';
import { DxDataGridModule } from 'devextreme-angular';
import {HttpEntityRepositoryService} from '../../../core/services/http-entity-repository.service';
import {TitleService} from '../../../core/services/title.service';

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
    private readonly _products: WritableSignal<Product[]> = signal([]);
    public readonly products: Signal<Product[]> = this._products.asReadonly();
    constructor(private productRepo: HttpEntityRepositoryService<Product>,
                private destroyRef: DestroyRef,
                private titleService: TitleService) {}

    ngOnInit(): void {
        this.fetchProducts();
        this.titleService.setPageTitle('Products');

    }

    fetchProducts(): void {
        this.productRepo.getAll('Products', this.destroyRef)
            .subscribe({
                next: (data) => {
                    this._products.set(data);
                    console.log('Ürünler DevExtreme için yüklendi.');
                },
                error: (err) => {
                    console.error('Ürünler yüklenirken hata oluştu:', err);
                }
            });
    }

    goToDetail(id: any) {}
}
