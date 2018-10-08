using GetSocialSdk.Core;
using System;
using UnityEngine;

namespace GetSocialSdk.Ui
{
	public sealed class ActivityDetailsViewBuilder : ViewBuilder<ActivityDetailsViewBuilder>
	{
		private readonly string _activityId;

		private string _commentId;

		private bool _showActivityFeedView;

		private bool _readOnly;

		private Action<string, ActivityPost> _onButtonClicked;

		private Action<PublicUser> _onAvatarClickListener;

		private Action<string> _onMentionClickListener;

		private Action<string> _tagClickListener;

		internal ActivityDetailsViewBuilder(string activityId)
		{
			_activityId = activityId;
			_showActivityFeedView = true;
		}

		public ActivityDetailsViewBuilder SetShowActivityFeedView(bool showFeedView)
		{
			_showActivityFeedView = showFeedView;
			return this;
		}

		public ActivityDetailsViewBuilder SetButtonActionListener(Action<string, ActivityPost> onButtonClicked)
		{
			_onButtonClicked = onButtonClicked;
			return this;
		}

		public ActivityDetailsViewBuilder SetAvatarClickListener(Action<PublicUser> onAvatarClickListener)
		{
			_onAvatarClickListener = onAvatarClickListener;
			return this;
		}

		public ActivityDetailsViewBuilder SetMentionClickListener(Action<string> mentionClickListener)
		{
			_onMentionClickListener = mentionClickListener;
			return this;
		}

		public ActivityDetailsViewBuilder SetTagClickListener(Action<string> tagClickListener)
		{
			_tagClickListener = tagClickListener;
			return this;
		}

		public ActivityDetailsViewBuilder SetReadOnly(bool readOnly)
		{
			_readOnly = readOnly;
			return this;
		}

		public ActivityDetailsViewBuilder SetCommentId(string commentId)
		{
			_commentId = commentId;
			return this;
		}

		internal override bool ShowInternal()
		{
			return ShowBuilder(ToAJO());
		}

		private AndroidJavaObject ToAJO()
		{
			//IL_0014: Unknown result type (might be due to invalid IL or missing references)
			//IL_0019: Expected O, but got Unknown
			AndroidJavaObject val = new AndroidJavaObject("im.getsocial.sdk.ui.activities.ActivityDetailsViewBuilder", new object[1]
			{
				_activityId
			});
			val.CallAJO("setShowActivityFeedView", _showActivityFeedView);
			if (_onButtonClicked != null)
			{
				val.CallAJO("setButtonActionListener", new ActionButtonListenerProxy(_onButtonClicked));
			}
			if (_onAvatarClickListener != null)
			{
				val.CallAJO("setAvatarClickListener", new AvatarClickListenerProxy(_onAvatarClickListener));
			}
			if (_onMentionClickListener != null)
			{
				val.CallAJO("setMentionClickListener", new MentionClickListenerProxy(_onMentionClickListener));
			}
			if (_tagClickListener != null)
			{
				val.CallAJO("setTagClickListener", new TagClickListenerProxy(_tagClickListener));
			}
			if (_commentId != null)
			{
				val.CallAJO("setCommentId", _commentId);
			}
			val.CallAJO("setReadOnly", _readOnly);
			return val;
		}
	}
}
