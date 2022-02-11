using System;
using System.Collections.Generic;
using System.Text;
using Ex03.GarageLogic;

namespace Ex03.ConsoleUI
{
    public class MyUiUtils
    {
        public static void PressEnterToContinue()
        {
            Console.WriteLine("Press Enter To Continue");
            Console.ReadLine();
        }

        public static void Exit()
        {
            Console.Clear();
            Console.WriteLine("See you next time...");
            System.Threading.Thread.Sleep(800);
            Console.Clear();
            Environment.Exit(1);
        }

        public static void RequirementsToInsert(
            VehicleFactory.eVehicleType i_UsersVehicleType,
            out Dictionary<string, object> i_RequirementsData)
        {
            Dictionary<string, Type> treatedVehicleRequirements =
                TreatedVehicleCreator.Requirements;
            Dictionary<string, Type> vehicleRequirements =
                VehicleFactory.Requirments(i_UsersVehicleType);
            Dictionary<string, Type> requirements =
                MyUtils.MergeDictionaries(
                    treatedVehicleRequirements,
                    vehicleRequirements);

            i_RequirementsData = InputRequirementsFromUser(
                i_UsersVehicleType, requirements);
        }

        public static Dictionary<string, object> InputRequirementsFromUser(
            VehicleFactory.eVehicleType i_Type,
            Dictionary<string, Type> i_Requirements)
        {
            Dictionary<string, object> requirementsData =
                new Dictionary<string, object>();
            int                        countLoop = 1;

            foreach (KeyValuePair<string, Type> requirement in i_Requirements)
            {
                bool validInput = false;

                Console.WriteLine("Please enter {0}", requirement.Key);
                while (!validInput)
                {
                    requirementsData[requirement.Key] = getUserGenericInput(
                        requirement.Value);
                    string reason;
                    if (countLoop < TreatedVehicleCreator.Requirements.Count)
                    {
                        validInput = VehicleFactory.ValidateRequirement(
                            i_Type, requirement.Key, requirementsData,
                            out reason);
                    }
                    else
                    {
                        validInput = TreatedVehicleCreator.ValidateRequirement(
                            i_Type, requirement.Key, requirementsData,
                            out reason);
                    }

                    if (!validInput)
                    {
                        Console.WriteLine(
                            "The input you entered is invalid. {0} Try again.", reason);
                    }
                }

                countLoop++;
            }

            return requirementsData;
        }

        public static void DrawMenuOptionEnum<eT>(string i_Title)
        {
            StringBuilder sb = new StringBuilder();
            string[]      menuItems = Enum.GetNames(typeof(eT));
            int           countMenuItems = 1;

            sb.AppendLine(i_Title);
            foreach (string type in menuItems)
            {
                sb.AppendLine(string.Format($"{countMenuItems}) {type}"));
                countMenuItems++;
            }

            Console.Write(sb);
        }

        public static int GetUserSelection<eT>(bool i_FlagToCheckZero = false)
        {
            int userSelection = 0;
            bool validInput = false;

            while (!validInput)
            {
                validInput = int.TryParse(Console.ReadLine(), out userSelection);
                if (!validInput)
                {
                   Console.WriteLine("The input you entered is invalid. Try again.");
                }

                if(!i_FlagToCheckZero)
                {
                    continue;
                }

                if(userSelection == 0)
                {
                    validInput = true;
                }
                else if(!Enum.IsDefined(typeof(eT), userSelection))
                {
                    Console.WriteLine(
                        "The input you entered is invalid, Option not exist. Try again.");
                    validInput = false;
                }
            }

            return userSelection;
        }

        public static T GetUserGenericInput<T>()
        {
            return (T)getUserGenericInput(typeof(T));
        }

        private static object getUserGenericInput(Type i_Type)
        {
            object     result = null;
            const bool v_True = true;

            while (v_True)
            {
                string inputStr = Console.ReadLine();

                try
                {
                    result = Convert.ChangeType(inputStr, i_Type);
                }
                catch
                {
                    // ignored
                }

                if (result == null)
                {
                    try
                    {
                        result = Enum.Parse(i_Type, inputStr);
                        if (!Enum.IsDefined(i_Type, result))
                        {
                            result = null;
                        }
                    }
                    catch
                    {
                        // ignored
                    }
                }

                if (result != null)
                {

                    return result;
                }

                Console.WriteLine(
                    "The input you entered is invalid. Try again.");
            }
        }

        public enum eUserOptionsMainMenu
        {
            InsertNewVehicle = 1,
            DisplayLicensePlateInGarage,
            ChangeVehicleStatus,
            InflateTires,
            RefuelVehicle,
            RechargeVehicle,
            DisplayVehicleInfoByLicensePlate,
            Exit
        }
    }
}