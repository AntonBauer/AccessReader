using System.IO;
using AccessReader.Lib.Domain;

namespace AccessReader.Lib.AssetReader
{
    public interface IAccessReader
    {
        AccessFileData ReadFile(Stream fileData);
    }
}