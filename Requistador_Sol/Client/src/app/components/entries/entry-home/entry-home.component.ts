import { Component } from '@angular/core';
import { eTableType } from 'src/app/models/enums/eTableType';

@Component({
    selector: 'entry-home',
    templateUrl: './entry-home.component.html',
    styleUrls: ['./entry-home.component.scss']
})
export class EntryHomeComponent {

    constructor() { }

    tableType = eTableType;
    currentTable: string = 'None';
    currentTableType = eTableType.None;


    loadCocktails() {
        this.currentTableType = eTableType.Cocktail;
    }

    loadIngredients() {
        this.currentTableType = eTableType.Ingredient;
    }
}
