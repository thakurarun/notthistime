import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-list',
  template: `
    <p>
      list works!
    </p>
  `,
  styleUrls: ['./list.component.scss']
})
export class ListComponent implements OnInit {

  constructor() { }

  ngOnInit() {
  }

}
