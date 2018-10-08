using UnityEngine;

public class UIAnimationGauge : UIHGauge
{
	private Material _mat;

	private void Awake()
	{
		//IL_0007: Unknown result type (might be due to invalid IL or missing references)
		//IL_000c: Expected O, but got Unknown
		_mat = this.GetComponent<MeshRenderer>().get_material();
	}

	protected override void UpdateGauge()
	{
		if (!(_mat == null))
		{
			_mat.SetFloat("_Ratio", base.nowPercent);
		}
	}
}
