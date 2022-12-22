import json
import random
import time
import uuid
from datetime import datetime

import pika


class MessagesProducer:

    def __init__(self):
        credentials = pika.PlainCredentials("dotnetowiec", "kocham.Net!")
        self.__connection = \
            pika.BlockingConnection(pika.ConnectionParameters('localhost', 5672, "/", credentials))
        self.__channel = self.connection.channel()

    def __del__(self):
        self.__connection.close()

    @property
    def connection(self):
        return self.__connection

    @property
    def channel(self):
        return self.__channel

    def _to_csv(self, data_dict: dict):
        return data_dict["datetime"] + ";" + str(data_dict["value"]) + ";" + data_dict["sensorId"] + ";" + data_dict[
            "type"] + ";" + data_dict["unit"]

    def _to_json(self, data_dict: dict):
        return json.dumps(data_dict, indent=4)

    def _send_measurement(self, data_dict: dict):
        # json_obj = self._to_json(data_dict)
        self.channel.basic_publish(exchange='SensorsExchange', routing_key='',
                                   body=bytes(self._to_csv(data_dict), 'utf-8'))

    def _gen_measurment(self, sensor_id: str, sensor_type: str):

        measurment = {
            'type': sensor_type,
            'sensorId': sensor_id,
            'datetime': datetime.now().isoformat()
        }

        if measurment['type'] == "temperature":
            range_c = [-10.0, 50.0]
            measurment['unit'] = "C"
            measurment['value'] = round(random.uniform(range_c[0], range_c[1]), 2)
        elif measurment['type'] == "humidity":
            measurment['unit'] = '%'
            measurment['value'] = round(random.uniform(0.0, 100.0), 2)
        elif measurment['type'] == "brightness":
            measurment['unit'] = 'lm'
            measurment['value'] = round(random.uniform(400.0, 1000.0), 2)
        elif measurment['type'] == "pressure":
            measurment['unit'] = 'hPa'
            measurment['value'] = round(random.uniform(970.0, 1030.0), 2)
        else:
            raise ValueError("Type not allowed")

        return measurment

    def _gen_sensor(self, sensor_type: str):
        sensor = {
            'type': sensor_type,
            'Id': str(uuid.uuid1())
        }
        return sensor

    # generate sensors, measurments data, serialize it to json
    # and send it to rabbitmq
    def _produce_data(self, sensors_num: int = 30, delay: float = 1):
        sensor_types = ['temperature', 'humidity', 'brightness', 'pressure']

        sensors = []
        for i in range(sensors_num):
            sensors.append(self._gen_sensor(random.choice(sensor_types)))

        while True:
            for sensor in sensors:
                measurement = self._gen_measurment(sensor["Id"], sensor["type"])
                self._send_measurement(measurement)
                print("sent " + str(measurement))
            time.sleep(delay)


producer = MessagesProducer()
producer._produce_data(sensors_num=3)
