FROM mcr.microsoft.com/dotnet/sdk:5.0-alpine AS build-env
WORKDIR /app
COPY *sln ./
COPY ./Api ./Api
COPY ./Common ./Common
RUN dotnet restore ./Api/KiedyKolos.Api/KiedyKolos.Api.csproj
RUN dotnet publish ./Api/KiedyKolos.Api -c Release -o out
RUN dotnet dev-certs https -ep /https/aspnetapp.pfx -p s49z2a49n9541e

FROM mcr.microsoft.com/dotnet/aspnet:5.0
WORKDIR /home
COPY --from=build-env /app/out .
COPY --from=build-env /https/aspnetapp.pfx /https/aspnetapp.pfx
ENTRYPOINT dotnet KiedyKolos.Api.dll