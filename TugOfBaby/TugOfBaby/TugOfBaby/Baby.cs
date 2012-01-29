using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TugOfBaby
{
    class Baby
    {
        private Sprite _torso;

        public Sprite Torso
        {
            get { return _torso; }
            set { _torso = value; }
        }
    }
}
