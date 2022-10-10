namespace DartApplicationLibrary
{
    /// <summary>
    /// The singleton pattern below was based on the information from https://csharpindepth.com/articles/singleton.
    /// </summary>
    public class ServiceLocator
    {
        private static readonly Lazy<ServiceLocator> Lazy = new(() => new ServiceLocator());

        public static ServiceLocator Instance => Lazy.Value;

        /// <summary>
        /// Internal only for tests.
        /// </summary>
        internal ServiceLocator()
        {
            Database = Database.FromDefault();
        }
        
        public Database Database { get; }
    }
}
