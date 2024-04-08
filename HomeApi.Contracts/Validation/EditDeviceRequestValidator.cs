using System.Collections.Generic;
using System.Linq;
using FluentValidation;
using HomeApi.Contracts.Models.Devices;
using static HomeApi.Contracts.Validation.ManualValidators;

namespace HomeApi.Contracts.Validation
{
    /// <summary>
    /// Класс-валидатор запросов обновления устройства
    /// </summary>
    public class EditDeviceRequestValidator : AbstractValidator<EditDeviceRequest>
    {
        /// <summary>
        /// Метод, конструктор, устанавливающий правила
        /// </summary>
        public EditDeviceRequestValidator() 
        {
            //RuleFor(x => x.NewName).NotEmpty(); //Убрано условие NotEmpty, т. к. не позволят не обновлять имя девайся
            RuleFor(x => x.NewLocation)
                .NotEmpty()
                .Must(RoomBeSupported)                
                .WithMessage($"Please choose one of the following locations: {string.Join(", ", RoomValues.ValidRooms)}");
        }
    }
}