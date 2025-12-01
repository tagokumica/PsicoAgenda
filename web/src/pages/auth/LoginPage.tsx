// src/pages/login/LoginPage.tsx
import {useState} from "react";

export default function LoginPage() {
    const [email, setEmail] = useState("");
    const [senha, setSenha] = useState("");

    function handleSubmit(e: React.FormEvent) {
        e.preventDefault();
        console.log("Tentando login:", email, senha);
    }

    return (
        <div className="position-relative">
            <div className="d-flex vh-100 justify-content-center align-items-center">
                <div className="card">
                    <div className="card-body border border-primary">
                        <div>
                            <h1 className="display-1 text-start">PsicoAgenda</h1>
                            <p className="lead"><span>Sistema de gestão de atendimentos psicológicos</span></p>
                        </div>
                        <form className="mt-5" method="post">
                            <div><label className="form-label">E-mail</label><input className="border border-dark form-control"
                                                                                    type="email" name="email"/>
                            </div>
                            <div className="mb-3"><label className="form-label">Password</label><input
                                className="border border-dark form-control" type="password" name="password"/></div>
                            <div className="mb-3">
                                <button className="btn btn-primary w-100 d-block" type="submit">Login</button>
                            </div>
                        </form>

                        <form className="mt-5" onSubmit={handleSubmit}>
                            <div className="mb-3">
                                <label className="form-label">E-mail</label>
                                <input
                                    type="email"
                                    className="border border-dark form-control"
                                    value={email}
                                    onChange={(e) => setEmail(e.target.value)}
                                />
                            </div>

                            <div className="mb-3">
                                <label className="form-label">Senha</label>
                                <input
                                    type="password"
                                    className="border border-dark form-control"
                                    value={senha}
                                    onChange={(e) => setSenha(e.target.value)}
                                />
                            </div>

                            <button className="btn btn-primary w-100">Entrar</button>
                        </form>
                    </div>
                </div>
            </div>
        </div>
    );
}
