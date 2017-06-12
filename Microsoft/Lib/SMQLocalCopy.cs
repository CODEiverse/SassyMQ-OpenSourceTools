using SassyMQ.Lib.RabbitMQ;
using SassyMQ.OSTL.Lib.RMQActors;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Xml.Serialization;

namespace CODEiverse.SassyMQ.Lib.SMQHost
{
    public class SMQLocalCopy
    {
        public SMQLocalCopy()
            : this("Docs", "/RabbitMQ_SDK/Docs")
        {

        }

        public SMQLocalCopy(String sdkModuleName, String localRelativePath)
        {
            this.SDKModuleName = sdkModuleName;
            this.LocalRelativePath = localRelativePath;
        }

        public String SDKModuleName { get; set; }

        private string _localRelativePath;
        public string LocalRelativePath
        {
            get
            {
                return String.Format("/{0}/", _localRelativePath.SafeToString()
                                                         .Trim("/\\"
                                                         .ToCharArray()));
            }
            set { _localRelativePath = value; }
        }

        public String[] MatchingFileSets { get; set; }

        public override string ToString()
        {
            return String.Format("{0} => {1}", this.SDKModuleName, this.LocalRelativePath);
        }

        internal bool HasMatches(IEnumerable<string> fileSets)
        {
            string matchingToken = String.Format("/{0}/", this.SDKModuleName);
            this.MatchingFileSets = fileSets.Where(whereFileSetPath => whereFileSetPath.IndexOf(matchingToken, StringComparison.OrdinalIgnoreCase) >= 0).ToArray();
            return this.MatchingFileSets.Any();
        }

        public void InitModule(String rootPath, OSTLPayload payload)
        {
            for (var i = 0; i < payload.ModuleFiles.Count; i++)
            {
                var fileName = payload.ModuleFiles[i];
                var contents = payload.ModuleFileContents[i];
                var fullFileName = Path.Combine(rootPath, this.LocalRelativePath.Trim('/'), fileName.Trim('/'));
                var ffnDI = new DirectoryInfo(Path.GetDirectoryName(fullFileName));
                if (!ffnDI.Exists) ffnDI.Create();
                var fullFI = new FileInfo(fullFileName);
                if (!fullFI.Exists) File.WriteAllBytes(fullFI.FullName, contents);
            }
        }
    }
}
