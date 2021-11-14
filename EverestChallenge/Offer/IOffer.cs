namespace EverestChallenge.Offers
{
    public interface IOffer
    {
        public bool CheckOfferValid(Offer offer, int weight, int distance);
    }
}