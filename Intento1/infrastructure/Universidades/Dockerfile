FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /webapp

COPY ./Backend/*.csproj ./
RUN dotnet restore

COPY ./Backend .
COPY ./infrastructure/Universidades/dbsettings.json .
RUN dotnet publish -c Release -o out

FROM mcr.microsoft.com/dotnet/sdk:8.0
WORKDIR /webapp
COPY --from=build /webapp/out .
ENTRYPOINT [ "dotnet","Backend.dll" ]