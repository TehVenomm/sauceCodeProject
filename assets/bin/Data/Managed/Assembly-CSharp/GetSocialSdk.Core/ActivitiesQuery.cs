using UnityEngine;

namespace GetSocialSdk.Core
{
	public sealed class ActivitiesQuery : IConvertableToNative
	{
		public enum Filter
		{
			NoFilter,
			Older,
			Newer
		}

		public const string GlobalFeed = "g-global";

		public const int DefaultLimit = 10;

		private readonly ActivityPost.Type _type;

		private readonly string _feed;

		private readonly string _parentActivityId;

		private int _limit = 10;

		private Filter _filter;

		private string _filteringActivityId;

		private string _filterUserId;

		private bool _isFriendsFeed;

		private string[] _tags = new string[0];

		private ActivitiesQuery(ActivityPost.Type type, string feed, string parentActivityId)
		{
			_type = type;
			_feed = feed;
			_parentActivityId = parentActivityId;
		}

		public static ActivitiesQuery PostsForFeed(string feed)
		{
			return new ActivitiesQuery(ActivityPost.Type.Post, feed, null);
		}

		public static ActivitiesQuery PostsForGlobalFeed()
		{
			return new ActivitiesQuery(ActivityPost.Type.Post, "g-global", null);
		}

		public static ActivitiesQuery CommentsToPost(string activityId)
		{
			return new ActivitiesQuery(ActivityPost.Type.Comment, null, activityId);
		}

		public ActivitiesQuery WithLimit(int limit)
		{
			_limit = limit;
			return this;
		}

		public ActivitiesQuery WithFilter(Filter filter, string activityId)
		{
			_filter = filter;
			_filteringActivityId = activityId;
			return this;
		}

		public ActivitiesQuery FilterByUser(string userId)
		{
			_filterUserId = userId;
			return this;
		}

		public ActivitiesQuery FriendsFeed(bool isFriendsFeed)
		{
			_isFriendsFeed = isFriendsFeed;
			return this;
		}

		public ActivitiesQuery WithTags(params string[] tags)
		{
			_tags = tags;
			return this;
		}

		public AndroidJavaObject ToAjo()
		{
			//IL_0005: Unknown result type (might be due to invalid IL or missing references)
			//IL_000a: Expected O, but got Unknown
			AndroidJavaClass ajo = new AndroidJavaClass("im.getsocial.sdk.activities.ActivitiesQuery");
			AndroidJavaObject val = (_type != 0) ? ajo.CallStaticAJO("commentsToPost", _parentActivityId) : ajo.CallStaticAJO("postsForFeed", _feed);
			val.CallAJO("withLimit", _limit);
			val.CallAJO("filterByUser", _filterUserId);
			val.CallAJO("friendsFeed", _isFriendsFeed);
			val.CallAJO("withTags", _tags.ToJavaStringArray());
			if (_filter != 0)
			{
				val.CallAJO("withFilter", _filter.ToAndroidJavaObject(), _filteringActivityId);
			}
			return val;
		}
	}
}
