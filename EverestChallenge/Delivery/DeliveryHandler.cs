using EverestChallenge.Package;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;

namespace EverestChallenge.Delivery
{
    
    public class DeliveryHandler
    {
        private int _NumberOFVehicles;
        private int _MaxSpeed;
        private int _MaxLoad;
        private List<decimal> _AvailabilityList;
        private List<DeliveryPackage> _DeliveryPackages;

        public void SetBasicInfo(int vehicles, int speed, int maxLoad)
        {
            _NumberOFVehicles = vehicles;
            _MaxSpeed = speed;
            _MaxLoad = maxLoad;
            _AvailabilityList = Enumerable.Repeat(decimal.Zero, _NumberOFVehicles).ToList();            
        }

        public void AddPackages(List<DeliveryPackage> deliveryPackages)
        {
            _DeliveryPackages = new List<DeliveryPackage>();
            _DeliveryPackages.AddRange(deliveryPackages);
        }

        public List<DeliveryPackage> EstimateDeliveryTime()
        {
            List<ShipmentNode> shipments = new List<ShipmentNode>();
            var packagesToDeliver = _DeliveryPackages.OrderBy(x => x.Weight).ThenBy(x => x.Distance).ToList();
            _DeliveryPackages.Clear();
            while (packagesToDeliver.Any())
            {
                ShipmentNode shipment = SearchForBestShipment(packagesToDeliver);
                shipments.Add(shipment);
                RemovePackages(shipment, packagesToDeliver);               
            }
            AssignToDelivery(shipments);
            return _DeliveryPackages;

        }
                

        private ShipmentNode SearchForBestShipment(List<DeliveryPackage> packages)
        {
           
            ShipmentNode bestShipmentNode = null;
            for (int pkg = 0; pkg < packages.Count; pkg++)
            {
                ShipmentNode baseNode = new ShipmentNode(packages[pkg], pkg);

                if (pkg == packages.Count - 1)                
                    baseNode.SetBestShipment();
                
                findBestRecursively(baseNode, pkg, packages, out bool maxReached);

                if (IsBestNode(baseNode, bestShipmentNode))
                    bestShipmentNode = baseNode;
            }
            return bestShipmentNode;

        }       

        private void findBestRecursively(ShipmentNode node, int index, List<DeliveryPackage> packages, out bool maxReached)
        {
            maxReached = false;
            
            for (int i = index + 1; i < packages.Count; i++)
            {
                var currentPackage = packages[i];

                if (CanChooseCurrentPackage(node, currentPackage))
                {
                    node.Add(currentPackage, i);
                    findBestRecursively(node, i, packages, out maxReached);

                    if (maxReached)
                        node.RemovePackage(currentPackage);
                }
                else
                {
                    maxReached = true;
                    if (node.CheckIfBestShipment())                    
                        node.SetBestShipment();
                    
                    return;

                }
            }
            
        }
        private Dictionary<string, decimal> AssignToDelivery(List<ShipmentNode> shipments)
        {
            Dictionary<string, decimal> timeToDeliverDict = new Dictionary<string, decimal>();
            foreach (var shipment in shipments)
            {
                decimal waitingTime = _AvailabilityList.Min();

                decimal timeToDeliver = 0;

                foreach (var pkg in shipment.bestPackages.OrderBy(x => x.Distance))
                {
                    timeToDeliver = SetDeliveryTime(pkg, waitingTime);
                    _DeliveryPackages.Add(pkg);
                }

                _AvailabilityList.Remove(waitingTime);
                _AvailabilityList.Add(waitingTime + (timeToDeliver * 2));
            }
            return timeToDeliverDict;

        }

        private bool IsBestNode(ShipmentNode baseNode, ShipmentNode bestShipmentNode)
        {
            if (bestShipmentNode == null) return true;
            if (baseNode.bestPackages.Length > bestShipmentNode.bestPackages.Length)
                return true;

            if (baseNode.bestPackages.Length == bestShipmentNode.bestPackages.Length)
                return baseNode.bestshipmentWeight >= bestShipmentNode.bestshipmentWeight;

            return false;
        }

        private decimal SetDeliveryTime(DeliveryPackage pkg, decimal waitingTime)
        {
            decimal timeToDeliver = (decimal)pkg.Distance / (decimal)_MaxSpeed;
            pkg.DeliveryTime = Math.Round(timeToDeliver + waitingTime, 2, MidpointRounding.ToZero);
            return Math.Round(timeToDeliver, 2, MidpointRounding.ToZero);
        }

        private void RemovePackages(ShipmentNode shipment, List<DeliveryPackage> packagesToDeliver)
        {
            packagesToDeliver.RemoveAll(x => shipment.bestPackages.Contains(x));
        }

       

        private bool CanChooseCurrentPackage(ShipmentNode node, DeliveryPackage currentPackage)
        {
            if (node.currShipmentWeight + currentPackage.Weight <= _MaxLoad)
                return true;
            return false;
        }
    }

}

