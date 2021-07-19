using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using VersionOriginalApp.Domain.Dtos;
using VersionOriginalApp.Services.Interfaces;

namespace VersionOriginalApp.Services
{
    public class VersionOriginalApiService : IVersionOriginalApiService
    {
        private readonly HttpClient _httpClient;

        public VersionOriginalApiService(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient("versionOriginalApi");
        }

        public async Task<PaginateResultDto<FilmDvdDto>> GetFilmDvds(PaginateParametersDto paginateParameters, DvdStatusDto dvdStatus)
        {
            var response = await _httpClient.GetAsync(
                $"film-dvds?CurrentPage={paginateParameters.CurrentPage}" +
                $"&ItemByPage={paginateParameters.ItemByPage}" +
                //"&Status.Name=DISPONIBLE&Status.Id=c0de22e2-e33c-42cf-8b31-9bb7b94c45e8");
                $"&Status.Id={dvdStatus.Id}");

            if (!response.IsSuccessStatusCode) {
                throw new Exception("Ha ocurrido un error tratando de obtener el listdo de peliculas");
            }

            var content = await response.Content.ReadAsStringAsync();
            var result
                = JsonConvert.DeserializeObject<ResponseDto<PaginateResultDto<FilmDvdDto>>>(content);

            if (result != null && result.IsSuccess())
            {
                return result.Data;
            }

            throw new Exception("Ha ocurrido un error tratando de obtener el listdo de peliculas");
        }

        public async Task<PaginateResultDto<GameDvdDto>> GetGameDvds(PaginateParametersDto paginateParameters, DvdStatusDto dvdStatus)
        {
            var response = await _httpClient.GetAsync(
                $"game-dvds?CurrentPage={paginateParameters.CurrentPage}" +
                $"&ItemByPage={paginateParameters.ItemByPage}" +
                $"&Status.Id={dvdStatus.Id}");

            if (!response.IsSuccessStatusCode) {
                throw new Exception("Ha ocurrido un error tratando de obtener el listdo de peliculas");
            }

            var content = await response.Content.ReadAsStringAsync();
            var result
                = JsonConvert.DeserializeObject<ResponseDto<PaginateResultDto<GameDvdDto>>>(content);

            if (result != null && result.IsSuccess()) {
                return result.Data;
            }

            throw new Exception("Ha ocurrido un error tratando de obtener el listdo de peliculas");
        }

        public async Task<PaginateResultDto<DvdStatusDto>> GetDvdsStatus(PaginateParametersDto paginateParameters)
        {
            var response = await _httpClient.GetAsync(
                $"dvd-status?CurrentPage={paginateParameters.CurrentPage}" +
                $"&ItemByPage={paginateParameters.ItemByPage}");

            if (!response.IsSuccessStatusCode) {
                throw new Exception("Ha ocurrido un error tratando de obtener el listdo de estados");
            }

            var content = await response.Content.ReadAsStringAsync();
            var result
                = JsonConvert.DeserializeObject<ResponseDto<PaginateResultDto<DvdStatusDto>>>(content);

            if (result != null && result.IsSuccess()) {
                return result.Data;
            }

            throw new Exception("Ha ocurrido un error tratando de obtener el listdo de estados");
        }

        public async Task<PaginateResultDto<ClientDto>> GetClients(PaginateParametersDto paginateParameters, ClientStatusDto clientStatus)
        {
            var response = await _httpClient.GetAsync(
                $"clients?CurrentPage={paginateParameters.CurrentPage}" +
                $"&ItemByPage={paginateParameters.ItemByPage}" +
                $"&ClientStatus.Id={clientStatus.Id}");

            if (!response.IsSuccessStatusCode) {
                throw new Exception("Ha ocurrido un error tratando de obtener el listdo de clientes");
            }

            var content = await response.Content.ReadAsStringAsync();
            var result
                = JsonConvert.DeserializeObject<ResponseDto<PaginateResultDto<ClientDto>>>(content);

            if (result != null && result.IsSuccess()) {
                return result.Data;
            }

            throw new Exception("Ha ocurrido un error tratando de obtener el listdo de clients");
        }

        public async Task<PaginateResultDto<ClientStatusDto>> GetClientsStatus(PaginateParametersDto paginateParameters)
        {
            var response = await _httpClient.GetAsync(
                $"client-status?CurrentPage={paginateParameters.CurrentPage}" +
                $"&ItemByPage={paginateParameters.ItemByPage}");

            if (!response.IsSuccessStatusCode) {
                throw new Exception("Ha ocurrido un error tratando de obtener el listdo de estados");
            }

            var content = await response.Content.ReadAsStringAsync();
            var result
                = JsonConvert.DeserializeObject<ResponseDto<PaginateResultDto<ClientStatusDto>>>(content);

            if (result != null && result.IsSuccess()) {
                return result.Data;
            }

            throw new Exception("Ha ocurrido un error tratando de obtener el listdo de estados");
        }

        public async Task<RentSummaryDto> GetRentSummary()
        {
            var response = await _httpClient.GetAsync(
                $"rents/summary");

            if (!response.IsSuccessStatusCode) {
                throw new Exception("Ha ocurrido un error tratando de obtener el resumen de rentas");
            }

            var content = await response.Content.ReadAsStringAsync();
            var result
                = JsonConvert.DeserializeObject<ResponseDto<RentSummaryDto>>(content);

            if (result != null && result.IsSuccess()) {
                return result.Data;
            }

            throw new Exception("Ha ocurrido un error tratando de obtener el resumen de rentas");
        }

        public async Task<RentDto> AddRent(RentDto rent)
        {
            var data = new StringContent(JsonConvert.SerializeObject(rent), Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync(
                "rents", data);

            if (!response.IsSuccessStatusCode) {
                throw new Exception("Ha ocurrido un error tratando de adicionar la renta");
            }

            var content = await response.Content.ReadAsStringAsync();
            var result
                = JsonConvert.DeserializeObject<ResponseDto<RentDto>>(content);

            if (result != null && result.IsSuccess()) {
                return result.Data;
            }

            throw new Exception("Ha ocurrido un error tratando de adicionar la renta");
        }

        public async Task<ReturnDto> ReturnRent(string code)
        {
            var response = await _httpClient.GetAsync($"rents/return/{code}");

            if (!response.IsSuccessStatusCode) {
                throw new Exception("Ha ocurrido un error tratando de hacer la devolución de la renta");
            }

            var content = await response.Content.ReadAsStringAsync();
            var result
                = JsonConvert.DeserializeObject<ResponseDto<ReturnDto>>(content);

            if (result != null && result.IsSuccess()) {
                return result.Data;
            }

            throw new Exception("Ha ocurrido un error tratando de hacer la devolución de la renta");
        }
    }
}
