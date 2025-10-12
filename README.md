# PsicoAgenda
# 🧰 Guia de Instalação do Ambiente de Desenvolvimento

Este guia explica como preparar seu ambiente para desenvolver aplicações **.NET 8 + React + SQL Server**, com Docker e GitHub.

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

## GitHub Desktop

O GitHub Desktop facilita o controle de versão e sincronização de projetos com o GitHub.

### Instalação

Acesse:
 [https://desktop.github.com/](https://desktop.github.com/)

Baixe e instale normalmente.
Faça login com sua conta do GitHub.
Clone um repositório diretamente pela interface ou arraste a pasta do projeto.