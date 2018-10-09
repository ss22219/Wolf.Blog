#!/bin/sh
export ASPNETCORE_URLS=http://localhost:5001; nohup dotnet ArticleHost.dll >/dev/null 2>&1 &
export ASPNETCORE_URLS=http://localhost:5002; dotnet BlogWeb.dll