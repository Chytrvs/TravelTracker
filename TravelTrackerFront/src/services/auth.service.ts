import { Injectable } from '@angular/core';
import { HttpClient,HttpHeaders } from '@angular/common/http';
import {map} from 'rxjs/operators'
import { JwtHelperService } from '@auth0/angular-jwt'

const helper = new JwtHelperService();

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  baseUrl='http://localhost:5000/api/Authentication/';
  private options = { headers: new HttpHeaders().set('Content-Type', 'application/json') };
  decodedToken: any;
  
constructor(private http: HttpClient) { }
login(model: any){
  return this.http.post(this.baseUrl+"LoginUser",model,this.options).pipe(map((response:any)=>{
    const user=response;
    if(user){
      localStorage.setItem('token' , user.token);
      this.decodeToken(user.token);
      }
    })
  );
}
register(model: any){
  return this.http.post(this.baseUrl+"RegisterUser",model);
}
loggedIn(){
  const token = localStorage.getItem('token');
  return !helper.isTokenExpired(token);
}
decodeToken(token: any){
  this.decodedToken=helper.decodeToken(token);
}
}