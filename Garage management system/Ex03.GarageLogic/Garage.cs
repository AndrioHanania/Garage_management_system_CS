using System;
using System.Collections.Generic;

namespace Ex03.GarageLogic
{
    public class Garage
    {
        private readonly Dictionary<string, TreatedVehicle> r_Vehicles;

        public Garage()
        {
            r_Vehicles = new Dictionary<string, TreatedVehicle>();
        }

        public bool AddVehicle(Vehicle i_Vehicle, string i_OwnerName,
                    string i_OwnerPhoneNumber, VehicleFactory.eVehicleType i_VehicleType,
                    Dictionary<string, object> i_Parameters)
        {
            bool isAdd = true;

            if (!r_Vehicles.ContainsKey(i_Vehicle.LicenseNumber))
            {
                TreatedVehicle treatedVehicle =
                    TreatedVehicleCreator.Create(i_Vehicle, i_Parameters);

                r_Vehicles.Add(i_Vehicle.LicenseNumber, treatedVehicle);
            }
            else
            {
                r_Vehicles.TryGetValue(i_Vehicle.LicenseNumber,
                    out TreatedVehicle treatedVehicle);
                treatedVehicle.CurrentStatusInGarage =
                    TreatedVehicle.eTreatedVehicleStatus.Fixing;
                isAdd = false;
            }

            return isAdd;
        }

        public List<string> GetLicenseNumbers(bool i_FilterFlag,
                            TreatedVehicle.eTreatedVehicleStatus i_FilterStatus =
                            TreatedVehicle.eTreatedVehicleStatus.Fixing)
        {
            List<string> listLicenseNumbers = new List<string>();

            foreach(KeyValuePair<string, TreatedVehicle> treatedVehicle in r_Vehicles)
            {
                if(i_FilterFlag)
                {
                    if(treatedVehicle.Value.CurrentStatusInGarage == i_FilterStatus)
                    {
                        listLicenseNumbers.Add(treatedVehicle.Key);
                    }
                }
                else
                {
                    listLicenseNumbers.Add(treatedVehicle.Key);
                }
            }

            return listLicenseNumbers;
        }

        public void ChangeStatusVehicle(string i_LicenseNumber,
                    TreatedVehicle.eTreatedVehicleStatus i_NewStatus)
        {
            if(r_Vehicles.TryGetValue(i_LicenseNumber,
                out TreatedVehicle treatedVehicle))
            {
                treatedVehicle.CurrentStatusInGarage = i_NewStatus;
            }
            else
            {
                throw new ArgumentException("The vehicle does not in garage");
            }
        }

        public void BlowingWheelsToMax(string i_LicenseNumber)
        {
            if(r_Vehicles.TryGetValue(i_LicenseNumber,
                out TreatedVehicle treatedVehicle))
            {
                foreach(Wheel wheel in treatedVehicle.Vehicle.Wheels)
                {
                    try
                    {
                        wheel.Blowing(wheel.MaxAirPressure -
                                      wheel.CurrentAirPressure);
                    }
                    catch(Exception e)
                    {
                        throw e;
                    }
                }
            }
            else
            {
                throw new ArgumentException("The vehicle does not in garage");
            }
        }

        public void Refuel(
            string i_LicenseNumber,
            FuelBattery.eFuelType i_FuelType, float i_AddFuelQuantity)
        {
            if(!r_Vehicles.TryGetValue(i_LicenseNumber,
                   out TreatedVehicle treatedVehicle))
            {
                throw new ArgumentException("The vehicle does not in garage");
            }

            if (treatedVehicle.Vehicle.Battery == null)
            {
                throw new NullReferenceException("The battery was null!");
            }

            if (!(treatedVehicle.Vehicle.Battery is FuelBattery))
            {
                throw new ArgumentException("The battery was not FuelBattery");
            }

            try
            {
                (treatedVehicle.Vehicle.Battery as FuelBattery).Refuel(
                    i_AddFuelQuantity, i_FuelType);
            }
            catch(Exception e)
            {
                throw e;
            }
        }

        public void Recharge(string i_LicenseNumber, float i_ChargeTimeInMinutes)
        {
            if (r_Vehicles.TryGetValue(i_LicenseNumber,
                out TreatedVehicle treatedVehicle))
            {
                if (treatedVehicle.Vehicle.Battery != null)
                {
                    if(!(treatedVehicle.Vehicle.Battery is ElectricBattery))
                    {
                        throw new ArgumentException("The battery was not ElectricBattery");
                    }

                    try
                    {
                        (treatedVehicle.Vehicle.Battery as ElectricBattery).Recharge(i_ChargeTimeInMinutes / 60);
                    }
                    catch(Exception e)
                    {
                        throw e;
                    }
                }
                else
                {
                    throw new NullReferenceException("The battery was null!");
                }
            }
            else
            {
                throw new ArgumentException("The vehicle does not in garage");
            }
        }

        public string GetVehicleInfo(string i_LicenseNumber)
        {
            if(r_Vehicles.TryGetValue(i_LicenseNumber,
                out TreatedVehicle treatedVehicle))
            {

                return treatedVehicle.ToString();
            }

            throw new ArgumentException("The vehicle does not in garage");
        }

        public Dictionary<string, TreatedVehicle> Vehicles
        {
            get
            {
                return r_Vehicles;
            }
        }
    }
}