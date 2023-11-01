import { Component, OnInit } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { ActivatedRoute } from '@angular/router';
import { Observable } from 'rxjs';
import { DoctorAdmissionConditions } from 'src/app/models/doctor-admission';
import { DoctorService } from 'src/app/services/doctor-service';
import { SetDoctorAdmissionForClinicDialogComponent } from '../set-doctor-admission-for-clinic-dialog/set-doctor-admission-for-clinic-dialog.component';

@Component({
    selector: 'app-doctor-admission-page-for-doctor',
    templateUrl: './doctor-admission-page-for-doctor.component.html',
    styleUrls: ['./doctor-admission-page-for-doctor.component.scss']
})
export class DoctorAdmissionPageForDoctorComponent implements OnInit {
    doctorAdmission$: Observable<DoctorAdmissionConditions>;
    daysOfWeek = ['Poniedziałek', 'Wtorek', 'Środa', 'Czwartek', 'Piątek'];

    constructor(
        private doctorService: DoctorService,
        private route: ActivatedRoute,
        private dialog: MatDialog
    ) { }

    ngOnInit(): void {
        this.route.paramMap.subscribe(params => {
            const clinicId = params.get('clinicId');
            this.loadDoctorAdmission(clinicId);
        });

    }

    private loadDoctorAdmission(clinicId: string): void {
        if (clinicId) {
            this.doctorAdmission$ = this.doctorService.getAdmissionByClinicForDoctor(clinicId.toString());
        } else {
            console.error('Brak wymaganych parametrów w URL.');
        }
    }

    formatTime(date: Date | null): string {
        if (date) {
            const formattedDate = new Date(date);
            const hours = formattedDate.getHours().toString().padStart(2, '0');
            const minutes = formattedDate.getMinutes().toString().padStart(2, '0');
            return `${hours}:${minutes}`;
        }
        return '';
    }

    formatWorkingDays(workingDays: number[]): string {
        const days = workingDays.map(day => this.daysOfWeek[day - 1]);
        return days.join(', ');
    }

    openDialog(): void {
        const dialogRef = this.dialog.open(SetDoctorAdmissionForClinicDialogComponent, {
            width: '400px',
            height: '700px',
            position: {
                top: '220px'
            }
        });

        dialogRef.afterClosed().subscribe(result => {
        });
    }
}
