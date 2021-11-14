using EverestChallenge.Offers;
using EverestChallenge.Package;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace EverestChallenge.Tests
{
    [TestClass]
    public class PackageTest
    {
        public decimal BaseDeliveryCost = 100;
        public int PackageCount = 3;

        [TestMethod]
        public void DeliveryCostShouldBeCorrect()
        {
            PackageHandler packageHandler = new PackageHandler();
            packageHandler.SetBasicInfo(BaseDeliveryCost, PackageCount);

            var cost = packageHandler.CalculateDeliveryCost(100, 10);
            Assert.AreEqual(100 + (100 * 10) + (10 * 5), cost);


            var cost2 = packageHandler.CalculateDeliveryCost(10, 10);
            Assert.AreEqual(100 + (10 * 10) + (10 * 5), cost2);
        }


        [TestMethod]
        public void EstimatedCostShouldBeCorrectWithoutOfferCode()
        {
            PackageHandler packageHandler = new PackageHandler();
            packageHandler.SetBasicInfo(BaseDeliveryCost, PackageCount);


            
            DeliveryPackage pkg = new DeliveryPackage
            {
                Id = "PKG1",
                Weight = 100,
                Distance = 20,
                OfferCode = "NA"
            };
            packageHandler.AddPackage(pkg);

            var costEstimate = packageHandler.EstimateCost()[0];
            var expectedCost = packageHandler.CalculateDeliveryCost(pkg.Weight, pkg.Distance);
            Assert.AreEqual(expectedCost, costEstimate.Total);
        }

        [TestMethod]
        public void EstimatedCostShouldBeCorrectWithOfferCode()
        {
            PackageHandler packageHandler = new PackageHandler();
            packageHandler.SetBasicInfo(BaseDeliveryCost, PackageCount);

            var offerHelper = OfferHelper.Get();
            var offer = new Offer
            {
                Code = "OFR003",
                MinDistance = 0,
                MaxDistance = 200,
                MinWeight = 70,
                MaxWeight = 200,
                Discount = 10

            };
            offerHelper.AddOfferCode(offer);

            DeliveryPackage pkg = new DeliveryPackage
            {
                Id = "PKG1",
                Weight = 100,
                Distance = 20,
                OfferCode = "OFR003"
            };
            packageHandler.AddPackage(pkg);

            var discountPercent = offerHelper.GetDicountOffer(offer.Code, offer.MinWeight, offer.MinDistance);

            var costEstimate = packageHandler.EstimateCost()[0];
            var deliveryCost= packageHandler.CalculateDeliveryCost(pkg.Weight, pkg.Distance);

            var expectedCost = deliveryCost - (deliveryCost * discountPercent) / 100;
            Assert.AreEqual(expectedCost, costEstimate.Total);
        }

    }
}
