using System.Collections.Generic;
using UnityEngine;

namespace GetSocialSdk.Core
{
	public class Notification : IConvertableFromNative<Notification>
	{
		public enum Type
		{
			Custom,
			OpenProfile,
			OpenActivity,
			OpenInvites,
			OpenUrl
		}

		public enum NotificationTypes
		{
			Comment = 0,
			LikeActivity = 1,
			LikeComment = 2,
			CommentedInSameThread = 5,
			NewFriendship = 6,
			InviteAccepted = 7,
			MentionInComment = 8,
			MentionInActivity = 9,
			ReplyToComment = 10,
			Targeting = 11,
			Direct = 12
		}

		public static class Key
		{
			public static class OpenActivity
			{
				public const string ActivityId = "$activity_id";

				public const string CommentId = "$comment_id";
			}

			public static class OpenProfile
			{
				public const string UserId = "$user_id";
			}
		}

		public string Id
		{
			get;
			private set;
		}

		public Type Action
		{
			get;
			private set;
		}

		public bool WasRead
		{
			get;
			private set;
		}

		public NotificationTypes NotificationType
		{
			get;
			private set;
		}

		public long CreatedAt
		{
			get;
			private set;
		}

		public string Title
		{
			get;
			private set;
		}

		public string Text
		{
			get;
			private set;
		}

		public Dictionary<string, string> ActionData
		{
			get;
			private set;
		}

		public override string ToString()
		{
			return $"Id: {Id}, Action: {Action}, WasRead: {WasRead}, NotificationType: {NotificationType}, CreatedAt: {CreatedAt}, Title: {Title}, Text: {Text}, ActionData: {ActionData.ToDebugString()}";
		}

		public Notification ParseFromAJO(AndroidJavaObject ajo)
		{
			Id = ajo.CallStr("getId");
			Action = (Type)ajo.CallInt("getActionType");
			WasRead = ajo.CallBool("wasRead");
			NotificationType = (NotificationTypes)ajo.CallInt("getType");
			CreatedAt = ajo.CallLong("getCreatedAt");
			Title = ajo.CallStr("getTitle");
			Text = ajo.CallStr("getText");
			ActionData = ajo.CallAJO("getActionData").FromJavaHashMap();
			return this;
		}
	}
}
