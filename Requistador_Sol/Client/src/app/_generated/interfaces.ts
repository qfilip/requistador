//     This code was generated by a Reinforced.Typings tool. 
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.

export interface IClientRequestDto
{
}
export interface ICocktailDto
{
	name: string;
	excerpts: IExcerptDto[];
	ingredients: IIngredientDto[];
}
export interface IExcerptDto
{
	cocktailId: string;
	ingredientId: string;
	amount: number;
}
export interface IIngredientDto
{
	name: string;
	strength: number;
	excerpts: IExcerptDto[];
}