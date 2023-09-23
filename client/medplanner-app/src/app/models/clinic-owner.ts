import { User } from "./User";
import { Clinic } from "./clinic";

export interface ClinicOwner extends User {
    clinics?: Clinic[];
}