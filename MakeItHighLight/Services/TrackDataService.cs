using MakeItHighLight.Models;
using NAudio.Lame;
using NAudio.Wave;
using NAudio.Wave.SampleProviders;
using System;
using System.Threading.Tasks;
using System.Windows.Forms;
using TagLib.Riff;

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

        internal static async Task<long[]> HighLight(Track item, Paths paths, Settings settings, bool isWav)
        {
            WaveStream waveStream;
            long[] firstAndLastSample;


            if (isWav)
            {
                WaveFileReader reader;
                reader = new WaveFileReader(item.Origpath);
                WaveFileWriter.CreateWaveFile(paths.Temppath + paths.Title, reader);

                waveStream = reader;
            }

            else
            {
                Mp3FileReader reader;
                reader = new Mp3FileReader(item.Origpath);
                WaveFileWriter.CreateWaveFile(paths.Temppath + paths.Title, reader);
                waveStream = reader;
            }

            await item.Tag.TagsToPath(paths.Temppath + paths.Title);
            waveStream.Flush();
            waveStream.Close();
            waveStream = new WaveFileReader(paths.Temppath + paths.Title);
            firstAndLastSample = TrackDataService.DefineFirstandLastSample(item, settings, (WaveFileReader)waveStream, paths.Temppath + paths.Title);
            await TrackDataService.TrimFile(paths.Temppath4 + paths.Title, firstAndLastSample, item, paths.Temppath + paths.Title);
            await item.Tag.TagsToPath(paths.Temppath4 + paths.Title);
            waveStream.Flush();
            waveStream.Close();
            return firstAndLastSample;
        }

        public static async Task TrimFile(string outputpath, long[] firstAndLastSample, Track item, string inputpath)
        {
            await using WaveFileReader reader = new WaveFileReader(inputpath);

            using (WaveFileWriter waveFileWriter = new WaveFileWriter(outputpath, reader.WaveFormat))
            {
                await TrimWavFile(reader, waveFileWriter, firstAndLastSample[0], firstAndLastSample[1]);
                waveFileWriter.Flush();
                waveFileWriter.Close();
            }
            reader.Flush();
            reader.Close();
        }
        private static async Task TrimWavFile(WaveFileReader reader, WaveFileWriter writer, long startPos, long endPos)
        {
            reader.Position = startPos;
            byte[] buffer = new byte[1024];
            while (reader.Position < endPos)
            {
                long bytesRequired = (long)(endPos - reader.Position);
                if (bytesRequired > 0)
                {
                    long temp = Math.Min(bytesRequired, buffer.Length);
                    int bytesToRead = (int)temp;
                    int bytesRead = reader.Read(buffer, 0, bytesToRead);
                    if (bytesRead > 0)
                    {
                        writer.Write(buffer, 0, bytesRead);
                    }
                    else
                    {
                        break;
                    }
                }
            }
        }

        internal static long[] DefineFirstandLastSample(Track item, Settings settings, WaveFileReader reader, string inputpath)
        {
            long[] firstAndLastSample = new long[2];

            var test1 = false;
            var test2 = false;
            double[] loudestsample = new double[2] { -100, -100 };
            double[] quietsample = new double[2];
            long loudestposition = 0;
            long quietposition = 0;
            bool firstrun = true;
            TimeSpan loudestTime = new TimeSpan();
            TimeSpan quietTime = new TimeSpan();

            long samples = reader.SampleCount;


            // Errechnet lautesten Wert sowie Position und Zeit / umgeht 0 exeption falls Schnitt
            for (int i = 0; i < samples; i++)
            {
                var test = reader.ReadNextSampleFrame();


                if ((reader.CurrentTime.TotalSeconds - 20 > 0) && (reader.CurrentTime.TotalSeconds + 15 < reader.TotalTime.TotalSeconds))
                {
                    if (
                 loudestsample[0] < Math.Log10(Math.Abs(test[0]))
                 &&
                 loudestsample[1] < Math.Log10(Math.Abs(test[1]))
                         )
                    {
                        loudestsample[0] = Math.Log10(Math.Abs(test[0]));
                        loudestsample[1] = Math.Log10(Math.Abs(test[1]));
                        loudestposition = reader.Position;
                        loudestTime = reader.CurrentTime;
                    }

                }
            }

            reader.Flush();
            reader.Close();
            reader = new WaveFileReader(inputpath);
            //Errechnet niedrigsten Wert voreingehend innerhalb einer 10 sek Spanne 

            for (int i = 0; i < samples; i++)
            {
                var test = reader.ReadNextSampleFrame();
                if (reader.CurrentTime.TotalSeconds + 20 > loudestTime.TotalSeconds)
                {
                    if (firstrun)
                    {
                        quietposition = reader.Position;
                        quietsample[0] = Math.Log10(Math.Abs(test[0]));
                        quietsample[1] = Math.Log10(Math.Abs(test[1]));
                        quietTime = reader.CurrentTime;
                        firstrun = false;
                    }
                    if (
                     quietsample[0] > Math.Log10(Math.Abs(test[0]))
                     &&
                     quietsample[1] > Math.Log10(Math.Abs(test[1]))
                     )
                    {
                        quietsample[0] = Math.Log10(Math.Abs(test[0]));
                        quietsample[1] = Math.Log10(Math.Abs(test[1]));
                        quietposition = reader.Position;
                        quietTime = reader.CurrentTime;
                        var testing = Math.Round(quietTime.TotalSeconds, 2);
                    }


                }

            }

       


            firstAndLastSample[0] = quietposition - (reader.WaveFormat.AverageBytesPerSecond * 5);



            firstAndLastSample[1] = loudestposition + (reader.WaveFormat.AverageBytesPerSecond * 5);


            reader.Flush();
            reader.Close();

            return firstAndLastSample;
        }

        internal static async void ConvertToMp3(Track item, Paths pathing)
        {
            using (var reader = new AudioFileReader(pathing.Finalpath + pathing.Title))
            {

                pathing.FinalpathDir = pathing.Finalpath;
                if (pathing.IsGenreDir)
                {
                    pathing.FinalpathDirGenre = pathing.Finalpath + pathing.Genre + "\\";

                    pathing.Finalpath = (pathing.Finalpath + pathing.Genre + "\\" + pathing.Title).Replace(".wav", ".mp3");
                }

                else
                {

                    pathing.Finalpath = (pathing.Finalpath + pathing.Title).Replace(".wav", ".mp3");
                }

                using (var writer = new LameMP3FileWriter(pathing.Finalpath, reader.WaveFormat, item.Tag.Bitrate))
                {
                    reader.CopyTo(writer);


                }
            }
        }

        internal static async Task FadeInAndOut(string inputpath, Paths paths, Track item, Settings settings)
        {
            TimeSpan time = default;

            time = GetTimeSpanFromPathWAV(inputpath);

            double timedouble = time.TotalMilliseconds;

            PerformFadeIn(
                inputpath,
                paths.Temppath3 + paths.Title,
                (long)timedouble,
                Double.Parse(settings.FadeinSecondsOut)
                );
            await item.Tag.TagsToPath(paths.Temppath3 + paths.Title);

            PerformFadeOut(
             paths.Temppath3 + paths.Title,
             paths.Finalpath + paths.Title,
             (long)timedouble,
             Double.Parse(settings.FadeinSecondsOut)
             );
            await item.Tag.TagsToPath(paths.Finalpath + paths.Title);
        }

        private static string PerformFadeOut(string inputPath, string outputPath, long totalmili, double secounds, bool playNoSave = false)
        {

            long fadeduration = (long)(secounds * 1000);
            long fadeat = totalmili - fadeduration;




            using (WaveFileReader waveSource = new WaveFileReader(inputPath))
            {
                ISampleProvider sampleSource = waveSource.ToSampleProvider();


                var fadeOut = new DelayFadeOutSampleProvider(sampleSource);

                fadeOut.BeginFadeOut(fadeat, fadeduration);
                WaveFileWriter.CreateWaveFile(outputPath, new SampleToWaveProvider(fadeOut));
                return outputPath;

            }


        }

        private static string PerformFadeIn(string inputpath, string outputpath, long timedouble, double secounds, bool playNoSave = false)
        {

            long fadeduration = (long)(secounds * 1000);
            using (WaveFileReader waveSource = new WaveFileReader(inputpath))
            {
                ISampleProvider sampleSource = waveSource.ToSampleProvider();
                var fadeOut = new DelayFadeOutSampleProvider(sampleSource);
                fadeOut.BeginFadeOut(0, fadeduration);
                WaveFileWriter.CreateWaveFile(outputpath, new SampleToWaveProvider(fadeOut));
                return outputpath;

            }






        }
        internal static async void FadeOut(string inputpath, Paths pathing, Track item, Settings settings)
        {
            TimeSpan time = default;

            time = GetTimeSpanFromPathWAV(inputpath);

            double timedouble = time.TotalMilliseconds;

            PerformFadeOut(
                inputpath,
                pathing.Finalpath + pathing.Title,
                (long)timedouble,
                Double.Parse(settings.FadeinSecondsOut)
                );
            await item.Tag.TagsToPath(pathing.Finalpath + pathing.Title);
        }
        internal static async void FadeIn(string inputpath, Paths pathing, Track item, Settings settings)
        {
            TimeSpan time = default;

            time = GetTimeSpanFromPathWAV(inputpath);

            double timedouble = time.TotalMilliseconds;

            PerformFadeIn(
                inputpath,
                pathing.Finalpath + pathing.Title,
                (long)timedouble,
                Double.Parse(settings.FadeinSecondsOut)
                );
            await item.Tag.TagsToPath(pathing.Finalpath + pathing.Title);

        }

        internal static async Task CopyFile(string inputpath, Paths paths, Track item)
        {
            WaveFileReader reader;
            reader = new WaveFileReader(inputpath);
            WaveFileWriter.CreateWaveFile(paths.Finalpath + paths.Title, reader);
            await item.Tag.TagsToPath(paths.Finalpath + paths.Title);
            reader.Flush();
            reader.Close();


        }
    }
}