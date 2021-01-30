import { Component, OnInit } from '@angular/core';
import { Observable } from 'rxjs';
import { CocktailController } from 'src/app/controllers/cocktail.controller';
import { IngredientController } from 'src/app/controllers/ingretient.controller';
import { PageLoaderService } from 'src/app/services/page-loader.service';
import { ICocktailDto, IIngredientDto } from 'src/app/_generated/interfaces';

@Component({
    selector: 'entry-home',
    templateUrl: './entry-home.component.html',
    styleUrls: ['./entry-home.component.scss']
})
export class EntryHomeComponent {

    constructor(
        private pageLoader: PageLoaderService,
        private cocktailController: CocktailController,
        private ingredientController: IngredientController
    ) { }
    
    currentTable: string = 'None';


    loadCocktails() {
        this.pageLoader.show('Fetching cocktails');
        this.cocktailController.getAll()
        .subscribe(
            result => {
                this.pageLoader.hide();
            },
            error => {
                console.log(error);
                this.pageLoader.hide();
            }
        );
    }

    loadIngredients() {
        this.pageLoader.show('Fetching cocktails');
        this.ingredientController.getAll()
        .subscribe(
            result => {
                this.pageLoader.hide();
            },
            error => {
                console.log(error);
                this.pageLoader.hide();
            }
        );
    }
}
