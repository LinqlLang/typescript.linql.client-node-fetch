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

  constructor(public context: LinqlContext, public customContext: CustomLinqlContext, public CD: ChangeDetectorRef)
  {

  }

  async ngOnInit()
  {
    const search = this.customContext.Set<State>(State, { this: this });
    const results = await search.ToListAsync();
    this.StateData = results;
    const search2 = this.customContext.Set<State>(State, { this: this });
    const search3 = search2.Where(r => r.State_Name!.ToLower().Contains(this.StateSearch));
    this.StateSearchData = await search3.ToListAsync();

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

