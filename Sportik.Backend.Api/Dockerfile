FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build

WORKDIR /src

COPY Sportik.Backend.sln ./
COPY Sportik.Backend.Api/Sportik.Backend.Api.csproj ./Sportik.Backend.Api/
COPY Sportik.Backend.Application/Sportik.Backend.Application.csproj ./Sportik.Backend.Application/
COPY Sportik.Backend.Domain/Sportik.Backend.Domain.csproj ./Sportik.Backend.Domain/
COPY Sportik.Backend.Infrastructure/Sportik.Backend.Infrastructure.csproj ./Sportik.Backend.Infrastructure/

RUN dotnet restore

COPY . .

WORKDIR /src/Sportik.Backend.Api

FROM build AS publish

RUN dotnet publish Sportik.Backend.Api.csproj \
      -c Release \
      -f net8.0 \
      -r linux-x64 \
      --self-contained false \
      -o "/app/publish" \
      -p:DeleteExistingFiles=true

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime

WORKDIR /app

COPY --from=publish /app/publish .

ENTRYPOINT ["dotnet", "Sportik.Backend.Api.dll"]
