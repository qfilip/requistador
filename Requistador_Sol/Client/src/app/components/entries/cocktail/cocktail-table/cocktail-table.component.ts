import { Component, OnInit } from '@angular/core';
import { CocktailController } from 'src/app/controllers/cocktail.controller';
import { IngredientController } from 'src/app/controllers/ingretient.controller';
import { PageLoaderService } from 'src/app/services/page-loader.service';
import { ICocktailDto } from 'src/app/_generated/interfaces';

@Component({
    selector: 'cocktail-table',
    templateUrl: './cocktail-table.component.html',
    styleUrls: ['./cocktail-table.component.scss']
})
export class CocktailTableComponent implements OnInit {

    constructor(
        private pageLoader: PageLoaderService,
        private cocktailController: CocktailController,
        private ingredientController: IngredientController
    ) { }

    cocktails: ICocktailDto[];

    ngOnInit() {
        this.loadCocktails();
    }

    loadCocktails() {
        this.pageLoader.show('Fetching cocktails');
        this.cocktailController.getAll()
        .subscribe(
            result => {
                this.cocktails = result;
                this.pageLoader.hide();
            },
            error => {
                console.log(error);
                this.pageLoader.hide();
            }
        );
    }
}
