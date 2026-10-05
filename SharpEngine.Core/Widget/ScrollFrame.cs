using JetBrains.Annotations;
using SharpEngine.Core.Manager;
using SharpEngine.Core.Math;
using SharpEngine.Core.Renderer;
using SharpEngine.Core.Utils;

namespace SharpEngine.Core.Widget;

/// <summary>
/// Class which represents Scroll Frame
/// </summary>
[UsedImplicitly]
public class ScrollFrame : Widget
{
    /// <summary>
    /// Color of Scroll Frame Border
    /// </summary>
    [UsedImplicitly]
    public Color BorderColor { get; set; } = Color.Black;

    /// <summary>
    /// Size of Scroll Frame
    /// </summary>
    [UsedImplicitly]
    public required Vec2 Size { get; set; }

    /// <summary>
    /// Size of Scroll Frame Border
    /// </summary>
    [UsedImplicitly]
    public int BorderSize { get; set; } = 3;

    /// <summary>
    /// Color of Scroll Frame Background
    /// </summary>
    [UsedImplicitly]
    public Color? BackgroundColor { get; set; } = null;

    /// <summary>
    /// Scroll Factor of Scroll Frame
    /// </summary>
    [UsedImplicitly]
    public int ScrollFactor { get; set; } = 5;

    /// <inheritdoc />
    public override void Update(float delta)
    {
        base.Update(delta);
        
        if(!RealDisplayed)
            return;

        var move = InputManager.GetMouseWheelMove();
        if (move == 0) return;
        
        foreach (var child in Children)
            child.Position = new Vec2(child.Position.X, child.Position.Y + move * ScrollFactor);
    }

    /// <inheritdoc />
    public override void Draw()
    {
        if (!Displayed || Scene == null)
            return;

        if (BackgroundColor != null)
            SERender.DrawRectangle(
                new Rect(RealPosition.X, RealPosition.Y, Size.X, Size.Y),
                Size / 2,
                0,
                BackgroundColor.Value,
                InstructionSource.Ui,
                ZLayer
            );
        SERender.DrawRectangleLines(
            new Rect(RealPosition.X - Size.X / 2, RealPosition.Y - Size.Y / 2, Size.X, Size.Y),
            BorderSize,
            BorderColor,
            InstructionSource.Ui,
            ZLayer + 0.00001f
        );

        SERender.ScissorMode(
            RealPosition.X - (Size.X / 2),
            RealPosition.Y - Size.Y / 2,
            Size.X,
            Size.Y,
            InstructionSource.Ui,
            ZLayer + 0.00002f,
            base.Draw
        );
    }
}
