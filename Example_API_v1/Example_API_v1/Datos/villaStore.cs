using Example_API_v1.Models.DTO;

namespace Example_API_v1.Store
{
    public static class villaStore
    {
        public static List<villaDto> villaList = new List<villaDto>
        {
            new villaDto { Id = 1, Nombre = "Bosquemar", Ocupantes=3, MetrosCuadrados=50 },
            new villaDto { Id = 2, Nombre = "Palo Alto" ,Ocupantes=6, MetrosCuadrados=70 },
        };
    }
}
