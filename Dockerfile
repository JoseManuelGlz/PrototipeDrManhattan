FROM microsoft/dotnet:sdk AS build-env

LABEL "com.conekta.vendor"="Conekta"
LABEL "com.conekta.maintainer"="Carmen Mendiola <maria.mendiola@conekta.com>"
LABEL "com.conekta.product"="Conekta Documents Manager"
LABEL "version"="0.1"
LABEL "description"="Documents Manager is responsible for handling the documents involved in the various conekta processes"

WORKDIR /app

COPY ./*.sln ./

COPY src/*/*.csproj ./
RUN for file in $(ls *.csproj); do mkdir -p src/${file%.*}/ && mv $file src/${file%.*}/; done

COPY tests/*/*.csproj ./
RUN for file in $(ls *.csproj); do mkdir -p tests/${file%.*}/ && mv $file tests/${file%.*}/; done

COPY nuget.config /root/.nuget/NuGet/NuGet.Config

RUN dotnet restore "./src/Documents.Manager.Models/Documents.Manager.Models.csproj"
RUN dotnet restore "./src/Documents.Manager.DataAccess/Documents.Manager.DataAccess.csproj"
RUN dotnet restore "./src/Documents.Manager.Business/Documents.Manager.Business.csproj"
RUN dotnet restore "./src/Documents.Manager.Service/Documents.Manager.Service.csproj"

COPY ./tests ./tests
COPY ./src ./src
COPY ./config ./config

RUN dotnet publish "./src/Documents.Manager.Service/Documents.Manager.Service.csproj" -c Release -o "../../dist"

FROM microsoft/dotnet:aspnetcore-runtime
WORKDIR /app

COPY --from=build-env /app/dist .
COPY --from=build-env /app/config/newrelic-netcore20-*.deb .
COPY --from=build-env /app/config/newrelic.config .

ENV ASPNETCORE_URLS="http://*:5000"
ENV ASPNETCORE_ENVIRONMENT="Development"
ENV AWS_SECRETS_MANAGER_NAME="DocumentsManager/sandbox"
ENV AWS_SECRETS_MANAGER_REGION="us-east-1"
ENV ASPNETCORE_ENVIRONMENT=$ASPNETCORE_ENVIRONMENT
ENV AWS_SECRETS_MANAGER_NAME=$AWS_SECRETS_MANAGER_NAME
ENV AWS_SECRETS_MANAGER_REGION=$AWS_SECRETS_MANAGER_REGION
ENV NEW_RELIC_DISTRIBUTED_TRACING_ENABLED=true

RUN dpkg -i newrelic-netcore20-*.deb
RUN ls -la ./

ENTRYPOINT [ "/usr/local/newrelic-netcore20-agent/run.sh", "dotnet", "Documents.Manager.Service.dll"]
