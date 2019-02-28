FROM microsoft/dotnet:2.2-aspnetcore-runtime

EXPOSE 5000
EXPOSE 5001

COPY . /publish
WORKDIR /publish

ENTRYPOINT ["dotnet", "BlogWeb.dll"]