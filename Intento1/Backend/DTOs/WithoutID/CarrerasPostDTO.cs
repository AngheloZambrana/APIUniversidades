namespace Backend.DTOs.WithoutID;

public class CarrerasPostDTO
{
    public string? Nombre { get; set; }
    public int Duracion { get; set; }
    //Foreign Key
    public int FacultadID { get; set; }
}