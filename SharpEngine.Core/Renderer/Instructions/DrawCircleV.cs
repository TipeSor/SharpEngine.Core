using Raylib_cs;
using SharpEngine.Core.Math;
using System;

namespace SharpEngine.Core.Renderer.Instructions;

/// <summary>
/// Draws circle.
/// </summary>
internal record DrawCircleV : Instruction
{
    internal override void Execute()
    {
        base.Execute();
        Raylib.DrawCircleV(
            (Vec2)Parameters[0],
            Convert.ToSingle(Parameters[1]),
            (Utils.Color)Parameters[2]
        );
    }
}
