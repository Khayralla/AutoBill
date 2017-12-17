using AutoBill.Services;
using System.Linq;
using System.Threading.Tasks;

namespace AutoBill.Data
{
    public static class DatabaseSetup
    {
        //public static async Task EnsureDatabaseNotEmptyAsync()
        //{
        //    var dbContext = new DesignTimeDbContextFactory().CreateDbContext(null);
        //    if (dbContext != null)
        //    {
        //        if (dbContext.Makes.Any())
        //            return;   // DB has been seeded.

        //        string makeName = "Honda";
        //        var service = new BillService(dbContext);
        //        //var make = await service.GetMakeAsync(makeName);
        //        //if (make != null && !string.IsNullOrEmpty(make.MakeName))
        //        //    return;

        //        var make = await service.SaveMakeAsync(makeName);
        //        if (make != null)
        //        {
        //            await service.SaveModelAsync(make.MakeId, "Civic GX");
        //            await service.SaveModelAsync(make.MakeId, "Accord");
        //        }

        //        await service.SaveBodyTypeAsync("4-door Sedan");
        //        await service.SaveBodyTypeAsync("SUV");

        //        makeName = "Toyota";
        //        make = await service.SaveMakeAsync(makeName);
        //        if (make != null)
        //        {
        //            await service.SaveModelAsync(make.MakeId, "Corolla");
        //            await service.SaveModelAsync(make.MakeId, "Crown");
        //        }

        //        makeName = "Nissan";
        //        make = await service.SaveMakeAsync(makeName);
        //        if (make != null)
        //        {
        //            await service.SaveModelAsync(make.MakeId, "Maxima");
        //            await service.SaveModelAsync(make.MakeId, "Altima");
        //        }

        //    }
        //}
    }
}
