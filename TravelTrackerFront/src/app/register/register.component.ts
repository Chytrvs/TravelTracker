import { Component, OnInit, Input, Output, EventEmitter } from "@angular/core";
import { AuthService } from "src/services/auth.service";
import { AlertifyService } from 'src/services/alertify.service';

@Component({
  selector: "app-register",
  templateUrl: "./register.component.html",
  styleUrls: ["./register.component.css"]
})
export class RegisterComponent implements OnInit {
  @Input() airportsFromHome: any;
  @Output() cancelRegister = new EventEmitter();
  model: any = {};

  constructor(private authService: AuthService,private alertify:AlertifyService) {}

  ngOnInit() {}

  register() {
    this.authService
      .register(this.model)
      .subscribe(
        result => this.alertify.success("Registered successfuly"),
        error => this.alertify.error(error)
      );
  }
  cancel() {
    this.cancelRegister.emit();
    console.log("cancelled");
  }
}
