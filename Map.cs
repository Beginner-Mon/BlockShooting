using SplashKitSDK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoGame
{
    public  class Map
    {
        List<Obstacle> _obstacles;

        public Map()
        {
            _obstacles = new List<Obstacle>();
        }
        public void AddObstacle(Obstacle obstacle)
        {
            _obstacles.Add(obstacle);
        }
        public void DisplayMap()
        {
            foreach (var obstacle in _obstacles)
            {
                obstacle.Draw();
            }
        }
        public List<Obstacle> Obstacles { get { return _obstacles; } }




    }
}
