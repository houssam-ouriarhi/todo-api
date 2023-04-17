#!/bin/bash

echo "Building docker image"

docker build -t myapi-v1 .

echo "Starting docker container"

docker rm -f $(docker ps -a -q --filter="name=myapi-container")

docker run -d -p 4300:4300 --rm --name myapi-container myapi-v1