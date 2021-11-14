# Everest Coding Challenge - Delivery System
### _Introduction_
The Everest Delivery System is capable of estimating the cost and the shortest time it takes to deliver a given set 
of packages. It consists of two basic modules:
- Cost Estimator
- Delivery Time Estimator

### Tech Stack
- C# , .Net 5.0

### Features

- The Cost estimation service is a simple service which allows you to estimate costs for a given weight and distance based on a predifined formula
- In addition to just calculating costs u can use coupon codes for providing further discounts
- The Delivery Estimation system is built using Dynamic Programming. It effectively calculates the shortest way of delivering the packages in multiple shipments


### Input

Please use the below Input sample to get a working example of the system


```sh
100
5
PKG1
50
30
OFR001
PKG2
75
125
OFFFR00082
PKG3
175
100
OFFR003
PKG4
110
60
OFFR002
PKG5
155
95
NA
2
70
200
```
