using System.Threading.Tasks;
using VersionOriginalApp.Domain.Dtos;

namespace VersionOriginalApp.Services.Interfaces
{
    public interface IVersionOriginalApiService
    {
        Task<PaginateResultDto<FilmDvdDto>> GetFilmDvds(PaginateParametersDto paginateParameters,
            DvdStatusDto dvdStatus);

        Task<PaginateResultDto<GameDvdDto>> GetGameDvds(PaginateParametersDto paginateParameters,
            DvdStatusDto dvdStatus);

        Task<PaginateResultDto<DvdStatusDto>> GetDvdsStatus(PaginateParametersDto paginateParameters);

        Task<PaginateResultDto<ClientDto>> GetClients(PaginateParametersDto paginateParameters,
            ClientStatusDto clientStatus);

        Task<PaginateResultDto<ClientStatusDto>> GetClientsStatus(PaginateParametersDto paginateParameters);
        
        Task<RentSummaryDto> GetRentSummary();

        Task<RentDto> AddRent(RentDto rent);
        Task<ReturnDto> ReturnRent(string code);
    }
}
