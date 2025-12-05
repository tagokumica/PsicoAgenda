import {AddressViewModel} from "../../viewmodels/AddressViewModel";

export default function AddressDetails(props: AddressViewModel) {
    const {street, number, complement, city, zipCode, state} = props;

    return (
        <>
            <div className="row">
                <div className="col">
                    <h3>Detalhes do Endereço</h3>
                    <p><span>Detalhamento das informações do Endereço do Usuário</span>
                    </p>
                </div>
            </div>
            <div className="row mt-5">
                <div className="col">
                    <div className="card">
                        <div className="card-body">
                            <div className="row">
                                <div className="col">
                                    <h4>Logradouro</h4>
                                    <small className="fs-5"><span></span></small>{street}
                                </div>
                            </div>
                            <div className="row">
                                <div className="col">
                                    <h4>Número</h4>
                                    <small className="fs-5"><span></span></small>{number}
                                </div>
                            </div>
                            <div className="row">
                                <div className="col">
                                    <h4>Complemento</h4>
                                    <small className="fs-5"><span></span></small>{complement}
                                </div>
                            </div>
                            <div className="row">
                                <div className="col">
                                    <h4>Cidade</h4><small className="fs-5"></small>{city}
                                </div>
                            </div>
                            <div className="row">
                                <div className="col">
                                    <h4>CEP</h4><small className="fs-5"></small>{state}
                                </div>
                            </div>
                            <div className="row">
                                <div className="col">
                                    <h4>UF</h4><small className="fs-5"></small>{zipCode}
                                </div>
                            </div>
                            <div className="row">
                                <div className="col">
                                    <button className="btn btn-primary" type="button">Atualizar</button>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </>
    );
}
