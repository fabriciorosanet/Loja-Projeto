# Use a imagem base do .NET 8 SDK para compilar a aplicação
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app

# Copie os arquivos de projeto e restaure as dependências
COPY *.csproj ./
RUN dotnet restore

# Copie o restante dos arquivos e compile a aplicação
COPY . ./
RUN dotnet publish ./Loja-Projeto.csproj -c Release -o out

# Use a imagem base do .NET 8 Runtime para rodar a aplicação
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app
COPY --from=build /app/out .

# Exponha a porta em que a aplicação irá rodar
EXPOSE 8080

# Defina o comando de entrada para rodar a aplicação
ENTRYPOINT ["dotnet", "Loja-Projeto.dll"]