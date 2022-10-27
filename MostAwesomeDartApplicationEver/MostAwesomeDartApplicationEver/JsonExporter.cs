using Microsoft.Win32;
using MostAwesomeDartApplicationEver.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;

namespace MostAwesomeDartApplicationEver
{
    internal class JsonExporter
    {
        public static readonly JsonExporter Instance = new();

        private JsonSerializerOptions _serializerOptions;

        public JsonExporter()
        {
            _serializerOptions = new JsonSerializerOptions 
            {
                WriteIndented = true,

            };
        }

        public string Export(Match match) => JsonSerializer.Serialize((Match.Serializable)match, _serializerOptions);

        public string Export(IEnumerable<Match> matches) => JsonSerializer.Serialize(matches.Cast<Match.Serializable>, _serializerOptions);

        public string Export<T>(T exportable) => JsonSerializer.Serialize(exportable, _serializerOptions);

        public void SerializeAndSave<T>(T matches) where T : notnull
        {
            string myDocuments = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);

            SaveFileDialog saveFileDialog = new()
            {
                DefaultExt = "json",
                Filter = "Dartwedstrijden (*.json)|*.json",
                FileName = System.IO.Path.Combine(myDocuments, "dartsdata.json"),
                InitialDirectory = myDocuments,
                OverwritePrompt = true,
                RestoreDirectory = true
            };

            if (saveFileDialog.ShowDialog() != true)
            {
                return;
            }

            // Hack to force dynamic dispatch for overloads of Export.
            // See https://stackoverflow.com/a/25685515.
            dynamic @dynamic = matches;

            System.IO.File.WriteAllText(saveFileDialog.FileName, Export(@dynamic));
        }
    }
}
