#!/bin/bash

set -o allexport
source .env 
set +o allexport

docker build -t "$IMAGE" .

docker rm "$CONTAINER" 
docker run -p "$CONTAINER_PORT_1" --name "$CONTAINER" \
  --env MINIO_ACCESS_KEY="$MINIO_ACCESS_KEY" \
  --env MINIO_SECRET_KEY="$MINIO_SECRET_KEY" \
  --env MINIO_URL="$MINIO_URL" \
  --env SHORTENER_API_KEY="$SHORTENER_API_KEY" \
  --env SHORTENER_API_HOST="$SHORTENER_API_HOST" "$IMAGE"
