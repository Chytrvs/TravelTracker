import { Component, OnInit } from '@angular/core';

import * as arcjs from 'node_modules/arc/arc.js';

import OlMap from 'ol/Map';
import OlOSM from 'ol/source/OSM.js';
import OlVectorLayer from 'ol/layer/Vector.js';
import OlTileLayer from 'ol/layer/Tile.js';
import OlView from 'ol/View';
import GeoJSON from 'ol/format/GeoJSON.js';
import Vector from 'ol/source/Vector.js';
import Style from 'ol/style/Style.js';
import Stroke from 'ol/style/stroke.js';

import { fromLonLat } from 'ol/proj';
import { HttpClient, HttpParams, HttpHeaders } from '@angular/common/http';
import { modelGroupProvider } from '@angular/forms/src/directives/ng_model_group';
import { map } from 'rxjs/operators';

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

  constructor(private http: HttpClient) { }

  ngOnInit() {
    this.getFlights();
    this.DrawMap(this.GenerateCurves(this.flights));
  }
  getFlights(){
    this.http.post("http://localhost:5000/api/Trips/GetUserFlights",
    {
    "Username":  "logintest",
    })
    .subscribe(
    data  => {
    console.log("POST Request is successful ", data);
    this.flights=data;
    },
    error  => {
    console.log("Error", error);
    }
    );


  }
  GenerateCurves(flightsData: any){
    
    var generator = new arcjs.GreatCircle({x: 50, y: 0},{x: -10, y: 30});
    var n = 50; // n of points
    var coords = generator.Arc(n).geometries[0].coords;
    var geojson = {"type":"Feature","geometry":{"type":"LineString","coordinates": coords},"properties":null };
  
    var sgenerator = new arcjs.GreatCircle({x: 100, y: 100},{x: -150, y: 140});
    var sn = 50; // n of points
    var scoords = sgenerator.Arc(sn).geometries[0].coords;
    var sgeojson = {"type":"Feature","geometry":{"type":"LineString","coordinates": scoords},"properties":null };
     
    this.format=new GeoJSON({
        featureProjection:"EPSG:3857"
      })
      this.vector=new Vector();
      this.vector.addFeatures(this.format.readFeatures(geojson));
      this.vector.addFeatures(this.format.readFeatures(sgeojson));
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
        zoom: 2
      })
    });
  }

}
