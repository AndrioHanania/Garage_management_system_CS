using System;
using System.Collections.Generic;

namespace Ex03.GarageLogic
{
    public class CarCreator
    {
        private const int k_NumbersOfWheels = 4;
        private const float k_MaxWheelsPressure = 32;
        private const FuelBattery.eFuelType k_FuelType = FuelBattery.eFuelType.Octan95;
        private const float k_MaxFuelTankLiter = 45;
        private const float k_MaxChargeTime = 3.2f;
        private static Dictionary<string, Type> s_CarRequirements =
            new Dictionary<string, Type>()
            {
                { Keywords.k_EnergyTypeOption, typeof(Battery.eEnergyType)},
                { Keywords.k_ColorOption, typeof(Car.eColor)},
                { Keywords.k_NumberOfDoorsOption, typeof(Car.eNumberDoors)},
                { Keywords.k_CurrentWheelsPressure, typeof(float)},
                { Keywords.k_CurrentEnergyAmount, typeof(float)}
            };

        public static Vehicle Create(
            Battery.eEnergyType i_EnergyType,
            Car.eNumberDoors i_NumOfDoors,
            Car.eColor i_Color,
            string i_ModelName,
            string i_LicenseNumber,
            string i_WheelsManufacturer,
            float i_CurrentWheelsPressure,
            float i_CurrentEnergyAmount)
        {
            Battery     battery;
            List<Wheel> wheels;

            switch (i_EnergyType)
            {
                case Battery.eEnergyType.Fuel
                    when i_CurrentEnergyAmount < k_MaxFuelTankLiter:
                    battery = FuelBatteryCreator.Create(k_MaxFuelTankLiter,
                        i_CurrentEnergyAmount, k_FuelType);
                    break;

                case Battery.eEnergyType.Fuel:
                    throw new ValueOutOfRangeException(
                        "Current energy amount is more than the max for fuel car",
                        0, k_MaxFuelTankLiter);

                case Battery.eEnergyType.Electic
                    when i_CurrentEnergyAmount < k_MaxChargeTime:
                    battery = ElectricBatteryCreator.Create(k_MaxChargeTime,
                        i_CurrentEnergyAmount);
                    break;

                case Battery.eEnergyType.Electic:
                    throw new ValueOutOfRangeException(
                        "Current energy amount is more than the max for electric car",
                        0, k_MaxChargeTime);

                default:
                    throw new FormatException(
                        ExceptionMessage.FormatUndefinedEnumValueExceptionMessage(
                            nameof(i_EnergyType)));
            }

            try
            {
                wheels = WheelsCreator.Create(
                    i_WheelsManufacturer, k_NumbersOfWheels, k_MaxWheelsPressure,
                    i_CurrentWheelsPressure);
            }
            catch(Exception e)
            {
                Console.WriteLine(e);
                throw;
            }

            Car newCar = new Car(i_Color, i_NumOfDoors, i_ModelName,
                i_LicenseNumber, wheels, battery);

            return newCar;
        }

        public static Dictionary<string, Type>
            Requirements
        {
            get
            {
                return MyUtils.MergeDictionaries(VehicleRequirements.Requirements,
                    s_CarRequirements);
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
                if (i_Name == Keywords.k_NumberOfDoorsOption)
                {
                    isValid = true;
                    o_ReasonForFail = ExceptionMessage.FormatOutOfRangeExceptionMessage(
                        Keywords.k_NumberOfDoorsOption,
                        (int)Car.eNumberDoors.Two,
                        (int)Car.eNumberDoors.Five);
                }
                else if (i_Name == Keywords.k_CurrentWheelsPressure)
                {
                    float currentPressure = (float)value;

                    isValid = currentPressure >= 0 && currentPressure <=
                              k_MaxWheelsPressure;
                    o_ReasonForFail = ExceptionMessage.FormatOutOfRangeExceptionMessage(
                        Keywords.k_CurrentWheelsPressure,
                        0, k_MaxWheelsPressure);
                }
                else if (i_Name == Keywords.k_CurrentEnergyAmount)
                {
                    float               currentEnergy = (float)value;
                    Battery.eEnergyType energyType =
                        (Battery.eEnergyType)i_Parameters[Keywords.k_EnergyTypeOption];

                    switch (energyType)
                    {
                        case Battery.eEnergyType.Electic:
                            isValid = currentEnergy >= 0 && currentEnergy <=
                                      k_MaxChargeTime;
                            o_ReasonForFail =
                                ExceptionMessage.FormatOutOfRangeExceptionMessage(
                                    Keywords.k_CurrentEnergyAmount,
                                    0,
                                    k_MaxChargeTime);
                            break;

                        case Battery.eEnergyType.Fuel:
                            isValid = currentEnergy >= 0 && currentEnergy <=
                                      k_MaxFuelTankLiter;
                            o_ReasonForFail =
                                ExceptionMessage.FormatOutOfRangeExceptionMessage(
                                    Keywords.k_CurrentEnergyAmount,
                                    0,
                                    k_MaxFuelTankLiter);
                            break;

                        default:
                            throw new FormatException(
                                ExceptionMessage.FormatUndefinedEnumValueExceptionMessage(
                                    nameof(energyType)));
                    }
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