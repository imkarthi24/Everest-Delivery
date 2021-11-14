using EverestChallenge.Package;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EverestChallenge.Delivery
{
    public class ShipmentNode
    {
        public DeliveryPackage[] bestPackages;
        public int bestshipmentWeight;

        public List<DeliveryPackage> currentPackages;
        public int currShipmentWeight;

        public int baseIndex;

        public ShipmentNode(DeliveryPackage package, int index)
        {

            currentPackages = new List<DeliveryPackage>();

            currShipmentWeight += package.Weight;
            baseIndex = index;
            currentPackages.Add(package);
        }

        public void Add(DeliveryPackage package, int index)
        {
            currShipmentWeight += package.Weight;
            currentPackages.Add(package);
        }
        public void SetBestShipment()
        {
            bestshipmentWeight = currShipmentWeight;
            bestPackages = currentPackages.ToArray();
        }

        public void RemovePackage(DeliveryPackage currentPackage)
        {
            currentPackages.Remove(currentPackage);            
            currShipmentWeight -= currentPackage.Weight;
        }

        public bool CheckIfBestShipment()
        {
            if (currShipmentWeight > bestshipmentWeight) return true;

            if (currShipmentWeight == bestshipmentWeight)
                return currentPackages.Count() >= bestshipmentWeight;

            return false;
        }
    }

}
