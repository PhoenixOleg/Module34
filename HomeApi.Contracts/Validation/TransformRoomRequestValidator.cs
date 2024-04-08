using FluentValidation;
using HomeApi.Contracts.Models.Rooms;
using static HomeApi.Contracts.Validation.ManualValidators;

namespace HomeApi.Contracts.Validation
{
    /// <summary>
    /// Класс-валидатор запросов на обновление помещения (полное или частичное)
    /// </summary>
    public class TransformRoomRequestValidator : AbstractValidator<TransformRoomRequest>
    {
        /// <summary>
        /// Метод, конструктор, устанавливающий правила
        /// </summary>
        public TransformRoomRequestValidator()
        {
            //Название помещения должно быть из перечня
            RuleFor(x => x.Name)
                .Must(RoomBeSupported)
                .When (x =>  !string.IsNullOrEmpty(x.Name))
                .WithMessage($"Please choose one of the following locations: {string.Join(", ", RoomValues.ValidRooms)}");
            //Вольтаж должен быть изперечня, если задан
            RuleFor(X => X.Voltage)
                .Must(VoltageBeSupported)
                .When(x => x.Voltage != 0)
                .WithMessage("Acceptable voltage values are 120, 220. As well as an unspecified value (not set).");
            //Площадь помещения должна быть > 0, если задана
            RuleFor(x => x.Area)
                .GreaterThan(0)
                .When(x => x.Area != 0)
                .WithMessage("Acceptable area values are greater then 0. As well as an unspecified value (not set).");
        }
    }
}
