namespace BusinessClientsManager.Models;

public class BusinessClient
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Address { get; set; }
    public Postcode Postcode { get; set; }
}
