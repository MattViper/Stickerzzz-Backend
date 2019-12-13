FROM mcr.microsoft.com/dotnet/core/aspnet:3.1 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS build
WORKDIR /src
COPY ["src/Stickerzzz.Core/Stickerzzz.Core.csproj", "src/Stickerzzz.Core/"]
COPY ["src/Stickerzzz.Infrastructure/Stickerzzz.Infrastructure.csproj", "src/Stickerzzz.Infrastructure/"]
COPY ["src/Stickerzzz.Web/Stickerzzz.Web.csproj", "src/Stickerzzz.Web/"]

RUN dotnet restore "src/Stickerzzz.Web/Stickerzzz.Web.csproj"
COPY . .
WORKDIR "/src/src/Stickerzzz.Web"
RUN dotnet build "Stickerzzz.Web.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Stickerzzz.Web.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Stickerzzz.Web.dll"]
