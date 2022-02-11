using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace Ex03.GarageLogic
{
    public class VehicleRequirements
    {
        private static Dictionary<string, Type> s_VehicleRequirements =
            new Dictionary<string, Type>()
            {
                {Keywords.k_ModelName, typeof(string)},
                {Keywords.k_LicenseNumber, typeof(string)},
                {Keywords.k_WheelsManufacturer, typeof(string)}
            };

        public static Dictionary<string, Type> Requirements
        {
            get
            {
                return s_VehicleRequirements;
            }
        }

        public static bool ValidateRequirement(string i_Name, object i_Value,
                                               out string o_ReasonForFail)
        {
            string strValue = i_Value as string;
            bool   isValidInput = !string.IsNullOrEmpty(strValue);

            o_ReasonForFail = string.Empty;

            if (isValidInput)
            {
                if (i_Name == Keywords.k_LicenseNumber)
                {
                    isValidInput = Regex.IsMatch(strValue, @"^[0-9]+$")
                                   && strValue != string.Empty;
                    if (!isValidInput)
                    {
                        o_ReasonForFail = Keywords.k_ShouldBePositiveNumber;
                    }
                }
            }

            return isValidInput;
        }
    }
}