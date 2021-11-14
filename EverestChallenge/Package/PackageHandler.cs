using EverestChallenge.Offers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EverestChallenge.Package
{
    public class PackageHandler : IPackageHandler
    {
        private  decimal _BaseDeliveryCost;
        private int _NumberOfPackages;

        private List<DeliveryPackage> Packages = new List<DeliveryPackage>();

        public void SetBasicInfo(decimal baseCost,int packages)
        {
            _BaseDeliveryCost = baseCost;
            _NumberOfPackages = packages;
        }

        public void AddPackage(DeliveryPackage package)
        {
            Packages.Add(package);
        }

        public List<PackageCost> EstimateCost()
        {
            List<PackageCost> result = new List<PackageCost>();
            OfferHelper helper = OfferHelper.Get();
            foreach (var package in Packages)
            {
                decimal deliveryCost = CalculateDeliveryCost(package.Weight, package.Distance);
                decimal discountPercent = helper.GetDicountOffer(package.OfferCode, package.Weight, package.Distance);
                
                decimal discountAmount = (deliveryCost * discountPercent)/100;
                decimal totalCost = deliveryCost - discountAmount;

                PackageCost cost = new PackageCost
                {
                    Package = package,
                    Discount = discountAmount,
                    Total = totalCost
                };
                result.Add(cost);
            }
            return result;
        }

        public List<DeliveryPackage> GetPackages()
        {
            return Packages;
        }

        public decimal CalculateDeliveryCost(int weight, int distance)
        {
            return _BaseDeliveryCost + (weight * 10) + (distance * 5);
        }
    }
}
