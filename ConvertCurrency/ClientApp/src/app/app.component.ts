import { Component, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';

//response object to return just the string
interface ConversionResponse {
  result: string;
}

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html'
})

export class AppComponent {

  title = 'app';
  amount: string = "";
  convertedAmount: any;
  baseUrl: string = 'http://localhost:5002/ConvertCurrency/';
  constructor(private http: HttpClient) { }

  convertToWords() {
    this.http.get<ConversionResponse>(this.baseUrl +'checknumber?amount=' + this.amount)
      .subscribe(
        (response) => {
          this.convertedAmount = response.result;
        },
        (error) => {
          console.error('Error:', error);
        }
      );
  }
}


