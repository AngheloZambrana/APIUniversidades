namespace Backend.DTOs.WithID;

public class CarrerasDTO
{
    public int CarrerasID { get; set; }
    public string? Nombre { get; set; }
    public int Duracion { get; set; }
    //Foreign Key
    public int FacultadID { get; set; }
}