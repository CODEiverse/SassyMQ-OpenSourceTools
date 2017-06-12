using SassyMQ.Lib.RabbitMQ.Payload;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SassyMQ.OSTL.Lib.RMQActors
{
    public class OSTLPayload : StandardPayload<OSTLPayload>
    {
        public List<String> ModuleFiles { get; set; }
        public List<Byte[]> ModuleFileContents { get; set; }
        public string ProjectName { get; set; }
        public string ProjectHash { get; set; }
        public string PrivateKey { get; set; }
        public string GoogleKey { get; set; }
        public string GID { get; set; }
        public string LoadedFile { get; set; }
        public string LoadFileName { get; set; }
        public List<string> AllFiles { get; set; }
        public string Uri { get { return this.Content; } set { this.Content = value; } }
        public string ProjectAcronym { get; set; }
    }
}
