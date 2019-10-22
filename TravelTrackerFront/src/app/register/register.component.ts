import { Component, OnInit, Input, Output, EventEmitter } from "@angular/core";
import { AuthService } from "src/services/auth.service";
import { AlertifyService } from "src/services/alertify.service";
import { FormGroup, FormControl, Validators } from "@angular/forms";
import { User } from "../interfaces/user";

@Component({
  selector: "app-register",
  templateUrl: "./register.component.html",
  styleUrls: ["./register.component.css"]
})
export class RegisterComponent implements OnInit {
  @Input() airportsFromHome: any;
  @Output() cancelRegister = new EventEmitter();
  registerForm: FormGroup;
  user: User;

  constructor(
    private authService: AuthService,
    private alertify: AlertifyService
  ) {}

  ngOnInit() {
    this.generateForm();
  }

  generateForm() {
    this.registerForm = new FormGroup({
      username: new FormControl("", [
        Validators.required,
        Validators.minLength(2),
        Validators.maxLength(10)
      ]),
      email: new FormControl("", [
         Validators.required,
         Validators.email
      ]),
      password: new FormControl("", [
        Validators.required,
        Validators.minLength(6),
        Validators.maxLength(64)
      ]),
      favouriteAirport: new FormControl("", [
        Validators.required
      ])
    });
  }
  register() {
   if (this.registerForm.valid) {
      this.user = Object.assign({}, this.registerForm.value);
        this.authService.register(this.user).subscribe(
        result => {
          this.alertify.success("Registered successfuly");
        },
        err => {
          this.alertify.error(err.error.title);
        },
        () => {
          this.authService.login({
            username: this.user.Username,
            password: this.user.Password
          });
        }
      );
    }
    else{
      this.alertify.error("Registration failed")
    }
  }
  cancel() {
    this.cancelRegister.emit();
  }
}
