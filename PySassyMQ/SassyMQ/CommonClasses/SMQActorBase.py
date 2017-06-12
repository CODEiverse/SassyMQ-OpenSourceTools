import uuid
import pika

import StandardPayload
from BaseClasses import *

class SMQActorBase(Object):

    def __init__(self, allExchange, isAutoConnect):
        super(SMQActorBase, self).__init__()

        self.SenderName = ""
        self.IsConnected = False;
        self.IsAutoConnect = isAutoConnect
        self.AllExchange = allExchange
        if (self.IsAutoConnect): self.AutoConnect()
    

    def AutoConnect(self):
        virtualHost = "SassyMQHost"
        username = ""
        if (username == ""): username = "smqPublic"
        password = ""
        if (password == ""): password = "smqPublic"

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

