import { ComponentFixture, TestBed } from "@angular/core/testing";

import { UpdatePageComponent } from "./update-page.component";
import { provideHttpClientTesting } from "@angular/common/http/testing";
import { appConfig } from "../../../app.config";

describe("UpdatePageComponent", () => {
  let component: UpdatePageComponent;
  let fixture: ComponentFixture<UpdatePageComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [UpdatePageComponent],
      providers: [provideHttpClientTesting(), ...appConfig.providers]
    }).compileComponents();

    fixture = TestBed.createComponent(UpdatePageComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it("should create", () => {
    expect(component).toBeTruthy();
  });
});
