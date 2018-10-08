using System;
using UnityEngine;

public class AndroidClipBoard : iClipBoard
{
	public unsafe void SetClipBoard(string s)
	{
		//IL_0012: Unknown result type (might be due to invalid IL or missing references)
		//IL_0017: Expected O, but got Unknown
		//IL_0043: Unknown result type (might be due to invalid IL or missing references)
		//IL_0048: Expected O, but got Unknown
		AndroidJavaClass val = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
		AndroidJavaObject activity = val.GetStatic<AndroidJavaObject>("currentActivity");
		_003CSetClipBoard_003Ec__AnonStorey6EA _003CSetClipBoard_003Ec__AnonStorey6EA;
		activity.Call("runOnUiThread", new object[1]
		{
			(object)new AndroidJavaRunnable((object)_003CSetClipBoard_003Ec__AnonStorey6EA, (IntPtr)(void*)/*OpCode not supported: LdFtn*/)
		});
	}
}
