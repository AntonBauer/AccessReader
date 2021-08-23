namespace AccessReader.Lib.Domain
{
    public record AccessPage
    {
        public PageType Type { get; init; }
        
        public byte[] Data { get; init; }
    }
}