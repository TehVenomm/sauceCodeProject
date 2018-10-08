using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace GetSocialSdk.Core
{
	public sealed class ActivityPost : IConvertableFromNative<ActivityPost>
	{
		public enum Type
		{
			Post,
			Comment
		}

		public string Id
		{
			get;
			private set;
		}

		public string Text
		{
			get;
			private set;
		}

		public bool HasText => !string.IsNullOrEmpty(Text);

		public string ImageUrl
		{
			get;
			private set;
		}

		public bool HasImage => !string.IsNullOrEmpty(ImageUrl);

		public DateTime CreatedAt
		{
			get;
			private set;
		}

		public string ButtonTitle
		{
			get;
			private set;
		}

		public string ButtonAction
		{
			get;
			private set;
		}

		public bool HasButton => ButtonTitle != null;

		public PostAuthor Author
		{
			get;
			private set;
		}

		public int CommentsCount
		{
			get;
			private set;
		}

		public int LikesCount
		{
			get;
			private set;
		}

		public bool IsLikedByMe
		{
			get;
			private set;
		}

		public DateTime StickyStart
		{
			get;
			private set;
		}

		public DateTime StickyEnd
		{
			get;
			private set;
		}

		public List<Mention> Mentions
		{
			get;
			private set;
		}

		public string FeedId
		{
			get;
			private set;
		}

		public ActivityPost()
		{
		}

		internal ActivityPost(string id, string text, string imageUrl, DateTime createdAt, string buttonTitle, string buttonAction, PostAuthor author, int commentsCount, int likesCount, bool isLikedByMe, DateTime stickyStart, DateTime stickyEnd, List<Mention> mentions, string feedId)
		{
			Id = id;
			Text = text;
			ImageUrl = imageUrl;
			CreatedAt = createdAt;
			ButtonTitle = buttonTitle;
			ButtonAction = buttonAction;
			Author = author;
			CommentsCount = commentsCount;
			LikesCount = likesCount;
			IsLikedByMe = isLikedByMe;
			StickyStart = stickyStart;
			StickyEnd = stickyEnd;
			Mentions = mentions;
			FeedId = feedId;
		}

		public bool IsStickyAt(DateTime dateTime)
		{
			return dateTime.Ticks > StickyStart.Ticks && dateTime.Ticks < StickyEnd.Ticks;
		}

		public override string ToString()
		{
			return $"Id: {Id}, Text: {Text}, HasText: {HasText}, ImageUrl: {ImageUrl}, HasImage: {HasImage}, CreatedAt: {CreatedAt}, ButtonTitle: {ButtonTitle}, ButtonAction: {ButtonAction}, HasButton: {HasButton}, Author: {Author}, CommentsCount: {CommentsCount}, LikesCount: {LikesCount}, IsLikedByMe: {IsLikedByMe}, StickyStart: {StickyStart}, StickyEnd: {StickyEnd}, FeedId: {FeedId}, Mentions: {Mentions.ToDebugString()}";
		}

		private bool Equals(ActivityPost other)
		{
			return string.Equals(Id, other.Id) && string.Equals(Text, other.Text) && string.Equals(ImageUrl, other.ImageUrl) && CreatedAt.Equals(other.CreatedAt) && string.Equals(ButtonTitle, other.ButtonTitle) && string.Equals(ButtonAction, other.ButtonAction) && object.Equals(Author, other.Author) && CommentsCount == other.CommentsCount && LikesCount == other.LikesCount && IsLikedByMe == other.IsLikedByMe && StickyStart.Equals(other.StickyStart) && StickyEnd.Equals(other.StickyEnd) && Mentions.ListEquals(other.Mentions) && string.Equals(FeedId, other.FeedId);
		}

		public override bool Equals(object obj)
		{
			if (object.ReferenceEquals(null, obj))
			{
				return false;
			}
			if (object.ReferenceEquals(this, obj))
			{
				return true;
			}
			return obj is ActivityPost && Equals((ActivityPost)obj);
		}

		public override int GetHashCode()
		{
			int num = (Id != null) ? Id.GetHashCode() : 0;
			num = ((num * 397) ^ ((Text != null) ? Text.GetHashCode() : 0));
			num = ((num * 397) ^ ((ImageUrl != null) ? ImageUrl.GetHashCode() : 0));
			num = ((num * 397) ^ CreatedAt.GetHashCode());
			num = ((num * 397) ^ ((ButtonTitle != null) ? ButtonTitle.GetHashCode() : 0));
			num = ((num * 397) ^ ((ButtonAction != null) ? ButtonAction.GetHashCode() : 0));
			num = ((num * 397) ^ ((Author != null) ? Author.GetHashCode() : 0));
			num = ((num * 397) ^ CommentsCount);
			num = ((num * 397) ^ LikesCount);
			num = ((num * 397) ^ IsLikedByMe.GetHashCode());
			num = ((num * 397) ^ StickyStart.GetHashCode());
			num = ((num * 397) ^ StickyEnd.GetHashCode());
			num = ((num * 397) ^ ((Mentions != null) ? Mentions.GetHashCode() : 0));
			return (num * 397) ^ ((FeedId != null) ? FeedId.GetHashCode() : 0);
		}

		public ActivityPost ParseFromAJO(AndroidJavaObject ajo)
		{
			try
			{
				Id = ajo.CallStr("getId");
				Text = ajo.CallStr("getText");
				ImageUrl = ajo.CallStr("getImageUrl");
				ButtonTitle = ajo.CallStr("getButtonTitle");
				ButtonAction = ajo.CallStr("getButtonAction");
				CreatedAt = DateUtils.FromUnixTime(ajo.CallLong("getCreatedAt"));
				Author = new PostAuthor().ParseFromAJO(ajo.CallAJO("getAuthor"));
				CommentsCount = ajo.CallInt("getCommentsCount");
				LikesCount = ajo.CallInt("getLikesCount");
				IsLikedByMe = ajo.CallBool("isLikedByMe");
				StickyStart = DateUtils.FromUnixTime(ajo.CallLong("getStickyStart"));
				StickyEnd = DateUtils.FromUnixTime(ajo.CallLong("getStickyEnd"));
				FeedId = ajo.CallStr("getFeedId");
				Mentions = ajo.CallAJO("getMentions").FromJavaList().ConvertAll(delegate(AndroidJavaObject mentionAjo)
				{
					try
					{
						return new Mention().ParseFromAJO(mentionAjo);
						IL_0013:
						Mention result;
						return result;
					}
					finally
					{
						((IDisposable)mentionAjo)?.Dispose();
					}
				})
					.ToList();
				return this;
			}
			finally
			{
				((IDisposable)ajo)?.Dispose();
			}
		}
	}
}
