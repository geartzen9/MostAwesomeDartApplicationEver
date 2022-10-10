using System.IO;
using System.Linq;
using System.Reflection;
using DartApplicationLibrary;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DartApplicationLibraryTests
{
    [TestClass]
    public class DatabaseImporterTest
    {
        [TestMethod]
        public void TestSqlImport()
        {
            Assembly assembly = Assembly.GetAssembly(typeof(DatabaseImporterTest))!;
            
            string testDataPath = assembly.GetManifestResourceNames().Single(
                s => s.EndsWith("DartsData.sql")
            );

            using Stream? stream = assembly.GetManifestResourceStream(testDataPath);

            if (stream == null)
            {
                Assert.Fail("DartsData.sql does not exist.");
            }

            using DatabaseImporter importer = new(stream!, Database.NewMemoryDatabase());
            importer.Import();
        }
    }
}
