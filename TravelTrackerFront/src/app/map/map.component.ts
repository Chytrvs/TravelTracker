import { Component, OnInit } from '@angular/core';

import * as arcjs from 'node_modules/arc/arc.js';
import * as Collections from 'typescript-collections';
import * as sphere from 'node_modules/ol/sphere.js';

import OlMap from 'ol/Map';
import OlOSM from 'ol/source/OSM.js';
import OlVectorLayer from 'ol/layer/Vector.js';
import OlTileLayer from 'ol/layer/Tile.js';
import OlView from 'ol/View';
import GeoJSON from 'ol/format/GeoJSON.js';
import Vector from 'ol/source/Vector.js';
import Style from 'ol/style/Style.js';
import Stroke from 'ol/style/stroke.js';


import { HttpClient, HttpParams, HttpHeaders } from '@angular/common/http';
import { AuthService } from 'src/services/auth.service';
import { AlertifyService } from 'src/services/alertify.service';

@Component({
  selector: 'app-map',
  templateUrl: './map.component.html',
  styleUrls: ['./map.component.css']
})

export class MapComponent implements OnInit {
  map: OlMap;
  source: OlOSM;
  layer: OlVectorLayer;
  tilelayer: OlTileLayer;
  view: OlView;
  format: GeoJSON;
  vector: Vector;
  style: Style;
  stroke: Stroke;

 


  private options = { headers: new HttpHeaders().set('Content-Type', 'application/json') };
  flights: any;
  NumberOfFlights: number;
  NumberOfAirportsVisited:number;
  DistanceTraveled:number;

  constructor(private http: HttpClient,private auth: AuthService,private alertify: AlertifyService) { }

  ngOnInit() {
    this.NumberOfFlights=0;
    this.NumberOfAirportsVisited=0;
    this.DistanceTraveled=0;
    this.getFlights();
    
  }
  getFlights(){
    this.http.post("http://localhost:5000/api/Trips/GetUserFlights",
    {
    "Username":  this.auth.decodedToken.unique_name,
    })
    .subscribe(
    data  => {
      this.alertify.success("Successfully loaded your flights");
    this.DrawMap(this.GenerateCurves(data));
    },
    error  => {
    this.alertify.message("In order to see your flights displayed on a map, add some of them in a first place");
    this.DrawMap(new Vector());
    }
    );


  }
  GenerateCurves(flightsData: any){
    var distance=sphere.getDistance([0,0],[180,0]);
    console.log(distance);
    var AirportsVisited=new Collections.Set(String);
    this.vector=new Vector();
    this.format=new GeoJSON({
      featureProjection:"EPSG:3857"
    })
    flightsData.forEach(Flight => {

      AirportsVisited.add(Flight.departureAirport.acronym);
      AirportsVisited.add(Flight.destinationAirport.acronym);
      this.NumberOfFlights++;
      this.DistanceTraveled+=sphere.getDistance([Flight.departureAirport.longitude,Flight.departureAirport.latitude],[Flight.destinationAirport.longitude,Flight.destinationAirport.latitude]);
      var generator = new arcjs.GreatCircle({x: Flight.departureAirport.longitude, y: Flight.departureAirport.latitude},{x: Flight.destinationAirport.longitude, y: Flight.destinationAirport.latitude});
      var n = 50; // n of points
      var coords = generator.Arc(n).geometries[0].coords;
      var geojson = {"type":"Feature","geometry":{"type":"LineString","coordinates": coords},"properties":null };
      this.vector.addFeatures(this.format.readFeatures(geojson));
    });
    this.NumberOfAirportsVisited=AirportsVisited.size();
    return this.vector; 
  }
  DrawMap(vector: Vector){
    this.layer = new OlVectorLayer({
      source: this.vector,
      style: new Style({
        stroke: new Stroke({
          color: 'red',
          width: 3
        })
      })
    });
    this.map = new OlMap({
      layers: [
        new OlTileLayer({
          source: new OlOSM()
        }),
        this.layer
      ],
      target: 'map',
      view: new OlView({
        center: [0,0],
        zoom: 2.5,
        minZoom:2,
        maxZoom:19
      })
    });
  }

}
