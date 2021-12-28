import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-chattest',
  templateUrl: './chattest.component.html',
  styleUrls: ['./chattest.component.scss']
})



export class ChattestComponent implements OnInit {

  typesOfShoes: string[] = ['Boots', 'Clogs', 'Loafers', 'Moccasins', 'Sneakers'];
  selectedShoe: string | undefined;

  constructor() { }

  ngOnInit(): void {
  }

  selectChannel(channel: string) {
    console.log(channel);
    this.selectedShoe = channel;
  }
}
