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
                .ForMember(subscription => subscription.Id, opt => opt.Ignore())
                .ForMember(subscription => subscription.PeriodType, opt => opt.Ignore());

            CreateMap<Subscription, RecurringAmountFormViewModel>();

            // Income Maps
            CreateMap<Income, IncomeDTO>();
            CreateMap<IncomeDTO, Income>()
                .ForMember(income => income.Id, opt => opt.Ignore())
                .ForMember(income => income.PeriodType, opt => opt.Ignore());

            CreateMap<Income, RecurringAmountFormViewModel>();

            // Period Type maps
            CreateMap<PeriodType, PeriodTypeDTO>();
            CreateMap<PeriodTypeDTO, PeriodType>();

        }
    }
}

