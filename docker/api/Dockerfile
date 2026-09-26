FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /app

COPY src/Analyzer.Html/Analyzer.Html.csproj src/Analyzer.Html/ 

RUN dotnet restore src/Analyzer.Html/Analyzer.Html.csproj

COPY src/ src/

RUN dotnet publish src/Analyzer.Html/Analyzer.Html.csproj \
    -c Release \
    -o /app/publish \
    --no-restore 

FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS runtime
WORKDIR /app

COPY --from=build /app/publish .

ENTRYPOINT ["./Analyzer.Html"]
