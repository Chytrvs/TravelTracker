import { Component, OnInit, Input, Output, EventEmitter } from '@angular/core';
import { AuthService } from 'src/services/auth.service';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent implements OnInit {
  @Input() airportsFromHome: any;
  @Output() cancelRegister = new EventEmitter();
  model: any={};

  constructor(private authService: AuthService) { }

  ngOnInit() {
  }

  register(){
    this.authService.register(this.model).subscribe(result=>console.log("registerSuccessfuly"),error=>console.log(error));
    console.log(this.model);
  }
  cancel(){
    this.cancelRegister.emit();
    console.log("cancelled");
  }

}
