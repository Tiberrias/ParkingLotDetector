using System;
using ParkingLotDetector.Services.Interfaces;

namespace ParkingLotDetector.Services
{
    public class LoggingService : ILoggingService
    {

        public event Action<string> MessageLogged = delegate { };

        public void Log(string message)
        {
            MessageLogged?.Invoke(DateTime.Now.ToShortTimeString() + ": "+ message);
        }
    }
}