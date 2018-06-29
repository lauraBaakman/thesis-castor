using System.Collections.Generic;
using Registration;
using UnityEngine;

public class SampleStorage : MonoBehaviour
{
	private IPointSampler pointSampler;
	public IPointSampler PointSampler
	{
		set { this.pointSampler = value; }
	}

	private List<Point> sample;

	void Start()
	{
		this.sample = new List<Point>();
	}

	public List<Point> GetSample()
	{
		Debug.Log(this.gameObject.name + "GetSample");
		if (this.sample.Count == 0) this.Sample();
		return this.sample;
	}

	private void Sample()
	{
		Debug.Log(this.gameObject.name + "Sample");
		Ticker.Receiver.Instance.SendMessage(
			methodName: "OnMessage",
			value: new Ticker.Message.InfoMessage(
				string.Format("Sampling the fragment with name {0}.", this.gameObject.name)
			),
			options: SendMessageOptions.RequireReceiver
		);
		RealExperimentLogger.Instance.Log("Sampling " + this.gameObject.name);
		this.sample = this.pointSampler.Sample(new SamplingInformation(this.gameObject));
	}
}