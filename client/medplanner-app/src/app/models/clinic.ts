import { ClinicDoctor } from "./clinic-doctor";

export interface Clinic {
    id?: string;
    name?: string;
    address?: string;
    nip?: string;
    isNFZ?: boolean;
    isPrivate?: boolean;
    clinicOwnerId?: string;
    clinicDoctors?: ClinicDoctor[];
    officeHoursStartDate?: Date;
    officeHoursEndDate?: Date;
    workingDays?: string[]; // Załóżmy, że dni robocze są reprezentowane jako stringi (np. "Monday", "Tuesday", itd.).
}