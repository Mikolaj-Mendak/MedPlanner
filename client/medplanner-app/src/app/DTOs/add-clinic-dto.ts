import { ClinicDoctor } from "../models/clinic-doctor";

export interface AddClinicDto {
    id?: string;
    name?: string;
    address?: string;
    nip?: string;
    isNFZ?: boolean;
    isPrivate?: boolean;
    clinicDoctors?: ClinicDoctor[];
    officeHoursStartDate?: Date;
    officeHoursEndDate?: Date;
    workingDays?: DayOfWeek[];
    phoneNumber?: string;
}


export enum DayOfWeek {
    Sunday = 0,
    Monday = 1,
    Tuesday = 2,
    Wednesday = 3,
    Thursday = 4,
    Friday = 5,
    Saturday = 6
}
