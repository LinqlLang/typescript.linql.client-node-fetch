import { ChangeDetectorRef, Component, OnInit } from '@angular/core';
import { LinqlContext } from 'linql.client-angular';
import { CustomLinqlContext } from './CustomLinqlContext';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent implements OnInit
{
  title = 'nglinqlexample';

  StateData: Array<State> = new Array<State>();

  StateSearchData: Array<State> = new Array<State>();

  StateSearch: string = "en";

  BatchData: Array<any> | undefined;

  constructor(public context: LinqlContext, public customContext: CustomLinqlContext, public CD: ChangeDetectorRef)
  {

  }

  async ngOnInit()
  {
    const search = this.customContext.Set<State>(State, { this: this });

    const searches = [
      search.ToListAsyncSearch(),
      search.Where(r => r.State_Code!.Contains("A")).ToListAsyncSearch(),
      search.Where(r => r.State_Name!.ToLower().Contains(this.StateSearch)),
      search.FirstOrDefaultSearch()
    ];
    const serialStart = performance.now()


    const results = await search.ToListAsync();
    this.StateData = results;

    const search3 = search.Where(r => r.State_Name!.ToLower().Contains(this.StateSearch));
    this.StateSearchData = await search3.ToListAsync();

    this.StateData = await search.Where(r => r.State_Code!.Contains("A")).ToListAsync();

    const serialEnd = performance.now()

    console.log(`Time it takes to run sequentially: ${ serialEnd - serialStart } ms`)


    const batchStart = performance.now()

    this.BatchData = await this.customContext.Batch(searches);

    const batchEnd = performance.now()

    console.log(`Time it takes to run in batch: ${ batchEnd - batchStart } ms`)

    this.CD.markForCheck();
  }
}


export class State
{
  FID!: number;
  Program!: string | undefined;
  State_Code: string | undefined;
  State_Name: string | undefined;
  Flowing_St: string | undefined;
  FID_1!: number;
  Data: Array<StateData> | undefined;
}

export class StateData
{
  Year!: number;
  Value!: number;
  Variable!: string;
  DateOfRecording!: Date;
}
