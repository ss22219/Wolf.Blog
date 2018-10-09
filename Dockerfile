FROM microsoft/dotnet:2.1-aspnetcore-runtime AS base
WORKDIR /app
COPY docker_boot.sh /usr/local/bin/
RUN chmod +x /usr/local/bin/docker_boot.sh

EXPOSE 5002

FROM microsoft/dotnet:2.1-sdk AS build
WORKDIR /src
COPY . .
RUN dotnet restore
RUN dotnet publish ArticleHost/ArticleHost.csproj -c Release -o /app
RUN dotnet publish BlogWeb/BlogWeb.csproj -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=build /app .

ENV ASPNETCORE_ENVIRONMENT Production
ENTRYPOINT ["docker_boot.sh"]