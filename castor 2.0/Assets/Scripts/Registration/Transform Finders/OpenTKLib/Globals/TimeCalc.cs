using System;

namespace OpenTKLib
{
    
    public class TimeCalc
    {
        public static DateTime CurrentTime;

        public static void ShowLastTimeSpan(string name)
        {

            DateTime now = DateTime.Now;
            TimeSpan ts = now - CurrentTime;
            System.Diagnostics.Debug.WriteLine("--Duration for " + name + " : " + ts.TotalMilliseconds.ToString() + " - miliseconds");
            CurrentTime = now;
        }
        public static void ResetTime()
        {
            CurrentTime = DateTime.Now;

        }
        

    }
}
