import { Component, OnInit,AfterViewInit, ViewChild } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Subject } from 'rxjs/Rx';
import { MatTableDataSource } from '@angular/material/table';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { LogDto } from './models/logdto';
import { LogDtoService } from './services/logdto.service';
import { LookUpService } from '../../../services/LookUp.service';
import { AlertifyService } from '../../../services/Alertify.service';
import { AuthService } from '../login/Services/Auth.service';


declare var jQuery: any;

@Component({
    selector: 'app-logDto',
    templateUrl: './logDto.component.html',
    styleUrls: ['./logDto.component.scss'],
    standalone: false
})
export class LogDtoComponent implements AfterViewInit, OnInit {

	dataSource: MatTableDataSource<any> = new MatTableDataSource<any>([]); 
	@ViewChild(MatPaginator) paginator?: MatPaginator;
	@ViewChild(MatSort) sort?: MatSort;
	displayedColumns: string[] = ['id', 'level', 'exceptionMessage','timeStamp','user','value','type'];	


	logDtoList?: LogDto[];
	logDto: LogDto = new LogDto();

	logDtoAddForm?: FormGroup;

	logDtoId?: number;
	dtTrigger: Subject<any> = new Subject<any>();

	constructor(private logDtoService: LogDtoService,
		private lookupService: LookUpService,
		private alertifyService: AlertifyService,
		private formBuilder: FormBuilder,
		private authService: AuthService) { }

	ngOnInit() {

		this.getLogDtoList();
	}

	getLogDtoList() {
		this.logDtoService.getLogDtoList().subscribe(data => {
			this.logDtoList = data;
			this.dataSource = new MatTableDataSource(data);
			this.configDataTable();
		});
	}

	ngOnDestroy(): void {
		this.dtTrigger.unsubscribe();
	}

	ngAfterViewInit(): void {
		this.getLogDtoList();
	}

	clearFormGroup(group: FormGroup) {

		group.markAsUntouched();
		group.reset();

		Object.keys(group.controls).forEach(key => {
			const control = group.get(key);
			if (control) {
				control.setErrors(null);
				if (key == 'id') {
					control.setValue(0);
				}
			}
		});
	}

	checkClaim(claim: string): boolean {
		return this.authService.claimGuard(claim)
	}

	configDataTable(): void {
		if (this.dataSource) {
			this.dataSource.paginator = this.paginator ?? null;
			this.dataSource.sort = this.sort ?? null;
		}
	  }

	  applyFilter(event: Event) {
		const filterValue = (event.target as HTMLInputElement).value;
		if (this.dataSource) {
			this.dataSource.filter = filterValue.trim().toLowerCase();
		}

		if (this.dataSource && this.dataSource.paginator) {
			this.dataSource.paginator.firstPage();
		}
	}

}
