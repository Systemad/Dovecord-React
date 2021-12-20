import { Component, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { WeatherForecastClient, WeatherForecast } from '../web-api-client';
import { ChannelClient, ChannelDto } from '../web-api-client';

@Component({
  selector: 'app-chat',
  templateUrl: './chat.component.html',
  styleUrls: ['./chat.component.css']
})
export class ChatComponent implements OnInit {

  public forecasts: WeatherForecast[] = [];
  selectedChannel: WeatherForecast | undefined;

  constructor(private service: WeatherForecastClient) {
    this.service.get().subscribe(result => {
      this.forecasts = result;
      console.log(this.forecasts);
    }, error => console.error(error));
  }

  ngOnInit() {
  }

  selectChannel(weatherForecast: WeatherForecast) { this.selectChannel = weatherForecast.toJSON }
}
