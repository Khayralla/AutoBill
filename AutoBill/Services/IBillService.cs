using AutoBill.Models;
using AutoBill.Models.AutoSaleBillViewModels;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AutoBill.Services
{
    public interface IBillService
    {
        Task<List<SelectListItem>> GetMakesAsync();

        Task<Make> GetMakeAsync(int makeId);

        Task<Make> SaveMakeAsync(string makeName);

        Task<List<Model>> GetModelsAsync(int makeId);

        Task<Model> GetModelAsync(int makeId, int modelId);

        Task<Model> SaveModelAsync(int makeId, string modelName);

        Task<List<SelectListItem>> GetBodyTypesAsync();

        Task<BodyType> GetBodyTypeAsync(int bodyTypeId);

        Task<BodyType> SaveBodyTypeAsync(string bodyTypeName);

        Task<Car> GetCarByVinAsync(string vin);

        Task<bool> SaveCarAsync(Car car);

        Task<Customer> GetCustomerAsync(string firstName, string lastName);

        Task<Customer> GetCustomerAsync(long customerId);

        Task<bool> SaveCustomerAsync(Customer customer);

        Task<Insurance> GetInsuranceAsync(string insuranceName);

        Task<Insurance> GetInsuranceAsync(int insuranceId);

        Task<bool> SaveInsuranceAsync(Insurance insurance);

        Task<SaleBill> GetSaleBillAsync(string firstName, string lastName, string vin, string insuranceName, ApplicationUser user);

        Task<SaleBill> GetSaleBillAsync(long id, ApplicationUser user);

        Task<long> SaveSaleBillAsync(SaleBillViewModel saleBillViewModel, ApplicationUser user);
    }
}
