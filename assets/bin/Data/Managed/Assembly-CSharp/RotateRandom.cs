using UnityEngine;

public class RotateRandom : MonoBehaviour
{
	public float Xrandom_low;

	public float Xrandom_high = 360f;

	public float Yrandom_low;

	public float Yrandom_high = 360f;

	public float Zrandom_low;

	public float Zrandom_high = 360f;

	private void OnEnable()
	{
		base.transform.rotation = Quaternion.Euler(Random.Range(Xrandom_low, Xrandom_high), Random.Range(Yrandom_low, Yrandom_high), Random.Range(Zrandom_low, Zrandom_high));
	}
}
