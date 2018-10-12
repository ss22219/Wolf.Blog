#!/bin/bash
service ssh start
cd ./host
export ASPNETCORE_URLS=http://localhost:5001; nohup dotnet ArticleHost.dll >/dev/null 2>&1 &
cd ../blog
export ASPNETCORE_URLS=http://localhost:5002; dotnet BlogWeb.dll
