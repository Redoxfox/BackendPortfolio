namespace ServerAPI.Helpers
{
    public class conversorFechas
    {
        public string yyyymmdd(string fechaformato)
        {
            
            string fechaconvertida = "";
            
            string[] subsCompleta = fechaformato.Split(' ');


            string[] ddmmyyyy = subsCompleta[0].Split('/');

            string dd = ddmmyyyy[0];
            string mm = ddmmyyyy[1];
            string yyyy = ddmmyyyy[2];

            fechaconvertida = yyyy + "-" + mm +"-"+ dd;
         
            return fechaconvertida;
        }
    }
}
