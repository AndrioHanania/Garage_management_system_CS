using System;
using System.Collections.Generic;

namespace Ex03.GarageLogic
{
    public class MyUtils
    {
        public static Dictionary<string, Type> MergeDictionaries(
            Dictionary<string, Type> i_Dictionary1,
            Dictionary<string, Type> i_Dictionary2)
        {
            Dictionary<string, Type> result = new Dictionary<string, Type>();

            foreach (KeyValuePair<string, Type> parameter in i_Dictionary1)
            {
                result[parameter.Key] = parameter.Value;
            }

            foreach (KeyValuePair<string, Type> parameter in i_Dictionary2)
            {
                result[parameter.Key] = parameter.Value;
            }

            return result;
        }

        public static Dictionary<string, object> MergeDictionaries(
            Dictionary<string, object> i_Dictionary1,
            Dictionary<string, Type> i_Dictionary2)
        {
            Dictionary<string, object> result = new Dictionary<string, object>();

            foreach (KeyValuePair<string, object> parameter in i_Dictionary1)
            {
                result[parameter.Key] = (string)parameter.Value;
            }

            foreach (KeyValuePair<string, Type> parameter in i_Dictionary2)
            {
                result[parameter.Key] = parameter.Value;
            }

            return result;
        }

        public static Dictionary<string, object> MergeDictionaries(
            Dictionary<string, object> i_Dictionary1,
            Dictionary<string, object> i_Dictionary2)
        {
            Dictionary<string, object> result = new Dictionary<string, object>();

            foreach (KeyValuePair<string, object> parameter in i_Dictionary1)
            {
                result[parameter.Key] = (string)parameter.Value;
            }

            foreach (KeyValuePair<string, object> parameter in i_Dictionary2)
            {
                result[parameter.Key] = parameter.Value;
            }

            return result;
        }
    }
}