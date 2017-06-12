using Newtonsoft.Json;
using RabbitMQ.Client.Events;
using SassyMQ.OSTL.Lib.RabbitMQ;
using SassyMQ.OSTL.Lib.RabbitMQ;
using System;
using System.IO;
using System.Net;
using System.Text.RegularExpressions;

namespace SassyMQ.Lib.RabbitMQ.Payload
{
    public class StandardPayload<T>
        where T : StandardPayload<T>, new()
    {
        public StandardPayload() : this(String.Empty)
        {

        }

        public StandardPayload(string content, string senderId = default(String))
        {
            this.PayloadId = Guid.NewGuid().ToString();
            this.Content = content;
            this.SenderId = senderId;
        }

        public LexiconTerm<LexiconTermEnum> LexiconTerm
        {
            get { return Lexicon.Terms.FromRoutingKey(this.RoutingKey); }
        }

        public string PayloadId { get; set; }
        public string SenderId { get; set; }
        public string SenderName { get; set; }
        public string Content { get; set; }
        public string DeliveryTag { get; set; }
        public string RoutingKey { get; set; }
        public String Exchange { get; set; }
        public String ReplyTo { get; set; }



        public string ToJSonString()
        {
            return JsonConvert.SerializeObject(this);
        }

        public static T FromJSonString(String json)
        {
            return JsonConvert.DeserializeObject<T>(json);
        }


        public bool IsLexiconTerm(LexiconTermEnum termKey)
        {
            LexiconTerm term = Lexicon.Terms[termKey];
            return (this.RoutingKey == term.RoutingKey);

        }
    }
}