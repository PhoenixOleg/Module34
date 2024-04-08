using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeApi.Contracts.Models.Rooms
{
    /// <summary>
    /// Модель для полного или частичного обновления помещения
    /// </summary>
    public class TransformRoomRequest
    {
        public string Name { get; set; } = string.Empty;
        public int Area { get; set; } = 0;
        public bool? GasConnected { get; set; }
        public int Voltage { get; set; } = 0;
    }
}
