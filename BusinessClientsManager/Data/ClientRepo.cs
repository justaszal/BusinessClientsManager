using BusinessClientsManager.Models;
using Microsoft.EntityFrameworkCore;

namespace BusinessClientsManager.Data;

public class ClientRepo : IClientRepo
{
    private readonly ClientDBContext _context;

    public ClientRepo(ClientDBContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<BusinessClient>> FilterDuplicateClients(IEnumerable<BusinessClient> clients)
    {
        var filtertedClients = new List<BusinessClient>();

        foreach (var c in clients)
        {
            if (!await _context.BusinessClients.AnyAsync(bc => bc.Name == c.Name.Trim() || bc.Address == c.Address.Trim()))
            {
                filtertedClients.Add(c);
            }
        }

        return filtertedClients;
    }

    public async Task InsertClients(IEnumerable<BusinessClient> clients)
    {
        await _context.BusinessClients.AddRangeAsync(clients);
    }

    public async Task<IEnumerable<BusinessClient>> GetBusinessClientsWithoutPostCode()
    {
        return await _context.BusinessClients.Where(x => x.Postcode == null).ToListAsync();
    }

    public async Task<IEnumerable<BusinessClient>> GetBusinessClients(int from, int count)
    {
        return await _context.BusinessClients.Skip(from).Take(count).ToListAsync();
    }

    public async Task<bool> UpdateClientPostcode(int id, string postCode, string city)
    {
        var client = await _context.BusinessClients.FirstOrDefaultAsync(x => x.Id == id);

        if (client is null)
        {
            return false;
        }

        var postCodeEntity = await _context.Postcodes.FirstOrDefaultAsync(x => x.Name == postCode);

        if (postCodeEntity is null)
        {
            postCodeEntity = await AddPostcode(postCode, city);
            postCodeEntity.BusinessClients.Add(client);
            return true;
        } else
        {
            await AddPostcode(postCode, city);
            return true;
        }
    }

    public async Task<Postcode> AddPostcode(string name, string city)
    {
        var postCode = new Postcode { Name = name, City = city };
        await _context.Postcodes.AddAsync(postCode);
        return postCode;
    }

    public async Task<int> SaveChanges()
    {

        return await _context.SaveChangesAsync();
    }
}
