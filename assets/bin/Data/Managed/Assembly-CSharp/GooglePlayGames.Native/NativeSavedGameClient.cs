using GooglePlayGames.BasicApi;
using GooglePlayGames.BasicApi.SavedGame;
using GooglePlayGames.Native.Cwrapper;
using GooglePlayGames.Native.PInvoke;
using GooglePlayGames.OurUtils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace GooglePlayGames.Native
{
	internal class NativeSavedGameClient : ISavedGameClient
	{
		private class NativeConflictResolver : IConflictResolver
		{
			private readonly GooglePlayGames.Native.PInvoke.SnapshotManager mManager;

			private readonly string mConflictId;

			private readonly NativeSnapshotMetadata mOriginal;

			private readonly NativeSnapshotMetadata mUnmerged;

			private readonly Action<SavedGameRequestStatus, ISavedGameMetadata> mCompleteCallback;

			private readonly Action mRetryFileOpen;

			internal NativeConflictResolver(GooglePlayGames.Native.PInvoke.SnapshotManager manager, string conflictId, NativeSnapshotMetadata original, NativeSnapshotMetadata unmerged, Action<SavedGameRequestStatus, ISavedGameMetadata> completeCallback, Action retryOpen)
			{
				mManager = Misc.CheckNotNull(manager);
				mConflictId = Misc.CheckNotNull(conflictId);
				mOriginal = Misc.CheckNotNull(original);
				mUnmerged = Misc.CheckNotNull(unmerged);
				mCompleteCallback = Misc.CheckNotNull<Action<SavedGameRequestStatus, ISavedGameMetadata>>(completeCallback);
				mRetryFileOpen = Misc.CheckNotNull<Action>(retryOpen);
			}

			public void ChooseMetadata(ISavedGameMetadata chosenMetadata)
			{
				NativeSnapshotMetadata nativeSnapshotMetadata = chosenMetadata as NativeSnapshotMetadata;
				if (nativeSnapshotMetadata != mOriginal && nativeSnapshotMetadata != mUnmerged)
				{
					Logger.e("Caller attempted to choose a version of the metadata that was not part of the conflict");
					mCompleteCallback.Invoke(SavedGameRequestStatus.BadInputError, (ISavedGameMetadata)null);
				}
				else
				{
					mManager.Resolve(nativeSnapshotMetadata, new NativeSnapshotMetadataChange.Builder().Build(), mConflictId, delegate(GooglePlayGames.Native.PInvoke.SnapshotManager.CommitResponse response)
					{
						if (!response.RequestSucceeded())
						{
							mCompleteCallback.Invoke(AsRequestStatus(response.ResponseStatus()), (ISavedGameMetadata)null);
						}
						else
						{
							mRetryFileOpen.Invoke();
						}
					});
				}
			}
		}

		private class Prefetcher
		{
			private readonly object mLock = new object();

			private bool mOriginalDataFetched;

			private byte[] mOriginalData;

			private bool mUnmergedDataFetched;

			private byte[] mUnmergedData;

			private Action<SavedGameRequestStatus, ISavedGameMetadata> completedCallback;

			private readonly Action<byte[], byte[]> mDataFetchedCallback;

			internal Prefetcher(Action<byte[], byte[]> dataFetchedCallback, Action<SavedGameRequestStatus, ISavedGameMetadata> completedCallback)
			{
				mDataFetchedCallback = Misc.CheckNotNull<Action<byte[], byte[]>>(dataFetchedCallback);
				this.completedCallback = Misc.CheckNotNull<Action<SavedGameRequestStatus, ISavedGameMetadata>>(completedCallback);
			}

			internal unsafe void OnOriginalDataRead(GooglePlayGames.Native.PInvoke.SnapshotManager.ReadResponse readResponse)
			{
				lock (mLock)
				{
					if (!readResponse.RequestSucceeded())
					{
						Logger.e("Encountered error while prefetching original data.");
						completedCallback.Invoke(AsRequestStatus(readResponse.ResponseStatus()), (ISavedGameMetadata)null);
						if (_003C_003Ef__am_0024cache7 == null)
						{
							_003C_003Ef__am_0024cache7 = new Action<SavedGameRequestStatus, ISavedGameMetadata>((object)null, (IntPtr)(void*)/*OpCode not supported: LdFtn*/);
						}
						completedCallback = _003C_003Ef__am_0024cache7;
					}
					else
					{
						Logger.d("Successfully fetched original data");
						mOriginalDataFetched = true;
						mOriginalData = readResponse.Data();
						MaybeProceed();
					}
				}
			}

			internal unsafe void OnUnmergedDataRead(GooglePlayGames.Native.PInvoke.SnapshotManager.ReadResponse readResponse)
			{
				lock (mLock)
				{
					if (!readResponse.RequestSucceeded())
					{
						Logger.e("Encountered error while prefetching unmerged data.");
						completedCallback.Invoke(AsRequestStatus(readResponse.ResponseStatus()), (ISavedGameMetadata)null);
						if (_003C_003Ef__am_0024cache8 == null)
						{
							_003C_003Ef__am_0024cache8 = new Action<SavedGameRequestStatus, ISavedGameMetadata>((object)null, (IntPtr)(void*)/*OpCode not supported: LdFtn*/);
						}
						completedCallback = _003C_003Ef__am_0024cache8;
					}
					else
					{
						Logger.d("Successfully fetched unmerged data");
						mUnmergedDataFetched = true;
						mUnmergedData = readResponse.Data();
						MaybeProceed();
					}
				}
			}

			private void MaybeProceed()
			{
				if (mOriginalDataFetched && mUnmergedDataFetched)
				{
					Logger.d("Fetched data for original and unmerged, proceeding");
					mDataFetchedCallback.Invoke(mOriginalData, mUnmergedData);
				}
				else
				{
					Logger.d("Not all data fetched - original:" + mOriginalDataFetched + " unmerged:" + mUnmergedDataFetched);
				}
			}
		}

		private static readonly Regex ValidFilenameRegex = new Regex("\\A[a-zA-Z0-9-._~]{1,100}\\Z");

		private readonly GooglePlayGames.Native.PInvoke.SnapshotManager mSnapshotManager;

		internal NativeSavedGameClient(GooglePlayGames.Native.PInvoke.SnapshotManager manager)
		{
			mSnapshotManager = Misc.CheckNotNull(manager);
		}

		public void OpenWithAutomaticConflictResolution(string filename, DataSource source, ConflictResolutionStrategy resolutionStrategy, Action<SavedGameRequestStatus, ISavedGameMetadata> callback)
		{
			Misc.CheckNotNull(filename);
			Misc.CheckNotNull<Action<SavedGameRequestStatus, ISavedGameMetadata>>(callback);
			callback = ToOnGameThread<SavedGameRequestStatus, ISavedGameMetadata>(callback);
			if (!IsValidFilename(filename))
			{
				Logger.e("Received invalid filename: " + filename);
				callback.Invoke(SavedGameRequestStatus.BadInputError, (ISavedGameMetadata)null);
			}
			else
			{
				OpenWithManualConflictResolution(filename, source, false, delegate(IConflictResolver resolver, ISavedGameMetadata original, byte[] originalData, ISavedGameMetadata unmerged, byte[] unmergedData)
				{
					switch (resolutionStrategy)
					{
					case ConflictResolutionStrategy.UseOriginal:
						resolver.ChooseMetadata(original);
						break;
					case ConflictResolutionStrategy.UseUnmerged:
						resolver.ChooseMetadata(unmerged);
						break;
					case ConflictResolutionStrategy.UseLongestPlaytime:
						if (original.TotalTimePlayed >= unmerged.TotalTimePlayed)
						{
							resolver.ChooseMetadata(original);
						}
						else
						{
							resolver.ChooseMetadata(unmerged);
						}
						break;
					default:
						Logger.e("Unhandled strategy " + resolutionStrategy);
						callback.Invoke(SavedGameRequestStatus.InternalError, (ISavedGameMetadata)null);
						break;
					}
				}, callback);
			}
		}

		private unsafe ConflictCallback ToOnGameThread(ConflictCallback conflictCallback)
		{
			return delegate
			{
				//IL_0043: Unknown result type (might be due to invalid IL or missing references)
				//IL_0048: Expected O, but got Unknown
				Logger.d("Invoking conflict callback");
				_003CToOnGameThread_003Ec__AnonStorey807._003CToOnGameThread_003Ec__AnonStorey808 _003CToOnGameThread_003Ec__AnonStorey;
				PlayGamesHelperObject.RunOnGameThread(new Action((object)_003CToOnGameThread_003Ec__AnonStorey, (IntPtr)(void*)/*OpCode not supported: LdFtn*/));
			};
		}

		public void OpenWithManualConflictResolution(string filename, DataSource source, bool prefetchDataOnConflict, ConflictCallback conflictCallback, Action<SavedGameRequestStatus, ISavedGameMetadata> completedCallback)
		{
			Misc.CheckNotNull(filename);
			Misc.CheckNotNull(conflictCallback);
			Misc.CheckNotNull<Action<SavedGameRequestStatus, ISavedGameMetadata>>(completedCallback);
			conflictCallback = ToOnGameThread(conflictCallback);
			completedCallback = ToOnGameThread<SavedGameRequestStatus, ISavedGameMetadata>(completedCallback);
			if (!IsValidFilename(filename))
			{
				Logger.e("Received invalid filename: " + filename);
				completedCallback.Invoke(SavedGameRequestStatus.BadInputError, (ISavedGameMetadata)null);
			}
			else
			{
				InternalManualOpen(filename, source, prefetchDataOnConflict, conflictCallback, completedCallback);
			}
		}

		private unsafe void InternalManualOpen(string filename, DataSource source, bool prefetchDataOnConflict, ConflictCallback conflictCallback, Action<SavedGameRequestStatus, ISavedGameMetadata> completedCallback)
		{
			mSnapshotManager.Open(filename, AsDataSource(source), Types.SnapshotConflictPolicy.MANUAL, delegate(GooglePlayGames.Native.PInvoke.SnapshotManager.OpenResponse response)
			{
				//IL_00a6: Unknown result type (might be due to invalid IL or missing references)
				//IL_00ab: Expected O, but got Unknown
				if (!response.RequestSucceeded())
				{
					completedCallback.Invoke(AsRequestStatus(response.ResponseStatus()), (ISavedGameMetadata)null);
				}
				else if (response.ResponseStatus() == CommonErrorStatus.SnapshotOpenStatus.VALID)
				{
					completedCallback.Invoke(SavedGameRequestStatus.Success, (ISavedGameMetadata)response.Data());
				}
				else if (response.ResponseStatus() == CommonErrorStatus.SnapshotOpenStatus.VALID_WITH_CONFLICT)
				{
					NativeSnapshotMetadata original = response.ConflictOriginal();
					NativeSnapshotMetadata unmerged = response.ConflictUnmerged();
					_003CInternalManualOpen_003Ec__AnonStorey809._003CInternalManualOpen_003Ec__AnonStorey80A _003CInternalManualOpen_003Ec__AnonStorey80A;
					NativeConflictResolver resolver = new NativeConflictResolver(mSnapshotManager, response.ConflictId(), original, unmerged, completedCallback, new Action((object)_003CInternalManualOpen_003Ec__AnonStorey80A, (IntPtr)(void*)/*OpCode not supported: LdFtn*/));
					if (!prefetchDataOnConflict)
					{
						conflictCallback(resolver, original, null, unmerged, null);
					}
					else
					{
						Prefetcher @object = new Prefetcher(new Action<byte[], byte[]>((object)_003CInternalManualOpen_003Ec__AnonStorey80A, (IntPtr)(void*)/*OpCode not supported: LdFtn*/), completedCallback);
						mSnapshotManager.Read(original, @object.OnOriginalDataRead);
						mSnapshotManager.Read(unmerged, @object.OnUnmergedDataRead);
					}
				}
				else
				{
					Logger.e("Unhandled response status");
					completedCallback.Invoke(SavedGameRequestStatus.InternalError, (ISavedGameMetadata)null);
				}
			});
		}

		public void ReadBinaryData(ISavedGameMetadata metadata, Action<SavedGameRequestStatus, byte[]> completedCallback)
		{
			Misc.CheckNotNull(metadata);
			Misc.CheckNotNull<Action<SavedGameRequestStatus, byte[]>>(completedCallback);
			completedCallback = ToOnGameThread<SavedGameRequestStatus, byte[]>(completedCallback);
			NativeSnapshotMetadata nativeSnapshotMetadata = metadata as NativeSnapshotMetadata;
			if (nativeSnapshotMetadata == null)
			{
				Logger.e("Encountered metadata that was not generated by this ISavedGameClient");
				completedCallback.Invoke(SavedGameRequestStatus.BadInputError, (byte[])null);
			}
			else if (!nativeSnapshotMetadata.IsOpen)
			{
				Logger.e("This method requires an open ISavedGameMetadata.");
				completedCallback.Invoke(SavedGameRequestStatus.BadInputError, (byte[])null);
			}
			else
			{
				mSnapshotManager.Read(nativeSnapshotMetadata, delegate(GooglePlayGames.Native.PInvoke.SnapshotManager.ReadResponse response)
				{
					if (!response.RequestSucceeded())
					{
						completedCallback.Invoke(AsRequestStatus(response.ResponseStatus()), (byte[])null);
					}
					else
					{
						completedCallback.Invoke(SavedGameRequestStatus.Success, response.Data());
					}
				});
			}
		}

		public void ShowSelectSavedGameUI(string uiTitle, uint maxDisplayedSavedGames, bool showCreateSaveUI, bool showDeleteSaveUI, Action<SelectUIStatus, ISavedGameMetadata> callback)
		{
			Misc.CheckNotNull(uiTitle);
			Misc.CheckNotNull<Action<SelectUIStatus, ISavedGameMetadata>>(callback);
			callback = ToOnGameThread<SelectUIStatus, ISavedGameMetadata>(callback);
			if (maxDisplayedSavedGames == 0)
			{
				Logger.e("maxDisplayedSavedGames must be greater than 0");
				callback.Invoke(SelectUIStatus.BadInputError, (ISavedGameMetadata)null);
			}
			else
			{
				mSnapshotManager.SnapshotSelectUI(showCreateSaveUI, showDeleteSaveUI, maxDisplayedSavedGames, uiTitle, delegate(GooglePlayGames.Native.PInvoke.SnapshotManager.SnapshotSelectUIResponse response)
				{
					callback.Invoke(AsUIStatus(response.RequestStatus()), (ISavedGameMetadata)((!response.RequestSucceeded()) ? null : response.Data()));
				});
			}
		}

		public void CommitUpdate(ISavedGameMetadata metadata, SavedGameMetadataUpdate updateForMetadata, byte[] updatedBinaryData, Action<SavedGameRequestStatus, ISavedGameMetadata> callback)
		{
			Misc.CheckNotNull(metadata);
			Misc.CheckNotNull(updatedBinaryData);
			Misc.CheckNotNull<Action<SavedGameRequestStatus, ISavedGameMetadata>>(callback);
			callback = ToOnGameThread<SavedGameRequestStatus, ISavedGameMetadata>(callback);
			NativeSnapshotMetadata nativeSnapshotMetadata = metadata as NativeSnapshotMetadata;
			if (nativeSnapshotMetadata == null)
			{
				Logger.e("Encountered metadata that was not generated by this ISavedGameClient");
				callback.Invoke(SavedGameRequestStatus.BadInputError, (ISavedGameMetadata)null);
			}
			else if (!nativeSnapshotMetadata.IsOpen)
			{
				Logger.e("This method requires an open ISavedGameMetadata.");
				callback.Invoke(SavedGameRequestStatus.BadInputError, (ISavedGameMetadata)null);
			}
			else
			{
				mSnapshotManager.Commit(nativeSnapshotMetadata, AsMetadataChange(updateForMetadata), updatedBinaryData, delegate(GooglePlayGames.Native.PInvoke.SnapshotManager.CommitResponse response)
				{
					if (!response.RequestSucceeded())
					{
						callback.Invoke(AsRequestStatus(response.ResponseStatus()), (ISavedGameMetadata)null);
					}
					else
					{
						callback.Invoke(SavedGameRequestStatus.Success, (ISavedGameMetadata)response.Data());
					}
				});
			}
		}

		public void FetchAllSavedGames(DataSource source, Action<SavedGameRequestStatus, List<ISavedGameMetadata>> callback)
		{
			Misc.CheckNotNull<Action<SavedGameRequestStatus, List<ISavedGameMetadata>>>(callback);
			callback = ToOnGameThread<SavedGameRequestStatus, List<ISavedGameMetadata>>(callback);
			mSnapshotManager.FetchAll(AsDataSource(source), delegate(GooglePlayGames.Native.PInvoke.SnapshotManager.FetchAllResponse response)
			{
				if (!response.RequestSucceeded())
				{
					callback.Invoke(AsRequestStatus(response.ResponseStatus()), new List<ISavedGameMetadata>());
				}
				else
				{
					callback.Invoke(SavedGameRequestStatus.Success, response.Data().Cast<ISavedGameMetadata>().ToList());
				}
			});
		}

		public void Delete(ISavedGameMetadata metadata)
		{
			Misc.CheckNotNull(metadata);
			mSnapshotManager.Delete((NativeSnapshotMetadata)metadata);
		}

		internal static bool IsValidFilename(string filename)
		{
			if (filename == null)
			{
				return false;
			}
			return ValidFilenameRegex.IsMatch(filename);
		}

		private static Types.SnapshotConflictPolicy AsConflictPolicy(ConflictResolutionStrategy strategy)
		{
			switch (strategy)
			{
			case ConflictResolutionStrategy.UseLongestPlaytime:
				return Types.SnapshotConflictPolicy.LONGEST_PLAYTIME;
			case ConflictResolutionStrategy.UseOriginal:
				return Types.SnapshotConflictPolicy.LAST_KNOWN_GOOD;
			case ConflictResolutionStrategy.UseUnmerged:
				return Types.SnapshotConflictPolicy.MOST_RECENTLY_MODIFIED;
			default:
				throw new InvalidOperationException("Found unhandled strategy: " + strategy);
			}
		}

		private static SavedGameRequestStatus AsRequestStatus(CommonErrorStatus.SnapshotOpenStatus status)
		{
			switch (status)
			{
			case CommonErrorStatus.SnapshotOpenStatus.VALID:
				return SavedGameRequestStatus.Success;
			case CommonErrorStatus.SnapshotOpenStatus.ERROR_NOT_AUTHORIZED:
				return SavedGameRequestStatus.AuthenticationError;
			case CommonErrorStatus.SnapshotOpenStatus.ERROR_TIMEOUT:
				return SavedGameRequestStatus.TimeoutError;
			default:
				Logger.e("Encountered unknown status: " + status);
				return SavedGameRequestStatus.InternalError;
			}
		}

		private static Types.DataSource AsDataSource(DataSource source)
		{
			switch (source)
			{
			case DataSource.ReadCacheOrNetwork:
				return Types.DataSource.CACHE_OR_NETWORK;
			case DataSource.ReadNetworkOnly:
				return Types.DataSource.NETWORK_ONLY;
			default:
				throw new InvalidOperationException("Found unhandled DataSource: " + source);
			}
		}

		private static SavedGameRequestStatus AsRequestStatus(CommonErrorStatus.ResponseStatus status)
		{
			switch (status)
			{
			case CommonErrorStatus.ResponseStatus.ERROR_INTERNAL:
				return SavedGameRequestStatus.InternalError;
			case CommonErrorStatus.ResponseStatus.ERROR_LICENSE_CHECK_FAILED:
				Logger.e("User attempted to use the game without a valid license.");
				return SavedGameRequestStatus.AuthenticationError;
			case CommonErrorStatus.ResponseStatus.ERROR_NOT_AUTHORIZED:
				Logger.e("User was not authorized (they were probably not logged in).");
				return SavedGameRequestStatus.AuthenticationError;
			case CommonErrorStatus.ResponseStatus.ERROR_TIMEOUT:
				return SavedGameRequestStatus.TimeoutError;
			case CommonErrorStatus.ResponseStatus.VALID:
			case CommonErrorStatus.ResponseStatus.VALID_BUT_STALE:
				return SavedGameRequestStatus.Success;
			default:
				Logger.e("Unknown status: " + status);
				return SavedGameRequestStatus.InternalError;
			}
		}

		private static SelectUIStatus AsUIStatus(CommonErrorStatus.UIStatus uiStatus)
		{
			switch (uiStatus)
			{
			case CommonErrorStatus.UIStatus.VALID:
				return SelectUIStatus.SavedGameSelected;
			case CommonErrorStatus.UIStatus.ERROR_CANCELED:
				return SelectUIStatus.UserClosedUI;
			case CommonErrorStatus.UIStatus.ERROR_INTERNAL:
				return SelectUIStatus.InternalError;
			case CommonErrorStatus.UIStatus.ERROR_NOT_AUTHORIZED:
				return SelectUIStatus.AuthenticationError;
			case CommonErrorStatus.UIStatus.ERROR_TIMEOUT:
				return SelectUIStatus.TimeoutError;
			default:
				Logger.e("Encountered unknown UI Status: " + uiStatus);
				return SelectUIStatus.InternalError;
			}
		}

		private static NativeSnapshotMetadataChange AsMetadataChange(SavedGameMetadataUpdate update)
		{
			NativeSnapshotMetadataChange.Builder builder = new NativeSnapshotMetadataChange.Builder();
			if (update.IsCoverImageUpdated)
			{
				builder.SetCoverImageFromPngData(update.UpdatedPngCoverImage);
			}
			if (update.IsDescriptionUpdated)
			{
				builder.SetDescription(update.UpdatedDescription);
			}
			if (update.IsPlayedTimeUpdated)
			{
				builder.SetPlayedTime((ulong)update.UpdatedPlayedTime.Value.TotalMilliseconds);
			}
			return builder.Build();
		}

		private unsafe static Action<T1, T2> ToOnGameThread<T1, T2>(Action<T1, T2> toConvert)
		{
			_003CToOnGameThread_003Ec__AnonStorey80F<T1, T2> _003CToOnGameThread_003Ec__AnonStorey80F;
			return new Action<_003F, _003F>((object)_003CToOnGameThread_003Ec__AnonStorey80F, (IntPtr)(void*)/*OpCode not supported: LdFtn*/);
		}
	}
}
