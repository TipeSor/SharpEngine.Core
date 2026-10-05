using System;
using System.Collections.Generic;
using JetBrains.Annotations;
using Raylib_cs;
using SharpEngine.Core.Manager;
using SharpEngine.Core.Math;
using SharpEngine.Core.Utils.EventArgs;

namespace SharpEngine.Core.Widget;

/// <summary>
/// Class, which display a selector
/// </summary>
[UsedImplicitly]
public class Selector : Widget
{
	/// <summary>
	/// Values of Selector
	/// </summary>
	[UsedImplicitly]
	public List<string> Values { get; } = [];

	/// <summary>
	/// Selected Index of Selector
	/// </summary>
	[UsedImplicitly]
	public int SelectedIndex { get; set; }

	/// <summary>
	/// Left Button of Selector
	/// </summary>
	[UsedImplicitly]
	public Button LeftButton { get; }

	/// <summary>
	/// Right Button of Selector
	/// </summary>
	[UsedImplicitly]
	public Button RightButton { get; }

	/// <summary>
	/// Label of Selector
	/// </summary>
	[UsedImplicitly]
	public Label LabelValue { get; }

	/// <summary>
	/// Font of Selector
	/// </summary>
	[UsedImplicitly]
	public string Font { get; set; } = FontManager.Default;

	/// <summary>
	/// Font Size of Selector
	/// </summary>
	[UsedImplicitly]
	public int? FontSize
	{
		get; set
		{
			field = value;
			LabelValue.FontSize = field;
			LeftButton.FontSize = field;
			RightButton.FontSize = field;
		}
	}

	/// <inheritdoc/>
	public override int ZLayer
	{
		get; set
		{
			field = value;
			LabelValue.ZLayer = field;
			LeftButton.ZLayer = field;
			RightButton.ZLayer = field;
		}
	}

	/// <summary>
	/// Event trigger when the value is changed
	/// </summary>
	[UsedImplicitly]
	public event EventHandler<ValueEventArgs<string>>? ValueChanged;

	/// <summary>
	/// Selected Value
	/// </summary>
	[UsedImplicitly]
	public string Selected => Values[SelectedIndex];

	/// <summary>
	/// Create Selector
	/// </summary>
	public Selector()
	{
		LabelValue = new Label() { Position = Vec2.Zero };
		LeftButton = new Button()
		{
			Position = Vec2.Zero,
			Text = "<",
			Size = new Vec2(30),
		};
		RightButton = new Button()
		{
			Position = Vec2.Zero,
			Text = ">",
			Size = new Vec2(30),
		};

		AddChild(LabelValue);
		AddChild(LeftButton).Clicked += LeftButtonClick;
		AddChild(RightButton).Clicked += RightButtonClick;
	}

	private void LeftButtonClick(object? sender, EventArgs e)
	{
		var old = Selected;
		if (SelectedIndex == 0)
			SelectedIndex = Values.Count - 1;
		else
			SelectedIndex--;
		LabelValue.Text = Selected;

		ValueChanged?.Invoke(
			this,
			new ValueEventArgs<string> { OldValue = old, NewValue = Selected }
		);
	}

	private void RightButtonClick(object? sender, EventArgs e)
	{
		var old = Selected;
		if (SelectedIndex == Values.Count - 1)
			SelectedIndex = 0;
		else
			SelectedIndex++;
		LabelValue.Text = Selected;

		ValueChanged?.Invoke(
			this,
			new ValueEventArgs<string> { OldValue = old, NewValue = Selected }
		);
	}

	/// <inheritdoc />
	public override void Load()
	{
		base.Load();

		var font = Scene?.Window?.FontManager.GetFont(Font);

		if (font != null)
		{
			var fontSize = FontSize ?? font.Value.BaseSize;
			var maxSizeX = 0f;
			foreach (var value in Values)
			{
				var size = Raylib.MeasureTextEx(font.Value, value, fontSize, 2);
				if (maxSizeX < size.X)
					maxSizeX = size.X;
			}

			LeftButton.Position = new Vec2(-maxSizeX / 2 - 30, 0);
			RightButton.Position = new Vec2(maxSizeX / 2 + 30, 0);
		}
	}
}
