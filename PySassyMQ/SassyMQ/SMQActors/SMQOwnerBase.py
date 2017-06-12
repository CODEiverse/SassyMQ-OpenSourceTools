
from SassyMQ.CommonClasses.BaseClasses import *
from SassyMQ.CommonClasses.SMQActorBase import *
from json import JSONEncoder


class SMQOwnerBase(SMQActorBase):
     
    def __init__(self, isAutoConnect = True):
        super(SMQOwnerBase, self).__init__("owner.all", isAutoConnect)
        self.IsDebugMode = True;





      

    # OpenSourceToolsLexicon - OSTL
    def CheckRouting(self, message_frame, header_frame, body):
        pass
    
            # And can also hear everything which : Public hears.
            


    # ACTOR CAN SAY:
    
    def OwnerTakeOwnershipNoPayload(self):
        self.OwnerTakeOwnership(self.CreatePayload())
        

    def OwnerTakeOwnershipString(self, content):
        payload = self.CreatePayload()
        payload.Content = content
        self.OwnerTakeOwnership(payload)
        

    def OwnerTakeOwnership(self, payload):
        if self.IsDebugMode:
            print("Take Ownership - ")
            print("payload: " + payload.toJSON())
            
        self.RMQChannel.basic_publish(exchange='ownermic', routing_key='host.general.owner.takeownership',  body=payload.toJSON())
    
    def OwnerSetSourceNoPayload(self):
        self.OwnerSetSource(self.CreatePayload())
        

    def OwnerSetSourceString(self, content):
        payload = self.CreatePayload()
        payload.Content = content
        self.OwnerSetSource(payload)
        

    def OwnerSetSource(self, payload):
        if self.IsDebugMode:
            print("Set Source - ")
            print("payload: " + payload)
            
        self.RMQChannel.basic_publish(exchange='ownermic', routing_key='host.general.owner.setsource',  body=payload.toJSON())
    
    def OwnerGetSDKNoPayload(self):
        self.OwnerGetSDK(self.CreatePayload())
        

    def OwnerGetSDKString(self, content):
        payload = self.CreatePayload()
        payload.Content = content
        self.OwnerGetSDK(payload)
        

    def OwnerGetSDK(self, payload):
        if self.IsDebugMode:
            print("Get S D K - ")
            print("payload: " + payload)
            
        self.RMQChannel.basic_publish(exchange='ownermic', routing_key='host.general.owner.getsdk',  body=payload.toJSON())
    
    def OwnerRebuildSDKNoPayload(self):
        self.OwnerRebuildSDK(self.CreatePayload())
        

    def OwnerRebuildSDKString(self, content):
        payload = self.CreatePayload()
        payload.Content = content
        self.OwnerRebuildSDK(payload)
        

    def OwnerRebuildSDK(self, payload):
        if self.IsDebugMode:
            print("Rebuild S D K - ")
            print("payload: " + payload)
            
        self.RMQChannel.basic_publish(exchange='ownermic', routing_key='host.general.owner.rebuildsdk',  body=payload.toJSON())
    
    def OwnerGetFilesNoPayload(self):
        self.OwnerGetFiles(self.CreatePayload())
        

    def OwnerGetFilesString(self, content):
        payload = self.CreatePayload()
        payload.Content = content
        self.OwnerGetFiles(payload)
        

    def OwnerGetFiles(self, payload):
        if self.IsDebugMode:
            print("Get Files - ")
            print("payload: " + payload)
            
        self.RMQChannel.basic_publish(exchange='ownermic', routing_key='host.general.owner.getfiles',  body=payload.toJSON())
    
    def OwnerGetFileNoPayload(self):
        self.OwnerGetFile(self.CreatePayload())
        

    def OwnerGetFileString(self, content):
        payload = self.CreatePayload()
        payload.Content = content
        self.OwnerGetFile(payload)
        

    def OwnerGetFile(self, payload):
        if self.IsDebugMode:
            print("Get File - ")
            print("payload: " + payload)
            
        self.RMQChannel.basic_publish(exchange='ownermic', routing_key='host.general.owner.getfile',  body=payload.toJSON())
    
    def OwnerGetModuleInitFileSetNoPayload(self):
        self.OwnerGetModuleInitFileSet(self.CreatePayload())
        

    def OwnerGetModuleInitFileSetString(self, content):
        payload = self.CreatePayload()
        payload.Content = content
        self.OwnerGetModuleInitFileSet(payload)
        

    def OwnerGetModuleInitFileSet(self, payload):
        if self.IsDebugMode:
            print("Get Module Init File Set - ")
            print("payload: " + payload)
            
        self.RMQChannel.basic_publish(exchange='ownermic', routing_key='host.general.owner.getmoduleinitfileset',  body=payload.toJSON())
    
        # And can also say/hear everything which : Public hears.
        
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
    

                    