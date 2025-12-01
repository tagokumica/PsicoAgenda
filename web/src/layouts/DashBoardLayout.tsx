import { Outlet } from "react-router-dom";

export default function DashBoardLayout() {
    return (
        <>
            <div className="row">
                <div className="col col-xxl-2">
                    <nav className="navbar navbar-expand-md bg-body">
                        <div className="container-fluid"><a className="navbar-brand" href="#">PsicoAgenda</a></div>
                    </nav>
                </div>
                <div className="col col-xxl-10 offset-xxl-0">
                    <nav className="navbar navbar-expand-md bg-body">
                        <div className="container-fluid">
                            <button className="navbar-toggler" data-bs-toggle="collapse"
                                    data-bs-target="#navcol-1"><span
                                className="visually-hidden">Toggle navigation</span><span
                                className="navbar-toggler-icon"></span>
                            </button>
                            <div id="navcol-1" className="collapse navbar-collapse justify-content-end">
                                <ul className="navbar-nav">
                                    <li className="nav-item"><a className="nav-link" href="#">
                                        <svg className="bi bi-arrow-bar-right" xmlns="http://www.w3.org/2000/svg"
                                             width="1em"
                                             height="1em" fill="currentColor" viewBox="0 0 16 16">
                                            <path fill-rule="evenodd"
                                                  d="M6 8a.5.5 0 0 0 .5.5h5.793l-2.147 2.146a.5.5 0 0 0 .708.708l3-3a.5.5 0 0 0 0-.708l-3-3a.5.5 0 0 0-.708.708L12.293 7.5H6.5A.5.5 0 0 0 6 8m-2.5 7a.5.5 0 0 1-.5-.5v-13a.5.5 0 0 1 1 0v13a.5.5 0 0 1-.5.5"></path>
                                        </svg>
                                        Sair</a></li>
                                </ul>
                            </div>
                        </div>
                    </nav>
                </div>
            </div>
            <div className="row">
                <div className="col-xxl-2 mt-5">
                    <ul className="nav flex-column">
                        <li className="nav-item"><a className="nav-link active link-info" href="#">
                            <svg className="bi bi-window-dash" xmlns="http://www.w3.org/2000/svg" width="1em"
                                 height="1em"
                                 fill="currentColor" viewBox="0 0 16 16">
                                <path
                                    d="M2.5 5a.5.5 0 1 0 0-1 .5.5 0 0 0 0 1M4 5a.5.5 0 1 0 0-1 .5.5 0 0 0 0 1m2-.5a.5.5 0 1 1-1 0 .5.5 0 0 1 1 0"></path>
                                <path
                                    d="M0 4a2 2 0 0 1 2-2h11a2 2 0 0 1 2 2v4a.5.5 0 0 1-1 0V7H1v5a1 1 0 0 0 1 1h5.5a.5.5 0 0 1 0 1H2a2 2 0 0 1-2-2zm1 2h13V4a1 1 0 0 0-1-1H2a1 1 0 0 0-1 1z"></path>
                                <path
                                    d="M16 12.5a3.5 3.5 0 1 1-7 0 3.5 3.5 0 0 1 7 0m-5.5 0a.5.5 0 0 0 .5.5h3a.5.5 0 0 0 0-1h-3a.5.5 0 0 0-.5.5"></path>
                            </svg>
                            Dashboard</a></li>
                        <li className="nav-item"><a className="nav-link link-info" href="#">
                            <svg className="bi bi-person" xmlns="http://www.w3.org/2000/svg" width="1em" height="1em"
                                 fill="currentColor" viewBox="0 0 16 16">
                                <path
                                    d="M8 8a3 3 0 1 0 0-6 3 3 0 0 0 0 6m2-3a2 2 0 1 1-4 0 2 2 0 0 1 4 0m4 8c0 1-1 1-1 1H3s-1 0-1-1 1-4 6-4 6 3 6 4m-1-.004c-.001-.246-.154-.986-.832-1.664C11.516 10.68 10.289 10 8 10c-2.29 0-3.516.68-4.168 1.332-.678.678-.83 1.418-.832 1.664z"></path>
                            </svg>
                            Pacientes</a></li>
                        <li className="nav-item"><a className="nav-link link-info" href="#">
                            <svg className="bi bi-person-badge-fill" xmlns="http://www.w3.org/2000/svg" width="1em"
                                 height="1em"
                                 fill="currentColor" viewBox="0 0 16 16">
                                <path
                                    d="M2 2a2 2 0 0 1 2-2h8a2 2 0 0 1 2 2v12a2 2 0 0 1-2 2H4a2 2 0 0 1-2-2zm4.5 0a.5.5 0 0 0 0 1h3a.5.5 0 0 0 0-1zM8 11a3 3 0 1 0 0-6 3 3 0 0 0 0 6m5 2.755C12.146 12.825 10.623 12 8 12s-4.146.826-5 1.755V14a1 1 0 0 0 1 1h8a1 1 0 0 0 1-1z"></path>
                            </svg>
                            Profisiionais</a></li>
                        <li className="nav-item"><a className="nav-link link-info" href="#">
                            <svg className="bi bi-book" xmlns="http://www.w3.org/2000/svg" width="1em" height="1em"
                                 fill="currentColor" viewBox="0 0 16 16">
                                <path
                                    d="M1 2.828c.885-.37 2.154-.769 3.388-.893 1.33-.134 2.458.063 3.112.752v9.746c-.935-.53-2.12-.603-3.213-.493-1.18.12-2.37.461-3.287.811V2.828zm7.5-.141c.654-.689 1.782-.886 3.112-.752 1.234.124 2.503.523 3.388.893v9.923c-.918-.35-2.107-.692-3.287-.81-1.094-.111-2.278-.039-3.213.492V2.687zM8 1.783C7.015.936 5.587.81 4.287.94c-1.514.153-3.042.672-3.994 1.105A.5.5 0 0 0 0 2.5v11a.5.5 0 0 0 .707.455c.882-.4 2.303-.881 3.68-1.02 1.409-.142 2.59.087 3.223.877a.5.5 0 0 0 .78 0c.633-.79 1.814-1.019 3.222-.877 1.378.139 2.8.62 3.681 1.02A.5.5 0 0 0 16 13.5v-11a.5.5 0 0 0-.293-.455c-.952-.433-2.48-.952-3.994-1.105C10.413.809 8.985.936 8 1.783"></path>
                            </svg>
                            Agenda</a></li>
                        <li className="nav-item"><a className="nav-link link-info" href="#">
                            <svg className="bi bi-door-open" xmlns="http://www.w3.org/2000/svg" width="1em" height="1em"
                                 fill="currentColor" viewBox="0 0 16 16">
                                <path d="M8.5 10c-.276 0-.5-.448-.5-1s.224-1 .5-1 .5.448.5 1-.224 1-.5 1"></path>
                                <path
                                    d="M10.828.122A.5.5 0 0 1 11 .5V1h.5A1.5 1.5 0 0 1 13 2.5V15h1.5a.5.5 0 0 1 0 1h-13a.5.5 0 0 1 0-1H3V1.5a.5.5 0 0 1 .43-.495l7-1a.5.5 0 0 1 .398.117zM11.5 2H11v13h1V2.5a.5.5 0 0 0-.5-.5M4 1.934V15h6V1.077z"></path>
                            </svg>
                            Fila de Espera</a></li>
                        <li className="nav-item"><a className="nav-link link-info" href="#">
                            <svg className="bi bi-person-fill-exclamation" xmlns="http://www.w3.org/2000/svg"
                                 width="1em"
                                 height="1em" fill="currentColor" viewBox="0 0 16 16">
                                <path
                                    d="M11 5a3 3 0 1 1-6 0 3 3 0 0 1 6 0m-9 8c0 1 1 1 1 1h5.256A4.493 4.493 0 0 1 8 12.5a4.49 4.49 0 0 1 1.544-3.393C9.077 9.038 8.564 9 8 9c-5 0-6 3-6 4"></path>
                                <path
                                    d="M16 12.5a3.5 3.5 0 1 1-7 0 3.5 3.5 0 0 1 7 0m-3.5-2a.5.5 0 0 0-.5.5v1.5a.5.5 0 0 0 1 0V11a.5.5 0 0 0-.5-.5m0 4a.5.5 0 1 0 0-1 .5.5 0 0 0 0 1"></path>
                            </svg>
                            Sessões</a></li>
                        <li className="nav-item"><a className="nav-link link-info" href="#">
                            <svg className="bi bi-file-person" xmlns="http://www.w3.org/2000/svg" width="1em"
                                 height="1em"
                                 fill="currentColor" viewBox="0 0 16 16">
                                <path
                                    d="M12 1a1 1 0 0 1 1 1v10.755S12 11 8 11s-5 1.755-5 1.755V2a1 1 0 0 1 1-1zM4 0a2 2 0 0 0-2 2v12a2 2 0 0 0 2 2h8a2 2 0 0 0 2-2V2a2 2 0 0 0-2-2z"></path>
                                <path d="M8 10a3 3 0 1 0 0-6 3 3 0 0 0 0 6"></path>
                            </svg>
                            Perfil</a></li>
                    </ul>
                </div>
                <div className="col bg-light">
                    <div className="row">
                        <Outlet/>
                    </div>
                </div>
            </div>
        </>
    );
}
