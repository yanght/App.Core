using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Microsoft.Extensions.Configuration;
using Serilog;

namespace App.Core.Common
{
    public class LoggerManager
    {
        public static void Debug(string message)
        {
            Log.Debug(message);
        }
        public static void Debug(Exception ex, string message)
        {
            Log.Error(ex, message);
        }

        public static void Warning(string message)
        {
            Log.Warning(message);
        }
        public static void Warning(Exception ex, string message)
        {
            Log.Error(ex, message);
        }

        public static void Info(string message)
        {
            Log.Information(message);
        }
        public static void Info(Exception ex, string message)
        {
            Log.Information(ex, message);
        }

        public static void Fatal(string message)
        {
            Log.Fatal(message);
        }
        public static void Fatal(Exception ex, string message)
        {
            Log.Error(ex, message);
        }

        public static void Error(string message)
        {
            Log.Error(message);
        }
        public static void Error(Exception ex, string message)
        {
            Log.Error(ex, message);
        }

        public static void Verbose(string message)
        {
            Log.Verbose(message);
        }
        public static void Verbose(Exception ex, string message)
        {
            Log.Verbose(ex, message);
        }

    }
}
