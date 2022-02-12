FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["DigitalStudio.InvoiceManagement.WebApi/DigitalStudio.InvoiceManagement.WebApi.csproj", "DigitalStudio.InvoiceManagement.WebApi/"]
COPY ["DigitalStudio.InvoiceManagement.Domain/DigitalStudio.InvoiceManagement.Domain.csproj", "DigitalStudio.InvoiceManagement.Domain/"]
RUN dotnet restore "DigitalStudio.InvoiceManagement.WebApi/DigitalStudio.InvoiceManagement.WebApi.csproj"
COPY . .
WORKDIR "/src/DigitalStudio.InvoiceManagement.WebApi"
RUN dotnet build "DigitalStudio.InvoiceManagement.WebApi.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "DigitalStudio.InvoiceManagement.WebApi.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENV ASPNETCORE_URLS=http://+:7004
ENTRYPOINT ["dotnet", "DigitalStudio.InvoiceManagement.WebApi.dll"]