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
        // varies + or - 30 degrees right now
        //int angleVariance = 24;
        double angle = 0;
        double x = 0;
        double y = 0;
        _line = new Line2D();
        _line.Width = 2.0f;
        _line.DefaultColor = Colors.Red;
        double totalGeneratingAngleSum = 0;

        // higher value for angle variance makes it vary less
        double randomAngle = Math.PI * 2 / random.Next(-angleVariance, angleVariance);
        double firstAngle = randomAngle;

        for (int a = 0; a <= sides; a++)
            {
                totalGeneratingAngleSum = totalGeneratingAngleSum + a*(2*Math.PI / sides);
            }
        for (int i = 0; i < sides ; i++)
        {
            //angle = 2*Math.PI / random.Next(-angleVariance, angleVariance) + angle + i*(2*Math.PI / sides);
            angle = i*(2*Math.PI / sides) + randomAngle;
            
            //GD.Print($"{totalGeneratingAngleSum}");
            x = (double)(Math.Cos(angle) * radius) + x;
            y = (double)(Math.Sin(angle) * radius) + y;
            _line.AddPoint(new Vector2((float)x, (float)y) + position + new Vector2(200,200));
            //totalGeneratingAngleSum = Math.PI / 2/*totalGeneratingAngleSum - angle*/;

            randomAngle = Math.PI * 2 / random.Next(-angleVariance, angleVariance);

        }

        x = (double)(Math.Cos(firstAngle) * radius);
        y = (double)(Math.Sin(firstAngle) * radius);
        //x = Math.Cos(totalGeneratingAngleSum - angle) * radius;
        //y = Math.Sin(totalGeneratingAngleSum - angle) * radius;
        _line.AddPoint(new Vector2((float)x, (float)y) + position + new Vector2(200,200));
        AddChild(_line);

        //GD.Print($"{angle}");
        GD.Print($"{totalGeneratingAngleSum}");
    }
}
