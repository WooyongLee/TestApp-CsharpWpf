using System;
using System.Diagnostics;
using System.Threading;

namespace ProcessTest
{
    public  class Program
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
            // string cmdStr = @"-re -i D:\ACTD_DATA\Video\EO\20200228\testest.ts -map 0 -c:v copy -c:d copy -f mpegts rtsp://127.0.0.1:8554/testRtsp"; // rtsp 비디오 스트림 해보는거(안됨)
            // string cmdStr = string.Format(@"-re -i {0} -map 0 -c:v copy -c:d copy -f mpegts udp://@@{1}:{2}", @"D:\ACTD_DATA\Video\EO\20200228\testestUDP.ts", "224.10.11.11", "5009");

            // UDP : 됨
            // string cmdStr = @"-i udp://224.10.11.11:5001 -map 0 -c copy -f mpegts D:\ACTD_DATA\Video\EO\20200228\testestestUDP.ts"; // multicast udp로 수신하는 영상 파일 저장
            string cmdStr = @"-i rtsp://127.0.0.1:8554/testRtsp -map 0 -c copy -f mpegts D:\ACTD_DATA\Video\EO\20200228\testestRTSP.ts"; // rtsp로 전송하는 영상 파일 저장(cmd창에서는 됨, 여기서 안됨)
            pro.StartInfo.Arguments =cmdStr;
            pro.Start();

            // pro.StandardInput.Write(cmd);
            pro.StandardInput.Close();

            string resultValue = pro.StandardOutput.ReadToEnd();
            pro.WaitForExit();
            pro.Close();

            Console.WriteLine(resultValue);

            Thread.Sleep(100000);
        }
    }
}
