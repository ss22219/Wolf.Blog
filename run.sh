#!/bin/sh
docker build . -f ./Dockerfile -t wolf.blog
docker rm -f wolf.blog
docker run -d -p 2003:2003 --restart always --name wolf.blog wolf.blog