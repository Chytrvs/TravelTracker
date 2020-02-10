import { Component, OnInit } from "@angular/core";

import * as arcjs from "node_modules/arc/arc.js";
import * as Collections from "typescript-collections";
import * as sphere from "node_modules/ol/sphere.js";

import OlMap from "ol/Map";
import OlOSM from "ol/source/OSM.js";
import OlVectorLayer from "ol/layer/Vector.js";
import OlTileLayer from "ol/layer/Tile.js";
import OlView from "ol/View";
import GeoJSON from "ol/format/GeoJSON.js";
import Vector from "ol/source/Vector.js";
import Style from "ol/style/Style.js";
import Stroke from "ol/style/stroke.js";

import { HttpClient } from "@angular/common/http";
import { AuthService } from "src/services/auth.service";
import { AlertifyService } from "src/services/alertify.service";
import { Flight } from '../interfaces/flight';
import { environment } from 'src/environments/environment';

@Component({
  selector: "app-map",
  templateUrl: "./map.component.html",
  styleUrls: ["./map.component.css"]
})
export class MapComponent implements OnInit {
  map: OlMap;
  source: OlOSM;
  layer: OlVectorLayer;
  tilelayer: OlTileLayer;
  view: OlView;
  format: GeoJSON = new GeoJSON({
    featureProjection: "EPSG:3857"
  });
  vector: Vector = new Vector();
  
  style: Style;
  stroke: Stroke;
  NumberOfFlights: number = 0;
  NumberOfAirportsVisited: number = 0;
  TotalDistanceTraveled: any =0;
  AirportsVisited = new Collections.Set(String);

  constructor(
    private http: HttpClient,
    private auth: AuthService,
    private alertify: AlertifyService
  ) {}

  ngOnInit() {
    this.getFlights();
  }
  getFlights() {
    this.http.get<Flight[]>(`${environment.baseURL}/api/Trips/GetUserFlights/`+this.auth.decodedToken.unique_name)
    .subscribe(
      data => {
        this.alertify.success("Successfully loaded your flights");
        this.CalculateFlightsStatistics(data);
        this.GenerateFlightsCurvedVector(data);
        this.DrawMap();
      },
      error => {
        this.alertify.message(
          "In order to see your flights displayed on a map, add some of them in a first place"
        );
        this.DrawMap();
      }
    );
  }


  CalculateFlightsStatistics(flights: Flight[]){
    this.SetNumberOfFlights(flights);

    flights.forEach(flight=>{
        this.UpdateTotalDistanceTraveled(flight);
        this.UpdateNumberOfVisitedAirports(flight);
    })

    this.NumberOfAirportsVisited = this.AirportsVisited.size();
    this.FormatDistance();
  } 

  SetNumberOfFlights(flights: Flight[]){
    this.NumberOfFlights=Object.keys(flights).length;
  }

  UpdateTotalDistanceTraveled(flight: Flight){
    this.TotalDistanceTraveled += sphere.getDistance(
      [
        flight.DepartureAirport.Longitude,
        flight.DepartureAirport.Latitude],
      [
        flight.DestinationAirport.Longitude,
        flight.DestinationAirport.Latitude
      ]
    );
  }

  UpdateNumberOfVisitedAirports(flight: Flight){
    this.AirportsVisited.add(flight.DepartureAirport.Acronym);
    this.AirportsVisited.add(flight.DestinationAirport.Acronym);
  }

  FormatDistance() {
    if (this.TotalDistanceTraveled >= 1000) 
    {
      this.TotalDistanceTraveled =
        Math.round((this.TotalDistanceTraveled / 1000) * 100) / 100 + " " + "km";
    } 
    else 
    {
      this.TotalDistanceTraveled = Math.round(this.TotalDistanceTraveled) + " " + "m";
    }
  }

  GenerateFlightsCurvedVector(flightsData: Flight[]) {
    flightsData.forEach(Flight => {
      var generator = new arcjs.GreatCircle(
        {
          x: Flight.DepartureAirport.Longitude,
          y: Flight.DepartureAirport.Latitude
        },
        {
          x: Flight.DestinationAirport.Longitude,
          y: Flight.DestinationAirport.Latitude
        }
      );

      var numberOfPoints = 50; 
      var coords = generator.Arc(numberOfPoints).geometries[0].coords;
      var geojson = {
        type: "Feature",
        geometry: { type: "LineString", coordinates: coords },
        properties: null
      };
      this.vector.addFeatures(this.format.readFeatures(geojson));
    });
  }

  DrawMap() {
    this.layer = new OlVectorLayer({
      source: this.vector,
      style: new Style({
        stroke: new Stroke({
          color: "red",
          width: 2
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
      target: "map",
      view: new OlView({
        center: [0, 0],
        zoom: 2.5,
        minZoom: 2,
        maxZoom: 19
      })
    });
  }
}
