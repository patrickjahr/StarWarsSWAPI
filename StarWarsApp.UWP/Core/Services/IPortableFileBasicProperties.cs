using System;

namespace Core.Services
{
    public interface IPortableFileBasicProperties
    {
        object BasicProperties { get; }
        ulong Size { get; }
        DateTimeOffset ItemDate { get; }
        DateTimeOffset DateModified { get; }
    }
}