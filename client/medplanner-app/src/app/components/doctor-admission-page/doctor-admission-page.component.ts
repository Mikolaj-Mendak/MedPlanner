import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { Observable } from 'rxjs';
import { DoctorAdmissionConditions } from 'src/app/models/doctor-admission';
import { DoctorService } from 'src/app/services/doctor-service';

@Component({
    selector: 'app-doctor-admission-page',
    templateUrl: './doctor-admission-page.component.html',
    styleUrls: ['./doctor-admission-page.component.scss']
})

export class DoctorAdmissionPageComponent implements OnInit {
    doctorAdmission$: Observable<DoctorAdmissionConditions>;
    daysOfWeek = ['Poniedziałek', 'Wtorek', 'Środa', 'Czwartek', 'Piątek'];

    constructor(
        private doctorService: DoctorService,
        private route: ActivatedRoute
    ) { }

    ngOnInit(): void {
        this.route.paramMap.subscribe(params => {
            const doctorId = params.get('doctorId');
            const clinicId = params.get('clinicId');
            if (doctorId && clinicId) {
                this.loadDoctorAdmission(doctorId, clinicId);
            } else {
                console.error('Brak wymaganych parametrów w URL.');
            }
        });

    }

    private loadDoctorAdmission(doctorId: string, clinicId: string): void {
        if (doctorId && clinicId) {
            this.doctorAdmission$ = this.doctorService.getAdmissionByClinicAndDoctor(doctorId.toString(), clinicId.toString());
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
}