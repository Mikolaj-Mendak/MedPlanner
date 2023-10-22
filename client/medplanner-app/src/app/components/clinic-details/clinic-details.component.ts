import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { Observable, map } from 'rxjs';
import { Clinic } from 'src/app/models/clinic';
import { ClinicOwnerServiceService } from 'src/app/services/clinic-owner/clinic-owner-service.service';

@Component({
    selector: 'app-clinic-details',
    templateUrl: './clinic-details.component.html',
    styleUrls: ['./clinic-details.component.scss']
})

export class ClinicDetailsComponent implements OnInit {

    clinic$: Observable<Clinic>;

    constructor(
        private route: ActivatedRoute,
        private clinicOwnerService: ClinicOwnerServiceService
    ) { }

    ngOnInit(): void {
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
        });
    }

    formatTime(date: Date | null): string {
        if (date) {
            const hours = date.getHours().toString().padStart(2, '0');
            const minutes = date.getMinutes().toString().padStart(2, '0');
            return `${hours}:${minutes}`;
        }
        return '';
    }
}