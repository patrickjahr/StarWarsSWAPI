using System;
using Windows.Storage.FileProperties;
using Core.Services;

namespace StarWarsApp.UWP.Utils
{
    public class WinRTFileBasicProperties : IPortableFileBasicProperties
    {
        private readonly BasicProperties _basicProperties;
        public object BasicProperties { get { return _basicProperties; } }

        public WinRTFileBasicProperties(BasicProperties properties)
        {
            _basicProperties = properties;
            
        }

        public ulong Size { get { return _basicProperties.Size; } }
        public DateTimeOffset ItemDate { get { return _basicProperties.ItemDate; } }
        public DateTimeOffset DateModified { get { return _basicProperties.DateModified; } }


        
    }
}