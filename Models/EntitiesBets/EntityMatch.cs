namespace ServerAPI.Models.EntitiesBets
{
    public class EntityMatch
    {
        public int id { get; set; }

        public int nro_fecha { get; set; }

        public int id_liga { get; set; }

        public int equipo_1 { get; set; }

        public int gol_eq1 { get; set; }

        public int equipo_2 { get; set; }

        public int gol_eq2 { get; set; }

        public int local_eq1 { get; set; }
        public int local_eq2 { get; set; }
        public string estado { get; set; }

        public string temporada { get; set; }

        public string fecha { get; set; }
    }
}
