using System;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

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
            List<Point> points = new AllPointsSampler(this.config).Sample(samplingInfo);
            return Sample(points);
        }

        public List<Point> Sample(List<Point> points)
        {
            Point[] shuffeledPoints = points.ToArray();
            Double[] order = BuildOrderArray(points.Count);

            Array.Sort(order, shuffeledPoints);

            int sampleSize = ComputeSampleSize(points.Count);

            return new List<Point>(shuffeledPoints).Take(sampleSize).ToList();
        }

        public SerializablePointSampler ToSerializableObject()
        {
            throw new NotImplementedException();
        }

        private Double[] BuildOrderArray(int size)
        {
            Double[] order = new Double[size];
            System.Random random = new System.Random();
            for (int i = 0; i < order.Length; i++) order[i] = random.NextDouble();

            return order;
        }

        private int ComputeSampleSize(int numElements)
        {
            return (int)Mathf.Round(numElements * config.Probability);
        }

        public class Configuration : AllPointsSampler.Configuration
        {
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
                    this.probability = percentage / 100;
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
                : base(referenceTransform, normalProcessing)
            {
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