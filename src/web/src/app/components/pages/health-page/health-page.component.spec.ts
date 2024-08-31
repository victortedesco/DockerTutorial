import { ComponentFixture, TestBed } from "@angular/core/testing";

import { HealthPageComponent } from "./health-page.component";

describe("HealthPageComponent", () => {
  let component: HealthPageComponent;
  let fixture: ComponentFixture<HealthPageComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [HealthPageComponent],
    }).compileComponents();

    fixture = TestBed.createComponent(HealthPageComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it("should create", () => {
    expect(component).toBeTruthy();
  });
});
