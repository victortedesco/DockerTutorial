import { Component, Input } from "@angular/core";
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
import { UpdateUserRequest } from "../../../models/requests/update-user-request";
import { User } from "../../../models/user.model";
import { Router } from "@angular/router";

@Component({
  selector: "app-update-page",
  standalone: true,
  imports: [ReactiveFormsModule, HeaderComponent, FooterComponent],
  templateUrl: "./update-page.component.html",
})
export class UpdatePageComponent {
  user?: User;

  @Input()
  set id(id: string) {
    if (id == null) this.router.navigate([""]);

    this.getUser(id);
  }

  constructor(
    private router: Router,
    private toastService: ToastService,
    private userService: UserService
  ) {}

  userForm = new FormGroup({
    id: new FormControl(this.id),
    name: new FormControl(this.user?.name, Validators.required),
    email: new FormControl(this.user?.email, [
      Validators.required,
      Validators.email,
    ]),
    document: new FormControl(this.user?.document),
  });

  getUser(id: string): void {
    this.userService.getById(id).subscribe({
      next: (response) => {
        this.user = response;
        this.userForm.patchValue(this.user);
      },
      error: () => {
        this.router.navigate([""]);
      },
    });
  }

  resetUser(): void {
    this.userForm.reset();
    this.userForm.patchValue({
      id: this.user?.id,
      document: this.formatDocument(this.user!.document),
    });
  }

  convertToRequest(): UpdateUserRequest {
    let user: UpdateUserRequest = {
      name: this.userForm.get("name")?.value?.toString() ?? "",
      email: this.userForm.get("email")?.value?.toString() ?? "",
    };
    return user;
  }

  updateUser(): void {
    let request = this.convertToRequest();

    this.userService.update(this.user!.id!, request).subscribe({
      next: () => {
        this.toastService.success("User updated sucessfully!");
      },
      error: (response) => {
        let messages: string[] = response.error;
        messages.forEach((m) => {
          this.toastService.error(m);
        });
      },
    });
  }

  formatDocument(value: string): string {
    const cleanedValue = value.replace(/\D/g, "");

    if (cleanedValue.length === 11) {
      return cleanedValue.replace(
        /(\d{3})(\d{3})(\d{3})(\d{2})/,
        "$1.$2.$3-$4"
      );
    }
    if (cleanedValue.length === 14) {
      return cleanedValue.replace(
        /(\d{2})(\d{3})(\d{3})(\d{4})(\d{2})/,
        "$1.$2.$3/$4-$5"
      );
    }
    return value;
  }
}
