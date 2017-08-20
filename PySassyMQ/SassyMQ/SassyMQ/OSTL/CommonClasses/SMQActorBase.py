import uuid
import pika

import StandardPayload
import config
from BaseClasses import *

class SMQActorBase(Object):

    def __init__(self, allExchange, isAutoConnect):
        super(SMQActorBase, self).__init__()
        self.RMQChannel = ""
        self.RMQConnection = ""
        self.RMQFactory = ""
        self.IsConnected = ""
        self.MonitorTask = ""
        self.QueueName = ""

        self.Password = ""
        self.RabbitEndpoint = ""
        self.Username = ""
        self.VirtualHost = ""
        self.SenderId = ""
        self.SenderName = ""
        self.AllExchange = ""
        self.IsAutoConnect = ""
        self.IsDebugMode = ""
        self.ShowPings = ""

        self.CustomizePayloadObservers = []


        self.IsAutoConnect = isAutoConnect
        self.AllExchange = allExchange
        if (self.IsAutoConnect): self.AutoConnect()
    

    def AutoConnect(self):
        virtualHost = config.virtualHost
        username = ""
        if (username == ""): username = config.username
        password = ""
        if (password == ""): password = config.password

        self.Connect(virtualHost, username, password)

    def callback(self, ch, method, properties, body):
        print ch;
        pass

    def onCustomizePayload(self, callback):
        self.CustomizePayloadObservers.append(callback)


    def handleConsume(channel, method_frame, header_frame, body):
        print body

    def callback(self, ch, method, props, body):
        self.CheckRouting(method, props, body)
        return True

    def Connect(self, virtualHost, username, password):
        if (not self.IsConnected):
            self.IsConnected = True
            self.VirtualHost = virtualHost
            self.Username = username
            self.Password = password
            self.RabbitEndpoint = config.rabbitmqServer
            self.SenderId = uuid.uuid4()
            if (self.AllExchange != ""):
                self.AllExchange = self.AllExchange

            credentials = pika.PlainCredentials(self.Username, self.Password)
            self.RMQConnection = pika.BlockingConnection(pika.ConnectionParameters(config.rabbitmqServer, virtual_host=self.VirtualHost, credentials = credentials))
            self.RMQChannel = self.RMQConnection.channel()
            self.RMQChannel.add_on_return_callback(self.callback)

            self.Result = self.RMQChannel.queue_declare(exclusive=True, durable = True, auto_delete=True);
            self.QueueName = self.Result.method.queue;
            self.RMQChannel.queue_bind(self.QueueName, self.AllExchange, '#')
            self.RMQChannel.basic_consume(self.callback, self.QueueName);

        return True

 
    def CheckRouting(self, payload):
        pass

    def Consumer_Received(self, sender, e):
        body = Encoding.UTF8.GetString(e.Body)
        payload = StandardPayload.FromJSonString(body)
        self.OnReplyTo(payload)



    def Disconnect(self):
        self.IsConnected = false


    def CreatePayload(self):
        payload = StandardPayload.StandardPayload("")
        payload.SenderId = self.SenderId
        payload.SenderName = self.SenderName
        payload.PayloadId = uuid.uuid4()
        return payload


    def PayloadFromMsg(self, method, props, body):
        if isinstance(body, bytes): body = bytes.decode(body)
        bodyDict = json.loads(body)
        payload = self.CreatePayload()

        for attrib in bodyDict: 
            if bodyDict[attrib] != None and bodyDict[attrib] != "": setattr(payload, attrib, bodyDict[attrib])

        payload.SenderId = self.SenderId;
        payload.SenderCreatedAt = self.CreatedAt;

        payload.ReplyTo = props.reply_to if props != None else None
        payload.RoutingKey = method.routing_key if method != None else None

        for customizePayloadCallback in self.CustomizePayloadObservers:
            customizePayloadCallback(bodyDict, payload);

        return payload


