using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using Ex03.GarageLogic;

namespace Ex03.ConsoleUI
{
    public class UserInterface
    {
        private Garage m_Garage = new Garage();

        public void Run()
        {
            bool isRunable = true;

            while(isRunable)
            {
                Console.Clear();
                drawMainMenu();
                MyUiUtils.eUserOptionsMainMenu userSelectionMainMenu =
                    (MyUiUtils.eUserOptionsMainMenu)MyUiUtils.GetUserSelection
                    <MyUiUtils.eUserOptionsMainMenu>();
                try
                {
                    runUserSelectionMainMenu(userSelectionMainMenu);
                }
                catch(Exception e)
                {
                    Console.WriteLine(e);
                    isRunable = false;
                }
            }

            MyUiUtils.PressEnterToContinue();
            MyUiUtils.Exit();
        }

        private static void drawMainMenu()
        {
            StringBuilder sb = new StringBuilder();

            sb.AppendLine("Please enter one of the following option:");
            sb.AppendLine("1) Insert vehicle to garage");
            sb.AppendLine("2) Display license plates of all vehicles in garage");
            sb.AppendLine("3) Change vehicle's status in garage");
            sb.AppendLine("4) Blowing vehicle's wheels");
            sb.AppendLine("5) Refuel vehicles");
            sb.AppendLine("6) Recharge vehicles");
            sb.AppendLine("7) Display vehicle's full information by license plate");
            sb.AppendLine("8) Exit");
            Console.Write(sb);
        }

        private void runUserSelectionMainMenu(MyUiUtils.eUserOptionsMainMenu i_UserSelectionMainMenu)
        {
            Console.Clear();
            try
            {
                switch (i_UserSelectionMainMenu)
                {
                    case MyUiUtils.eUserOptionsMainMenu.InsertNewVehicle:
                        insertNewVehicle();
                        break;

                    case MyUiUtils.eUserOptionsMainMenu.DisplayLicensePlateInGarage:
                        displayLicenseNumbersInGarage();
                        break;

                    case MyUiUtils.eUserOptionsMainMenu.ChangeVehicleStatus:
                        changeVehicleStatus();
                        break;

                    case MyUiUtils.eUserOptionsMainMenu.InflateTires:
                        inflateVehicleTiresToMax();
                        break;
                    case MyUiUtils.eUserOptionsMainMenu.RefuelVehicle:
                        refuelVehicle();
                        break;

                    case MyUiUtils.eUserOptionsMainMenu.RechargeVehicle:
                        rechargeVehicle();
                        break;

                    case MyUiUtils.eUserOptionsMainMenu.DisplayVehicleInfoByLicensePlate:
                        displayVehicleInformationByLicenseNumber();
                        break;

                    case MyUiUtils.eUserOptionsMainMenu.Exit:
                        MyUiUtils.Exit();
                        break;

                    default:
                        throw new FormatException(
                            ExceptionMessage.FormatUndefinedEnumValueExceptionMessage(
                                nameof(i_UserSelectionMainMenu)));
                }
            }
            catch(Exception e)
            {
                throw e;
            }

            MyUiUtils.PressEnterToContinue();
        }

        private void displayVehicleInformationByLicenseNumber()
        {
            string licenseNumber = getVehicleLicenseNumber();

            try
            {
                Console.WriteLine(m_Garage.GetVehicleInfo(licenseNumber));
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        private void rechargeVehicle()
        {
            string licenseNumber = getVehicleLicenseNumber();

            Console.WriteLine("Please insert time to recharge in minutes: ");
            float chargeTimeInMinutes = MyUiUtils.GetUserGenericInput<float>();

            try
            {
                m_Garage.Recharge(licenseNumber, chargeTimeInMinutes);
            }
            catch(Exception e)
            {
                throw e;
            }

            Console.WriteLine("The vehicle was recharged!");
        }

        private void refuelVehicle()
        {
            string licenseNumber = getVehicleLicenseNumber();

            MyUiUtils.DrawMenuOptionEnum<FuelBattery.eFuelType>(
                "Please enter fuel type from the option: ");
            FuelBattery.eFuelType fuelType =
                (FuelBattery.eFuelType)MyUiUtils.GetUserSelection
                    <FuelBattery.eFuelType>();

            Console.WriteLine("Please insert the amount of fuel you want to fill with: ");
            float amountOfFuel = MyUiUtils.GetUserGenericInput<float>();

            try
            {
                m_Garage.Refuel(licenseNumber, fuelType, amountOfFuel);
            }
            catch(Exception e)
            {
                throw e;
            }

            Console.WriteLine("The vehicle was refueled!");
        }

        private void inflateVehicleTiresToMax()
        {
            string licenseNumber = getVehicleLicenseNumber();

            try
            {
                m_Garage.BlowingWheelsToMax(licenseNumber);
            }
            catch(Exception e)
            {
                throw e;
            }

            Console.WriteLine("The wheels was inflated to max pressure!");
        }

        private void changeVehicleStatus()
        {
            string licenseNumber = getVehicleLicenseNumber();

            MyUiUtils.DrawMenuOptionEnum<TreatedVehicle.eTreatedVehicleStatus>(
                "Please enter new status for the vehicle");
            TreatedVehicle.eTreatedVehicleStatus newVehicleStatus =
                (TreatedVehicle.eTreatedVehicleStatus)MyUiUtils.GetUserSelection
                <TreatedVehicle.eTreatedVehicleStatus>();

            try
            {
                m_Garage.ChangeStatusVehicle(licenseNumber, newVehicleStatus);
            }
            catch(Exception e)
            {
                throw e;
            }

            Console.WriteLine("The status was changed!");
        }

        private string getVehicleLicenseNumber()
        {
            string licensePlate;
            int    countLoop = 0;

            Console.WriteLine("Please enter license plate in garage: ");
            do
            {
                if (countLoop > 0)
                {
                   Console.WriteLine("Error with format license plate, try again: ");
                }

                licensePlate = Console.ReadLine();
                if(!m_Garage.Vehicles.ContainsKey(licensePlate))
                {
                    Console.WriteLine("License plate not in garage. try again: ");
                    licensePlate = string.Empty;
                }

                countLoop++;
            }
            while (!Regex.IsMatch(licensePlate,
                       @"^[0-9]+$") || licensePlate == string.Empty);

            return licensePlate;
        }

        private void displayLicenseNumbersInGarage()
        {
            List<string>  licensePlates;
            const bool    v_FilterFlag = true;
            StringBuilder sb = new StringBuilder();

            MyUiUtils.DrawMenuOptionEnum
            <TreatedVehicle.eTreatedVehicleStatus>(
                "Please enter the option of your status vehicle type:");
            Console.WriteLine("0) all");
            int filterStatusInt = MyUiUtils.GetUserSelection
                <TreatedVehicle.eTreatedVehicleStatus>(true);

            if(filterStatusInt == 0)
            {
                licensePlates = m_Garage.GetLicenseNumbers(
                    !v_FilterFlag);
            }
            else
            {
                TreatedVehicle.eTreatedVehicleStatus filterStatus =
                    (TreatedVehicle.eTreatedVehicleStatus)filterStatusInt;

                licensePlates = m_Garage.GetLicenseNumbers(v_FilterFlag, filterStatus);
            }

            foreach(string licensePlate in licensePlates)
            {
                sb.AppendLine(licensePlate);
            }

            if(licensePlates.Count == 0)
            {
                sb.AppendLine("none");
            }

            Console.WriteLine(sb);
        }

        private void insertNewVehicle()
        {
            StringBuilder sb = new StringBuilder();
            MyUiUtils.DrawMenuOptionEnum<VehicleFactory.eVehicleType>(
                "Please enter the option of your vehicle type:");
            VehicleFactory.eVehicleType usersVehicleType =
                (VehicleFactory.eVehicleType)MyUiUtils.GetUserSelection<
                    VehicleFactory.eVehicleType>();

            MyUiUtils.RequirementsToInsert(usersVehicleType,
                out Dictionary<string, object> requirementsData);
            Vehicle newVehicle;

            try
            {
                newVehicle = VehicleFactory.Create(usersVehicleType,
                    requirementsData);
            }
            catch(Exception e)
            {
                throw e;
            }

            if (m_Garage.AddVehicle(newVehicle,
                requirementsData[Keywords.k_OwnerName].ToString(),
                requirementsData[
                    Keywords.k_OwnerPhone].ToString(),
                usersVehicleType,requirementsData))
            {
                sb.AppendLine("The vehicle was added!");
            }
            else
            {
                sb.Append("The vehicle already exists. ");
                sb.AppendLine($"Now the status is {TreatedVehicle.eTreatedVehicleStatus.Fixing} again");
            }

            Console.Write(sb);
        }
    }
}