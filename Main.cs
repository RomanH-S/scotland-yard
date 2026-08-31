using System;
using System.Collections.Generic;
using Godot;

public partial class Main : Control
{
    private Button _generateShapes;
    private SpinBox _numberOfShapes;
    private SpinBox _minNumberOfSides;
    private SpinBox _sizeOfShapes;
    private OptionButton _polygonTypeButton;

    private SpinBox _maxNumberOfSides;
    private Shape _shape;
    private Line2D _line;
    private ColorRect _generationSpace;
    private Marker2D _shapeOrigin;
    private List<Shape> _shapes = [];
    private static PackedScene _shapeScene;

    public enum PolygonType
    {
        Regular,
        Irregular,
    }

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        _generateShapes = GetNode<Button>("%GenerateShapesButton");
        _generateShapes.Pressed += OnGenerateShapesButtonPressed;

        _numberOfShapes = GetNode<SpinBox>("%NumberOfShapesSpinBox");

        _minNumberOfSides = GetNode<SpinBox>("%MinSidesSpinBox");

        _maxNumberOfSides = GetNode<SpinBox>("%MaxSidesSpinBox");

        _sizeOfShapes = GetNode<SpinBox>("%SizeOfShapesSpinBox");

        _polygonTypeButton = GetNode<OptionButton>("%PolygonType");
        _polygonTypeButton.Clear();

        // Loop through all values of the enum
        foreach (PolygonType polygonType in Enum.GetValues(typeof(PolygonType)))
        {
            string name = Enum.GetName(typeof(PolygonType), polygonType);
            int id = (int)polygonType;

            // Add item with the name as text and the enum int value as ID
            _polygonTypeButton.AddItem(name, id);
        }

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

    private void displayShape(int sides, Vector2 position, int radius, PolygonType polygonType)
    {
        // Check if the scene has already been loaded once
        if (_shapeScene == null)
        {
            _shapeScene = GD.Load<PackedScene>("res://shape.tscn");
        }

        Shape shapeInstance = _shapeScene.Instantiate<Shape>();
        if (polygonType == PolygonType.Regular)
        {
            shapeInstance.RegularPolygon(sides, position, radius);
        }
        else if (polygonType == PolygonType.Irregular)
        {
            shapeInstance.IrregularPolygon(sides, position, radius);
        }
        else
        {
            GD.PushError("Unrecognized polygon type in displayShape");
        }

        AddChild(shapeInstance);
    }

    private PolygonType GetSelectedPolygonType()
    {
        int selectedId = _polygonTypeButton.Selected;
        return (PolygonType)selectedId;
    }

    private void OnGenerateShapesButtonPressed()
    {
        var numberOfShapes = _numberOfShapes.Value;
        var minNumberOfSides = _minNumberOfSides.Value;
        var maxNumberOfSides = _maxNumberOfSides.Value;
        var sizeOfShapes = (int)_sizeOfShapes.Value;
        var polygonType = GetSelectedPolygonType();

        GD.Print($"button pressed: {numberOfShapes}");
        GD.Print($"minimum number of sides: {minNumberOfSides}");
        GD.Print($"Maximum number of Sides: {maxNumberOfSides}");

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
            displayShape(numberOfSides, position, sizeOfShapes, polygonType);
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
