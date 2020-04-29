import { Component, OnInit } from "@angular/core";
import { HttpClient } from "@angular/common/http";
import { AuthService } from 'src/services/auth.service';
import { AlertifyService } from 'src/services/alertify.service';
import { Airport } from '../interfaces/airport';
import { environment } from 'src/environments/environment';
import { FormGroup, FormControl, Validators } from '@angular/forms';

@Component({
  selector: "app-flight",
  templateUrl: "./flight.component.html",
  styleUrls: ["./flight.component.css"]
})
export class FlightComponent implements OnInit {
  airports: Airport[];
  flightForm = new FormGroup({
    departureAirport: new FormControl(null,[Validators.required]),
    destinationAirport: new FormControl(null,[Validators.required]),
  },this.flightUniqueAirportsValidator);
  

  flightUniqueAirportsValidator(group: FormGroup){
    return group.get('departureAirport').value!=group.get('destinationAirport').value ? null : {"notUnique":true}
  }

  constructor(private http: HttpClient,private auth: AuthService,private alertify:AlertifyService) {}

  ngOnInit() {
    
    this.getAirports();
  }
  getAirports() {
    this.http.get<Airport[]>(`${environment.baseURL}/api/Flight/GetAirports`).subscribe(
      response => {
        this.airports = response;
      },
      error => {
        console.log(error);
      }
    );
  }
  addFlight() {
   if(this.flightForm.valid){
     this.http
      .post(`${environment.baseURL}/api/Flight/AddFlight`, {
        Username: this.auth.decodedToken.unique_name,
        DepartureAirportAcronym: this.flightForm.get('departureAirport').value.Acronym,
        DestinationAirportAcronym: this.flightForm.get('destinationAirport').value.Acronym
      })
      .subscribe(
        data => {
          this.alertify.success("Successfully added new flight")
        },
        error => {
          this.alertify.error("Failed to add new flight")
        }
      );
   }
   else{
    this.alertify.error("Failed to add new flight")
   }
    
      }
}
