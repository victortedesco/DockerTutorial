import { Component, ElementRef, OnInit, ViewChild } from "@angular/core";
import { HeaderComponent } from "../../shared/header/header.component";
import { FooterComponent } from "../../shared/footer/footer.component";
import { User } from "../../../models/user.model";
import { UserService } from "../../../services/user.service";
import { Router } from "@angular/router";
import { ToastService } from "angular-toastify";

@Component({
  selector: "app-home-page",
  standalone: true,
  imports: [HeaderComponent, FooterComponent],
  templateUrl: "./home-page.component.html",
})
export class HomePageComponent implements OnInit {
  users: User[] = [];

  @ViewChild("searchInput", { static: true })
  searchInputElementRef!: ElementRef;
  searchInputElement!: HTMLInputElement;

  constructor(
    private toastService: ToastService,
    private userService: UserService,
    private router: Router
  ) {}

  ngOnInit(): void {
    this.getUsers();
  }

  isEmpty = (text: string): boolean => {
    return text === null || text.match(/^ *$/) !== null;
  };

  searchUsers = (event: KeyboardEvent): void => {
    const element = event.currentTarget as HTMLInputElement;
    const value = element.value;

    if (event.key !== "Enter") return;

    if (this.isEmpty(value)) {
      this.getUsers();
      return;
    }
    this.userService.searchByName(value).subscribe((response) => {
      if (response == null) {
        this.toastService.warn("There is no user matching this name.");
        return;
      }
      this.users = response;
    });
  };

  getUsers(): void {
    this.userService.getAll().subscribe({
      next: (response) => {
        if (response == null) {
          this.users = [];
          return;
        }
        this.users = response;
      },
      error: () => {
        this.users = [];
      },
    });
  }

  editUser(id: string): void {
    this.router.navigate(["/update", id]);
  }

  deleteUser(id: string): void {
    this.userService.delete(id).subscribe(() => {
      this.toastService.info("User deleted!");
      this.getUsers();
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
