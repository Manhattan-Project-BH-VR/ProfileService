# To run RabbitMQ docker container
To be able to use events, please use next command before launch:

docker run -d --hostname my-rabbit --name some-rabbit -p 5672:5672 -p 15672:15672 rabbitmq:3-management
