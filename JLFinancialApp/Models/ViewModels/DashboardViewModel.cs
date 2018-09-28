using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using JLFinancialApp.Models.DTOs;

namespace JLFinancialApp.Models.ViewModels
{
    public class DashboardViewModel
    {
        public IOrderedEnumerable<IncomeDTO> Incomes { get; set; }
        public IList<Statistic> Subscriptions { get; set; }

        public int TotalIncome { get; set; }
        public int TotalCosts { get; set; }

        public int Surplus {
            get
            {
                int surplus = TotalIncome - TotalCosts;
                return surplus >= 0 
                    ? surplus 
                    : 0; 
            }
        }

        public float CostsPercent {
            get
            {
                if (TotalIncome <= 0) return 100f;

                float costsPercent = ((float)TotalCosts / TotalIncome) * 100;
                return costsPercent < 100f 
                    ? costsPercent
                    : 100f;
            }
        }

        public float SurplusPercent { get { return 100f - CostsPercent; } }

        public int RoundedCostsPercent { get { return (int)Math.Round(CostsPercent); } }

        public int RoundedSurplusPercent { get { return (int)Math.Round(SurplusPercent); } }

        public DashboardViewModel(IOrderedEnumerable<IncomeDTO> incomes, IOrderedEnumerable<SubscriptionDTO> subscriptions, int totalIncome, int totalCosts)
        {
            TotalIncome = totalIncome;
            TotalCosts = totalCosts;

            Incomes = incomes;

            Subscriptions = new List<Statistic>();
            for (int i = 0; i < subscriptions.Count(); i++)
            {
                AddStatistic(subscriptions.ElementAt(i));
            }
        }

        public IList<Statistic> GetTopCostsStats()
        {
            var stats = new List<Statistic>(NUMBER_OF_TOP_STATS);

            for (int i = 0; i < NUMBER_OF_TOP_STATS; i++)
            {
                stats.Add(Subscriptions[i]);
            }            

            stats.Add(GenerateOtherStat());

            return stats;
        }

        private Statistic GenerateOtherStat()
        {
            var otherAmount = 0;

            for (int i = NUMBER_OF_TOP_STATS; i < Subscriptions.Count; i++)
            {
                otherAmount += Subscriptions[i].Amount;
            }

            return new Statistic("Other", otherAmount, TotalCosts);
        }

        private void AddStatistic(SubscriptionDTO sub)
        {
            Subscriptions.Add(new Statistic(sub.Name, sub.MonthlyAmount, TotalCosts));
        }


        public struct Statistic
        {
            public readonly string Name;
            public readonly int Amount;
            public readonly float PercentageOfTotal;

            public Statistic(string name, int amount, int totalCosts)
            {
                Name = name;
                Amount = amount;
                PercentageOfTotal = ((float)amount / totalCosts) * 100f;
            }
        }

        private static readonly byte NUMBER_OF_TOP_STATS = 5;
    }
}