export class Apartment {

    constructor() {
        this.isRented = false;
    }

    id: string;
    title: string;
    description: string;
    area: number;
    pricePerMonth: number;
    nrOfRooms: number;
    latitude: number;
    longitude: number;
    isRented: boolean;
    realtorId: string;
    realtorName: string;
}
