using Backend.DTOs.WithID;
using Backend.DTOs.WithoutID;
using Backend.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Controllers;

[ApiController]
[Route ("[controller]")]
public class FacultadesController : ControllerBase
{
    private readonly IFacultadesServices _facultadesServices;

    public FacultadesController(IFacultadesServices facultadesServices)
    {
        _facultadesServices = facultadesServices;
    }

    [HttpGet]
    public ActionResult<List<FacultadesDTO>> GetAllFacultades()
    {
        var result = _facultadesServices.GetAllFacultades();
        return Ok(result);
    }

    [HttpGet("{id}")]
    public ActionResult<FacultadesDTO> GetFacultadeById(int id)
    {
        var result = _facultadesServices.GetFacultadeById(id);
        return Ok(result);
    }
    
    [HttpPost]
    public ActionResult<FacultadesDTO> CreateFacultades(FacultadesPostDTO facultades)
    {
        var result = _facultadesServices.CreateFacultades(facultades);
        return Ok(result);
    }

    [HttpPut("{id}")]
    public ActionResult<FacultadesDTO> UpdateFacultades(int id, [FromBody] FacultadesDTO facultades)
    {
        if (id != facultades.FacultadID)
        {
            return BadRequest("El ID de la URL no coincide con el ID del cuerpo.");
        }
        var result = _facultadesServices.UpdateFacultades(facultades);
        return Ok(result);
    }
}