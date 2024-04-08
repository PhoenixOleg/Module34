using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeApi.Contracts.Validation
{
    internal static class ManualValidators
    {
        /// <summary>
        ///  Метод кастомной валидации для свойства Voltage (номинал напряжения электроснабжения)
        /// </summary>
        internal static bool VoltageBeSupported(int voltage)
        {
            return VoltageValues.ValidVoltage.Any(v => v == voltage);
        }

        /// <summary>
        ///  Метод кастомной валидации для свойства location (тип рпомещения)
        /// </summary>
        internal static bool RoomBeSupported(string location)
        {
            // Проверим, содержится ли значение в списке допустимых
            return RoomValues.ValidRooms.Any(e => e == location);
        }
    }
}
