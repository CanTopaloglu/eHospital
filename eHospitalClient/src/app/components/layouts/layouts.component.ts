import { Component } from '@angular/core';
import { Router, RouterLink, RouterOutlet } from '@angular/router';

@Component({
  selector: 'app-layouts',
  standalone: true,
  imports: [RouterOutlet, RouterLink],
  templateUrl: './layouts.component.html',
})
export class LayoutsComponent {
  constructor(
    private router: Router
  ){}

  logout(){
    localStorage.clear();
    this.router.navigateByUrl("/login");
  }
}
