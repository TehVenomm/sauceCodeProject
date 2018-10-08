using GetSocialSdk.Core;
using System;

namespace GetSocialSdk.Ui
{
	public class TagClickListenerProxy : JavaInterfaceProxy
	{
		private readonly Action<string> _tagClickListener;

		public TagClickListenerProxy(Action<string> tagClickListener)
			: base("im.getsocial.sdk.ui.TagClickListener")
		{
			_tagClickListener = tagClickListener;
		}

		private unsafe void onTagClicked(string tag)
		{
			//IL_001b: Unknown result type (might be due to invalid IL or missing references)
			//IL_0020: Expected O, but got Unknown
			_003ConTagClicked_003Ec__AnonStorey7F8 _003ConTagClicked_003Ec__AnonStorey7F;
			JavaInterfaceProxy.ExecuteOnMainThread(new Action((object)_003ConTagClicked_003Ec__AnonStorey7F, (IntPtr)(void*)/*OpCode not supported: LdFtn*/));
		}
	}
}
