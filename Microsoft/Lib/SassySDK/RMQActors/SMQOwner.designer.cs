
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
    public partial class SMQOwner : SMQActorBase<OSTLPayload>
    {
     
        public SMQOwner(bool isAutoConnect = true)
            : base("owner.all", isAutoConnect)
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

            
            // And can also hear everything which : Public hears.
            
        }

        
            public void OwnerTakeOwnership() {
                this.OwnerTakeOwnership(this.CreatePayload());
            }

            public void OwnerTakeOwnership(System.String content) {
                var payload = this.CreatePayload();
                payload.Content = content;
                this.OwnerTakeOwnership(payload);
            }

            public void OwnerTakeOwnership(OSTLPayload payload)
            {
                
                this.SendMessage(payload, "Take Ownership - ",
                        "ownermic", "host.general.owner.takeownership");
             }

 
        
            public void OwnerSetSource() {
                this.OwnerSetSource(this.CreatePayload());
            }

            public void OwnerSetSource(System.String content) {
                var payload = this.CreatePayload();
                payload.Content = content;
                this.OwnerSetSource(payload);
            }

            public void OwnerSetSource(OSTLPayload payload)
            {
                
                this.SendMessage(payload, "Set Source - ",
                        "ownermic", "host.general.owner.setsource");
             }

 
        
            public void OwnerGetSDK() {
                this.OwnerGetSDK(this.CreatePayload());
            }

            public void OwnerGetSDK(System.String content) {
                var payload = this.CreatePayload();
                payload.Content = content;
                this.OwnerGetSDK(payload);
            }

            public void OwnerGetSDK(OSTLPayload payload)
            {
                
                this.SendMessage(payload, "Get S D K - ",
                        "ownermic", "host.general.owner.getsdk");
             }

 
        
            public void OwnerRebuildSDK() {
                this.OwnerRebuildSDK(this.CreatePayload());
            }

            public void OwnerRebuildSDK(System.String content) {
                var payload = this.CreatePayload();
                payload.Content = content;
                this.OwnerRebuildSDK(payload);
            }

            public void OwnerRebuildSDK(OSTLPayload payload)
            {
                
                this.SendMessage(payload, "Rebuild S D K - ",
                        "ownermic", "host.general.owner.rebuildsdk");
             }

 
        
            public void OwnerGetFiles() {
                this.OwnerGetFiles(this.CreatePayload());
            }

            public void OwnerGetFiles(System.String content) {
                var payload = this.CreatePayload();
                payload.Content = content;
                this.OwnerGetFiles(payload);
            }

            public void OwnerGetFiles(OSTLPayload payload)
            {
                
                this.SendMessage(payload, "Get Files - ",
                        "ownermic", "host.general.owner.getfiles");
             }

 
        
            public void OwnerGetFile() {
                this.OwnerGetFile(this.CreatePayload());
            }

            public void OwnerGetFile(System.String content) {
                var payload = this.CreatePayload();
                payload.Content = content;
                this.OwnerGetFile(payload);
            }

            public void OwnerGetFile(OSTLPayload payload)
            {
                
                this.SendMessage(payload, "Get File - ",
                        "ownermic", "host.general.owner.getfile");
             }

 
        
            public void OwnerGetModuleInitFileSet() {
                this.OwnerGetModuleInitFileSet(this.CreatePayload());
            }

            public void OwnerGetModuleInitFileSet(System.String content) {
                var payload = this.CreatePayload();
                payload.Content = content;
                this.OwnerGetModuleInitFileSet(payload);
            }

            public void OwnerGetModuleInitFileSet(OSTLPayload payload)
            {
                
                this.SendMessage(payload, "Get Module Init File Set - ",
                        "ownermic", "host.general.owner.getmoduleinitfileset");
             }

 
        
            // And can also say/hear everything which : Public hears.
            
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

 
        

        private void SendMessage(OSTLPayload payload, string description, string mic, string routingKey, string directRoutingKey = "")
        {
            if (IsDebugMode)
            {
                System.Console.WriteLine(description);
                System.Console.WriteLine("payload: " + payload.SafeToString());
            }

            var finalRoute = payload.RoutingKey = routingKey;

            if (!string.IsNullOrEmpty(directRoutingKey))
            {
                finalRoute = directRoutingKey;
                mic = "";
            }

            this.ReplyTo += payload.HandleReplyTo;

            IBasicProperties props = this.RMQChannel.CreateBasicProperties();
            props.ReplyTo = "amq.rabbitmq.reply-to";
            this.RMQChannel.BasicPublish(mic, finalRoute, props, Encoding.UTF8.GetBytes(payload.ToJSonString()));
        }
    }
}

                    