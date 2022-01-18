namespace WebApiAuthores.Controllers.Entidades
{
    public class Libro
    {
        public int Id { get; set; }
        public string Titulo { get; set; }
        public string descripcion { get; set; } 
        public int AutorId { get; set; }
        public Autor Autor { get; set; }
    }
}