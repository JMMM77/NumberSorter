export interface SortedResultsDetailsDto {
  id: number;
  sortedValues: number[];
  initialValues: number[];
  sortTime: string;
  isAscending: boolean;
}

export interface SortedResultsCreateDto {
  initialValues: number[];
  isAscending: boolean;
}