import { Routes } from '@angular/router';
import { ROOT_PATHS, SORTED_RESULTS_PATHS } from './core/constants/paths.constants';
import { SortedResultsComponent } from './features/sorted-results/sorted-results.component';
import { HomeComponent } from './features/home/home.component';

export const routes: Routes = [
  { path: ROOT_PATHS.home, component: HomeComponent },
  { path: SORTED_RESULTS_PATHS.base, component: SortedResultsComponent },
];
