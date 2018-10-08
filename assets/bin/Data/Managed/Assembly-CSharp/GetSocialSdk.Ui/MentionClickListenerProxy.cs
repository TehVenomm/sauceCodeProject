using GetSocialSdk.Core;
using System;

namespace GetSocialSdk.Ui
{
	public class MentionClickListenerProxy : JavaInterfaceProxy
	{
		private readonly Action<string> _onMentionClickListener;

		public MentionClickListenerProxy(Action<string> onMentionClickListener)
			: base("im.getsocial.sdk.ui.MentionClickListener")
		{
			_onMentionClickListener = onMentionClickListener;
		}

		private unsafe void onMentionClicked(string userId)
		{
			//IL_001b: Unknown result type (might be due to invalid IL or missing references)
			//IL_0020: Expected O, but got Unknown
			_003ConMentionClicked_003Ec__AnonStorey80B _003ConMentionClicked_003Ec__AnonStorey80B;
			JavaInterfaceProxy.ExecuteOnMainThread(new Action((object)_003ConMentionClicked_003Ec__AnonStorey80B, (IntPtr)(void*)/*OpCode not supported: LdFtn*/));
		}
	}
}
