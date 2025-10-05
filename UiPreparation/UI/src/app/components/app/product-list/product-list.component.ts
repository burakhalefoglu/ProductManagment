import { Component, ChangeDetectionStrategy, OnInit, Signal, DestroyRef, WritableSignal, signal, ViewChild } from '@angular/core';
import { CommonModule } from '@angular/common';
import { TranslateModule } from '@ngx-translate/core';
import { DxDataGridModule, DxDataGridComponent } from 'devextreme-angular';
import DataSource from 'devextreme/data/data_source';
import ArrayStore from 'devextreme/data/array_store';
import {HttpEntityRepositoryService} from '../../../core/services/http-entity-repository.service';
import {TitleService} from '../../../core/services/title.service';
import {Router} from '@angular/router';
import {Product} from '../models/product';

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
    @ViewChild(DxDataGridComponent) dataGrid!: DxDataGridComponent;

    private readonly _products: WritableSignal<Product[]> = signal([]);
    public readonly products: Signal<Product[]> = this._products.asReadonly();

    public dxDataSource: DataSource = new DataSource({
        store: new ArrayStore({ data: [], key: 'id' })
    });

    constructor(private productRepo: HttpEntityRepositoryService<Product>,
                private destroyRef: DestroyRef,
                private titleService: TitleService,
                private router: Router,) {}

    ngOnInit(): void {
        this.fetchProducts();
        this.titleService.setPageTitle('Products');
    }

    fetchProducts(): void {
        this.productRepo.getAll('/Products', this.destroyRef).subscribe({
            next: (data: any) => {
                console.log('raw data ok?', Array.isArray(data), data.length);
                if (!Array.isArray(data)) {
                    console.error('Beklenen array gelmedi', data);
                    return;
                }
                if (data.length === 0) {
                    console.warn('Gelen array boş.');
                } else {
                    console.log('first item keys:', Object.keys(data[0]));
                }

                // Eğer id alanı farklıysa burada key değiştir
                const keyName = 'id'; // veya 'Id' / 'productId' --> kontrol edip güncelle

                // 1) Sinyale de set et (uygulama mantığı için)
                this._products.set(data);

                // 2) Yeni ArrayStore ile dxDataSource'ı yeniden oluştur (en temiz yol)
                import('devextreme/data/array_store').then(({ default: ArrayStore }) => {
                    import('devextreme/data/data_source').then(({ default: DataSource }) => {
                        this.dxDataSource = new DataSource({
                            store: new ArrayStore({ data: data, key: keyName })
                        });

                        // 3) Grid instance varsa doğrudan option setle ve refresh et
                        try {
                            if (this.dataGrid && this.dataGrid.instance) {
                                // grid'e yeni datasource referansını ver
                                this.dataGrid.instance.option('dataSource', this.dxDataSource);
                                // küçük bir zaman aşımıyla refresh et (Angular + DevExtreme zamanlaması için güvenli)
                                setTimeout(() => {
                                    try { this.dataGrid.instance.refresh(); } catch(e){ /* ignore */ }
                                }, 0);
                            }
                        } catch (e) {
                            console.warn('Grid instance ayarlanamadı:', e);
                        }
                    });
                });
            },
            error: (err) => {
                console.error('Ürünler yüklenirken hata oluştu:', err);
            }
        });
    }

    goToDetail(id: any) {
        this.router.navigate(['/app/products/detail/' + id]);
    }

    openCreatePage() {
        this.router.navigate(['/app/products/add/']);
    }
    formatCurrency = (e: any) => {
        if (e?.value == null) return '';
        const formatted = e.value.toLocaleString('tr-TR', {
            style: 'currency',
            currency: 'TRY',
            minimumFractionDigits: 2,
            maximumFractionDigits: 2
        });
        return `Toplam: ${formatted}`;
    };
}
