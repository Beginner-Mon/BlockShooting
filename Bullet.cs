using SplashKitSDK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoGame
{
    public class Bullet : Object
    {
        private int _speed;
        private bool _leftSide;
        private int _damage;
        public Bullet(Color color,bool side, float posX, float posY) : this(color, side, posX, posY, 33,5) { }       
        public Bullet(Color color, bool side,float posX, float posY,float width,float height) : base(color , posX, posY, width, height)
        {
            _speed = 10;
            _leftSide = side;
            _damage = 30;
        }
        public Bullet(Color color, float width, float height) : this (color , false,0,0,width, height) { }

        public Bullet(): this(Color.Black,false,0,0,33,5) { }

        public int Damage { get { return _damage; } }

        public override void Draw()
        {
            SplashKit.FillRectangle(Color, PosX, PosY, Width, Height);
        }
        
        public void BulletMovement()
        {
      
            if (!_leftSide)
            {
                this.PosX += _speed;
            } else
            {
                this.PosX -= _speed;
            }
        }
        public bool BulletOut()
        {
            if (this.PosX  > Screen.WIDTH )
            {
                return true;
            }
            if(this.PosX + Width <0 && _leftSide)
            {
                return true;
            }
            return false;
        }
        
         ~Bullet()
        {
            Console.WriteLine("destroyed bullet");
        }
    }
}
