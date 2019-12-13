FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS build-env
WORKDIR /app

COPY . ./
RUN dotnet publish Stickerzzz.Web -c Release -o out

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1
WORKDIR /app
COPY --from=build-env /app/Stickerzzz.Web/out .

ENTRYPOINT ["dotnet", "Stickerzzz.Web.dll"]
