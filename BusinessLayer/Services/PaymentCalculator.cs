﻿using BusinessLayer.Contracts;
using BusinessLayer.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Services
{
    public class PaymentCalculator
    {
        private readonly IPaymentStrategy _paymentStrategy;
        private readonly UsersService _usersService;

        public PaymentCalculator(IPaymentStrategy paymentStrategy, UsersService usersService)
        {
            this._paymentStrategy = paymentStrategy;
            this._usersService = usersService;
        }

        public double GetUserHasToPay(Guid id)
        {
            List<HotelReservationDto> hotelReservationsDtos = _usersService.GetUserHotelReservations(id);
            List<FlightReservationDto> flightReservationsDtos = _usersService.GetUserFlightReservations(id);
            UserDto userDto = _usersService.GetUser(id);

            return _paymentStrategy.CalculateTotalPrice(userDto, hotelReservationsDtos, flightReservationsDtos, userDto.Student);
        }
    }
}
