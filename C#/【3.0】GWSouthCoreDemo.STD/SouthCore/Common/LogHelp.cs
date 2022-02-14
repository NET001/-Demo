using GWDataCenter;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;
using System.Threading;

namespace SouthCore.Common
{
    public static class LogHelp
    {
        private static string logName = "";
        private const LogTypeEnum logTypeEnum = LogTypeEnum.LogManage;
        private static ReaderWriterLockSlim LogWriteLock = new ReaderWriterLockSlim();

        enum LogTypeEnum
        {
            XLog,
            DllLog,
            LogManage,
        }
        public static void WriteLogFile(string text, Exception ex = null)
        {
            string logContext = "【" + logName + "】" + text + "(" + ex?.ToString() + ")";
            switch (logTypeEnum)
            {
                case LogTypeEnum.XLog:
                    {
                        DataCenter.WriteLogFile(logContext);
                    }
                    break;
                case LogTypeEnum.LogManage:
                    {
                        try
                        {
                            string logFilePath = "../log/" + logName.Split('.')[0];
                            LogWriteLock.EnterWriteLock();
                            DateTime dT = DateTime.Now;
                            string fileName = $"../log/" + logName.Split('.')[0] + $"/Info-{dT.Year}-{dT.Month}-{dT.Day}.txt";
                            if (!Directory.Exists(logFilePath))
                            {
                                //创建文件夹
                                Directory.CreateDirectory(logFilePath);
                            }
                            using (StreamWriter writer = System.IO.File.AppendText(fileName))
                            {
                                writer.WriteLine($"{dT.Hour}:{dT.Minute}:{dT.Second}>>{logContext}");
                            }
                        }
                        catch (Exception _ex)
                        {
                            DataCenter.WriteLogFile($"写入文件出错！{_ex.Message}");
                        }
                        finally
                        {
                            LogWriteLock.ExitWriteLock();
                        }
                    }
                    break;
            }
        }
    }
}
