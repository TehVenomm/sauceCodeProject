using GetSocialSdk.Core;
using System;
using UnityEngine;

namespace GetSocialSdk.Ui
{
	public sealed class ActivityFeedViewBuilder : ViewBuilder<ActivityFeedViewBuilder>
	{
		private readonly string _feed;

		private Action<string, ActivityPost> _onButtonClickListener;

		private Action<PublicUser> _onAvatarClickListener;

		private Action<string> _onMentionClickListener;

		private Action<string> _tagClickListener;

		private string _filterUserId;

		private bool _readOnly;

		private bool _friendsFeed;

		private string[] _tags = new string[0];

		internal ActivityFeedViewBuilder()
		{
			_feed = "g-global";
		}

		internal ActivityFeedViewBuilder(string feed)
		{
			_feed = feed;
		}

		public ActivityFeedViewBuilder SetButtonActionListener(Action<string, ActivityPost> onButtonClickListener)
		{
			_onButtonClickListener = onButtonClickListener;
			return this;
		}

		public ActivityFeedViewBuilder SetAvatarClickListener(Action<PublicUser> onAvatarClickListener)
		{
			_onAvatarClickListener = onAvatarClickListener;
			return this;
		}

		public ActivityFeedViewBuilder SetMentionClickListener(Action<string> mentionClickListener)
		{
			_onMentionClickListener = mentionClickListener;
			return this;
		}

		public ActivityFeedViewBuilder SetTagClickListener(Action<string> tagClickListener)
		{
			_tagClickListener = tagClickListener;
			return this;
		}

		public ActivityFeedViewBuilder SetFilterByUser(string userId)
		{
			_filterUserId = userId;
			return this;
		}

		public ActivityFeedViewBuilder SetReadOnly(bool readOnly)
		{
			_readOnly = readOnly;
			return this;
		}

		public ActivityFeedViewBuilder SetShowFriendsFeed(bool showFriendsFeed)
		{
			_friendsFeed = showFriendsFeed;
			return this;
		}

		public ActivityFeedViewBuilder SetFilterByTags(params string[] tags)
		{
			_tags = tags;
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
			AndroidJavaObject val = new AndroidJavaObject("im.getsocial.sdk.ui.activities.ActivityFeedViewBuilder", new object[1]
			{
				_feed
			});
			if (_filterUserId != null)
			{
				val.CallAJO("setFilterByUser", _filterUserId);
			}
			if (_onButtonClickListener != null)
			{
				val.CallAJO("setButtonActionListener", new ActionButtonListenerProxy(_onButtonClickListener));
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
			val.CallAJO("setReadOnly", _readOnly);
			val.CallAJO("setShowFriendsFeed", _friendsFeed);
			val.CallAJO("setFilterByTags", _tags.ToJavaStringArray());
			return val;
		}
	}
}
