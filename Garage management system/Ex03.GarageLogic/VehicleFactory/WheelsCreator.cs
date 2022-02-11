using System;
using System.Collections.Generic;

namespace Ex03.GarageLogic
{
    public class WheelsCreator
    {
        public static List<Wheel> Create(
            string i_NameManufacturer, int i_NumOfWheels,
            float i_MaxAirPressure, float i_CurrentAirPressure)
        {
            if(i_CurrentAirPressure > i_MaxAirPressure)
            {
                throw new ArgumentException(
                    "Can not create wheels that current pressure is bigger than max");
            }

            List<Wheel> wheels = new List<Wheel>(i_NumOfWheels);

            for (int i = 0; i < i_NumOfWheels; i++)
            {
                Wheel currentWheel = new Wheel(i_NameManufacturer,
                    i_CurrentAirPressure, i_MaxAirPressure);

                wheels.Add(currentWheel);
            }

            return wheels;
        }
    }
}