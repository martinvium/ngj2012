using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TugOfBaby
{
    class Stats
    {
        private static int _pointsCollected;
        private static int _itemsCollected;
        private static int _deedsDone;
        
        public Stats()         
        {
            _pointsCollected = 1;
            _itemsCollected = 0;
            _deedsDone = 0;
        }

        #region Properties
        public int DeedsDone
        {
            get { return Stats._deedsDone; }
            set { Stats._deedsDone = value; }
        }
        public int ItemsCollected
        {
            get { return Stats._itemsCollected; }
            set { Stats._itemsCollected = value; }
        }
        public int PointsCollected
        {
            get { return Stats._pointsCollected; }
            set { Stats._pointsCollected = value; }
        }
        #endregion
    }
}
