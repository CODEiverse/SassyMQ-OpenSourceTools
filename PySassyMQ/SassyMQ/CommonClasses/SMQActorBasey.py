import uuid
import pika

import StandardPayload
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

        self.IsAutoConnect = isAutoConnect
        self.AllExchange = allExchange
        if (self.IsAutoConnect): self.AutoConnect()
    

    def AutoConnect(self):
        virtualHost = "/"
        username = ""
        if (username == ""): username = "guest"
        password = ""
        if (password == ""): password = "guest"

        self.Connect(virtualHost, username, password)

    def callback(self, ch, method, properties, body):
        print ch;
        pass


    def handleConsume(channel, method_frame, header_frame, body):
        print body

    def checkForMessages(self):
        method_frame, header_frame, body = self.RMQChannel.basic_get(self.AllExchange)
        if method_frame:
            self.CheckRouting(method_frame, header_frame, body)
            self.RMQChannel.basic_ack(method_frame.delivery_tag)

    def Connect(self, virtualHost, username, password):
        if (not self.IsConnected):
            self.IsConnected = True
            self.VirtualHost = virtualHost
            self.Username = username
            self.Password = password
            self.RabbitEndpoint = "sassymq.anabstractlevel.com"
            self.SenderId = uuid.uuid4()
            if (self.AllExchange != ""):
                self.AllExchange = self.AllExchange

            credentials = pika.PlainCredentials(self.Username, self.Password)
            self.RMQConnection = pika.BlockingConnection(pika.ConnectionParameters('sassymq.anabstractlevel.com', virtual_host=self.VirtualHost, credentials = credentials))
            self.RMQChannel = self.RMQConnection.channel()
            self.RMQChannel.add_on_return_callback(self.callback)


        return True

        """
                self.RMQFactory = ""; # pika.connection()
                self.RMQFactory.HostName = self.RabbitEndpoint
                self.RMQFactory.VirtualHost = virtualHost
                self.RMQFactory.UserName = username
                self.RMQFactory.Password = password
                self.RMQConnection = self.RMQFactory.CreateConnection()
                self.RMQChannel = self.RMQConnection.CreateModel()

                consumer = EventingBasicConsumer(self.RMQChannel)
                consumer.Received += Consumer_Received
                RMQChannel.BasicConsume("amq.rabbitmq.reply-to", true, consumer)

                self.QueueName = RMQChannel.QueueDeclare().QueueName

                RMQChannel.QueueBind(queue= QueueName, exchange= self.AllExchange, routingKey= "#")

                print(" [*] Waiting for messages. To exit press CTRL+C")

        

        subscription = Subscription(RMQChannel, QueueName)
        count = 0
        while (self.IsConnected):
            bdea = default(BasicDeliverEventArgs)
            gotMessage = subscription.Next(100, bdea)
            if (gotMessage):
                msgText = format("{0}{1}. {2} => {3}{0}", Environment.NewLine, ++count, bdea.Exchange, bdea.RoutingKey)
                #var msgText = string.Format("{3}.  {0}: {1} -> '{2}'",
                #bdea.Exchange, bdea.RoutingKey,
                #Encoding.UTF8.GetString(bdea.Body), ++count);

                isPrint = SMQActorBase.IsDebugMode

                isPrint = isPrint and (not bdea.IsPing() or SMQActorBase.ShowPings)

                if (isPrint): print(msgText)

                payload = StandardPayload.FromJSonString(Encoding.UTF8.GetString(bdea.Body))

                payload.DeliveryTag = bdea.DeliveryTag.SafeToString()
                payload.RoutingKey = bdea.RoutingKey
                payload.ReplyTo = bdea.BasicProperties.ReplyTo
                payload.Exchange = bdea.Exchange

                self.OnMessageReceived(payload)

                self.CheckRouting(payload)
                self.OnAfterMessageReceived(payload)

        self.RMQChannel.Close()

        self.RMQConnection.Close()
        """
 

    def OnMessageReceived(self, payload):
        plea = PayloadEventArgs(payload)
        self.Invoke(self.MessageReceived, plea)
    
    def OnAfterMessageReceived(self, payload):
        plea = PayloadEventArgs(payload)
        self.Invoke(self.AfterMessageReceived, plea)
    

    def CheckRouting(self, payload):
        pass

    def Consumer_Received(self, sender, e):
        body = Encoding.UTF8.GetString(e.Body)
        payload = StandardPayload.FromJSonString(body)
        self.OnReplyTo(payload)


    def OnReplyTo(self, payload):
        plea = PayloadEventArgs(payload)
        self.Invoke(self.ReplyTo, plea)
    

    def Disconnect(self):
        self.IsConnected = false


    def CreatePayload(self):
        payload = StandardPayload.StandardPayload("")
        payload.SenderId = self.SenderId
        payload.SenderName = self.SenderName
        payload.PayloadId = uuid.uuid4()
        return payload

