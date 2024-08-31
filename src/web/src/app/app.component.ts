import { Component } from "@angular/core";
import { RouterOutlet } from "@angular/router";
import { AngularToastifyModule, ToastService } from "angular-toastify";

@Component({
  selector: "app-root",
  standalone: true,
  imports: [AngularToastifyModule, RouterOutlet],
  providers: [ToastService],
  templateUrl: "./app.component.html",
})
export class AppComponent {
  title = "Users | Web";
}
