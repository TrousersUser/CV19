namespace CV19.ViewModels
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using CV19.ViewModels.Base;

    internal class DirectoryViewModel : ViewModelBase
    {   
        private readonly DirectoryInfo _DirectoryInfo;
        public IEnumerable<DirectoryViewModel> SubDirectories
        {
            get
            {
                try
                {
                    var directories = _DirectoryInfo
                     .EnumerateDirectories()
                     .Select(subDirectory => new DirectoryViewModel(subDirectory.FullName));
                    return directories;
                }
                catch (UnauthorizedAccessException ex)
                {
                    System.Diagnostics.Debug.WriteLine(ex.Message);
                }
                return Enumerable.Empty<DirectoryViewModel>();
            }
        }
        
        public IEnumerable<FileViewModel> Files
        {
            get
            {
                try
                {
                    var files = _DirectoryInfo
                     .EnumerateFiles()
                     .Select(file => new FileViewModel(file.FullName));
                    return files;
                }
                catch (UnauthorizedAccessException ex)
                {
                    System.Diagnostics.Debug.WriteLine(ex.Message);
                }
                return Enumerable.Empty<FileViewModel>();
            }
        }
        public IEnumerable<object> DirectoryItems
        {
            get
            {
                try
                {
                    var allItems = SubDirectories
                     .Cast<object>()
                     .Concat(Files);
                    return allItems;
                }
                catch (UnauthorizedAccessException ex)
                {
                     System.Diagnostics.Debug.WriteLine(ex.Message);
                }
                return Enumerable.Empty<object>();
            }
        }

        public string Name => _DirectoryInfo.Name;
        public string Path => _DirectoryInfo.FullName;
        public DateTime CreationTime => _DirectoryInfo.CreationTime;

        public DirectoryViewModel(string path) => _DirectoryInfo = new DirectoryInfo(path);
    }
}
