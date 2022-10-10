using System.Text;

namespace DartApplicationLibrary
{
    /// <summary>
    /// Converter for data source to our database.
    ///
    /// Note: This is a one-time use only class. Don't attempt to call its members more than once per instance.
    ///
    /// TODO: Needs a CLI application to help importing
    /// </summary>
    public sealed class DatabaseImporter : IDisposable
    {
        private readonly Stream _sourceStream;

        /// <summary>
        /// Our database.
        /// </summary>
        private readonly Database _ourDatabase;
        
        /// <summary>
        /// Database containing imported definitions.
        /// </summary>
        private readonly Database _memoryDatabase;

        public DatabaseImporter(Stream stream, Database? ourDatabase = null)
        {
            _sourceStream = stream;

            _ourDatabase = ourDatabase ?? Database.FromDefault();
            _memoryDatabase = Database.NewMemoryDatabase();
        }

        public void Import()
        {
            using var command = _memoryDatabase.Connection.CreateCommand();
            command.CommandText = ConvertSql();

            using var reader = command.ExecuteReader();

            do
            {
                reader.Read();
            } while (reader.NextResult());
        }

        public void Export()
        {
            throw new NotImplementedException("This depends on the final implementation of the DB model.");
        }

        /// <summary>
        /// Removes MySql-isms and makes SQL suitable for SQLite.
        /// </summary>
        /// <returns></returns>
        private string ConvertSql()
        {
            StringBuilder sb = new();
            using StreamReader reader = new(this._sourceStream);
            string? line;
            bool isAlterTable = false;

            while ((line = reader.ReadLine()) != null)
            {
                // https://www.sqlite.org/omitted.html; ALTER statements mostly don't work.
                if (line.StartsWith("ALTER TABLE"))
                {
                    isAlterTable = true;
                    continue;
                }
                else if (isAlterTable)
                {
                    if (line.EndsWith(";"))
                    {
                        isAlterTable = false;
                    }
                    
                    continue;
                }
                else if (line.StartsWith("SET ") || line.StartsWith("-- "))
                {
                    continue;
                }
                if (line.StartsWith(") ENGINE"))
                {
                    line = ");";
                }
                else if (line.Contains("COMMENT '"))
                {
                    bool endsWithComma = line.EndsWith(",");
                    
                    line = line.Substring(0, line.IndexOf("COMMENT '", StringComparison.Ordinal));

                    if (endsWithComma)
                    {
                        line += ',';
                    }
                }

                if (line.Contains("\\'"))
                {
                    line = line.Replace("\\'", "''");
                }

                sb.AppendLine(line);
            }

            return sb.ToString();
        }

        public void Dispose()
        {
            _memoryDatabase.Dispose();
            _sourceStream.Dispose();
        }
    }
}
