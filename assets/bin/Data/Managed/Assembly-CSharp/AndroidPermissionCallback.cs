using System;
using UnityEngine;

public class AndroidPermissionCallback : AndroidJavaProxy
{
	private event Action<string> OnPermissionGrantedAction;

	private event Action<string> OnPermissionDeniedAction;

	public AndroidPermissionCallback(Action<string> onGrantedCallback, Action<string> onDeniedCallback)
		: base("com.unity3d.player.UnityAndroidPermissions$IPermissionRequestResult")
	{
		if (onGrantedCallback != null)
		{
			this.OnPermissionGrantedAction = (Action<string>)Delegate.Combine(this.OnPermissionGrantedAction, onGrantedCallback);
		}
		if (onDeniedCallback != null)
		{
			this.OnPermissionDeniedAction = (Action<string>)Delegate.Combine(this.OnPermissionDeniedAction, onDeniedCallback);
		}
	}

	public virtual void OnPermissionGranted(string permissionName)
	{
		if (this.OnPermissionGrantedAction != null)
		{
			this.OnPermissionGrantedAction(permissionName);
		}
	}

	public virtual void OnPermissionDenied(string permissionName)
	{
		if (this.OnPermissionDeniedAction != null)
		{
			this.OnPermissionDeniedAction(permissionName);
		}
	}
}
