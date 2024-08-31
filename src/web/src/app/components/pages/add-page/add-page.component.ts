import { Component } from "@angular/core";
import {
  FormControl,
  FormGroup,
  ReactiveFormsModule,
  Validators,
} from "@angular/forms";
import { ToastService } from "angular-toastify";
import { UserService } from "../../../services/user.service";
import { HeaderComponent } from "../../shared/header/header.component";
import { FooterComponent } from "../../shared/footer/footer.component";
import { AddUserRequest } from "../../../models/requests/add-user-request";

@Component({
  selector: "app-update-page",
  standalone: true,
  imports: [ReactiveFormsModule, HeaderComponent, FooterComponent],
  templateUrl: "./add-page.component.html",
})
export class AddPageComponent {
  userForm = new FormGroup({
    name: new FormControl("", Validators.required),
    email: new FormControl("", [Validators.required, Validators.email]),
    document: new FormControl("", [
      Validators.required,
      Validators.pattern(
        "^(\\d{2}\\.?\\d{3}\\.?\\d{3}\\/?\\d{4}-?\\d{2}|\\d{3}\\.?\\d{3}\\.?\\d{3}-?\\d{2})$"
      ),
    ]),
  });

  constructor(
    private toastService: ToastService,
    private userService: UserService
  ) {}

  convertToRequest(): AddUserRequest {
    let user: AddUserRequest = {
      name: this.userForm.get("name")?.value?.toString() ?? "",
      email: this.userForm.get("email")?.value?.toString() ?? "",
      document: this.userForm.get("document")?.value?.toString() ?? "",
    };
    return user;
  }

  addUser(): void {
    let request = this.convertToRequest();

    this.userService.add(request).subscribe({
      next: () => {
        this.toastService.success("User added sucessfully!");
      },
      error: (response) => {
        let messages: string[] = response.error;
        messages.forEach((m) => {
          this.toastService.error(m);
        });
      },
    });
  }
}
