import { Component, OnInit } from '@angular/core';
import { AuthService } from 'src/services/auth.service';
import { BsDropdownModule } from 'ngx-bootstrap/dropdown';
import { Router } from '@angular/router';
import { AlertifyService } from 'src/services/alertify.service';


@Component({
  selector: 'app-nav',
  templateUrl: './nav.component.html',
  styleUrls: ['./nav.component.css']
})
export class NavComponent implements OnInit {
  model:any={};

  constructor(public authService: AuthService,private router: Router,private alertify:AlertifyService) { }

  ngOnInit() {
    const token=localStorage.getItem('token')
    if(token)
    this.authService.decodeToken(token);
  }
  login(){
    this.authService.login(this.model).subscribe(next=>
      this.alertify.success("Logged in succesfully"),error=>
      this.alertify.error("Failed to login"))
  }

  loggedIn(){
    return this.authService.loggedIn();
  }
  logout(){
    localStorage.removeItem('token');
    this.router.navigate(['/home']);
  }

}
