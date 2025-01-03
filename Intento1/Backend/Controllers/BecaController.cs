using Backend.DTOs.WithID;
using Backend.DTOs.WithoutID;
using Backend.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Controllers;

[ApiController]
[Route("[controller]")]
public class BecaController : ControllerBase
{
    private readonly IBecaServices _becaServices;

    public BecaController(IBecaServices becaServices)
    {
        _becaServices = becaServices;
    }

    [HttpGet]
    public ActionResult<List<BecaDTO>> GetAllBecas()
    {
        var result = _becaServices.GetAllBecas();
        return Ok(result);
    }

    [HttpGet("{id}")]
    public ActionResult<BecaDTO> GetBecaById(int id)
    {
        var result = _becaServices.GetBecaById(id);
        return Ok(result);
    }

    [HttpPost]
    public ActionResult<BecaDTO> CreateBeca(BecaPostDTO beca)
    {
        var result = _becaServices.CreateBeca(beca);
        return Ok(result);
    }

    [HttpPut("{id}")]
    public ActionResult<BecaDTO> UpdateBeca(int id, [FromBody] BecaDTO beca)
    {
        if (id != beca.BecaID)
        {
            return BadRequest("El ID de la URL no coincide con el ID del cuerpo.");
        }

        var result = _becaServices.UpdateBeca(beca);
        return Ok(result);
    }
}
