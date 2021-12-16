using System;

namespace API.Data
{
    public class Pelicula
    {
        public string Imagen { get; set; }
        public string  Titulo { get; set; }
        public DateTime FechaCreacion { get; set; }
        public int Calificacion { get; set; }
    }
}
