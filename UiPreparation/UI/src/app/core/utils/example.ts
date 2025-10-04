// src/app/admin/product-list/product-list.component.ts (Örnek)

import { Component, ChangeDetectionStrategy, DestroyRef, inject } from '@angular/core';
import { HttpEntityRepositoryService } from '../../core/services/http-entity-repository.service';

class Product {
}

@Component({
    standalone: true,
    selector: 'app-product-list',
    changeDetection: ChangeDetectionStrategy.OnPush,
    template: ``
})
export class ProductListComponent {
    private productRepo = inject(HttpEntityRepositoryService<Product>);
    private destroyRef = inject(DestroyRef); // DestroyRef'i inject ediyoruz

    ngOnInit() {
        this.fetchProducts();
    }

    fetchProducts() {
        this.productRepo.getAll('/Products', this.destroyRef) // 🔥 Burada DestroyRef'i servise iletiyoruz
            .subscribe({
                next: (products) => {
                    console.log('Ürünler yüklendi ve otomatik iptal mekanizması aktif!');
                    // ... Ürünleri sinyale atama veya kullanma
                },
                error: (err) => {
                    // İstek iptal edildiğinde (sayfa değişimi) buraya düşer, hata mesajını filtreleyebilirsiniz.
                    console.log('API isteği iptal edildi veya hata oluştu.', err);
                }
            });
    }
}
