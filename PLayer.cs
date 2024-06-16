using SplashKitSDK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoGame
{
    public class Player : Object
    {
        private int _health;
        private int jumpHeight = 45;
        private bool _isJumped;
        private List<Bullet> _bullets;
        private bool _leftSide;
        private Obstacle _isCollided;
        public Player(Color color , float posX,float posY, float width, float height) : base(color ,posX,posY,width, height)
        {
            _health = 250;
            _isJumped = false;
            _bullets = new List<Bullet>();
            _leftSide = false;
            _isCollided = null;
        }
        public Player() : this (Color.Green,50,50,50,50) { }

        public Player(Color color ,float posX,float posY): this(color ,posX,posY,50,50) { }

        
        public bool LeftSide
        {
            get { return _leftSide; }
            set { _leftSide = value; }
        }
        public int Health { get { return _health; } }
        public bool IsJumped
        {
            get { return _isJumped; } set { _isJumped = value;}
        }
        public Obstacle IsCollided
        {
            get { return _isCollided; } set { _isCollided = value;}
        }
        public override void Draw()
        {
            SplashKit.DrawRectangle(Color.Black, PosX - 250 / 8 + Width / 2, PosY - 13, 250 / 4, 6);

            SplashKit.FillRectangle(Color.Red, PosX - 250/8 + Width/2, PosY - 13, _health/4, 6);
            foreach (Bullet bullet in _bullets)
            {
                bullet.Draw();
            }
            
            SplashKit.FillRectangle(_color, PosX, PosY, Width, Height);
            if(!_leftSide)
            {
                SplashKit.FillRectangle(Color.Blue, PosX + Width - 5 - 10, PosY + 5, 10, 10);
            }
            else
            {
                SplashKit.FillRectangle(Color.Blue, PosX + 5, PosY + 5, 10, 10);
            }
        }
        public void RunLeft()
        {
            PosX -= 5;
        }
        public void RunRight() {  PosX += 5; }

        public List<Bullet> Bullets { get { return _bullets; } }
        public void Jump()
        {

            if (this.jumpHeight >= 0)
            {
                this.jumpHeight -= 5;
                this.PosY -= this.jumpHeight;
            }
            else
            {
                this.jumpHeight = 45;
                this.IsJumped = false;
            }
        }
        public void Charge()
        {
            if (!LeftSide)
            {
            _bullets.Add(new Bullet(Color,this.LeftSide,PosX + Width + 5,PosY + Height/2));

            } else
            {
                _bullets.Add(new Bullet(Color, this.LeftSide, PosX - 33-5 , PosY + Height / 2));
            }
        }
        public void Shoot(Player p)
        {
            foreach (var bullet in this.Bullets.ToList())
            {
                if (Program.CheckCollision(p, bullet)){
                    p.TakeDamage(bullet.Damage);
                    this.Bullets.Remove(bullet);
                }
                if (bullet.BulletOut())
                {
                    this.Bullets.Remove(bullet);
                }
                else
                {
                    bullet.BulletMovement();
                }
            }
        }
        
        public void ResetHealth()
        {
            _health = 250;
        }
        public void TakeDamage(int dmg)
        {

            _health -= dmg;
            if (_health < 0)
            {
                _health = 0;
            }
        }
    }
    
}
