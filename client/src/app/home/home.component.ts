import { Component, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css'],
})
export class HomeComponent implements OnInit {
  users: any[] = [];
  constructor(private http: HttpClient) {}

  ngOnInit() {
    // this.http.get('https://localhost:7018/api/Users').subscribe(
    //   (res) => {
    //     console.log(res);
    //   },
    //   (err) => {
    //     console.log(err);
    //   }
    // );
    this.getUsers();
  }

  getUsers() {
    this.http.get('https://localhost:7018/api/Users').subscribe({
      next: (response) => console.log(response),
      error: (error) => console.log(error),
    });
  }
}
