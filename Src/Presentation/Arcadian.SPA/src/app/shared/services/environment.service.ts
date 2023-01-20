import { InjectionToken, Provider } from "@angular/core";
import { environment } from "src/environments/environment";

export const API_URL: InjectionToken<string> = new InjectionToken('');

export const environmentInjectables: Array<Provider> = [
    { provide: API_URL, useValue: environment.apiUrl }
]