using EverestChallenge.Delivery;
using EverestChallenge.Package;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;

namespace EverestChallenge.Tests
{
    [TestClass]
    public class DeliveryTest
    {

        public decimal BaseDeliveryCost = 100;
        public int PackageCount = 3;

        [TestMethod]
        public void ShouldEstimateCostCorrectlyForPackages()
        {
            DeliveryHandler deliveryHandler = new DeliveryHandler();
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
            DeliveryPackage pkg2 = new DeliveryPackage
            {
                Id = "PKG2",
                Weight = 40,
                Distance = 40,
                OfferCode = "NA"
            };
            packageHandler.AddPackage(pkg2);
            DeliveryPackage pkg3 = new DeliveryPackage
            {
                Id = "PKG3",
                Weight = 60,
                Distance = 90,
                OfferCode = "NA"
            };
            packageHandler.AddPackage(pkg3);

            deliveryHandler.SetBasicInfo(2, 70, 100);
            deliveryHandler.AddPackages(packageHandler.GetPackages());

            var actualResponse = deliveryHandler.EstimateDeliveryTime();

            var pkg1Response = new { Id = pkg.Id, Time = (decimal)0.28 };
            var pkg2Response = new { Id = pkg2.Id, Time = (decimal)0.57 };
            var pkg3Response = new { Id = pkg3.Id, Time = (decimal)1.28 };

            Assert.AreEqual(pkg1Response.Time, actualResponse.Where(x => x.Id == pkg.Id).FirstOrDefault().DeliveryTime);
            Assert.AreEqual(pkg2Response.Time, actualResponse.Where(x => x.Id == pkg2.Id).FirstOrDefault().DeliveryTime);
            Assert.AreEqual(pkg3Response.Time, actualResponse.Where(x => x.Id == pkg3.Id).FirstOrDefault().DeliveryTime);



        }

    }
}
