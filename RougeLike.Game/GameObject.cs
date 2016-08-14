namespace RougeLike.Game
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class GameObject
    {
        public GameObject()
        {
            Components = new List<GameComponent>();
        }

        public IList<GameComponent> Components { get; set; }

        public virtual void Draw()
        {
        }
    }
}