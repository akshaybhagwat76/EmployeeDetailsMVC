using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApplication.EDM;
using WebApplication.ViewModel;
namespace WebApplication.Repository
{

    public class CommonRepository
    {
        private EmployeeEntities db = new EmployeeEntities();

        public List<CountryVM> GetCountries()
        {
            try
            {
                var lstCountry = db.Countries.ToList();
                return Mapper.Map<List<Country>, List<CountryVM>>(lstCountry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<StateVM> GetStates()
        {
            try
            {
                var lstCountry = db.States.ToList();
                return Mapper.Map<List<State>, List<StateVM>>(lstCountry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public List<CityVM> GetCities()
        {
            try
            {
                var lstCountry = db.Cities.ToList();
                return Mapper.Map<List<City>, List<CityVM>>(lstCountry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public List<StateVM> GetStates(int id)
        {
            try
            {
                List<State> listStates = db.States.Where(x => x.CountryId == id).ToList();
                return Mapper.Map<List<State>, List<StateVM>>(listStates);
            }
            catch (Exception ex)
            {
               
                throw ex;
            }
        }
        public List<CityVM> GetCities(int id)
        {
            try
            {
                List<City> listCities = db.Cities.Where(x => x.StateId == id).ToList();
                return Mapper.Map<List<City>, List<CityVM>>(listCities);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
            
    }
}