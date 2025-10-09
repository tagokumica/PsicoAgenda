# Build com Node (Alpine)
FROM node:20-alpine AS build
WORKDIR /app

# Copie só o package.json (NÃO copie o lock)
COPY package.json ./

# Gere as dependências para o ambiente Linux/musl
RUN npm install

# Agora sim copie o código
COPY . .

# Build de produção (Vite)
RUN npm run build

# Nginx para servir o build
FROM nginx:1.27-alpine
RUN rm -f /etc/nginx/conf.d/default.conf
COPY nginx.conf /etc/nginx/conf.d/default.conf
COPY --from=build /app/dist /usr/share/nginx/html
EXPOSE 80
CMD ["nginx", "-g", "daemon off;"]