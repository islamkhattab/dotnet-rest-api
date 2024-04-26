# How to run

## Start SQL server

```bash

SQL_PASSWORD=YOUR_PASSWORD

sudo docker run -e "ACCEPT_EULA=Y" -e "MSSQL_SA_PASSWORD=$SQL_PASSWORD" -p 1433:1433 \
        -v gameshop-sqlvolume:/var/opt/mssql \
        --name mssql \
        --hostname gameshop \
        -d --rm mcr.microsoft.com/mssql/server:2022-latest 
```

## Setting web api connection string in user secrets

```bash
cd GameShop.Api

SQL_PASSWORD=YOUR_PASSWORD

dotnet user-secrets set "ConnectionStrings:GameShopDB" "Server=localhost; Database=GameShop; User Id=sa; Password=$SQL_PASSWORD; TrustServerCertificate=True;"

dotnet user-secrets list
```