// Comments:
// [Behavior] Awake()
//     ↪ Triggered as soon as the object is created
// [Special] __sBotics__Activate(), __sBotics__Deactivate()
//     ↪ Triggered in the beggining and end of games

using UnityEngine;
using System;
using sBotics.CodeUtils;

namespace sBotics
{
    namespace Robot
    {
        public class Buzzer : __sBotics__RobotComponent
        {   
            AudioClip Square, Sawtooth, Noise, Mute;

            // AudioSource cache
            AudioSource _audioSource = null;
            public AudioSource __sBotics__AudioSource
            {
                get
                {
                    if(!_audioSource)
                        _audioSource = GetComponent<AudioSource>();
                    return _audioSource;
                }
                set
                {
                    if(!_audioSource)
                        _audioSource = GetComponent<AudioSource>();

                    _audioSource = value;
                }
            }

            public bool Playing
            {
                get => (__sBotics__AudioSource.clip != null);
            }

            public WaveFormats WaveLoaded
            {
                get
                {
                    if(__sBotics__AudioSource.clip == Square)
                        return WaveFormats.Square;
                    else if(__sBotics__AudioSource.clip == Sawtooth)
                        return WaveFormats.Sawtooth;
                    else if(__sBotics__AudioSource.clip == Noise)
                        return WaveFormats.Noise;
                    else return WaveFormats.Mute;
                }
            }

            double ToSoundPitch(double hz) => (hz * 264 / 5933);
            
            public void PlaySound(string _note, WaveFormats wave = WaveFormats.Square)
            {
                Notes note;
                Solfege solfegeNote;
               
                if(Enum.TryParse<Notes>(_note, out note))
                {
                    PlaySound(note, wave);
                    return;
                }

                if(Enum.TryParse<Solfege>(_note, out solfegeNote))
                {
                    PlaySound(note, wave);
                    return;
                }
            }

            public void PlaySound(Solfege note, WaveFormats wave = WaveFormats.Square) =>
                PlaySound((Notes) note, wave);

            public void PlaySound(Notes note, WaveFormats wave = WaveFormats.Square) =>
                PlaySound(__sBotics__SpecialCodeUtils.NoteHz[note], wave);

            public void PlaySound(double hertz, WaveFormats wave = WaveFormats.Square)
            {
                __sBotics__AudioSource.enabled = true;
                __sBotics__AudioSource.pitch = (float) ToSoundPitch(hertz);
                switch(wave)
                {
                    case WaveFormats.Square:
                        __sBotics__AudioSource.clip = Square;
                        break;
                    case WaveFormats.Sawtooth:
                        __sBotics__AudioSource.clip = Sawtooth;
                        break;
                    case WaveFormats.Noise:
                        __sBotics__AudioSource.clip = Noise;
                        break;
                    default://case WaveFormats.Mute:
                        __sBotics__AudioSource.clip = Mute;
                        break;
                }

                __sBotics__AudioSource.Play();
            }

            public void StopSound()
            {
                __sBotics__AudioSource.Stop();
                __sBotics__AudioSource.clip = null;
                __sBotics__AudioSource.enabled = false;
            }

            public override void __sBotics__Activate() =>  StopSound();
            public override void __sBotics__Deactivate() =>  StopSound();
        }
    }
}