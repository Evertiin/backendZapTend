class Car {
    // Propriedades da classe
    make: string;
    model: string;
    year: number;

    // Construtor para inicializar a classe
    constructor(make: string, model: string, year: number) {
        this.make = make;
        this.model = model;
        this.year = year;
    }

    // M�todo para exibir informa��es do carro
    displayInfo(): void {
        console.log(`Car: ${this.year} ${this.make} ${this.model}`);
    }

    // M�todo para calcular a idade do carro
    getCarAge(currentYear: number): number {
        return currentYear - this.year;
    }
}

// Exemplo de uso da classe
const myCar = new Car('Toyota', 'Corolla', 2020);
myCar.displayInfo();
console.log(`Car age: ${myCar.getCarAge(2024)} years`);
console.lo
