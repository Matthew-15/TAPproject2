﻿using BusinessLayer.Contracts;
using BusinessLayer.Dto;
using DataAccessLayer;
using DataAccessLayer.Models;
using DataAccessLayer.Repository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Services
{
    public class HotelReservationsService
    {
        private readonly IRepository<HotelReservation> _repository;
        private readonly BusinessLayer.Contracts.ILogger _logger;

        public HotelReservationsService(IRepository<HotelReservation> _repository, ILogger _logger)
        {
            this._repository = _repository;
            this._logger = _logger;
        }

        public HotelReservationDto GetHotelReservation(Guid id)
        {
            HotelReservation hotelReservation;
            try
            {
                hotelReservation = _repository.GetById(id);
                if (hotelReservation == null)
                {
                    throw new NullReferenceException();
                }
            }
            catch
            {
                throw new NullReferenceException();
            }

            var hotelReservationDto = new HotelReservationDto
            {
                Id = hotelReservation.Id,
                IdHotel = hotelReservation.IdHotel,
                IdUser = hotelReservation.IdUser,
                RoomType = hotelReservation.RoomType,
                CheckIn = hotelReservation.CheckIn,
                CheckOut = hotelReservation.CheckOut,
                Price = hotelReservation.Price
            };

            _logger.LogHotelReservationRequestFromDB(hotelReservation);

            return hotelReservationDto;
        }

        public void AddHotelReservation(HotelReservationDto hotelReservationDto)
        {
            var hotelReservation = new HotelReservation
            {
                IdHotel = hotelReservationDto.IdHotel,
                IdUser = hotelReservationDto.IdUser,
                CheckIn = hotelReservationDto.CheckIn,
                CheckOut = hotelReservationDto.CheckOut,
                RoomType = hotelReservationDto.RoomType,
                Price = hotelReservationDto.Price
            };

            _logger.LogHotelReservationInsertRequestToDB(hotelReservation);

            _repository.Add(hotelReservation);
            _repository.SaveChanges();
        }

        public void UpdateHotelReservation(Guid id, HotelReservationDto updatedHotelReservationDto)
        {
            HotelReservation hotelReservation;
            try
            {
                hotelReservation = _repository.GetById(id);
                if (hotelReservation == null)
                {
                    throw new NullReferenceException();
                }
            }
            catch
            {
                throw new NullReferenceException();
            }

            var oldHotelReservation = hotelReservation;

            hotelReservation.CheckIn = updatedHotelReservationDto.CheckIn;
            hotelReservation.CheckOut = updatedHotelReservationDto.CheckOut;
            hotelReservation.RoomType = updatedHotelReservationDto.RoomType;
            hotelReservation.Price = updatedHotelReservationDto.Price;
            hotelReservation.IdHotel = updatedHotelReservationDto.IdHotel;
            hotelReservation.IdUser = updatedHotelReservationDto.IdUser;

            _logger.LogHotelReservationUpdateRequestInDB(oldHotelReservation, hotelReservation);

            _repository.Update(hotelReservation);
            _repository.SaveChanges();
        }

        public void DeleteHotelReservation(Guid id)
        {
            HotelReservation hotelReservation;
            try
            {
                hotelReservation = _repository.GetById(id);
                if (hotelReservation == null)
                {
                    throw new NullReferenceException();
                }
            }
            catch
            {
                throw new NullReferenceException();
            }

            _logger.LogHotelReservationDeleteRequestFromDB(hotelReservation);

            _repository.Remove(hotelReservation);
            _repository.SaveChanges();
        }
    }
}
