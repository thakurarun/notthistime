import { Component, OnInit } from "@angular/core";
import { HttpClient } from "@angular/common/http";

@Component({
  selector: "app-main-nav",
  templateUrl: "./main.component.html",
  styleUrls: ["./main.component.scss"]
})
export class MainComponent implements OnInit {
  constructor(private http: HttpClient) {}

  ngOnInit() {}

  async setupDb() {
    await this.http.get("api/SetupDb/Create").toPromise();
    console.log("Db created");
  }
}
