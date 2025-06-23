# Commands

```shell
#Verifica versão do EF.
dotnet tool list -g

#Instala EF.
dotnet tool install --global dotnet-ef

#Cria Migrations.
dotnet ef migrations add InitialCreate --project MinhaRedeSocial.Infra --startup-project MinhaRedeSocial

#Atualiza Migrations.
dotnet ef database update --project MinhaRedeSocial.Infra --startup-project MinhaRedeSocial
```