import { Component, OnInit } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { Observable, map } from 'rxjs';
import { Clinic } from 'src/app/models/clinic';
import { ClinicOwnerServiceService } from 'src/app/services/clinic-owner/clinic-owner-service.service';
import { DoctorService } from 'src/app/services/doctor-service';

@Component({
    selector: 'app-doctor-clinic-page',
    templateUrl: './doctor-clinic-page.component.html',
    styleUrls: ['./doctor-clinic-page.component.scss']
})
export class DoctorClinicPageComponent implements OnInit {

    clinics$: Observable<Clinic[]>;
    currentPage = 1;
    pageSize = 10;
    name = '';
    address = '';

    constructor(private doctorService: DoctorService, private dialog: MatDialog, private toastr: ToastrService, private router: Router) {
    }

    ngOnInit(): void {
        this.loadClinics();
    }


    resignFromClinic(id: string): void {
        this.doctorService.resingFromClinic(id).subscribe(
            () => {
                this.toastr.success('Klinika została usunięta', 'Sukces');
                this.clinics$ = this.doctorService.getClinicsForDoctor();
            },
            (error) => {
                this.toastr.error('Wystąpił błąd podczas usuwania kliniki', 'Błąd');
                console.error(error);
            }
        );
    }

    showAdmission(clinicId: string): void {
        this.router.navigate(['doctor', 'clinics', 'admission', clinicId]);
    }

    loadClinics(): void {
        this.clinics$ = this.doctorService.getClinicsForDoctor(this.currentPage, this.pageSize, this.address, this.name)
            .pipe(map(response => response["$values"]));
    }

    onPageChange(newPage: number) {
        this.currentPage = newPage;
        this.loadClinics();
    }

}
