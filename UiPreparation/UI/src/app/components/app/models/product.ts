export class Product {
    id: number;
    name: string;
    unitPrice: number;
    unitsInStock: number;
    colors: { colorName: string }[];
    constructor(id: number, name: string, unitPrice: number, unitsInStock: number, colors: any ) {
        this.id = id;
        this.name = name;
        this.unitPrice = unitPrice;
        this.unitsInStock = unitsInStock;
        this.colors = colors;
    }
}
