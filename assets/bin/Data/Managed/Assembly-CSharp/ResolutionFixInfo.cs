using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class ResolutionFixInfo
{
	public string Name;

	public GameObject PanelFix;

	public int DepthParent = 1;

	public FixedPanelAction FixedPanelAction = FixedPanelAction.FIX_SIZE;

	public List<FixOffsetPosition> FixOffsetHeightPosition;

	public List<string> LockPosition;

	public List<string> LockScaleUI;

	public List<string> ScaleUIChild;

	public List<LockAnchorPath> LockAnchorRoot;

	public List<string> ListUpdateAnchor;

	public List<string> ListObjectFix;

	public List<string> UIStaticUnLock;

	public string Note;

	public string Path = "";

	public bool IsLockMyObj;

	public bool IsEditMain;

	public bool IsEditOffsetHeighPosition;

	public bool IsEditLockPosition;

	public bool IsEditLockScale;

	public bool IsEditScaleUIChild;

	public bool IsObjectFix;

	public bool IsLockAnchorRoot;

	public bool IsUIStaticUnLock;

	public bool IsUpdateAnchorAfterFix;

	public bool IsAdd
	{
		get
		{
			if (PanelFix != null)
			{
				return PanelFix.GetComponent<FixedPanelNGUI>();
			}
			return false;
		}
	}
}
