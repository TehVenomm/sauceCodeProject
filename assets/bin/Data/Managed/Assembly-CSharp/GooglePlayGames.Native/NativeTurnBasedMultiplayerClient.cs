using GooglePlayGames.BasicApi;
using GooglePlayGames.BasicApi.Multiplayer;
using GooglePlayGames.Native.Cwrapper;
using GooglePlayGames.Native.PInvoke;
using GooglePlayGames.OurUtils;
using System;
using System.Collections;

namespace GooglePlayGames.Native
{
	public class NativeTurnBasedMultiplayerClient : ITurnBasedMultiplayerClient
	{
		private readonly TurnBasedManager mTurnBasedManager;

		private readonly NativeClient mNativeClient;

		private volatile Action<GooglePlayGames.BasicApi.Multiplayer.TurnBasedMatch, bool> mMatchDelegate;

		internal NativeTurnBasedMultiplayerClient(NativeClient nativeClient, TurnBasedManager manager)
		{
			mTurnBasedManager = manager;
			mNativeClient = nativeClient;
		}

		public void CreateQuickMatch(uint minOpponents, uint maxOpponents, uint variant, Action<bool, GooglePlayGames.BasicApi.Multiplayer.TurnBasedMatch> callback)
		{
			CreateQuickMatch(minOpponents, maxOpponents, variant, 0uL, callback);
		}

		public unsafe void CreateQuickMatch(uint minOpponents, uint maxOpponents, uint variant, ulong exclusiveBitmask, Action<bool, GooglePlayGames.BasicApi.Multiplayer.TurnBasedMatch> callback)
		{
			callback = Callbacks.AsOnGameThreadCallback<bool, GooglePlayGames.BasicApi.Multiplayer.TurnBasedMatch>(callback);
			using (GooglePlayGames.Native.PInvoke.TurnBasedMatchConfigBuilder turnBasedMatchConfigBuilder = GooglePlayGames.Native.PInvoke.TurnBasedMatchConfigBuilder.Create())
			{
				turnBasedMatchConfigBuilder.SetVariant(variant).SetMinimumAutomatchingPlayers(minOpponents).SetMaximumAutomatchingPlayers(maxOpponents)
					.SetExclusiveBitMask(exclusiveBitmask);
				using (GooglePlayGames.Native.PInvoke.TurnBasedMatchConfig config = turnBasedMatchConfigBuilder.Build())
				{
					_003CCreateQuickMatch_003Ec__AnonStorey830 _003CCreateQuickMatch_003Ec__AnonStorey;
					mTurnBasedManager.CreateMatch(config, BridgeMatchToUserCallback(new Action<UIStatus, GooglePlayGames.BasicApi.Multiplayer.TurnBasedMatch>((object)_003CCreateQuickMatch_003Ec__AnonStorey, (IntPtr)(void*)/*OpCode not supported: LdFtn*/)));
				}
			}
		}

		public unsafe void CreateWithInvitationScreen(uint minOpponents, uint maxOpponents, uint variant, Action<bool, GooglePlayGames.BasicApi.Multiplayer.TurnBasedMatch> callback)
		{
			_003CCreateWithInvitationScreen_003Ec__AnonStorey831 _003CCreateWithInvitationScreen_003Ec__AnonStorey;
			CreateWithInvitationScreen(minOpponents, maxOpponents, variant, new Action<UIStatus, GooglePlayGames.BasicApi.Multiplayer.TurnBasedMatch>((object)_003CCreateWithInvitationScreen_003Ec__AnonStorey, (IntPtr)(void*)/*OpCode not supported: LdFtn*/));
		}

		public void CreateWithInvitationScreen(uint minOpponents, uint maxOpponents, uint variant, Action<UIStatus, GooglePlayGames.BasicApi.Multiplayer.TurnBasedMatch> callback)
		{
			callback = Callbacks.AsOnGameThreadCallback<UIStatus, GooglePlayGames.BasicApi.Multiplayer.TurnBasedMatch>(callback);
			mTurnBasedManager.ShowPlayerSelectUI(minOpponents, maxOpponents, true, delegate(PlayerSelectUIResponse result)
			{
				if (result.Status() != CommonErrorStatus.UIStatus.VALID)
				{
					callback.Invoke((UIStatus)result.Status(), (GooglePlayGames.BasicApi.Multiplayer.TurnBasedMatch)null);
				}
				else
				{
					using (GooglePlayGames.Native.PInvoke.TurnBasedMatchConfigBuilder turnBasedMatchConfigBuilder = GooglePlayGames.Native.PInvoke.TurnBasedMatchConfigBuilder.Create())
					{
						turnBasedMatchConfigBuilder.PopulateFromUIResponse(result).SetVariant(variant);
						using (GooglePlayGames.Native.PInvoke.TurnBasedMatchConfig config = turnBasedMatchConfigBuilder.Build())
						{
							mTurnBasedManager.CreateMatch(config, BridgeMatchToUserCallback(callback));
						}
					}
				}
			});
		}

		public void GetAllInvitations(Action<Invitation[]> callback)
		{
			mTurnBasedManager.GetAllTurnbasedMatches(delegate(TurnBasedManager.TurnBasedMatchesResponse allMatches)
			{
				Invitation[] array = new Invitation[allMatches.InvitationCount()];
				int num = 0;
				foreach (GooglePlayGames.Native.PInvoke.MultiplayerInvitation item in allMatches.Invitations())
				{
					array[num++] = item.AsInvitation();
				}
				callback(array);
			});
		}

		public void GetAllMatches(Action<GooglePlayGames.BasicApi.Multiplayer.TurnBasedMatch[]> callback)
		{
			mTurnBasedManager.GetAllTurnbasedMatches(delegate(TurnBasedManager.TurnBasedMatchesResponse allMatches)
			{
				int num = allMatches.MyTurnMatchesCount() + allMatches.TheirTurnMatchesCount() + allMatches.CompletedMatchesCount();
				GooglePlayGames.BasicApi.Multiplayer.TurnBasedMatch[] array = new GooglePlayGames.BasicApi.Multiplayer.TurnBasedMatch[num];
				int num2 = 0;
				foreach (NativeTurnBasedMatch item in allMatches.MyTurnMatches())
				{
					array[num2++] = item.AsTurnBasedMatch(mNativeClient.GetUserId());
				}
				foreach (NativeTurnBasedMatch item2 in allMatches.TheirTurnMatches())
				{
					array[num2++] = item2.AsTurnBasedMatch(mNativeClient.GetUserId());
				}
				foreach (NativeTurnBasedMatch item3 in allMatches.CompletedMatches())
				{
					array[num2++] = item3.AsTurnBasedMatch(mNativeClient.GetUserId());
				}
				callback(array);
			});
		}

		private Action<TurnBasedManager.TurnBasedMatchResponse> BridgeMatchToUserCallback(Action<UIStatus, GooglePlayGames.BasicApi.Multiplayer.TurnBasedMatch> userCallback)
		{
			return delegate(TurnBasedManager.TurnBasedMatchResponse callbackResult)
			{
				using (NativeTurnBasedMatch nativeTurnBasedMatch = callbackResult.Match())
				{
					if (nativeTurnBasedMatch == null)
					{
						UIStatus uIStatus = UIStatus.InternalError;
						switch (callbackResult.ResponseStatus())
						{
						case CommonErrorStatus.MultiplayerStatus.VALID:
							uIStatus = UIStatus.Valid;
							break;
						case CommonErrorStatus.MultiplayerStatus.VALID_BUT_STALE:
							uIStatus = UIStatus.Valid;
							break;
						case CommonErrorStatus.MultiplayerStatus.ERROR_INTERNAL:
							uIStatus = UIStatus.InternalError;
							break;
						case CommonErrorStatus.MultiplayerStatus.ERROR_NOT_AUTHORIZED:
							uIStatus = UIStatus.NotAuthorized;
							break;
						case CommonErrorStatus.MultiplayerStatus.ERROR_VERSION_UPDATE_REQUIRED:
							uIStatus = UIStatus.VersionUpdateRequired;
							break;
						case CommonErrorStatus.MultiplayerStatus.ERROR_TIMEOUT:
							uIStatus = UIStatus.Timeout;
							break;
						}
						userCallback.Invoke(uIStatus, (GooglePlayGames.BasicApi.Multiplayer.TurnBasedMatch)null);
					}
					else
					{
						GooglePlayGames.BasicApi.Multiplayer.TurnBasedMatch turnBasedMatch = nativeTurnBasedMatch.AsTurnBasedMatch(mNativeClient.GetUserId());
						Logger.d("Passing converted match to user callback:" + turnBasedMatch);
						userCallback.Invoke(UIStatus.Valid, turnBasedMatch);
					}
				}
			};
		}

		public void AcceptFromInbox(Action<bool, GooglePlayGames.BasicApi.Multiplayer.TurnBasedMatch> callback)
		{
			callback = Callbacks.AsOnGameThreadCallback<bool, GooglePlayGames.BasicApi.Multiplayer.TurnBasedMatch>(callback);
			mTurnBasedManager.ShowInboxUI(delegate(TurnBasedManager.MatchInboxUIResponse callbackResult)
			{
				using (NativeTurnBasedMatch nativeTurnBasedMatch = callbackResult.Match())
				{
					if (nativeTurnBasedMatch == null)
					{
						callback.Invoke(false, (GooglePlayGames.BasicApi.Multiplayer.TurnBasedMatch)null);
					}
					else
					{
						GooglePlayGames.BasicApi.Multiplayer.TurnBasedMatch turnBasedMatch = nativeTurnBasedMatch.AsTurnBasedMatch(mNativeClient.GetUserId());
						Logger.d("Passing converted match to user callback:" + turnBasedMatch);
						callback.Invoke(true, turnBasedMatch);
					}
				}
			});
		}

		public unsafe void AcceptInvitation(string invitationId, Action<bool, GooglePlayGames.BasicApi.Multiplayer.TurnBasedMatch> callback)
		{
			callback = Callbacks.AsOnGameThreadCallback<bool, GooglePlayGames.BasicApi.Multiplayer.TurnBasedMatch>(callback);
			_003CAcceptInvitation_003Ec__AnonStorey837 _003CAcceptInvitation_003Ec__AnonStorey;
			FindInvitationWithId(invitationId, delegate(GooglePlayGames.Native.PInvoke.MultiplayerInvitation invitation)
			{
				if (invitation == null)
				{
					Logger.e("Could not find invitation with id " + invitationId);
					callback.Invoke(false, (GooglePlayGames.BasicApi.Multiplayer.TurnBasedMatch)null);
				}
				else
				{
					mTurnBasedManager.AcceptInvitation(invitation, BridgeMatchToUserCallback(new Action<UIStatus, GooglePlayGames.BasicApi.Multiplayer.TurnBasedMatch>((object)_003CAcceptInvitation_003Ec__AnonStorey, (IntPtr)(void*)/*OpCode not supported: LdFtn*/)));
				}
			});
		}

		private void FindInvitationWithId(string invitationId, Action<GooglePlayGames.Native.PInvoke.MultiplayerInvitation> callback)
		{
			mTurnBasedManager.GetAllTurnbasedMatches(delegate(TurnBasedManager.TurnBasedMatchesResponse allMatches)
			{
				if (allMatches.Status() <= (CommonErrorStatus.MultiplayerStatus)0)
				{
					callback(null);
				}
				else
				{
					foreach (GooglePlayGames.Native.PInvoke.MultiplayerInvitation item in allMatches.Invitations())
					{
						using (item)
						{
							if (item.Id().Equals(invitationId))
							{
								callback(item);
								return;
							}
						}
					}
					callback(null);
				}
			});
		}

		public unsafe void RegisterMatchDelegate(MatchDelegate del)
		{
			if (del == null)
			{
				mMatchDelegate = null;
			}
			else
			{
				_003CRegisterMatchDelegate_003Ec__AnonStorey839 _003CRegisterMatchDelegate_003Ec__AnonStorey;
				mMatchDelegate = Callbacks.AsOnGameThreadCallback<GooglePlayGames.BasicApi.Multiplayer.TurnBasedMatch, bool>(new Action<GooglePlayGames.BasicApi.Multiplayer.TurnBasedMatch, bool>((object)_003CRegisterMatchDelegate_003Ec__AnonStorey, (IntPtr)(void*)/*OpCode not supported: LdFtn*/));
			}
		}

		internal unsafe void HandleMatchEvent(Types.MultiplayerEvent eventType, string matchId, NativeTurnBasedMatch match)
		{
			//IL_0063: Unknown result type (might be due to invalid IL or missing references)
			//IL_0068: Expected O, but got Unknown
			Action<GooglePlayGames.BasicApi.Multiplayer.TurnBasedMatch, bool> currentDelegate = mMatchDelegate;
			if (currentDelegate != null)
			{
				if (eventType == Types.MultiplayerEvent.REMOVED)
				{
					Logger.d("Ignoring REMOVE event for match " + matchId);
				}
				else
				{
					bool shouldAutolaunch = eventType == Types.MultiplayerEvent.UPDATED_FROM_APP_LAUNCH;
					match.ReferToMe();
					_003CHandleMatchEvent_003Ec__AnonStorey83A _003CHandleMatchEvent_003Ec__AnonStorey83A;
					Callbacks.AsCoroutine(WaitForLogin(new Action((object)_003CHandleMatchEvent_003Ec__AnonStorey83A, (IntPtr)(void*)/*OpCode not supported: LdFtn*/)));
				}
			}
		}

		private IEnumerator WaitForLogin(Action method)
		{
			if (string.IsNullOrEmpty(mNativeClient.GetUserId()))
			{
				yield return (object)null;
			}
			method.Invoke();
		}

		public unsafe void TakeTurn(GooglePlayGames.BasicApi.Multiplayer.TurnBasedMatch match, byte[] data, string pendingParticipantId, Action<bool> callback)
		{
			Logger.describe(data);
			callback = Callbacks.AsOnGameThreadCallback(callback);
			_003CTakeTurn_003Ec__AnonStorey83B _003CTakeTurn_003Ec__AnonStorey83B;
			FindEqualVersionMatchWithParticipant(match, pendingParticipantId, callback, new Action<GooglePlayGames.Native.PInvoke.MultiplayerParticipant, NativeTurnBasedMatch>((object)_003CTakeTurn_003Ec__AnonStorey83B, (IntPtr)(void*)/*OpCode not supported: LdFtn*/));
		}

		private void FindEqualVersionMatch(GooglePlayGames.BasicApi.Multiplayer.TurnBasedMatch match, Action<bool> onFailure, Action<NativeTurnBasedMatch> onVersionMatch)
		{
			mTurnBasedManager.GetMatch(match.MatchId, delegate(TurnBasedManager.TurnBasedMatchResponse response)
			{
				using (NativeTurnBasedMatch nativeTurnBasedMatch = response.Match())
				{
					if (nativeTurnBasedMatch == null)
					{
						Logger.e($"Could not find match {match.MatchId}");
						onFailure(false);
					}
					else if (nativeTurnBasedMatch.Version() != match.Version)
					{
						Logger.e($"Attempted to update a stale version of the match. Expected version was {match.Version} but current version is {nativeTurnBasedMatch.Version()}.");
						onFailure(false);
					}
					else
					{
						onVersionMatch(nativeTurnBasedMatch);
					}
				}
			});
		}

		private void FindEqualVersionMatchWithParticipant(GooglePlayGames.BasicApi.Multiplayer.TurnBasedMatch match, string participantId, Action<bool> onFailure, Action<GooglePlayGames.Native.PInvoke.MultiplayerParticipant, NativeTurnBasedMatch> onFoundParticipantAndMatch)
		{
			FindEqualVersionMatch(match, onFailure, delegate(NativeTurnBasedMatch foundMatch)
			{
				if (participantId == null)
				{
					using (GooglePlayGames.Native.PInvoke.MultiplayerParticipant multiplayerParticipant = GooglePlayGames.Native.PInvoke.MultiplayerParticipant.AutomatchingSentinel())
					{
						onFoundParticipantAndMatch.Invoke(multiplayerParticipant, foundMatch);
						return;
						IL_0023:;
					}
				}
				using (GooglePlayGames.Native.PInvoke.MultiplayerParticipant multiplayerParticipant2 = foundMatch.ParticipantWithId(participantId))
				{
					if (multiplayerParticipant2 == null)
					{
						Logger.e($"Located match {match.MatchId} but desired participant with ID {participantId} could not be found");
						onFailure(false);
					}
					else
					{
						onFoundParticipantAndMatch.Invoke(multiplayerParticipant2, foundMatch);
					}
				}
			});
		}

		public int GetMaxMatchDataSize()
		{
			throw new NotImplementedException();
		}

		public void Finish(GooglePlayGames.BasicApi.Multiplayer.TurnBasedMatch match, byte[] data, MatchOutcome outcome, Action<bool> callback)
		{
			callback = Callbacks.AsOnGameThreadCallback(callback);
			FindEqualVersionMatch(match, callback, delegate(NativeTurnBasedMatch foundMatch)
			{
				GooglePlayGames.Native.PInvoke.ParticipantResults participantResults = foundMatch.Results();
				foreach (string participantId in outcome.ParticipantIds)
				{
					Types.MatchResult matchResult = ResultToMatchResult(outcome.GetResultFor(participantId));
					uint placementFor = outcome.GetPlacementFor(participantId);
					if (participantResults.HasResultsForParticipant(participantId))
					{
						Types.MatchResult matchResult2 = participantResults.ResultsForParticipant(participantId);
						uint num = participantResults.PlacingForParticipant(participantId);
						if (matchResult != matchResult2 || placementFor != num)
						{
							Logger.e($"Attempted to override existing results for participant {participantId}: Placing {num}, Result {matchResult2}");
							callback(false);
							return;
						}
					}
					else
					{
						GooglePlayGames.Native.PInvoke.ParticipantResults participantResults2 = participantResults;
						participantResults = participantResults2.WithResult(participantId, placementFor, matchResult);
						participantResults2.Dispose();
					}
				}
				mTurnBasedManager.FinishMatchDuringMyTurn(foundMatch, data, participantResults, delegate(TurnBasedManager.TurnBasedMatchResponse response)
				{
					callback(response.RequestSucceeded());
				});
			});
		}

		private static Types.MatchResult ResultToMatchResult(MatchOutcome.ParticipantResult result)
		{
			switch (result)
			{
			case MatchOutcome.ParticipantResult.Loss:
				return Types.MatchResult.LOSS;
			case MatchOutcome.ParticipantResult.None:
				return Types.MatchResult.NONE;
			case MatchOutcome.ParticipantResult.Tie:
				return Types.MatchResult.TIE;
			case MatchOutcome.ParticipantResult.Win:
				return Types.MatchResult.WIN;
			default:
				Logger.e("Received unknown ParticipantResult " + result);
				return Types.MatchResult.NONE;
			}
		}

		public void AcknowledgeFinished(GooglePlayGames.BasicApi.Multiplayer.TurnBasedMatch match, Action<bool> callback)
		{
			callback = Callbacks.AsOnGameThreadCallback(callback);
			FindEqualVersionMatch(match, callback, delegate(NativeTurnBasedMatch foundMatch)
			{
				mTurnBasedManager.ConfirmPendingCompletion(foundMatch, delegate(TurnBasedManager.TurnBasedMatchResponse response)
				{
					callback(response.RequestSucceeded());
				});
			});
		}

		public void Leave(GooglePlayGames.BasicApi.Multiplayer.TurnBasedMatch match, Action<bool> callback)
		{
			callback = Callbacks.AsOnGameThreadCallback(callback);
			FindEqualVersionMatch(match, callback, delegate(NativeTurnBasedMatch foundMatch)
			{
				mTurnBasedManager.LeaveMatchDuringTheirTurn(foundMatch, delegate(CommonErrorStatus.MultiplayerStatus status)
				{
					callback(status > (CommonErrorStatus.MultiplayerStatus)0);
				});
			});
		}

		public unsafe void LeaveDuringTurn(GooglePlayGames.BasicApi.Multiplayer.TurnBasedMatch match, string pendingParticipantId, Action<bool> callback)
		{
			callback = Callbacks.AsOnGameThreadCallback(callback);
			_003CLeaveDuringTurn_003Ec__AnonStorey841 _003CLeaveDuringTurn_003Ec__AnonStorey;
			FindEqualVersionMatchWithParticipant(match, pendingParticipantId, callback, new Action<GooglePlayGames.Native.PInvoke.MultiplayerParticipant, NativeTurnBasedMatch>((object)_003CLeaveDuringTurn_003Ec__AnonStorey, (IntPtr)(void*)/*OpCode not supported: LdFtn*/));
		}

		public void Cancel(GooglePlayGames.BasicApi.Multiplayer.TurnBasedMatch match, Action<bool> callback)
		{
			callback = Callbacks.AsOnGameThreadCallback(callback);
			FindEqualVersionMatch(match, callback, delegate(NativeTurnBasedMatch foundMatch)
			{
				mTurnBasedManager.CancelMatch(foundMatch, delegate(CommonErrorStatus.MultiplayerStatus status)
				{
					callback(status > (CommonErrorStatus.MultiplayerStatus)0);
				});
			});
		}

		public unsafe void Rematch(GooglePlayGames.BasicApi.Multiplayer.TurnBasedMatch match, Action<bool, GooglePlayGames.BasicApi.Multiplayer.TurnBasedMatch> callback)
		{
			callback = Callbacks.AsOnGameThreadCallback<bool, GooglePlayGames.BasicApi.Multiplayer.TurnBasedMatch>(callback);
			_003CRematch_003Ec__AnonStorey843 _003CRematch_003Ec__AnonStorey;
			FindEqualVersionMatch(match, delegate
			{
				callback.Invoke(false, (GooglePlayGames.BasicApi.Multiplayer.TurnBasedMatch)null);
			}, delegate(NativeTurnBasedMatch foundMatch)
			{
				mTurnBasedManager.Rematch(foundMatch, BridgeMatchToUserCallback(new Action<UIStatus, GooglePlayGames.BasicApi.Multiplayer.TurnBasedMatch>((object)_003CRematch_003Ec__AnonStorey, (IntPtr)(void*)/*OpCode not supported: LdFtn*/)));
			});
		}

		public void DeclineInvitation(string invitationId)
		{
			FindInvitationWithId(invitationId, delegate(GooglePlayGames.Native.PInvoke.MultiplayerInvitation invitation)
			{
				if (invitation != null)
				{
					mTurnBasedManager.DeclineInvitation(invitation);
				}
			});
		}
	}
}
