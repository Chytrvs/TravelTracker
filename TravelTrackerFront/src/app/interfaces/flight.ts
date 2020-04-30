import { Airport } from './airport';

export interface Flight {
    username:string;
    departureAirport:Airport;
    destinationAirport:Airport;
}
