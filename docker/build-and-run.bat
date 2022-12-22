docker build -t iot-api -f ./api-dockerfile ../back
docker compose -f docker-compose.local.yml up
