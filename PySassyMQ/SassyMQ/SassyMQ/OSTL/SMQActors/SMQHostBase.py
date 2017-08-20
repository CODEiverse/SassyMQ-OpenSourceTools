﻿from SassyMQ.OSTL.Lexicon import *
from SassyMQ.OSTL.CommonClasses.BaseClasses import *
from SassyMQ.OSTL.CommonClasses.SMQActorBase import *
from json import JSONEncoder
import sys
import traceback

class SMQHostBase(SMQActorBase):
     
    def __init__(self, isAutoConnect = True):
        super(SMQHostBase, self).__init__("host.all", isAutoConnect)
        
        self.PublicCreateSMQProjectObservers = []
        
        self.OwnerTakeOwnershipObservers = []
        
        self.OwnerSetSourceObservers = []
        
        self.OwnerGetSDKObservers = []
        
        self.OwnerRebuildSDKObservers = []
        
        self.OwnerGetFilesObservers = []
        
        self.OwnerGetFileObservers = []
        
        self.OwnerGetModuleInitFileSetObservers = []
        
        # And can also say/hear everything which : Owner hears.
            
        # And can also say/hear everything which : Public hears.
            

    # "OpenSourceTools" - OSTL
    def CheckRouting(self, message_frame, header_frame, body):
        payload = self.PayloadFromMsg(message_frame, header_frame, body)
        print("CHECKING: " + message_frame.routing_key)

    
        if message_frame.routing_key == "host.general.public.createsmqproject":
            if self.IsDebugMode:
                print("Create S M Q Project -  ")
                print("payload: " + message_frame)

            try:
                for callbackMethod in self.PublicCreateSMQProjectObservers:
                    payload.IsHandled = True
                    callbackMethod(payload)
            except:
                payload.ErrorMessage = traceback.format_exc(True)
                print(payload.ErrorMessage)

            payload.sendReply()

    
        elif message_frame.routing_key == "host.general.owner.takeownership":
            if self.IsDebugMode:
                print("Take Ownership -  ")
                print("payload: " + message_frame)

            try:
                for callbackMethod in self.OwnerTakeOwnershipObservers:
                    payload.IsHandled = True
                    callbackMethod(payload)
            except:
                payload.ErrorMessage = traceback.format_exc(True)
                print(payload.ErrorMessage)

            payload.sendReply()

    
        elif message_frame.routing_key == "host.general.owner.setsource":
            if self.IsDebugMode:
                print("Set Source -  ")
                print("payload: " + message_frame)

            try:
                for callbackMethod in self.OwnerSetSourceObservers:
                    payload.IsHandled = True
                    callbackMethod(payload)
            except:
                payload.ErrorMessage = traceback.format_exc(True)
                print(payload.ErrorMessage)

            payload.sendReply()

    
        elif message_frame.routing_key == "host.general.owner.getsdk":
            if self.IsDebugMode:
                print("Get S D K -  ")
                print("payload: " + message_frame)

            try:
                for callbackMethod in self.OwnerGetSDKObservers:
                    payload.IsHandled = True
                    callbackMethod(payload)
            except:
                payload.ErrorMessage = traceback.format_exc(True)
                print(payload.ErrorMessage)

            payload.sendReply()

    
        elif message_frame.routing_key == "host.general.owner.rebuildsdk":
            if self.IsDebugMode:
                print("Rebuild S D K -  ")
                print("payload: " + message_frame)

            try:
                for callbackMethod in self.OwnerRebuildSDKObservers:
                    payload.IsHandled = True
                    callbackMethod(payload)
            except:
                payload.ErrorMessage = traceback.format_exc(True)
                print(payload.ErrorMessage)

            payload.sendReply()

    
        elif message_frame.routing_key == "host.general.owner.getfiles":
            if self.IsDebugMode:
                print("Get Files -  ")
                print("payload: " + message_frame)

            try:
                for callbackMethod in self.OwnerGetFilesObservers:
                    payload.IsHandled = True
                    callbackMethod(payload)
            except:
                payload.ErrorMessage = traceback.format_exc(True)
                print(payload.ErrorMessage)

            payload.sendReply()

    
        elif message_frame.routing_key == "host.general.owner.getfile":
            if self.IsDebugMode:
                print("Get File -  ")
                print("payload: " + message_frame)

            try:
                for callbackMethod in self.OwnerGetFileObservers:
                    payload.IsHandled = True
                    callbackMethod(payload)
            except:
                payload.ErrorMessage = traceback.format_exc(True)
                print(payload.ErrorMessage)

            payload.sendReply()

    
        elif message_frame.routing_key == "host.general.owner.getmoduleinitfileset":
            if self.IsDebugMode:
                print("Get Module Init File Set -  ")
                print("payload: " + message_frame)

            try:
                for callbackMethod in self.OwnerGetModuleInitFileSetObservers:
                    payload.IsHandled = True
                    callbackMethod(payload)
            except:
                payload.ErrorMessage = traceback.format_exc(True)
                print(payload.ErrorMessage)

            payload.sendReply()

    
            # And can also hear everything which : Owner hears.
            
            # And can also hear everything which : Public hears.
            


    # ACTOR CAN SAY:
    
    def OnPublicCreateSMQProjectReceived(self, callback):
        self.PublicCreateSMQProjectObservers.append(callback)
    
    def OnOwnerTakeOwnershipReceived(self, callback):
        self.OwnerTakeOwnershipObservers.append(callback)
    
    def OnOwnerSetSourceReceived(self, callback):
        self.OwnerSetSourceObservers.append(callback)
    
    def OnOwnerGetSDKReceived(self, callback):
        self.OwnerGetSDKObservers.append(callback)
    
    def OnOwnerRebuildSDKReceived(self, callback):
        self.OwnerRebuildSDKObservers.append(callback)
    
    def OnOwnerGetFilesReceived(self, callback):
        self.OwnerGetFilesObservers.append(callback)
    
    def OnOwnerGetFileReceived(self, callback):
        self.OwnerGetFileObservers.append(callback)
    
    def OnOwnerGetModuleInitFileSetReceived(self, callback):
        self.OwnerGetModuleInitFileSetObservers.append(callback)
    
        # And can also say/hear everything which : Owner hears.
        
    def OwnerTakeOwnershipNoPayload(self):
        self.OwnerTakeOwnership(self.CreatePayload())
        

    def OwnerTakeOwnershipString(self, content):
        payload = self.CreatePayload()
        payload.Content = content
        self.OwnerTakeOwnership(payload)
        

    def OwnerTakeOwnership(self, payload):
        if self.IsDebugMode:
            print("Take Ownership - ")
            print("payload: " + payload)
        
        term = payload.LexiconTerm = Lexicon.TermsByRoutingKey['host.general.owner.takeownership'];
        self.RMQChannel.basic_publish(exchange=term.Sender + 'mic', routing_key=term.RoutingKey,  body=payload.toJSON())            

    
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
        
        term = payload.LexiconTerm = Lexicon.TermsByRoutingKey['host.general.owner.setsource'];
        self.RMQChannel.basic_publish(exchange=term.Sender + 'mic', routing_key=term.RoutingKey,  body=payload.toJSON())            

    
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
        
        term = payload.LexiconTerm = Lexicon.TermsByRoutingKey['host.general.owner.getsdk'];
        self.RMQChannel.basic_publish(exchange=term.Sender + 'mic', routing_key=term.RoutingKey,  body=payload.toJSON())            

    
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
        
        term = payload.LexiconTerm = Lexicon.TermsByRoutingKey['host.general.owner.rebuildsdk'];
        self.RMQChannel.basic_publish(exchange=term.Sender + 'mic', routing_key=term.RoutingKey,  body=payload.toJSON())            

    
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
        
        term = payload.LexiconTerm = Lexicon.TermsByRoutingKey['host.general.owner.getfiles'];
        self.RMQChannel.basic_publish(exchange=term.Sender + 'mic', routing_key=term.RoutingKey,  body=payload.toJSON())            

    
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
        
        term = payload.LexiconTerm = Lexicon.TermsByRoutingKey['host.general.owner.getfile'];
        self.RMQChannel.basic_publish(exchange=term.Sender + 'mic', routing_key=term.RoutingKey,  body=payload.toJSON())            

    
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
        
        term = payload.LexiconTerm = Lexicon.TermsByRoutingKey['host.general.owner.getmoduleinitfileset'];
        self.RMQChannel.basic_publish(exchange=term.Sender + 'mic', routing_key=term.RoutingKey,  body=payload.toJSON())            

    
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
        
        term = payload.LexiconTerm = Lexicon.TermsByRoutingKey['host.general.public.createsmqproject'];
        self.RMQChannel.basic_publish(exchange=term.Sender + 'mic', routing_key=term.RoutingKey,  body=payload.toJSON())            

    

                    