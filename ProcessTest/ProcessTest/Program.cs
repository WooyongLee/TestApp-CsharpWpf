using System;
using System.Diagnostics;
using System.Threading;

namespace ProcessTest
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Console.WriteLine("Hello World");

            ProcessStartInfo proInfo = new ProcessStartInfo();
            Process pro = new Process();

            proInfo.FileName = Environment.CurrentDirectory + @"\ffmpeg.exe";

            proInfo.CreateNoWindow = false;
            proInfo.UseShellExecute = false;
            proInfo.RedirectStandardOutput = true;
            proInfo.RedirectStandardInput = true;
            proInfo.RedirectStandardError = true;

            pro.StartInfo = proInfo;

            while (true)
            {
                pro.StartInfo.Arguments = GetCmdStr();
                pro.Start();

                pro.StandardInput.Close();
                string resultValue = pro.StandardOutput.ReadToEnd();

                pro.WaitForExit();
                pro.Close();
            }
        }

        public static string GetCmdStr()
        {
            DateTime nowDT = DateTime.Now;
            DateTime nextDT = nowDT.AddMinutes(FileRecvUtil.FileSavingMinute);
            // int nextMin = nowDT.Minute + FileRecvUtil.FileSavingMinute;

            // string cmdStr = @"-re -i D:\ACTD_DATA\Video\EO\20200228\testest.ts -map 0 -c:v copy -c:d copy -f mpegts rtsp://127.0.0.1:8554/testRtsp"; // rtsp 비디오 스트림 해보는거(안됨)
            // string cmdStr = string.Format(@"-re -i {0} -map 0 -c:v copy -c:d copy -f mpegts udp://@@{1}:{2}", @"D:\ACTD_DATA\Video\EO\20200228\testestUDP.ts", "224.10.11.11", "5009");

            // UDP : 됨
            // string cmdStr = @"-i udp://224.10.11.11:5001 -map 0 -c copy -f mpegts D:\ACTD_DATA\Video\EO\20200228\testestestUDP.ts"; // multicast udp로 수신하는 영상 파일 저장
            // string cmdStr = @"-i rtsp://127.0.0.1/testRtsp -map 0 -c copy -f mpegts D:\ACTD_DATA\Video\EO\20200228\testestRTSPCODE.ts"; // rtsp로 전송하는 영상 파일 저장

            string strNextDT = "20" + nextDT.Month.ToString().PadLeft(2, '0') + ":" + nextDT.Day.ToString() + ":" + nextDT.Hour.ToString() + ":" + nextDT.Minute.ToString() + ":" + nextDT.Second.ToString();
            string cmdStr = string.Format(@"-i rtsp://127.0.0.1/testRtsp -map 0 -c copy -f -t {0} mpegts D:\ACTD_DATA\Video\EO\{1}\{2}.ts",
                                           strNextDT, nowDT.ToString("yyyyMMdd"), nowDT.ToString("yyyyMMddHHmmss")); // rtsp로 전송하는 영상 파일 저장
            return cmdStr;
        }
        

        private int GetSleepTime(DateTime NowDT)
        {
            int savingMin = FileRecvUtil.FileSavingMinute;

            int nextMin = savingMin - (NowDT.Minute % savingMin);
            DateTime NextDT = new DateTime(NowDT.Year, NowDT.Month, NowDT.Day, NowDT.Hour, NowDT.Minute, 0).AddMinutes(nextMin);

            // 17.04.26 JSI : 소수점 올림을 위해 int로 캐스팅 후 1을 더함
            int sleepTime = (int)((NextDT - NowDT).TotalMilliseconds) + 1;
            return sleepTime;
        }
    }
    public static class FileRecvUtil
    {
        public const int FileSavingMinute = 1;
    }

}
