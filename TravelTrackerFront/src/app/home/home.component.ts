import { Component, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { error } from '@angular/compiler/src/util';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent implements OnInit {
  registerMode=false;
  airports:any;
  constructor(private http: HttpClient) { }

  ngOnInit() {
    this.getAirports();
  }
  registerToggle(){
    this.registerMode=!this.registerMode;
  }
  getAirports(){
    this.http.get('http://localhost:5000/api/Trips/GetAirports').subscribe(response=>{
      this.airports=response;
    },error=>{
      console.log(error);
    })
  }

}
