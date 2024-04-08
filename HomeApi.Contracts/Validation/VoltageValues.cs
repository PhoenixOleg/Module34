using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeApi.Contracts.Validation
{
    /// <summary>
    /// Массив допустимых значений электроснабжения
    /// </summary>
    public class VoltageValues
    {
        public static int[] ValidVoltage = new[]
{
            120,
            220
        };
    }
}
