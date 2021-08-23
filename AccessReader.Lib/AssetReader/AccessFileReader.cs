using System.Collections.Generic;
using System.IO;
using System.Linq;
using AccessReader.Lib.Constants;
using AccessReader.Lib.Domain;

namespace AccessReader.Lib.AssetReader
{
    internal class AccessFileReader : IAccessReader
    {
        public AccessFileData ReadFile(Stream fileData)
        {
            using var file = new MemoryStream();
            fileData.CopyTo(file);
            var version = ReadVersion(file);
            var pages = GetPages(file).ToArray();

            return new AccessFileData { Version = version, Pages = pages };
        }

        private static IEnumerable<AccessPage> GetPages(MemoryStream file)
        {
            var version = ReadVersion(file);
            return GetPages(file, GetPageLength(version));
        }

        private static IEnumerable<AccessPage> GetPages(MemoryStream file, int pageLength)
        {
            while (file.Position < file.Length - 1)
                yield return ReadPage(file, pageLength);
        }

        private static AccessVersion ReadVersion(MemoryStream file)
        {
            file.Position = 0x14;
            var versionByte = file.ReadByte();
            file.Position = 0;

            return versionByte switch
            {
                0x00 => AccessVersion.Jet3,
                0x01 => AccessVersion.Jet4,
                _ => AccessVersion.Unknown
            };
        }

        private static int GetPageLength(AccessVersion version) =>
            version switch
            {
                AccessVersion.Jet3 => AccessConstants.PageSize.Jet3,
                AccessVersion.Jet4 => AccessConstants.PageSize.Jet4,
                _ => 0
            };

        private static AccessPage ReadPage(MemoryStream file, int pageLength)
        {
            var pageData = new byte[pageLength];
            file.Read(pageData, 0, pageLength);

            return new AccessPage
            {
                Data = pageData,
                Type = GetPageType(pageData)
            };
        }

        private static PageType GetPageType(byte[] pageData) =>
            pageData[0] switch
            {
                0x00 => PageType.DbDefinition,
                0x01 => PageType.DataPage,
                0x02 => PageType.TableDefinition,
                0x03 => PageType.IntermediateIndex,
                0x04 => PageType.LeafIndex,
                0x05 => PageType.PageUsageBitmaps,
                _ => PageType.Unknown
            };
    }
}