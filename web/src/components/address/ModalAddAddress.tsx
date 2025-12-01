import { useEffect, useState } from "react";


type Props = {
    show: boolean;
    initialData?: AddressViewModel;
    onClose: () => void;
    onSave: (vm: AddressViewModel) => void;
};

export default function AddressModal({ show, initialData, onClose, onSave }: Props) {
    const [model, setModel] = useState<AddressViewModel>(AddressViewModel.empty());
    const [info, setInfo] = useState("");

    useEffect(() => {
        if (initialData) {
            setModel(initialData);
        } else {
            setModel(AddressViewModel.empty());
        }
    }, [initialData]);

    async function handleCepChange(value: string) {
        setModel(model.with({ zipCode: value }));

        const numeric = value.replace(/\D/g, "");

        if (numeric.length === 8) {
            const res = await fetch(`https://viacep.com.br/ws/${numeric}/json/`);
            const data = await res.json();

            if (!data.erro) {
                const updated = model.with({
                    street: data.logradouro ?? "",
                    city: data.localidade ?? "",
                    state: data.uf ?? "",
                });

                setModel(updated);
                setInfo("Endereço encontrado.");
            } else {
                setInfo("CEP inválido.");
            }
        }
    }

    function handleSave() {
        onSave(model);
    }

    if (!show) return null;
    return (
        <div className="modal fade show d-block" tabIndex={-1} style={{ background: "rgba(0,0,0,.4)" }}>
            <div className="modal-dialog modal-lg">
                <div className="modal-content">

                    {/* HEADER */}
                    <div className="modal-header bg-primary text-white">
                        <h4 className="modal-title">
                            {initialData ? "Atualizar Endereço" : "Adicionar Endereço"}
                        </h4>
                        <button className="btn-close btn-close-white" onClick={onClose}></button>
                    </div>

                    {/* BODY */}
                    <div className="modal-body">
                        <form>
                            <div className="row mb-3">
                                <div className="col">
                                    <label className="form-label">CEP</label>
                                    <input
                                        type="text"
                                        className="form-control"
                                        value={model.zipCode}
                                        onChange={(e) => handleCepChange(e.target.value)}
                                    />
                                </div>

                                <div className="col">
                                    <label className="form-label">Logradouro</label>
                                    <input
                                        type="text"
                                        className="form-control"
                                        value={model.street}
                                        readOnly
                                    />
                                </div>
                            </div>

                            <div className="row mb-3">
                                <div className="col">
                                    <label className="form-label">Número</label>
                                    <input
                                        type="number"
                                        className="form-control"
                                        value={model.number}
                                        onChange={(e) =>
                                            setModel(model.with({ number: Number(e.target.value) }))
                                        }
                                    />
                                </div>

                                <div className="col">
                                    <label className="form-label">Complemento</label>
                                    <input
                                        type="text"
                                        className="form-control"
                                        value={model.complement}
                                        onChange={(e) =>
                                            setModel(model.with({ complement: e.target.value }))
                                        }
                                    />
                                </div>
                            </div>

                            <div className="row mb-3">
                                <div className="col">
                                    <label className="form-label">Cidade</label>
                                    <input type="text" className="form-control" value={model.city} readOnly />
                                </div>

                                <div className="col">
                                    <label className="form-label">Estado</label>
                                    <input type="text" className="form-control" value={model.state} readOnly />
                                </div>
                            </div>

                            {info && <span className="text-secondary">{info}</span>}
                        </form>
                    </div>

                    {/* FOOTER */}
                    <div className="modal-footer">
                        <button className="btn btn-light" type="button" onClick={onClose}>
                            Fechar
                        </button>

                        <button className="btn btn-primary" type="button" onClick={handleSave}>
                            {initialData ? "Salvar Alterações" : "Adicionar"}
                        </button>
                    </div>

                </div>
            </div>
        </div>
    );
}