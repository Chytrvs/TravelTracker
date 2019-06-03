import { Component, OnInit } from '@angular/core';
import { AuthService } from 'src/services/auth.service';
import { BsDropdownModule } from 'ngx-bootstrap/dropdown';


@Component({
  selector: 'app-nav',
  templateUrl: './nav.component.html',
  styleUrls: ['./nav.component.css']
})
export class NavComponent implements OnInit {
  model:any={};

  constructor(public authService: AuthService) { }

  ngOnInit() {
    const token=localStorage.getItem('token')
    if(token)
    this.authService.decodeToken(token);
  }
  login(){
    this.authService.login(this.model).subscribe(next=>
      console.log("Logged successfuly"),error=>
      console.log("Failed to login"))
  }

  loggedIn(){
    return this.authService.loggedIn();
  }
  logout(){
    localStorage.removeItem('token');
    console.log('logged out');
  }

}
