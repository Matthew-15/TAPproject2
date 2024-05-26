using BusinessLayer.Contracts;
using DataAccessLayer.Models;
using System;
using System.IO;

namespace BusinessLayer.Services
{
    public class Logger : ILogger
    {
        private readonly object _lock = new object();

        public void Log(string logPath, string log)
        {
            try
            {
                Console.Write(log);
                lock (_lock)
                {
                    File.AppendAllText(logPath, log);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Failed to log: {ex.Message}");
            }
        }

        public string GetLogPath()
        {
            string logPath = $"../BusinessLayer/Logs/{DateTime.Now:dd.MM.yyyy}.log";
            lock (_lock)
            {
                if (!File.Exists(logPath))
                {
                    File.WriteAllText(logPath, "");
                }
            }

            string log = $"[{DateTime.Now}]: ";
            Log(logPath, log);

            return logPath;
        }

        private string ObjectToString(object obj)
        {
            switch (obj)
            {
                case Airline airline:
                    return $@"
    Id:               {airline.Id}
    Name:             {airline.Name}
    Location:         {airline.Location}
    ChairsLeft:       {airline.ChairsLeft}
";
                case FlightReservation flightReservation:
                    return $@"
    Id:                         {flightReservation.Id}
    IdAirline:                  {flightReservation.IdAirline}
    IdUser:                     {flightReservation.IdUser}
    Chair:                      {flightReservation.Chair}
    FlightDate:                 {flightReservation.FlightDate}
    FlightDuration:             {flightReservation.FlightDuration}
    CheckInFlightLocation:      {flightReservation.CheckInFlightLocation}
    CheckOutFlightLocation:     {flightReservation.CheckOutFlightLocation}
    Price:                      {flightReservation.Price}
";
                case HotelReservation hotelReservation:
                    return $@"
    Id:             {hotelReservation.Id}
    IdHotel:        {hotelReservation.IdHotel}
    IdUser:         {hotelReservation.IdUser}
    RoomType:       {hotelReservation.RoomType}
    CheckIn:        {hotelReservation.CheckIn}
    CheckOut:       {hotelReservation.CheckOut}
    Price:          {hotelReservation.Price}
";
                case Hotel hotel:
                    return $@"
    Id:             {hotel.Id}
    Name:           {hotel.Name}
    Location:       {hotel.Location}
    RoomsLeft:      {hotel.RoomsLeft}
";
                case User user:
                    return $@"
    Id:               {user.Id}
    Username:         {user.Username}
    Email:            {user.Email}
    Active:           {user.Active}
    Student:          {user.Student}
";
                default:
                    return "Unknown object type";
            }
        }

        private void LogRequest(string action, object obj)
        {
            string logPath = GetLogPath();
            string log = $"{action} Request of a {obj.GetType().Name} from/to the database:\n{ObjectToString(obj)}";
            Log(logPath, log);
        }

        public void LogAirlineRequestFromDB(Airline airline) => LogRequest("Get", airline);
        public void LogFlightReservationRequestFromDB(FlightReservation flightReservation) => LogRequest("Get", flightReservation);
        public void LogHotelReservationRequestFromDB(HotelReservation hotelReservation) => LogRequest("Get", hotelReservation);
        public void LogHotelRequestFromDB(Hotel hotel) => LogRequest("Get", hotel);
        public void LogUserRequestFromDB(User user) => LogRequest("Get", user);

        public void LogAirlineInsertRequestToDB(Airline airline) => LogRequest("Insert", airline);
        public void LogFlightReservationInsertRequestToDB(FlightReservation flightReservation) => LogRequest("Insert", flightReservation);
        public void LogHotelReservationInsertRequestToDB(HotelReservation hotelReservation) => LogRequest("Insert", hotelReservation);
        public void LogHotelInsertRequestToDB(Hotel hotel) => LogRequest("Insert", hotel);
        public void LogUserInsertRequestToDB(User user) => LogRequest("Insert", user);

        public void LogAirlineUpdateRequestInDB(Airline oldAirline, Airline newAirline) => LogRequest("Update", oldAirline, newAirline);
        public void LogFlightReservationUpdateRequestInDB(FlightReservation oldFlightReservation, FlightReservation newFlightReservation) => LogRequest("Update", oldFlightReservation, newFlightReservation);
        public void LogHotelReservationUpdateRequestInDB(HotelReservation oldHotelReservation, HotelReservation newHotelReservation) => LogRequest("Update", oldHotelReservation, newHotelReservation);
        public void LogHotelUpdateRequestInDB(Hotel oldHotel, Hotel newHotel) => LogRequest("Update", oldHotel, newHotel);
        public void LogUserUpdateRequestInDB(User oldUser, User newUser) => LogRequest("Update", oldUser, newUser);

        public void LogAirlineDeleteRequestFromDB(Airline airline) => LogRequest("Delete", airline);
        public void LogFlightReservationDeleteRequestFromDB(FlightReservation flightReservation) => LogRequest("Delete", flightReservation);
        public void LogHotelReservationDeleteRequestFromDB(HotelReservation hotelReservation) => LogRequest("Delete", hotelReservation);
        public void LogHotelDeleteRequestFromDB(Hotel hotel) => LogRequest("Delete", hotel);
        public void LogUserDeleteRequestFromDB(User user) => LogRequest("Delete", user);

        private void LogRequest(string action, object oldObj, object newObj)
        {
            string logPath = GetLogPath();
            string log = $"{action} Request of a {oldObj.GetType().Name} in the database:\n  Old {oldObj.GetType().Name}:\n{ObjectToString(oldObj)}  New {oldObj.GetType().Name}:\n{ObjectToString(newObj)}";
            Log(logPath, log);
        }
    }
}
