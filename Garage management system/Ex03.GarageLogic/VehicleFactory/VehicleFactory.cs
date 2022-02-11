using System;
using System.Collections.Generic;

namespace Ex03.GarageLogic
{
    public class VehicleFactory
    {
        public static Vehicle Create(eVehicleType i_VehicleType,
                                    Dictionary<string,
                                        object> i_Parameters)
        {
            Vehicle newVehicle;
            string  modelName = (string)i_Parameters[Keywords.k_ModelName];
            string  licenseNumber = (string)i_Parameters[Keywords.k_LicenseNumber];
            string  wheelsManufacturer = (string)i_Parameters[Keywords.k_WheelsManufacturer];
            float   currentWheelsPressure = (float)i_Parameters[Keywords.k_CurrentWheelsPressure];
            float   currentEnergyAmount = (float)i_Parameters[Keywords.k_CurrentEnergyAmount];

            try
            {
                Battery.eEnergyType energyType;

                switch (i_VehicleType)
                {
                    case eVehicleType.Car:
                        Car.eColor color =
                            (Car.eColor)i_Parameters[
                                Keywords.k_ColorOption];
                        Car.eNumberDoors numOfDoors =
                            (Car.eNumberDoors)i_Parameters[
                                Keywords.k_NumberOfDoorsOption];
                        energyType =
                            (Battery.eEnergyType)i_Parameters[
                                Keywords.k_EnergyTypeOption];
                        newVehicle = CarCreator.Create(energyType,
                            numOfDoors, color, modelName, licenseNumber,
                            wheelsManufacturer, currentWheelsPressure,
                            currentEnergyAmount);
                        break;

                    case eVehicleType.Truck:
                        bool isCarryDangerous = (bool)i_Parameters[
                            Keywords.k_CarryDangerousMaterial];
                        float maxCarry = (float)i_Parameters[
                            Keywords.k_MaxCarryWeight];
                        newVehicle = TruckCreator.Create(isCarryDangerous,
                            maxCarry, modelName,
                            licenseNumber, wheelsManufacturer,
                            currentWheelsPressure, currentEnergyAmount);
                        break;

                    case eVehicleType.Motorcycle:
                        Motorcycle.eLicenseType typeLicense =
                            (Motorcycle.eLicenseType)i_Parameters[
                                Keywords.k_LicenseTypeOption];
                        int maxCapacity =
                            (int)i_Parameters[Keywords.k_EngineVolume];
                        energyType = 
                            (Battery.eEnergyType)i_Parameters[
                                Keywords.k_EnergyTypeOption];
                        newVehicle = MotorcycleCreator.Create(
                            energyType, typeLicense, maxCapacity,
                            modelName, licenseNumber, wheelsManufacturer,
                            currentWheelsPressure, currentEnergyAmount);
                        break;

                    default:
                        throw new FormatException(
                            ExceptionMessage.FormatUndefinedEnumValueExceptionMessage(
                                nameof(i_VehicleType)));
                }
            }
            catch(Exception e)
            {
                throw e;
            }

            return newVehicle;
        }

        public static Dictionary<string, Type>
            Requirments(eVehicleType i_VehicleType)
        {
            Dictionary<string, Type> requirements;

            switch (i_VehicleType)
            {
                case eVehicleType.Car:
                    requirements = CarCreator.Requirements;
                    break;

                case eVehicleType.Truck:
                    requirements = TruckCreator.Requirements;
                    break;

                case eVehicleType.Motorcycle:
                    requirements = MotorcycleCreator.Requirements;
                    break;

                default:
                    throw new FormatException(
                        ExceptionMessage.FormatUndefinedEnumValueExceptionMessage(
                            nameof(i_VehicleType)));
            }

            return requirements;
        }

        public static bool ValidateRequirement(eVehicleType i_VehicleType,
           string i_Name, Dictionary<string,
               object> i_Parameters, out string o_ReasonForFail)
        {
            bool isValid;

            o_ReasonForFail = string.Empty;
            switch (i_VehicleType)
            {
                case eVehicleType.Car:
                    isValid = CarCreator.ValidateRequirement(i_Name,
                        i_Parameters,
                        out o_ReasonForFail);
                    break;

                case eVehicleType.Truck:
                    isValid = TruckCreator.ValidateRequirement(i_Name,
                        i_Parameters,
                        out o_ReasonForFail);
                    break;

                case eVehicleType.Motorcycle:
                    isValid = MotorcycleCreator.ValidateRequirement(i_Name,
                        i_Parameters,
                        out o_ReasonForFail);
                    break;

                default:
                    throw new FormatException(
                        ExceptionMessage.FormatUndefinedEnumValueExceptionMessage(
                            nameof(i_VehicleType)));
            }

            if (isValid)
            {
                o_ReasonForFail = string.Empty;
            }

            return isValid;
        }

        public enum eVehicleType
        {
            Car = 1,
            Motorcycle,
            Truck
        }
    }
}