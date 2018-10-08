package com.google.android.gms.games;

import android.content.Context;
import android.content.Intent;
import android.os.Bundle;
import android.os.Looper;
import android.support.annotation.RequiresPermission;
import android.view.View;
import com.google.android.gms.auth.api.signin.GoogleSignInOptionsExtension;
import com.google.android.gms.common.Scopes;
import com.google.android.gms.common.annotation.KeepForSdk;
import com.google.android.gms.common.api.Api;
import com.google.android.gms.common.api.Api.ApiOptions.Optional;
import com.google.android.gms.common.api.Api.zze;
import com.google.android.gms.common.api.Api.zzf;
import com.google.android.gms.common.api.GoogleApiClient;
import com.google.android.gms.common.api.GoogleApiClient.ConnectionCallbacks;
import com.google.android.gms.common.api.GoogleApiClient.OnConnectionFailedListener;
import com.google.android.gms.common.api.PendingResult;
import com.google.android.gms.common.api.Result;
import com.google.android.gms.common.api.Scope;
import com.google.android.gms.common.api.Status;
import com.google.android.gms.common.api.internal.zzm;
import com.google.android.gms.common.internal.zzbp;
import com.google.android.gms.common.internal.zzq;
import com.google.android.gms.games.achievement.Achievements;
import com.google.android.gms.games.event.Events;
import com.google.android.gms.games.internal.GamesClientImpl;
import com.google.android.gms.games.internal.api.zzab;
import com.google.android.gms.games.internal.api.zzaf;
import com.google.android.gms.games.internal.api.zzav;
import com.google.android.gms.games.internal.api.zzaw;
import com.google.android.gms.games.internal.api.zzax;
import com.google.android.gms.games.internal.api.zzbh;
import com.google.android.gms.games.internal.api.zzbs;
import com.google.android.gms.games.internal.api.zzbt;
import com.google.android.gms.games.internal.api.zzcb;
import com.google.android.gms.games.internal.api.zzcp;
import com.google.android.gms.games.internal.api.zzcq;
import com.google.android.gms.games.internal.api.zzcu;
import com.google.android.gms.games.internal.api.zzdr;
import com.google.android.gms.games.internal.api.zzo;
import com.google.android.gms.games.internal.api.zzp;
import com.google.android.gms.games.internal.api.zzx;
import com.google.android.gms.games.leaderboard.Leaderboards;
import com.google.android.gms.games.multiplayer.Invitations;
import com.google.android.gms.games.multiplayer.Multiplayer;
import com.google.android.gms.games.multiplayer.realtime.RealTimeMultiplayer;
import com.google.android.gms.games.multiplayer.turnbased.TurnBasedMultiplayer;
import com.google.android.gms.games.quest.Quests;
import com.google.android.gms.games.request.Requests;
import com.google.android.gms.games.snapshot.Snapshots;
import com.google.android.gms.games.stats.Stats;
import com.google.android.gms.games.video.Videos;
import com.google.android.gms.internal.zzbvg;
import com.google.android.gms.internal.zzbvo;
import java.util.ArrayList;

@KeepForSdk
public final class Games {
    public static final Api<GamesOptions> API = new Api("Games.API", zzdwr, zzdwq);
    public static final Achievements Achievements = new com.google.android.gms.games.internal.api.zza();
    public static final String EXTRA_PLAYER_IDS = "players";
    public static final String EXTRA_STATUS = "status";
    public static final Events Events = new zzp();
    public static final GamesMetadata GamesMetadata = new zzx();
    public static final Invitations Invitations = new zzab();
    public static final Leaderboards Leaderboards = new zzaf();
    public static final Notifications Notifications = new zzaw();
    public static final Players Players = new zzax();
    public static final Quests Quests = new zzbh();
    public static final RealTimeMultiplayer RealTimeMultiplayer = new zzbs();
    public static final Requests Requests = new zzbt();
    public static final Scope SCOPE_GAMES = new Scope(Scopes.GAMES);
    public static final Snapshots Snapshots = new zzcb();
    public static final Stats Stats = new zzcq();
    public static final TurnBasedMultiplayer TurnBasedMultiplayer = new zzcu();
    public static final Videos Videos = new zzdr();
    static final zzf<GamesClientImpl> zzdwq = new zzf();
    private static final com.google.android.gms.common.api.Api.zza<GamesClientImpl, GamesOptions> zzdwr = new zzb();
    private static final com.google.android.gms.common.api.Api.zza<GamesClientImpl, GamesOptions> zzhbv = new zzc();
    private static Scope zzhbw = new Scope("https://www.googleapis.com/auth/games_lite");
    public static final Scope zzhbx = new Scope("https://www.googleapis.com/auth/games.firstparty");
    private static Api<GamesOptions> zzhby = new Api("Games.API_1P", zzhbv, zzdwq);
    private static zzbvg zzhbz = new zzo();
    private static Multiplayer zzhca = new zzav();
    private static zzbvo zzhcb = new zzcp();

    public static final class GamesOptions implements GoogleSignInOptionsExtension, Optional {
        public final boolean zzhcd;
        private boolean zzhce;
        private int zzhcf;
        private boolean zzhcg;
        private int zzhch;
        private String zzhci;
        private ArrayList<String> zzhcj;
        private boolean zzhck;
        public final boolean zzhcl;
        private boolean zzhcm;

        public static final class Builder {
            private boolean zzhcd;
            private boolean zzhce;
            private int zzhcf;
            private boolean zzhcg;
            private int zzhch;
            private String zzhci;
            private ArrayList<String> zzhcj;
            private boolean zzhck;
            private boolean zzhcl;
            private boolean zzhcm;

            private Builder() {
                this.zzhcd = false;
                this.zzhce = true;
                this.zzhcf = 17;
                this.zzhcg = false;
                this.zzhch = 4368;
                this.zzhci = null;
                this.zzhcj = new ArrayList();
                this.zzhck = false;
                this.zzhcl = false;
                this.zzhcm = false;
            }

            public final GamesOptions build() {
                return new GamesOptions(this.zzhce, this.zzhcf, this.zzhch, this.zzhcj);
            }

            public final Builder setSdkVariant(int i) {
                this.zzhch = i;
                return this;
            }

            public final Builder setShowConnectingPopup(boolean z) {
                this.zzhce = z;
                this.zzhcf = 17;
                return this;
            }

            public final Builder setShowConnectingPopup(boolean z, int i) {
                this.zzhce = z;
                this.zzhcf = i;
                return this;
            }
        }

        private GamesOptions(boolean z, boolean z2, int i, boolean z3, int i2, String str, ArrayList<String> arrayList, boolean z4, boolean z5, boolean z6) {
            this.zzhcd = z;
            this.zzhce = z2;
            this.zzhcf = i;
            this.zzhcg = z3;
            this.zzhch = i2;
            this.zzhci = str;
            this.zzhcj = arrayList;
            this.zzhck = z4;
            this.zzhcl = z5;
            this.zzhcm = z6;
        }

        public static Builder builder() {
            return new Builder();
        }

        public final Bundle toBundle() {
            return zzapl();
        }

        public final Bundle zzapl() {
            Bundle bundle = new Bundle();
            bundle.putBoolean("com.google.android.gms.games.key.isHeadless", this.zzhcd);
            bundle.putBoolean("com.google.android.gms.games.key.showConnectingPopup", this.zzhce);
            bundle.putInt("com.google.android.gms.games.key.connectingPopupGravity", this.zzhcf);
            bundle.putBoolean("com.google.android.gms.games.key.retryingSignIn", this.zzhcg);
            bundle.putInt("com.google.android.gms.games.key.sdkVariant", this.zzhch);
            bundle.putString("com.google.android.gms.games.key.forceResolveAccountKey", this.zzhci);
            bundle.putStringArrayList("com.google.android.gms.games.key.proxyApis", this.zzhcj);
            bundle.putBoolean("com.google.android.gms.games.key.requireGooglePlus", this.zzhck);
            bundle.putBoolean("com.google.android.gms.games.key.unauthenticated", this.zzhcl);
            bundle.putBoolean("com.google.android.gms.games.key.skipWelcomePopup", this.zzhcm);
            return bundle;
        }
    }

    @KeepForSdk
    @Deprecated
    public interface GetServerAuthCodeResult extends Result {
        @KeepForSdk
        String getCode();
    }

    public static abstract class zza<R extends Result> extends zzm<R, GamesClientImpl> {
        public zza(GoogleApiClient googleApiClient) {
            super(Games.zzdwq, googleApiClient);
        }

        public final /* bridge */ /* synthetic */ void setResult(Object obj) {
            super.setResult((Result) obj);
        }
    }

    static class zzb extends com.google.android.gms.common.api.Api.zza<GamesClientImpl, GamesOptions> {
        private zzb() {
        }

        public final int getPriority() {
            return 1;
        }

        public final /* synthetic */ zze zza(Context context, Looper looper, zzq zzq, Object obj, ConnectionCallbacks connectionCallbacks, OnConnectionFailedListener onConnectionFailedListener) {
            GamesOptions gamesOptions = (GamesOptions) obj;
            return new GamesClientImpl(context, looper, zzq, gamesOptions == null ? new Builder().build() : gamesOptions, connectionCallbacks, onConnectionFailedListener);
        }
    }

    static abstract class zzc extends zza<GetServerAuthCodeResult> {
        private zzc(GoogleApiClient googleApiClient) {
            super(googleApiClient);
        }

        public final /* synthetic */ Result zzb(Status status) {
            return new zzf(this, status);
        }
    }

    static abstract class zzd extends zza<Status> {
        private zzd(GoogleApiClient googleApiClient) {
            super(googleApiClient);
        }

        public final /* synthetic */ Result zzb(Status status) {
            return status;
        }
    }

    private Games() {
    }

    public static String getAppId(GoogleApiClient googleApiClient) {
        return zza(googleApiClient, true).getAppId();
    }

    @RequiresPermission("android.permission.GET_ACCOUNTS")
    public static String getCurrentAccountName(GoogleApiClient googleApiClient) {
        return zza(googleApiClient, true).zzapv();
    }

    @KeepForSdk
    @Deprecated
    public static PendingResult<GetServerAuthCodeResult> getGamesServerAuthCode(GoogleApiClient googleApiClient, String str) {
        zzbp.zzh(str, "Please provide a valid serverClientId");
        return googleApiClient.zze(new zzd(googleApiClient, str));
    }

    public static int getSdkVariant(GoogleApiClient googleApiClient) {
        return zza(googleApiClient, true).zzaqi();
    }

    public static Intent getSettingsIntent(GoogleApiClient googleApiClient) {
        return zza(googleApiClient, true).zzaqh();
    }

    public static void setGravityForPopups(GoogleApiClient googleApiClient, int i) {
        GamesClientImpl zza = zza(googleApiClient, false);
        if (zza != null) {
            zza.zzdc(i);
        }
    }

    public static void setViewForPopups(GoogleApiClient googleApiClient, View view) {
        zzbp.zzu(view);
        GamesClientImpl zza = zza(googleApiClient, false);
        if (zza != null) {
            zza.zzs(view);
        }
    }

    public static PendingResult<Status> signOut(GoogleApiClient googleApiClient) {
        return googleApiClient.zze(new zze(googleApiClient));
    }

    public static GamesClientImpl zza(GoogleApiClient googleApiClient, boolean z) {
        zzbp.zzb(googleApiClient != null, (Object) "GoogleApiClient parameter is required.");
        zzbp.zza(googleApiClient.isConnected(), (Object) "GoogleApiClient must be connected.");
        return zzb(googleApiClient, z);
    }

    public static GamesClientImpl zzb(GoogleApiClient googleApiClient, boolean z) {
        zzbp.zza(googleApiClient.zza(API), (Object) "GoogleApiClient is not configured to use the Games Api. Pass Games.API into GoogleApiClient.Builder#addApi() to use this feature.");
        boolean hasConnectedApi = googleApiClient.hasConnectedApi(API);
        if (!z || hasConnectedApi) {
            return hasConnectedApi ? (GamesClientImpl) googleApiClient.zza(zzdwq) : null;
        } else {
            throw new IllegalStateException("GoogleApiClient has an optional Games.API and is not connected to Games. Use GoogleApiClient.hasConnectedApi(Games.API) to guard this call.");
        }
    }

    public static GamesClientImpl zzf(GoogleApiClient googleApiClient) {
        return zza(googleApiClient, true);
    }
}
