class AddressViewModel {
    id: string = ""
    zipCode: string = "";
    street: string = "";
    number: number = 0;
    complement: string = "";
    city: string = "";
    state: string = "";

    static empty(): AddressViewModel {
        return new AddressViewModel();
    }

    with(values: Partial<AddressViewModel>): AddressViewModel {
        return Object.assign(new AddressViewModel(), this, values);
    }
}

