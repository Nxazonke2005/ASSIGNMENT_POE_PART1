using System;
using System.Collections.Generic;

namespace CyberSecurityAwarenessBot
{
    public class ActivityLogger
    {
        private static ActivityLogger instance;
        private List<string> activityLog = new List<string>();
        private int maxLogSize = 50;

        private ActivityLogger() { }
        // Uptated the Instance property to use a private static field for the singleton instance

        public static ActivityLogger Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new ActivityLogger();
                }
                return instance;
            }
        }

        public void LogAction(string action)
        {
            string timestamp = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            string logEntry = timestamp + " - " + action;

            activityLog.Insert(0, logEntry);

            if (activityLog.Count > maxLogSize)
            {
                activityLog.RemoveAt(activityLog.Count - 1);
            }

            Console.WriteLine("LOG: " + logEntry);
        }

        public List<string> GetRecentLogs(int count)
        {
            int takeCount = Math.Min(count, activityLog.Count);
            return activityLog.GetRange(0, takeCount);
        }

        public List<string> GetAllLogs()
        {
            return new List<string>(activityLog);
        }

        public void ClearLog()
        {
            activityLog.Clear();
            LogAction("Log cleared");
        }

        public int GetLogCount()
        {
            return activityLog.Count;
        }
    }
}