FROM microsoft/dotnet:2.2-aspnetcore-runtime AS base
WORKDIR /app
EXPOSE 5001

FROM microsoft/dotnet:2.2-sdk AS build
WORKDIR /src
COPY src/Documents.Manager.Service/Documents.Manager.Service.csproj src/Documents.Manager.Service/
COPY src/Documents.Manager.Business/Documents.Manager.Business.csproj src/Documents.Manager.Business/
COPY src/Documents.Manager.Models/Documents.Manager.Models.csproj src/Documents.Manager.Models/
COPY src/Documents.Manager.DataAccess/Documents.Manager.DataAccess.csproj src/Documents.Manager.DataAccess/
RUN dotnet restore src/Documents.Manager.Service/Documents.Manager.Service.csproj
COPY . .
WORKDIR /src/src/Documents.Manager.Service
RUN dotnet build Documents.Manager.Service.csproj -c Release -o /app

FROM build AS publish
RUN dotnet publish Documents.Manager.Service.csproj -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "Documents.Manager.Service.dll"]
