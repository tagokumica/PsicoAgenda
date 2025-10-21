# PsicoAgenda
# Guia de Instalação do Ambiente de Desenvolvimento

Este guia explica como preparar seu ambiente para desenvolver aplicações **.NET 8 + React + SQL Server**, com Docker e GitHub.
# Sumário

1. [Instalação do Node.js e NPM](#-1-nodejs-e-npm)  
2. [Instalação do Docker Desktop](#-2-docker-desktop)  
3. [Instalação do GitHub Desktop](#-3-github-desktop)  
4. [Instalação do Visual Studio 2022 (para .NET)](#-4-visual-studio-2022-para-net)  
5. [Instalação do Visual Studio Code (para front-end React)](#-5-visual-studio-code-para-front-end-react)  
6. [Instalação do SQL Server Management Studio (SSMS)](#-6-sql-server-management-studio-ssms) 
---

##  1. Node.js e NPM

O **Node.js** é necessário para executar o front-end React.  
O **NPM** (Node Package Manager) vem junto com o Node.

### Instalação (Windows)

1. Acesse o site oficial:  
    [https://nodejs.org/](https://nodejs.org/)
2. Baixe a **versão LTS (Long Term Support)** — geralmente marcada como “Recommended for Most Users”.
3. Execute o instalador:
   - Marque a opção **“Add to PATH”** durante a instalação.
4. Após concluir, verifique a instalação no terminal (PowerShell ou CMD):

   ```bash
   node -v
   npm -v
   ```
## 2. Docker Desktop

O Docker permite criar e executar containers para o back-end (.NET), front-end (React) e banco de dados (SQL Server).

### Instalação

Baixe o instalador em:
[https://www.docker.com/products/docker-desktop/](https://www.docker.com/products/docker-desktop/)
Execute o instalador e siga os passos padrão.

Após a instalação, abra o Docker Desktop e espere o daemon inicializar.

## 3. GitHub Desktop

O GitHub Desktop facilita o controle de versão e sincronização de projetos com o GitHub.

### Instalação

Acesse:
 [https://desktop.github.com/](https://desktop.github.com/)

Baixe e instale normalmente.
Faça login com sua conta do GitHub.
Clone um repositório diretamente pela interface ou arraste a pasta do projeto.

## 4. Visual Studio 2022 (para .NET)

O **Visual Studio 2022** é o ambiente recomendado para o desenvolvimento **.NET 8**.

### Instalação

1. Acesse o site oficial:  
     [https://visualstudio.microsoft.com/pt-br/downloads/](https://visualstudio.microsoft.com/pt-br/downloads/)
2. Escolha a edição **Community (gratuita)**.
3. Durante a instalação, selecione o workload:
   -**ASP.NET e desenvolvimento web**
4. Após a instalação, verifique a versão do SDK .NET executando o comando abaixo no terminal:

   ```bash
   dotnet --version

## 5. Visual Studio Code (para front-end React)

O **Visual Studio Code (VS Code)** é um editor leve e moderno, ideal para o desenvolvimento **React + TypeScript**.

### Instalação

1. Acesse o site oficial:  
      [https://code.visualstudio.com/](https://code.visualstudio.com/)
2. Baixe e instale normalmente.
3. Após a instalação, abra o VS Code e instale as seguintes extensões recomendadas:

   -  **ES7+ React/Redux/React-Native snippets** — atalhos de código para React  
   -  **Prettier – Code formatter** — formatação automática de código  
   -  **Error Lens** — destaque de erros em tempo real  
   -  **GitHub Copilot** — sugestões inteligentes de código  
   -  **Docker** — integração e gerenciamento de containers  

## 6. SQL Server Management Studio (SSMS)

O **SQL Server Management Studio (SSMS)** é o cliente gráfico oficial da Microsoft para gerenciar bancos de dados **SQL Server**.

### Instalação

1. Acesse o instalador oficial:  
   [https://aka.ms/ssmsfullsetup](https://aka.ms/ssmsfullsetup)
2. Baixe e instale normalmente.
3. Após abrir o SSMS, conecte-se ao servidor configurado no **Docker** conforme os dados abaixo:

   | Campo              | Valor                                                        |
   |--------------------|--------------------------------------------------------------|
   | **Server name**    | `localhost,1433`                                             |
   | **Authentication** | SQL Server Authentication                                    |
   | **Login**          | `sa`                                                         |
   | **Password**       | conforme definido em seu `.env` (exemplo: `MSSQL_SA_PASSWORD`) |