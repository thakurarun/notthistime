import { Component, OnInit } from "@angular/core";
import {
  FormGroup,
  FormBuilder,
  AbstractControlOptions,
  FormControl,
  Validators
} from "@angular/forms";
import { RegisterModel } from "../model";
import { RegisterService } from "../register.service";

@Component({
  selector: "app-register",
  templateUrl: "./register.component.html",
  styleUrls: ["./register.component.scss"]
})
export class RegisterComponent implements OnInit {
  registerForm: FormGroup;

  constructor(
    private fb: FormBuilder,
    private registerService: RegisterService
  ) {}

  ngOnInit() {
    this.buildLoginForm();
  }

  buildLoginForm(): void {
    this.registerForm = this.fb.group({
      email: new FormControl("", <AbstractControlOptions>{
        validators: [Validators.required, Validators.email]
      }),
      fullName: new FormControl("", <AbstractControlOptions>{
        validators: [Validators.required, Validators.minLength(3)]
      }),
      username: new FormControl("", <AbstractControlOptions>{
        validators: [Validators.required, Validators.minLength(5)]
      }),
      password: new FormControl("", <AbstractControlOptions>{
        validators: [Validators.required, Validators.minLength(5)]
      }),
      confirmPassword: new FormControl("", <AbstractControlOptions>{
        validators: [Validators.required]
      })
    });
  }

  onSubmit() {
    this.registerService
      .registerUser(this.registerForm.value as RegisterModel)
      .then(res => {
        console.log("registred");
      });
  }

  resetForm() {
    this.registerForm.reset();
  }
}
