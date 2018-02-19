using UnityEngine;
using System;
using System.Collections.Generic;

namespace Utils
{
    public sealed class ColorGenerator
    {
        private static readonly ColorGenerator instance = new ColorGenerator();

        private readonly List<Color> Colors;
        private int idx = 0;

        private ColorGenerator()
        {
            //Source Spectral_11 at https://jiffyclub.github.io/palettable/colorbrewer/diverging/
            Colors = new List<Color>(11)
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

        public static ColorGenerator Instance
        {
            get { return instance; }
        }

        private void UpdateCircularIdx()
        {
            idx = (idx + 1) % Colors.Count;
        }

        public Color GetNextColor()
        {
            Color color = Colors[idx];
            UpdateCircularIdx();
            return color;
        }
    }

    public class ColorSet
    {
        public readonly Color Normal;
        public readonly Color Selected;
        public readonly Color Locked;

        public ColorSet(Color normal)
        {
            Normal = normal;

            Selected = GenerateSelectedColor(normal);
            Locked = GenerateLockedColor(normal);
        }

        public ColorSet(Color normal, Color selected, Color locked)
        {
            Normal = normal;
            Selected = selected;
            Locked = locked;
        }

        private Color GenerateSelectedColor(Color baseColor)
        {
            float ValueScalingFactor = 2.0f;

            HSVColor hsvColor = new HSVColor(baseColor);
            hsvColor.Value = hsvColor.Value * ValueScalingFactor;
            return hsvColor.ToRGBColor();
        }

        private Color GenerateLockedColor(Color baseColor)
        {
            float HueScalingFactor = 0.25f;

            HSVColor hsvColor = new HSVColor(baseColor);
            hsvColor.Saturation = hsvColor.Saturation * HueScalingFactor;
            return hsvColor.ToRGBColor();
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

        public Color ToRGBColor()
        {
            return Color.HSVToRGB(hue, saturation, value);
        }
    }

}