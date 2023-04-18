#!/bin/bash

echo "Building docker image"

docker build -t myapi-v1 .

echo "Starting docker container"

docker rm -f $(docker ps -a -q --filter="name=myapi-container")

docker run -d --network todo-network -p 4300:4300 --link mongo-container --rm --name myapi-container myapi-v1

# to launch mongodb container
# docker run -d --network todo-network --network-alias mongo -p 27017:27017 --rm --name mongo-container mongo