using System;
using System.Collections.Generic;

namespace Ex03.GarageLogic
{
    public class TruckCreator
    {
        private const int k_NumbersOfWheels = 16;
        private const float k_MaxWheelsPressure = 26;
        private const FuelBattery.eFuelType k_FuelType = FuelBattery.eFuelType.Soler;
        private const float k_MaxFuelTankLiter = 120;
        private static Dictionary<string, Type> s_TruckRequirements =
            new Dictionary<string, Type>()
            {
                { Keywords.k_CarryDangerousMaterial, typeof(bool)},
                { Keywords.k_MaxCarryWeight, typeof(float)},
                { Keywords.k_CurrentWheelsPressure, typeof(float)},
                { Keywords.k_CurrentEnergyAmount, typeof(float)}
            };

        public static Vehicle Create(
            bool i_IsCarryDangerousMaterial,
            float i_MaxCarryWeight,
            string i_ModelName,
            string i_LicenseNumber,
            string i_WheelsManufacturer,
            float i_CurrentWheelsPressure,
            float i_CurrentEnergyAmount)
        {
            if (i_CurrentEnergyAmount > k_MaxFuelTankLiter)
            {
                throw new ValueOutOfRangeException(
                    "Current energy amount is more than the max for fuel truck",
                    0, k_MaxFuelTankLiter);
            }

            Battery     battery = FuelBatteryCreator.Create(k_MaxFuelTankLiter,
                i_CurrentEnergyAmount, k_FuelType);
            List<Wheel> wheels;

            try
            {
                wheels = WheelsCreator.Create(
                    i_WheelsManufacturer, k_NumbersOfWheels, k_MaxWheelsPressure,
                    i_CurrentWheelsPressure);
            }
            catch(Exception e)
            {
                throw e;
            }

            Truck newMotorcycle = new Truck(i_IsCarryDangerousMaterial,
                i_MaxCarryWeight, i_ModelName, i_LicenseNumber, wheels, battery);

            return newMotorcycle;
        }

        public static Dictionary<string, Type>
            Requirements
        {
            get
            {
                return MyUtils.MergeDictionaries(VehicleRequirements.Requirements,
                    s_TruckRequirements);
            }
        }

        public static bool ValidateRequirement(
            string i_Name, Dictionary<string,
                object> i_Parameters, out string o_ReasonForFail)
        {
            bool   isValid = true;
            object value = i_Parameters[i_Name];

            o_ReasonForFail = string.Empty;
            if (VehicleRequirements.Requirements.ContainsKey(i_Name))
            {
                isValid = VehicleRequirements.ValidateRequirement(i_Name, value,
                    out o_ReasonForFail);
            }
            else
            {
                if (i_Name == Keywords.k_EngineVolume)
                {
                    int engineCapacity = (int)value;

                    isValid = engineCapacity >= 0;
                    o_ReasonForFail = Keywords.k_ShouldBePositiveNumber;
                }
                else if (i_Name == Keywords.k_CurrentWheelsPressure)
                {
                    float currentPressure = (float)value;

                    isValid = currentPressure >= 0 && currentPressure <=
                              k_MaxWheelsPressure;
                    o_ReasonForFail = ExceptionMessage.FormatOutOfRangeExceptionMessage(
                        Keywords.k_CurrentWheelsPressure,
                        0,
                        k_MaxWheelsPressure);
                }
                else if (i_Name == Keywords.k_CurrentEnergyAmount)
                {
                    float currentEnergy = (float)value;

                    isValid = currentEnergy >= 0 && currentEnergy <=
                              k_MaxFuelTankLiter;
                    o_ReasonForFail = ExceptionMessage.FormatOutOfRangeExceptionMessage(
                        Keywords.k_CurrentWheelsPressure, 0,
                        k_MaxFuelTankLiter);
                }
                else
                {
                    o_ReasonForFail = string.Empty;
                }
            }

            return isValid;
        }
    }
}