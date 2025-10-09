FROM node:20-alpine AS build
WORKDIR /app


COPY package.json package-lock.json* ./
RUN npm ci --no-audit --no-fund


COPY . .
RUN npm run build


# ===== Runtime stage (Nginx) =====
FROM nginx:1.27-alpine AS final


# Copia estáticos do Vite
COPY --from=build /app/dist /usr/share/nginx/html


# Nginx com proxy para a API em /api
COPY nginx.conf /etc/nginx/conf.d/default.conf


EXPOSE 80
CMD ["nginx", "-g", "daemon off;"]