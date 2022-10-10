using Microsoft.Data.Sqlite;

namespace DartApplicationLibrary
{
    public sealed class Database : IDisposable
    {
        /// <summary>
        /// Direct connection to the database.
        /// </summary>
        public SqliteConnection Connection { get; }
        
        public string Location { get; }
        
        internal Database(string path)
        {
            this.Location = path;
            Connection = new SqliteConnection($"Data Source={path}");
        }

        /// <inheritdoc />
        public void Dispose()
        {
            Connection.Dispose();
        }

        public static Database FromDefault()
        {
            string path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "darts.sqlite");
            return new Database(path);
        }
    }
}
