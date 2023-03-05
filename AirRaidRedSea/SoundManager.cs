using Mogre;
using NAudio.Wave;
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
        private SoundPlayer currentSoundPlayer;

        public SoundPlayer CurrentSoundPlayer
        {
            get { return currentSoundPlayer; }
        }

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

        public void PlayLoop(string musicFileName)
        {
            if (currentSoundPlayer != null)
            {
                currentSoundPlayer.Stoploop();
            }

            IWaveProvider waveProvider = getWaveProvider(musicFileName);
            if (waveProvider != null)
            {
                currentSoundPlayer = new SoundPlayer(waveProvider);
                currentSoundPlayer.Playloop();
            }
        }

        public void StopCurrentLoop()
        {
            if (currentSoundPlayer != null)
            {
                currentSoundPlayer.Stoploop();
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
