using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading.Tasks;
using System.Threading;

namespace Lab3
{
    class Copier
    {
        private DirectoryInfo _directoryInfo;

        private string _sourceDirectory, _destinationDirectory;

        private int _copiedFilesCounter;

        public Copier(string source, string dest)
        {
            _copiedFilesCounter = 0;
            if (!Directory.Exists(source) & Directory.Exists(dest))
            {
                throw new FileNotFoundException("Directory not found");
            }
            _sourceDirectory = source;
            _destinationDirectory = dest;
        }

        public void StartCopying()
        {
            CopyingDirectories(_sourceDirectory, _destinationDirectory);
        }

        private void CopyingDirectories(string sourceDirectory, string destinationDirectory)
        {
            _directoryInfo = new DirectoryInfo(sourceDirectory);
            foreach (DirectoryInfo directory in _directoryInfo.GetDirectories())
            {
                if (!Directory.Exists(Path.Combine(destinationDirectory, directory.Name)))
                {
                    Directory.CreateDirectory(Path.Combine(destinationDirectory, directory.Name));
                }
                CopyingDirectories(directory.FullName, );
            }
        }

        private void CopyingFiles()
        {
            
        }
    }
}
