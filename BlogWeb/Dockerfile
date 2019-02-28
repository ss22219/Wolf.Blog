FROM microsoft/dotnet:2.2-aspnetcore-runtime

EXPOSE 80

COPY . /publish
WORKDIR /publish

ENTRYPOINT ["dotnet", "BlogWeb.dll"]