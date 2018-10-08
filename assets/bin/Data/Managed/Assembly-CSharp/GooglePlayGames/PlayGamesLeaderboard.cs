using GooglePlayGames.BasicApi;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms;

namespace GooglePlayGames
{
	public class PlayGamesLeaderboard
	{
		private string mId;

		private UserScope mUserScope;

		private Range mRange;

		private TimeScope mTimeScope;

		private string[] mFilteredUserIds;

		private bool mLoading;

		private IScore mLocalUserScore;

		private uint mMaxRange;

		private List<PlayGamesScore> mScoreList = new List<PlayGamesScore>();

		private string mTitle;

		public bool loading
		{
			get
			{
				return mLoading;
			}
			internal set
			{
				mLoading = value;
			}
		}

		public string id
		{
			get
			{
				return mId;
			}
			set
			{
				mId = value;
			}
		}

		public UserScope userScope
		{
			get
			{
				//IL_0001: Unknown result type (might be due to invalid IL or missing references)
				return mUserScope;
			}
			set
			{
				//IL_0001: Unknown result type (might be due to invalid IL or missing references)
				//IL_0002: Unknown result type (might be due to invalid IL or missing references)
				mUserScope = value;
			}
		}

		public Range range
		{
			get
			{
				//IL_0001: Unknown result type (might be due to invalid IL or missing references)
				return mRange;
			}
			set
			{
				//IL_0001: Unknown result type (might be due to invalid IL or missing references)
				//IL_0002: Unknown result type (might be due to invalid IL or missing references)
				mRange = value;
			}
		}

		public TimeScope timeScope
		{
			get
			{
				//IL_0001: Unknown result type (might be due to invalid IL or missing references)
				return mTimeScope;
			}
			set
			{
				//IL_0001: Unknown result type (might be due to invalid IL or missing references)
				//IL_0002: Unknown result type (might be due to invalid IL or missing references)
				mTimeScope = value;
			}
		}

		public IScore localUserScore => mLocalUserScore;

		public uint maxRange => mMaxRange;

		public IScore[] scores
		{
			get
			{
				PlayGamesScore[] array = new PlayGamesScore[mScoreList.Count];
				mScoreList.CopyTo(array);
				return (IScore[])array;
			}
		}

		public string title => mTitle;

		public int ScoreCount => mScoreList.Count;

		public PlayGamesLeaderboard(string id)
		{
			mId = id;
		}

		public void SetUserFilter(string[] userIDs)
		{
			mFilteredUserIds = userIDs;
		}

		public void LoadScores(Action<bool> callback)
		{
			PlayGamesPlatform.Instance.LoadScores(this, callback);
		}

		internal bool SetFromData(LeaderboardScoreData data)
		{
			if (data.Valid)
			{
				Debug.Log((object)("Setting leaderboard from: " + data));
				SetMaxRange(data.ApproximateCount);
				SetTitle(data.Title);
				SetLocalUserScore((PlayGamesScore)data.PlayerScore);
				IScore[] scores = data.Scores;
				foreach (IScore val in scores)
				{
					AddScore((PlayGamesScore)val);
				}
				mLoading = (data.Scores.Length == 0 || HasAllScores());
			}
			return data.Valid;
		}

		internal void SetMaxRange(ulong val)
		{
			mMaxRange = (uint)val;
		}

		internal void SetTitle(string value)
		{
			mTitle = value;
		}

		internal void SetLocalUserScore(PlayGamesScore score)
		{
			mLocalUserScore = score;
		}

		internal int AddScore(PlayGamesScore score)
		{
			if (mFilteredUserIds == null || mFilteredUserIds.Length == 0)
			{
				mScoreList.Add(score);
			}
			else
			{
				string[] array = mFilteredUserIds;
				foreach (string text in array)
				{
					if (text.Equals(score.userID))
					{
						return mScoreList.Count;
					}
				}
				mScoreList.Add(score);
			}
			return mScoreList.Count;
		}

		internal bool HasAllScores()
		{
			return mScoreList.Count >= mRange.count || mScoreList.Count >= maxRange;
		}
	}
}
