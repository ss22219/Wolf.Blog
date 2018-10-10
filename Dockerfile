FROM microsoft/dotnet:2.1-aspnetcore-runtime AS base

# Install the SSHD server
RUN apt-get update \ 
 && apt-get install -y --no-install-recommends openssh-server \ 
 && mkdir -p /run/sshd

# Set password to '123456'. Change as needed.  
RUN echo "root:123456" | chpasswd

#Copy settings file. See elsewhere to find them. 
COPY sshd_config /etc/ssh/sshd_config
COPY authorized_keys  root/.ssh/authorized_keys

# Install Visual Studio Remote Debugger
RUN apt-get install zip unzip
RUN curl -sSL https://aka.ms/getvsdbgsh | bash /dev/stdin -v latest -l ~/vsdbg  
EXPOSE 2222

WORKDIR /app
COPY docker_boot.sh /usr/local/bin/
RUN chmod +x /usr/local/bin/docker_boot.sh

EXPOSE 5001
EXPOSE 5002

FROM microsoft/dotnet:2.1-sdk AS build
WORKDIR /src
COPY . .
RUN dotnet publish ArticleHost/ArticleHost.csproj -c Release -o /app
RUN dotnet publish BlogWeb/BlogWeb.csproj -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=build /app .

ENV ASPNETCORE_ENVIRONMENT Production
ENTRYPOINT ["docker_boot.sh"]
