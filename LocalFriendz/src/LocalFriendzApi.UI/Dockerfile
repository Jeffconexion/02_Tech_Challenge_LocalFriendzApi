# Fase 1: Imagem Base
# ---------------------
# Esta imagem será usada para executar o aplicativo. 
# Utiliza a imagem oficial do ASP.NET Core 8.0, que fornece um ambiente de execução.
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base

# Define o usuário para executar o aplicativo como um usuário não root, aumentando a segurança.
USER app

# Define o diretório de trabalho dentro do container onde os arquivos serão colocados.
WORKDIR /app

# Expõe as portas que o aplicativo vai utilizar.
# Portas 8080 e 8081 são expostas para comunicação externa.
EXPOSE 8080
EXPOSE 8081

# Fase 2: Construção
# --------------------
# Esta imagem será usada para construir o aplicativo.
# Utiliza a imagem oficial do SDK .NET Core 8.0, que fornece ferramentas de compilação e desenvolvimento.
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build

# Define a configuração de build, que pode ser "Release" ou "Debug".
ARG BUILD_CONFIGURATION=Release

# Define o diretório de trabalho dentro do container para a fase de construção.
WORKDIR /src

# Copia os arquivos .csproj (arquivos de projeto) para seus respectivos diretórios dentro do container.
# Isso permite que o .NET Core restaure as dependências antes de copiar o código-fonte completo.
COPY ["LocalFriendzApi.UI/LocalFriendzApi.UI.csproj", "LocalFriendzApi.UI/"]
COPY ["LocalFriendzApi.Application/LocalFriendzApi.Application.csproj", "LocalFriendzApi.Application/"]
COPY ["LocalFriendzApi.Infrastructure/LocalFriendzApi.Infrastructure.csproj", "LocalFriendzApi.Infrastructure/"]
COPY ["LocalFriendzApi.Domain/LocalFriendzApi.Domain.csproj", "LocalFriendzApi.Domain/"]
COPY ["tests/LocalFriendzApi.UnitTests/LocalFriendzApi.UnitTests.csproj", "tests/LocalFriendzApi.UnitTests/"]


# Restaura as dependências do projeto. Isso faz o download de todos os pacotes NuGet necessários.
RUN dotnet restore "LocalFriendzApi.UI/LocalFriendzApi.UI.csproj"

# Copia todos os arquivos do código-fonte para o diretório de trabalho dentro do container.
COPY . .

# Define o diretório de trabalho para a construção do projeto principal.
WORKDIR "/src/LocalFriendzApi.UI"

# Construa o projeto especificado com a configuração definida (Release ou Debug).
# O resultado da construção será colocado no diretório /app/build dentro do container.
RUN dotnet build "LocalFriendzApi.UI.csproj" -c $BUILD_CONFIGURATION -o /app/build

# Fase 3: Publicação
# ---------------------
# Esta imagem publica o aplicativo em um formato que pode ser executado em um ambiente de produção.
FROM build AS publish

# Publica o aplicativo. A opção /p:UseAppHost=false é usada para não gerar um executável específico do sistema operacional,
# facilitando a execução em diferentes plataformas.
RUN dotnet publish "LocalFriendzApi.UI/LocalFriendzApi.UI.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# Fase 4: Imagem Final
# ----------------------
# Esta imagem é a imagem final que será usada para executar o aplicativo publicado.
FROM base AS final

# Define o diretório de trabalho dentro do container.
WORKDIR /app

# Copia os arquivos publicados da fase anterior para o diretório de trabalho da imagem final.
COPY --from=publish /app/publish .

# Define o ponto de entrada do container. O comando `dotnet LocalFriendzApi.UI.dll` é executado quando o container é iniciado.
ENTRYPOINT ["dotnet", "LocalFriendzApi.UI.dll"]
