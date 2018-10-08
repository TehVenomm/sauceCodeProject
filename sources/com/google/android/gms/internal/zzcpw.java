package com.google.android.gms.internal;

import android.accounts.Account;
import android.content.Context;
import android.os.Bundle;
import android.os.IBinder;
import android.os.IInterface;
import android.os.Looper;
import android.os.RemoteException;
import android.util.Log;
import com.google.android.gms.auth.api.signin.GoogleSignInAccount;
import com.google.android.gms.auth.api.signin.internal.zzy;
import com.google.android.gms.common.api.GoogleApiClient.ConnectionCallbacks;
import com.google.android.gms.common.api.GoogleApiClient.OnConnectionFailedListener;
import com.google.android.gms.common.internal.zzaa;
import com.google.android.gms.common.internal.zzam;
import com.google.android.gms.common.internal.zzbp;
import com.google.android.gms.common.internal.zzbq;
import com.google.android.gms.common.internal.zzm;
import com.google.android.gms.common.internal.zzq;

public final class zzcpw extends zzaa<zzcpu> implements zzcpm {
    private final zzq zzfkd;
    private Integer zzfto;
    private final boolean zzjnk;
    private final Bundle zzjnl;

    private zzcpw(Context context, Looper looper, boolean z, zzq zzq, Bundle bundle, ConnectionCallbacks connectionCallbacks, OnConnectionFailedListener onConnectionFailedListener) {
        super(context, looper, 44, zzq, connectionCallbacks, onConnectionFailedListener);
        this.zzjnk = true;
        this.zzfkd = zzq;
        this.zzjnl = bundle;
        this.zzfto = zzq.zzajy();
    }

    public zzcpw(Context context, Looper looper, boolean z, zzq zzq, zzcpn zzcpn, ConnectionCallbacks connectionCallbacks, OnConnectionFailedListener onConnectionFailedListener) {
        this(context, looper, true, zzq, zza(zzq), connectionCallbacks, onConnectionFailedListener);
    }

    public static Bundle zza(zzq zzq) {
        zzcpn zzajx = zzq.zzajx();
        Integer zzajy = zzq.zzajy();
        Bundle bundle = new Bundle();
        bundle.putParcelable("com.google.android.gms.signin.internal.clientRequestedAccount", zzq.getAccount());
        if (zzajy != null) {
            bundle.putInt("com.google.android.gms.common.internal.ClientSettings.sessionId", zzajy.intValue());
        }
        if (zzajx != null) {
            bundle.putBoolean("com.google.android.gms.signin.internal.offlineAccessRequested", zzajx.zzbbu());
            bundle.putBoolean("com.google.android.gms.signin.internal.idTokenRequested", zzajx.isIdTokenRequested());
            bundle.putString("com.google.android.gms.signin.internal.serverClientId", zzajx.getServerClientId());
            bundle.putBoolean("com.google.android.gms.signin.internal.usePromptModeForAuthCode", true);
            bundle.putBoolean("com.google.android.gms.signin.internal.forceCodeForRefreshToken", zzajx.zzbbv());
            bundle.putString("com.google.android.gms.signin.internal.hostedDomain", zzajx.zzbbw());
            bundle.putBoolean("com.google.android.gms.signin.internal.waitForAccessTokenRefresh", zzajx.zzbbx());
            if (zzajx.zzbby() != null) {
                bundle.putLong("com.google.android.gms.signin.internal.authApiSignInModuleVersion", zzajx.zzbby().longValue());
            }
            if (zzajx.zzbbz() != null) {
                bundle.putLong("com.google.android.gms.signin.internal.realClientLibraryVersion", zzajx.zzbbz().longValue());
            }
        }
        return bundle;
    }

    public final void connect() {
        zza(new zzm(this));
    }

    public final void zza(zzam zzam, boolean z) {
        try {
            ((zzcpu) zzajj()).zza(zzam, this.zzfto.intValue(), z);
        } catch (RemoteException e) {
            Log.w("SignInClientImpl", "Remote service probably died when saveDefaultAccount is called");
        }
    }

    public final void zza(zzcps zzcps) {
        zzbp.zzb((Object) zzcps, (Object) "Expecting a valid ISignInCallbacks");
        try {
            Account zzajp = this.zzfkd.zzajp();
            GoogleSignInAccount googleSignInAccount = null;
            if ("<<default account>>".equals(zzajp.name)) {
                googleSignInAccount = zzy.zzbm(getContext()).zzaar();
            }
            ((zzcpu) zzajj()).zza(new zzcpx(new zzbq(zzajp, this.zzfto.intValue(), googleSignInAccount)), zzcps);
        } catch (Throwable e) {
            Log.w("SignInClientImpl", "Remote service probably died when signIn is called");
            try {
                zzcps.zzb(new zzcpz(8));
            } catch (RemoteException e2) {
                Log.wtf("SignInClientImpl", "ISignInCallbacks#onSignInComplete should be executed from the same process, unexpected RemoteException.", e);
            }
        }
    }

    public final boolean zzaaa() {
        return this.zzjnk;
    }

    public final void zzbbt() {
        try {
            ((zzcpu) zzajj()).zzeb(this.zzfto.intValue());
        } catch (RemoteException e) {
            Log.w("SignInClientImpl", "Remote service probably died when clearAccountFromSessionStore is called");
        }
    }

    protected final /* synthetic */ IInterface zze(IBinder iBinder) {
        if (iBinder == null) {
            return null;
        }
        IInterface queryLocalInterface = iBinder.queryLocalInterface("com.google.android.gms.signin.internal.ISignInService");
        return queryLocalInterface instanceof zzcpu ? (zzcpu) queryLocalInterface : new zzcpv(iBinder);
    }

    protected final String zzhc() {
        return "com.google.android.gms.signin.service.START";
    }

    protected final String zzhd() {
        return "com.google.android.gms.signin.internal.ISignInService";
    }

    protected final Bundle zzzs() {
        if (!getContext().getPackageName().equals(this.zzfkd.zzaju())) {
            this.zzjnl.putString("com.google.android.gms.signin.internal.realClientPackageName", this.zzfkd.zzaju());
        }
        return this.zzjnl;
    }
}
