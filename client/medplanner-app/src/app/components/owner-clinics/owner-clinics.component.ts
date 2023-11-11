import { Component, OnInit } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { ToastrService } from 'ngx-toastr';
import { Observable } from 'rxjs';
import { Clinic } from 'src/app/models/clinic';
import { ClinicOwnerServiceService } from 'src/app/services/clinic-owner/clinic-owner-service.service';
import { PatientRegisterDialogComponent } from '../patient-register-dialog/patient-register-dialog.component';
import { AddClinicComponent } from '../add-clinic/add-clinic.component';
import { Router } from '@angular/router';

@Component({
    selector: 'app-owner-clinics',
    templateUrl: './owner-clinics.component.html',
    styleUrls: ['./owner-clinics.component.scss']
})
export class OwnerClinicsComponent implements OnInit {

    clinics$: Observable<Clinic[]>;
    currentPage = 1;
    pageSize = 10;
    name = '';
    address = '';

    constructor(private clinicOwnerService: ClinicOwnerServiceService, private dialog: MatDialog, private toastr: ToastrService, private router: Router) {
    }

    ngOnInit(): void {
        this.loadClinics();
    }

    openAddClinic(): void {
        const dialogRef = this.dialog.open(AddClinicComponent, {
            width: '400px',
            height: '700px',
            position: {
                top: '300px'
            }
        });

        dialogRef.afterClosed().subscribe(result => {
            this.clinics$ = this.clinicOwnerService.getAllClinics();
        });
    }

    deleteClinic(clinic: Clinic): void {
        this.clinicOwnerService.deleteClinic(clinic.id).subscribe(
            () => {
                this.toastr.success('Klinika została usunięta', 'Sukces');
                this.clinics$ = this.clinicOwnerService.getAllClinics();
            },
            (error) => {
                this.toastr.error('Wystąpił błąd podczas usuwania kliniki', 'Błąd');
                console.error(error);
            }
        );
    }

    showDetails(clinic: Clinic): void {
        this.router.navigate(['ownerClinics', clinic.id]);
    }

    loadClinics(): void {
        this.clinics$ = this.clinicOwnerService.getAllClinics(this.currentPage, this.pageSize, this.address, this.name);
    }

    onPageChange(newPage: number) {
        this.currentPage = newPage;
        this.loadClinics();
    }

}
