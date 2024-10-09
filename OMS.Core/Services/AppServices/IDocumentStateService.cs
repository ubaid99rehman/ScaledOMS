using OMS.Core.Models.App;
using System.Collections.Generic;

namespace OMS.Core.Services.AppServices
{
    public interface IDocumentStateService
    {
        void SaveDocumentStates(List<ScreenState> documentStates);
        List<ScreenState> GetDocumentStates();
        bool LoadFile(string fileName);
    }
}
