using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Media;
using System.Runtime.InteropServices;

namespace SharpMoku
{
    public abstract class Player
    {
        public  string SoundFileName { get; set; }
        public abstract void Play();

    }
    public class DefaultPlay : Player
    {
        SoundPlayer Player;
        public override void Play()
        {
            if(Player ==null)
            {
                Player = new SoundPlayer();
                Player.SoundLocation = this.SoundFileName;
                Player.LoadAsync();


            }


            Player.PlaySync();
        }
    }
    public class WNMPlayer : Player
    {

        [DllImport("winmm.dll", SetLastError = true)]
        static extern bool PlaySound(string pszSound, UIntPtr hmod, uint fdwSound);

        [Flags]
        public enum SoundFlags
        {
            /// <summary>play synchronously (default)</summary>
            SND_SYNC = 0X0000,
            /// <summary>play asynchronously</summary>
            SND_ASYNC = 0X0001,
            /// <summary>silence (!default) if sound not found</summary>
            SND_NODEFAULT = 0X0002,
            /// <summary>pszSound points to a memory file</summary>
            SND_MEMORY = 0X0004,
            /// <summary>loop the sound until next sndPlaySound</summary>
            SND_LOOP = 0X0008,
            /// <summary>don’t stop any currently playing sound</summary>
            SND_NOSTOP = 0X0010,
            /// <summary>Stop Playing Wave</summary>
            SND_PURGE = 0X40,
            /// <summary>don’t wait if the driver is busy</summary>
            SND_NOWAIT = 0X00002000,
            /// <summary>name is a registry alias</summary>
            SND_ALIAS = 0X00010000,
            /// <summary>alias is a predefined id</summary>
            SND_ALIAS_ID = 0X00110000,
            /// <summary>name is file name</summary>
            SND_FILENAME = 0X00020000,
            /// <summary>name is resource name or atom</summary>
            SND_RESOURCE = 0X00040004
    }

        /*
        public static void Play(string strFileName)
        {
            PlaySound(strFileName, UIntPtr.Zero,
               (uint)(SoundFlags.SND_FILENAME | SoundFlags.SND_ASYNC));
        }
        */
        public override void Play()
        {
            PlaySound(SoundFileName , UIntPtr.Zero,
               (uint)(SoundFlags.SND_FILENAME | SoundFlags.SND_ASYNC));
        }
    }
    public class WMPPlayer : Player
    {

        WMPLib.WindowsMediaPlayer Player;
        public  void PlaySoundUsingWMD()
        {
            Player = new WMPLib.WindowsMediaPlayer();
            Player.PlayStateChange +=
                new WMPLib._WMPOCXEvents_PlayStateChangeEventHandler(Player_PlayStateChange);
            Player.MediaError +=
                new WMPLib._WMPOCXEvents_MediaErrorEventHandler(Player_MediaError);
            Player.URL = SoundFileName;
      
            Player.controls.play();
            
        }

        private void Player_PlayStateChange(int NewState)
        {
            if ((WMPLib.WMPPlayState)NewState == WMPLib.WMPPlayState.wmppsStopped)
            {
                Player.close();
            }
        }

        private void Player_MediaError(object pMediaObject)
        {
            throw new Exception("There is a problem in Media");
            /*
             * TODO:Add code to get more information from pMediaObject
             */
        }
        public override void Play()
        {
            // throw new NotImplementedException();
            PlaySoundUsingWMD();
        }


    }
    public class ShareSoundEffect
    {
        public enum SoundEffect
        {
            PutStone,
            Won,
            Lost
        }
        private static Dictionary<SoundEffect, Player> soundPlayerDic = new Dictionary<SoundEffect, Player>();
        private static Dictionary<SoundEffect, String> _soundFileName = null;
        private static Dictionary<SoundEffect, String> soundFileName
        {
            get
            {
                if(_soundFileName ==null)
                {
                    _soundFileName = new Dictionary<SoundEffect, string>();
                    _soundFileName.Add(SoundEffect.PutStone, @"D:\Temp\2022_04_17\PutStone.wav");
                    _soundFileName.Add(SoundEffect.Won , @"D:\Temp\2022_04_17\Won.wav");
                    _soundFileName.Add(SoundEffect.Lost , @"D:\Temp\2022_04_17\Lost.wav");
                }
                return _soundFileName;
            }

        }
        public static void PlaySound(SoundEffect soundeffect,Type type)
        {
            if(soundPlayerDic == null)
            {
                soundPlayerDic = new Dictionary<SoundEffect, Player>();
            }
            //System.Media.SoundPlayer player = null;
            Player player = new DefaultPlay();
            if (type==typeof(WMPPlayer))
            {
                player = new WMPPlayer();
            }

            if (!soundPlayerDic.ContainsKey (soundeffect))
            {
                /*
                player = new System.Media.SoundPlayer(soundFileName[soundeffect]);
                player.LoadAsync();
                */
                player.SoundFileName = soundFileName[soundeffect];
                soundPlayerDic.Add(soundeffect, player);

            } else
            {
                player = soundPlayerDic[soundeffect];
            }

            // player.Play();
            // player.PlaySync();
            // System.Media.SoundPlayer player = new System.Media.SoundPlayer(file);
            player.Play();

        }
       // MediaPlayer.MediaPlayer r
    
       
    }
}
