using Mogre;
using NAudio.Vorbis;
using NAudio.Wave;
using NVorbis;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AirRaidRedSea
{
    public class SoundManager
    {
        private WaveOut waveOut;
        private Dictionary<string, SoundPlayer> soundPlayers;

        private static SoundManager instance;
        public static SoundManager Instance
        {
            get
            {
                if(instance == null)
                    instance = new SoundManager();
                return instance;
            }
        }

        public SoundManager() 
        {
            waveOut = new WaveOut();
            soundPlayers = new Dictionary<string, SoundPlayer>();
        }

        public void PlaySound(string soundFileName)
        {
            IWaveProvider waveProvider = getWaveProvider(soundFileName);
            if (waveProvider != null)
            {
                waveOut.Init(waveProvider);
                waveOut.Play();
            }
        }

        public void PlayLoop(string musicFileName, string name = "")
        {
            IWaveProvider waveProvider = getWaveProvider(musicFileName);
            if (waveProvider != null)
            {
                var soundPlayer = new SoundPlayer(waveProvider);
                if (!string.IsNullOrEmpty(name))
                {
                    soundPlayers[name] = soundPlayer;
                }
                else
                {
                    soundPlayers[musicFileName] = soundPlayer;
                }
            }
        }

        public void StopCurrentLoop(string name = "")
        {
            if (soundPlayers.Count == 0)
                return;

            if(!string.IsNullOrEmpty(name))
            {
                soundPlayers[name].Stoploop();
                soundPlayers.Remove(name);
            }
            else
            {
                soundPlayers.ElementAt(0).Value.Stoploop();
                soundPlayers.Remove(soundPlayers.ElementAt(0).Key);
            }
        }

        private IWaveProvider getWaveProvider(string soundFileName)
        {
            DataStreamPtr soundDataStream = ResourceGroupManager.Singleton.OpenResource(soundFileName,
                ResourceGroupManager.DEFAULT_RESOURCE_GROUP_NAME);

            Stream soundStream = Helper.DataPtrToStream(soundDataStream);

            string extension = Path.GetExtension(soundFileName);
            switch(extension)
            {
                case ".mp3":
                    Mp3FileReader mp3FileReader = new Mp3FileReader(soundStream);
                    return mp3FileReader;
                case ".wave":
                    WaveFileReader waveFileReader= new WaveFileReader(soundStream);
                    return waveFileReader;
                case ".aiff":
                    AiffFileReader aiffFileReader = new AiffFileReader(soundStream);
                    return aiffFileReader;
                case ".ogg":
                    VorbisWaveReader vorbisWaveReader = new VorbisWaveReader(soundStream);
                    return vorbisWaveReader;
                default: 
                    return null;
            }
        }
    }

    public class SoundPlayer
    {
        private ManualResetEvent mre;
        private IWaveProvider waveProvider;
        private WaveOut waveOut;

        private Thread thread;
        private bool isStopped;

        public SoundPlayer(IWaveProvider waveProvider) 
        {
            this.waveProvider = waveProvider;
            waveOut = new WaveOut();

            mre = new ManualResetEvent(false);
            thread = new Thread(soundLoopListener);
        }

        private void soundLoopListener()
        {
            while(!isStopped) 
            {
                mre.WaitOne();
                if(waveOut.PlaybackState== PlaybackState.Stopped)
                {
                    waveOut.Play();
                }
            }
        }

        public void Playloop()
        {
            waveOut.Init(waveProvider);
            waveOut.Play();

            isStopped = false;

            mre.Set();
            thread.Start();
        }

        public void Stoploop()
        {
            mre.Reset();
            thread.Abort();
        }
    }
}