namespace AccessReader.Lib.Domain
{
    public record AccessFileData
    {
        public AccessVersion Version { get; init; }
        
        public AccessPage[] Pages { get; init; }
    }
}