using UnityEngine;

public class FloatRangeSliderAttribute : PropertyAttribute
{
	private float _min;
	public float Min => _min;

	private float _max;
	public float Max => _max;

	public FloatRangeSliderAttribute(float min, float max)
	{
		_min = min;
		_max = max < min ? min : max;
	}
}