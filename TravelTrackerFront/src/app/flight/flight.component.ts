import { Component, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';

@Component({
  selector: 'app-flight',
  templateUrl: './flight.component.html',
  styleUrls: ['./flight.component.css']
})
export class FlightComponent implements OnInit {
  airports:any;
  constructor(private http: HttpClient) { }

  ngOnInit() {
    this.getAirports();
  }
  getAirports(){
    this.http.get('http://localhost:5000/api/Trips/GetAirports').subscribe(response=>{
      this.airports=response;
    },error=>{
      console.log(error);
    })
  }
  addFlight(){

  }

}
