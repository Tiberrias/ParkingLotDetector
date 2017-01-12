using System;

namespace ParkingLotDetector.Services.Interfaces
{
    public interface ILoggingService
    {
        event Action<string> MessageLogged;

        void Log(string message);
    }
}