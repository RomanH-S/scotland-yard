using System;
using System.Collections.Generic;
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

    // Generates a random irregular polygon
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

    public double ExteriorAngle(Vector2 originPoint)
    {
        int i = Array.IndexOf(_line.Points, originPoint);
        Vector2 a;
        Vector2 b = originPoint;
        Vector2 c;

        if (i - 1 < 0)
        {
            a = _line.Points[_line.Points.Length - 1];
        }
        else
        {
            a = _line.Points[i - 1];
        }

        if (i + 1 > _line.Points.Length - 1)
        {
            c = _line.Points[0];
        }
        else
        {
            c = _line.Points[i + 1];
        }

        double angle = 0.0;
        return angle;
    }

    public void AdjacentIrregularPolygon(
        Shape shape,
        Vector2 point,
        int sides,
        int radius,
        int angleVariance
    )
    {
        double angle = shape.ExteriorAngle(point);
    }

    private void AddPoint(double angle, int radius, Vector2 position)
    {
        double x = (double)(Math.Cos(angle) * radius);
        double y = (double)(Math.Sin(angle) * radius);
        _line.AddPoint(new Vector2((float)x, (float)y) + position + new Vector2(200, 200));
    }
}
