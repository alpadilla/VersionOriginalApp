using System;
using System.Collections.Generic;
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
                $"&Status.Name={dvdStatus.Name}&Status.Id={dvdStatus.Id}");

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
                $"&ClientStatus.Name={clientStatus.Name}&ClientStatus.Id={clientStatus.Id}");

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
    }
}
