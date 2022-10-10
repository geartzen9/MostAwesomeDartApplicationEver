using System.Text;

namespace DartApplicationLibrary
{
    /// <summary>
    /// Converter for data source to our database.
    ///
    /// TODO: Needs a CLI application to help importing
    /// </summary>
    public sealed class DatabaseImporter : IDisposable
    {
        private readonly string _sourceFile;

        /// <summary>
        /// Our database.
        /// </summary>
        private readonly Database _ourDatabase;
        
        /// <summary>
        /// Database containing imported definitions.
        /// </summary>
        private readonly Database _memoryDatabase;
        
        public DatabaseImporter(string sourceFile, Database? ourDatabase = null)
        {
            if (File.Exists(sourceFile))
            {
                throw new FileNotFoundException("Source file does not exist!");
            }
            
            _sourceFile = sourceFile;

            _ourDatabase = ourDatabase ?? Database.FromDefault();
            _memoryDatabase = new Database(":memory:");
        }

        public DatabaseImporter(string sourceFile, string ourDatabase) : this(sourceFile, new Database(ourDatabase)) {}

        public void Import()
        {
            using var command = _memoryDatabase.Connection.CreateCommand();
            command.CommandText = ConvertSql(this._sourceFile);
            command.Prepare();
            command.ExecuteReader();
        }

        public void Export()
        {
            throw new NotImplementedException("This depends on the final implementation of the DB model.");
        }

        /// <summary>
        /// Removes MySql-isms and makes SQL suitable for SQLite.
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        private string ConvertSql(string path)
        {
            string[] lines = File.ReadAllLines(path);
            StringBuilder sb = new();

            for (int i = 0; i < lines.Length; i++)
            {
                string line = lines[i];
                if (line == ") ENGINE=InnoDB DEFAULT CHARSET=utf8;")
                {
                    line = ");";
                }
                else if (line.StartsWith("SET ") || line.StartsWith("-- "))
                {
                    continue;
                }

                sb.AppendLine(line);
            }

            return sb.ToString();
        }

        public void Dispose()
        {
            _memoryDatabase.Dispose();
        }
    }
}
