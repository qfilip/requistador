import { Component, OnInit } from '@angular/core';
import { IngredientController } from 'src/app/controllers/ingretient.controller';
import { IIngredientDto } from 'src/app/_generated/interfaces';

@Component({
    selector: 'ingredient-table',
    templateUrl: './ingredient-table.component.html',
    styleUrls: ['./ingredient-table.component.scss']
})
export class IngredientTableComponent implements OnInit {

    constructor(
        private controller: IngredientController 
    ) { }

    ngOnInit(): void {
    }

    async postTest() {
        let vodka = {
            name: 'Wine',
            strength: 40
        } as IIngredientDto;

        let rum = {
            name: 'Beer',
            strength: 40
        } as IIngredientDto;

        let r1 = await this.controller.create(vodka).toPromise();
        let r2 = await this.controller.create(rum).toPromise();

        console.table([r1, r2]);
    }

}
