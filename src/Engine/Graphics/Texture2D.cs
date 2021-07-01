namespace GraphicsRenderer
{

    // Structure that stores the information of a sprite texture
    public struct Texture2D
    {
        private int id;
        private int width, height;

        public int Id
        {
        get { return id; }
        }
        public int Width
        {
        get { return width; }
        }
        public int Height
        {
        get { return height; }
        }

        public Texture2D(int id, int width, int height)
        {
        this.id = id;
        this.width = width;
        this.height = height;
        }
    }
}