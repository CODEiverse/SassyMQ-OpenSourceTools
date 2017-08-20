import uuid
from BaseClasses import *

class StandardPayload(Object):
    def __init__(self, content, senderId=""):
        self.Content = content
        self.SenderName = ""
        self.PayloadId = ""
        self.Content = ""
        self.DeliveryTag = ""
        self.RoutingKey = ""
        self.Exchange = ""
        self.ReplyTo = ""
        self.Actor = None

    
    def sendReply(self):
        if self.ReplyTo != None:
            responsePayload = self.toJSON()
            self.Actor.RMQChannel.basic_publish("", self.ReplyTo, responsePayload);
            return True
        else: return False
