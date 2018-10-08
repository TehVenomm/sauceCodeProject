package com.google.android.gms.games.internal;

import android.content.Context;
import android.content.Intent;
import android.graphics.Bitmap;
import android.os.Binder;
import android.os.Bundle;
import android.os.IBinder;
import android.os.IInterface;
import android.os.Looper;
import android.os.RemoteException;
import android.support.annotation.NonNull;
import android.view.View;
import com.google.android.gms.common.ConnectionResult;
import com.google.android.gms.common.Scopes;
import com.google.android.gms.common.api.GoogleApiClient.ConnectionCallbacks;
import com.google.android.gms.common.api.GoogleApiClient.OnConnectionFailedListener;
import com.google.android.gms.common.api.Scope;
import com.google.android.gms.common.api.Status;
import com.google.android.gms.common.data.AbstractDataBuffer;
import com.google.android.gms.common.data.BitmapTeleporter;
import com.google.android.gms.common.data.DataHolder;
import com.google.android.gms.common.internal.BinderWrapper;
import com.google.android.gms.games.Game;
import com.google.android.gms.games.GameBuffer;
import com.google.android.gms.games.GameEntity;
import com.google.android.gms.games.Games.GamesOptions;
import com.google.android.gms.games.Games.GetServerAuthCodeResult;
import com.google.android.gms.games.GamesMetadata.LoadGamesResult;
import com.google.android.gms.games.GamesStatusCodes;
import com.google.android.gms.games.Player;
import com.google.android.gms.games.PlayerBuffer;
import com.google.android.gms.games.PlayerEntity;
import com.google.android.gms.games.Players.LoadPlayersResult;
import com.google.android.gms.games.achievement.AchievementBuffer;
import com.google.android.gms.games.achievement.Achievements.LoadAchievementsResult;
import com.google.android.gms.games.achievement.Achievements.UpdateAchievementResult;
import com.google.android.gms.games.event.EventBuffer;
import com.google.android.gms.games.event.Events.LoadEventsResult;
import com.google.android.gms.games.leaderboard.Leaderboard;
import com.google.android.gms.games.leaderboard.LeaderboardBuffer;
import com.google.android.gms.games.leaderboard.LeaderboardEntity;
import com.google.android.gms.games.leaderboard.LeaderboardScore;
import com.google.android.gms.games.leaderboard.LeaderboardScoreBuffer;
import com.google.android.gms.games.leaderboard.LeaderboardScoreEntity;
import com.google.android.gms.games.leaderboard.Leaderboards.LeaderboardMetadataResult;
import com.google.android.gms.games.leaderboard.Leaderboards.LoadPlayerScoreResult;
import com.google.android.gms.games.leaderboard.Leaderboards.LoadScoresResult;
import com.google.android.gms.games.leaderboard.Leaderboards.SubmitScoreResult;
import com.google.android.gms.games.leaderboard.ScoreSubmissionData;
import com.google.android.gms.games.multiplayer.Invitation;
import com.google.android.gms.games.multiplayer.InvitationBuffer;
import com.google.android.gms.games.multiplayer.Invitations.LoadInvitationsResult;
import com.google.android.gms.games.multiplayer.OnInvitationReceivedListener;
import com.google.android.gms.games.multiplayer.ParticipantResult;
import com.google.android.gms.games.multiplayer.realtime.RealTimeMessage;
import com.google.android.gms.games.multiplayer.realtime.RealTimeMessageReceivedListener;
import com.google.android.gms.games.multiplayer.realtime.RealTimeMultiplayer.ReliableMessageSentCallback;
import com.google.android.gms.games.multiplayer.realtime.Room;
import com.google.android.gms.games.multiplayer.realtime.RoomConfig;
import com.google.android.gms.games.multiplayer.realtime.RoomEntity;
import com.google.android.gms.games.multiplayer.realtime.RoomStatusUpdateListener;
import com.google.android.gms.games.multiplayer.realtime.RoomUpdateListener;
import com.google.android.gms.games.multiplayer.turnbased.LoadMatchesResponse;
import com.google.android.gms.games.multiplayer.turnbased.OnTurnBasedMatchUpdateReceivedListener;
import com.google.android.gms.games.multiplayer.turnbased.TurnBasedMatch;
import com.google.android.gms.games.multiplayer.turnbased.TurnBasedMatchBuffer;
import com.google.android.gms.games.multiplayer.turnbased.TurnBasedMatchConfig;
import com.google.android.gms.games.multiplayer.turnbased.TurnBasedMultiplayer.CancelMatchResult;
import com.google.android.gms.games.multiplayer.turnbased.TurnBasedMultiplayer.InitiateMatchResult;
import com.google.android.gms.games.multiplayer.turnbased.TurnBasedMultiplayer.LeaveMatchResult;
import com.google.android.gms.games.multiplayer.turnbased.TurnBasedMultiplayer.LoadMatchResult;
import com.google.android.gms.games.multiplayer.turnbased.TurnBasedMultiplayer.LoadMatchesResult;
import com.google.android.gms.games.multiplayer.turnbased.TurnBasedMultiplayer.UpdateMatchResult;
import com.google.android.gms.games.quest.Milestone;
import com.google.android.gms.games.quest.Quest;
import com.google.android.gms.games.quest.QuestBuffer;
import com.google.android.gms.games.quest.QuestEntity;
import com.google.android.gms.games.quest.QuestUpdateListener;
import com.google.android.gms.games.quest.Quests.AcceptQuestResult;
import com.google.android.gms.games.quest.Quests.ClaimMilestoneResult;
import com.google.android.gms.games.quest.Quests.LoadQuestsResult;
import com.google.android.gms.games.request.GameRequest;
import com.google.android.gms.games.request.GameRequestBuffer;
import com.google.android.gms.games.request.OnRequestReceivedListener;
import com.google.android.gms.games.request.Requests.LoadRequestsResult;
import com.google.android.gms.games.request.Requests.UpdateRequestsResult;
import com.google.android.gms.games.snapshot.Snapshot;
import com.google.android.gms.games.snapshot.SnapshotContents;
import com.google.android.gms.games.snapshot.SnapshotEntity;
import com.google.android.gms.games.snapshot.SnapshotMetadata;
import com.google.android.gms.games.snapshot.SnapshotMetadataBuffer;
import com.google.android.gms.games.snapshot.SnapshotMetadataChange;
import com.google.android.gms.games.snapshot.SnapshotMetadataEntity;
import com.google.android.gms.games.snapshot.Snapshots.CommitSnapshotResult;
import com.google.android.gms.games.snapshot.Snapshots.DeleteSnapshotResult;
import com.google.android.gms.games.snapshot.Snapshots.LoadSnapshotsResult;
import com.google.android.gms.games.snapshot.Snapshots.OpenSnapshotResult;
import com.google.android.gms.games.stats.PlayerStats;
import com.google.android.gms.games.stats.PlayerStatsBuffer;
import com.google.android.gms.games.stats.Stats.LoadPlayerStatsResult;
import com.google.android.gms.games.video.CaptureState;
import com.google.android.gms.games.video.VideoCapabilities;
import com.google.android.gms.games.video.Videos.CaptureAvailableResult;
import com.google.android.gms.games.video.Videos.CaptureCapabilitiesResult;
import com.google.android.gms.games.video.Videos.CaptureOverlayStateListener;
import com.google.android.gms.games.video.Videos.CaptureStateResult;
import com.google.android.gms.games.video.Videos.CaptureStreamingUrlResult;
import com.google.android.gms.internal.zzbvi;
import com.google.android.gms.internal.zzbvk;
import com.google.android.gms.internal.zzbvl;
import com.google.android.gms.internal.zzcpw;
import java.util.ArrayList;
import java.util.List;
import java.util.Set;

public final class GamesClientImpl extends com.google.android.gms.common.internal.zzaa<zzj> {
    private zzbvk zzhdy = new zzd(this);
    private final String zzhdz;
    private PlayerEntity zzhea;
    private GameEntity zzheb;
    private final zzn zzhec;
    private boolean zzhed = false;
    private final Binder zzhee;
    private final long zzhef;
    private final GamesOptions zzheg;
    private boolean zzheh = false;

    static final class CaptureStreamingUrlResultImpl implements CaptureStreamingUrlResult {
        private final String zzad;

        public final Status getStatus() {
            throw new NoSuchMethodError();
        }

        public final String getUrl() {
            return this.zzad;
        }
    }

    static abstract class zzc extends com.google.android.gms.common.api.internal.zzal<RoomStatusUpdateListener> {
        zzc(DataHolder dataHolder) {
            super(dataHolder);
        }

        protected abstract void zza(RoomStatusUpdateListener roomStatusUpdateListener, Room room);

        protected final /* synthetic */ void zza(Object obj, DataHolder dataHolder) {
            zza((RoomStatusUpdateListener) obj, GamesClientImpl.zzak(dataHolder));
        }
    }

    static abstract class zza extends zzc {
        private final ArrayList<String> zzhej = new ArrayList();

        zza(DataHolder dataHolder, String[] strArr) {
            super(dataHolder);
            for (Object add : strArr) {
                this.zzhej.add(add);
            }
        }

        protected final void zza(RoomStatusUpdateListener roomStatusUpdateListener, Room room) {
            zza(roomStatusUpdateListener, room, this.zzhej);
        }

        protected abstract void zza(RoomStatusUpdateListener roomStatusUpdateListener, Room room, ArrayList<String> arrayList);
    }

    static class zzw extends com.google.android.gms.common.api.internal.zzam {
        protected zzw(DataHolder dataHolder) {
            super(dataHolder, GamesStatusCodes.zzdb(dataHolder.getStatusCode()));
        }
    }

    static class zzcr extends zzw {
        private TurnBasedMatch zzhfj;

        zzcr(DataHolder dataHolder) {
            super(dataHolder);
            AbstractDataBuffer turnBasedMatchBuffer = new TurnBasedMatchBuffer(dataHolder);
            try {
                if (turnBasedMatchBuffer.getCount() > 0) {
                    this.zzhfj = (TurnBasedMatch) ((TurnBasedMatch) turnBasedMatchBuffer.get(0)).freeze();
                } else {
                    this.zzhfj = null;
                }
                turnBasedMatchBuffer.release();
            } catch (Throwable th) {
                turnBasedMatchBuffer.release();
            }
        }

        public TurnBasedMatch getMatch() {
            return this.zzhfj;
        }
    }

    static final class zzaa extends zzcr implements InitiateMatchResult {
        zzaa(DataHolder dataHolder) {
            super(dataHolder);
        }
    }

    static final class zzab extends zza {
        private final com.google.android.gms.common.api.internal.zzcj<OnInvitationReceivedListener> zzghr;

        zzab(com.google.android.gms.common.api.internal.zzcj<OnInvitationReceivedListener> zzcj) {
            this.zzghr = zzcj;
        }

        public final void onInvitationRemoved(String str) {
            this.zzghr.zza(new zzad(str));
        }

        public final void zzn(DataHolder dataHolder) {
            AbstractDataBuffer invitationBuffer = new InvitationBuffer(dataHolder);
            Invitation invitation = null;
            try {
                if (invitationBuffer.getCount() > 0) {
                    invitation = (Invitation) ((Invitation) invitationBuffer.get(0)).freeze();
                }
                invitationBuffer.release();
                if (invitation != null) {
                    this.zzghr.zza(new zzac(invitation));
                }
            } catch (Throwable th) {
                invitationBuffer.release();
            }
        }
    }

    static final class zzac implements com.google.android.gms.common.api.internal.zzcm<OnInvitationReceivedListener> {
        private final Invitation zzheu;

        zzac(Invitation invitation) {
            this.zzheu = invitation;
        }

        public final void zzagw() {
        }

        public final /* synthetic */ void zzq(Object obj) {
            ((OnInvitationReceivedListener) obj).onInvitationReceived(this.zzheu);
        }
    }

    static final class zzad implements com.google.android.gms.common.api.internal.zzcm<OnInvitationReceivedListener> {
        private final String zzdww;

        zzad(String str) {
            this.zzdww = str;
        }

        public final void zzagw() {
        }

        public final /* synthetic */ void zzq(Object obj) {
            ((OnInvitationReceivedListener) obj).onInvitationRemoved(this.zzdww);
        }
    }

    static final class zzae extends zza {
        private final com.google.android.gms.common.api.internal.zzn<LoadInvitationsResult> zzfwc;

        zzae(com.google.android.gms.common.api.internal.zzn<LoadInvitationsResult> zzn) {
            this.zzfwc = (com.google.android.gms.common.api.internal.zzn) com.google.android.gms.common.internal.zzbp.zzb((Object) zzn, (Object) "Holder must not be null");
        }

        public final void zzm(DataHolder dataHolder) {
            this.zzfwc.setResult(new zzao(dataHolder));
        }
    }

    static abstract class zzb extends com.google.android.gms.common.api.internal.zzal<RoomUpdateListener> {
        zzb(DataHolder dataHolder) {
            super(dataHolder);
        }

        protected abstract void zza(RoomUpdateListener roomUpdateListener, Room room, int i);

        protected final /* synthetic */ void zza(Object obj, DataHolder dataHolder) {
            zza((RoomUpdateListener) obj, GamesClientImpl.zzak(dataHolder), dataHolder.getStatusCode());
        }
    }

    static final class zzaf extends zzb {
        public zzaf(DataHolder dataHolder) {
            super(dataHolder);
        }

        public final void zza(RoomUpdateListener roomUpdateListener, Room room, int i) {
            roomUpdateListener.onJoinedRoom(i, room);
        }
    }

    static final class zzag extends zzw implements LeaderboardMetadataResult {
        private final LeaderboardBuffer zzhev;

        zzag(DataHolder dataHolder) {
            super(dataHolder);
            this.zzhev = new LeaderboardBuffer(dataHolder);
        }

        public final LeaderboardBuffer getLeaderboards() {
            return this.zzhev;
        }
    }

    static final class zzah extends zza {
        private final com.google.android.gms.common.api.internal.zzn<LoadScoresResult> zzfwc;

        zzah(com.google.android.gms.common.api.internal.zzn<LoadScoresResult> zzn) {
            this.zzfwc = (com.google.android.gms.common.api.internal.zzn) com.google.android.gms.common.internal.zzbp.zzb((Object) zzn, (Object) "Holder must not be null");
        }

        public final void zza(DataHolder dataHolder, DataHolder dataHolder2) {
            this.zzfwc.setResult(new zzaw(dataHolder, dataHolder2));
        }
    }

    static final class zzai extends zza {
        private final com.google.android.gms.common.api.internal.zzn<LeaderboardMetadataResult> zzfwc;

        zzai(com.google.android.gms.common.api.internal.zzn<LeaderboardMetadataResult> zzn) {
            this.zzfwc = (com.google.android.gms.common.api.internal.zzn) com.google.android.gms.common.internal.zzbp.zzb((Object) zzn, (Object) "Holder must not be null");
        }

        public final void zzh(DataHolder dataHolder) {
            this.zzfwc.setResult(new zzag(dataHolder));
        }
    }

    static final class zzaj extends zzcr implements LeaveMatchResult {
        zzaj(DataHolder dataHolder) {
            super(dataHolder);
        }
    }

    static final class zzak implements com.google.android.gms.common.api.internal.zzcm<RoomUpdateListener> {
        private final int zzezx;
        private final String zzhew;

        zzak(int i, String str) {
            this.zzezx = i;
            this.zzhew = str;
        }

        public final void zzagw() {
        }

        public final /* synthetic */ void zzq(Object obj) {
            ((RoomUpdateListener) obj).onLeftRoom(this.zzezx, this.zzhew);
        }
    }

    static final class zzal extends zzw implements LoadAchievementsResult {
        private final AchievementBuffer zzhex;

        zzal(DataHolder dataHolder) {
            super(dataHolder);
            this.zzhex = new AchievementBuffer(dataHolder);
        }

        public final AchievementBuffer getAchievements() {
            return this.zzhex;
        }
    }

    static final class zzam extends zzw implements LoadEventsResult {
        private final EventBuffer zzhey;

        zzam(DataHolder dataHolder) {
            super(dataHolder);
            this.zzhey = new EventBuffer(dataHolder);
        }

        public final EventBuffer getEvents() {
            return this.zzhey;
        }
    }

    static final class zzan extends zzw implements LoadGamesResult {
        private final GameBuffer zzhez;

        zzan(DataHolder dataHolder) {
            super(dataHolder);
            this.zzhez = new GameBuffer(dataHolder);
        }

        public final GameBuffer getGames() {
            return this.zzhez;
        }
    }

    static final class zzao extends zzw implements LoadInvitationsResult {
        private final InvitationBuffer zzhfa;

        zzao(DataHolder dataHolder) {
            super(dataHolder);
            this.zzhfa = new InvitationBuffer(dataHolder);
        }

        public final InvitationBuffer getInvitations() {
            return this.zzhfa;
        }
    }

    static final class zzap extends zzcr implements LoadMatchResult {
        zzap(DataHolder dataHolder) {
            super(dataHolder);
        }
    }

    static final class zzaq implements LoadMatchesResult {
        private final Status mStatus;
        private final LoadMatchesResponse zzhfb;

        zzaq(Status status, Bundle bundle) {
            this.mStatus = status;
            this.zzhfb = new LoadMatchesResponse(bundle);
        }

        public final LoadMatchesResponse getMatches() {
            return this.zzhfb;
        }

        public final Status getStatus() {
            return this.mStatus;
        }

        public final void release() {
            this.zzhfb.release();
        }
    }

    static final class zzar extends zzw implements LoadPlayerScoreResult {
        private final LeaderboardScoreEntity zzhfc;

        zzar(DataHolder dataHolder) {
            super(dataHolder);
            AbstractDataBuffer leaderboardScoreBuffer = new LeaderboardScoreBuffer(dataHolder);
            try {
                if (leaderboardScoreBuffer.getCount() > 0) {
                    this.zzhfc = (LeaderboardScoreEntity) ((LeaderboardScore) leaderboardScoreBuffer.get(0)).freeze();
                } else {
                    this.zzhfc = null;
                }
                leaderboardScoreBuffer.release();
            } catch (Throwable th) {
                leaderboardScoreBuffer.release();
            }
        }

        public final LeaderboardScore getScore() {
            return this.zzhfc;
        }
    }

    static final class zzas extends zzw implements LoadPlayerStatsResult {
        private final PlayerStats zzhfd;

        zzas(DataHolder dataHolder) {
            super(dataHolder);
            AbstractDataBuffer playerStatsBuffer = new PlayerStatsBuffer(dataHolder);
            try {
                if (playerStatsBuffer.getCount() > 0) {
                    this.zzhfd = new com.google.android.gms.games.stats.zza((PlayerStats) playerStatsBuffer.get(0));
                } else {
                    this.zzhfd = null;
                }
                playerStatsBuffer.release();
            } catch (Throwable th) {
                playerStatsBuffer.release();
            }
        }

        public final PlayerStats getPlayerStats() {
            return this.zzhfd;
        }
    }

    static final class zzat extends zzw implements LoadPlayersResult {
        private final PlayerBuffer zzhfe;

        zzat(DataHolder dataHolder) {
            super(dataHolder);
            this.zzhfe = new PlayerBuffer(dataHolder);
        }

        public final PlayerBuffer getPlayers() {
            return this.zzhfe;
        }
    }

    static final class zzau extends zzw implements LoadQuestsResult {
        private final DataHolder zzfkz;

        zzau(DataHolder dataHolder) {
            super(dataHolder);
            this.zzfkz = dataHolder;
        }

        public final QuestBuffer getQuests() {
            return new QuestBuffer(this.zzfkz);
        }
    }

    static final class zzav implements LoadRequestsResult {
        private final Status mStatus;
        private final Bundle zzhff;

        zzav(Status status, Bundle bundle) {
            this.mStatus = status;
            this.zzhff = bundle;
        }

        public final GameRequestBuffer getRequests(int i) {
            String str;
            switch (i) {
                case 1:
                    str = "GIFT";
                    break;
                case 2:
                    str = "WISH";
                    break;
                default:
                    zze.zzz("RequestType", "Unknown request type: " + i);
                    str = "UNKNOWN_TYPE";
                    break;
            }
            return !this.zzhff.containsKey(str) ? null : new GameRequestBuffer((DataHolder) this.zzhff.get(str));
        }

        public final Status getStatus() {
            return this.mStatus;
        }

        public final void release() {
            for (String parcelable : this.zzhff.keySet()) {
                DataHolder dataHolder = (DataHolder) this.zzhff.getParcelable(parcelable);
                if (dataHolder != null) {
                    dataHolder.close();
                }
            }
        }
    }

    static final class zzaw extends zzw implements LoadScoresResult {
        private final LeaderboardEntity zzhfg;
        private final LeaderboardScoreBuffer zzhfh;

        zzaw(DataHolder dataHolder, DataHolder dataHolder2) {
            super(dataHolder2);
            AbstractDataBuffer leaderboardBuffer = new LeaderboardBuffer(dataHolder);
            try {
                if (leaderboardBuffer.getCount() > 0) {
                    this.zzhfg = (LeaderboardEntity) ((Leaderboard) leaderboardBuffer.get(0)).freeze();
                } else {
                    this.zzhfg = null;
                }
                leaderboardBuffer.release();
                this.zzhfh = new LeaderboardScoreBuffer(dataHolder2);
            } catch (Throwable th) {
                leaderboardBuffer.release();
            }
        }

        public final Leaderboard getLeaderboard() {
            return this.zzhfg;
        }

        public final LeaderboardScoreBuffer getScores() {
            return this.zzhfh;
        }
    }

    static final class zzax extends zzw implements LoadSnapshotsResult {
        zzax(DataHolder dataHolder) {
            super(dataHolder);
        }

        public final SnapshotMetadataBuffer getSnapshots() {
            return new SnapshotMetadataBuffer(this.zzfkz);
        }
    }

    static final class zzay implements com.google.android.gms.common.api.internal.zzcm<OnTurnBasedMatchUpdateReceivedListener> {
        private final String zzhfi;

        zzay(String str) {
            this.zzhfi = str;
        }

        public final void zzagw() {
        }

        public final /* synthetic */ void zzq(Object obj) {
            ((OnTurnBasedMatchUpdateReceivedListener) obj).onTurnBasedMatchRemoved(this.zzhfi);
        }
    }

    static final class zzaz extends zza {
        private final com.google.android.gms.common.api.internal.zzcj<OnTurnBasedMatchUpdateReceivedListener> zzghr;

        zzaz(com.google.android.gms.common.api.internal.zzcj<OnTurnBasedMatchUpdateReceivedListener> zzcj) {
            this.zzghr = zzcj;
        }

        public final void onTurnBasedMatchRemoved(String str) {
            this.zzghr.zza(new zzay(str));
        }

        public final void zzt(DataHolder dataHolder) {
            AbstractDataBuffer turnBasedMatchBuffer = new TurnBasedMatchBuffer(dataHolder);
            TurnBasedMatch turnBasedMatch = null;
            try {
                if (turnBasedMatchBuffer.getCount() > 0) {
                    turnBasedMatch = (TurnBasedMatch) ((TurnBasedMatch) turnBasedMatchBuffer.get(0)).freeze();
                }
                turnBasedMatchBuffer.release();
                if (turnBasedMatch != null) {
                    this.zzghr.zza(new zzba(turnBasedMatch));
                }
            } catch (Throwable th) {
                turnBasedMatchBuffer.release();
            }
        }
    }

    static final class zzba implements com.google.android.gms.common.api.internal.zzcm<OnTurnBasedMatchUpdateReceivedListener> {
        private final TurnBasedMatch zzhfj;

        zzba(TurnBasedMatch turnBasedMatch) {
            this.zzhfj = turnBasedMatch;
        }

        public final void zzagw() {
        }

        public final /* synthetic */ void zzq(Object obj) {
            ((OnTurnBasedMatchUpdateReceivedListener) obj).onTurnBasedMatchReceived(this.zzhfj);
        }
    }

    static final class zzbb implements com.google.android.gms.common.api.internal.zzcm<RealTimeMessageReceivedListener> {
        private final RealTimeMessage zzhfk;

        zzbb(RealTimeMessage realTimeMessage) {
            this.zzhfk = realTimeMessage;
        }

        public final void zzagw() {
        }

        public final /* synthetic */ void zzq(Object obj) {
            ((RealTimeMessageReceivedListener) obj).onRealTimeMessageReceived(this.zzhfk);
        }
    }

    static final class zzbc extends zzw implements OpenSnapshotResult {
        private final Snapshot zzhfl;
        private final String zzhfm;
        private final Snapshot zzhfn;
        private final com.google.android.gms.drive.zzc zzhfo;
        private final SnapshotContents zzhfp;

        zzbc(DataHolder dataHolder, com.google.android.gms.drive.zzc zzc) {
            this(dataHolder, null, zzc, null, null);
        }

        zzbc(DataHolder dataHolder, String str, com.google.android.gms.drive.zzc zzc, com.google.android.gms.drive.zzc zzc2, com.google.android.gms.drive.zzc zzc3) {
            boolean z = false;
            super(dataHolder);
            AbstractDataBuffer snapshotMetadataBuffer = new SnapshotMetadataBuffer(dataHolder);
            try {
                if (snapshotMetadataBuffer.getCount() == 0) {
                    this.zzhfl = null;
                    this.zzhfn = null;
                } else if (snapshotMetadataBuffer.getCount() == 1) {
                    if (dataHolder.getStatusCode() != GamesStatusCodes.STATUS_SNAPSHOT_CONFLICT) {
                        z = true;
                    }
                    com.google.android.gms.common.internal.zzc.zzbg(z);
                    this.zzhfl = new SnapshotEntity(new SnapshotMetadataEntity((SnapshotMetadata) snapshotMetadataBuffer.get(0)), new com.google.android.gms.games.snapshot.zza(zzc));
                    this.zzhfn = null;
                } else {
                    this.zzhfl = new SnapshotEntity(new SnapshotMetadataEntity((SnapshotMetadata) snapshotMetadataBuffer.get(0)), new com.google.android.gms.games.snapshot.zza(zzc));
                    this.zzhfn = new SnapshotEntity(new SnapshotMetadataEntity((SnapshotMetadata) snapshotMetadataBuffer.get(1)), new com.google.android.gms.games.snapshot.zza(zzc2));
                }
                snapshotMetadataBuffer.release();
                this.zzhfm = str;
                this.zzhfo = zzc3;
                this.zzhfp = new com.google.android.gms.games.snapshot.zza(zzc3);
            } catch (Throwable th) {
                snapshotMetadataBuffer.release();
            }
        }

        public final String getConflictId() {
            return this.zzhfm;
        }

        public final Snapshot getConflictingSnapshot() {
            return this.zzhfn;
        }

        public final SnapshotContents getResolutionSnapshotContents() {
            return this.zzhfp;
        }

        public final Snapshot getSnapshot() {
            return this.zzhfl;
        }
    }

    static final class zzbd implements com.google.android.gms.common.api.internal.zzcm<RoomStatusUpdateListener> {
        private final String zzhfq;

        zzbd(String str) {
            this.zzhfq = str;
        }

        public final void zzagw() {
        }

        public final /* synthetic */ void zzq(Object obj) {
            ((RoomStatusUpdateListener) obj).onP2PConnected(this.zzhfq);
        }
    }

    static final class zzbe implements com.google.android.gms.common.api.internal.zzcm<RoomStatusUpdateListener> {
        private final String zzhfq;

        zzbe(String str) {
            this.zzhfq = str;
        }

        public final void zzagw() {
        }

        public final /* synthetic */ void zzq(Object obj) {
            ((RoomStatusUpdateListener) obj).onP2PDisconnected(this.zzhfq);
        }
    }

    static final class zzbf extends zza {
        zzbf(DataHolder dataHolder, String[] strArr) {
            super(dataHolder, strArr);
        }

        protected final void zza(RoomStatusUpdateListener roomStatusUpdateListener, Room room, ArrayList<String> arrayList) {
            roomStatusUpdateListener.onPeersConnected(room, arrayList);
        }
    }

    static final class zzbg extends zza {
        zzbg(DataHolder dataHolder, String[] strArr) {
            super(dataHolder, strArr);
        }

        protected final void zza(RoomStatusUpdateListener roomStatusUpdateListener, Room room, ArrayList<String> arrayList) {
            roomStatusUpdateListener.onPeerDeclined(room, arrayList);
        }
    }

    static final class zzbh extends zza {
        zzbh(DataHolder dataHolder, String[] strArr) {
            super(dataHolder, strArr);
        }

        protected final void zza(RoomStatusUpdateListener roomStatusUpdateListener, Room room, ArrayList<String> arrayList) {
            roomStatusUpdateListener.onPeersDisconnected(room, arrayList);
        }
    }

    static final class zzbi extends zza {
        zzbi(DataHolder dataHolder, String[] strArr) {
            super(dataHolder, strArr);
        }

        protected final void zza(RoomStatusUpdateListener roomStatusUpdateListener, Room room, ArrayList<String> arrayList) {
            roomStatusUpdateListener.onPeerInvitedToRoom(room, arrayList);
        }
    }

    static final class zzbj extends zza {
        zzbj(DataHolder dataHolder, String[] strArr) {
            super(dataHolder, strArr);
        }

        protected final void zza(RoomStatusUpdateListener roomStatusUpdateListener, Room room, ArrayList<String> arrayList) {
            roomStatusUpdateListener.onPeerJoined(room, arrayList);
        }
    }

    static final class zzbk extends zza {
        zzbk(DataHolder dataHolder, String[] strArr) {
            super(dataHolder, strArr);
        }

        protected final void zza(RoomStatusUpdateListener roomStatusUpdateListener, Room room, ArrayList<String> arrayList) {
            roomStatusUpdateListener.onPeerLeft(room, arrayList);
        }
    }

    static final class zzbl extends zza {
        private final com.google.android.gms.common.api.internal.zzn<LoadPlayerScoreResult> zzfwc;

        zzbl(com.google.android.gms.common.api.internal.zzn<LoadPlayerScoreResult> zzn) {
            this.zzfwc = (com.google.android.gms.common.api.internal.zzn) com.google.android.gms.common.internal.zzbp.zzb((Object) zzn, (Object) "Holder must not be null");
        }

        public final void zzab(DataHolder dataHolder) {
            this.zzfwc.setResult(new zzar(dataHolder));
        }
    }

    static final class zzbm extends zza {
        private final com.google.android.gms.common.api.internal.zzn<LoadPlayerStatsResult> zzfwc;

        public zzbm(com.google.android.gms.common.api.internal.zzn<LoadPlayerStatsResult> zzn) {
            this.zzfwc = (com.google.android.gms.common.api.internal.zzn) com.google.android.gms.common.internal.zzbp.zzb((Object) zzn, (Object) "Holder must not be null");
        }

        public final void zzaj(DataHolder dataHolder) {
            this.zzfwc.setResult(new zzas(dataHolder));
        }
    }

    static final class zzbn extends zza {
        private final com.google.android.gms.common.api.internal.zzn<LoadPlayersResult> zzfwc;

        zzbn(com.google.android.gms.common.api.internal.zzn<LoadPlayersResult> zzn) {
            this.zzfwc = (com.google.android.gms.common.api.internal.zzn) com.google.android.gms.common.internal.zzbp.zzb((Object) zzn, (Object) "Holder must not be null");
        }

        public final void zzj(DataHolder dataHolder) {
            this.zzfwc.setResult(new zzat(dataHolder));
        }

        public final void zzk(DataHolder dataHolder) {
            this.zzfwc.setResult(new zzat(dataHolder));
        }
    }

    static final class zzbo extends zzb {
        private final zzn zzhec;

        public zzbo(zzn zzn) {
            this.zzhec = zzn;
        }

        public final zzl zzapu() {
            return new zzl(this.zzhec.zzhgu);
        }
    }

    static final class zzbp extends zza {
        private final com.google.android.gms.common.api.internal.zzn<AcceptQuestResult> zzhfr;

        public zzbp(com.google.android.gms.common.api.internal.zzn<AcceptQuestResult> zzn) {
            this.zzhfr = (com.google.android.gms.common.api.internal.zzn) com.google.android.gms.common.internal.zzbp.zzb((Object) zzn, (Object) "Holder must not be null");
        }

        public final void zzag(DataHolder dataHolder) {
            this.zzhfr.setResult(new zzd(dataHolder));
        }
    }

    static final class zzbq implements com.google.android.gms.common.api.internal.zzcm<QuestUpdateListener> {
        private final Quest zzhek;

        zzbq(Quest quest) {
            this.zzhek = quest;
        }

        public final void zzagw() {
        }

        public final /* synthetic */ void zzq(Object obj) {
            ((QuestUpdateListener) obj).onQuestCompleted(this.zzhek);
        }
    }

    static final class zzbr extends zza {
        private final com.google.android.gms.common.api.internal.zzn<ClaimMilestoneResult> zzhfs;
        private final String zzhft;

        public zzbr(com.google.android.gms.common.api.internal.zzn<ClaimMilestoneResult> zzn, String str) {
            this.zzhfs = (com.google.android.gms.common.api.internal.zzn) com.google.android.gms.common.internal.zzbp.zzb((Object) zzn, (Object) "Holder must not be null");
            this.zzhft = (String) com.google.android.gms.common.internal.zzbp.zzb((Object) str, (Object) "MilestoneId must not be null");
        }

        public final void zzaf(DataHolder dataHolder) {
            this.zzhfs.setResult(new zzp(dataHolder, this.zzhft));
        }
    }

    static final class zzbs extends zza {
        private final com.google.android.gms.common.api.internal.zzcj<QuestUpdateListener> zzghr;

        zzbs(com.google.android.gms.common.api.internal.zzcj<QuestUpdateListener> zzcj) {
            this.zzghr = zzcj;
        }

        private static Quest zzam(DataHolder dataHolder) {
            AbstractDataBuffer questBuffer = new QuestBuffer(dataHolder);
            Quest quest = null;
            try {
                if (questBuffer.getCount() > 0) {
                    quest = (Quest) ((Quest) questBuffer.get(0)).freeze();
                }
                questBuffer.release();
                return quest;
            } catch (Throwable th) {
                questBuffer.release();
            }
        }

        public final void zzah(DataHolder dataHolder) {
            Quest zzam = zzam(dataHolder);
            if (zzam != null) {
                this.zzghr.zza(new zzbq(zzam));
            }
        }
    }

    static final class zzbt extends zza {
        private final com.google.android.gms.common.api.internal.zzn<LoadQuestsResult> zzhfu;

        public zzbt(com.google.android.gms.common.api.internal.zzn<LoadQuestsResult> zzn) {
            this.zzhfu = (com.google.android.gms.common.api.internal.zzn) com.google.android.gms.common.internal.zzbp.zzb((Object) zzn, (Object) "Holder must not be null");
        }

        public final void zzai(DataHolder dataHolder) {
            this.zzhfu.setResult(new zzau(dataHolder));
        }
    }

    static final class zzbu implements com.google.android.gms.common.api.internal.zzcm<ReliableMessageSentCallback> {
        private final int zzezx;
        private final String zzhfv;
        private final int zzhfw;

        zzbu(int i, int i2, String str) {
            this.zzezx = i;
            this.zzhfw = i2;
            this.zzhfv = str;
        }

        public final void zzagw() {
        }

        public final /* synthetic */ void zzq(Object obj) {
            ReliableMessageSentCallback reliableMessageSentCallback = (ReliableMessageSentCallback) obj;
            if (reliableMessageSentCallback != null) {
                reliableMessageSentCallback.onRealTimeMessageSent(this.zzezx, this.zzhfw, this.zzhfv);
            }
        }
    }

    static final class zzbv extends zza {
        private com.google.android.gms.common.api.internal.zzcj<ReliableMessageSentCallback> zzhfx;

        public zzbv(com.google.android.gms.common.api.internal.zzcj<ReliableMessageSentCallback> zzcj) {
            this.zzhfx = zzcj;
        }

        public final void zzb(int i, int i2, String str) {
            if (this.zzhfx != null) {
                this.zzhfx.zza(new zzbu(i, i2, str));
            }
        }
    }

    static final class zzbw extends zza {
        private final com.google.android.gms.common.api.internal.zzcj<OnRequestReceivedListener> zzghr;

        zzbw(com.google.android.gms.common.api.internal.zzcj<OnRequestReceivedListener> zzcj) {
            this.zzghr = zzcj;
        }

        public final void onRequestRemoved(String str) {
            this.zzghr.zza(new zzby(str));
        }

        public final void zzo(DataHolder dataHolder) {
            AbstractDataBuffer gameRequestBuffer = new GameRequestBuffer(dataHolder);
            GameRequest gameRequest = null;
            try {
                if (gameRequestBuffer.getCount() > 0) {
                    gameRequest = (GameRequest) ((GameRequest) gameRequestBuffer.get(0)).freeze();
                }
                gameRequestBuffer.release();
                if (gameRequest != null) {
                    this.zzghr.zza(new zzbx(gameRequest));
                }
            } catch (Throwable th) {
                gameRequestBuffer.release();
            }
        }
    }

    static final class zzbx implements com.google.android.gms.common.api.internal.zzcm<OnRequestReceivedListener> {
        private final GameRequest zzhfy;

        zzbx(GameRequest gameRequest) {
            this.zzhfy = gameRequest;
        }

        public final void zzagw() {
        }

        public final /* synthetic */ void zzq(Object obj) {
            ((OnRequestReceivedListener) obj).onRequestReceived(this.zzhfy);
        }
    }

    static final class zzby implements com.google.android.gms.common.api.internal.zzcm<OnRequestReceivedListener> {
        private final String zzcjq;

        zzby(String str) {
            this.zzcjq = str;
        }

        public final void zzagw() {
        }

        public final /* synthetic */ void zzq(Object obj) {
            ((OnRequestReceivedListener) obj).onRequestRemoved(this.zzcjq);
        }
    }

    static final class zzbz extends zza {
        private final com.google.android.gms.common.api.internal.zzn<LoadRequestsResult> zzhfz;

        public zzbz(com.google.android.gms.common.api.internal.zzn<LoadRequestsResult> zzn) {
            this.zzhfz = (com.google.android.gms.common.api.internal.zzn) com.google.android.gms.common.internal.zzbp.zzb((Object) zzn, (Object) "Holder must not be null");
        }

        public final void zzc(int i, Bundle bundle) {
            bundle.setClassLoader(getClass().getClassLoader());
            this.zzhfz.setResult(new zzav(GamesStatusCodes.zzdb(i), bundle));
        }
    }

    static final class zzca extends zza {
        private final com.google.android.gms.common.api.internal.zzn<UpdateRequestsResult> zzhga;

        public zzca(com.google.android.gms.common.api.internal.zzn<UpdateRequestsResult> zzn) {
            this.zzhga = (com.google.android.gms.common.api.internal.zzn) com.google.android.gms.common.internal.zzbp.zzb((Object) zzn, (Object) "Holder must not be null");
        }

        public final void zzac(DataHolder dataHolder) {
            this.zzhga.setResult(new zzcw(dataHolder));
        }
    }

    static final class zzcb extends zzc {
        zzcb(DataHolder dataHolder) {
            super(dataHolder);
        }

        public final void zza(RoomStatusUpdateListener roomStatusUpdateListener, Room room) {
            roomStatusUpdateListener.onRoomAutoMatching(room);
        }
    }

    static final class zzcc extends zza {
        private final com.google.android.gms.common.api.internal.zzcj<? extends RoomUpdateListener> zzhgb;
        private final com.google.android.gms.common.api.internal.zzcj<? extends RoomStatusUpdateListener> zzhgc;
        private final com.google.android.gms.common.api.internal.zzcj<RealTimeMessageReceivedListener> zzhgd;

        public zzcc(com.google.android.gms.common.api.internal.zzcj<RoomUpdateListener> zzcj) {
            this.zzhgb = (com.google.android.gms.common.api.internal.zzcj) com.google.android.gms.common.internal.zzbp.zzb((Object) zzcj, (Object) "Callbacks must not be null");
            this.zzhgc = null;
            this.zzhgd = null;
        }

        public zzcc(com.google.android.gms.common.api.internal.zzcj<? extends RoomUpdateListener> zzcj, com.google.android.gms.common.api.internal.zzcj<? extends RoomStatusUpdateListener> zzcj2, com.google.android.gms.common.api.internal.zzcj<RealTimeMessageReceivedListener> zzcj3) {
            this.zzhgb = (com.google.android.gms.common.api.internal.zzcj) com.google.android.gms.common.internal.zzbp.zzb((Object) zzcj, (Object) "Callbacks must not be null");
            this.zzhgc = zzcj2;
            this.zzhgd = zzcj3;
        }

        public final void onLeftRoom(int i, String str) {
            this.zzhgb.zza(new zzak(i, str));
        }

        public final void onP2PConnected(String str) {
            if (this.zzhgc != null) {
                this.zzhgc.zza(new zzbd(str));
            }
        }

        public final void onP2PDisconnected(String str) {
            if (this.zzhgc != null) {
                this.zzhgc.zza(new zzbe(str));
            }
        }

        public final void onRealTimeMessageReceived(RealTimeMessage realTimeMessage) {
            if (this.zzhgd != null) {
                this.zzhgd.zza(new zzbb(realTimeMessage));
            }
        }

        public final void zza(DataHolder dataHolder, String[] strArr) {
            if (this.zzhgc != null) {
                this.zzhgc.zza(new zzbi(dataHolder, strArr));
            }
        }

        public final void zzaa(DataHolder dataHolder) {
            if (this.zzhgc != null) {
                this.zzhgc.zza(new zzt(dataHolder));
            }
        }

        public final void zzb(DataHolder dataHolder, String[] strArr) {
            if (this.zzhgc != null) {
                this.zzhgc.zza(new zzbj(dataHolder, strArr));
            }
        }

        public final void zzc(DataHolder dataHolder, String[] strArr) {
            if (this.zzhgc != null) {
                this.zzhgc.zza(new zzbk(dataHolder, strArr));
            }
        }

        public final void zzd(DataHolder dataHolder, String[] strArr) {
            if (this.zzhgc != null) {
                this.zzhgc.zza(new zzbg(dataHolder, strArr));
            }
        }

        public final void zze(DataHolder dataHolder, String[] strArr) {
            if (this.zzhgc != null) {
                this.zzhgc.zza(new zzbf(dataHolder, strArr));
            }
        }

        public final void zzf(DataHolder dataHolder, String[] strArr) {
            if (this.zzhgc != null) {
                this.zzhgc.zza(new zzbh(dataHolder, strArr));
            }
        }

        public final void zzu(DataHolder dataHolder) {
            this.zzhgb.zza(new zzcf(dataHolder));
        }

        public final void zzv(DataHolder dataHolder) {
            this.zzhgb.zza(new zzaf(dataHolder));
        }

        public final void zzw(DataHolder dataHolder) {
            if (this.zzhgc != null) {
                this.zzhgc.zza(new zzce(dataHolder));
            }
        }

        public final void zzx(DataHolder dataHolder) {
            if (this.zzhgc != null) {
                this.zzhgc.zza(new zzcb(dataHolder));
            }
        }

        public final void zzy(DataHolder dataHolder) {
            this.zzhgb.zza(new zzcd(dataHolder));
        }

        public final void zzz(DataHolder dataHolder) {
            if (this.zzhgc != null) {
                this.zzhgc.zza(new zzr(dataHolder));
            }
        }
    }

    static final class zzcd extends zzb {
        zzcd(DataHolder dataHolder) {
            super(dataHolder);
        }

        public final void zza(RoomUpdateListener roomUpdateListener, Room room, int i) {
            roomUpdateListener.onRoomConnected(i, room);
        }
    }

    static final class zzce extends zzc {
        zzce(DataHolder dataHolder) {
            super(dataHolder);
        }

        public final void zza(RoomStatusUpdateListener roomStatusUpdateListener, Room room) {
            roomStatusUpdateListener.onRoomConnecting(room);
        }
    }

    static final class zzcf extends zzb {
        public zzcf(DataHolder dataHolder) {
            super(dataHolder);
        }

        public final void zza(RoomUpdateListener roomUpdateListener, Room room, int i) {
            roomUpdateListener.onRoomCreated(i, room);
        }
    }

    static final class zzcg extends zza {
        private final com.google.android.gms.common.api.internal.zzn<Status> zzfwc;

        public zzcg(com.google.android.gms.common.api.internal.zzn<Status> zzn) {
            this.zzfwc = (com.google.android.gms.common.api.internal.zzn) com.google.android.gms.common.internal.zzbp.zzb((Object) zzn, (Object) "Holder must not be null");
        }

        public final void zzapt() {
            this.zzfwc.setResult(GamesStatusCodes.zzdb(0));
        }
    }

    static final class zzch extends zza {
        private final com.google.android.gms.common.api.internal.zzn<CommitSnapshotResult> zzhge;

        public zzch(com.google.android.gms.common.api.internal.zzn<CommitSnapshotResult> zzn) {
            this.zzhge = (com.google.android.gms.common.api.internal.zzn) com.google.android.gms.common.internal.zzbp.zzb((Object) zzn, (Object) "Holder must not be null");
        }

        public final void zzae(DataHolder dataHolder) {
            this.zzhge.setResult(new zzq(dataHolder));
        }
    }

    static final class zzci extends zza {
        private final com.google.android.gms.common.api.internal.zzn<DeleteSnapshotResult> zzfwc;

        public zzci(com.google.android.gms.common.api.internal.zzn<DeleteSnapshotResult> zzn) {
            this.zzfwc = (com.google.android.gms.common.api.internal.zzn) com.google.android.gms.common.internal.zzbp.zzb((Object) zzn, (Object) "Holder must not be null");
        }

        public final void zzj(int i, String str) {
            this.zzfwc.setResult(new zzs(i, str));
        }
    }

    static final class zzcj extends zza {
        private final com.google.android.gms.common.api.internal.zzn<OpenSnapshotResult> zzhgf;

        public zzcj(com.google.android.gms.common.api.internal.zzn<OpenSnapshotResult> zzn) {
            this.zzhgf = (com.google.android.gms.common.api.internal.zzn) com.google.android.gms.common.internal.zzbp.zzb((Object) zzn, (Object) "Holder must not be null");
        }

        public final void zza(DataHolder dataHolder, com.google.android.gms.drive.zzc zzc) {
            this.zzhgf.setResult(new zzbc(dataHolder, zzc));
        }

        public final void zza(DataHolder dataHolder, String str, com.google.android.gms.drive.zzc zzc, com.google.android.gms.drive.zzc zzc2, com.google.android.gms.drive.zzc zzc3) {
            this.zzhgf.setResult(new zzbc(dataHolder, str, zzc, zzc2, zzc3));
        }
    }

    static final class zzck extends zza {
        private final com.google.android.gms.common.api.internal.zzn<LoadSnapshotsResult> zzhgg;

        public zzck(com.google.android.gms.common.api.internal.zzn<LoadSnapshotsResult> zzn) {
            this.zzhgg = (com.google.android.gms.common.api.internal.zzn) com.google.android.gms.common.internal.zzbp.zzb((Object) zzn, (Object) "Holder must not be null");
        }

        public final void zzad(DataHolder dataHolder) {
            this.zzhgg.setResult(new zzax(dataHolder));
        }
    }

    static final class zzcl extends zza {
        private final com.google.android.gms.common.api.internal.zzn<SubmitScoreResult> zzfwc;

        public zzcl(com.google.android.gms.common.api.internal.zzn<SubmitScoreResult> zzn) {
            this.zzfwc = (com.google.android.gms.common.api.internal.zzn) com.google.android.gms.common.internal.zzbp.zzb((Object) zzn, (Object) "Holder must not be null");
        }

        public final void zzi(DataHolder dataHolder) {
            this.zzfwc.setResult(new zzcm(dataHolder));
        }
    }

    static final class zzcm extends zzw implements SubmitScoreResult {
        private final ScoreSubmissionData zzhgh;

        public zzcm(DataHolder dataHolder) {
            super(dataHolder);
            try {
                this.zzhgh = new ScoreSubmissionData(dataHolder);
            } finally {
                dataHolder.close();
            }
        }

        public final ScoreSubmissionData getScoreData() {
            return this.zzhgh;
        }
    }

    static final class zzcn extends zza {
        private final com.google.android.gms.common.api.internal.zzn<CancelMatchResult> zzhgi;

        public zzcn(com.google.android.gms.common.api.internal.zzn<CancelMatchResult> zzn) {
            this.zzhgi = (com.google.android.gms.common.api.internal.zzn) com.google.android.gms.common.internal.zzbp.zzb((Object) zzn, (Object) "Holder must not be null");
        }

        public final void zzi(int i, String str) {
            this.zzhgi.setResult(new zzg(GamesStatusCodes.zzdb(i), str));
        }
    }

    static final class zzco extends zza {
        private final com.google.android.gms.common.api.internal.zzn<InitiateMatchResult> zzhgj;

        public zzco(com.google.android.gms.common.api.internal.zzn<InitiateMatchResult> zzn) {
            this.zzhgj = (com.google.android.gms.common.api.internal.zzn) com.google.android.gms.common.internal.zzbp.zzb((Object) zzn, (Object) "Holder must not be null");
        }

        public final void zzq(DataHolder dataHolder) {
            this.zzhgj.setResult(new zzaa(dataHolder));
        }
    }

    static final class zzcp extends zza {
        private final com.google.android.gms.common.api.internal.zzn<LeaveMatchResult> zzhgk;

        public zzcp(com.google.android.gms.common.api.internal.zzn<LeaveMatchResult> zzn) {
            this.zzhgk = (com.google.android.gms.common.api.internal.zzn) com.google.android.gms.common.internal.zzbp.zzb((Object) zzn, (Object) "Holder must not be null");
        }

        public final void zzs(DataHolder dataHolder) {
            this.zzhgk.setResult(new zzaj(dataHolder));
        }
    }

    static final class zzcq extends zza {
        private final com.google.android.gms.common.api.internal.zzn<LoadMatchResult> zzhgl;

        public zzcq(com.google.android.gms.common.api.internal.zzn<LoadMatchResult> zzn) {
            this.zzhgl = (com.google.android.gms.common.api.internal.zzn) com.google.android.gms.common.internal.zzbp.zzb((Object) zzn, (Object) "Holder must not be null");
        }

        public final void zzp(DataHolder dataHolder) {
            this.zzhgl.setResult(new zzap(dataHolder));
        }
    }

    static final class zzcs extends zza {
        private final com.google.android.gms.common.api.internal.zzn<UpdateMatchResult> zzhgm;

        public zzcs(com.google.android.gms.common.api.internal.zzn<UpdateMatchResult> zzn) {
            this.zzhgm = (com.google.android.gms.common.api.internal.zzn) com.google.android.gms.common.internal.zzbp.zzb((Object) zzn, (Object) "Holder must not be null");
        }

        public final void zzr(DataHolder dataHolder) {
            this.zzhgm.setResult(new zzcv(dataHolder));
        }
    }

    static final class zzct extends zza {
        private final com.google.android.gms.common.api.internal.zzn<LoadMatchesResult> zzhgn;

        public zzct(com.google.android.gms.common.api.internal.zzn<LoadMatchesResult> zzn) {
            this.zzhgn = (com.google.android.gms.common.api.internal.zzn) com.google.android.gms.common.internal.zzbp.zzb((Object) zzn, (Object) "Holder must not be null");
        }

        public final void zzb(int i, Bundle bundle) {
            bundle.setClassLoader(getClass().getClassLoader());
            this.zzhgn.setResult(new zzaq(GamesStatusCodes.zzdb(i), bundle));
        }
    }

    static final class zzcu implements UpdateAchievementResult {
        private final Status mStatus;
        private final String zzhdk;

        zzcu(int i, String str) {
            this.mStatus = GamesStatusCodes.zzdb(i);
            this.zzhdk = str;
        }

        public final String getAchievementId() {
            return this.zzhdk;
        }

        public final Status getStatus() {
            return this.mStatus;
        }
    }

    static final class zzcv extends zzcr implements UpdateMatchResult {
        zzcv(DataHolder dataHolder) {
            super(dataHolder);
        }
    }

    static final class zzcw extends zzw implements UpdateRequestsResult {
        private final zzbvl zzhgo;

        zzcw(DataHolder dataHolder) {
            super(dataHolder);
            this.zzhgo = zzbvl.zzan(dataHolder);
        }

        public final Set<String> getRequestIds() {
            return this.zzhgo.getRequestIds();
        }

        public final int getRequestOutcome(String str) {
            return this.zzhgo.getRequestOutcome(str);
        }
    }

    static final class zzd extends zzw implements AcceptQuestResult {
        private final Quest zzhek;

        zzd(DataHolder dataHolder) {
            super(dataHolder);
            AbstractDataBuffer questBuffer = new QuestBuffer(dataHolder);
            try {
                if (questBuffer.getCount() > 0) {
                    this.zzhek = new QuestEntity((Quest) questBuffer.get(0));
                } else {
                    this.zzhek = null;
                }
                questBuffer.release();
            } catch (Throwable th) {
                questBuffer.release();
            }
        }

        public final Quest getQuest() {
            return this.zzhek;
        }
    }

    static final class zze extends zza {
        private final com.google.android.gms.common.api.internal.zzn<UpdateAchievementResult> zzfwc;

        zze(com.google.android.gms.common.api.internal.zzn<UpdateAchievementResult> zzn) {
            this.zzfwc = (com.google.android.gms.common.api.internal.zzn) com.google.android.gms.common.internal.zzbp.zzb((Object) zzn, (Object) "Holder must not be null");
        }

        public final void zzh(int i, String str) {
            this.zzfwc.setResult(new zzcu(i, str));
        }
    }

    static final class zzf extends zza {
        private final com.google.android.gms.common.api.internal.zzn<LoadAchievementsResult> zzfwc;

        zzf(com.google.android.gms.common.api.internal.zzn<LoadAchievementsResult> zzn) {
            this.zzfwc = (com.google.android.gms.common.api.internal.zzn) com.google.android.gms.common.internal.zzbp.zzb((Object) zzn, (Object) "Holder must not be null");
        }

        public final void zzf(DataHolder dataHolder) {
            this.zzfwc.setResult(new zzal(dataHolder));
        }
    }

    static final class zzg implements CancelMatchResult {
        private final Status mStatus;
        private final String zzhel;

        zzg(Status status, String str) {
            this.mStatus = status;
            this.zzhel = str;
        }

        public final String getMatchId() {
            return this.zzhel;
        }

        public final Status getStatus() {
            return this.mStatus;
        }
    }

    static final class zzh extends zza {
        private final com.google.android.gms.common.api.internal.zzn<CaptureAvailableResult> zzfwc;

        zzh(com.google.android.gms.common.api.internal.zzn<CaptureAvailableResult> zzn) {
            this.zzfwc = (com.google.android.gms.common.api.internal.zzn) com.google.android.gms.common.internal.zzbp.zzb((Object) zzn, (Object) "Holder must not be null");
        }

        public final void zzi(int i, boolean z) {
            this.zzfwc.setResult(new zzi(new Status(i), z));
        }
    }

    static final class zzi implements CaptureAvailableResult {
        private final Status mStatus;
        private final boolean zzhem;

        zzi(Status status, boolean z) {
            this.mStatus = status;
            this.zzhem = z;
        }

        public final Status getStatus() {
            return this.mStatus;
        }

        public final boolean isAvailable() {
            return this.zzhem;
        }
    }

    static final class zzj extends zza {
        private final com.google.android.gms.common.api.internal.zzn<CaptureCapabilitiesResult> zzfwc;

        zzj(com.google.android.gms.common.api.internal.zzn<CaptureCapabilitiesResult> zzn) {
            this.zzfwc = (com.google.android.gms.common.api.internal.zzn) com.google.android.gms.common.internal.zzbp.zzb((Object) zzn, (Object) "Holder must not be null");
        }

        public final void zza(int i, VideoCapabilities videoCapabilities) {
            this.zzfwc.setResult(new zzk(new Status(i), videoCapabilities));
        }
    }

    static final class zzk implements CaptureCapabilitiesResult {
        private final Status mStatus;
        private final VideoCapabilities zzhen;

        zzk(Status status, VideoCapabilities videoCapabilities) {
            this.mStatus = status;
            this.zzhen = videoCapabilities;
        }

        public final VideoCapabilities getCapabilities() {
            return this.zzhen;
        }

        public final Status getStatus() {
            return this.mStatus;
        }
    }

    static final class zzl extends zza {
        private final com.google.android.gms.common.api.internal.zzcj<CaptureOverlayStateListener> zzghr;

        zzl(com.google.android.gms.common.api.internal.zzcj<CaptureOverlayStateListener> zzcj) {
            this.zzghr = (com.google.android.gms.common.api.internal.zzcj) com.google.android.gms.common.internal.zzbp.zzb((Object) zzcj, (Object) "Callback must not be null");
        }

        public final void onCaptureOverlayStateChanged(int i) {
            this.zzghr.zza(new zzm(i));
        }
    }

    static final class zzm implements com.google.android.gms.common.api.internal.zzcm<CaptureOverlayStateListener> {
        private final int zzheo;

        zzm(int i) {
            this.zzheo = i;
        }

        public final void zzagw() {
        }

        public final /* synthetic */ void zzq(Object obj) {
            ((CaptureOverlayStateListener) obj).onCaptureOverlayStateChanged(this.zzheo);
        }
    }

    static final class zzn extends zza {
        private final com.google.android.gms.common.api.internal.zzn<CaptureStateResult> zzfwc;

        public zzn(com.google.android.gms.common.api.internal.zzn<CaptureStateResult> zzn) {
            this.zzfwc = (com.google.android.gms.common.api.internal.zzn) com.google.android.gms.common.internal.zzbp.zzb((Object) zzn, (Object) "Holder must not be null");
        }

        public final void zzd(int i, Bundle bundle) {
            this.zzfwc.setResult(new zzo(new Status(i), CaptureState.zzo(bundle)));
        }
    }

    static final class zzo implements CaptureStateResult {
        private final Status mStatus;
        private final CaptureState zzhep;

        zzo(Status status, CaptureState captureState) {
            this.mStatus = status;
            this.zzhep = captureState;
        }

        public final CaptureState getCaptureState() {
            return this.zzhep;
        }

        public final Status getStatus() {
            return this.mStatus;
        }
    }

    static final class zzp extends zzw implements ClaimMilestoneResult {
        private final Quest zzhek;
        private final Milestone zzheq;

        zzp(DataHolder dataHolder, String str) {
            super(dataHolder);
            AbstractDataBuffer questBuffer = new QuestBuffer(dataHolder);
            try {
                if (questBuffer.getCount() > 0) {
                    this.zzhek = new QuestEntity((Quest) questBuffer.get(0));
                    List zzarw = this.zzhek.zzarw();
                    int size = zzarw.size();
                    for (int i = 0; i < size; i++) {
                        if (((Milestone) zzarw.get(i)).getMilestoneId().equals(str)) {
                            this.zzheq = (Milestone) zzarw.get(i);
                            return;
                        }
                    }
                    this.zzheq = null;
                } else {
                    this.zzheq = null;
                    this.zzhek = null;
                }
                questBuffer.release();
            } finally {
                questBuffer.release();
            }
        }

        public final Milestone getMilestone() {
            return this.zzheq;
        }

        public final Quest getQuest() {
            return this.zzhek;
        }
    }

    static final class zzq extends zzw implements CommitSnapshotResult {
        private final SnapshotMetadata zzher;

        zzq(DataHolder dataHolder) {
            super(dataHolder);
            AbstractDataBuffer snapshotMetadataBuffer = new SnapshotMetadataBuffer(dataHolder);
            try {
                if (snapshotMetadataBuffer.getCount() > 0) {
                    this.zzher = new SnapshotMetadataEntity((SnapshotMetadata) snapshotMetadataBuffer.get(0));
                } else {
                    this.zzher = null;
                }
                snapshotMetadataBuffer.release();
            } catch (Throwable th) {
                snapshotMetadataBuffer.release();
            }
        }

        public final SnapshotMetadata getSnapshotMetadata() {
            return this.zzher;
        }
    }

    static final class zzr extends zzc {
        zzr(DataHolder dataHolder) {
            super(dataHolder);
        }

        public final void zza(RoomStatusUpdateListener roomStatusUpdateListener, Room room) {
            roomStatusUpdateListener.onConnectedToRoom(room);
        }
    }

    static final class zzs implements DeleteSnapshotResult {
        private final Status mStatus;
        private final String zzhes;

        zzs(int i, String str) {
            this.mStatus = GamesStatusCodes.zzdb(i);
            this.zzhes = str;
        }

        public final String getSnapshotId() {
            return this.zzhes;
        }

        public final Status getStatus() {
            return this.mStatus;
        }
    }

    static final class zzt extends zzc {
        zzt(DataHolder dataHolder) {
            super(dataHolder);
        }

        public final void zza(RoomStatusUpdateListener roomStatusUpdateListener, Room room) {
            roomStatusUpdateListener.onDisconnectedFromRoom(room);
        }
    }

    static final class zzu extends zza {
        private final com.google.android.gms.common.api.internal.zzn<LoadEventsResult> zzfwc;

        zzu(com.google.android.gms.common.api.internal.zzn<LoadEventsResult> zzn) {
            this.zzfwc = (com.google.android.gms.common.api.internal.zzn) com.google.android.gms.common.internal.zzbp.zzb((Object) zzn, (Object) "Holder must not be null");
        }

        public final void zzg(DataHolder dataHolder) {
            this.zzfwc.setResult(new zzam(dataHolder));
        }
    }

    final class zzv extends zzbvi {
        private /* synthetic */ GamesClientImpl zzhei;

        public zzv(GamesClientImpl gamesClientImpl) {
            this.zzhei = gamesClientImpl;
            super(gamesClientImpl.getContext().getMainLooper(), 1000);
        }

        protected final void zzs(String str, int i) {
            try {
                if (this.zzhei.isConnected()) {
                    ((zzj) this.zzhei.zzajj()).zzp(str, i);
                    return;
                }
                zze.zzz("GamesClientImpl", new StringBuilder(String.valueOf(str).length() + 89).append("Unable to increment event ").append(str).append(" by ").append(i).append(" because the games client is no longer connected").toString());
            } catch (RemoteException e) {
                GamesClientImpl.zzd(e);
            }
        }
    }

    static final class zzx extends zza {
        private final com.google.android.gms.common.api.internal.zzn<LoadGamesResult> zzfwc;

        zzx(com.google.android.gms.common.api.internal.zzn<LoadGamesResult> zzn) {
            this.zzfwc = (com.google.android.gms.common.api.internal.zzn) com.google.android.gms.common.internal.zzbp.zzb((Object) zzn, (Object) "Holder must not be null");
        }

        public final void zzl(DataHolder dataHolder) {
            this.zzfwc.setResult(new zzan(dataHolder));
        }
    }

    static final class zzy extends zza {
        private final com.google.android.gms.common.api.internal.zzn<GetServerAuthCodeResult> zzfwc;

        public zzy(com.google.android.gms.common.api.internal.zzn<GetServerAuthCodeResult> zzn) {
            this.zzfwc = (com.google.android.gms.common.api.internal.zzn) com.google.android.gms.common.internal.zzbp.zzb((Object) zzn, (Object) "Holder must not be null");
        }

        public final void zzg(int i, String str) {
            this.zzfwc.setResult(new zzz(GamesStatusCodes.zzdb(i), str));
        }
    }

    static final class zzz implements GetServerAuthCodeResult {
        private final Status mStatus;
        private final String zzhet;

        zzz(Status status, String str) {
            this.mStatus = status;
            this.zzhet = str;
        }

        public final String getCode() {
            return this.zzhet;
        }

        public final Status getStatus() {
            return this.mStatus;
        }
    }

    public GamesClientImpl(Context context, Looper looper, com.google.android.gms.common.internal.zzq zzq, GamesOptions gamesOptions, ConnectionCallbacks connectionCallbacks, OnConnectionFailedListener onConnectionFailedListener) {
        super(context, looper, 1, zzq, connectionCallbacks, onConnectionFailedListener);
        this.zzhdz = zzq.zzaju();
        this.zzhee = new Binder();
        this.zzhec = new zzq(this, zzq.zzajq());
        this.zzhef = (long) hashCode();
        this.zzheg = gamesOptions;
        if (!this.zzheg.zzhcl) {
            zzs(zzq.zzajw());
        }
    }

    private static Room zzak(DataHolder dataHolder) {
        AbstractDataBuffer zzb = new com.google.android.gms.games.multiplayer.realtime.zzb(dataHolder);
        Room room = null;
        try {
            if (zzb.getCount() > 0) {
                room = (Room) ((Room) zzb.get(0)).freeze();
            }
            zzb.release();
            return room;
        } catch (Throwable th) {
            zzb.release();
        }
    }

    private static void zzd(RemoteException remoteException) {
        zze.zzc("GamesClientImpl", "service died", remoteException);
    }

    public final void disconnect() {
        this.zzhed = false;
        if (isConnected()) {
            try {
                zzj zzj = (zzj) zzajj();
                zzj.zzaqs();
                this.zzhdy.flush();
                zzj.zzac(this.zzhef);
            } catch (RemoteException e) {
                zze.zzy("GamesClientImpl", "Failed to notify client disconnect.");
            }
        }
        super.disconnect();
    }

    public final String getAppId() {
        try {
            return ((zzj) zzajj()).getAppId();
        } catch (RemoteException e) {
            zzd(e);
            return null;
        }
    }

    public final void onConnectionFailed(ConnectionResult connectionResult) {
        super.onConnectionFailed(connectionResult);
        this.zzhed = false;
    }

    public final int zza(com.google.android.gms.common.api.internal.zzcj<ReliableMessageSentCallback> zzcj, byte[] bArr, String str, String str2) {
        try {
            return ((zzj) zzajj()).zza(new zzbv(zzcj), bArr, str, str2);
        } catch (RemoteException e) {
            zzd(e);
            return -1;
        }
    }

    public final int zza(byte[] bArr, String str, String[] strArr) {
        com.google.android.gms.common.internal.zzbp.zzb((Object) strArr, (Object) "Participant IDs must not be null");
        try {
            return ((zzj) zzajj()).zzb(bArr, str, strArr);
        } catch (RemoteException e) {
            zzd(e);
            return -1;
        }
    }

    public final Intent zza(int i, byte[] bArr, int i2, Bitmap bitmap, String str) {
        try {
            Intent zza = ((zzj) zzajj()).zza(i, bArr, i2, str);
            com.google.android.gms.common.internal.zzbp.zzb((Object) bitmap, (Object) "Must provide a non null icon");
            zza.putExtra("com.google.android.gms.games.REQUEST_ITEM_ICON", bitmap);
            return zza;
        } catch (RemoteException e) {
            zzd(e);
            return null;
        }
    }

    public final Intent zza(PlayerEntity playerEntity) {
        try {
            return ((zzj) zzajj()).zza(playerEntity);
        } catch (RemoteException e) {
            zzd(e);
            return null;
        }
    }

    public final Intent zza(Room room, int i) {
        try {
            return ((zzj) zzajj()).zza((RoomEntity) room.freeze(), i);
        } catch (RemoteException e) {
            zzd(e);
            return null;
        }
    }

    public final Intent zza(String str, boolean z, boolean z2, int i) {
        try {
            return ((zzj) zzajj()).zza(str, z, z2, i);
        } catch (RemoteException e) {
            zzd(e);
            return null;
        }
    }

    protected final void zza(int i, IBinder iBinder, Bundle bundle, int i2) {
        if (i == 0 && bundle != null) {
            bundle.setClassLoader(GamesClientImpl.class.getClassLoader());
            this.zzhed = bundle.getBoolean("show_welcome_popup");
            this.zzheh = this.zzhed;
            this.zzhea = (PlayerEntity) bundle.getParcelable("com.google.android.gms.games.current_player");
            this.zzheb = (GameEntity) bundle.getParcelable("com.google.android.gms.games.current_game");
        }
        super.zza(i, iBinder, bundle, i2);
    }

    public final void zza(IBinder iBinder, Bundle bundle) {
        if (isConnected()) {
            try {
                ((zzj) zzajj()).zza(iBinder, bundle);
            } catch (RemoteException e) {
                zzd(e);
            }
        }
    }

    public final /* synthetic */ void zza(@NonNull IInterface iInterface) {
        zzj zzj = (zzj) iInterface;
        super.zza((IInterface) zzj);
        if (this.zzhed) {
            this.zzhec.zzaqy();
            this.zzhed = false;
        }
        if (!this.zzheg.zzhcd && !this.zzheg.zzhcl) {
            try {
                zzj.zza(new zzbo(this.zzhec), this.zzhef);
            } catch (RemoteException e) {
                zzd(e);
            }
        }
    }

    public final void zza(com.google.android.gms.common.api.internal.zzcj<OnInvitationReceivedListener> zzcj) {
        try {
            ((zzj) zzajj()).zza(new zzab(zzcj), this.zzhef);
        } catch (RemoteException e) {
            zzd(e);
        }
    }

    public final void zza(com.google.android.gms.common.api.internal.zzcj<RoomUpdateListener> zzcj, com.google.android.gms.common.api.internal.zzcj<RoomStatusUpdateListener> zzcj2, com.google.android.gms.common.api.internal.zzcj<RealTimeMessageReceivedListener> zzcj3, RoomConfig roomConfig) {
        try {
            ((zzj) zzajj()).zza(new zzcc(zzcj, zzcj2, zzcj3), this.zzhee, roomConfig.getVariant(), roomConfig.getInvitedPlayerIds(), roomConfig.getAutoMatchCriteria(), false, this.zzhef);
        } catch (RemoteException e) {
            zzd(e);
        }
    }

    public final void zza(com.google.android.gms.common.api.internal.zzcj<RoomUpdateListener> zzcj, String str) {
        try {
            ((zzj) zzajj()).zza(new zzcc(zzcj), str);
        } catch (RemoteException e) {
            zzd(e);
        }
    }

    public final void zza(com.google.android.gms.common.api.internal.zzn<LoadInvitationsResult> zzn, int i) throws RemoteException {
        ((zzj) zzajj()).zza(new zzae(zzn), i);
    }

    public final void zza(com.google.android.gms.common.api.internal.zzn<LoadRequestsResult> zzn, int i, int i2, int i3) throws RemoteException {
        ((zzj) zzajj()).zza(new zzbz(zzn), i, i2, i3);
    }

    public final void zza(com.google.android.gms.common.api.internal.zzn<LoadPlayersResult> zzn, int i, boolean z, boolean z2) throws RemoteException {
        ((zzj) zzajj()).zza(new zzbn(zzn), i, z, z2);
    }

    public final void zza(com.google.android.gms.common.api.internal.zzn<LoadMatchesResult> zzn, int i, int[] iArr) throws RemoteException {
        ((zzj) zzajj()).zza(new zzct(zzn), i, iArr);
    }

    public final void zza(com.google.android.gms.common.api.internal.zzn<LoadScoresResult> zzn, LeaderboardScoreBuffer leaderboardScoreBuffer, int i, int i2) throws RemoteException {
        ((zzj) zzajj()).zza(new zzah(zzn), leaderboardScoreBuffer.zzarq().asBundle(), i, i2);
    }

    public final void zza(com.google.android.gms.common.api.internal.zzn<InitiateMatchResult> zzn, TurnBasedMatchConfig turnBasedMatchConfig) throws RemoteException {
        ((zzj) zzajj()).zza(new zzco(zzn), turnBasedMatchConfig.getVariant(), turnBasedMatchConfig.zzarv(), turnBasedMatchConfig.getInvitedPlayerIds(), turnBasedMatchConfig.getAutoMatchCriteria());
    }

    public final void zza(com.google.android.gms.common.api.internal.zzn<CommitSnapshotResult> zzn, Snapshot snapshot, SnapshotMetadataChange snapshotMetadataChange) throws RemoteException {
        SnapshotContents snapshotContents = snapshot.getSnapshotContents();
        com.google.android.gms.common.internal.zzbp.zza(!snapshotContents.isClosed(), (Object) "Snapshot already closed");
        BitmapTeleporter zzary = snapshotMetadataChange.zzary();
        if (zzary != null) {
            zzary.zzc(getContext().getCacheDir());
        }
        com.google.android.gms.drive.zzc zzamq = snapshotContents.zzamq();
        snapshotContents.close();
        ((zzj) zzajj()).zza(new zzch(zzn), snapshot.getMetadata().getSnapshotId(), (com.google.android.gms.games.snapshot.zze) snapshotMetadataChange, zzamq);
    }

    public final void zza(com.google.android.gms.common.api.internal.zzn<UpdateAchievementResult> zzn, String str) throws RemoteException {
        zzf zzf;
        if (zzn == null) {
            zzf = null;
        } else {
            Object zze = new zze(zzn);
        }
        ((zzj) zzajj()).zza(zzf, str, this.zzhec.zzhgu.zzhgv, this.zzhec.zzhgu.zzaqz());
    }

    public final void zza(com.google.android.gms.common.api.internal.zzn<UpdateAchievementResult> zzn, String str, int i) throws RemoteException {
        ((zzj) zzajj()).zza(zzn == null ? null : new zze(zzn), str, i, this.zzhec.zzhgu.zzhgv, this.zzhec.zzhgu.zzaqz());
    }

    public final void zza(com.google.android.gms.common.api.internal.zzn<LoadScoresResult> zzn, String str, int i, int i2, int i3, boolean z) throws RemoteException {
        ((zzj) zzajj()).zza(new zzah(zzn), str, i, i2, i3, z);
    }

    public final void zza(com.google.android.gms.common.api.internal.zzn<LoadPlayersResult> zzn, String str, int i, boolean z, boolean z2) throws RemoteException {
        Object obj = -1;
        switch (str.hashCode()) {
            case 156408498:
                if (str.equals("played_with")) {
                    obj = null;
                    break;
                }
                break;
        }
        switch (obj) {
            case null:
                ((zzj) zzajj()).zza(new zzbn(zzn), str, i, z, z2);
                return;
            default:
                String valueOf = String.valueOf(str);
                throw new IllegalArgumentException(valueOf.length() != 0 ? "Invalid player collection: ".concat(valueOf) : new String("Invalid player collection: "));
        }
    }

    public final void zza(com.google.android.gms.common.api.internal.zzn<SubmitScoreResult> zzn, String str, long j, String str2) throws RemoteException {
        ((zzj) zzajj()).zza(zzn == null ? null : new zzcl(zzn), str, j, str2);
    }

    public final void zza(com.google.android.gms.common.api.internal.zzn<LeaveMatchResult> zzn, String str, String str2) throws RemoteException {
        ((zzj) zzajj()).zza(new zzcp(zzn), str, str2);
    }

    public final void zza(com.google.android.gms.common.api.internal.zzn<LoadPlayerScoreResult> zzn, String str, String str2, int i, int i2) throws RemoteException {
        ((zzj) zzajj()).zza(new zzbl(zzn), null, str2, i, i2);
    }

    public final void zza(com.google.android.gms.common.api.internal.zzn<OpenSnapshotResult> zzn, String str, String str2, SnapshotMetadataChange snapshotMetadataChange, SnapshotContents snapshotContents) throws RemoteException {
        com.google.android.gms.common.internal.zzbp.zza(!snapshotContents.isClosed(), (Object) "SnapshotContents already closed");
        BitmapTeleporter zzary = snapshotMetadataChange.zzary();
        if (zzary != null) {
            zzary.zzc(getContext().getCacheDir());
        }
        com.google.android.gms.drive.zzc zzamq = snapshotContents.zzamq();
        snapshotContents.close();
        ((zzj) zzajj()).zza(new zzcj(zzn), str, str2, (com.google.android.gms.games.snapshot.zze) snapshotMetadataChange, zzamq);
    }

    public final void zza(com.google.android.gms.common.api.internal.zzn<LoadPlayersResult> zzn, String str, boolean z) throws RemoteException {
        ((zzj) zzajj()).zzb(new zzbn(zzn), str, z);
    }

    public final void zza(com.google.android.gms.common.api.internal.zzn<OpenSnapshotResult> zzn, String str, boolean z, int i) throws RemoteException {
        ((zzj) zzajj()).zza(new zzcj(zzn), str, z, i);
    }

    public final void zza(com.google.android.gms.common.api.internal.zzn<UpdateMatchResult> zzn, String str, byte[] bArr, String str2, ParticipantResult[] participantResultArr) throws RemoteException {
        ((zzj) zzajj()).zza(new zzcs(zzn), str, bArr, str2, participantResultArr);
    }

    public final void zza(com.google.android.gms.common.api.internal.zzn<UpdateMatchResult> zzn, String str, byte[] bArr, ParticipantResult[] participantResultArr) throws RemoteException {
        ((zzj) zzajj()).zza(new zzcs(zzn), str, bArr, participantResultArr);
    }

    public final void zza(com.google.android.gms.common.api.internal.zzn<LoadPlayersResult> zzn, boolean z) throws RemoteException {
        ((zzj) zzajj()).zzc(new zzbn(zzn), z);
    }

    public final void zza(com.google.android.gms.common.api.internal.zzn<LoadEventsResult> zzn, boolean z, String... strArr) throws RemoteException {
        this.zzhdy.flush();
        ((zzj) zzajj()).zza(new zzu(zzn), z, strArr);
    }

    public final void zza(com.google.android.gms.common.api.internal.zzn<LoadQuestsResult> zzn, int[] iArr, int i, boolean z) throws RemoteException {
        this.zzhdy.flush();
        ((zzj) zzajj()).zza(new zzbt(zzn), iArr, i, z);
    }

    public final void zza(com.google.android.gms.common.api.internal.zzn<UpdateRequestsResult> zzn, String[] strArr) throws RemoteException {
        ((zzj) zzajj()).zza(new zzca(zzn), strArr);
    }

    public final void zza(com.google.android.gms.common.internal.zzj zzj) {
        this.zzhea = null;
        this.zzheb = null;
        super.zza(zzj);
    }

    public final void zza(Snapshot snapshot) {
        SnapshotContents snapshotContents = snapshot.getSnapshotContents();
        com.google.android.gms.common.internal.zzbp.zza(!snapshotContents.isClosed(), (Object) "Snapshot already closed");
        com.google.android.gms.drive.zzc zzamq = snapshotContents.zzamq();
        snapshotContents.close();
        try {
            ((zzj) zzajj()).zza(zzamq);
        } catch (RemoteException e) {
            zzd(e);
        }
    }

    public final boolean zzaaa() {
        return true;
    }

    public final Bundle zzaeg() {
        try {
            Bundle zzaeg = ((zzj) zzajj()).zzaeg();
            if (zzaeg == null) {
                return zzaeg;
            }
            zzaeg.setClassLoader(GamesClientImpl.class.getClassLoader());
            return zzaeg;
        } catch (RemoteException e) {
            zzd(e);
            return null;
        }
    }

    public final String zzapv() {
        try {
            return ((zzj) zzajj()).zzapv();
        } catch (RemoteException e) {
            zzd(e);
            return null;
        }
    }

    public final Player zzapw() {
        AbstractDataBuffer playerBuffer;
        zzaji();
        synchronized (this) {
            if (this.zzhea == null) {
                try {
                    playerBuffer = new PlayerBuffer(((zzj) zzajj()).zzaqv());
                    if (playerBuffer.getCount() > 0) {
                        this.zzhea = (PlayerEntity) ((Player) playerBuffer.get(0)).freeze();
                    }
                    playerBuffer.release();
                } catch (RemoteException e) {
                    zzd(e);
                } catch (Throwable th) {
                    playerBuffer.release();
                }
            }
        }
        return this.zzhea;
    }

    public final Game zzapx() {
        zzaji();
        synchronized (this) {
            if (this.zzheb == null) {
                AbstractDataBuffer gameBuffer;
                try {
                    gameBuffer = new GameBuffer(((zzj) zzajj()).zzaqw());
                    if (gameBuffer.getCount() > 0) {
                        this.zzheb = (GameEntity) ((Game) gameBuffer.get(0)).freeze();
                    }
                    gameBuffer.release();
                } catch (RemoteException e) {
                    zzd(e);
                } catch (Throwable th) {
                    gameBuffer.release();
                }
            }
        }
        return this.zzheb;
    }

    public final Intent zzapy() {
        try {
            return ((zzj) zzajj()).zzapy();
        } catch (RemoteException e) {
            zzd(e);
            return null;
        }
    }

    public final Intent zzapz() {
        try {
            return ((zzj) zzajj()).zzapz();
        } catch (RemoteException e) {
            zzd(e);
            return null;
        }
    }

    public final Intent zzaqa() {
        try {
            return ((zzj) zzajj()).zzaqa();
        } catch (RemoteException e) {
            zzd(e);
            return null;
        }
    }

    public final Intent zzaqb() {
        try {
            return ((zzj) zzajj()).zzaqb();
        } catch (RemoteException e) {
            zzd(e);
            return null;
        }
    }

    public final void zzaqc() {
        try {
            ((zzj) zzajj()).zzad(this.zzhef);
        } catch (RemoteException e) {
            zzd(e);
        }
    }

    public final void zzaqd() {
        try {
            ((zzj) zzajj()).zzae(this.zzhef);
        } catch (RemoteException e) {
            zzd(e);
        }
    }

    public final void zzaqe() {
        try {
            ((zzj) zzajj()).zzag(this.zzhef);
        } catch (RemoteException e) {
            zzd(e);
        }
    }

    public final void zzaqf() {
        try {
            ((zzj) zzajj()).zzaf(this.zzhef);
        } catch (RemoteException e) {
            zzd(e);
        }
    }

    public final Intent zzaqg() {
        try {
            return ((zzj) zzajj()).zzaqg();
        } catch (RemoteException e) {
            zzd(e);
            return null;
        }
    }

    public final Intent zzaqh() {
        try {
            return ((zzj) zzajj()).zzaqh();
        } catch (RemoteException e) {
            zzd(e);
            return null;
        }
    }

    public final int zzaqi() {
        try {
            return ((zzj) zzajj()).zzaqi();
        } catch (RemoteException e) {
            zzd(e);
            return 4368;
        }
    }

    public final int zzaqj() {
        try {
            return ((zzj) zzajj()).zzaqj();
        } catch (RemoteException e) {
            zzd(e);
            return -1;
        }
    }

    public final Intent zzaqk() {
        try {
            return ((zzj) zzajj()).zzaqk();
        } catch (RemoteException e) {
            zzd(e);
            return null;
        }
    }

    public final int zzaql() {
        try {
            return ((zzj) zzajj()).zzaql();
        } catch (RemoteException e) {
            zzd(e);
            return -1;
        }
    }

    public final int zzaqm() {
        try {
            return ((zzj) zzajj()).zzaqm();
        } catch (RemoteException e) {
            zzd(e);
            return -1;
        }
    }

    public final int zzaqn() {
        try {
            return ((zzj) zzajj()).zzaqn();
        } catch (RemoteException e) {
            zzd(e);
            return -1;
        }
    }

    public final int zzaqo() {
        try {
            return ((zzj) zzajj()).zzaqo();
        } catch (RemoteException e) {
            zzd(e);
            return -1;
        }
    }

    public final Intent zzaqp() {
        try {
            return ((zzj) zzajj()).zzaqx();
        } catch (RemoteException e) {
            zzd(e);
            return null;
        }
    }

    public final boolean zzaqq() {
        try {
            return ((zzj) zzajj()).zzaqq();
        } catch (RemoteException e) {
            zzd(e);
            return false;
        }
    }

    public final void zzaqr() {
        try {
            ((zzj) zzajj()).zzah(this.zzhef);
        } catch (RemoteException e) {
            zzd(e);
        }
    }

    public final void zzaqs() {
        if (isConnected()) {
            try {
                ((zzj) zzajj()).zzaqs();
            } catch (RemoteException e) {
                zzd(e);
            }
        }
    }

    public final Intent zzb(int i, int i2, boolean z) {
        try {
            return ((zzj) zzajj()).zzb(i, i2, z);
        } catch (RemoteException e) {
            zzd(e);
            return null;
        }
    }

    protected final Set<Scope> zzb(Set<Scope> set) {
        Scope scope = new Scope(Scopes.GAMES);
        Scope scope2 = new Scope("https://www.googleapis.com/auth/games.firstparty");
        boolean z = false;
        int i = 0;
        for (Scope scope3 : set) {
            if (scope3.equals(scope)) {
                z = true;
            } else {
                i = scope3.equals(scope2) ? 1 : i;
            }
        }
        if (i != 0) {
            com.google.android.gms.common.internal.zzbp.zza(!z, "Cannot have both %s and %s!", Scopes.GAMES, "https://www.googleapis.com/auth/games.firstparty");
        } else {
            com.google.android.gms.common.internal.zzbp.zza(z, "Games APIs requires %s to function.", Scopes.GAMES);
        }
        return set;
    }

    public final void zzb(com.google.android.gms.common.api.internal.zzcj<OnTurnBasedMatchUpdateReceivedListener> zzcj) {
        try {
            ((zzj) zzajj()).zzb(new zzaz(zzcj), this.zzhef);
        } catch (RemoteException e) {
            zzd(e);
        }
    }

    public final void zzb(com.google.android.gms.common.api.internal.zzcj<RoomUpdateListener> zzcj, com.google.android.gms.common.api.internal.zzcj<RoomStatusUpdateListener> zzcj2, com.google.android.gms.common.api.internal.zzcj<RealTimeMessageReceivedListener> zzcj3, RoomConfig roomConfig) {
        try {
            ((zzj) zzajj()).zza(new zzcc(zzcj, zzcj2, zzcj3), this.zzhee, roomConfig.getInvitationId(), false, this.zzhef);
        } catch (RemoteException e) {
            zzd(e);
        }
    }

    public final void zzb(com.google.android.gms.common.api.internal.zzn<CaptureAvailableResult> zzn, int i) throws RemoteException {
        ((zzj) zzajj()).zzb(new zzh(zzn), i);
    }

    public final void zzb(com.google.android.gms.common.api.internal.zzn<UpdateAchievementResult> zzn, String str) throws RemoteException {
        zzf zzf;
        if (zzn == null) {
            zzf = null;
        } else {
            Object zze = new zze(zzn);
        }
        ((zzj) zzajj()).zzb(zzf, str, this.zzhec.zzhgu.zzhgv, this.zzhec.zzhgu.zzaqz());
    }

    public final void zzb(com.google.android.gms.common.api.internal.zzn<UpdateAchievementResult> zzn, String str, int i) throws RemoteException {
        ((zzj) zzajj()).zzb(zzn == null ? null : new zze(zzn), str, i, this.zzhec.zzhgu.zzhgv, this.zzhec.zzhgu.zzaqz());
    }

    public final void zzb(com.google.android.gms.common.api.internal.zzn<LoadScoresResult> zzn, String str, int i, int i2, int i3, boolean z) throws RemoteException {
        ((zzj) zzajj()).zzb(new zzah(zzn), str, i, i2, i3, z);
    }

    public final void zzb(com.google.android.gms.common.api.internal.zzn<ClaimMilestoneResult> zzn, String str, String str2) throws RemoteException {
        this.zzhdy.flush();
        ((zzj) zzajj()).zzb(new zzbr(zzn, str2), str, str2);
    }

    public final void zzb(com.google.android.gms.common.api.internal.zzn<LeaderboardMetadataResult> zzn, String str, boolean z) throws RemoteException {
        ((zzj) zzajj()).zza(new zzai(zzn), str, z);
    }

    public final void zzb(com.google.android.gms.common.api.internal.zzn<LeaderboardMetadataResult> zzn, boolean z) throws RemoteException {
        ((zzj) zzajj()).zzb(new zzai(zzn), z);
    }

    public final void zzb(com.google.android.gms.common.api.internal.zzn<LoadQuestsResult> zzn, boolean z, String[] strArr) throws RemoteException {
        this.zzhdy.flush();
        ((zzj) zzajj()).zza(new zzbt(zzn), strArr, z);
    }

    public final void zzb(com.google.android.gms.common.api.internal.zzn<UpdateRequestsResult> zzn, String[] strArr) throws RemoteException {
        ((zzj) zzajj()).zzb(new zzca(zzn), strArr);
    }

    public final void zzb(String str, com.google.android.gms.common.api.internal.zzn<GetServerAuthCodeResult> zzn) throws RemoteException {
        com.google.android.gms.common.internal.zzbp.zzh(str, "Please provide a valid serverClientId");
        ((zzj) zzajj()).zza(str, new zzy(zzn));
    }

    public final String zzbj(boolean z) {
        if (this.zzhea != null) {
            return this.zzhea.getPlayerId();
        }
        try {
            return ((zzj) zzajj()).zzaqu();
        } catch (RemoteException e) {
            zzd(e);
            return null;
        }
    }

    public final Intent zzc(int i, int i2, boolean z) {
        try {
            return ((zzj) zzajj()).zzc(i, i2, z);
        } catch (RemoteException e) {
            zzd(e);
            return null;
        }
    }

    public final void zzc(com.google.android.gms.common.api.internal.zzcj<QuestUpdateListener> zzcj) {
        try {
            ((zzj) zzajj()).zzd(new zzbs(zzcj), this.zzhef);
        } catch (RemoteException e) {
            zzd(e);
        }
    }

    public final void zzc(com.google.android.gms.common.api.internal.zzn<InitiateMatchResult> zzn, String str) throws RemoteException {
        ((zzj) zzajj()).zzb(new zzco(zzn), str);
    }

    public final void zzc(com.google.android.gms.common.api.internal.zzn<LoadAchievementsResult> zzn, boolean z) throws RemoteException {
        ((zzj) zzajj()).zza(new zzf(zzn), z);
    }

    public final int zzd(byte[] bArr, String str) {
        try {
            return ((zzj) zzajj()).zzb(bArr, str, null);
        } catch (RemoteException e) {
            zzd(e);
            return -1;
        }
    }

    public final Intent zzd(int[] iArr) {
        try {
            return ((zzj) zzajj()).zzd(iArr);
        } catch (RemoteException e) {
            zzd(e);
            return null;
        }
    }

    public final void zzd(com.google.android.gms.common.api.internal.zzcj<OnRequestReceivedListener> zzcj) {
        try {
            ((zzj) zzajj()).zzc(new zzbw(zzcj), this.zzhef);
        } catch (RemoteException e) {
            zzd(e);
        }
    }

    public final void zzd(com.google.android.gms.common.api.internal.zzn<InitiateMatchResult> zzn, String str) throws RemoteException {
        ((zzj) zzajj()).zzc(new zzco(zzn), str);
    }

    public final void zzd(com.google.android.gms.common.api.internal.zzn<LoadEventsResult> zzn, boolean z) throws RemoteException {
        this.zzhdy.flush();
        ((zzj) zzajj()).zze(new zzu(zzn), z);
    }

    public final void zzdc(int i) {
        this.zzhec.zzhgu.gravity = i;
    }

    public final void zzdd(int i) {
        try {
            ((zzj) zzajj()).zzdd(i);
        } catch (RemoteException e) {
            zzd(e);
        }
    }

    protected final /* synthetic */ IInterface zze(IBinder iBinder) {
        if (iBinder == null) {
            return null;
        }
        IInterface queryLocalInterface = iBinder.queryLocalInterface("com.google.android.gms.games.internal.IGamesService");
        return queryLocalInterface instanceof zzj ? (zzj) queryLocalInterface : new zzk(iBinder);
    }

    public final void zze(com.google.android.gms.common.api.internal.zzcj<CaptureOverlayStateListener> zzcj) {
        try {
            ((zzj) zzajj()).zze(new zzl(zzcj), this.zzhef);
        } catch (RemoteException e) {
            zzd(e);
        }
    }

    public final void zze(com.google.android.gms.common.api.internal.zzn<LeaveMatchResult> zzn, String str) throws RemoteException {
        ((zzj) zzajj()).zze(new zzcp(zzn), str);
    }

    public final void zze(com.google.android.gms.common.api.internal.zzn<LoadPlayerStatsResult> zzn, boolean z) throws RemoteException {
        ((zzj) zzajj()).zzf(new zzbm(zzn), z);
    }

    public final void zzf(com.google.android.gms.common.api.internal.zzn<LoadGamesResult> zzn) throws RemoteException {
        ((zzj) zzajj()).zzb(new zzx(zzn));
    }

    public final void zzf(com.google.android.gms.common.api.internal.zzn<CancelMatchResult> zzn, String str) throws RemoteException {
        ((zzj) zzajj()).zzd(new zzcn(zzn), str);
    }

    public final void zzf(com.google.android.gms.common.api.internal.zzn<LoadSnapshotsResult> zzn, boolean z) throws RemoteException {
        ((zzj) zzajj()).zzd(new zzck(zzn), z);
    }

    public final void zzg(com.google.android.gms.common.api.internal.zzn<Status> zzn) throws RemoteException {
        this.zzhdy.flush();
        ((zzj) zzajj()).zza(new zzcg(zzn));
    }

    public final void zzg(com.google.android.gms.common.api.internal.zzn<LoadMatchResult> zzn, String str) throws RemoteException {
        ((zzj) zzajj()).zzf(new zzcq(zzn), str);
    }

    public final void zzh(com.google.android.gms.common.api.internal.zzn<CaptureCapabilitiesResult> zzn) throws RemoteException {
        ((zzj) zzajj()).zzc(new zzj(zzn));
    }

    public final void zzh(com.google.android.gms.common.api.internal.zzn<AcceptQuestResult> zzn, String str) throws RemoteException {
        this.zzhdy.flush();
        ((zzj) zzajj()).zzh(new zzbp(zzn), str);
    }

    protected final String zzhc() {
        return "com.google.android.gms.games.service.START";
    }

    protected final String zzhd() {
        return "com.google.android.gms.games.internal.IGamesService";
    }

    public final void zzhj(String str) {
        try {
            ((zzj) zzajj()).zzhm(str);
        } catch (RemoteException e) {
            zzd(e);
        }
    }

    public final Intent zzhk(String str) {
        try {
            return ((zzj) zzajj()).zzhk(str);
        } catch (RemoteException e) {
            zzd(e);
            return null;
        }
    }

    public final void zzhl(String str) {
        try {
            ((zzj) zzajj()).zza(str, this.zzhec.zzhgu.zzhgv, this.zzhec.zzhgu.zzaqz());
        } catch (RemoteException e) {
            zzd(e);
        }
    }

    public final void zzi(com.google.android.gms.common.api.internal.zzn<CaptureStateResult> zzn) throws RemoteException {
        ((zzj) zzajj()).zzd(new zzn(zzn));
    }

    public final void zzi(com.google.android.gms.common.api.internal.zzn<DeleteSnapshotResult> zzn, String str) throws RemoteException {
        ((zzj) zzajj()).zzg(new zzci(zzn), str);
    }

    public final Intent zzj(String str, int i, int i2) {
        try {
            return ((zzj) zzajj()).zzk(str, i, i2);
        } catch (RemoteException e) {
            zzd(e);
            return null;
        }
    }

    public final void zzp(String str, int i) {
        this.zzhdy.zzp(str, i);
    }

    public final void zzq(String str, int i) {
        try {
            ((zzj) zzajj()).zzq(str, i);
        } catch (RemoteException e) {
            zzd(e);
        }
    }

    public final void zzr(String str, int i) {
        try {
            ((zzj) zzajj()).zzr(str, i);
        } catch (RemoteException e) {
            zzd(e);
        }
    }

    public final void zzs(View view) {
        this.zzhec.zzt(view);
    }

    protected final Bundle zzzs() {
        String locale = getContext().getResources().getConfiguration().locale.toString();
        Bundle zzapl = this.zzheg.zzapl();
        zzapl.putString("com.google.android.gms.games.key.gamePackageName", this.zzhdz);
        zzapl.putString("com.google.android.gms.games.key.desiredLocale", locale);
        zzapl.putParcelable("com.google.android.gms.games.key.popupWindowToken", new BinderWrapper(this.zzhec.zzhgu.zzhgv));
        zzapl.putInt("com.google.android.gms.games.key.API_VERSION", 6);
        zzapl.putBundle("com.google.android.gms.games.key.signInOptions", zzcpw.zza(zzakd()));
        return zzapl;
    }
}
