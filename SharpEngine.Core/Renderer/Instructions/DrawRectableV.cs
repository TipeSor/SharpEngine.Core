using Raylib_cs;
using SharpEngine.Core.Math;
using System;

namespace SharpEngine.Core.Renderer.Instructions;

/// <summary>
/// Draws a rectangle v.
/// </summary>
internal record DrawRectangleV : Instruction
{
    internal override void Execute()
    {
        base.Execute();
        Raylib.DrawRectangleV(
            (Vec2)(Parameters[0]), // Pos
            (Vec2)(Parameters[1]), // Size
            (Utils.Color)(Parameters[2]) // Color
        );
    }
}
