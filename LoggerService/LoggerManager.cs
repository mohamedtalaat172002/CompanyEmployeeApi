using Contracts;
using NLog;

namespace LoggerService
{
    public class LoggerManger : ILoggerManager
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();

        public LoggerManger()
        {
            
        }

        public void DebuggingLog(string message) => logger.Debug(message);


        public void errorLog(string message) => logger.Error(message);
      

        public void InfoLog(string message)=> logger.Info(message);
       

        public void warningLog(string message)=> logger.Warn(message);
      
    }
}