namespace Backend.DTOs.WithoutID;

public class FacultadesPostDTO
{
    public string? Nombre { get; set; }
    public string? Descripcion { get; set; }
    //Foreign Key
    public int UniversidadID { get; set; }
}