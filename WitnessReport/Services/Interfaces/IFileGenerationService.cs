using WitnessReport.Models;

namespace WitnessReport.Services.Interfaces
{
    public interface IFileGenerationService
    {
        void GenerateFile(Fugitive fugitive);
    }
}
