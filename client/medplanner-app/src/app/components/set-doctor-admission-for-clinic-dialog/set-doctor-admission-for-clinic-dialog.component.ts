import { Component, OnInit } from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { MatDialog } from '@angular/material/dialog';
import { ActivatedRoute } from '@angular/router';
import { Observable } from 'rxjs';
import { DoctorAdmissionConditions } from 'src/app/models/doctor-admission';
import { DoctorService } from 'src/app/services/doctor-service';

@Component({
    selector: 'app-set-doctor-admission-for-clinic-dialog',
    templateUrl: './set-doctor-admission-for-clinic-dialog.component.html',
    styleUrls: ['./set-doctor-admission-for-clinic-dialog.component.scss']
})
export class SetDoctorAdmissionForClinicDialogComponent implements OnInit {
    doctorAdmission$: Observable<DoctorAdmissionConditions>;
    daysOfWeek: any[] = [
        { value: 0, label: 'Poniedziałek' },
        { value: 1, label: 'Wtorek' },
        { value: 2, label: 'Środa' },
        { value: 3, label: 'Czwartek' },
        { value: 4, label: 'Piątek' },
        { value: 5, label: 'Sobota' },
        { value: 6, label: 'Niedziela' }
    ];
    admissionForm: FormGroup;

    constructor(
        private doctorService: DoctorService,
        private route: ActivatedRoute,
        private dialog: MatDialog,
        private fb: FormBuilder
    ) { }

    ngOnInit(): void {
        this.route.paramMap.subscribe(params => {
            const clinicId = params.get('clinicId');
            this.loadDoctorAdmission(clinicId);
        });

        this.admissionForm = this.fb.group({
            clinicId: ['', Validators.required],
            doctorId: ['', Validators.required],
            specialization: [''],
            isNFZ: [false],
            isPrivate: [false],
            consultationFee: [0],
            workingDays: [[]],
            workHoursStart: [''],
            workHoursEnd: ['']
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

    onSubmit() {
        if (this.admissionForm.valid) {
            const formData = this.admissionForm.value;
            console.log(formData);
        }
    }
}
