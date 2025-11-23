using System.Text.Json.Serialization;

namespace mindtrack.Enums
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum StatusHumorEnum
    {
        FELIZ,
        NEUTRO,
        TRISTE,
        RAIVA,
    }
}
