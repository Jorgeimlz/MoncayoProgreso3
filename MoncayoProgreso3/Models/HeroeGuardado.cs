using SQLite;

namespace MoncayoProgreso3.Models
{
    public class HeroeGuardado
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        public string Nombre { get; set; }
        public string Poder { get; set; }
        public string ImagenUrl { get; set; }
    }
}

