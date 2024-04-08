using System.ComponentModel.DataAnnotations;

namespace HomeApi.Data.Queries
{
    /// <summary>
    /// Класс для передачи дополнительных параметров при обновлении устройства
    /// </summary>
    public class UpdateDeviceQuery
    {
        public string NewName { get; }
        public string NewSerial { get; }
        [Required]
        public string NewLocation { get; }

        public UpdateDeviceQuery(string newLocation, string newName = null, string newSerial = null)
        {
            NewName = newName;
            NewSerial = newSerial;
            NewLocation = newLocation;
        }
    }
}