using Windows.Storage;
using Windows.Storage.FileProperties;
using Core.Services;

namespace StarWarsApp.UWP.Utils
{
    public static class WinRTStorageFileExtensions
    {
        public static IPortableStorageFile AsPortableStorageFile(this IStorageFile file)
        {
            return new WinRTStorageFile(file);
        }

        public static IStorageFile AsWinRTStorageFile(this IPortableStorageFile file)
        {
            return file.BaseFile as IStorageFile;
        }

        public static IPortableFileBasicProperties AsPortableBasicProperties(this BasicProperties properties)
        {
            return new WinRTFileBasicProperties(properties);
        }

        public static BasicProperties AsWinRTBasicProperties(this IPortableFileBasicProperties properties)
        {
            return properties.BasicProperties as BasicProperties;
        }
    }
}
