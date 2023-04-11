using System.IO;

namespace MyDatabase
{
    public partial class DatabaseFileInfo
    {
        public string FolderDirectory { get; private set; }
        public string FileName { get; private set; }
        public string FilePath { get { return Path.Combine(FolderDirectory, FileName); } }

        public DatabaseFileInfo(string folderDirectory, string fileName)
        {
            FolderDirectory = folderDirectory;
            FileName = fileName;
        }
    }
}
