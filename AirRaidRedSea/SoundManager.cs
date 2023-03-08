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

        public event Action<string> OnSoundPlayerTriggerEvent;

        public SoundManager() 
        {
            waveOut = new WaveOut();
            soundPlayers = new Dictionary<string, SoundPlayer>();
        }

        public void PlaySound(string soundFileName, string name = "")
        {
            if (string.IsNullOrEmpty(name))
                name = Path.GetFileNameWithoutExtension(soundFileName);

            IWaveProvider waveProvider = getWaveProvider(soundFileName);
            if (waveProvider != null)
            {
                var soundPlayer = new SoundPlayer(name, waveProvider, SoundPlayEventType.JustPlay);
                soundPlayer.Play();
                soundPlayers[name] = soundPlayer;
            }
        }

        public void PlayEvent(string soundFileName, string name = "")
        {
            if (string.IsNullOrEmpty(name))
                name = Path.GetFileNameWithoutExtension(soundFileName);

            IWaveProvider waveProvider = getWaveProvider(soundFileName);
            if (waveProvider != null)
            {
                var soundPlayer = new SoundPlayer(name, waveProvider, SoundPlayEventType.TriggerEventWhenStopped);
                soundPlayer.Play();
                soundPlayer.OnTriggerEvent += SoundPlayer_OnTriggerEvent;
                soundPlayers[name] = soundPlayer;
            }
        }

        private void SoundPlayer_OnTriggerEvent(SoundPlayer sp)
        {
            OnSoundPlayerTriggerEvent?.Invoke(sp.Name);
        }

        public void PlayLoop(string musicFileName, string name = "")
        {
            if(string.IsNullOrEmpty(name))
                name = Path.GetFileNameWithoutExtension(musicFileName);

            IWaveProvider waveProvider = getWaveProvider(musicFileName);
            if (waveProvider != null)
            {
                var soundPlayer = new SoundPlayer(name, waveProvider, SoundPlayEventType.Loop);
                soundPlayers[name] = soundPlayer;
                soundPlayer.Play();
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

    public enum SoundPlayEventType
    {
        JustPlay,
        TriggerEventWhenStopped,
        Loop,
    }

    public class SoundPlayer
    {
        private SoundPlayEventType eventType;
        private ManualResetEvent mre;
        private IWaveProvider waveProvider;
        private WaveOut waveOut;

        private Thread thread;
        private bool isStopped;
        private string name;

        public event Action<SoundPlayer> OnTriggerEvent;
        public event Action OnStopped;

        public string Name
        {
            get { return name; }
        }
        public SoundPlayEventType EventType
        { 
            get { return eventType; } 
        }

        public SoundPlayer(string name, IWaveProvider waveProvider, SoundPlayEventType eventType) 
        {
            this.name = name;
            this.eventType= eventType;
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
                if (waveOut.PlaybackState == PlaybackState.Stopped)
                {
                    switch (eventType)
                    {
                        case SoundPlayEventType.Loop:
                            waveOut.Play();
                            break;
                        case SoundPlayEventType.TriggerEventWhenStopped:
                            OnTriggerEvent?.Invoke(this);
                            isStopped = true;
                            break;
                        case SoundPlayEventType.JustPlay:
                            isStopped = true;
                            break;
                    }
                }
            }

            OnStopped?.Invoke();
        }

        public void Play()
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