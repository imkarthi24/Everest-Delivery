using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EverestChallenge.Offers
{
    
    public class OfferHelper : IOffer
    {


        private static OfferHelper instance = null;
        private Dictionary<string, Offer> _OfferCodes = new Dictionary<string, Offer>();

        private OfferHelper() { }

        public static OfferHelper Get()
        {
            if (instance == null)
                instance = new OfferHelper();
            
            return instance; 
        }

        public bool AddOfferCode(Offer offer)
        {            
            return _OfferCodes.TryAdd(offer.Code, offer);            
        }

        public decimal GetDicountOffer(string offerCode, int weight, int distance)
        {
            _OfferCodes.TryGetValue(offerCode, out Offer offer);
            if (offer == null) return default;

            if (CheckOfferValid(offer, weight, distance))
                return offer.Discount;

            return default;
        }

        public bool CheckOfferValid(Offer offer, int weight, int distance)
        {
            //Check Weight
            var validWeight = weight >= offer.MinWeight && weight <= offer.MaxWeight;

            //Check Distance
            var validDistance = distance >= offer.MinDistance && weight <= offer.MaxDistance;

            return validDistance && validWeight;
        }
    }

    
}
