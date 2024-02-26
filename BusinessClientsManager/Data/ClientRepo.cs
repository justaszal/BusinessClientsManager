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

    public async Task<IEnumerable<BusinessClient>> GetBusinessClients(int from, int count)
    {
        return await _context.BusinessClients.Skip(from).Take(count).ToListAsync();
    }

    public Task UpdateClient(int id)
    {
        throw new NotImplementedException();
    }

    public async Task<bool> SaveChanges()
    {
        
        return await _context.SaveChangesAsync() > 0;
    }
}
