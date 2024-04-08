using System;
using System.Threading.Tasks;
using AutoMapper;
using HomeApi.Contracts.Models.Devices;
using HomeApi.Contracts.Models.Rooms;
using HomeApi.Data.Models;
using HomeApi.Data.Queries;
using HomeApi.Data.Repos;
using Microsoft.AspNetCore.Mvc;

namespace HomeApi.Controllers
{
    /// <summary>
    /// Контроллер комнат
    /// </summary>
    [ApiController]
    [Route("[controller]")]
    public class RoomsController : ControllerBase
    {
        private readonly IRoomRepository _repository;
        private readonly IMapper _mapper;

        public RoomsController(IRoomRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        //TODO: Задание - добавить метод на получение всех существующих комнат @@@
        [HttpGet]
        [Route("")]
        public async Task<IActionResult> GetRooms()
        {
            var rooms = await _repository.GetRooms();

            var resp = new GetRoomsResponse
            {
                RoomAmount = rooms.Length,
                Rooms = _mapper.Map<Room[], RoomView[]>(rooms)
            };

            return StatusCode(200, resp);
        }

        /// <summary>
        /// Добавление комнаты
        /// </summary>
        [HttpPost] 
        [Route("")] 
        public async Task<IActionResult> Add([FromBody] AddRoomRequest request)
        {
            var existingRoom = await _repository.GetRoomByName(request.Name);
            if (existingRoom == null)
            {
                var newRoom = _mapper.Map<AddRoomRequest, Room>(request);
                await _repository.AddRoom(newRoom);
                return StatusCode(201, $"Комната {request.Name} добавлена!");
            }
            
            return StatusCode(409, $"Ошибка: Комната {request.Name} уже существует.");
        }

        /// <summary>
        /// Обновление комнаты с учетом перепланировки
        /// </summary>
        /// <param name="Name">Входной параметр - название помещения для модификации</param>
        /// <param name="request">Новые параметры помещения</param>
        /// <returns>Http код ответа с описанием</returns>
        [HttpPut]
        [Route("{Name}")]
        public async Task<IActionResult> Transform(
            [FromRoute] string Name,
            [FromBody] TransformRoomRequest request
            )
        {
            var room = await _repository.GetRoomByName(Name);
            if (room == null)
                return StatusCode(400, $"Ошибка: Комната {request.Name} не существует.");

            await _repository.TransformRoom(
                room, 
                new TransformRoomQuery(request.Name, request.Area, request.GasConnected, request.Voltage)
            );

            return StatusCode(200, $"Комната обновлена! Имя - {room.Name}, Площадь - {room.Area},  Подключение к газовому снабжению - {room.GasConnected}, Номинальное напряжение электросети - {room.Voltage}");
        }
    }
}