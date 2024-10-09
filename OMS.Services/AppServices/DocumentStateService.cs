using OMS.Core.Models.App;
using OMS.Core.Services.AppServices;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml.Serialization;

namespace OMS.Services.AppServices
{
    public class DocumentStateService : IDocumentStateService
    {
        private string _fileName;

        public DocumentStateService()
        {
        }

        public bool LoadFile(string fileName)
        {
            _fileName = fileName;
            if (!File.Exists(_fileName))
            {
                using (var fileStream = File.Create(fileName))
                {
                }
                SaveDocumentStates(new List<ScreenState>()); // Initialize with an empty list to avoid deserialization issues
            }
            return true;
        }
        public void SaveDocumentStates(List<ScreenState> documentStates)
        {
            var serializer = new XmlSerializer(typeof(List<ScreenState>));

            using (var writer = new StreamWriter(_fileName))
            {
                serializer.Serialize(writer, documentStates);
            }
        }
        public List<ScreenState> GetDocumentStates()
        {
            var serializer = new XmlSerializer(typeof(List<ScreenState>));
            using (var reader = new StreamReader(_fileName))
            {
                if (reader.Peek() == -1)
                {
                    return new List<ScreenState>(); // Return empty list if file is empty
                }
                return (List<ScreenState>)serializer.Deserialize(reader);
            }
        }
    }

}
