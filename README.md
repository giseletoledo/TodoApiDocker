## Todo API
Esta é uma API simples para gerenciamento de notas utilizando .NET Core. A API permite criar, atualizar, deletar e listar notas. 
Ela está configurada para rodar em um contêiner Docker e tem uma documentação Swagger.

### Requisitos
- .NET 8 SDK

- Docker

### Estrutura do Projeto
O projeto segue a arquitetura básica MVC com as seguintes pastas principais:

- Controllers: Contém os controladores da API que lidam com as requisições HTTP.
- Services: Contém a lógica de negócio.
- DTOs: Contém os objetos de transferência de dados (Data Transfer Objects).
- Models: Contém os modelos usados na aplicação.
- Repository: Contém a lógica de interação com o banco de dados.

```
/TodoApi
│
├── /Controllers
│   └── TodoAppController.cs
│
├── /DTOs
│   └── NoteDto.cs
│
├── /Models
│   └── Note.cs
│
├── /Exceptions
│   ├── ApiError.cs
│   └── CustomException.cs
|   └── ErrorHandlerMiddleware.cs
|
├── /Repositories
│   ├── INoteRepository.cs
│   └── NoteRepository.cs
│
├── /Services
│   ├── INoteService.cs
│   └── NoteService.cs
│
├── /Migrations
│   └── [Arquivo de migração para banco de dados]
│
├── /Properties
│   └── launchSettings.json
│
├── /wwwroot
│   └── [Arquivos estáticos (se aplicável)]
│
├── appsettings.json
├── Program.cs
├── docker-compose.yml
├── Dockerfile
├── README.md
├── .gitignore
├── TodoApi.csproj
└── TodoApi.http
```

### Configuração e Execução

#### 1. Clonar o Repositório
git clone https://github.com/seu-usuario/seu-repositorio.git
cd seu-repositorio

#### 2. Compilar e Executar a API Localmente

```
dotnet restore
dotnet build
dotnet run

```

#### 3. Executar com Docker

#### 3.1. Construir a Imagem Docker

```
docker build -t todo-api .
```

#### 3.2. Executar o Contêiner

```
docker run -d -p 80:80 --name todo-api todo-api
```

#### 4. Testar a API
Pode ser utilizadas ferramentas como Postman, curl ou qualquer outro cliente HTTP para fazer requisições à API.

Exemplo de Requisições:

Listar todas as notas  

```
GET http://localhost/api/TodoApp/all
```

Obter uma nota por ID

 ```
  GET http://localhost/api/TodoApp/note/{id}
 ``` 
 
Adicionar uma nova nota
```
POST http://localhost/api/TodoApp/AddNote
Content-Type: application/json

{
  "description": "Estudar para a prova de matemática"
}
```
 Atualizar uma nota
 
```
PUT http://localhost/api/TodoApp/UpdateNote/{id}
Content-Type: application/json

{
  "description": "Revisar notas de matemática e português"
}
```

Deletar uma nota

```
 DELETE http://localhost/api/TodoApp/DeleteNote/{id}
```

#### 5. Acessar a Documentação do Swagger
A documentação interativa da API estará disponível no Swagger:

http://localhost/swagger/index.html

Variáveis de Ambiente
Se quiser configurar variáveis de ambiente para o contêiner Docker, você pode fazer isso passando-as com a flag -e ao rodar o contêiner. Exemplo:

```
docker run -d -p 80:80 --name todo-api -e "ASPNETCORE_ENVIRONMENT=Production" todo-api
```

Licença
Este projeto está sob a licença MIT. 
Veja o arquivo LICENSE(https://opensource.org/license/mit) para mais detalhes.
