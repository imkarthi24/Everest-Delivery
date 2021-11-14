using EverestChallenge.Offers;
using Microsoft.VisualStudio.TestTools.UnitTesting;



namespace EverestChallenge.Tests
{
    [TestClass]
    public class OfferTest
    {
        [TestMethod]
        public void DiscountOfferForCodeShouldBeCorrect()
        {
            var offerHelper = OfferHelper.Get();
            var offer = new Offer
            {
                Code = "OFR001",
                MinDistance = 0,
                MaxDistance = 200,
                MinWeight = 70,
                MaxWeight = 200,
                Discount = 10

            };
            offerHelper.AddOfferCode(offer);
            var discount = offerHelper.GetDicountOffer(offer.Code, offer.MinWeight, offer.MinDistance);
            Assert.AreEqual(offer.Discount, discount);
        }
        
        [TestMethod]
        public void DiscountOfferForIncorrectCodeShouldBeIncCorrect()
        {
            var offerHelper = OfferHelper.Get();
            var offer = new Offer
            {
                Code = "OFR004",
                MinDistance = 0,
                MaxDistance = 20,
                MinWeight = 10,
                MaxWeight = 20,
                Discount = 10

            };
            offerHelper.AddOfferCode(offer);
            var discount = offerHelper.GetDicountOffer("IncorrectCode", offer.MinWeight, offer.MinDistance);
            Assert.AreEqual(decimal.Zero, discount);
        }
        [TestMethod]
        public void OfferIsValidForCorrectDistanceAndWeight()
        {
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
            var isValid = offerHelper.CheckOfferValid(offer, offer.MinWeight, offer.MaxDistance);
            Assert.AreEqual(true, isValid);
        }

        [TestMethod]
        public void OfferIsInValidForInCorrectDistanceAndWeight()
        {
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
            var isValid = offerHelper.CheckOfferValid(offer, 250, offer.MaxDistance);
            Assert.AreEqual(false, isValid);
        }
    }
}
