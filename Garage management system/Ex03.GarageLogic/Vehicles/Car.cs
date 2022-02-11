using System.Text;

namespace Ex03.GarageLogic
{
    public class Car : Vehicle
    {
        private eColor m_Color;
        private eNumberDoors m_NumberDoors;

        internal Car(eColor i_Color,
                     eNumberDoors i_NumerDoors,
                     string i_NameModel,
                     string i_LicenseNumber,
                     System.Collections.Generic.List<Wheel> i_Wheels,
                     Battery i_Battery)
            : base(i_NameModel, i_LicenseNumber, i_Wheels, i_Battery)
        {
            m_Color = i_Color;
            m_NumberDoors = i_NumerDoors;
        }

        public enum eNumberDoors
        {
            Two = 2,
            Three,
            Four,
            Five
        }

        public enum eColor
        {
            Red = 1,
            White,
            Black,
            Silver
        }

        public eColor Color
        {
            get
            {
                return m_Color;
            }
        }

        public eNumberDoors NumberDoors
        {
            get
            {
                return m_NumberDoors;
            }
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();

            sb.AppendLine($"{base.ToString()}");
            sb.AppendLine($"{Keywords.k_Color}: {m_Color}");
            sb.AppendLine($"{Keywords.k_NumberOfDoors}: {m_NumberDoors}");

            return sb.ToString();
        }
    }
}
