import { Component, ChangeDetectionStrategy, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ReactiveFormsModule, FormBuilder, FormGroup, Validators, FormArray } from '@angular/forms';
import { TranslateModule } from '@ngx-translate/core';
import {Router, RouterLink} from '@angular/router';
import {HttpEntityRepositoryService} from '../../../core/services/http-entity-repository.service';
import {TitleService} from '../../../core/services/title.service';

@Component({
    standalone: true,
    selector: 'app-product-add',
    templateUrl: './product-add.component.html',
    styleUrls: ['./product-add.component.scss'],
    changeDetection: ChangeDetectionStrategy.OnPush,
    imports: [
        CommonModule,
        ReactiveFormsModule,
        TranslateModule,
        RouterLink
    ]
})
export class ProductAddComponent implements OnInit {
    productForm!: FormGroup;

    constructor(private fb: FormBuilder,
                private router: Router,
                private productRepo: HttpEntityRepositoryService<Product>,
                private titleService: TitleService) {}

    ngOnInit(): void {
        this.initializeForm();
        this.titleService.setPageTitle('Product Add');
    }

    initializeForm(): void {
        this.productForm = this.fb.group({
            name: ['', Validators.required],
            unitPrice: [0, [Validators.required, Validators.min(0.01)]],
            unitsInStock: [0, [Validators.required, Validators.min(0)]],
            colors: this.fb.array([
                this.createColorGroup('Kırmızı')
            ])
        });
    }

    createColorGroup(initialColorName: string = ''): FormGroup {
        return this.fb.group({
            colorName: [initialColorName, Validators.required]
        });
    }

    get colorsArray(): FormArray {
        return this.productForm.get('colors') as FormArray;
    }

    addColor(): void {
        this.colorsArray.push(this.createColorGroup());
    }

    removeColor(index: number): void {
        this.colorsArray.removeAt(index);
    }

    onSubmit(): void {
        if (this.productForm.valid) {
            console.log('Ürün Kayıt Ediliyor:', this.productForm.value);
            const rawColors = this.productForm.value.colors.map((c: { colorName: string }) => c.colorName);
            const finalProduct = new Product(
                 0,
                 this.productForm.value.name,
                 this.productForm.value.unitPrice,
                 this.productForm.value.unitsInStock,
                 rawColors
        );

        this.productRepo.add('Products', finalProduct)
            .subscribe({
                next: () => {
                    alert('Ürün başarıyla kaydedildi!');
                    this.router.navigate(['/app/products']);
                },
                error: (err: any) => {
                    console.error('Ürün kayıt hatası:', err);
                }
            });
        }
    }
}
