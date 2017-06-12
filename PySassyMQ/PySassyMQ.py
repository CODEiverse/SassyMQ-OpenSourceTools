from SassyMQ.SMQActors.SMQOwner import *
from SassyMQ.SMQActors.SMQHost import *

import sys


ownerProxy = SMQOwner()

payload = ownerProxy.CreatePayload()
payload.Uri = sys.argv[0]
print payload.Uri



ownerProxy.OwnerTakeOwnership(payload)


while 1:
    ownerProxy.checkForMessages();