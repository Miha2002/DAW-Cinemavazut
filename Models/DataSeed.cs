using Microsoft.EntityFrameworkCore;
using Proiect_DAW_Cinemavazut.ContextModels;


namespace Proiect_DAW_Cinemavazut.Models
{
    public class DataSeed
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new CinemavazutContext(
                serviceProvider.GetRequiredService<
                    DbContextOptions<CinemavazutContext>>()))
            {
                if (context.Filme.Any())
                {
                    return;
                }

                if (context.Categorii_filme.Any())
                {
                    return;
                }

                if (context.Utilizatori.Any())
                {
                    return;
                }
            }
        }
    }
}
