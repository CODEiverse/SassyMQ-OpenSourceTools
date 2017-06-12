
from SassyMQ.CommonClasses.BaseClasses import *
from SassyMQ.CommonClasses.SMQActorBase import *
from json import JSONEncoder


class SMQPublicBase(SMQActorBase):
     
    def __init__(self, isAutoConnect = True):
        super(SMQPublicBase, self).__init__("public.all", isAutoConnect)
        





      

    # OpenSourceToolsLexicon - OSTL
    def CheckRouting(self, message_frame, header_frame, body):
        pass


    # ACTOR CAN SAY:
    
    def PublicCreateSMQProjectNoPayload(self):
        self.PublicCreateSMQProject(self.CreatePayload())
        

    def PublicCreateSMQProjectString(self, content):
        payload = self.CreatePayload()
        payload.Content = content
        self.PublicCreateSMQProject(payload)
        

    def PublicCreateSMQProject(self, payload):
        if self.IsDebugMode:
            print("Create S M Q Project - ")
            print("payload: " + payload)
            
        self.RMQChannel.basic_publish(exchange='publicmic', routing_key='host.general.public.createsmqproject',  body=payload.toJSON())
    

                    