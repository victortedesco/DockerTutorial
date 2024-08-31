import { ComponentFixture, TestBed } from "@angular/core/testing";

import { AddPageComponent } from "./add-page.component";
import { provideHttpClientTesting } from "@angular/common/http/testing";
import { appConfig } from "../../../app.config";

describe("AddPageComponent", () => {
  let component: AddPageComponent;
  let fixture: ComponentFixture<AddPageComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [AddPageComponent],
      providers: [provideHttpClientTesting(), ...appConfig.providers]
    }).compileComponents();

    fixture = TestBed.createComponent(AddPageComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it("should create", () => {
    expect(component).toBeTruthy();
  });
});
