using System;
using System.Collections.Generic;
using UnityEngine;

namespace Registration
{
    public class RandomSubSampling : IPointSampler
    {
        Configuration config;

        public RandomSubSampling(Configuration configuration)
        {
            this.config = configuration;
        }

        public List<Point> Sample(SamplingInformation samplingInfo)
        {
            List<Point> points = new AllPointsSampler(new AllPointsSampler.Configuration(this.config)).Sample(samplingInfo);

            List<Point> sample = new List<Point>(ApproximateSampleSize(points.Count));

            //TODO Implement


            return sample;
        }

        private int ApproximateSampleSize(int numElements)
        {
            return (int)Math.Round(numElements * config.Probability);
        }

        public class Configuration : Registration.SamplingConfiguration
        {
            public readonly AllPointsSampler.Configuration.NormalProcessing normalProcessing;


            /// <summary>
            /// The percentage of the points that should be kept, percentage in [0, 100].
            /// </summary>
            private float percentage;
            public float Percentage
            {
                get { return percentage; }
                set
                {
                    ValidatePercentage(value);
                    this.percentage = value;
                    this.probability = 1.0f / percentage;
                }
            }

            private float probability;
            /// <summary>
            /// 1.0 / percentage, probability in [0, 1].
            /// </summary>
            public float Probability
            {
                get { return probability; }
            }

            public Configuration(Transform referenceTransform, AllPointsSampler.Configuration.NormalProcessing normalProcessing, float percentage)
                : base(referenceTransform)
            {
                this.normalProcessing = normalProcessing;
                this.Percentage = percentage;
            }

            private void ValidatePercentage(float value)
            {
                if (value < 0 || value > 100)
                    throw new ArgumentException("Percentages outside of the range [0, 100] are not accepted.");
            }
        }
    }
}