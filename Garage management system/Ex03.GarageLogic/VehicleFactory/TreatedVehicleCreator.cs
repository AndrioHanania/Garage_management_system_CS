using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace Ex03.GarageLogic
{
    public class TreatedVehicleCreator
    {
        private static Dictionary<string, Type>
            s_TreatedVehicleRequirements = new Dictionary<string, Type>()
            {
                { Keywords.k_OwnerName, typeof(string)},
                { Keywords.k_OwnerPhone, typeof(string)}
            };

        public static TreatedVehicle Create(Vehicle i_Vehicle,
                      Dictionary<string, object> i_Parameters)
        {
            string ownerName = (string)i_Parameters[Keywords.k_OwnerName];
            string ownerPhoneNumber = (string)i_Parameters[Keywords.k_OwnerPhone];

            return new TreatedVehicle(i_Vehicle, ownerName, ownerPhoneNumber);
        }

        public static Dictionary<string, Type> Requirements
        {
            get
            {
                return s_TreatedVehicleRequirements;
            }
        }

        public static bool ValidateRequirement(
            VehicleFactory.eVehicleType i_VehicleType, string i_Name,
            Dictionary<string, object> i_Parameters,
            out string o_ReasonForFail)
        {
            bool   isValid = true;
            object value = i_Parameters[i_Name];

            o_ReasonForFail = string.Empty;
            if(i_Name == Keywords.k_OwnerName)
            {
                if(!Regex.IsMatch(value.ToString(),
                       @"^[a-zA-Z]+$") || value.ToString() == string.Empty)
                {
                    o_ReasonForFail = ExceptionMessage.FormatBadString(
                        Keywords.k_OwnerName);
                    isValid = false;
                }
            }
            else if(i_Name == Keywords.k_OwnerPhone)
            {
                if(!Regex.IsMatch(value.ToString(),
                       @"^[0-9]+$") || value.ToString() == string.Empty)
                {
                    o_ReasonForFail =
                        ExceptionMessage.FormatBadPhone(
                            Keywords.k_OwnerPhone);
                    isValid = false;
                }
            }

            return isValid;
        }
    }
}