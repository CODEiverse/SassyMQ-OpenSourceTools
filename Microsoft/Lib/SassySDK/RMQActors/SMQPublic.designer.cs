
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
        // OpenSourceTools - OSTL
        public virtual bool Connect(string virtualHost, string username, string password)
        {
            return base.Connect(virtualHost, username, password);
        }   

        protected override void CheckRouting(OSTLPayload payload) 
        {
            this.CheckRouting(payload, false);
        }

        partial void CheckPayload(OSTLPayload payload);

        private void Reply(OSTLPayload payload)
        {
            if (!System.String.IsNullOrEmpty(payload.ReplyTo))
            {
                payload.DirectMessageQueue = this.QueueName;
                this.CheckPayload(payload);
                this.RMQChannel.BasicPublish("", payload.ReplyTo, body: Encoding.UTF8.GetBytes(payload.ToJSonString()));
            }
        }

        protected override void CheckRouting(OSTLPayload payload, bool isDirectMessage) 
        {
            // if (payload.IsDirectMessage && !isDirectMessage) return;

            
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

 
        
    }
}

                    