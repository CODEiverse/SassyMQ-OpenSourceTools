
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using RabbitMQ.Client.MessagePatterns;
using SassyMQ.Lib.RabbitMQ;
using SassyMQ.OSTL.Lib.RabbitMQ;
using SassyMQ.Lib.RabbitMQ.Payload;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SassyMQ.OSTL.Lib.RMQActors
{
    public partial class SMQPublic : SMQActorBase<OSTLPayload>
    {
     
        public SMQPublic(bool isAutoConnect = true)
            : base("public.all", isAutoConnect)
        {
        }
        // OpenSourceToolsLexicon - OSTL
        public virtual bool Connect(string virtualHost, string username, string password)
        {
            return base.Connect(virtualHost, username, password);
        }   

        protected override void CheckRouting(OSTLPayload payload) {
            
        }

        
            public void PublicCreateSMQProject() {
                this.PublicCreateSMQProject(this.CreatePayload());
            }

            public void PublicCreateSMQProject(System.String content) {
                var payload = this.CreatePayload();
                payload.Content = content;
                this.PublicCreateSMQProject(payload);
            }

            public void PublicCreateSMQProject(OSTLPayload payload)
            {
                this.SendMessage(payload, "Create S M Q Project - ",
                        "publicmic", "host.general.public.createsmqproject");
             }

 
        

            private void SendMessage(OSTLPayload payload, string description, string mic, string routingKey)
            {
                if (IsDebugMode)
                {
                    System.Console.WriteLine(description);
                    System.Console.WriteLine("payload: " + payload.SafeToString());
                }

                IBasicProperties props = this.RMQChannel.CreateBasicProperties();
                props.ReplyTo = "amq.rabbitmq.reply-to";
                this.RMQChannel.BasicPublish(mic, routingKey, props, Encoding.UTF8.GetBytes(payload.ToJSonString()));
            }

     
    }
}

                    