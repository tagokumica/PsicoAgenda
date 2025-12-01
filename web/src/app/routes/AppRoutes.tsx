
import { Routes, Route } from "react-router-dom";

import AuthLayout from "../../layouts/AuthLayout";
import DashBoardLayout from "../../layouts/DashBoardLayout";

import LoginPage from "../../pages/auth/LoginPage";
import AddressDetailsPage from "../../pages/address/AddressDetailsPage";
import AddressesAddPage from "../../pages/address/AddressAddPage";
import AddressesUpdatePage from "../../pages/address/AddressUpdatePage";

export default function AppRoutes() {
    return (
        <Routes>
            <Route element={<AuthLayout />}>
                <Route path="/login" element={<LoginPage />} />
            </Route>

            <Route path="/dashboard" element={<DashBoardLayout />}>
                <Route path="address/:id/detalhes" element={<AddressDetailsPage />} />
                <Route path="/enderecos/:id/editar" element={<AddressesUpdatePage modal="edit" />} />
                <Route path="/enderecos/adicionar" element={<AddressesAddPage modal="add" />} />
            </Route>
        </Routes>
    );
}
