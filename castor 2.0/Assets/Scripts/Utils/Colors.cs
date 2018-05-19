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

			//Source Spectral_11 at https://jiffyclub.github.io/palettable/colorbrewer/diverging/
			colors = new List<Color>(11)
				{
					new Color(0.55f, 0.00f, 0.20f), //deep red (140, 0, 51)
                    new Color(0.29f, 0.22f, 0.57f), //purple
                    new Color(0.93f, 0.33f, 0.20f), //deep orange
                    new Color(1.00f, 1.00f, 0.70f), //yellow
                    new Color(0.61f, 0.85f, 0.58f), //green
                    new Color(0.16f, 0.45f, 0.69f), //blue
                    new Color(0.78f, 0.16f, 0.25f), //red
                    new Color(0.98f, 0.62f, 0.31f), //orange
                    new Color(0.88f, 0.96f, 0.53f), //pale green
                    new Color(0.99f, 0.85f, 0.47f), //light orange
                    new Color(0.34f, 0.72f, 0.58f)  //blueish-green
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