using System;
using System.Diagnostics;
using System.Threading.Tasks;
using Windows.Storage;
using Core.Services;

namespace StarWarsApp.UWP.Utils
{
    public class WinRTStorageFile : IPortableStorageFile
    {
        private TaskCompletionSource<bool> _renameTaskCompletionSource = new TaskCompletionSource<bool>();
        public string Path
        {
            get { return _baseFile.Path; }
        }

        public string Name
        {
            get { return _baseFile.Name; }
        }

        private readonly IStorageFile _baseFile;
        public object BaseFile { get { return _baseFile; } }

   
        public WinRTStorageFile(IStorageFile baseFile)
        {
            if (baseFile == null) throw new ArgumentNullException("baseFile");
            _baseFile = baseFile;
        }

        public async Task RenameAsync(string newName)
        {
            Debug.WriteLine("WinRTStorageFile.cs | RenameAsync | ");
            await _baseFile.RenameAsync(newName, NameCollisionOption.ReplaceExisting);
            _renameTaskCompletionSource.SetResult(true);
        }

        public async Task WaitForFirstRenamingAsync()
        {
            Debug.WriteLine("WinRTStorageFile.cs | WaitForFirstRenamingAsync | WAIT");
            await _renameTaskCompletionSource.Task;
            Debug.WriteLine("WinRTStorageFile.cs | WaitForFirstRenamingAsync | RETURN");
        }

        public async Task<IPortableFileBasicProperties> GetBasicPropertiesAsync()
        {
            return (await _baseFile.GetBasicPropertiesAsync()).AsPortableBasicProperties();
        }


    }
}
