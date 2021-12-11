import { Component, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { WeatherForecastClient, WeatherForecast } from '../web-api-client';
import { ChannelClient, ChannelDto } from '../web-api-client';
@Component({
  selector: 'app-fetch-data',
  templateUrl: './fetch-data.component.html'
})
export class FetchDataComponent {
  public forecasts: WeatherForecast[] = [];

  constructor(client: WeatherForecastClient) {
    client.get().subscribe(result => {
      this.forecasts = result;
      console.log(this.forecasts);
    }, error => console.error(error));
  }
}
