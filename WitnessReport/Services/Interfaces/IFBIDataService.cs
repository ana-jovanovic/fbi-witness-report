using System.Threading.Tasks;
using WitnessReport.Models;

namespace WitnessReport.Services.Interfaces
{
    public interface IFbiDataService
    {
        Task<Fugitive> GetMostWanted(string title);
    }
}
