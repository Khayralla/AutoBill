using AutoBill.Data;
using AutoBill.Models;
using AutoBill.Models.AutoSaleBillViewModels;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AutoBill.Services
{
    public class BillService : IBillService
    {
        private readonly ApplicationDbContext _context;

        public BillService(ApplicationDbContext context)
        {
            _context = context;
        }

        #region Make

        public async Task<bool> MakeExists(int id)
        {
            return await _context.Makes.AnyAsync(e => e.MakeId == id);
        }

        public async Task<Make> GetMakeAsync(int id)
        {
            return await _context.Makes.SingleOrDefaultAsync(c => c.MakeId == id);
        }

        public async Task<Make> GetMakeAsync(string makeName)
        {
            if (makeName != null)
                return await _context.Makes
                                     .Where(c => c.MakeName.ToUpperInvariant() == makeName.ToUpperInvariant())
                                     .SingleOrDefaultAsync();

            return null;
        }

        public async Task<List<SelectListItem>> GetMakesAsync()
        {
            return await _context.Makes
                                 .OrderBy(m => m.MakeName)
                                 .Select(m => new SelectListItem {  Value = m.MakeId.ToString(), Text = m.MakeName})
                                 .ToListAsync();
        }

        public async Task<IEnumerable<Make>> GetMakesListAsync()
        {
            return await _context.Makes
                                 .OrderBy(m => m.MakeName)
                                 .ToListAsync();
        }

        public async Task<Make> SaveMakeAsync(string makeName)
        {
            if (makeName != null)
            {
                var make = new Make { MakeName = makeName };
                await _context.Makes.AddAsync(make);

                var saveResult = await _context.SaveChangesAsync();
                if (saveResult == 1)
                    return make;
            }

            return null;
        }

        public async Task<bool> UpdateMakeAsync(Make make)
        {
            if (make != null)
            {
                _context.Makes.Update(make);
                return await _context.SaveChangesAsync() == 1;
            }

            return false;
        }

        public async Task<bool> DeleteMakeAsync(int id)
        {
            var make = await GetMakeAsync(id);
            if(make != null)
            {
                _context.Makes.Remove(make);
                return await _context.SaveChangesAsync() == 1;
            }

            return false;
        }

        #endregion Make

        #region Model

        public async Task<bool> ModelExists(int modelId)
        {
            return await _context.Models.AnyAsync(e => e.ModelId == modelId);
        }

        public async Task<Model> GetModelAsync(int modelId)
        {
            return await _context.Models.SingleOrDefaultAsync(m => m.ModelId == modelId);
        }

        public async Task<List<Model>> GetModelsAsync()
        {
            return await _context.Models
                            .Join(_context.Makes,
                                  d => d.MakeId,
                                  k => k.MakeId,
                                  (d, k) => new Model
                                  {
                                      MakeId = d.MakeId,
                                      MakeName = k.MakeName,
                                      ModelId = d.ModelId,
                                      ModelName = d.ModelName
                                  }
                                  )
                                  .OrderBy(d => d.ModelName)
                                  .OrderBy(k => k.MakeName)
                            .ToListAsync();
        }

        /*
         * 
         var query_where3 = from k in Makes
                    join d in Models
                    on k.MakeId equals d.MakeId
                    
                    select new
                    {
					MakeId = d.MakeId,
					MakeName = k.MakeName,
					ModelId = d.ModelId,
                     ModelName = d.ModelName,
                     
                    };

foreach (var c in query_where3)
 {
  System.Console.WriteLine(c.MakeId + "   Make: " + c.ModelName + "\t\t\t" + c.ModelId  + "    Model: " + c.MakeName  );
 }

         * 
         */
        public async Task<List<Model>> GetModelsAsync(int makeId)
        {
            return await _context.Models
                                 .Where(m => m.MakeId == makeId)
                                 .OrderBy(m => m.ModelName)
                                 .ToListAsync();
        }

        public async Task<List<SelectListItem>> GetSelectListModelsAsync(int makeId)
        {
            return await _context.Models
                                 .Where(m => m.MakeId == makeId)
                                 .OrderBy(m => m.ModelName)
                                 .Select(m => new SelectListItem { Value = m.ModelId.ToString(), Text = m.ModelName })
                                 .ToListAsync();
        }

        public async Task<Model> GetModelAsync(int makeId, int modelId)
        {
            return await _context.Models
                                 .Where(c => c.MakeId == makeId && c.ModelId == modelId)
                                 .SingleOrDefaultAsync();
        }

        public async Task<Model> AddModelAsync(Model model)
        {
            if (model != null)
            {
                await _context.Models.AddAsync(model);

                var saveResult = await _context.SaveChangesAsync();
                if (saveResult == 1)
                    return model;
            }

            return null;
        }

        public async Task<Model> UpdateModelAsync(Model model)
        {
            if (model != null)
            {
                _context.Models.Update(model);
                var saveResult = await _context.SaveChangesAsync();
                if (saveResult == 1)
                    return model;
            }

            return null;
        }

        public async Task<bool> DeleteModelAsync(int modelId)
        {
            var model = await GetModelAsync(modelId);
            if (model != null)
            {
                _context.Models.Remove(model);
                var saveResult = await _context.SaveChangesAsync();
                return saveResult == 1;
            }

            return false;
        }

        #endregion Model

        #region Body Types

        public async Task<bool> BodyTypeExistsAsync(int id)
        {
            return await _context.BodyTypes.AnyAsync(e => e.BodyTypeId == id);
        }

        public async Task<List<BodyType>> GetBodyTypesListAsync()
        {
            return await _context.BodyTypes
                                 .OrderBy(m => m.BodyTypeName)
                                 .ToListAsync();
        }

        public async Task<List<SelectListItem>> GetBodyTypesAsync()
        {
            return await _context.BodyTypes
                                 .OrderBy(m => m.BodyTypeName)
                                 .Select(m => new SelectListItem { Value = m.BodyTypeId.ToString(), Text = m.BodyTypeName })
                                 .ToListAsync();
        }

        public async Task<BodyType> GetBodyTypeAsync(int id)
        {
            return await _context.BodyTypes
                                 .SingleOrDefaultAsync(b => b.BodyTypeId == id);
        }

        public async Task<bool> AddBodyTypeAsync(BodyType bodyType)
        {
            if (bodyType != null)
            {
                var exists = await _context.BodyTypes.AnyAsync(b => b.BodyTypeName.Trim().ToUpperInvariant() == bodyType.BodyTypeName.Trim().ToUpperInvariant());
                if (exists)
                    return true;

                await _context.BodyTypes.AddAsync(bodyType);
                var saveResult = await _context.SaveChangesAsync();
                return saveResult == 1;
            }

            return false;
        }

        public async Task<bool> UpdateBodyTypeAsync(BodyType bodyType)
        {
            if (bodyType != null)
            {
                _context.BodyTypes.Update(bodyType);
                return await _context.SaveChangesAsync() == 1;
            }

            return false;
        }

        public async Task<bool> DeleteBodyTypeAsync(int id)
        {
            var bodyType = await GetBodyTypeAsync(id);
            if (bodyType != null)
            {
                _context.BodyTypes.Remove(bodyType);
                return 1 == await _context.SaveChangesAsync();
            }
            return false;
        }

        #endregion Body Types

        public async Task<Car> GetCarByVinAsync(string vin)
        {
            if (vin != null)
                return await _context.Cars
                                     .Where(c => c.VIN.ToUpperInvariant() == vin.ToUpperInvariant())
                                     .SingleOrDefaultAsync();

            return null;
        }

        public async Task<bool> SaveCarAsync(Car car)
        {
            if (car != null)
            {
                await _context.Cars.AddAsync(car);
                var saveResult = await _context.SaveChangesAsync();
                return saveResult == 1;
            }

            return false;
        }

        public async Task<Customer> GetCustomerAsync(string firstName, string lastName)
        {
            if (firstName != null && lastName != null)
            {
                return await _context.Customers
                                     .Where(c => c.FirstName.ToUpperInvariant() == firstName.ToUpperInvariant() && c.LastName.ToUpperInvariant() == lastName.ToUpperInvariant())
                                     .FirstOrDefaultAsync();
            }

            return null;
        }

        public async Task<Customer> GetCustomerAsync(long customerId)
        {
            var customers = await _context.Customers.ToListAsync();
            return await _context.Customers
                                 .Where(c => c.Id == customerId)
                                 .FirstOrDefaultAsync();
        }

        public async Task<bool> SaveCustomerAsync(Customer customer)
        {
            if (customer != null)
            {
                await _context.Customers.AddAsync(customer);

                var saveResult = await _context.SaveChangesAsync();
                return saveResult == 1;
            }

            return false;
        }

        public async Task<Insurance> GetInsuranceAsync(int insuranceId)
        {
            var insurances = await _context.Insurances.ToListAsync();

            return await _context.Insurances
                                 .Where(c => c.Id == insuranceId)
                                 .FirstOrDefaultAsync();
        }

        public async Task<Insurance> GetInsuranceAsync(string insuranceName)
        {
            if (insuranceName != null)
            {
                return await _context.Insurances
                         .Where(c => c.InsuranceName.ToUpperInvariant() == insuranceName.ToUpperInvariant())
                         .FirstOrDefaultAsync();
            }

            return null;
        }

        public async Task<bool> SaveInsuranceAsync(Insurance insurance)
        {
            if (insurance != null)
            {
                await _context.Insurances.AddAsync(insurance);
                var saveResult = await _context.SaveChangesAsync();
                return saveResult == 1;
            }

            return false;
        }

        public async Task<SaleBill> GetSaleBillAsync(long id, ApplicationUser user)
        {
            if (user == null)
                return null;

            return await _context.SaleBills
                                 .Where(s => s.Id == id && s.AgentId == user.Id)
                                 .FirstOrDefaultAsync();
        }

        public async Task<SaleBill> GetSaleBillAsync(string firstName, string lastName, string vin, string insuranceName, ApplicationUser user)
        {
            if (firstName != null && lastName != null && vin != null && insuranceName != null)
            {
                var car = await GetCarByVinAsync(vin);
                if (car == null)
                    return null;

                var customer = await GetCustomerAsync(firstName, lastName);
                if (customer == null)
                    return null;

                var insurance = await GetInsuranceAsync(insuranceName);
                if (insurance == null)
                    return null;

                return await _context.SaleBills
                                      .Where(s => s.AgentId == user.Id && s.CarVin == car.VIN && s.InsuranceId == insurance.Id && s.CustomerId == customer.Id)
                                      .FirstOrDefaultAsync();
            }

            return null;
        }

        public async Task<long> SaveSaleBillAsync(SaleBillViewModel saleBillViewModel, ApplicationUser user)
        {
            if (saleBillViewModel != null)
            {
                var saleBill = new SaleBill();

                if (saleBillViewModel.Car == null)
                    return -1;

                var make = await GetMakeAsync(saleBillViewModel.Car.MakeId);
                if (make == null)
                {
                }

                var model = await GetModelAsync(make.MakeId, saleBillViewModel.Car.ModelId);
                if (model == null)
                {
                }

                var bodyType = await GetBodyTypeAsync(saleBillViewModel.Car.BodyTypeId);
                if (bodyType == null)
                {
                }

                var car = await GetCarByVinAsync(saleBillViewModel.Car.VIN);
                if (car == null)
                {
                    car = MapCar(saleBillViewModel.Car);
                    if (car == null)
                        return -1;

                    car.MakeId = make.MakeId;
                    car.ModelId = model.ModelId;
                    car.BodyTypeId = bodyType.BodyTypeId;

                    var saveResult = await SaveCarAsync(car);
                    if (!saveResult)
                        return -1;
                }

                saleBill.CarVin = car.VIN;

                if (saleBillViewModel.Customer == null)
                    return -1;

                var customer = await GetCustomerAsync(saleBillViewModel.Customer.FirstName, saleBillViewModel.Customer.LastName);
                if (customer == null)
                {
                    customer = MapCustomer(saleBillViewModel.Customer);
                    var saveResult = await SaveCustomerAsync(customer);
                    if (saveResult)
                        saleBill.CustomerId = customer.Id;
                    else
                        return -1;
                }

                if (saleBillViewModel.Insurance == null)
                    return -1;

                var insurance = await GetInsuranceAsync(saleBillViewModel.Insurance.InsuranceName);
                if (insurance == null)
                {
                    insurance = MapInsurance(saleBillViewModel.Insurance);
                    var saveResult = await SaveInsuranceAsync(insurance);
                    if (saveResult)
                        saleBill.InsuranceId = insurance.Id;
                    else
                        return -1;
                }

                if (saleBillViewModel.SaleBill == null)
                    return -1;

                saleBill.AgentId = user.Id;
                saleBill.Price = saleBillViewModel.SaleBill.Price;
                saleBill.Tax = saleBillViewModel.SaleBill.Tax;
                saleBill.Total = saleBillViewModel.SaleBill.Total;
                saleBill.SalesTax = saleBillViewModel.SaleBill.SalesTax;
                saleBill.PaymentForm = saleBillViewModel.SaleBill.PaymentForm;

                await _context.SaleBills.AddAsync(saleBill);
                if (await _context.SaveChangesAsync() == 1)
                    return saleBill.Id;
            }

            return -1;
        }

        private static Car MapCar(CarViewModel carViewModel)
        {
            if (carViewModel == null)
                return null;

            return new Car
            {
                VIN = carViewModel.VIN,
                Year = carViewModel.Year,
                Color = carViewModel.Color,
                Kilometres = carViewModel.Kilometres,
                DateAdded = DateTime.Now,
                Odometer = carViewModel.Odometer,
                Sold = true
            };
        }

        private static Customer MapCustomer(CustomerViewModel customerViewModel)
        {
            if (customerViewModel == null)
                return null;

            return new Customer
            {
                FirstName = customerViewModel.FirstName,
                LastName = customerViewModel.LastName,
                Address = customerViewModel.Address
            };
        }

        private static Insurance MapInsurance(InsuranceViewModel insuranceViewModel)
        {
            if (insuranceViewModel == null)
                return null;

            return new Insurance
            {
                InsuranceName = insuranceViewModel.InsuranceName
            };
        }

    }
}