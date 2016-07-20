using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Core.Utils;

namespace Core.Services
{
    public interface ILocalStorageService
    {
        /// <summary>
        /// Writes the given Bytes to the Storage
        /// </summary>
        /// <param name="fileName">Should include the BaseFile extension (e.g. "mainImage.png")</param>
        /// <param name="bytesToWrite">Data to write</param>
        /// <param name="storageTargetType">Type of the target Storage (e.g. Local Storage)</param>
        /// <param name="targetPath">targetPath. If omitted root of the LocalFolder will be used as Default. </param>
        /// <returns></returns>
        Task<bool> WriteBytesToStorageAsync(string fileName, byte[] bytesToWrite, StorageTargetType storageTargetType = StorageTargetType.Local, string targetPath = null);

        /// <summary>
        /// Writes the given string to the Storage
        /// </summary>
        /// <param name="fileName">Should include the BaseFile extension (e.g. "history.json")</param>
        /// <param name="stringToWrite">Data to write</param>
        /// <param name="storageTargetType">Type of the target Storage (e.g. Local Storage)</param>
        /// <param name="targetPath">targetPath. If omitted root of the LocalFolder will be used as Default. </param>
        /// <returns></returns>
        Task<bool> WriteStringToStorageAsync(string fileName, string stringToWrite, StorageTargetType storageTargetType = StorageTargetType.Local, string targetPath = null);

        /// <summary>
        /// Writes the given stream to storage
        /// </summary>
        /// <param name="fileName">Should include the BaseFile extension (e.g. "history.json")</param>
        /// <param name="streamToWrite">Data to write</param>
        /// <param name="storageTargetType">Type of the target Storage (e.g. Local Storage)</param>
        /// <param name="targetPath">targetPath. If omitted root of the LocalFolder will be used as Default. </param>
        /// <returns></returns>
        Task<bool> WriteStreamToStorageAsync(string fileName, Stream streamToWrite, StorageTargetType storageTargetType = StorageTargetType.Local, string targetPath = null);

        /// <summary>
        /// Read a string from the Storage
        /// </summary>
        /// <param name="fileName">Filename including the file extension</param>
        /// <param name="storageTargetType">Type of the target Storage (e.g. Local Storage)</param>
        /// <param name="targetPath">Optional targetPath. If omitted root of the LocalFolder will be used as Default. </param>
        /// <returns>BaseFile Content as string</returns>
        Task<string> ReadStringFromStorageAsync(string fileName, StorageTargetType storageTargetType = StorageTargetType.Local, string targetPath = null);

        /// <summary>
        /// Returns the BaseFile as Stream
        /// </summary>
        /// <param name="fileName"></param>
        /// <param name="storageTargetType">Type of the target Storage (e.g. Local Storage)</param>
        /// <param name="targetPath">Optional targetPath. If omitted root of the LocalFolder will be used as Default. </param>
        /// <returns>Cache Content as string</returns>
        Task<Stream> ReadStreamFromStorageAsync(string fileName, StorageTargetType storageTargetType = StorageTargetType.Local, string targetPath = null);


        /// <summary>
        /// Reads a byte array from the storage
        /// </summary>
        /// <param name="fileName">Filename including extension (e.g. "mainImage.png")</param>
        /// <param name="storageTargetType">Type of the target Storage (e.g. Local Storage)</param>
        /// <param name="targetPath">Optional targetPath. If omitted root of the LocalFolder will be used as Default. </param>
        /// <returns>BaseFile Content as byte array</returns>
        Task<byte[]> ReadBytesFromStorageAsync(string fileName, StorageTargetType storageTargetType = StorageTargetType.Local, string targetPath = null);

        /// <summary>
        /// Delete BaseFile from the Storage
        /// </summary>
        /// <param name="fileName">Filename including extension (e.g. "mainImage.png")</param>
        /// <param name="storageTargetType">Type of the target Storage (e.g. Local Storage)</param>
        /// <param name="targetPath">Optional targetPath. If omitted root of the LocalFolder will be used as Default. </param>
        /// <returns>Return True if the delete operation was successfull otherwise false</returns>
        Task<bool> DeleteFromStorageAsync(string fileName, StorageTargetType storageTargetType = StorageTargetType.Local, string targetPath = null);

        /// <summary>
        /// Queries the given Folder or LocalFolder recursivly for all types of Files
        /// </summary>
        /// <param name="storageTargetType">Type of the target Storage (e.g. Local Storage)</param>
        /// <param name="targetPath">Optional targetPath. If omitted root of the LocalFolder will be used as Default. </param>
        /// <returns>A List of BaseFile Names including the extension</returns>
        Task<List<string>> GetAllFilesListAsync(StorageTargetType storageTargetType = StorageTargetType.Local, string targetPath = null);

        /// <summary>
        /// Queries the given Folder or LocalFolder recursivly for the given search query
        /// </summary>
        /// <param name="query">keyword or file extension (case insensitive)</param>
        /// <param name="storageTargetType">Type of the target Storage (e.g. Local Storage)</param>
        /// <param name="targetPath">Optional targetPath. If omitted root of the LocalFolder will be used as Default. </param>
        /// <param name="deep">iterate all sub folders</param>
        /// <returns>A List of BaseFile Names including the extension</returns>
        Task<List<IPortableStorageFile>> GetFilesListAsync(string query, StorageTargetType storageTargetType = StorageTargetType.Local, string targetPath = null, bool deep = false);


        /// <summary>
        /// Creates or opens a file and writes the option file content to it.
        /// </summary>
        /// <param name="fileName">Name of the file.</param>
        /// <param name="storageTargetType">Type of the storage target.</param>
        /// <param name="optionalFileContent">Content of the optional file.</param>
        /// <param name="targetPath">The target path.</param>
        /// <returns>The dedicated file</returns>
        Task<IPortableStorageFile> CreateOrOpenFileAsync(string fileName, StorageTargetType storageTargetType, string optionalFileContent = null, string targetPath = null);


        /// <summary>
        /// Deletes the given file from storage.
        /// </summary>
        /// <param name="file">The file.</param>
        Task DeleteFileAsync(IPortableStorageFile file);

        /// <summary>
        /// Deletes the file located at the absolute target path.
        /// </summary>
        /// <param name="absoluteTargetFilePath">The absolute target file path.</param>
        /// <param name="throwExceptions"></param>
        /// <returns></returns>
        Task DeleteFileAsync(string absoluteTargetFilePath, bool throwExceptions = true);


        /// <summary>
        /// Determines if the file exists
        /// </summary>
        /// <param name="file">The file.</param>
        /// <returns></returns>
        Task<bool> FileExistsAsync(IPortableStorageFile file);

        Task<bool> FileExistsAsync(string filename, StorageTargetType targetType, string targetPath = null);

        /// <summary>
        /// Creates or opens a file for the give absolute storage path.
        /// </summary>
        /// <param name="absoluteTargetFilePath">The absolute target file path.</param>
        /// <param name="throwExceptions"></param>
        /// <returns></returns>
        Task<IPortableStorageFile> CreateOrOpenFileAsync(string absoluteTargetFilePath, bool throwExceptions = true);
        /// <summary>
        /// Gets the files list sort by date asynchronous.
        /// </summary>
        /// <param name="query">The query.</param>
        /// <param name="storageTargetType">Type of the storage target.</param>
        /// <param name="targetPath">The target path.</param>
        /// <param name="deep">iterate all sub folders</param>
        /// <returns></returns>
        Task<List<IPortableStorageFile>> GetFilesListSortByDateAsync(string query, StorageTargetType storageTargetType = StorageTargetType.Local, string targetPath = null, bool deep = false);

    }
}