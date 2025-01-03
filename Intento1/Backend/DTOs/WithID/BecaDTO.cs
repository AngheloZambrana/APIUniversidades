namespace Backend.DTOs.WithID;

public class BecaDTO 
{
    public int BecaID { get; set; }
    public string? Nombre { get; set; }
    public string? Descripcion { get; set; }
    public string? Criterios { get; set; }
    //Foreign Key
    public int UniversidadID { get; set; }
}