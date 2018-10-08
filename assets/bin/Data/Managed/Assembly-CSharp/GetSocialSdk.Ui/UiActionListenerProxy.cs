using GetSocialSdk.Core;
using System;
using UnityEngine;

namespace GetSocialSdk.Ui
{
	public class UiActionListenerProxy : JavaInterfaceProxy
	{
		private UiActionListener _uiActionListener;

		public UiActionListenerProxy(UiActionListener uiActionListener)
			: base("im.getsocial.sdk.ui.UiActionListener")
		{
			_uiActionListener = uiActionListener;
		}

		private unsafe void onUiAction(AndroidJavaObject actionType, AndroidJavaObject pendingAction)
		{
			//IL_0021: Unknown result type (might be due to invalid IL or missing references)
			//IL_0026: Expected O, but got Unknown
			_003ConUiAction_003Ec__AnonStorey80D _003ConUiAction_003Ec__AnonStorey80D;
			_uiActionListener(toUIActionEnum(actionType), new Action((object)_003ConUiAction_003Ec__AnonStorey80D, (IntPtr)(void*)/*OpCode not supported: LdFtn*/));
		}

		private UiAction toUIActionEnum(AndroidJavaObject actionType)
		{
			return (UiAction)actionType.CallInt("ordinal");
		}
	}
}
