using UnityEngine;
using System;
using System.Collections.Generic;

namespace Utils
{
	public sealed class ColorGenerator : RTEditor.SingletonBase<ColorGenerator>
	{
		private readonly List<Color> colors;
		private int idx;

		public ColorGenerator()
		{
			idx = 0;

			colors = new List<Color>(8)
				{
					new Color(247f / 255f, 146f / 255f, 086 / 255f),
					new Color(029f / 255f, 078f / 255f, 137 / 255f),
					new Color(117f / 255f, 166f / 255f, 157 / 255f),
					new Color(251f / 255f, 210f / 255f, 162 / 255f),
					new Color(8f / 255f, 159f / 255f, 179 / 255f),
					new Color(125f / 255f, 206f / 255f, 181 / 255f),
					new Color(38f / 255f, 056f / 255f, 075 / 255f),
					new Color(177f / 255f, 143f / 255f, 122 / 255f)
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

		public Color GetNextColor()
		{
			Color color = colors[idx];
			UpdateCircularIdx();
			return color;
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