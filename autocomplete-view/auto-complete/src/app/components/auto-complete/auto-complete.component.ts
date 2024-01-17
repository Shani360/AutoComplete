import { Component } from '@angular/core';
import { CitiesService } from '../../services/cities.service';
import { City } from '../../interfaces/city';
import { FormControl } from '@angular/forms';
import { debounceTime, filter, switchMap } from 'rxjs/operators';


@Component({
  selector: 'app-auto-complete',
  templateUrl: './auto-complete.component.html',
  styleUrl: './auto-complete.component.scss',
})
export class AutoCompleteComponent {

  public cities : City[] = [];
  public cityInput = new FormControl();
  public isShowOptions : boolean = false;
  private focusOutTimeout: NodeJS.Timeout | undefined;

  constructor(public citiesService : CitiesService) {
    this.setupSearch();
  }

  ngOnInit() : void {
  }

  private setupSearch() {
      try {
          this.cityInput.valueChanges
          .pipe(
            debounceTime(300),
            filter(searchQuery => searchQuery && searchQuery.trim() !== ''), 
            switchMap(searchQuery => this.citiesService.searchCities(searchQuery))
          )
          .subscribe((cities: City[]) => {
            this.cities = cities;
            this.resetSubmit();
          }, (error : any) => { this.resetSubmit();});
      }    
      
      catch(err : any) {
        this.resetSubmit();
      }
  }

  resetSubmit() : void {
    this.citiesService.isSubmittimg = false;
  }

  onInputBlur() {
    this.clearFocousOutTimeout();

    this.focusOutTimeout = setTimeout(() => {
      this.isShowOptions = false;
    }, 200);
  }

  
  updateSrearchValue(cityName: string): void {
    this.cityInput.setValue(cityName);
  }

  clearFocousOutTimeout() {
    if(this.focusOutTimeout) {
      clearTimeout(this.focusOutTimeout);
    }
  }

  ngOnDestroy() {
    this.clearFocousOutTimeout();
  }

}
