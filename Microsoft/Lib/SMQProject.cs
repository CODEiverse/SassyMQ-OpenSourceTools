using CODEiverse.SassyMQ.Lib.SMQHost;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Xml.Serialization;

namespace CODEIverse.SassyMQ.Lib.SMQHost
{
    public class SMQProject
    {
        public SMQProject()
        {
            this.LocalCopies = new List<SMQLocalCopy>();
        }

        public string FileName { get; set; }
        public string GID { get; set; }
        public string GoogleKey { get; set; }
        public string PrivateKey { get; set; }
        public string ProjectHash { get; set; }
        private string _projectName;
        public string ProjectName
        {
            get { return _projectName; }
            set
            {
                _projectName = value;
                if (String.IsNullOrEmpty(this.ProjectAcronym))
                {
                    this.ProjectAcronym = String.Join("", this.ProjectName.Where(whereProjectNameChar => Char.IsUpper(whereProjectNameChar) || Char.IsNumber(whereProjectNameChar)));
                }
            }
        }
        public string ProjectAcronym { get; set; }

        public List<SMQLocalCopy> LocalCopies { get; set; }
        public SMQLocalCopy[] LocalCopiesXML
        {
            get { return new List<SMQLocalCopy>().ToArray(); }
            set
            {
                if (value.Any()) LocalCopies = value.ToList();
            }
        }
        protected string SMQKeyFilename
        {
            get
            {
                var myDocsPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
                var smqKeyFilename = String.Format("{0}.smqk", Path.Combine(myDocsPath, "SassyMQ", ".keys", this.ProjectHash));
                return smqKeyFilename;
            }
        }

        public static SMQProject FromFile(String fileName)
        {
            fileName = HttpUtility.UrlDecode(new Uri(fileName).AbsolutePath);

            var fileContents = File.ReadAllText(fileName);

            var project = default(SMQProject);
            if (fileContents.Trim().StartsWith("{"))
            {
                project = JsonConvert.DeserializeObject<SMQProject>(fileContents);
            }
            else
            {
                XmlSerializer ser = new XmlSerializer(typeof(SMQProject));
                MemoryStream ms = new MemoryStream(Encoding.UTF8.GetBytes(fileContents));
                project = ser.Deserialize(ms) as SMQProject;
            }

            project.FileName = fileName;
            project.ProjectName = Path.GetFileNameWithoutExtension(Path.GetFileName(fileName));

            project.LoadPrivateKey();

            return project;
        }



        public void Save()
        {
            this.Save(this.FileName);
        }

        public void Save(String fileName)
        {
            this.SaveAndClearPrivateKey();

            this.FileName = fileName;
            File.WriteAllText(fileName, JsonConvert.SerializeObject(this));

            this.LoadPrivateKey();
        }


        private void SaveAndClearPrivateKey()
        {
            this.SavePrivateKey();
            this.PrivateKey = null;
        }

        private void SavePrivateKey()
        {
            if (!String.IsNullOrEmpty(this.PrivateKey))
            {
                var smqKeyFI = new FileInfo(this.SMQKeyFilename);
                if (!smqKeyFI.Directory.Exists) smqKeyFI.Directory.Create();
                File.WriteAllText(smqKeyFI.FullName, this.PrivateKey);
            }


        }

        private void LoadPrivateKey()
        {
            var smqKeyFI = new FileInfo(this.SMQKeyFilename);
            if (smqKeyFI.Exists) this.PrivateKey = File.ReadAllText(smqKeyFI.FullName);
        }

        public List<SMQLocalCopy> FindLocalCopies(List<string> allFiles)
        {
            var fileSets = allFiles.Where(whereFile => whereFile.EndsWith(".xml", StringComparison.OrdinalIgnoreCase) &&
                                                        whereFile.ToLower().Contains("/_fileset"));
            var matchingCopies = this.LocalCopies
                                    .Where(whereLocalCopy => whereLocalCopy.HasMatches(fileSets))
                                    .ToList();

            return matchingCopies;

        }
    }
}
