using CODEIverse.SassyMQ.Lib.SMQHost;
using SassyMQ.Lib.RabbitMQ;
using SassyMQ.OSTL.Lib.RabbitMQ;
using SassyMQ.OSTL.Lib.RMQActors;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CODEiverse.SassyMQ.OpenSourceTools.ConApp
{
    class Program
    {
        private static SMQPublic publicProxy;

        public static Task ActiveTask { get; set; }

        static void Main(string[] args)
        {
            String command = args.Length > 0 ? args[0] : "";
            String projectName = args.Length > 1 ? args[1] : "";

            if (!String.IsNullOrEmpty(command) && !String.IsNullOrEmpty(projectName))
            {
                switch (command.ToLower())
                {
                    case "create":
                        var url = (args.Length > 2) ? args[2] : "";
                        if (String.IsNullOrEmpty(url)) break;

                        publicProxy = new SMQPublic();

                        try
                        {
                            publicProxy.ReplyTo += PublicProxy_ReplyTo;
                            publicProxy.PublicCreateSMQProject(url);
                            ActiveTask = Task.Factory.StartNew(() =>
                            {
                                Console.WriteLine("SassyMQ is building your SDK now.  Please wait - this can take 30-45 seconds....");
                                while (!ReferenceEquals(ActiveTask, null))
                                {
                                    Thread.Sleep(2500);
                                    Console.Write(".");
                                }
                            });

                            while (!ReferenceEquals(ActiveTask, null))
                            {
                                // do nothing
                            }

                        }
                        finally
                        {
                            publicProxy.ReplyTo -= PublicProxy_ReplyTo;
                            publicProxy.Disconnect();
                        }


                        return;

                    case "rebuild":
                        return;
                }
            }

            Console.WriteLine(@"Syntax: 
SassyMQ Command ProjectName

 Commands:

    create GSheetURL
    Creates a new SassyMQ Project, and returns the details in a project file which is created as ProjectName.smqp.

    rebuild
    Rebuilds the project (found in the file ProjectName.smqp) - and asks SassyMQ to rebuild the project

    list
    Lists modules (and their paths)

    delete ModuleName
    Deletes a module

    available
    Lists modules available

    add ModuleName RelativePath
    Adds a module
");


        }

        private static void PublicProxy_ReplyTo(object sender, PayloadEventArgs<OSTLPayload> e)
        {
            if (e.Payload.IsLexiconTerm(LexiconTermEnum.public_createsmqproject_host))
            {
                String projectFileName = String.Format("{0}.smqp", e.Payload.ProjectName);
                SMQProject smqp = new SMQProject();
                smqp.ProjectName = e.Payload.ProjectName;
                smqp.ProjectHash = e.Payload.ProjectHash;
                smqp.GoogleKey = e.Payload.GoogleKey;
                smqp.GID = e.Payload.GID;
                smqp.Save(projectFileName);


                var sassyMQRootDir = new DirectoryInfo(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "SassyMQ"));
                if (!sassyMQRootDir.Exists) sassyMQRootDir.Create();
                // Create projects folderr
                var projDir = new DirectoryInfo(Path.Combine(sassyMQRootDir.FullName, ".keys"));
                if (!projDir.Exists) projDir.Create();

                var keyPath = Path.Combine(projDir.FullName, String.Format("{0}.smqk", smqp.ProjectHash));
                File.WriteAllText(keyPath, e.Payload.PrivateKey);

                ActiveTask = null;
            }

        }
    }
}
