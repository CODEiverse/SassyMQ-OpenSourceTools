using System;

using System.Linq;
using System.Collections.Generic;
using System.Collections;
using SassyMQ.Lib.RabbitMQ;

namespace SassyMQ.OSTL.Lib.RabbitMQ
{
    public class LexiconTerm : LexiconTerm<LexiconTermEnum> { }

    public partial class Lexicon  : IEnumerable<LexiconTerm>
    {
        public static Lexicon Terms = new Lexicon();
        protected static new Dictionary<LexiconTermEnum, LexiconTerm> TermsByKey { get; set; }

        public LexiconTerm this[LexiconTermEnum termKey]
        {
            get { return TermsByKey[termKey]; }
        }

        public LexiconTerm FromRoutingKey(string routingKey)
        {
            return Lexicon.TermsByKey.Values.FirstOrDefault(first => first.RoutingKey == routingKey);
        }


        public IEnumerator<LexiconTerm> GetEnumerator()
        {
            return Lexicon.TermsByKey.Values.GetEnumerator();
        }

        
        IEnumerator IEnumerable.GetEnumerator()
        {
            return Lexicon.TermsByKey.Values.GetEnumerator();
        }

        static Lexicon()
        {
            Lexicon.TermsByKey = new Dictionary<LexiconTermEnum, LexiconTerm>();
            
            Lexicon.TermsByKey[LexiconTermEnum.public_createsmqproject_host] = new LexiconTerm() {
                Term = LexiconTermEnum.public_createsmqproject_host,
                Sender = "public",
                Verb = "createsmqproject",
                Receiver = "host",
                Category = "general"
            };
            
            Lexicon.TermsByKey[LexiconTermEnum.owner_takeownership_host] = new LexiconTerm() {
                Term = LexiconTermEnum.owner_takeownership_host,
                Sender = "owner",
                Verb = "takeownership",
                Receiver = "host",
                Category = "general"
            };
            
            Lexicon.TermsByKey[LexiconTermEnum.owner_setsource_host] = new LexiconTerm() {
                Term = LexiconTermEnum.owner_setsource_host,
                Sender = "owner",
                Verb = "setsource",
                Receiver = "host",
                Category = "general"
            };
            
            Lexicon.TermsByKey[LexiconTermEnum.owner_getsdk_host] = new LexiconTerm() {
                Term = LexiconTermEnum.owner_getsdk_host,
                Sender = "owner",
                Verb = "getsdk",
                Receiver = "host",
                Category = "general"
            };
            
            Lexicon.TermsByKey[LexiconTermEnum.owner_rebuildsdk_host] = new LexiconTerm() {
                Term = LexiconTermEnum.owner_rebuildsdk_host,
                Sender = "owner",
                Verb = "rebuildsdk",
                Receiver = "host",
                Category = "general"
            };
            
            Lexicon.TermsByKey[LexiconTermEnum.owner_getfiles_host] = new LexiconTerm() {
                Term = LexiconTermEnum.owner_getfiles_host,
                Sender = "owner",
                Verb = "getfiles",
                Receiver = "host",
                Category = "general"
            };
            
            Lexicon.TermsByKey[LexiconTermEnum.owner_getfile_host] = new LexiconTerm() {
                Term = LexiconTermEnum.owner_getfile_host,
                Sender = "owner",
                Verb = "getfile",
                Receiver = "host",
                Category = "general"
            };
            
            Lexicon.TermsByKey[LexiconTermEnum.owner_getmoduleinitfileset_host] = new LexiconTerm() {
                Term = LexiconTermEnum.owner_getmoduleinitfileset_host,
                Sender = "owner",
                Verb = "getmoduleinitfileset",
                Receiver = "host",
                Category = "general"
            };
            
        }
    }
}