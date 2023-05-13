using NAudio.Wave;
using System;
using System.Threading.Tasks;

namespace MakeItHighLight.Services
{
    internal class TrackDataService
    {
        public static async Task<TimeSpan> GetTimeSpanFromPathMP3(string path)
        {
            try
            {
                using (var reader = new Mp3FileReader(path))
                {

                    return reader.TotalTime;

                }

            }
            catch (Exception)
            {

                return TimeSpan.Zero;
            }




        }

        public static TimeSpan GetTimeSpanFromPathWAV(string path)
        {

            using (WaveFileReader reader = new WaveFileReader(path))
            {
                WaveStream pcm = reader;
                return pcm.TotalTime;

            }
        }
    }
}