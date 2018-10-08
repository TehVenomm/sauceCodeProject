using GetSocialSdk.Core;
using System;
using UnityEngine;

namespace GetSocialSdk.Ui
{
	public sealed class InvitesViewBuilder : ViewBuilder<InvitesViewBuilder>
	{
		private LinkParams _linkParams;

		private InviteContent _inviteContent;

		private Action<string> _onInviteComplete;

		private Action<string> _onInviteCancel;

		private Action<string, GetSocialError> _onInviteFailure;

		public InvitesViewBuilder SetCustomInviteContent(InviteContent inviteContent)
		{
			_inviteContent = inviteContent;
			return this;
		}

		[Obsolete("Deprecated, use SetLinkParams instead.")]
		public InvitesViewBuilder SetCustomReferralData(CustomReferralData customReferralData)
		{
			_linkParams = new LinkParams(customReferralData);
			return this;
		}

		public InvitesViewBuilder SetLinkParams(LinkParams linkParams)
		{
			_linkParams = linkParams;
			return this;
		}

		public InvitesViewBuilder SetInviteCallbacks(Action<string> onComplete, Action<string> onCancel, Action<string, GetSocialError> onFailure)
		{
			Check.Argument.IsNotNull(onComplete, "onComplete", null);
			Check.Argument.IsNotNull(onCancel, "onCancel", null);
			Check.Argument.IsNotNull(onFailure, "onFailure", null);
			_onInviteComplete = onComplete;
			_onInviteCancel = onCancel;
			_onInviteFailure = onFailure;
			return this;
		}

		internal override bool ShowInternal()
		{
			return ShowBuilder(ToAJO());
		}

		private AndroidJavaObject ToAJO()
		{
			//IL_000b: Unknown result type (might be due to invalid IL or missing references)
			//IL_0010: Expected O, but got Unknown
			AndroidJavaObject val = new AndroidJavaObject("im.getsocial.sdk.ui.invites.InvitesViewBuilder", new object[0]);
			if (_linkParams != null)
			{
				val.CallAJO("setLinkParams", _linkParams.ToAjo());
			}
			if (_inviteContent != null)
			{
				AndroidJavaObject val2 = _inviteContent.ToAjo();
				val.CallAJO("setCustomInviteContent", val2);
			}
			if (_onInviteComplete != null)
			{
				val.CallAJO("setInviteCallback", new InviteUiCallbackProxy(_onInviteComplete, _onInviteCancel, _onInviteFailure));
			}
			return val;
		}
	}
}
