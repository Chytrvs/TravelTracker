import { Airport } from './airport';

export interface User {
    Username:string;
    Email:string;
    Password:string;
    FavouriteAirport:Airport;
}
