using GetSocialSdk.Core;
using System;
using UnityEngine;

namespace GetSocialSdk.Ui
{
	public abstract class ViewBuilder<T> where T : ViewBuilder<T>
	{
		protected Action _onOpen;

		protected Action _onClose;

		protected string _customWindowTitle;

		protected UiActionListener _uiActionListener;

		public T SetWindowTitle(string title)
		{
			_customWindowTitle = title;
			return (T)this;
		}

		public T SetViewStateCallbacks(Action onOpen, Action onClose)
		{
			_onOpen = onOpen;
			_onClose = onClose;
			return (T)this;
		}

		public T SetUiActionListener(UiActionListener listener)
		{
			_uiActionListener = listener;
			return (T)this;
		}

		internal abstract bool ShowInternal();

		public bool Show()
		{
			return GetSocialUi.ShowView<T>(this);
		}

		protected unsafe bool ShowBuilder(AndroidJavaObject builder)
		{
			//IL_003e: Unknown result type (might be due to invalid IL or missing references)
			//IL_0043: Expected O, but got Unknown
			SetUiActionListenerAJO(builder);
			SetTitleAJO(builder);
			SetViewStateListener(builder);
			GetSocialUiFactory.InstantiateGetSocialUi();
			_003CShowBuilder_003Ec__AnonStorey7F2 _003CShowBuilder_003Ec__AnonStorey7F;
			return JniUtils.RunOnUiThreadSafe(new Action((object)_003CShowBuilder_003Ec__AnonStorey7F, (IntPtr)(void*)/*OpCode not supported: LdFtn*/));
		}

		private void SetTitleAJO(AndroidJavaObject builderAJO)
		{
			if (_customWindowTitle != null)
			{
				builderAJO.CallAJO("setWindowTitle", _customWindowTitle);
			}
		}

		private void SetUiActionListenerAJO(AndroidJavaObject builderAJO)
		{
			if (_uiActionListener != null)
			{
				builderAJO.CallAJO("setUiActionListener", new UiActionListenerProxy(_uiActionListener));
			}
		}

		private void SetViewStateListener(AndroidJavaObject builderAJO)
		{
			if (_onOpen != null || _onClose != null)
			{
				builderAJO.CallAJO("setViewStateListener", new ViewStateListener(_onOpen, _onClose));
			}
		}
	}
}
