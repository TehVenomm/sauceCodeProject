using GooglePlayGames.OurUtils;
using System;
using System.Collections.Generic;
using System.Linq;

namespace GooglePlayGames.BasicApi.Multiplayer
{
	public class TurnBasedMatch
	{
		public enum MatchStatus
		{
			Active,
			AutoMatching,
			Cancelled,
			Complete,
			Expired,
			Unknown,
			Deleted
		}

		public enum MatchTurnStatus
		{
			Complete,
			Invited,
			MyTurn,
			TheirTurn,
			Unknown
		}

		private string mMatchId;

		private byte[] mData;

		private bool mCanRematch;

		private uint mAvailableAutomatchSlots;

		private string mSelfParticipantId;

		private List<Participant> mParticipants;

		private string mPendingParticipantId;

		private MatchTurnStatus mTurnStatus;

		private MatchStatus mMatchStatus;

		private uint mVariant;

		private uint mVersion;

		public string MatchId => mMatchId;

		public byte[] Data => mData;

		public bool CanRematch => mCanRematch;

		public string SelfParticipantId => mSelfParticipantId;

		public Participant Self => GetParticipant(mSelfParticipantId);

		public List<Participant> Participants => mParticipants;

		public string PendingParticipantId => mPendingParticipantId;

		public Participant PendingParticipant => (mPendingParticipantId != null) ? GetParticipant(mPendingParticipantId) : null;

		public MatchTurnStatus TurnStatus => mTurnStatus;

		public MatchStatus Status => mMatchStatus;

		public uint Variant => mVariant;

		public uint Version => mVersion;

		public uint AvailableAutomatchSlots => mAvailableAutomatchSlots;

		internal TurnBasedMatch(string matchId, byte[] data, bool canRematch, string selfParticipantId, List<Participant> participants, uint availableAutomatchSlots, string pendingParticipantId, MatchTurnStatus turnStatus, MatchStatus matchStatus, uint variant, uint version)
		{
			mMatchId = matchId;
			mData = data;
			mCanRematch = canRematch;
			mSelfParticipantId = selfParticipantId;
			mParticipants = participants;
			mParticipants.Sort();
			mAvailableAutomatchSlots = availableAutomatchSlots;
			mPendingParticipantId = pendingParticipantId;
			mTurnStatus = turnStatus;
			mMatchStatus = matchStatus;
			mVariant = variant;
			mVersion = version;
		}

		public Participant GetParticipant(string participantId)
		{
			foreach (Participant mParticipant in mParticipants)
			{
				if (mParticipant.ParticipantId.Equals(participantId))
				{
					return mParticipant;
				}
			}
			Logger.w("Participant not found in turn-based match: " + participantId);
			return null;
		}

		public unsafe override string ToString()
		{
			object[] obj = new object[10]
			{
				mMatchId,
				mData,
				mCanRematch,
				mSelfParticipantId,
				null,
				null,
				null,
				null,
				null,
				null
			};
			List<Participant> source = mParticipants;
			if (_003C_003Ef__am_0024cacheB == null)
			{
				_003C_003Ef__am_0024cacheB = new Func<Participant, string>((object)null, (IntPtr)(void*)/*OpCode not supported: LdFtn*/);
			}
			obj[4] = string.Join(",", source.Select<Participant, string>(_003C_003Ef__am_0024cacheB).ToArray());
			obj[5] = mPendingParticipantId;
			obj[6] = mTurnStatus;
			obj[7] = mMatchStatus;
			obj[8] = mVariant;
			obj[9] = mVersion;
			return string.Format("[TurnBasedMatch: mMatchId={0}, mData={1}, mCanRematch={2}, mSelfParticipantId={3}, mParticipants={4}, mPendingParticipantId={5}, mTurnStatus={6}, mMatchStatus={7}, mVariant={8}, mVersion={9}]", obj);
		}
	}
}
