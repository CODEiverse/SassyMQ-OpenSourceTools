
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
    public partial class SMQHost : SMQActorBase<OSTLPayload>
    {
     
        public SMQHost(bool isAutoConnect = true)
            : base("host.all", isAutoConnect)
        {
        }
        // OpenSourceToolsLexicon - OSTL
        public virtual bool Connect(string virtualHost, string username, string password)
        {
            return base.Connect(virtualHost, username, password);
        }   

        protected override void CheckRouting(OSTLPayload payload) {
            
             if (payload.IsLexiconTerm(LexiconTermEnum.public_createsmqproject_host)) 
            {
                this.OnPublicCreateSMQProjectReceived(payload);
                if (!System.String.IsNullOrEmpty(payload.ReplyTo)) 
                {
                    this.RMQChannel.BasicPublish("", payload.ReplyTo, body: Encoding.UTF8.GetBytes(payload.ToJSonString()));
                }
            }
        
            else  if (payload.IsLexiconTerm(LexiconTermEnum.owner_takeownership_host)) 
            {
                this.OnOwnerTakeOwnershipReceived(payload);
                if (!System.String.IsNullOrEmpty(payload.ReplyTo)) 
                {
                    this.RMQChannel.BasicPublish("", payload.ReplyTo, body: Encoding.UTF8.GetBytes(payload.ToJSonString()));
                }
            }
        
            else  if (payload.IsLexiconTerm(LexiconTermEnum.owner_setsource_host)) 
            {
                this.OnOwnerSetSourceReceived(payload);
                if (!System.String.IsNullOrEmpty(payload.ReplyTo)) 
                {
                    this.RMQChannel.BasicPublish("", payload.ReplyTo, body: Encoding.UTF8.GetBytes(payload.ToJSonString()));
                }
            }
        
            else  if (payload.IsLexiconTerm(LexiconTermEnum.owner_getsdk_host)) 
            {
                this.OnOwnerGetSDKReceived(payload);
                if (!System.String.IsNullOrEmpty(payload.ReplyTo)) 
                {
                    this.RMQChannel.BasicPublish("", payload.ReplyTo, body: Encoding.UTF8.GetBytes(payload.ToJSonString()));
                }
            }
        
            else  if (payload.IsLexiconTerm(LexiconTermEnum.owner_rebuildsdk_host)) 
            {
                this.OnOwnerRebuildSDKReceived(payload);
                if (!System.String.IsNullOrEmpty(payload.ReplyTo)) 
                {
                    this.RMQChannel.BasicPublish("", payload.ReplyTo, body: Encoding.UTF8.GetBytes(payload.ToJSonString()));
                }
            }
        
            else  if (payload.IsLexiconTerm(LexiconTermEnum.owner_getfiles_host)) 
            {
                this.OnOwnerGetFilesReceived(payload);
                if (!System.String.IsNullOrEmpty(payload.ReplyTo)) 
                {
                    this.RMQChannel.BasicPublish("", payload.ReplyTo, body: Encoding.UTF8.GetBytes(payload.ToJSonString()));
                }
            }
        
            else  if (payload.IsLexiconTerm(LexiconTermEnum.owner_getfile_host)) 
            {
                this.OnOwnerGetFileReceived(payload);
                if (!System.String.IsNullOrEmpty(payload.ReplyTo)) 
                {
                    this.RMQChannel.BasicPublish("", payload.ReplyTo, body: Encoding.UTF8.GetBytes(payload.ToJSonString()));
                }
            }
        
            else  if (payload.IsLexiconTerm(LexiconTermEnum.owner_getmoduleinitfileset_host)) 
            {
                this.OnOwnerGetModuleInitFileSetReceived(payload);
                if (!System.String.IsNullOrEmpty(payload.ReplyTo)) 
                {
                    this.RMQChannel.BasicPublish("", payload.ReplyTo, body: Encoding.UTF8.GetBytes(payload.ToJSonString()));
                }
            }
        
            // And can also hear everything which : Owner hears.
            
            // And can also hear everything which : Public hears.
            
        }

        
        public event System.EventHandler<PayloadEventArgs<OSTLPayload>> PublicCreateSMQProjectReceived;
        protected virtual void OnPublicCreateSMQProjectReceived(OSTLPayload payload)
        {
            if (IsDebugMode) 
            {
                System.Console.WriteLine("Create S M Q Project - ");
                System.Console.WriteLine("payload: " + payload.SafeToString());
            }

            var plea = new PayloadEventArgs<OSTLPayload>(payload);
            this.Invoke(this.PublicCreateSMQProjectReceived, plea);
        }
        
        public event System.EventHandler<PayloadEventArgs<OSTLPayload>> OwnerTakeOwnershipReceived;
        protected virtual void OnOwnerTakeOwnershipReceived(OSTLPayload payload)
        {
            if (IsDebugMode) 
            {
                System.Console.WriteLine("Take Ownership - ");
                System.Console.WriteLine("payload: " + payload.SafeToString());
            }

            var plea = new PayloadEventArgs<OSTLPayload>(payload);
            this.Invoke(this.OwnerTakeOwnershipReceived, plea);
        }
        
        public event System.EventHandler<PayloadEventArgs<OSTLPayload>> OwnerSetSourceReceived;
        protected virtual void OnOwnerSetSourceReceived(OSTLPayload payload)
        {
            if (IsDebugMode) 
            {
                System.Console.WriteLine("Set Source - ");
                System.Console.WriteLine("payload: " + payload.SafeToString());
            }

            var plea = new PayloadEventArgs<OSTLPayload>(payload);
            this.Invoke(this.OwnerSetSourceReceived, plea);
        }
        
        public event System.EventHandler<PayloadEventArgs<OSTLPayload>> OwnerGetSDKReceived;
        protected virtual void OnOwnerGetSDKReceived(OSTLPayload payload)
        {
            if (IsDebugMode) 
            {
                System.Console.WriteLine("Get S D K - ");
                System.Console.WriteLine("payload: " + payload.SafeToString());
            }

            var plea = new PayloadEventArgs<OSTLPayload>(payload);
            this.Invoke(this.OwnerGetSDKReceived, plea);
        }
        
        public event System.EventHandler<PayloadEventArgs<OSTLPayload>> OwnerRebuildSDKReceived;
        protected virtual void OnOwnerRebuildSDKReceived(OSTLPayload payload)
        {
            if (IsDebugMode) 
            {
                System.Console.WriteLine("Rebuild S D K - ");
                System.Console.WriteLine("payload: " + payload.SafeToString());
            }

            var plea = new PayloadEventArgs<OSTLPayload>(payload);
            this.Invoke(this.OwnerRebuildSDKReceived, plea);
        }
        
        public event System.EventHandler<PayloadEventArgs<OSTLPayload>> OwnerGetFilesReceived;
        protected virtual void OnOwnerGetFilesReceived(OSTLPayload payload)
        {
            if (IsDebugMode) 
            {
                System.Console.WriteLine("Get Files - ");
                System.Console.WriteLine("payload: " + payload.SafeToString());
            }

            var plea = new PayloadEventArgs<OSTLPayload>(payload);
            this.Invoke(this.OwnerGetFilesReceived, plea);
        }
        
        public event System.EventHandler<PayloadEventArgs<OSTLPayload>> OwnerGetFileReceived;
        protected virtual void OnOwnerGetFileReceived(OSTLPayload payload)
        {
            if (IsDebugMode) 
            {
                System.Console.WriteLine("Get File - ");
                System.Console.WriteLine("payload: " + payload.SafeToString());
            }

            var plea = new PayloadEventArgs<OSTLPayload>(payload);
            this.Invoke(this.OwnerGetFileReceived, plea);
        }
        
        public event System.EventHandler<PayloadEventArgs<OSTLPayload>> OwnerGetModuleInitFileSetReceived;
        protected virtual void OnOwnerGetModuleInitFileSetReceived(OSTLPayload payload)
        {
            if (IsDebugMode) 
            {
                System.Console.WriteLine("Get Module Init File Set - ");
                System.Console.WriteLine("payload: " + payload.SafeToString());
            }

            var plea = new PayloadEventArgs<OSTLPayload>(payload);
            this.Invoke(this.OwnerGetModuleInitFileSetReceived, plea);
        }
        
            // And can also say/hear everything which : Owner hears.
            
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

                    