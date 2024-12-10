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

    // Método para exibir informações do carro
    displayInfo(): void {
        console.log(`Car: ${this.year} ${this.make} ${this.model}`);
    }

    // Método para calcular a idade do carro
    getCarAge(currentYear: number): number {
        return currentYear - this.year;
    }
}

// Exemplo de uso da classe
const myCar = new Car('Toyota', 'Corolla', 2020);
myCar.displayInfo();
console.log(`Car age: ${myCar.getCarAge(2024)} years`);
console.lo
