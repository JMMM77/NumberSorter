import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { RouterLink, RouterLinkActive } from '@angular/router';
import { SORTED_RESULTS_URLS } from '../../core/constants/urls.constants';

@Component({
  selector: 'app-home',
  imports: [CommonModule, FormsModule, RouterLink, RouterLinkActive],
  templateUrl: './home.component.html',
})
export class HomeComponent {
  readonly SORTED_RESULTS_URLS = SORTED_RESULTS_URLS;
}
