class LexiconTerm(object):
    def __init__(self, term, routingKey, sender, verb, receiver, category):
        self.Term = term
        self.RoutingKey = routingKey
        self.Sender = sender
        self.Verb = verb
        self.Receiver = receiver
        self.Category = category