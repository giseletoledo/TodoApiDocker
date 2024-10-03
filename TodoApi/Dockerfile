# Usar a imagem base do SDK do .NET 8 para compilar a aplicação
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build-env
WORKDIR /app
EXPOSE 80      
EXPOSE 5001   

# Copiar csproj e restaurar dependências
COPY *.csproj ./
RUN dotnet restore

# Copiar o restante do código e compilar
COPY . ./
RUN dotnet publish -c Release -o out

# Usar a imagem base do runtime do .NET 8 para a aplicação
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=build-env /app/out .

# Definir o comando de execução
ENTRYPOINT ["dotnet", "TodoApi.dll"]
