# Use uma imagem base do SDK para construir a aplicação
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build

# Defina o diretório de trabalho
WORKDIR /app
EXPOSE 80
EXPOSE 5000


# Copie o arquivo de projeto e restaure as dependências
COPY *.csproj ./
RUN dotnet restore

# Copie todos os arquivos e publique a aplicação
COPY . ./
RUN dotnet publish -c Release -o out

# Use uma imagem base do runtime para rodar a aplicação
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime

# Defina o diretório de trabalho
WORKDIR /app

# Copie os arquivos publicados da etapa de build
COPY --from=build /app/out .
ENV ASPNETCORE_URLS="http://*:80;http://*:5001"

# Defina o comando para executar a aplicação
ENTRYPOINT ["dotnet", "TodoApi.dll"]