using System;
using System.Collections.Generic;
using UnityEngine;

namespace GetSocialSdk.Core
{
	public sealed class ReferralData : IConvertableFromNative<ReferralData>
	{
		public string Token
		{
			get;
			private set;
		}

		public string ReferrerUserId
		{
			get;
			private set;
		}

		public string ReferrerChannelId
		{
			get;
			private set;
		}

		public bool IsFirstMatch
		{
			get;
			private set;
		}

		public bool IsGuaranteedMatch
		{
			get;
			private set;
		}

		public bool IsReinstall
		{
			get;
			private set;
		}

		public bool IsFirstMatchLink
		{
			get;
			private set;
		}

		[Obsolete("Deprecated, use LinkParams instead.")]
		public CustomReferralData CustomReferralData
		{
			get;
			private set;
		}

		public LinkParams LinkParams
		{
			get;
			private set;
		}

		[Obsolete("Deprecated, use OriginalLinkParams instead.")]
		public CustomReferralData OriginalCustomReferralData
		{
			get;
			private set;
		}

		public LinkParams OriginalLinkParams
		{
			get;
			private set;
		}

		public ReferralData()
		{
		}

		internal ReferralData(string token, string referrerUserId, string referrerChannelId, bool isFirstMatch, bool isGuaranteedMatch, bool isReinstall, bool isFirstMatchLink, CustomReferralData customReferralData, LinkParams linkParams, CustomReferralData originalCustomReferralData, LinkParams originalLinkParams)
		{
			Token = token;
			ReferrerUserId = referrerUserId;
			ReferrerChannelId = referrerChannelId;
			IsFirstMatch = isFirstMatch;
			IsGuaranteedMatch = isGuaranteedMatch;
			IsReinstall = isReinstall;
			IsFirstMatchLink = isFirstMatchLink;
			CustomReferralData = customReferralData;
			OriginalCustomReferralData = originalCustomReferralData;
			LinkParams = linkParams;
			OriginalLinkParams = originalLinkParams;
		}

		private bool Equals(ReferralData other)
		{
			return string.Equals(Token, other.Token) && string.Equals(ReferrerUserId, other.ReferrerUserId) && string.Equals(ReferrerChannelId, other.ReferrerChannelId) && IsFirstMatch == other.IsFirstMatch && IsGuaranteedMatch == other.IsGuaranteedMatch && IsReinstall == other.IsReinstall && IsFirstMatchLink == other.IsFirstMatchLink && object.Equals(CustomReferralData, other.CustomReferralData) && object.Equals(LinkParams, other.LinkParams) && object.Equals(OriginalCustomReferralData, other.OriginalCustomReferralData) && object.Equals(OriginalLinkParams, other.OriginalLinkParams);
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
			return obj is ReferralData && Equals((ReferralData)obj);
		}

		public override int GetHashCode()
		{
			int num = (Token != null) ? Token.GetHashCode() : 0;
			num = ((num * 397) ^ ((ReferrerUserId != null) ? ReferrerUserId.GetHashCode() : 0));
			num = ((num * 397) ^ ((ReferrerChannelId != null) ? ReferrerChannelId.GetHashCode() : 0));
			num = ((num * 397) ^ IsFirstMatch.GetHashCode());
			num = ((num * 397) ^ IsGuaranteedMatch.GetHashCode());
			num = ((num * 397) ^ IsReinstall.GetHashCode());
			num = ((num * 397) ^ IsFirstMatchLink.GetHashCode());
			num = ((num * 397) ^ ((CustomReferralData != null) ? CustomReferralData.GetHashCode() : 0));
			num = ((num * 397) ^ ((OriginalCustomReferralData != null) ? OriginalCustomReferralData.GetHashCode() : 0));
			num = ((num * 397) ^ ((LinkParams != null) ? LinkParams.GetHashCode() : 0));
			return (num * 397) ^ ((OriginalLinkParams != null) ? OriginalLinkParams.GetHashCode() : 0);
		}

		public override string ToString()
		{
			return $"[ReferralData: Token: {Token}, ReferrerUserId={ReferrerUserId}, ReferrerChannelId={ReferrerChannelId}, IsFirstMatch={IsFirstMatch}, IsGuaranteedMatch={IsGuaranteedMatch}, LinkParams={LinkParams.ToDebugString()}, , OriginalLinkParams={OriginalLinkParams.ToDebugString()}]";
		}

		public ReferralData ParseFromAJO(AndroidJavaObject ajo)
		{
			if (!ajo.IsJavaNull())
			{
				try
				{
					Token = ajo.CallStr("getToken");
					ReferrerUserId = ajo.CallStr("getReferrerUserId");
					ReferrerChannelId = ajo.CallStr("getReferrerChannelId");
					IsFirstMatch = ajo.CallBool("isFirstMatch");
					IsGuaranteedMatch = ajo.CallBool("isGuaranteedMatch");
					IsReinstall = ajo.CallBool("isReinstall");
					IsFirstMatchLink = ajo.CallBool("isFirstMatchLink");
					Dictionary<string, string> data = ajo.CallAJO("getLinkParams").FromJavaHashMap();
					LinkParams = new LinkParams(data);
					Dictionary<string, string> data2 = ajo.CallAJO("getOriginalLinkParams").FromJavaHashMap();
					OriginalLinkParams = new LinkParams(data2);
					CustomReferralData = new CustomReferralData(LinkParams);
					OriginalCustomReferralData = new CustomReferralData(OriginalLinkParams);
					return this;
				}
				finally
				{
					((IDisposable)ajo)?.Dispose();
				}
			}
			return null;
		}
	}
}
