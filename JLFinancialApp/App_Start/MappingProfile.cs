using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AutoMapper;
using JLFinancialApp.Models;
using JLFinancialApp.Models.DTOs;
using JLFinancialApp.Models.ViewModels;

namespace JLFinancialApp.App_Start
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // Subscription maps
            CreateMap<Subscription, SubscriptionDTO>();
            CreateMap<SubscriptionDTO, Subscription>()
                .ForMember(subscription => subscription.Id, opt => opt.Ignore());

            CreateMap<Subscription, SubscriptionFormViewModel>();


            // Period Type maps
            CreateMap<PeriodType, PeriodTypeDTO>();

        }
    }
}

