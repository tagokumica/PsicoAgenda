import { useEffect, useState } from "react";
import { useParams } from "react-router-dom";
import AddressDetails from "../../components/address/AddressDetails";
import {AddressViewModel} from "../../viewmodels/AddressViewModel";



export default function AddressDetailsPage() {
    const { id } = useParams();
    const [address, setAddress] = useState<AddressViewModel | null>(null);
    const [loading, setLoading] = useState(true);
    const [error, setError] = useState<string | null>(null);

    useEffect(() => {
        async function load() {
            try {
                const response = await fetch(`https://localhost:7080/api/addresses/${id}`);
                if (!response.ok) throw new Error("Erro ao carregar endereço");

                const data = await response.json();
                setAddress(data);
            } catch (err: any) {
                setError(err.message);
            } finally {
                setLoading(false);
            }
        }

        load();
    }, [id]);

    if (loading) {
        return <div className="container mt-5">Carregando...</div>;
    }

    if (error) {
        return <div className="container mt-5 text-danger">{error}</div>;
    }

    if (!address) {
        return <div className="container mt-5">Endereço não encontrado.</div>;
    }

    return (
        <AddressDetails
            street={address.street}
            number={address.number}
            complement={address.complement}
            city={address.city}
            zipCode={address.zipCode}
            state={address.state}
        />
    );
}
