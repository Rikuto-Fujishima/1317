using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
namespace clock
{
    class Time
    {
        private int h;
        private int m;
        private int s;
        public int Hour => h;
        public int Minute => m;
        public int Second => s;
        public Time(int h = 0, int m = 0, int s = 0)
        {
            this.h = h;
            this.m = m;
            this.s = s;
        }
        public bool valid()
        {
            return h > 0 && m >= 0 && s >= 0
                && h < 24 && m < 60 && s < 60;
        }
        public override bool Equals(object obj)
        {
            var time = obj as Time;
            return time != null &&
                   Hour == time.Hour &&
                   Minute == time.Minute &&
                   Second == time.Second;
        }
        public override int GetHashCode()
        {
            var hashCode = 1505761165;
            hashCode = hashCode * -1521134295 + Hour.GetHashCode();
            hashCode = hashCode * -1521134295 + Minute.GetHashCode();
            hashCode = hashCode * -1521134295 + Second.GetHashCode();
            return hashCode;
        }
        public override string ToString()
        {
            return Hour + ":" + Minute + ":" + Second;
        }
    }
    class MainClass
    {
        static void Main(string[] args)
        {
            try
            {
                AlarmClock clock = new AlarmClock(); 
                clock.AlarmTime = new Time(DateTime.Now.Hour,
                              DateTime.Now.Minute,
                              DateTime.Now.Second + 5);
                int total = 0;
                clock.TickEvent += (c => Count(c, ref total));
                new Thread(clock.Run).Start();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            static void Count(AlarmClock sender, ref int total)
            {
                Time time = sender.CurrentTime;
                total = total + 1;
                Console.WriteLine($"Total= {total}");
            }
            static void PlayMusic(AlarmClock sender)
            {
                Time time = sender.CurrentTime;
                Console.WriteLine($"Event: {time.Hour}:{time.Minute}:{time.Second}");
                Console.WriteLine("Playing.");
            }
        }
        class AlarmClock
        {
            public event Action<AlarmClock> AlarmEvent;
            public event Action<AlarmClock> TickEvent;
            public AlarmClock()
            {
                CurrentTime = new Time();
                TickEvent += c => Console.WriteLine("Tick!" + CurrentTime);
                AlarmEvent += c => Console.WriteLine("Alarm!");
            }
            public Time CurrentTime { get; set; }
            public Time AlarmTime { get; set; }
            public void Run()
            {
                while (true)
                {
                    DateTime now = DateTime.Now;
                    CurrentTime = new Time(now.Hour, now.Minute, now.Second);
                    TickEvent(this);
                    if (AlarmTime.Equals(CurrentTime))
                        AlarmEvent(this);
                    Thread.Sleep(1000);
                }
            }
        }
    }
}