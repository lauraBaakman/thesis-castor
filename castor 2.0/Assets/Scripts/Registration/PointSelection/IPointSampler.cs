using UnityEngine;
using System.Collections.Generic;
using System;

namespace Registration
{
	public interface IPointSampler
	{
		List<Point> Sample(SamplingInformation samplingInfo);

		SerializablePointSampler ToSerializableObject();
	}

	[System.Serializable]
	public class SerializablePointSampler
	{
		public Utils.SerializableTransform referenceTransform;
		public string normalProcessing;
		public float percentage;
		public int numberOfBins;

		public SerializablePointSampler(
			Transform referenceTransform,
			AllPointsSampler.Configuration.NormalProcessing normalProcessing,
			float percentage, int numberOfBins
		) : this(new Utils.SerializableTransform(referenceTransform),
				 NormalProcessingEnumToString(normalProcessing),
				 percentage, numberOfBins)
		{ }

		public SerializablePointSampler(AllPointsSampler.Configuration configuration)
			: this(
				configuration.referenceTransform,
				configuration.normalProcessing,
				100, 1)
		{ }

		public SerializablePointSampler(RandomSubSampling.Configuration configuration)
			: this(
				configuration.referenceTransform,
				configuration.normalProcessing,
				configuration.Percentage, 1)
		{ }

		public SerializablePointSampler(NDOSubsampling.Configuration configuration)
			: this(
				configuration.referenceTransform,
				configuration.normalProcessing,
				configuration.Percentage,
				configuration.BinCount)
		{ }

		private SerializablePointSampler(
			Utils.SerializableTransform referenceTransform, string normalProcessing,
			float percentage, int numberOfBins)
		{
			this.referenceTransform = referenceTransform;
			this.normalProcessing = normalProcessing;
			this.percentage = percentage;
			this.numberOfBins = numberOfBins;
		}

		private static string NormalProcessingEnumToString(AllPointsSampler.Configuration.NormalProcessing normalProcessing)
		{
			switch (normalProcessing)
			{
				case AllPointsSampler.Configuration.NormalProcessing.AreaWeightedSmoothNormals:
					return "AreaWeightedSmoothNormals";
				case AllPointsSampler.Configuration.NormalProcessing.VertexNormals:
					return "VertexNormals";
				case AllPointsSampler.Configuration.NormalProcessing.NoNormals:
					return "NoNormals";
			}
			return "";
		}

	}
}
