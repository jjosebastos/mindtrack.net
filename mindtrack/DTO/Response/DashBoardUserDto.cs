using System.Text.Json.Serialization;

namespace mindtrack.DTO.Response
{
  
        public class DashboardUserDto
        {
            [JsonPropertyName("id_user")]
            public int IdUser { get; set; }

            [JsonPropertyName("nome")]
            public string Nome { get; set; }

            [JsonPropertyName("email")]
            public string Email { get; set; }

            [JsonPropertyName("setor")]
            public string Setor { get; set; }

            [JsonPropertyName("competencias")]
            public List<string> Competencias { get; set; }

            [JsonPropertyName("cursos_recomendados")]
            public List<string> CursosRecomendados { get; set; }

            [JsonPropertyName("ultimos_checkins")]
            public List<DashboardCheckinDto> UltimosCheckins { get; set; }

            [JsonPropertyName("indice_humor")]
            public double? IndiceHumor { get; set; }

            // Para capturar o erro retornado pelo PL/SQL caso ocorra
            [JsonPropertyName("erro")]
            public string? Erro { get; set; }
        }

        public class DashboardCheckinDto
        {
            [JsonPropertyName("humor")]
            public string Humor { get; set; }

            [JsonPropertyName("data")]
            public DateTime Data { get; set; }
        }
    
}
