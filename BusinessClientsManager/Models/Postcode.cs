namespace BusinessClientsManager.Models;

public class Postcode
{
    public string Name { get; set; }
    public string City { get; set; }
    public ICollection<BusinessClient> BusinessClients { get; set; }
}
