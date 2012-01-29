using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TugOfBaby
{
    class Stats
    {
        private int _pointsCollected;

        public int PointsCollected
        {
            get { return _pointsCollected; }
            set { _pointsCollected = value; }
        }
        private int _itemsCollected;

        public int ItemsCollected
        {
            get { return _itemsCollected; }
            set { _itemsCollected = value; }
        }
        private int _deedsDone;

        public int DeedsDone
        {
            get { return _deedsDone; }
            set { _deedsDone = value; }
        }
        
        public Stats()         
        {
            _pointsCollected = 1;
            _itemsCollected = 0;
            _deedsDone = 0;
        }

        #region Properties

        #endregion
    }
}
