using System;
using UnityEngine;

[Serializable]
public class ResolutionIphoneXResource
{
	public string Name;

	public GameObject PanelFix;

	public bool LockRatioPosition;

	public string Path = string.Empty;

	public bool IsEditMain;

	public bool IsAdd
	{
		get
		{
			if (PanelFix != null)
			{
				return Object.op_Implicit(PanelFix.GetComponent<FixedNGUIThrowIphoneX>());
			}
			return false;
		}
	}
}
