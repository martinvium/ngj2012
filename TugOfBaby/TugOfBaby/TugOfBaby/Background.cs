using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Storage;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Audio;
using System;

namespace TugOfBaby
{
    class Background
    {
        SoundEffectInstance heavenlySoundInstance, hellishSoundInstance;
        SoundEffect heavenlySound, hellishSound;

        Texture2D heavenBG, hellBG, nothingBG;
        int transHeaven = 0, transHell = 0;
        Vector2 position = Vector2.Zero;

        public void Update(float _status)
        {
            if (hellishSoundInstance.State == SoundState.Stopped)
                hellishSoundInstance.Play();
            if (heavenlySoundInstance.State == SoundState.Stopped)
                heavenlySoundInstance.Play();

            float hellChannel = 0;
            float heavenChannel = 0;

            if(_status <= 0){
                hellChannel = (_status/100);
                heavenChannel = 0;
                hellChannel *= -1;
                Console.WriteLine("hellChannel = " + hellChannel);
            } else if(_status > 0){
                heavenChannel = (_status/100);
                hellChannel = 1-heavenChannel;
                hellChannel = 0;
            }

            heavenlySoundInstance.Volume = heavenChannel;
            hellishSoundInstance.Volume = hellChannel;
            
//            if(hellChannel >= -0.5 && hellChannel >= -0.4)
//                Console.WriteLine("HEAVEN IS: " + heavenChannel + " HELL IS: " + hellChannel);

            _status *= 2.55f;

            if (_status < 0)
            {
                transHeaven = 0;
                transHell = (int)_status * -1;
            }
            else
            {
                transHell = 0;
                transHeaven = (int)_status;
            }
        }

        public void LoadContent(ContentManager _contentManager)
        {
            heavenBG = _contentManager.Load<Texture2D>("heavenBG");
            hellBG = _contentManager.Load<Texture2D>("hellBG");
            nothingBG = _contentManager.Load<Texture2D>("nothingBG");

            //LOAD SOUND
            hellishSound = _contentManager.Load<SoundEffect>("cowbell");
            heavenlySound = _contentManager.Load<SoundEffect>("synth_beep_2");
            hellishSoundInstance = hellishSound.CreateInstance();
            heavenlySoundInstance = heavenlySound.CreateInstance();
            heavenlySoundInstance.Pan = -1.0f;
            hellishSoundInstance.Pan = 1.0f;

            heavenlySoundInstance.IsLooped = true;
            hellishSoundInstance.IsLooped = true;
        }

        public void Draw(SpriteBatch _spriteBatch)
        {
            _spriteBatch.Draw(nothingBG, position, new Color(255, 255, 255, 255));
            _spriteBatch.Draw(heavenBG, position, new Color(transHeaven, transHeaven, transHeaven, transHeaven));
            _spriteBatch.Draw(hellBG, position, new Color(transHell, transHell, transHell, transHell));   
        }
    }
}
