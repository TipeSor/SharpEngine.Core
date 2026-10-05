using System;
using JetBrains.Annotations;
using SharpEngine.Core.Input;
using SharpEngine.Core.Manager;
using SharpEngine.Core.Math;
using SharpEngine.Core.Utils.EventArgs;

namespace SharpEngine.Core.Widget;

/// <summary>
/// Class which represents Slider
/// </summary>
[UsedImplicitly]
public class Slider : ProgressBar
{
    /// <summary>
    /// Event trigger when the value is changed
    /// </summary>
    [UsedImplicitly]
    public event EventHandler<ValueEventArgs<float>>? ValueChanged;

    /// <inheritdoc />
    public override void Update(float delta)
    {
        base.Update(delta);

        if (!RealDisplayed || !InputManager.IsMouseButtonDown(MouseButton.Left)) return;
        
        var finalPosition = RealPosition - Size / 2;
        if (!InputManager.IsMouseInRectangle(new Rect(finalPosition, Size)))
            return;

        var barSize = Horizontal ? Size.X : Size.Y;
        var point = Horizontal ? InputManager.GetMousePosition().X - finalPosition.X : InputManager.GetMousePosition().Y - finalPosition.Y;
        var temp = Value;
        Value = (int)System.Math.Round(point * 100 / barSize, MidpointRounding.AwayFromZero);
        if (System.Math.Abs(temp - Value) > 0.001f)
        {
            ValueChanged?.Invoke(
                this,
                new ValueEventArgs<float> { OldValue = temp, NewValue = Value }
            );
        }
    }
}
