from SassyMQ.OSTL.CommonClasses.LexiconTerm import *

class LexiconBase(object):
    Terms = []
    TermsByRoutingKey = {}
    TermsByVerb = {}

    @staticmethod
    def pushTerm(term):
        LexiconBase.Terms.append(term)
        LexiconBase.TermsByRoutingKey[term.RoutingKey] = term
        LexiconBase.TermsByVerb[term.Verb] = term


LexiconBase.pushTerm(LexiconTerm(term = "public_createsmqproject_host",
                                 routingKey = "host.general.public.createsmqproject",
                                 sender = "public",
                                 verb = "createsmqproject",
                                 receiver = "host",
                                 category = "general"))

LexiconBase.pushTerm(LexiconTerm(term = "owner_takeownership_host",
                                 routingKey = "host.general.owner.takeownership",
                                 sender = "owner",
                                 verb = "takeownership",
                                 receiver = "host",
                                 category = "general"))

LexiconBase.pushTerm(LexiconTerm(term = "owner_setsource_host",
                                 routingKey = "host.general.owner.setsource",
                                 sender = "owner",
                                 verb = "setsource",
                                 receiver = "host",
                                 category = "general"))

LexiconBase.pushTerm(LexiconTerm(term = "owner_getsdk_host",
                                 routingKey = "host.general.owner.getsdk",
                                 sender = "owner",
                                 verb = "getsdk",
                                 receiver = "host",
                                 category = "general"))

LexiconBase.pushTerm(LexiconTerm(term = "owner_rebuildsdk_host",
                                 routingKey = "host.general.owner.rebuildsdk",
                                 sender = "owner",
                                 verb = "rebuildsdk",
                                 receiver = "host",
                                 category = "general"))

LexiconBase.pushTerm(LexiconTerm(term = "owner_getfiles_host",
                                 routingKey = "host.general.owner.getfiles",
                                 sender = "owner",
                                 verb = "getfiles",
                                 receiver = "host",
                                 category = "general"))

LexiconBase.pushTerm(LexiconTerm(term = "owner_getfile_host",
                                 routingKey = "host.general.owner.getfile",
                                 sender = "owner",
                                 verb = "getfile",
                                 receiver = "host",
                                 category = "general"))

LexiconBase.pushTerm(LexiconTerm(term = "owner_getmoduleinitfileset_host",
                                 routingKey = "host.general.owner.getmoduleinitfileset",
                                 sender = "owner",
                                 verb = "getmoduleinitfileset",
                                 receiver = "host",
                                 category = "general"))



