using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Storage;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Audio;

namespace TugOfBaby
{
    class Background
    {
        SoundEffectInstance heavenlySoundInstance, hellishSoundInstance;
        SoundEffect heavenlySound, hellishSound;

        Texture2D heavenBG, hellBG, nothingBG;
        int transHeaven = 0, transHell = 0;
        Vector2 position = Vector2.Zero;

        void changeTransparencies(int _input)
        {
            
        }

        public void Update(float _status)
        {
            if (_status < 0)
            {

            }

            hellishSoundInstance.Play();
            
            //heavenlySoundInstance.Volume = _status / 100.0f;

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
            hellishSound = _contentManager.Load<SoundEffect>("ak-47");
            //heavenlySound = _contentManager.Load<SoundEffect>("sound...");
            hellishSoundInstance = hellishSound.CreateInstance();
            //heavenlySoundInstance = hellishSound.CreateInstance();
        }

        public void Draw(SpriteBatch _spriteBatch)
        {
            _spriteBatch.Draw(nothingBG, position, new Color(255, 255, 255, 255));
            _spriteBatch.Draw(heavenBG, position, new Color(transHeaven, transHeaven, transHeaven, transHeaven));
            _spriteBatch.Draw(hellBG, position, new Color(transHell, transHell, transHell, transHell));   
        }
    }
}
