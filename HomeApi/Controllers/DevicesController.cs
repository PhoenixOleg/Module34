﻿using System;
using System.Threading.Tasks;
using AutoMapper;
using HomeApi.Contracts.Models.Devices;
using HomeApi.Data.Models;
using HomeApi.Data.Queries;
using HomeApi.Data.Repos;
using Microsoft.AspNetCore.Mvc;

namespace HomeApi.Controllers
{
    /// <summary>
    /// Контроллер устройсив
    /// </summary>
    [ApiController]
    [Route("[controller]")]
    public class DevicesController : ControllerBase
    {
        private readonly IDeviceRepository _devices;
        private readonly IRoomRepository _rooms;
        private readonly IMapper _mapper;
        
        public DevicesController(IDeviceRepository devices, IRoomRepository rooms, IMapper mapper)
        {
            _devices = devices;
            _rooms = rooms;
            _mapper = mapper;
        }
        
        /// <summary>
        /// Просмотр списка подключенных устройств
        /// </summary>
        [HttpGet] 
        [Route("")] 
        public async Task<IActionResult> GetDevices()
        {
            var devices = await _devices.GetDevices();

            var resp = new GetDevicesResponse
            {
                DeviceAmount = devices.Length,
                Devices = _mapper.Map<Device[], DeviceView[]>(devices)
            };
            
            return StatusCode(200, resp);
        }
            
        /// <summary>
        /// Добавление нового устройства
        /// </summary>
        [HttpPost] 
        [Route("")] 
        public async Task<IActionResult> Add( AddDeviceRequest request )
        {
            var room = await _rooms.GetRoomByName(request.RoomLocation);
            if(room == null)
                return StatusCode(400, $"Ошибка: Комната {request.RoomLocation} не подключена. Сначала подключите комнату!");
            
            var device = await _devices.GetDeviceByName(request.Name);
            if(device != null)
                return StatusCode(400, $"Ошибка: Устройство {request.Name} уже существует.");
            
            var newDevice = _mapper.Map<AddDeviceRequest, Device>(request);
            await _devices.SaveDevice(newDevice, room);
            
            return StatusCode(201, $"Устройство {request.Name} добавлено. Идентификатор: {newDevice.Id}");
        }
        
        /// <summary>
        /// Обновление существующего устройства
        /// </summary>
        [HttpPatch] 
        [Route("{id}")] 
        public async Task<IActionResult> Edit(
            [FromRoute] Guid id,
            [FromBody]  EditDeviceRequest request)
        {
         
            var room = await _rooms.GetRoomByName(request.NewLocation);
            if(room == null)
                return StatusCode(400, $"Ошибка: Комната {request.NewLocation} не подключена. Сначала подключите комнату!");
            
            var device = await _devices.GetDeviceById(id);
            if(device == null)
                return StatusCode(400, $"Ошибка: Устройство с идентификатором {id} не существует.");
            
            var withSameName = await _devices.GetDeviceByName(request.NewName);
            if(withSameName != null)
                return StatusCode(400, $"Ошибка: Устройство с именем {request.NewName} уже подключено. Выберите другое имя!");

            await _devices.UpdateDevice(
                device,
                room,
                new UpdateDeviceQuery(request.NewLocation, request.NewName, request.NewSerial)
            );

            return StatusCode(200, $"Устройство обновлено! Имя - {device.Name}, Серийный номер - {device.SerialNumber},  Комната подключения - {device.Room.Name}");
        }

        // TODO: Задание: напишите запрос на удаление устройства +
        /// <summary>
        /// Удаление существующего устройства
        /// </summary>
        /// <param name="id">ID (GUID) устройства</param>
        /// <returns>Http код ответа с описанием</returns>
        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> Delete(
            [FromRoute] Guid id
            )
        {
            var device = await _devices.GetDeviceById(id);
            if (device == null)
                return StatusCode(400, $"Ошибка: Устройство с идентификатором {id} не существует.");

            await _devices.DeleteDevice(device);

            return StatusCode(200, $"Устройство с идентификатором {device.Id} удалено! Имя - {device.Name}, Серийный номер - {device.SerialNumber},  Комната отключения - {device.Room.Name}");
        }
    }
}