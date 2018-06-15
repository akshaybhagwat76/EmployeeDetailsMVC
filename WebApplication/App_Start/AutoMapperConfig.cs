using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApplication.EDM;
using WebApplication.ViewModel;

namespace WebApplication.App_Start
{
    public class AutoMapperConfig
    {
        public static void Initialize()
        {
            Mapper.Initialize(cfg =>
            {
                cfg.CreateMap<CountryVM, Country>();
                cfg.CreateMap<Country, CountryVM>();

                cfg.CreateMap<StateVM, State>();
                cfg.CreateMap<State, StateVM>();

                cfg.CreateMap<CityVM, City>();
                cfg.CreateMap<City, CityVM>();

                cfg.CreateMap<RoleVM, Role>();
                cfg.CreateMap<Role, Role>();

                cfg.CreateMap<UserVM, user>();
                cfg.CreateMap<user, UserVM>();
            });
        }
    }
}