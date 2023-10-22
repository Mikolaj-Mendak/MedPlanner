import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MatDialogRef } from '@angular/material/dialog';
import { ToastrService } from 'ngx-toastr';
import { AuthService } from 'src/app/services/auth-service';
import { PatientRegisterDialogComponent } from '../patient-register-dialog/patient-register-dialog.component';
import { ClinicOwnerServiceService } from 'src/app/services/clinic-owner/clinic-owner-service.service';
import { TimepickerConfig } from 'ngx-bootstrap/timepicker';
import { AddClinicDto, DayOfWeek } from 'src/app/DTOs/add-clinic-dto';

@Component({
    selector: 'app-add-clinic',
    templateUrl: './add-clinic.component.html',
    styleUrls: ['./add-clinic.component.scss']
})
export class AddClinicComponent implements OnInit {

    addClinicForm: FormGroup;
    daysOfWeek: any[] = [
        { value: 0, label: 'Poniedziałek' },
        { value: 1, label: 'Wtorek' },
        { value: 2, label: 'Środa' },
        { value: 3, label: 'Czwartek' },
        { value: 4, label: 'Piątek' },
        { value: 5, label: 'Sobota' },
        { value: 6, label: 'Niedziela' }
    ];


    constructor(
        private formBuilder: FormBuilder,
        private dialogRef: MatDialogRef<PatientRegisterDialogComponent>,
        private toastr: ToastrService,
        private clinicOwnerService: ClinicOwnerServiceService,
    ) {
        this.setAddClinicForm();
    }

    ngOnInit(): void {

    }


    setAddClinicForm(): void {
        this.addClinicForm = this.formBuilder.group({
            name: ['', Validators.required],
            address: ['', Validators.required],
            nip: [''],
            isNFZ: [false],
            isPrivate: [false],
            officeHoursStartDate: [null],
            officeHoursEndDate: [null],
            workingDays: this.formBuilder.array([]),
            phoneNumber: [null]
        });
    }

    onSubmit() {
        if (this.addClinicForm.valid) {
            const formData = this.addClinicForm.value;
            formData.officeHoursStartDate = this.convertTimeStringToDate(formData.officeHoursStartDate);
            formData.officeHoursEndDate = this.convertTimeStringToDate(formData.officeHoursEndDate);

            formData.workingDays = this.convertSelectedDaysToNumbersList(formData.workingDays);

            this.clinicOwnerService.addClinic(formData as AddClinicDto).subscribe(
                (response) => {
                    this.dialogRef.close();
                },
                (error) => {
                    console.error(error);
                }
            );
        }
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

    convertSelectedDaysToNumbersList(selectedDays: number[]): number[] {
        const numbersList: number[] = [];

        selectedDays.forEach((value: number) => {
            numbersList.push(value);
        });

        return numbersList;
    }
}
