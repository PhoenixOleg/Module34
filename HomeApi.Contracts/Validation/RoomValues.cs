namespace HomeApi.Contracts.Validation
{
    /// <summary>
    /// Класс-хранилище допустымых значений типа помещений для валидаторов помещений
    /// </summary>
    public static class RoomValues
    {
        internal static string [] ValidRooms = new  []
        {
            "Кухня",
            "Ванная",
            "Гостиная",
            "Туалет",
            "Спальня"
        };
    }
}