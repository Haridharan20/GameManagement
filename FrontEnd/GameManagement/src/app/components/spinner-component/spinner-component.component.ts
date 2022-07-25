import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-spinner-component',
  template: '<div class="lds-facebook"><div></div><div></div><div></div></div>',
  styleUrls: ['./spinner-component.component.css'],
})
export class SpinnerComponentComponent implements OnInit {
  constructor() {}

  ngOnInit(): void {}
}
