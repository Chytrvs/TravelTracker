import { Component, OnInit } from "@angular/core";
import { HttpClient } from "@angular/common/http";
import { AuthService } from 'src/services/auth.service';
import { AlertifyService } from 'src/services/alertify.service';
import { Airport } from '../interfaces/airport';
import { environment } from 'src/environments/environment';

@Component({
  selector: "app-flight",
  templateUrl: "./flight.component.html",
  styleUrls: ["./flight.component.css"]
})
export class FlightComponent implements OnInit {
  airports: Airport[];
  model: any={};
  constructor(private http: HttpClient,private auth: AuthService,private alertify:AlertifyService) {}

  ngOnInit() {
    this.getAirports();
  }
  getAirports() {
    this.http.get<Airport[]>(`${environment.baseURL}/api/Trips/GetAirports`).subscribe(
      response => {
        this.airports = response;
      },
      error => {
        console.log(error);
      }
    );
  }
  addFlight() {
    this.http
      .post(`${environment.baseURL}/api/Trips/AddFlight`, {
        Username: this.auth.decodedToken.unique_name,
        DepartureAirportAcronym: this.model.departureAirport,
        DestinationAirportAcronym: this.model.destinationAirport
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
}
