using FluentValidation;
using HomeApi.Contracts.Models.Rooms;
using static HomeApi.Contracts.Validation.ManualValidators;

namespace HomeApi.Contracts.Validation
{
    /// <summary>
    /// Класс-валидатор запросов на добавление новой комнаты
    /// </summary>
    public class AddRoomRequestValidator : AbstractValidator<AddRoomRequest>
    {
        public AddRoomRequestValidator() 
        {
            RuleFor(x => x.Area).NotEmpty(); 
            RuleFor(x => x.Name)
                .NotEmpty()
                .Must(RoomBeSupported)
                .WithMessage($"Please choose one of the following locations: {string.Join(", ", RoomValues.ValidRooms)}");
            RuleFor(x => x.Voltage)
                .NotEmpty()
                .Must(VoltageBeSupported) // Заменил проверку диапазона вольтажа на дискретные значения
                .WithMessage($"Please choose one of the following values: {string.Join(", ", VoltageValues.ValidVoltage)}");
            RuleFor(x => x.GasConnected).NotNull(); //NotEmpty заменено, т. к. не пропустит дефолтное значение False. И все помещения получатся газифицированные =)
        }
    }
}