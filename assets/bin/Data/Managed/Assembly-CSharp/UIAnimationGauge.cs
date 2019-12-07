using UnityEngine;

public class UIAnimationGauge : UIHGauge
{
	private Material _mat;

	private void Awake()
	{
		_mat = GetComponent<MeshRenderer>().material;
	}

	protected override void UpdateGauge()
	{
		if (!(_mat == null))
		{
			_mat.SetFloat("_Ratio", base.nowPercent);
		}
	}
}
