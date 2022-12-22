docker build -t iot-api -f ./api-dockerfile ../back
docker build --no-cache -t iot-vue -f ./vue-dockerfile ../front
docker compose -f docker-compose.local.yml up