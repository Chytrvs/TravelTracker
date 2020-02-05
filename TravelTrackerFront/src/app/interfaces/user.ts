import { Airport } from './airport';

export interface User {
    username:string;
    email:string;
    password:string;
    favouriteAirport:Airport;
}
