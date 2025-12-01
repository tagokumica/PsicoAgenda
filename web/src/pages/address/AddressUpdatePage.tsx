import { useState } from "react";
import AddressModal from "../../components/address/ModalUpdateAddress";


export default function AddressesPage({ modal }: any) {
    const [show, setShow] = useState(false);

    function handleSave(vm: AddressViewModel) {
        setShow(false);
    }

    return (
        <>
            <button className="btn btn-primary" onClick={() => setShow(true)}>
                Adicionar Endereço
            </button>

            <AddressModal
                show={show}
                onClose={() => setShow(false)}
                onSave={handleSave}
            />
        </>
    );
}
