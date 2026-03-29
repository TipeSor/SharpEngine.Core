using Raylib_cs;
using SharpEngine.Core.Math;
using System;

namespace SharpEngine.Core.Renderer.Instructions;

/// <summary>
/// Draws line.
/// </summary>
internal record DrawLineEx : Instruction
{
    internal override void Execute()
    {
        base.Execute();
        Raylib.DrawLineEx(
            (Vec2)Parameters[0],
            (Vec2)Parameters[1],
            Convert.ToSingle(Parameters[2]),
            (Utils.Color)Parameters[3]
        );
    }
}
