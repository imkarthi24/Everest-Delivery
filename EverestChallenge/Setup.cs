using EverestChallenge.Offers;

namespace EverestChallenge
{
    class Setup : ISetup
    {
        public void InitOfferCodes()
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

            offer = new Offer
            {
                Code = "OFR002",
                MinDistance = 50,
                MaxDistance = 150,
                MinWeight = 100,
                MaxWeight = 250,
                Discount = 7

            };
            offerHelper.AddOfferCode(offer);

            offer = new Offer
            {
                Code = "OFR003",
                MinDistance = 50,
                MaxDistance = 250,
                MinWeight = 10,
                MaxWeight = 150,
                Discount = 5

            };

            offerHelper.AddOfferCode(offer);
        }

       
    }
}
