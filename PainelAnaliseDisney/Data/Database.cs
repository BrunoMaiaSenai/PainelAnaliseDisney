namespace PainelAnaliseDisney.Data
{
    internal static class Database
    {
        // Endpoint da API REST que fornece e integra os dados da Disney
        public const string ApiUrl = "http://apirestmovies.runasp.net/api/movies";

        // Endpoint do seu banco Firebase Realtime Database
        public const string FirebaseUrl = "https://painelanalisedisney-default-rtdb.firebaseio.com/filmes.json";
    }
}
