namespace Module.Introduction.Infrastructure
{
    public class ApplicationSettings
    {
        public int NumberOfProducts { get; set; }
        
        public bool AllowLoggingInAction { get; set; }

        public int MaximumNumberOfImage { get; set; }

        public int MaximumPeriodBetweenRequest { get; set; }

        public int IdOfBrokenImage { get; set; }

        public string NameOfDirectory { get; set; }
    }
}
