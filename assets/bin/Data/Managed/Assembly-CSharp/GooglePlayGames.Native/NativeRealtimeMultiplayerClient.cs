using GooglePlayGames.BasicApi.Multiplayer;
using GooglePlayGames.Native.Cwrapper;
using GooglePlayGames.Native.PInvoke;
using GooglePlayGames.OurUtils;
using System;
using System.Collections.Generic;
using System.Linq;

namespace GooglePlayGames.Native
{
	public class NativeRealtimeMultiplayerClient : IRealTimeMultiplayerClient
	{
		private class NoopListener : RealTimeMultiplayerListener
		{
			public void OnRoomSetupProgress(float percent)
			{
			}

			public void OnRoomConnected(bool success)
			{
			}

			public void OnLeftRoom()
			{
			}

			public void OnParticipantLeft(Participant participant)
			{
			}

			public void OnPeersConnected(string[] participantIds)
			{
			}

			public void OnPeersDisconnected(string[] participantIds)
			{
			}

			public void OnRealTimeMessageReceived(bool isReliable, string senderId, byte[] data)
			{
			}
		}

		private class RoomSession
		{
			private readonly object mLifecycleLock = new object();

			private readonly OnGameThreadForwardingListener mListener;

			private readonly RealtimeManager mManager;

			private volatile string mCurrentPlayerId;

			private volatile State mState;

			private volatile bool mStillPreRoomCreation;

			private Invitation mInvitation;

			private volatile bool mShowingUI;

			private uint mMinPlayersToStart;

			internal bool ShowingUI
			{
				get
				{
					return mShowingUI;
				}
				set
				{
					mShowingUI = value;
				}
			}

			internal uint MinPlayersToStart
			{
				get
				{
					return mMinPlayersToStart;
				}
				set
				{
					mMinPlayersToStart = value;
				}
			}

			internal RoomSession(RealtimeManager manager, RealTimeMultiplayerListener listener)
			{
				mManager = Misc.CheckNotNull(manager);
				mListener = new OnGameThreadForwardingListener(listener);
				EnterState(new BeforeRoomCreateStartedState(this), false);
				mStillPreRoomCreation = true;
			}

			internal RealtimeManager Manager()
			{
				return mManager;
			}

			internal bool IsActive()
			{
				return mState.IsActive();
			}

			internal string SelfPlayerId()
			{
				return mCurrentPlayerId;
			}

			public void SetInvitation(Invitation invitation)
			{
				mInvitation = invitation;
			}

			public Invitation GetInvitation()
			{
				return mInvitation;
			}

			internal OnGameThreadForwardingListener OnGameThreadListener()
			{
				return mListener;
			}

			internal void EnterState(State handler)
			{
				EnterState(handler, true);
			}

			internal void EnterState(State handler, bool fireStateEnteredEvent)
			{
				lock (mLifecycleLock)
				{
					mState = Misc.CheckNotNull(handler);
					if (fireStateEnteredEvent)
					{
						Logger.d("Entering state: " + handler.GetType().Name);
						mState.OnStateEntered();
					}
				}
			}

			internal void LeaveRoom()
			{
				if (!ShowingUI)
				{
					lock (mLifecycleLock)
					{
						mState.LeaveRoom();
					}
				}
				else
				{
					Logger.d("Not leaving room since showing UI");
				}
			}

			internal void ShowWaitingRoomUI()
			{
				mState.ShowWaitingRoomUI(MinPlayersToStart);
			}

			internal void StartRoomCreation(string currentPlayerId, Action createRoom)
			{
				lock (mLifecycleLock)
				{
					if (!mStillPreRoomCreation)
					{
						Logger.e("Room creation started more than once, this shouldn't happen!");
					}
					else if (!mState.IsActive())
					{
						Logger.w("Received an attempt to create a room after the session was already torn down!");
					}
					else
					{
						mCurrentPlayerId = Misc.CheckNotNull(currentPlayerId);
						mStillPreRoomCreation = false;
						EnterState(new RoomCreationPendingState(this));
						createRoom.Invoke();
					}
				}
			}

			internal void OnRoomStatusChanged(NativeRealTimeRoom room)
			{
				lock (mLifecycleLock)
				{
					mState.OnRoomStatusChanged(room);
				}
			}

			internal void OnConnectedSetChanged(NativeRealTimeRoom room)
			{
				lock (mLifecycleLock)
				{
					mState.OnConnectedSetChanged(room);
				}
			}

			internal void OnParticipantStatusChanged(NativeRealTimeRoom room, GooglePlayGames.Native.PInvoke.MultiplayerParticipant participant)
			{
				lock (mLifecycleLock)
				{
					mState.OnParticipantStatusChanged(room, participant);
				}
			}

			internal void HandleRoomResponse(RealtimeManager.RealTimeRoomResponse response)
			{
				lock (mLifecycleLock)
				{
					mState.HandleRoomResponse(response);
				}
			}

			internal void OnDataReceived(NativeRealTimeRoom room, GooglePlayGames.Native.PInvoke.MultiplayerParticipant sender, byte[] data, bool isReliable)
			{
				mState.OnDataReceived(room, sender, data, isReliable);
			}

			internal void SendMessageToAll(bool reliable, byte[] data)
			{
				SendMessageToAll(reliable, data, 0, data.Length);
			}

			internal void SendMessageToAll(bool reliable, byte[] data, int offset, int length)
			{
				mState.SendToAll(data, offset, length, reliable);
			}

			internal void SendMessage(bool reliable, string participantId, byte[] data)
			{
				SendMessage(reliable, participantId, data, 0, data.Length);
			}

			internal void SendMessage(bool reliable, string participantId, byte[] data, int offset, int length)
			{
				mState.SendToSpecificRecipient(participantId, data, offset, length, reliable);
			}

			internal List<Participant> GetConnectedParticipants()
			{
				return mState.GetConnectedParticipants();
			}

			internal virtual Participant GetSelf()
			{
				return mState.GetSelf();
			}

			internal virtual Participant GetParticipant(string participantId)
			{
				return mState.GetParticipant(participantId);
			}

			internal virtual bool IsRoomConnected()
			{
				return mState.IsRoomConnected();
			}
		}

		private class OnGameThreadForwardingListener
		{
			private readonly RealTimeMultiplayerListener mListener;

			internal OnGameThreadForwardingListener(RealTimeMultiplayerListener listener)
			{
				mListener = Misc.CheckNotNull(listener);
			}

			public unsafe void RoomSetupProgress(float percent)
			{
				//IL_001b: Unknown result type (might be due to invalid IL or missing references)
				//IL_0020: Expected O, but got Unknown
				_003CRoomSetupProgress_003Ec__AnonStorey813 _003CRoomSetupProgress_003Ec__AnonStorey;
				PlayGamesHelperObject.RunOnGameThread(new Action((object)_003CRoomSetupProgress_003Ec__AnonStorey, (IntPtr)(void*)/*OpCode not supported: LdFtn*/));
			}

			public unsafe void RoomConnected(bool success)
			{
				//IL_001b: Unknown result type (might be due to invalid IL or missing references)
				//IL_0020: Expected O, but got Unknown
				_003CRoomConnected_003Ec__AnonStorey814 _003CRoomConnected_003Ec__AnonStorey;
				PlayGamesHelperObject.RunOnGameThread(new Action((object)_003CRoomConnected_003Ec__AnonStorey, (IntPtr)(void*)/*OpCode not supported: LdFtn*/));
			}

			public unsafe void LeftRoom()
			{
				//IL_0007: Unknown result type (might be due to invalid IL or missing references)
				//IL_000c: Expected O, but got Unknown
				PlayGamesHelperObject.RunOnGameThread(new Action((object)this, (IntPtr)(void*)/*OpCode not supported: LdFtn*/));
			}

			public unsafe void PeersConnected(string[] participantIds)
			{
				//IL_001b: Unknown result type (might be due to invalid IL or missing references)
				//IL_0020: Expected O, but got Unknown
				_003CPeersConnected_003Ec__AnonStorey815 _003CPeersConnected_003Ec__AnonStorey;
				PlayGamesHelperObject.RunOnGameThread(new Action((object)_003CPeersConnected_003Ec__AnonStorey, (IntPtr)(void*)/*OpCode not supported: LdFtn*/));
			}

			public unsafe void PeersDisconnected(string[] participantIds)
			{
				//IL_001b: Unknown result type (might be due to invalid IL or missing references)
				//IL_0020: Expected O, but got Unknown
				_003CPeersDisconnected_003Ec__AnonStorey816 _003CPeersDisconnected_003Ec__AnonStorey;
				PlayGamesHelperObject.RunOnGameThread(new Action((object)_003CPeersDisconnected_003Ec__AnonStorey, (IntPtr)(void*)/*OpCode not supported: LdFtn*/));
			}

			public unsafe void RealTimeMessageReceived(bool isReliable, string senderId, byte[] data)
			{
				//IL_0029: Unknown result type (might be due to invalid IL or missing references)
				//IL_002e: Expected O, but got Unknown
				_003CRealTimeMessageReceived_003Ec__AnonStorey817 _003CRealTimeMessageReceived_003Ec__AnonStorey;
				PlayGamesHelperObject.RunOnGameThread(new Action((object)_003CRealTimeMessageReceived_003Ec__AnonStorey, (IntPtr)(void*)/*OpCode not supported: LdFtn*/));
			}

			public unsafe void ParticipantLeft(Participant participant)
			{
				//IL_001b: Unknown result type (might be due to invalid IL or missing references)
				//IL_0020: Expected O, but got Unknown
				_003CParticipantLeft_003Ec__AnonStorey818 _003CParticipantLeft_003Ec__AnonStorey;
				PlayGamesHelperObject.RunOnGameThread(new Action((object)_003CParticipantLeft_003Ec__AnonStorey, (IntPtr)(void*)/*OpCode not supported: LdFtn*/));
			}
		}

		internal abstract class State
		{
			internal virtual void HandleRoomResponse(RealtimeManager.RealTimeRoomResponse response)
			{
				Logger.d(GetType().Name + ".HandleRoomResponse: Defaulting to no-op.");
			}

			internal virtual bool IsActive()
			{
				Logger.d(GetType().Name + ".IsNonPreemptable: Is preemptable by default.");
				return true;
			}

			internal virtual void LeaveRoom()
			{
				Logger.d(GetType().Name + ".LeaveRoom: Defaulting to no-op.");
			}

			internal virtual void ShowWaitingRoomUI(uint minimumParticipantsBeforeStarting)
			{
				Logger.d(GetType().Name + ".ShowWaitingRoomUI: Defaulting to no-op.");
			}

			internal virtual void OnStateEntered()
			{
				Logger.d(GetType().Name + ".OnStateEntered: Defaulting to no-op.");
			}

			internal virtual void OnRoomStatusChanged(NativeRealTimeRoom room)
			{
				Logger.d(GetType().Name + ".OnRoomStatusChanged: Defaulting to no-op.");
			}

			internal virtual void OnConnectedSetChanged(NativeRealTimeRoom room)
			{
				Logger.d(GetType().Name + ".OnConnectedSetChanged: Defaulting to no-op.");
			}

			internal virtual void OnParticipantStatusChanged(NativeRealTimeRoom room, GooglePlayGames.Native.PInvoke.MultiplayerParticipant participant)
			{
				Logger.d(GetType().Name + ".OnParticipantStatusChanged: Defaulting to no-op.");
			}

			internal virtual void OnDataReceived(NativeRealTimeRoom room, GooglePlayGames.Native.PInvoke.MultiplayerParticipant sender, byte[] data, bool isReliable)
			{
				Logger.d(GetType().Name + ".OnDataReceived: Defaulting to no-op.");
			}

			internal virtual void SendToSpecificRecipient(string recipientId, byte[] data, int offset, int length, bool isReliable)
			{
				Logger.d(GetType().Name + ".SendToSpecificRecipient: Defaulting to no-op.");
			}

			internal virtual void SendToAll(byte[] data, int offset, int length, bool isReliable)
			{
				Logger.d(GetType().Name + ".SendToApp: Defaulting to no-op.");
			}

			internal virtual List<Participant> GetConnectedParticipants()
			{
				Logger.d(GetType().Name + ".GetConnectedParticipants: Returning empty connected participants");
				return new List<Participant>();
			}

			internal virtual Participant GetSelf()
			{
				Logger.d(GetType().Name + ".GetSelf: Returning null self.");
				return null;
			}

			internal virtual Participant GetParticipant(string participantId)
			{
				Logger.d(GetType().Name + ".GetSelf: Returning null participant.");
				return null;
			}

			internal virtual bool IsRoomConnected()
			{
				Logger.d(GetType().Name + ".IsRoomConnected: Returning room not connected.");
				return false;
			}
		}

		private abstract class MessagingEnabledState : State
		{
			protected readonly RoomSession mSession;

			protected NativeRealTimeRoom mRoom;

			protected Dictionary<string, GooglePlayGames.Native.PInvoke.MultiplayerParticipant> mNativeParticipants;

			protected Dictionary<string, Participant> mParticipants;

			internal MessagingEnabledState(RoomSession session, NativeRealTimeRoom room)
			{
				mSession = Misc.CheckNotNull(session);
				UpdateCurrentRoom(room);
			}

			internal unsafe void UpdateCurrentRoom(NativeRealTimeRoom room)
			{
				if (mRoom != null)
				{
					mRoom.Dispose();
				}
				mRoom = Misc.CheckNotNull(room);
				IEnumerable<GooglePlayGames.Native.PInvoke.MultiplayerParticipant> source = mRoom.Participants();
				if (_003C_003Ef__am_0024cache4 == null)
				{
					_003C_003Ef__am_0024cache4 = new Func<GooglePlayGames.Native.PInvoke.MultiplayerParticipant, string>((object)null, (IntPtr)(void*)/*OpCode not supported: LdFtn*/);
				}
				mNativeParticipants = source.ToDictionary<GooglePlayGames.Native.PInvoke.MultiplayerParticipant, string>(_003C_003Ef__am_0024cache4);
				Dictionary<string, GooglePlayGames.Native.PInvoke.MultiplayerParticipant>.ValueCollection values = mNativeParticipants.Values;
				if (_003C_003Ef__am_0024cache5 == null)
				{
					_003C_003Ef__am_0024cache5 = new Func<GooglePlayGames.Native.PInvoke.MultiplayerParticipant, Participant>((object)null, (IntPtr)(void*)/*OpCode not supported: LdFtn*/);
				}
				IEnumerable<Participant> source2 = values.Select<GooglePlayGames.Native.PInvoke.MultiplayerParticipant, Participant>(_003C_003Ef__am_0024cache5);
				if (_003C_003Ef__am_0024cache6 == null)
				{
					_003C_003Ef__am_0024cache6 = new Func<Participant, string>((object)null, (IntPtr)(void*)/*OpCode not supported: LdFtn*/);
				}
				mParticipants = source2.ToDictionary<Participant, string>(_003C_003Ef__am_0024cache6);
			}

			internal sealed override void OnRoomStatusChanged(NativeRealTimeRoom room)
			{
				HandleRoomStatusChanged(room);
				UpdateCurrentRoom(room);
			}

			internal virtual void HandleRoomStatusChanged(NativeRealTimeRoom room)
			{
			}

			internal sealed override void OnConnectedSetChanged(NativeRealTimeRoom room)
			{
				HandleConnectedSetChanged(room);
				UpdateCurrentRoom(room);
			}

			internal virtual void HandleConnectedSetChanged(NativeRealTimeRoom room)
			{
			}

			internal sealed override void OnParticipantStatusChanged(NativeRealTimeRoom room, GooglePlayGames.Native.PInvoke.MultiplayerParticipant participant)
			{
				HandleParticipantStatusChanged(room, participant);
				UpdateCurrentRoom(room);
			}

			internal virtual void HandleParticipantStatusChanged(NativeRealTimeRoom room, GooglePlayGames.Native.PInvoke.MultiplayerParticipant participant)
			{
			}

			internal unsafe sealed override List<Participant> GetConnectedParticipants()
			{
				Dictionary<string, Participant>.ValueCollection values = mParticipants.Values;
				if (_003C_003Ef__am_0024cache7 == null)
				{
					_003C_003Ef__am_0024cache7 = new Func<Participant, bool>((object)null, (IntPtr)(void*)/*OpCode not supported: LdFtn*/);
				}
				List<Participant> list = values.Where(_003C_003Ef__am_0024cache7).ToList();
				list.Sort();
				return list;
			}

			internal override void SendToSpecificRecipient(string recipientId, byte[] data, int offset, int length, bool isReliable)
			{
				if (!mNativeParticipants.ContainsKey(recipientId))
				{
					Logger.e("Attempted to send message to unknown participant " + recipientId);
				}
				else if (isReliable)
				{
					mSession.Manager().SendReliableMessage(mRoom, mNativeParticipants[recipientId], Misc.GetSubsetBytes(data, offset, length), null);
				}
				else
				{
					mSession.Manager().SendUnreliableMessageToSpecificParticipants(mRoom, new List<GooglePlayGames.Native.PInvoke.MultiplayerParticipant>
					{
						mNativeParticipants[recipientId]
					}, Misc.GetSubsetBytes(data, offset, length));
				}
			}

			internal override void SendToAll(byte[] data, int offset, int length, bool isReliable)
			{
				byte[] subsetBytes = Misc.GetSubsetBytes(data, offset, length);
				if (isReliable)
				{
					foreach (string key in mNativeParticipants.Keys)
					{
						SendToSpecificRecipient(key, subsetBytes, 0, subsetBytes.Length, true);
					}
				}
				else
				{
					mSession.Manager().SendUnreliableMessageToAll(mRoom, subsetBytes);
				}
			}

			internal override void OnDataReceived(NativeRealTimeRoom room, GooglePlayGames.Native.PInvoke.MultiplayerParticipant sender, byte[] data, bool isReliable)
			{
				mSession.OnGameThreadListener().RealTimeMessageReceived(isReliable, sender.Id(), data);
			}
		}

		private class BeforeRoomCreateStartedState : State
		{
			private readonly RoomSession mContainingSession;

			internal BeforeRoomCreateStartedState(RoomSession session)
			{
				mContainingSession = Misc.CheckNotNull(session);
			}

			internal override void LeaveRoom()
			{
				Logger.d("Session was torn down before room was created.");
				mContainingSession.OnGameThreadListener().RoomConnected(false);
				mContainingSession.EnterState(new ShutdownState(mContainingSession));
			}
		}

		private class RoomCreationPendingState : State
		{
			private readonly RoomSession mContainingSession;

			internal RoomCreationPendingState(RoomSession session)
			{
				mContainingSession = Misc.CheckNotNull(session);
			}

			internal override void HandleRoomResponse(RealtimeManager.RealTimeRoomResponse response)
			{
				if (!response.RequestSucceeded())
				{
					mContainingSession.EnterState(new ShutdownState(mContainingSession));
					mContainingSession.OnGameThreadListener().RoomConnected(false);
				}
				else
				{
					mContainingSession.EnterState(new ConnectingState(response.Room(), mContainingSession));
				}
			}

			internal override bool IsActive()
			{
				return true;
			}

			internal override void LeaveRoom()
			{
				Logger.d("Received request to leave room during room creation, aborting creation.");
				mContainingSession.EnterState(new AbortingRoomCreationState(mContainingSession));
			}
		}

		private class ConnectingState : MessagingEnabledState
		{
			private const float InitialPercentComplete = 20f;

			private static readonly HashSet<Types.ParticipantStatus> FailedStatuses = new HashSet<Types.ParticipantStatus>
			{
				Types.ParticipantStatus.DECLINED,
				Types.ParticipantStatus.LEFT
			};

			private HashSet<string> mConnectedParticipants = new HashSet<string>();

			private float mPercentComplete = 20f;

			private float mPercentPerParticipant;

			internal ConnectingState(NativeRealTimeRoom room, RoomSession session)
				: base(session, room)
			{
				mPercentPerParticipant = 80f / (float)(double)session.MinPlayersToStart;
			}

			internal override void OnStateEntered()
			{
				mSession.OnGameThreadListener().RoomSetupProgress(mPercentComplete);
			}

			internal override void HandleConnectedSetChanged(NativeRealTimeRoom room)
			{
				HashSet<string> hashSet = new HashSet<string>();
				if ((room.Status() == Types.RealTimeRoomStatus.AUTO_MATCHING || room.Status() == Types.RealTimeRoomStatus.CONNECTING) && mSession.MinPlayersToStart <= room.ParticipantCount())
				{
					mSession.MinPlayersToStart += room.ParticipantCount();
					mPercentPerParticipant = 80f / (float)(double)mSession.MinPlayersToStart;
				}
				foreach (GooglePlayGames.Native.PInvoke.MultiplayerParticipant item in room.Participants())
				{
					using (item)
					{
						if (item.IsConnectedToRoom())
						{
							hashSet.Add(item.Id());
						}
					}
				}
				if (mConnectedParticipants.Equals(hashSet))
				{
					Logger.w("Received connected set callback with unchanged connected set!");
				}
				else
				{
					IEnumerable<string> source = mConnectedParticipants.Except(hashSet);
					if (room.Status() == Types.RealTimeRoomStatus.DELETED)
					{
						Logger.e("Participants disconnected during room setup, failing. Participants were: " + string.Join(",", source.ToArray()));
						mSession.OnGameThreadListener().RoomConnected(false);
						mSession.EnterState(new ShutdownState(mSession));
					}
					else
					{
						IEnumerable<string> source2 = hashSet.Except(mConnectedParticipants);
						Logger.d("New participants connected: " + string.Join(",", source2.ToArray()));
						if (room.Status() == Types.RealTimeRoomStatus.ACTIVE)
						{
							Logger.d("Fully connected! Transitioning to active state.");
							mSession.EnterState(new ActiveState(room, mSession));
							mSession.OnGameThreadListener().RoomConnected(true);
						}
						else
						{
							mPercentComplete += mPercentPerParticipant * (float)source2.Count();
							mConnectedParticipants = hashSet;
							mSession.OnGameThreadListener().RoomSetupProgress(mPercentComplete);
						}
					}
				}
			}

			internal override void HandleParticipantStatusChanged(NativeRealTimeRoom room, GooglePlayGames.Native.PInvoke.MultiplayerParticipant participant)
			{
				if (FailedStatuses.Contains(participant.Status()))
				{
					mSession.OnGameThreadListener().ParticipantLeft(participant.AsParticipant());
					if (room.Status() != Types.RealTimeRoomStatus.CONNECTING && room.Status() != Types.RealTimeRoomStatus.AUTO_MATCHING)
					{
						LeaveRoom();
					}
				}
			}

			internal unsafe override void LeaveRoom()
			{
				//IL_0019: Unknown result type (might be due to invalid IL or missing references)
				//IL_001e: Expected O, but got Unknown
				mSession.EnterState(new LeavingRoom(mSession, mRoom, new Action((object)this, (IntPtr)(void*)/*OpCode not supported: LdFtn*/)));
			}

			internal override void ShowWaitingRoomUI(uint minimumParticipantsBeforeStarting)
			{
				mSession.ShowingUI = true;
				mSession.Manager().ShowWaitingRoomUI(mRoom, minimumParticipantsBeforeStarting, delegate(RealtimeManager.WaitingRoomUIResponse response)
				{
					mSession.ShowingUI = false;
					Logger.d("ShowWaitingRoomUI Response: " + response.ResponseStatus());
					if (response.ResponseStatus() == CommonErrorStatus.UIStatus.VALID)
					{
						Logger.d("Connecting state ShowWaitingRoomUI: room pcount:" + response.Room().ParticipantCount() + " status: " + response.Room().Status());
						if (response.Room().Status() == Types.RealTimeRoomStatus.ACTIVE)
						{
							mSession.EnterState(new ActiveState(response.Room(), mSession));
						}
					}
					else if (response.ResponseStatus() == CommonErrorStatus.UIStatus.ERROR_LEFT_ROOM)
					{
						LeaveRoom();
					}
					else
					{
						mSession.OnGameThreadListener().RoomSetupProgress(mPercentComplete);
					}
				});
			}
		}

		private class ActiveState : MessagingEnabledState
		{
			internal ActiveState(NativeRealTimeRoom room, RoomSession session)
				: base(session, room)
			{
			}

			internal override void OnStateEntered()
			{
				if (GetSelf() == null)
				{
					Logger.e("Room reached active state with unknown participant for the player");
					LeaveRoom();
				}
			}

			internal override bool IsRoomConnected()
			{
				return true;
			}

			internal override Participant GetParticipant(string participantId)
			{
				if (!mParticipants.ContainsKey(participantId))
				{
					Logger.e("Attempted to retrieve unknown participant " + participantId);
					return null;
				}
				return mParticipants[participantId];
			}

			internal override Participant GetSelf()
			{
				foreach (Participant value in mParticipants.Values)
				{
					if (value.Player != null && value.Player.id.Equals(mSession.SelfPlayerId()))
					{
						return value;
					}
				}
				return null;
			}

			internal unsafe override void HandleConnectedSetChanged(NativeRealTimeRoom room)
			{
				List<string> list = new List<string>();
				List<string> list2 = new List<string>();
				IEnumerable<GooglePlayGames.Native.PInvoke.MultiplayerParticipant> source = room.Participants();
				if (_003C_003Ef__am_0024cache0 == null)
				{
					_003C_003Ef__am_0024cache0 = new Func<GooglePlayGames.Native.PInvoke.MultiplayerParticipant, string>((object)null, (IntPtr)(void*)/*OpCode not supported: LdFtn*/);
				}
				Dictionary<string, GooglePlayGames.Native.PInvoke.MultiplayerParticipant> dictionary = source.ToDictionary<GooglePlayGames.Native.PInvoke.MultiplayerParticipant, string>(_003C_003Ef__am_0024cache0);
				foreach (string key in mNativeParticipants.Keys)
				{
					GooglePlayGames.Native.PInvoke.MultiplayerParticipant multiplayerParticipant = dictionary[key];
					GooglePlayGames.Native.PInvoke.MultiplayerParticipant multiplayerParticipant2 = mNativeParticipants[key];
					if (!multiplayerParticipant.IsConnectedToRoom())
					{
						list2.Add(key);
					}
					if (!multiplayerParticipant2.IsConnectedToRoom() && multiplayerParticipant.IsConnectedToRoom())
					{
						list.Add(key);
					}
				}
				foreach (GooglePlayGames.Native.PInvoke.MultiplayerParticipant value in mNativeParticipants.Values)
				{
					value.Dispose();
				}
				mNativeParticipants = dictionary;
				Dictionary<string, GooglePlayGames.Native.PInvoke.MultiplayerParticipant>.ValueCollection values = mNativeParticipants.Values;
				if (_003C_003Ef__am_0024cache1 == null)
				{
					_003C_003Ef__am_0024cache1 = new Func<GooglePlayGames.Native.PInvoke.MultiplayerParticipant, Participant>((object)null, (IntPtr)(void*)/*OpCode not supported: LdFtn*/);
				}
				IEnumerable<Participant> source2 = values.Select<GooglePlayGames.Native.PInvoke.MultiplayerParticipant, Participant>(_003C_003Ef__am_0024cache1);
				if (_003C_003Ef__am_0024cache2 == null)
				{
					_003C_003Ef__am_0024cache2 = new Func<Participant, string>((object)null, (IntPtr)(void*)/*OpCode not supported: LdFtn*/);
				}
				mParticipants = source2.ToDictionary<Participant, string>(_003C_003Ef__am_0024cache2);
				Dictionary<string, Participant>.ValueCollection values2 = mParticipants.Values;
				if (_003C_003Ef__am_0024cache3 == null)
				{
					_003C_003Ef__am_0024cache3 = new Func<Participant, string>((object)null, (IntPtr)(void*)/*OpCode not supported: LdFtn*/);
				}
				Logger.d("Updated participant statuses: " + string.Join(",", values2.Select<Participant, string>(_003C_003Ef__am_0024cache3).ToArray()));
				if (list2.Contains(GetSelf().ParticipantId))
				{
					Logger.w("Player was disconnected from the multiplayer session.");
				}
				string selfId = GetSelf().ParticipantId;
				_003CHandleConnectedSetChanged_003Ec__AnonStorey819 _003CHandleConnectedSetChanged_003Ec__AnonStorey;
				list = list.Where(new Func<string, bool>((object)_003CHandleConnectedSetChanged_003Ec__AnonStorey, (IntPtr)(void*)/*OpCode not supported: LdFtn*/)).ToList();
				list2 = list2.Where(new Func<string, bool>((object)_003CHandleConnectedSetChanged_003Ec__AnonStorey, (IntPtr)(void*)/*OpCode not supported: LdFtn*/)).ToList();
				if (list.Count > 0)
				{
					list.Sort();
					mSession.OnGameThreadListener().PeersConnected(list.Where(new Func<string, bool>((object)_003CHandleConnectedSetChanged_003Ec__AnonStorey, (IntPtr)(void*)/*OpCode not supported: LdFtn*/)).ToArray());
				}
				if (list2.Count > 0)
				{
					list2.Sort();
					mSession.OnGameThreadListener().PeersDisconnected(list2.Where(new Func<string, bool>((object)_003CHandleConnectedSetChanged_003Ec__AnonStorey, (IntPtr)(void*)/*OpCode not supported: LdFtn*/)).ToArray());
				}
			}

			internal unsafe override void LeaveRoom()
			{
				//IL_0019: Unknown result type (might be due to invalid IL or missing references)
				//IL_001e: Expected O, but got Unknown
				mSession.EnterState(new LeavingRoom(mSession, mRoom, new Action((object)this, (IntPtr)(void*)/*OpCode not supported: LdFtn*/)));
			}
		}

		private class ShutdownState : State
		{
			private readonly RoomSession mSession;

			internal ShutdownState(RoomSession session)
			{
				mSession = Misc.CheckNotNull(session);
			}

			internal override bool IsActive()
			{
				return false;
			}

			internal override void LeaveRoom()
			{
				mSession.OnGameThreadListener().LeftRoom();
			}
		}

		private class LeavingRoom : State
		{
			private readonly RoomSession mSession;

			private readonly NativeRealTimeRoom mRoomToLeave;

			private readonly Action mLeavingCompleteCallback;

			internal LeavingRoom(RoomSession session, NativeRealTimeRoom room, Action leavingCompleteCallback)
			{
				mSession = Misc.CheckNotNull(session);
				mRoomToLeave = Misc.CheckNotNull(room);
				mLeavingCompleteCallback = Misc.CheckNotNull<Action>(leavingCompleteCallback);
			}

			internal override bool IsActive()
			{
				return false;
			}

			internal override void OnStateEntered()
			{
				mSession.Manager().LeaveRoom(mRoomToLeave, delegate
				{
					mLeavingCompleteCallback.Invoke();
					mSession.EnterState(new ShutdownState(mSession));
				});
			}
		}

		private class AbortingRoomCreationState : State
		{
			private readonly RoomSession mSession;

			internal AbortingRoomCreationState(RoomSession session)
			{
				mSession = Misc.CheckNotNull(session);
			}

			internal override bool IsActive()
			{
				return false;
			}

			internal unsafe override void HandleRoomResponse(RealtimeManager.RealTimeRoomResponse response)
			{
				//IL_004c: Unknown result type (might be due to invalid IL or missing references)
				//IL_0051: Expected O, but got Unknown
				if (!response.RequestSucceeded())
				{
					mSession.EnterState(new ShutdownState(mSession));
					mSession.OnGameThreadListener().RoomConnected(false);
				}
				else
				{
					mSession.EnterState(new LeavingRoom(mSession, response.Room(), new Action((object)this, (IntPtr)(void*)/*OpCode not supported: LdFtn*/)));
				}
			}
		}

		private readonly object mSessionLock = new object();

		private readonly NativeClient mNativeClient;

		private readonly RealtimeManager mRealtimeManager;

		private volatile RoomSession mCurrentSession;

		internal NativeRealtimeMultiplayerClient(NativeClient nativeClient, RealtimeManager manager)
		{
			mNativeClient = Misc.CheckNotNull(nativeClient);
			mRealtimeManager = Misc.CheckNotNull(manager);
			mCurrentSession = GetTerminatedSession();
			PlayGamesHelperObject.AddPauseCallback(HandleAppPausing);
		}

		private RoomSession GetTerminatedSession()
		{
			RoomSession roomSession = new RoomSession(mRealtimeManager, new NoopListener());
			roomSession.EnterState(new ShutdownState(roomSession), false);
			return roomSession;
		}

		public void CreateQuickGame(uint minOpponents, uint maxOpponents, uint variant, RealTimeMultiplayerListener listener)
		{
			CreateQuickGame(minOpponents, maxOpponents, variant, 0uL, listener);
		}

		public unsafe void CreateQuickGame(uint minOpponents, uint maxOpponents, uint variant, ulong exclusiveBitMask, RealTimeMultiplayerListener listener)
		{
			//IL_0118: Unknown result type (might be due to invalid IL or missing references)
			//IL_011d: Expected O, but got Unknown
			lock (mSessionLock)
			{
				RoomSession newSession = new RoomSession(mRealtimeManager, listener);
				if (mCurrentSession.IsActive())
				{
					Logger.e("Received attempt to create a new room without cleaning up the old one.");
					newSession.LeaveRoom();
				}
				else
				{
					mCurrentSession = newSession;
					Logger.d("QuickGame: Setting MinPlayersToStart = " + minOpponents);
					mCurrentSession.MinPlayersToStart = minOpponents;
					using (RealtimeRoomConfigBuilder realtimeRoomConfigBuilder = RealtimeRoomConfigBuilder.Create())
					{
						RealtimeRoomConfig config = realtimeRoomConfigBuilder.SetMinimumAutomatchingPlayers(minOpponents).SetMaximumAutomatchingPlayers(maxOpponents).SetVariant(variant)
							.SetExclusiveBitMask(exclusiveBitMask)
							.Build();
						using (config)
						{
							GooglePlayGames.Native.PInvoke.RealTimeEventListenerHelper helper = HelperForSession(newSession);
							try
							{
								_003CCreateQuickGame_003Ec__AnonStorey803 _003CCreateQuickGame_003Ec__AnonStorey;
								newSession.StartRoomCreation(mNativeClient.GetUserId(), new Action((object)_003CCreateQuickGame_003Ec__AnonStorey, (IntPtr)(void*)/*OpCode not supported: LdFtn*/));
							}
							finally
							{
								if (helper != null)
								{
									((IDisposable)helper).Dispose();
								}
							}
						}
					}
				}
			}
		}

		private unsafe static GooglePlayGames.Native.PInvoke.RealTimeEventListenerHelper HelperForSession(RoomSession session)
		{
			_003CHelperForSession_003Ec__AnonStorey805 _003CHelperForSession_003Ec__AnonStorey;
			return GooglePlayGames.Native.PInvoke.RealTimeEventListenerHelper.Create().SetOnDataReceivedCallback(new Action<NativeRealTimeRoom, GooglePlayGames.Native.PInvoke.MultiplayerParticipant, byte[], bool>((object)_003CHelperForSession_003Ec__AnonStorey, (IntPtr)(void*)/*OpCode not supported: LdFtn*/)).SetOnParticipantStatusChangedCallback(new Action<NativeRealTimeRoom, GooglePlayGames.Native.PInvoke.MultiplayerParticipant>((object)_003CHelperForSession_003Ec__AnonStorey, (IntPtr)(void*)/*OpCode not supported: LdFtn*/))
				.SetOnRoomConnectedSetChangedCallback(delegate(NativeRealTimeRoom room)
				{
					session.OnConnectedSetChanged(room);
				})
				.SetOnRoomStatusChangedCallback(delegate(NativeRealTimeRoom room)
				{
					session.OnRoomStatusChanged(room);
				});
		}

		private void HandleAppPausing(bool paused)
		{
			if (paused)
			{
				Logger.d("Application is pausing, which disconnects the RTMP  client.  Leaving room.");
				LeaveRoom();
			}
		}

		public unsafe void CreateWithInvitationScreen(uint minOpponents, uint maxOppponents, uint variant, RealTimeMultiplayerListener listener)
		{
			lock (mSessionLock)
			{
				RoomSession newRoom = new RoomSession(mRealtimeManager, listener);
				if (mCurrentSession.IsActive())
				{
					Logger.e("Received attempt to create a new room without cleaning up the old one.");
					newRoom.LeaveRoom();
				}
				else
				{
					mCurrentSession = newRoom;
					mCurrentSession.ShowingUI = true;
					RealtimeRoomConfig config;
					GooglePlayGames.Native.PInvoke.RealTimeEventListenerHelper helper;
					_003CCreateWithInvitationScreen_003Ec__AnonStorey806._003CCreateWithInvitationScreen_003Ec__AnonStorey809 _003CCreateWithInvitationScreen_003Ec__AnonStorey;
					mRealtimeManager.ShowPlayerSelectUI(minOpponents, maxOppponents, true, delegate(PlayerSelectUIResponse response)
					{
						//IL_00d1: Unknown result type (might be due to invalid IL or missing references)
						//IL_00d6: Expected O, but got Unknown
						mCurrentSession.ShowingUI = false;
						if (response.Status() != CommonErrorStatus.UIStatus.VALID)
						{
							Logger.d("User did not complete invitation screen.");
							newRoom.LeaveRoom();
						}
						else
						{
							mCurrentSession.MinPlayersToStart = (uint)((int)response.MinimumAutomatchingPlayers() + response.Count() + 1);
							using (RealtimeRoomConfigBuilder realtimeRoomConfigBuilder = RealtimeRoomConfigBuilder.Create())
							{
								realtimeRoomConfigBuilder.SetVariant(variant);
								realtimeRoomConfigBuilder.PopulateFromUIResponse(response);
								config = realtimeRoomConfigBuilder.Build();
								try
								{
									helper = HelperForSession(newRoom);
									try
									{
										newRoom.StartRoomCreation(mNativeClient.GetUserId(), new Action((object)_003CCreateWithInvitationScreen_003Ec__AnonStorey, (IntPtr)(void*)/*OpCode not supported: LdFtn*/));
									}
									finally
									{
										if (helper != null)
										{
											((IDisposable)helper).Dispose();
										}
									}
								}
								finally
								{
									if (config != null)
									{
										((IDisposable)config).Dispose();
									}
								}
							}
						}
					});
				}
			}
		}

		public void ShowWaitingRoomUI()
		{
			lock (mSessionLock)
			{
				mCurrentSession.ShowWaitingRoomUI();
			}
		}

		public void GetAllInvitations(Action<Invitation[]> callback)
		{
			mRealtimeManager.FetchInvitations(delegate(RealtimeManager.FetchInvitationsResponse response)
			{
				if (!response.RequestSucceeded())
				{
					Logger.e("Couldn't load invitations.");
					callback(new Invitation[0]);
				}
				else
				{
					List<Invitation> list = new List<Invitation>();
					foreach (GooglePlayGames.Native.PInvoke.MultiplayerInvitation item in response.Invitations())
					{
						using (item)
						{
							list.Add(item.AsInvitation());
						}
					}
					callback(list.ToArray());
				}
			});
		}

		public unsafe void AcceptFromInbox(RealTimeMultiplayerListener listener)
		{
			lock (mSessionLock)
			{
				RoomSession newRoom = new RoomSession(mRealtimeManager, listener);
				if (mCurrentSession.IsActive())
				{
					Logger.e("Received attempt to accept invitation without cleaning up active session.");
					newRoom.LeaveRoom();
				}
				else
				{
					mCurrentSession = newRoom;
					mCurrentSession.ShowingUI = true;
					mRealtimeManager.ShowRoomInboxUI(delegate(RealtimeManager.RoomInboxUIResponse response)
					{
						//IL_00aa: Unknown result type (might be due to invalid IL or missing references)
						//IL_00af: Expected O, but got Unknown
						mCurrentSession.ShowingUI = false;
						if (response.ResponseStatus() != CommonErrorStatus.UIStatus.VALID)
						{
							Logger.d("User did not complete invitation screen.");
							newRoom.LeaveRoom();
						}
						else
						{
							GooglePlayGames.Native.PInvoke.MultiplayerInvitation invitation = response.Invitation();
							GooglePlayGames.Native.PInvoke.RealTimeEventListenerHelper helper = HelperForSession(newRoom);
							try
							{
								Logger.d("About to accept invitation " + invitation.Id());
								_003CAcceptFromInbox_003Ec__AnonStorey80B._003CAcceptFromInbox_003Ec__AnonStorey80D _003CAcceptFromInbox_003Ec__AnonStorey80D;
								newRoom.StartRoomCreation(mNativeClient.GetUserId(), new Action((object)_003CAcceptFromInbox_003Ec__AnonStorey80D, (IntPtr)(void*)/*OpCode not supported: LdFtn*/));
							}
							finally
							{
								if (helper != null)
								{
									((IDisposable)helper).Dispose();
								}
							}
						}
					});
				}
			}
		}

		public unsafe void AcceptInvitation(string invitationId, RealTimeMultiplayerListener listener)
		{
			lock (mSessionLock)
			{
				RoomSession newRoom = new RoomSession(mRealtimeManager, listener);
				if (mCurrentSession.IsActive())
				{
					Logger.e("Received attempt to accept invitation without cleaning up active session.");
					newRoom.LeaveRoom();
				}
				else
				{
					mCurrentSession = newRoom;
					GooglePlayGames.Native.PInvoke.RealTimeEventListenerHelper helper;
					_003CAcceptInvitation_003Ec__AnonStorey80E._003CAcceptInvitation_003Ec__AnonStorey811 _003CAcceptInvitation_003Ec__AnonStorey;
					mRealtimeManager.FetchInvitations(delegate(RealtimeManager.FetchInvitationsResponse response)
					{
						//IL_010f: Unknown result type (might be due to invalid IL or missing references)
						//IL_0114: Expected O, but got Unknown
						if (!response.RequestSucceeded())
						{
							Logger.e("Couldn't load invitations.");
							newRoom.LeaveRoom();
						}
						else
						{
							using (IEnumerator<GooglePlayGames.Native.PInvoke.MultiplayerInvitation> enumerator = response.Invitations().GetEnumerator())
							{
								GooglePlayGames.Native.PInvoke.MultiplayerInvitation invitation;
								while (enumerator.MoveNext())
								{
									invitation = enumerator.Current;
									using (invitation)
									{
										if (invitation.Id().Equals(invitationId))
										{
											mCurrentSession.MinPlayersToStart = invitation.AutomatchingSlots() + invitation.ParticipantCount();
											Logger.d("Setting MinPlayersToStart with invitation to : " + mCurrentSession.MinPlayersToStart);
											helper = HelperForSession(newRoom);
											try
											{
												newRoom.StartRoomCreation(mNativeClient.GetUserId(), new Action((object)_003CAcceptInvitation_003Ec__AnonStorey, (IntPtr)(void*)/*OpCode not supported: LdFtn*/));
												return;
												IL_011e:;
											}
											finally
											{
												if (helper != null)
												{
													((IDisposable)helper).Dispose();
												}
											}
										}
									}
								}
							}
							Logger.e("Room creation failed since we could not find invitation with ID " + invitationId);
							newRoom.LeaveRoom();
						}
					});
				}
			}
		}

		public Invitation GetInvitation()
		{
			return mCurrentSession.GetInvitation();
		}

		public void LeaveRoom()
		{
			mCurrentSession.LeaveRoom();
		}

		public void SendMessageToAll(bool reliable, byte[] data)
		{
			mCurrentSession.SendMessageToAll(reliable, data);
		}

		public void SendMessageToAll(bool reliable, byte[] data, int offset, int length)
		{
			mCurrentSession.SendMessageToAll(reliable, data, offset, length);
		}

		public void SendMessage(bool reliable, string participantId, byte[] data)
		{
			mCurrentSession.SendMessage(reliable, participantId, data);
		}

		public void SendMessage(bool reliable, string participantId, byte[] data, int offset, int length)
		{
			mCurrentSession.SendMessage(reliable, participantId, data, offset, length);
		}

		public List<Participant> GetConnectedParticipants()
		{
			return mCurrentSession.GetConnectedParticipants();
		}

		public Participant GetSelf()
		{
			return mCurrentSession.GetSelf();
		}

		public Participant GetParticipant(string participantId)
		{
			return mCurrentSession.GetParticipant(participantId);
		}

		public bool IsRoomConnected()
		{
			return mCurrentSession.IsRoomConnected();
		}

		public void DeclineInvitation(string invitationId)
		{
			mRealtimeManager.FetchInvitations(delegate(RealtimeManager.FetchInvitationsResponse response)
			{
				if (!response.RequestSucceeded())
				{
					Logger.e("Couldn't load invitations.");
				}
				else
				{
					foreach (GooglePlayGames.Native.PInvoke.MultiplayerInvitation item in response.Invitations())
					{
						using (item)
						{
							if (item.Id().Equals(invitationId))
							{
								mRealtimeManager.DeclineInvitation(item);
							}
						}
					}
				}
			});
		}

		private static T WithDefault<T>(T presented, T defaultValue) where T : class
		{
			return (presented == null) ? defaultValue : presented;
		}
	}
}
