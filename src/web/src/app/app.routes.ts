import { Routes } from "@angular/router";
import { HomePageComponent } from "./components/pages/home-page/home-page.component";
import { AddPageComponent } from "./components/pages/add-page/add-page.component";
import { HealthPageComponent } from "./components/pages/health-page/health-page.component";
import { UpdatePageComponent } from "./components/pages/update-page/update-page.component";

export const routes: Routes = [
  { path: "", component: HomePageComponent },
  { path: "add", component: AddPageComponent },
  { path: "update/:id", component: UpdatePageComponent },
  { path: "health", component: HealthPageComponent },
];
