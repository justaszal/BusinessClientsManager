using Newtonsoft.Json;

namespace BusinessClientsManager.Models;

public class GetAddressResponse
{
    public string Status { get; set; }
    public bool Success { get; set; }
    public string Message { get; set; }
    public int MessageCode { get; set; }
    public int Total { get; set; }
    public List<AddressResponse> Data { get; set; }

}
