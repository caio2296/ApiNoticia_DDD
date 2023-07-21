# ApiNoticia_DDD

O objetivo dessa api e entender a arquitetura DDD e como se relaciona o back end(este projeto) com o front end(https://github.com/caio2296/NoticiaDDD_Front), escolhi usar a arquitetura DDD por algumas de suas vantagens como: separação de responsabilidades, organização do código, flexibilidade, facilidade de manutenção,agilidade e aplicação das regras de negócios.

## Tecnologias utilizadas
- ASP.NET Core 5.0
- Microsoft.EntityFrameworkCore.InMemory 5.0.8
- Microsoft.EntityFrameworkCore.SqlServer 5.0.8
- Microsoft.EntityFrameworkCore.Design 5.0.8
- Microsoft.AspNetCore.Identity.EntityFrameworkCore 5.0.8
- Microsoft.AspNetCore.Authentication.JwtBearer 5.0.8
- Microsoft.IdentityModel.Tokens 6.14.0
- System.IdentityModel.Tokens.Jwt 6.14.0
- Microsoft.AspNetCore.Identity 2.2.0
- Swagger

## Pré-requisitos
Visual Studio 2019 ou superior .NET 5.0 SDK

## Executando a API
Clone o repositório:
```bash
 git https://github.com/caio2296/ApiNoticia_DDD.git
```
Abra o projeto no Visual Studio

Use alguma ferramenta como o Postman ou o Swagger para testar os endpoints.

Alguns dos Endpoints disponíveis CriarTokenIdentity /api/CriarTokenIdentity -para criar o token.  AdicionaUsuarioIdentity /api/AdicionaUsuarioIdentity - Cria um usuário.
