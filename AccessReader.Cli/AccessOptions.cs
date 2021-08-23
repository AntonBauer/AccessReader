namespace AccessReader.Cli
{
    internal record AccessOptions
    {
        public const string Access = "Access";
        
        public string FileLocation { get; init; }
    }
}