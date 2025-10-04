import {Component, OnInit, AfterViewInit, ViewChild, ChangeDetectionStrategy} from '@angular/core';
import {FormBuilder, FormGroup, ReactiveFormsModule, Validators} from '@angular/forms';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import {MatTable, MatTableDataSource} from '@angular/material/table';
import { Language } from './models/language';
import { LanguageService } from './services/language.service';
import { LookUpService } from '../../../services/LookUp.service';
import { AlertifyService } from '../../../services/Alertify.service';
import {AuthService} from '../../public/login/Services/Auth.service';
import {CommonModule} from '@angular/common';
import {TranslateModule} from '@ngx-translate/core';
import {MatFormField, MatLabel} from '@angular/material/form-field';
import {SwalComponent, SwalDirective} from '@sweetalert2/ngx-sweetalert2';

declare var jQuery: any;


@Component({
    selector: 'app-language',
    templateUrl: './language.component.html',
    styleUrls: ['./language.component.scss'],
    standalone: true,
    changeDetection: ChangeDetectionStrategy.OnPush,
    imports: [
        CommonModule,
        TranslateModule,
        MatFormField,
        MatLabel,
        MatTable,
        SwalDirective,
        MatPaginator,
        ReactiveFormsModule,
        SwalComponent,
    ]
})
export class LanguageComponent implements AfterViewInit, OnInit {

dataSource: MatTableDataSource<any> = new MatTableDataSource<any>([]);
@ViewChild(MatPaginator) paginator!: MatPaginator;
@ViewChild(MatSort) sort!: MatSort;
displayedColumns: string[] = ['id', 'name', 'code', 'update', 'delete'];

languageList!: Language[];
language: Language = new Language();

languageAddForm!: FormGroup;

languageId!: number;


constructor(private languageService: LanguageService,
    private lookupService: LookUpService,
    private alertifyService: AlertifyService,
    private formBuilder: FormBuilder,
    private authService: AuthService) { }

ngAfterViewInit(): void {

    this.getLanguageList();
}

ngOnInit() {

    this.createLanguageAddForm();
}

getLanguageList() {
    this.languageService.getLanguageList().subscribe(data => {
        this.languageList = data;
        this.dataSource = new MatTableDataSource(data);
        this.configDataTable();
    });
}

save() {
    if (this.languageAddForm.valid) {
        this.language = Object.assign({}, this.languageAddForm.value)

        if (this.language.id === 0) {
            this.addLanguage();
        } else {
            this.updateLanguage();
        }
    }

}

addLanguage() {
    this.languageService.addLanguage(this.language).subscribe(data => {
        this.getLanguageList();
        this.language = new Language();
        jQuery('#language').modal('hide');
        this.alertifyService.success(data);
        this.clearFormGroup(this.languageAddForm);

    })

}

updateLanguage() {
    this.languageService.updateLanguage(this.language).subscribe(data => {

        const index = this.languageList.findIndex(x => x.id === this.language.id);
        this.languageList[index] = this.language;
        this.dataSource = new MatTableDataSource(this.languageList);
        this.configDataTable();
        this.language = new Language();
        jQuery('#language').modal('hide');
        this.alertifyService.success(data);
        this.clearFormGroup(this.languageAddForm);

    })

}

createLanguageAddForm() {
    this.languageAddForm = this.formBuilder.group({
        id: [0],
        name: ['', Validators.required],
        code: ['', Validators.required]
    })
}

deleteLanguage(languageId: number) {
    this.languageService.deleteLanguage(languageId).subscribe(data => {
        this.alertifyService.success(data.toString());
        this.languageList = this.languageList.filter(x => x.id !== languageId);
        this.dataSource = new MatTableDataSource(this.languageList);
        this.configDataTable();
    })
}

getLanguageById(languageId: number) {
    this.clearFormGroup(this.languageAddForm);
    this.languageService.getLanguage(languageId).subscribe(data => {
        this.language = data;
        this.languageAddForm.patchValue(data);
    })
}


clearFormGroup(group: FormGroup) {

    group.markAsUntouched();
    group.reset();

    Object.keys(group.controls).forEach(key => {
        const control = group.get(key);
        if (control) {
            control.setErrors(null);
            if (key === 'id') {
                control.setValue(0);
            }
        }
    });
}

checkClaim(claim: string): boolean {
    return this.authService.claimGuard(claim)
}

configDataTable(): void {
    this.dataSource.paginator = this.paginator;
    this.dataSource.sort = this.sort;
  }

  applyFilter(event: Event) {
    const filterValue = (event.target as HTMLInputElement).value;
    this.dataSource.filter = filterValue.trim().toLowerCase();

    if (this.dataSource.paginator) {
        this.dataSource.paginator.firstPage();
    }
}

}
