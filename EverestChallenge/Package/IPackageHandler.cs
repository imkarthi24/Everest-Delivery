using System.Collections.Generic;

namespace EverestChallenge.Package
{
    public interface IPackageHandler
    {
        public List<PackageCost> EstimateCost();
        public decimal CalculateDeliveryCost(int weight, int distance);
    }
}