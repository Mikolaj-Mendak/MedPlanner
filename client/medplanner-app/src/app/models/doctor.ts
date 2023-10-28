import { Clinic } from "./clinic";
import { ClinicDoctor } from "./clinic-doctor";
import { DoctorAdmissionConditions } from "./doctor-admission";
import { Photo } from "./photo";
import { User } from "./user";

export interface Doctor extends User {
    doctorNumber?: string;
    clinicWork?: Clinic[];
    admissionConditions?: DoctorAdmissionConditions[];
    photoId?: string;
    photo?: Photo;
    clinicDoctors?: ClinicDoctor[];
}