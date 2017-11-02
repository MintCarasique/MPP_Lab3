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
        private class DirectoryParameters
        {
            public string DestinationDirectory { get; set; }

            public string File { get; set; }
        }

        private DirectoryInfo _directoryInfo;

        private string _sourceDirectory, _destinationDirectory;

        private DirectoryParameters _directoryParameters;

        private int _copiedFilesCounter;

        private int _workerThreads;
        private int _portThreads;
        private int _availableWorkerThreads;
        private int _availablePortThreads;

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
            WaitThreadsToFinish();
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
                CopyingDirectories(directory.FullName, Path.Combine(destinationDirectory, directory.Name));
            }
            CopyingFiles(sourceDirectory, destinationDirectory);
        }

        private void CopyingFiles(string sourceDirectory, string destinationDirectory)
        {
            foreach (string file in Directory.GetFiles(sourceDirectory))
            {
                _directoryParameters = new DirectoryParameters
                {
                    DestinationDirectory = destinationDirectory,
                    File = file
                };
                ThreadPool.QueueUserWorkItem(CopyingOperation, _directoryParameters);
            }
        }

        private void CopyingOperation(object parameters)
        {
            bool isExisting = false;
            DirectoryParameters procParameters = (DirectoryParameters) parameters;
            string fileName = Path.GetFileName(procParameters.File);
            if (File.Exists(Path.Combine(procParameters.DestinationDirectory, fileName)))
            {
                isExisting = true;
            }
            File.Copy(procParameters.File, Path.Combine(procParameters.DestinationDirectory, fileName), true);
            if (!isExisting)
            {
                IterateCounter(fileName, procParameters.DestinationDirectory);
            }
        }

        private void IterateCounter(string fileName, string destinationDirectory)
        {
            Interlocked.Increment(ref _copiedFilesCounter);
            Console.WriteLine("{0}:{1} Copied {2} to {3}", 
                DateTime.Now.ToLongTimeString(), 
                DateTime.Now.Millisecond, 
                fileName, 
                destinationDirectory);
        }

        private void WaitThreadsToFinish()
        {
            while (true)
            {
                ThreadPool.GetMaxThreads(out _workerThreads, out _portThreads);
                ThreadPool.GetAvailableThreads(out _availableWorkerThreads, out _availablePortThreads);

                if (_workerThreads == _availableWorkerThreads && _portThreads == _availablePortThreads)
                {
                    return;
                }
            }
        }
    }
}
