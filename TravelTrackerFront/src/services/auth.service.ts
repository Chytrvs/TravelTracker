import { Injectable } from '@angular/core';
import { HttpClient,HttpHeaders } from '@angular/common/http';
import {map} from 'rxjs/operators'
import { JwtHelperService } from '@auth0/angular-jwt'
import { Router } from '@angular/router';
import { environment } from 'src/environments/environment';

const helper = new JwtHelperService();

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  baseUrl=`${environment.baseURL}/api/Authentication/`;
  private options = { headers: new HttpHeaders().set('Content-Type', 'application/json') };
  decodedToken: any;
  
constructor(private http: HttpClient,private router: Router) { }
login(model: any){
  return this.http.post(this.baseUrl+"LoginUser",model,this.options).pipe(map((response:any)=>{
    const user=response;
    if(user){
      localStorage.setItem('token' , user.token);
      this.decodeToken(user.token);
      this.router.navigate(['/map']);
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
getToken(){
  const token = localStorage.getItem('token');
  return token;
}
}