import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { SortedResultsService } from '../../core/services/sorted-results.service';
import { SortedResultsDetailsDto } from '../../core/models/sorted-results.models';

@Component({
  selector: 'app-sorted-results',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './sorted-results.component.html',
})
export class SortedResultsComponent implements OnInit {
  results: SortedResultsDetailsDto[] = [];

  inputValues = '';
  isAscending = true;

  constructor(private service: SortedResultsService) {}

  ngOnInit() {
    this.load();
  } 

  load() {
    this.service.getAll().subscribe(r => this.results = r);
  }

  create() {
    const values = this.inputValues
      .split(',')
      .map(v => Number(v.trim()))
      .filter(v => !isNaN(v));

    this.service
      .create({ initialValues: values, isAscending: this.isAscending })
      .subscribe(() => {
        this.inputValues = '';
        this.load();
      });
  }

  delete(id: number) {
    this.service.delete(id).subscribe(() => this.load());
  }
}
