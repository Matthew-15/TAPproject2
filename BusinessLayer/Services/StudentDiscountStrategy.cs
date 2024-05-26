using BusinessLayer.Contracts;
using BusinessLayer.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace BusinessLayer.Services
{
    public class StudentDiscountStrategy : IPaymentStrategy
    {
        public double CalculateTotalPrice(UserDto userDto, List<HotelReservationDto> hotelReservationsDtos, List<FlightReservationDto> flightReservationsDtos, bool isStudent)
        {
            double totalPrice = 0;

            foreach (var hotelReservationDto in hotelReservationsDtos)
            {
                totalPrice += hotelReservationDto.Price;
            }

            foreach (var flightReservationDto in flightReservationsDtos)
            {
                totalPrice += flightReservationDto.Price;
            }

            // Aplicăm reducerea de 15% pentru studenți
            if (isStudent)
            {
                totalPrice *= 0.85;
            }

            return totalPrice;
        }
    }
}
