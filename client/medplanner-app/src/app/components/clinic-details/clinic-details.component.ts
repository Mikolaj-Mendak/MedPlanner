import { Component, OnInit } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { ActivatedRoute, Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { Observable, map } from 'rxjs';
import { Clinic } from 'src/app/models/clinic';
import { Doctor } from 'src/app/models/doctor';
import { ClinicOwnerServiceService } from 'src/app/services/clinic-owner/clinic-owner-service.service';
import { DoctorService } from 'src/app/services/doctor-service';
import { AddClinicComponent } from '../add-clinic/add-clinic.component';
import { AddDoctorToClinicComponent } from '../add-doctor-to-clinic/add-doctor-to-clinic.component';

@Component({
    selector: 'app-clinic-details',
    templateUrl: './clinic-details.component.html',
    styleUrls: ['./clinic-details.component.scss']
})

export class ClinicDetailsComponent implements OnInit {

    clinic$: Observable<Clinic>;
    doctors$: Observable<Doctor[]>;
    currentPage = 1;
    pageSize = 10;
    firstName: string = '';
    lastName: string = '';
    pesel: string = '';
    doctorNumber: string = '';

    constructor(
        private route: ActivatedRoute,
        private clinicOwnerService: ClinicOwnerServiceService,
        private doctorService: DoctorService,
        private dialog: MatDialog, private toastr: ToastrService, private router: Router
    ) { }

    ngOnInit(): void {
        this.getDoctors()
    }

    formatTime(date: Date | null): string {
        if (date) {
            const hours = date.getHours().toString().padStart(2, '0');
            const minutes = date.getMinutes().toString().padStart(2, '0');
            return `${hours}:${minutes}`;
        }
        return '';
    }

    getAdmissionByClinicAndDoctor(doctorId: string, clinicId: string) {
        this.doctorService.getAdmissionByClinicAndDoctor(doctorId, clinicId).subscribe(result => {
        });
    }

    addDoctorToClinic(clinicId: string, doctorId: string): void {
        this.clinicOwnerService.addDoctorToClinic(clinicId, doctorId).subscribe(() => {
            console.log('Lekarz został dodany do kliniki.');
        }, error => {
            console.error('Wystąpił błąd podczas dodawania lekarza do kliniki:', error);
        });
    }

    openAddDoctor(): void {
        const dialogRef = this.dialog.open(AddDoctorToClinicComponent, {
            width: '400px',
            height: '200px',
            position: {
                top: '300px'
            }
        });

        dialogRef.afterClosed().subscribe(result => {
            this.getDoctors();
        });
    }

    getDoctors(): void {
        this.route.paramMap.subscribe(params => {
            const clinicId = params.get('id');

            if (clinicId) {
                this.clinic$ = this.clinicOwnerService.getSingleClinic(clinicId).pipe(
                    map(clinic => {
                        clinic.officeHoursStartDate = clinic.officeHoursStartDate ? new Date(clinic.officeHoursStartDate) : null;
                        clinic.officeHoursEndDate = clinic.officeHoursEndDate ? new Date(clinic.officeHoursEndDate) : null;
                        return clinic;
                    })
                );
            }

            this.doctors$ = this.doctorService.getDoctorsForClinic(clinicId,
                this.currentPage,
                this.pageSize, this.firstName,
                this.lastName, this.doctorNumber,
                this.pesel);
        });
    }


    deleteDoctor(doctor: Doctor): void {
        const clinicId = this.getClinicIdFromUrl(this.router.url);
        this.clinicOwnerService.removeDoctorFromClinic(clinicId, doctor.id.toString()).subscribe(
            () => {
                this.toastr.success("Poprawnie usunięto!");
                this.doctors$ = this.doctorService.getDoctorsForClinic(clinicId);
            },
            (error) => {
                this.toastr.error("Wystąpił błąd.")
            }
        );
    }

    showAdmissionDetails(doctor: Doctor) {
        const clinicId = this.getClinicIdFromUrl(this.router.url);
        this.router.navigate(['doctorAdmission', clinicId, doctor.id]);
    }


    private getClinicIdFromUrl(url: string): string | null {
        const segments = url.split('/');
        const lastSegment = segments[segments.length - 1];
        return lastSegment ? lastSegment : null;
    }

    onPageChange(newPage: number) {
        this.currentPage = newPage;
        this.getDoctors();
    }
}