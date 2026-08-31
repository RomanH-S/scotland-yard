using System;
using System.Collections.Generic;
using Godot;

public partial class Main : Control
{
    private Button _generateShapes;
    private SpinBox _numberOfShapes;
    private SpinBox _minNumberOfSides;
    private SpinBox _sizeOfShapes;

    private SpinBox _maxNumberOfSides;
    private Label _outputLabel;
    private Shape _shape;
    private Line2D _line;
    private ColorRect _generationSpace;
    private Marker2D _shapeOrigin;
    private List<Shape> _shapes = [];
    private static PackedScene _shapeScene;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        _generateShapes = GetNode<Button>("%GenerateShapesButton");
        _generateShapes.Pressed += OnGenerateShapesButtonPressed;

        _numberOfShapes = GetNode<SpinBox>("%NumberOfShapesSpinBox");

        _minNumberOfSides = GetNode<SpinBox>("%MinSidesSpinBox");

        _maxNumberOfSides = GetNode<SpinBox>("%MaxSidesSpinBox");

        _sizeOfShapes = GetNode<SpinBox>("%SizeOfShapesSpinBox");

        _outputLabel = GetNode<Label>("%OutputLabel");
        _outputLabel.Text = "";

        _generationSpace = GetNode<ColorRect>("%GenerationSpace");
        //_shape = GetNode<Shape>("%Shape");
        _shapeOrigin = GetNode<Marker2D>("%ShapeOrigin");
    }

    // Best Practice: Unsubscribe when the node leaves the tree to prevent memory leaks
    public override void _ExitTree()
    {
        if (_generateShapes != null)
        {
            _generateShapes.Pressed -= OnGenerateShapesButtonPressed;
        }
    }

    private void displayShape(int sides, Vector2 position, int radius)
    {
        // Check if the scene has already been loaded once
        if (_shapeScene == null)
        {
            _shapeScene = GD.Load<PackedScene>("res://shape.tscn");
        }

        Shape shapeInstance = _shapeScene.Instantiate<Shape>();
        shapeInstance.RegularPolygon(sides, position, radius);
        AddChild(shapeInstance);
    }

    private void OnGenerateShapesButtonPressed()
    {
        var numberOfShapes = _numberOfShapes.Value;
        var minNumberOfSides = _minNumberOfSides.Value;
        var maxNumberOfSides = _maxNumberOfSides.Value;
        var sizeOfShapes = (int)_sizeOfShapes.Value;

        GD.Print($"button pressed: {numberOfShapes}");
        GD.Print($"minimum number of sides: {minNumberOfSides}");
        GD.Print($"Maximum number of Sides: {maxNumberOfSides}");

        _outputLabel.Text = $"{numberOfShapes}, {minNumberOfSides}, {maxNumberOfSides}";
        var random = new Random();

        int gridContainerWidth = 500;

        for (int i = 0; i < numberOfShapes; i++)
        {
            int numberOfSides = random.Next((int)minNumberOfSides, (int)maxNumberOfSides + 1);
            Vector2 position = LayoutGrid(
                i,
                sizeOfShapes * 2,
                sizeOfShapes * 2,
                gridContainerWidth
            );
            displayShape(numberOfSides, position, sizeOfShapes);
        }
    }

    private Vector2 LayoutGrid(int i, int height, int width, int gridWidth)
    {
        int columns = gridWidth / width;
        int x = i % columns;
        int y = i / columns;

        return new Vector2(x * width, y * height);
    }
}
