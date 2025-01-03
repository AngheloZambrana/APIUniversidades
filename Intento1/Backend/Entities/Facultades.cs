namespace Backend.Entities;

public class Facultades 
{
    public int FacultadID { get; set; }
    public string? Nombre { get; set; }
    public string? Descripcion { get; set; }
    //Foreign Key
    public int UniversidadID { get; set; }

}