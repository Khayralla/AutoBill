using AutoBill.Models;
using AutoBill.Models.AutoSaleBillViewModels;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AutoBill.Services
{
    public interface IBillService
    {
        #region Make

        Task<bool> MakeExists(int id);

        Task<Make> GetMakeAsync(int makeId);

        Task<Make> GetMakeAsync(string makeName);

        Task<List<SelectListItem>> GetMakesAsync();

        Task<IEnumerable<Make>> GetMakesListAsync();

        Task<Make> SaveMakeAsync(string makeName);

        Task<bool> UpdateMakeAsync(Make make);

        Task<bool> DeleteMakeAsync(int id);

        #endregion Make

        #region Model

        Task<bool> ModelExists(int modelId);

        Task<Model> GetModelAsync(int modelId);

        Task<List<Model>> GetModelsAsync();

        Task<List<Model>> GetModelsAsync(int makeId);

        Task<List<SelectListItem>> GetSelectListModelsAsync(int makeId);

        Task<Model> GetModelAsync(int makeId, int modelId);

        Task<Model> AddModelAsync(Model model);

        Task<Model> UpdateModelAsync(Model model);

        Task<bool> DeleteModelAsync(int modelId);

        #endregion Model

        #region Body Types

        Task<bool> BodyTypeExistsAsync(int id);

        Task<List<BodyType>> GetBodyTypesListAsync();

        Task<List<SelectListItem>> GetBodyTypesAsync();

        Task<BodyType> GetBodyTypeAsync(int id);

        Task<bool> AddBodyTypeAsync(BodyType bodyType);

        Task<bool> UpdateBodyTypeAsync(BodyType bodyType);

        Task<bool> DeleteBodyTypeAsync(int id);

        #endregion Body Types

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
