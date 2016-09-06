namespace RougeLike.Game.Graphics
{
    public interface IDrawable
    {
        void Draw();

        void BindVao();

        int Vao { get; }
    }
}