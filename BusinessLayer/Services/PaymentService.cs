using BusinessLayer.Contracts;
using BusinessLayer.Dto;
using System;

namespace BusinessLayer.Services
{
    public class PaymentService
    {
        private readonly UsersService _usersService;

        public PaymentService(UsersService usersService)
        {
            _usersService = usersService;
        }

        public double CalculatePayment(Guid userId)
        {
            UserDto user = _usersService.GetUser(userId);
            IPaymentStrategy paymentStrategy;

            if (user.Student)
            {
                paymentStrategy = new StudentDiscountStrategy();
            }
            else
            {
                paymentStrategy = new NoDiscountStrategy();
            }

            PaymentCalculator paymentCalculator = new PaymentCalculator(paymentStrategy, _usersService);
            return paymentCalculator.GetUserHasToPay(userId);
        }
    }
}
