namespace Business
{
    public static class Config
    {
        public static string[] CommandLineArguments { get; set; }
        public static bool IsCommandLineArgumentsValid
        {
            get
            {
                return CommandLineArguments != null 
                    && CommandLineArguments.Length > 0
                    && !string.IsNullOrWhiteSpace(CommandLineArguments[0]);
            }
        }
    }
}