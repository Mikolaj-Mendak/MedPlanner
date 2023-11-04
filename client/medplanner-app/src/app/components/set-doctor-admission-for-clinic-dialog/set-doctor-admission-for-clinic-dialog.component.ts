import { Component, OnInit } from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { MatDialog, MatDialogRef } from '@angular/material/dialog';
import { ActivatedRoute, Route, Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
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
    clinicId: string;

    constructor(
        private doctorService: DoctorService,
        private route: ActivatedRoute,
        private dialog: MatDialog,
        private fb: FormBuilder,
        private router: Router,
        private toastr: ToastrService,
        private dialogRef: MatDialogRef<SetDoctorAdmissionForClinicDialogComponent>,
    ) {

        this.clinicId = this.getIdFromUrl(this.router.url);
    }

    ngOnInit(): void {
        this.loadDoctorAdmission();
        this.admissionForm = this.fb.group({
            clinicId: [this.clinicId, Validators.required],
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

    private loadDoctorAdmission(): void {
        if (this.clinicId) {
            this.doctorAdmission$ = this.doctorService.getAdmissionByClinicForDoctor(this.clinicId.toString());
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

    onSubmit() {
        const formData = this.admissionForm.value;

        const addDoctorAdmission: DoctorAdmissionConditions = {
            clinicId: formData.clinicId,
            doctorId: formData.doctorId,
            specialization: formData.specialization,
            isNFZ: formData.isNFZ,
            isPrivate: formData.isPrivate,
            consultationFee: formData.consultationFee,
            workingDays: formData.workingDays,
            workHoursStart: this.convertTimeStringToDate(formData.workHoursStart),
            workHoursEnd: this.convertTimeStringToDate(formData.workHoursEnd)
        };

        this.doctorService.addAdmissionCondition(addDoctorAdmission).subscribe(
            (user) => {
                this.toastr.success('Dodano warunki pracy.', 'Sukces');
                this.dialogRef.close();
            },
            (error) => {
                this.toastr.error('Błąd dodawania warunków pracy');
                this.dialogRef.close();
            }
        );
    }

    private getIdFromUrl(url: string): string {
        const segments = url.split('/');
        const idSegment = segments[segments.length - 1];
        return idSegment;
    }

    convertTimeStringToDate(timeString: string): Date | null {
        if (timeString) {
            const [hours, minutes] = timeString.split(':');
            if (hours && minutes) {
                const date = new Date();
                date.setHours(Number(hours));
                date.setMinutes(Number(minutes));
                date.setSeconds(0);
                return date;
            }
        }
        return null;
    }
}
