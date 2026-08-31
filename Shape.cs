using System;
using Godot;

public partial class Shape : Node2D
{
    private Line2D _line;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        _line = new Line2D();
    }

    public override void _ExitTree()
    {
        // remove _line
    }

    public void RegularPolygon(int sides, Vector2 position, int radius)
    {
        _line = new Line2D();
        _line.Width = 2.0f;
        _line.DefaultColor = Colors.Red;

        for (int i = 0; i <= sides; i++)
        {
            float angle = (float)(i * 2 * Math.PI / sides);
            float x = (float)(Math.Cos(angle) * radius);
            float y = (float)(Math.Sin(angle) * radius);
            _line.AddPoint(new Vector2(x, y) + position);
        }
        AddChild(_line);
    }
}
