import { Component, OnInit } from "@angular/core";
import { HttpClient } from "@angular/common/http";
import { AuthService } from 'src/services/auth.service';

@Component({
  selector: "app-flight",
  templateUrl: "./flight.component.html",
  styleUrls: ["./flight.component.css"]
})
export class FlightComponent implements OnInit {
  airports: any;
  model: any={};
  constructor(private http: HttpClient,private auth: AuthService) {}

  ngOnInit() {
    this.getAirports();
  }
  getAirports() {
    this.http.get("http://localhost:5000/api/Trips/GetAirports").subscribe(
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
      .post("http://localhost:5000/api/Trips/AddFlight", {
        Username: this.auth.decodedToken.unique_name,
        DepartureAirportAcronym: this.model.departureAirport,
        DestinationAirportAcronym: this.model.destinationAirport
      })
      .subscribe(
        data => {
          console.log(data);
        },
        error => {
          console.log("Error", error);
        }
      );
  }
}
