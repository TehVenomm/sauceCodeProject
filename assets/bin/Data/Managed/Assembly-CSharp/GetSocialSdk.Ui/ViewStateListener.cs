using GetSocialSdk.Core;
using System;

namespace GetSocialSdk.Ui
{
	public class ViewStateListener : JavaInterfaceProxy
	{
		private Action _onOpen;

		private Action _onClose;

		public ViewStateListener(Action onOpen, Action onClose)
			: base("im.getsocial.sdk.ui.ViewStateListener")
		{
			_onOpen = onOpen;
			_onClose = onClose;
		}

		private void onOpen()
		{
			if (_onOpen != null)
			{
				_onOpen.Invoke();
			}
		}

		private void onClose()
		{
			if (_onClose != null)
			{
				_onClose.Invoke();
			}
		}
	}
}
