import uuid
from BaseClasses import *

class StandardPayload(Object):
    def __init__(self, content = "", senderId=""):
        self.PayloadId = uuid.uuid4()
        self.Content = content
        self.SenderId = senderId
        self.PayloadId = ""
        self.SenderId = ""
        self.SenderName = ""
        self.Content = ""
        self.DeliveryTag = ""
        self.RoutingKey = ""
        self.Exchange = ""
        self.ReplyTo = ""
        self.GID = ""
        self.GoogleKey = ""
        self.IsRejected = ""
        self.PrivateKey = ""
        self.ProjectHash = ""
        self.ProjectName = ""
        self.RejectionMsg = ""
        self.ResolutionMsg = ""
    

    def LexiconTerm(self):
        pass #return Lexicon.Terms.FromRoutingKey(this.RoutingKey); }
    

    def RootDir(self):
        return format("C:\Development\SassyMQ\SDKs\SMQ_{}", this.ProjectHash)
    

    def SeedPath(self):
        return Path.Combine(this.RootDir, String.Format("{0}.txt", this.PrivateKey.SafeToString().HashPrivateKey()))
    

    def Reject(self, msg):
        self.IsRejected = true
        self.RejectionMsg = msg
    

    def Resolve(msg):
        this.IsRejected = false
        this.ResolutionMsg = msg
        this.PrivateKey = Guid.NewGuid().GuidToKey()

 