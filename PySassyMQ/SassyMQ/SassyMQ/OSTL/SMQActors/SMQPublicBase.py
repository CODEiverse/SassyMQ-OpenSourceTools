from SassyMQ.OSTL.Lexicon import *
from SassyMQ.OSTL.CommonClasses.BaseClasses import *
from SassyMQ.OSTL.CommonClasses.SMQActorBase import *
from json import JSONEncoder
import sys
import traceback

class SMQPublicBase(SMQActorBase):
     
    def __init__(self, isAutoConnect = True):
        super(SMQPublicBase, self).__init__("public.all", isAutoConnect)
        

    # "OpenSourceTools" - OSTL
    def CheckRouting(self, message_frame, header_frame, body):
        payload = self.PayloadFromMsg(message_frame, header_frame, body)
        print("CHECKING: " + message_frame.routing_key)

    


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
        
        term = payload.LexiconTerm = Lexicon.TermsByRoutingKey['host.general.public.createsmqproject'];
        self.RMQChannel.basic_publish(exchange=term.Sender + 'mic', routing_key=term.RoutingKey,  body=payload.toJSON())            

    

                    