namespace API.Models
{
    public class MovieAndCharactersDTO
    {
        public int IdMovie { get; set; }
        public int IdCharacter { get; set; }
    }
    public class UpdateMovieAndCharactersDTO : MovieAndCharactersDTO
    {

    }
}
