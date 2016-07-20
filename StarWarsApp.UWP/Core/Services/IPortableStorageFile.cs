using System.Threading.Tasks;

namespace Core.Services
{
    public interface IPortableStorageFile
    {
        string Path { get; }
        string Name { get; }

        object BaseFile { get; }
    
        Task <IPortableFileBasicProperties> GetBasicPropertiesAsync();

        Task RenameAsync(string newName);
        Task WaitForFirstRenamingAsync();
    }
}
