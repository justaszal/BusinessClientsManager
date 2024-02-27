using BusinessClientsManager.Data;
using BusinessClientsManager.Models;
using Newtonsoft.Json;

namespace BusinessClientsManager.Services;

public class BusinessClientService : IBusinessClientService
{

    private readonly IClientRepo _clientRepo;
    private readonly IConfiguration _configuration;
    private readonly IHttpClientFactory _httpClientFactory;

    public BusinessClientService(IClientRepo clientRepo, IConfiguration configuration, IHttpClientFactory httpClientFactory)
    {
        _clientRepo = clientRepo;
        _configuration = configuration;
        _httpClientFactory = httpClientFactory;
    }

    public async Task<bool> InsertBusinessClients(IEnumerable<BusinessClient> clients)
    {
        try
        {
            var filteredClients = await _clientRepo.FilterDuplicateClients(clients);
            if (filteredClients.Count() > 0)
            {
                await _clientRepo.InsertClients(filteredClients);
                return await _clientRepo.SaveChanges();
            }

            return false;
        } catch (Exception ex)
        {
            throw new Exception("Operation failed. Business clients were not inserted");
        }
    }

    public async Task<bool> UpdatePostCodes()
    {
        try
        {
            string? httpClientName = _configuration["APIs:PostIt:ClientName"];
            string? key = _configuration["APIs:PostIt:Key"];
            HttpClient httpClient = _httpClientFactory.CreateClient(httpClientName ?? "");

            var clients = await _clientRepo.GetBusinessClientsWithoutPostCode();

            foreach (var client in clients)
            {
                var resp = await httpClient.GetFromJsonAsync<GetAddressResponse>($"/?term={client.Address}&key={key}");

                // TODO: Count updated post codes and return the count
                if (resp != null && resp.Success && resp.Total > 0)
                {
                    var addressResponse = resp.Data[0];
                    await _clientRepo.UpdateClientPostcode(client.Id, addressResponse.PostCode, addressResponse.City);
                }
            }
            await _clientRepo.SaveChanges();
            return true;
        } catch (Exception ex)
        {
            throw new Exception("Operation failed. Post code was not updated");
        }
    }
}
