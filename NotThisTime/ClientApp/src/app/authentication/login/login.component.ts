import { Component, OnInit } from "@angular/core";
import {
  FormControl,
  FormGroup,
  FormBuilder,
  AbstractControlOptions,
  Validators
} from "@angular/forms";

@Component({
  selector: "app-login",
  templateUrl: "login.component.html",
  styleUrls: ["./login.component.scss"]
})
export class LoginComponent implements OnInit {
  loginForm: FormGroup;

  constructor(private fb: FormBuilder) {}

  ngOnInit() {
    this.buildLoginForm();
  }

  buildLoginForm(): void {
    this.loginForm = this.fb.group({
      username: new FormControl("", <AbstractControlOptions>{
        validators: [Validators.required, Validators.minLength(5)]
      }),
      password: new FormControl("", <AbstractControlOptions>{
        validators: [Validators.required, Validators.minLength(5)]
      })
    });
  }

  onSubmit() {}

  resetForm() {
    this.loginForm.reset();
  }
}
