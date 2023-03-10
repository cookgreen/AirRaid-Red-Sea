using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirRaidRedSea
{
    public class ScoreManager
    {
        private int score;

        public event Action ScoreChanged;
        public int Score
        {
            get { return score; }
        }

        private static ScoreManager instance;
        public static ScoreManager Instance
        {
            get
            {
                if(instance == null)
                    instance = new ScoreManager();
                return instance;
            }
        }

        public void ChangeScore(int newScore)
        {
            score = newScore;
            ScoreChanged?.Invoke();
        }
    }
}
