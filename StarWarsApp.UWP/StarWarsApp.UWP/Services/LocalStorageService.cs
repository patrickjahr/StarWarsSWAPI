using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Windows.Storage;
using Windows.Storage.Search;
using Core.Services;
using Core.Utils;
using Core.Utils.Extentions;
using StarWarsApp.UWP.Utils;

namespace StarWarsApp.UWP.Services
{
    public class LocalStorageService : ILocalStorageService
    {
       

        public async Task<bool> WriteBytesToStorageAsync(string fileName, byte[] bytesToWrite, StorageTargetType storageTargetType = StorageTargetType.Local, string targetPath = null)
        {
            try
            {
                var targetFolder = await getStorageFolderAsync(storageTargetType, targetPath);
                var file = await targetFolder.CreateFileAsync(fileName, CreationCollisionOption.ReplaceExisting);
                await FileIO.WriteBytesAsync(file, bytesToWrite);
                return true;
            }
            catch
            {
                Debug.WriteLine("LocalStorageService.cs | WriteBytesToStorageAsync | Error saving {0} to storage", fileName);
                return false;
            }
        }

        public async Task<bool> WriteStringToStorageAsync(string fileName, string stringToWrite, StorageTargetType storageTargetType = StorageTargetType.Local, string targetPath = null)
        {
            try
            {
                var targetFolder = await getStorageFolderAsync(storageTargetType, targetPath);
                var file = await targetFolder.CreateFileAsync(fileName, CreationCollisionOption.ReplaceExisting);
                await FileIO.WriteTextAsync(file, stringToWrite);
                return true;
            }
            catch
            {
                Debug.WriteLine("LocalStorageService.cs | WriteStringToStorageAsync | Error saving {0} to storage", fileName);
                return false;
            }
        }

        public async Task<bool> WriteStreamToStorageAsync(string fileName, Stream streamToWrite, StorageTargetType storageTargetType = StorageTargetType.Local, string targetPath = null)
        {
            try
            {
                var targetFolder = await getStorageFolderAsync(storageTargetType, targetPath);
                var file = await targetFolder.CreateFileAsync(fileName, CreationCollisionOption.ReplaceExisting);
                using (var outStream = await file.OpenStreamForWriteAsync())
                {
                    await streamToWrite.CopyToAsync(outStream);
                    await outStream.FlushAsync();
                }
                return true;
            }
            catch
            {
                Debug.WriteLine("LocalStorageService.cs | WriteStreamToStorageAsync | Error saving {0} to storage", fileName);
                return false;
            }
        }

        public async Task<string> ReadStringFromStorageAsync(string fileName, StorageTargetType storageTargetType = StorageTargetType.Local, string targetPath = null)
        {

            try
            {
                var targetFolder = await getStorageFolderAsync(storageTargetType, targetPath);
                var file = await targetFolder.GetFileAsync(fileName).AsTask().ConfigureAwait(false);
                if (file != null)
                {
                    var data = await FileIO.ReadTextAsync(file).AsTask().ConfigureAwait(false);
                    return data;
                }
            }
            catch
            {
                Debug.WriteLine("LocalStorageService.cs | ReadStringFromStorageAsync | Error reading file {0}", fileName);
            }

            return null;
        }

        public async Task<Stream> ReadStreamFromStorageAsync(string fileName, StorageTargetType storageTargetType = StorageTargetType.Local, string targetPath = null)
        {
            try
            {
                var targetFolder = await getStorageFolderAsync(storageTargetType, targetPath);
                var file = await targetFolder.GetFileAsync(fileName).AsTask().ConfigureAwait(false);
                if (file != null)
                {
                    var randomAccessStream = await file.OpenAsync(FileAccessMode.ReadWrite);
                    return randomAccessStream.AsStream();
                }
            }
            catch
            {
                Debug.WriteLine("LocalStorageService.cs | ReadStringFromStorageAsync | Error reading file {0}", fileName);
            }

            return null;
        }

        public async Task<byte[]> ReadBytesFromStorageAsync(string fileName, StorageTargetType storageTargetType = StorageTargetType.Local, string targetPath = null)
        {
            try
            {
                var targetFolder = await getStorageFolderAsync(storageTargetType, targetPath);

                var file = await targetFolder.GetFileAsync(fileName);
                byte[] data;
                using (var s = await file.OpenStreamForReadAsync())
                {
                    data = new byte[s.Length];
                    await s.ReadAsync(data, 0, (int)s.Length);
                }

                return data;
            }
            catch
            {
                Debug.WriteLine("LocalStorageService.cs | ReadBytesFromStorageAsync | Error reading file {0}", fileName);
            }
            return null;
        }

        public async Task<bool> DeleteFromStorageAsync(string fileName, StorageTargetType storageTargetType = StorageTargetType.Local, string targetPath = null)
        {
            try
            {
                var targetFolder = await getStorageFolderAsync(storageTargetType, targetPath);

                var file = await targetFolder.GetFileAsync(fileName);
                await file.DeleteAsync();
                return true;
            }
            catch
            {
                Debug.WriteLine("LocalStorageService.cs | DeleteFromStorageAsync | Could not delete file: {0}", fileName);
                return false;
            }
        }

        public async Task<List<string>> GetAllFilesListAsync(StorageTargetType storageTargetType = StorageTargetType.Local, string targetPath = null)
        {
            var targetFolder = await getStorageFolderAsync(storageTargetType, targetPath);
            var queryOptions = new QueryOptions(CommonFileQuery.DefaultQuery, new[] { "*" });
            queryOptions.FolderDepth = FolderDepth.Deep;
            var availableCachFiles = targetFolder.CreateFileQueryWithOptions(queryOptions);
            var files = await availableCachFiles.GetFilesAsync();

            return files.Select(storageFile => storageFile.Name).ToList();
        }

        public async Task<List<IPortableStorageFile>> GetFilesListAsync(string query, StorageTargetType storageTargetType = StorageTargetType.Local, string targetPath = null, bool deep = false)
        {
            var targetFolder = await getStorageFolderAsync(storageTargetType, targetPath);
            var queryOptions = new QueryOptions(CommonFileQuery.DefaultQuery, new[] { query });
            if (deep)
            {
                queryOptions.FolderDepth = FolderDepth.Deep;
            }
            var availableCachFiles = targetFolder.CreateFileQueryWithOptions(queryOptions);
            var files = await availableCachFiles.GetFilesAsync();

            return files.Select(storageFile => storageFile.AsPortableStorageFile()).ToList();
        }

        public async Task<List<IPortableStorageFile>> GetFilesListSortByDateAsync(string query, StorageTargetType storageTargetType = StorageTargetType.Local, string targetPath = null, bool deep = false)
        {
            var targetFolder = await getStorageFolderAsync(storageTargetType, targetPath);
            var queryOptions = new QueryOptions(CommonFileQuery.DefaultQuery, new[] { query });
            queryOptions.SortOrder.Add(new SortEntry() { PropertyName = "System.DateModified" });
            if (deep)
            {
                queryOptions.FolderDepth = FolderDepth.Deep;
            }
            var availableCachFiles = targetFolder.CreateFileQueryWithOptions(queryOptions);
            var files = await availableCachFiles.GetFilesAsync();

            return files.Select(storageFile => storageFile.AsPortableStorageFile()).ToList();
        }

        public async Task<IPortableStorageFile> CreateOrOpenFileAsync(string fileName, StorageTargetType storageTargetType, string optionalFileContent = null, string targetPath = null)
        {
            var targetFolder = await getStorageFolderAsync(storageTargetType, targetPath);

            var file = await targetFolder.CreateFileAsync(fileName, CreationCollisionOption.OpenIfExists);
            if (optionalFileContent.IsNeitherNullNorEmpty())
            {
                await FileIO.WriteTextAsync(file, optionalFileContent);
            }
            return file.AsPortableStorageFile();
        }


        public async Task<IPortableStorageFile> CreateOrOpenFileAsync(string absoluteTargetFilePath, bool throwExceptions = true)
        {
            try
            {
                var file = await StorageFile.GetFileFromPathAsync(absoluteTargetFilePath);
                return file.AsPortableStorageFile();
            }
            catch (Exception e)
            {
                Debug.WriteLine("LocalStorageService.cs | CreateOrOpenFileAsync | " + e);
                if (throwExceptions) throw;
            }
            return null;
        }

        public async Task DeleteFileAsync(string absoluteTargetFilePath, bool throwExceptions=true)
        {
            try
            {
                var file = await StorageFile.GetFileFromPathAsync(absoluteTargetFilePath);
                await file.DeleteAsync(StorageDeleteOption.PermanentDelete);
            }
            catch (Exception e)
            {
                Debug.WriteLine("LocalStorageService.cs | DeleteFileAsync | " + e);
                if (throwExceptions) throw;
            }
        }


        public async Task DeleteFileAsync(IPortableStorageFile file)
        {
            await file.AsWinRTStorageFile().DeleteAsync(StorageDeleteOption.PermanentDelete);
        }


        public async Task<bool> FileExistsAsync(IPortableStorageFile file)
        {
            try
            {
                return File.Exists(file.Path);
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> FileExistsAsync(string filename, StorageTargetType targetType, string targetPath = null)
        {
            try
            {
                var folder = await getStorageFolderAsync(targetType, targetPath);
                return File.Exists(Path.Combine(folder.Path, filename));
            }
            catch (FileNotFoundException)
            {
                return false;
            }
            catch (Exception)
            {
                return false;
            }
        }

        private async Task<StorageFolder> getStorageFolderAsync(StorageTargetType targetType, string targetPath)
        {
            var targetFolder = ApplicationData.Current.LocalFolder;
            try
            {
                switch (targetType)
                {
                    case StorageTargetType.Roaming:
                        targetFolder = ApplicationData.Current.RoamingFolder;
                        break;
                    case StorageTargetType.Temp:
                        targetFolder = ApplicationData.Current.TemporaryFolder;
                        break;
                }

                if (targetPath.IsNeitherNullNorEmpty())
                {
                    var splittedPath = targetPath.Split(new[] { '/', '\\' }, StringSplitOptions.RemoveEmptyEntries);
                    foreach (var folderName in splittedPath)
                    {
                        targetFolder = await targetFolder.CreateFolderAsync(folderName, CreationCollisionOption.OpenIfExists);
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("LocalStorageService.cs | getStorageFolderAsync | Error getting / creating folder in {0} for path {1}", targetType, targetPath);
                // as the PathTooLongException is not public...
                if (ex.HResult == -2147024690)
                {
                    Debug.WriteLine("LocalStorageService.cs | getStorageFolderAsync | Path was to long");
                }
                throw;
            }

            return targetFolder;
        }
    }
}