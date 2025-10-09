FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["./api/PsicoAgendaAPI/PsicoAgendaAPI.csproj", "PsicoAgendaAPI/"]
RUN dotnet restore "PsicoAgendaAPI/PsicoAgendaAPI.csproj"
COPY ./api/ .
WORKDIR "/src/PsicoAgendaAPI"
RUN dotnet publish "PsicoAgendaAPI.csproj" -c Release -o /app/publish /p:UseAppHost=false

# Etapa 2: Runtime
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app
COPY --from=build /app/publish .
RUN ls 
ENTRYPOINT ["dotnet", "PsicoAgendaAPI.dll"]