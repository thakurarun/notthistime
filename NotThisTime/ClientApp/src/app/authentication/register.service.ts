import { Injectable } from "@angular/core";
import { HttpClient } from "@angular/common/http";
import { RegisterModel } from "./model";

@Injectable({
  providedIn: "root"
})
export class RegisterService {
  constructor(private http: HttpClient) {}

  async registerUser(registerModel: RegisterModel): Promise<void> {
    const response = await this.http
      .post("/api/register", registerModel)
      .toPromise();
    console.log("registerd");
  }
}
