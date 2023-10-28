import { Component, Input, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { Observable } from 'rxjs';
import { Doctor } from 'src/app/models/doctor';
import { ClinicOwnerServiceService } from 'src/app/services/clinic-owner/clinic-owner-service.service';
import { DoctorService } from 'src/app/services/doctor-service';

@Component({
    selector: 'app-add-doctor-to-clinic',
    templateUrl: './add-doctor-to-clinic.component.html',
    styleUrls: ['./add-doctor-to-clinic.component.scss']
})
export class AddDoctorToClinicComponent implements OnInit {

    @Input() clinicId: string;

    doctors$: Observable<Doctor[]>;
    addDoctorToClinic: FormGroup;

    constructor(
        private router: Router,
        private clinicService: ClinicOwnerServiceService,
        private formBuilder: FormBuilder
    ) {
        this.addDoctorToClinic = this.formBuilder.group({
            doctorNumber: ['', Validators.required]
        });

    }

    ngOnInit(): void {

    }

    onSubmit(): void {
        if (this.addDoctorToClinic.valid) {
            const doctorNumber = this.addDoctorToClinic.value.doctorNumber;
            const clinicId = this.getClinicIdFromUrl(this.router.url);

            this.clinicService.addDoctorToClinicByNumber(clinicId, doctorNumber).subscribe(
                () => {
                    console.log('Lekarz został dodany do kliniki.');
                },
                (error) => {
                    console.error('Wystąpił błąd podczas dodawania lekarza do kliniki:', error);
                }
            );
        }
    }

    private getClinicIdFromUrl(url: string): string | null {
        const segments = url.split('/');
        const lastSegment = segments[segments.length - 1];
        return lastSegment ? lastSegment : null;
    }
}
