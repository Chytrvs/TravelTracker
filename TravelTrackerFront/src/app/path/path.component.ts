import { Component, OnInit } from '@angular/core';
import {HttpClient} from '@angular/common/http';

@Component({
  selector: 'app-path',
  templateUrl: './path.component.html',
  styleUrls: ['./path.component.css']
})
export class PathComponent implements OnInit {
  points: any;
  constructor(private http: HttpClient) { }

  ngOnInit() {
    this.GetPoints();
  }
  GetPoints() {
    this.http.get('http://localhost:5000/api/values').subscribe(res => {this.points = res; }, err => { console.log(err); } );
  }
}
