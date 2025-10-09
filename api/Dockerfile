
# ========= BACKEND (.NET 8 API) =========
# build da API
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS backend-build
WORKDIR /src

# copie só csproj primeiro para cache do restore
COPY PsicoAgendaAPI.csproj ./
RUN dotnet restore

# agora o restante do código
COPY . .
RUN dotnet publish -c Release -o /app/publish

# imagem final da API (runtime)
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS backend
WORKDIR /app
ENV ASPNETCORE_URLS=http://+:8080
COPY --from=backend-build /app/publish .
EXPOSE 8080
ENTRYPOINT ["dotnet", "PsicoAgendaAPI.dll"]
