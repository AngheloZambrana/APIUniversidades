namespace Backend.DTOs.WithoutID;

public class BecaPostDTO 
{
    public string? Nombre { get; set; }
    public string? Descripcion { get; set; }
    public string? Criterios { get; set; }
    //Foreign Key
    public int UniversidadID { get; set; }
}