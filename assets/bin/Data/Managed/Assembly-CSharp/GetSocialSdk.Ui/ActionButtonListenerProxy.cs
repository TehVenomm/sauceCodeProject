using GetSocialSdk.Core;
using System;
using UnityEngine;

namespace GetSocialSdk.Ui
{
	public class ActionButtonListenerProxy : JavaInterfaceProxy
	{
		private readonly Action<string, ActivityPost> _onButtonClicked;

		public ActionButtonListenerProxy(Action<string, ActivityPost> onButtonClicked)
			: base("im.getsocial.sdk.ui.activities.ActionButtonListener")
		{
			_onButtonClicked = onButtonClicked;
		}

		private unsafe void onButtonClicked(string action, AndroidJavaObject post)
		{
			//IL_0036: Unknown result type (might be due to invalid IL or missing references)
			//IL_003b: Expected O, but got Unknown
			Debug.Log((object)">>>>>>> XXXX");
			ActivityPost activityPost = new ActivityPost().ParseFromAJO(post);
			_003ConButtonClicked_003Ec__AnonStorey7F3 _003ConButtonClicked_003Ec__AnonStorey7F;
			JavaInterfaceProxy.ExecuteOnMainThread(new Action((object)_003ConButtonClicked_003Ec__AnonStorey7F, (IntPtr)(void*)/*OpCode not supported: LdFtn*/));
		}
	}
}
