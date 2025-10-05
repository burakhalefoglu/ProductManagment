import { Component, ChangeDetectionStrategy, OnInit, DestroyRef } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ReactiveFormsModule, FormBuilder, FormGroup, Validators, FormArray } from '@angular/forms';
import { TranslateModule } from '@ngx-translate/core';
import {ActivatedRoute, Router, RouterLink} from '@angular/router';
import {HttpEntityRepositoryService} from '../../../core/services/http-entity-repository.service';
import {TitleService} from '../../../core/services/title.service';
import {Product} from '../models/product';

@Component({
    standalone: true,
    selector: 'app-product-update',
    templateUrl: './product-update.component.html',
    styleUrls: ['./product-update.component.scss'],
    changeDetection: ChangeDetectionStrategy.OnPush,
    imports: [
        CommonModule,
        ReactiveFormsModule,
        TranslateModule,
        RouterLink
    ]
})
export class ProductUpdateComponent implements OnInit {
    productForm!: FormGroup;
    productId!: number;
    constructor(private fb: FormBuilder,
                private productRepo: HttpEntityRepositoryService<Product>,
                private route: ActivatedRoute,
                private router: Router,
                private destroyRef: DestroyRef,
                private titleService: TitleService) {
    }
    ngOnInit(): void {
        this.productId = Number(this.route.snapshot.paramMap.get('id'));
        this.initializeForm();
        this.fetchProductDetails();
        this.titleService.setPageTitle('Product Update');
    }

    initializeForm(): void {
        this.productForm = this.fb.group({
            name: ['', Validators.required],
            unitPrice: [0, [Validators.required, Validators.min(0.01)]],
            unitsInStock: [0, [Validators.required, Validators.min(0)]],
            colors: this.fb.array([])
        });
    }

    fetchProductDetails(): void {
        if (!this.productId) {
            this.router.navigate(['/app/products']);
            return;
        }

        this.productRepo.get('/Products/', this.productId, this.destroyRef)
            .subscribe({
                next: (product) => {
                    this.fillForm(product);
                },
                error: (err) => {
                    console.error('Ürün detayları çekilirken hata oluştu:', err);
                }
            });
    }

    fillForm(product: Product): void {
        this.productForm.patchValue({
            name: product.name,
            unitPrice: product.unitPrice,
            unitsInStock: product.unitsInStock,
        });
        this.colorsArray.clear();
        product.colors.forEach(color => {
            this.colorsArray.push(this.createColorGroup(color.colorName || color as any as string));
        });
        if (this.colorsArray.length === 0) {
            this.addColor();
        }
    }
    get colorsArray(): FormArray {
        return this.productForm.get('colors') as FormArray;
    }

    createColorGroup(initialColorName: string = ''): FormGroup {
        return this.fb.group({
            colorName: [initialColorName, Validators.required]
        });
    }

    addColor(): void {
        this.colorsArray.push(this.createColorGroup());
    }

    removeColor(index: number): void {
        this.colorsArray.removeAt(index);
    }

    onSubmit(): void {
        if (this.productForm.valid) {
            console.log('Ürün Güncelleniyor:', this.productId, this.productForm.value);

            const rawColors = this.productForm.value.colors.map((c: { colorName: string }) => c.colorName);
            const finalProduct = new Product(
                this.productId,
                this.productForm.value.name,
                this.productForm.value.unitPrice,
               this.productForm.value.unitsInStock,
                rawColors)

            this.productRepo.update('/Products', finalProduct)
                .subscribe({
                    next: () => {
                        alert(`Ürün ID: ${this.productId} başarıyla güncellendi!`);
                        this.router.navigate(['/app/products']);
                    },
                    error: (err) => {
                        console.error('Ürün güncelleme hatası:', err);
                    }
                });
        }
    }
}
