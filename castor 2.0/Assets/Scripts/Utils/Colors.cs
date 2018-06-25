using UnityEngine;
using System;
using System.Collections.Generic;

namespace Utils
{
	public sealed class ColorGenerator : RTEditor.SingletonBase<ColorGenerator>
	{
		private readonly List<NamedColor> colors;
		private int idx;

		public ColorGenerator()
		{
			idx = 0;

			colors = new List<NamedColor>(11){
				new NamedColor(new Color(166/255f, 206/255f, 227/255f), "light blue"),
				new NamedColor(new Color(031/255f, 120/255f, 180/255f), "dark blue"),
				new NamedColor(new Color(178/255f, 223/255f, 138/255f), "light green"),
				new NamedColor(new Color(051/255f, 160/255f, 044/255f), "dark green"),
				new NamedColor(new Color(251/255f, 154/255f, 153/255f), "pink"),
				new NamedColor(new Color(227/255f, 026/255f, 028/255f), "red"),
				new NamedColor(new Color(253/255f, 191/255f, 111/255f), "light orange"),
				new NamedColor(new Color(255/255f, 127/255f, 000/255f), "dark orange"),
				new NamedColor(new Color(202/255f, 178/255f, 214/255f), "light purple"),
				new NamedColor(new Color(106/255f, 061/255f, 154/255f), "dark purple"),
				new NamedColor(new Color(255/255f, 255/255f, 000/255f), "yellow")
			};
		}

		public void Reset()
		{
			idx = 0;
		}

		private void UpdateCircularIdx()
		{
			idx = (idx + 1) % colors.Count;
		}

		public NamedColor GetNextColor()
		{
			NamedColor color = colors[idx];
			UpdateCircularIdx();
			return color;
		}
	}

	[System.Serializable]
	public class NamedColor
	{
		public readonly Color color;
		public readonly string name;

		public NamedColor(Color color, string name)
		{
			this.color = color;
			this.name = name;
		}
	}

	public class MaterialSet
	{
		public readonly Material Normal;
		public readonly Material Selected;
		public readonly Material Locked;
		public readonly Material Registration;

		public MaterialSet(Material normal)
		{
			Normal = normal;

			Selected = NewOpaqueMaterial(normal, SelectedColor(normal.color));
			Locked = NewOpaqueMaterial(normal, LockedColor(normal.color));

			Registration = NewTransparentMaterial(normal, ICPColor(normal.color));
		}

		private Color SelectedColor(Color baseColor)
		{
			float ValueScalingFactor = 2.0f;

			HSVColor hsvColor = new HSVColor(baseColor);
			hsvColor.Value = hsvColor.Value * ValueScalingFactor;
			return hsvColor.ToColor();
		}

		private Color LockedColor(Color baseColor)
		{
			float HueScalingFactor = 0.25f;

			HSVColor hsvColor = new HSVColor(baseColor);
			hsvColor.Saturation = hsvColor.Saturation * HueScalingFactor;
			return hsvColor.ToColor();
		}

		private Color ICPColor(Color baseColor)
		{
			float ICPAlpha = 0.4f;

			return new Color(baseColor.r, baseColor.g, baseColor.b, ICPAlpha);
		}

		private Material NewOpaqueMaterial(Material baseMaterial, Color color)
		{
			Material newMaterial = new Material(baseMaterial);
			newMaterial.color = color;

			//Set properties as specified by http://answers.unity.com/answers/1265884/view.html
			newMaterial.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.One);
			newMaterial.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.Zero);
			newMaterial.SetInt("_ZWrite", 1);
			newMaterial.DisableKeyword("_ALPHATEST_ON");
			newMaterial.DisableKeyword("_ALPHABLEND_ON");
			newMaterial.DisableKeyword("_ALPHAPREMULTIPLY_ON");
			newMaterial.renderQueue = -1;
			return newMaterial;
		}

		private Material NewTransparentMaterial(Material baseMaterial, Color color)
		{
			Material newMaterial = new Material(baseMaterial);
			newMaterial.color = color;

			//Set properties as specified by http://answers.unity.com/answers/1265884/view.html
			newMaterial.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.One);
			newMaterial.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.OneMinusSrcAlpha);
			newMaterial.SetInt("_ZWrite", 0);
			newMaterial.DisableKeyword("_ALPHATEST_ON");
			newMaterial.DisableKeyword("_ALPHABLEND_ON");
			newMaterial.EnableKeyword("_ALPHAPREMULTIPLY_ON");
			newMaterial.renderQueue = 3000;

			return newMaterial;
		}
	}

	public class HSVColor
	{
		private float hue;
		public float Hue
		{
			get { return hue; }
			set { hue = Mathf.Clamp01(value); }
		}

		private float saturation;
		public float Saturation
		{
			get { return saturation; }

			set { saturation = Mathf.Clamp01(value); }
		}

		private float value;
		public float Value
		{
			get { return value; }
			set { this.value = Mathf.Clamp01(value); }
		}

		public HSVColor(Color RGBColor)
		{
			Color.RGBToHSV(RGBColor, out hue, out saturation, out value);
		}

		public HSVColor(float hue, float saturation, float value)
		{
			this.hue = hue;
			this.saturation = saturation;
			this.value = value;
		}

		public Color ToColor()
		{
			return Color.HSVToRGB(hue, saturation, value);
		}
	}
}