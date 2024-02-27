using AutoMapper;
using BusinessClientsManager.Data;
using BusinessClientsManager.Models;
using BusinessClientsManager.Services;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net.Mime;

namespace BusinessClientsManager.Controllers;

[ApiController]
[Route("api/business-clients")]
public class BusinessClientsController:  ControllerBase
{
    private readonly IClientRepo _clientRepo;
    private readonly IBusinessClientService _clientService;
    private readonly IMapper _mapper;

    public BusinessClientsController(IBusinessClientService clientService, IClientRepo clientRepo, IMapper mapper)
    {
        _clientRepo = clientRepo;
        _clientService = clientService;
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<IActionResult> Get([FromQuery] int from=0, [FromQuery] int count=10)
    {
        try
        {
            var clients = await _clientRepo.GetBusinessClients(from, count);
            var clientsResp = _mapper.Map<IEnumerable<BusinessClient>, List<GetBusinessClientsResponse>>(clients);
            return Ok(clientsResp);
        } catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPut("postcode")]
    public async Task<IActionResult> UpdatePostCode()
    {
        try
        {
            var updatedCount = await _clientService.UpdatePostCodes();
            return Ok($"Updated records {updatedCount}");
        } catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }


    [HttpPost]
    public async Task<IActionResult> Post([FromForm] CreateBusinessClientsRequest request)
    {
        if (request.ClientsFile.ContentType != MediaTypeNames.Application.Json)
        {
            return BadRequest("Invalid file type. The file type must be json.");
        }

        try
        {
            using (StreamReader r = new StreamReader(request.ClientsFile.OpenReadStream()))
            {
                string json = await r.ReadToEndAsync();
                List<BusinessClient>? clients = JsonConvert.DeserializeObject<List<BusinessClient>>(json);

                if (clients is null)
                {
                    return BadRequest("Invalid file content. The file content must be a valid json array of business clients.");
                }

                var isInserted = await _clientService.InsertBusinessClients(clients);

                if (isInserted)
                {
                    return Created();
                }

                return Ok();
            }
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
}
