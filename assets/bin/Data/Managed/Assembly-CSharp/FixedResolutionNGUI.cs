using UnityEngine;

public class FixedResolutionNGUI : MonoBehaviour
{
	private const float BASERATIO = 0.5625f;

	private void Start()
	{
		fixedNGUI();
	}

	private void fixedNGUI()
	{
		float aspect = Camera.main.aspect;
		float num = aspect / 0.5625f;
		Vector3 vector = Vector3.one * num;
		Debug.LogError("ratio" + num + " aspect " + aspect + " localScale " + vector);
		if (aspect < 0.5625f)
		{
			base.transform.localScale = vector;
		}
		Debug.LogError("Local Scale " + base.transform.localScale);
	}
}
