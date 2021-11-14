using EverestChallenge.Delivery;
using EverestChallenge.Package;
using System.Collections.Generic;
using System;
using System.Linq;

namespace EverestChallenge
{
    class Program
    {
        static void Main(string[] args)
        {
            Setup setup = new Setup();
            PackageHandler packageHandler = new PackageHandler();
            DeliveryHandler deliveryHandler = new DeliveryHandler();
            
            setup.InitOfferCodes();
            
            decimal baseDeliveryCost = Decimal.Parse(Console.ReadLine());
            int packages = Int32.Parse(Console.ReadLine());

            packageHandler.SetBasicInfo(baseDeliveryCost, packages);
            

            while(packages-->0)
            {
                string id = Console.ReadLine();
                int weight = Int32.Parse(Console.ReadLine());
                int distance = Int32.Parse(Console.ReadLine());
                string offer = Console.ReadLine();

                DeliveryPackage package = new DeliveryPackage
                {
                    Id = id,
                    Weight = weight,
                    Distance = distance,
                    OfferCode = offer
                };

                packageHandler.AddPackage(package);

            }
            int vehicleCount = Int32.Parse(Console.ReadLine());
            int maxSpeed = Int32.Parse(Console.ReadLine());
            int maxLoad = Int32.Parse(Console.ReadLine());

            deliveryHandler.SetBasicInfo(vehicleCount, maxSpeed,maxLoad);
            deliveryHandler.AddPackages(packageHandler.GetPackages());

            List<PackageCost> costResults = packageHandler.EstimateCost();
            List<DeliveryPackage> deliveryTimes = deliveryHandler.EstimateDeliveryTime();

            CollateResults(costResults, deliveryTimes);

            Console.WriteLine("Here are the Estimated Costs:");
            costResults.ForEach(PrintOutput);

        }

        private static void CollateResults(List<PackageCost> costResult, List<DeliveryPackage> deliveryTimes)
        {
            foreach (var pkg in costResult)
            {
                pkg.Package = deliveryTimes.Where(x => x.Id == pkg.Package.Id).FirstOrDefault();
                pkg.DeliveryTime = pkg.Package.DeliveryTime;
            }
        }

        private static void PrintOutput(PackageCost cost)
        {            
            Console.WriteLine("{0} {1} {2} {3}", cost.Package.Id, cost.Discount, cost.Total, cost.DeliveryTime);
        }
    }
}
