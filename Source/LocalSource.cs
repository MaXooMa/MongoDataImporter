
using System.IO;


namespace MongoDataImporter.Source
{
    internal class LocalSource: Source
    {
        public LocalSource(string sourceString): base(sourceString) { }

        public override bool IsValidSource()
        {
            DeleteLastSlash();
            return System.IO.File.Exists(SourceString);
        }

        public override string CreateTempCopy()
        {
            var fileName = Path.GetFileName(SourceString);
            string tempPath = $@"./Temp/{fileName}";
            System.IO.File.Copy(SourceString, tempPath, true);
            return tempPath;
        }

    }
}
