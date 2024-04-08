using System.Collections.Generic;
using System.Linq;
using FluentValidation;
using HomeApi.Contracts.Models.Devices;
using static HomeApi.Contracts.Validation.ManualValidators;

namespace HomeApi.Contracts.Validation
{
    /// <summary>
    /// Класс-валидатор запросов подключения
    /// </summary>
    public class AddDeviceRequestValidator : AbstractValidator<AddDeviceRequest>
    {
        /// <summary>
        /// Метод, конструктор, устанавливающий правила
        /// </summary>
        public AddDeviceRequestValidator() 
        {
            /* Зададим правила валидации */ 
            RuleFor(x => x.Name).NotEmpty(); // Проверим на null и на пустое свойство
            RuleFor(x => x.Manufacturer).NotEmpty();
            RuleFor(x => x.Model).NotEmpty();
            RuleFor(x => x.SerialNumber).NotEmpty();
            RuleFor(x => x.CurrentVolts)
                .NotEmpty()
                .Must(VoltageBeSupported) // Заменил проверку диапазона вольтажа на дискретные значения
                .WithMessage($"Please choose one of the following values: {string.Join(", ", VoltageValues.ValidVoltage)}");
            RuleFor(x => x.GasUsage).NotNull();
            RuleFor(x => x.RoomLocation)
                .NotEmpty()
                .Must(RoomBeSupported)
                .WithMessage($"Please choose one of the following locations: {string.Join(", ", RoomValues.ValidRooms)}");
        }        
    }
}