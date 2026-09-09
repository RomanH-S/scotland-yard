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
        _line.DefaultColor = Colors.Green;

        for (int i = 0; i <= sides; i++)
        {
            float angle = (float)(i * 2 * Math.PI / sides);
            float x = (float)(Math.Cos(angle) * radius);
            float y = (float)(Math.Sin(angle) * radius);
            _line.AddPoint(new Vector2(x, y) + position);
        }
        AddChild(_line);
    }

    public void IrregularPolygon(int sides, Vector2 position, int radius, int angleVariance)
    {
        var random = new Random();
        double angle;
        double x = 0;
        double y = 0;
        _line = new Line2D { Width = 2.0f, DefaultColor = Colors.Red };

        // higher value for angle variance makes it vary less
        double randomAngle = Math.PI * 2 / random.Next(-angleVariance, angleVariance);
        double firstAngle = randomAngle;

        for (int i = 0; i < sides; i++)
        {
            angle = i * (2 * Math.PI / sides) + randomAngle;
            AddPoint(angle, radius, position + new Vector2((float)x, (float)y));
            randomAngle = Math.PI * 2 / random.Next(-angleVariance, angleVariance);
        }

        AddPoint(firstAngle, radius, position);
        AddChild(_line);
    }

    private void AddPoint(double angle, int radius, Vector2 position)
    {
        double x = (double)(Math.Cos(angle) * radius);
        double y = (double)(Math.Sin(angle) * radius);
        _line.AddPoint(new Vector2((float)x, (float)y) + position + new Vector2(200, 200));
    }
}
