package com.google.android.gms.common.api;

import android.accounts.Account;
import android.app.Activity;
import android.content.Context;
import android.os.Handler;
import android.os.Looper;
import android.support.annotation.MainThread;
import android.support.annotation.NonNull;
import android.support.annotation.WorkerThread;
import com.google.android.gms.auth.api.signin.GoogleSignInOptions;
import com.google.android.gms.auth.api.signin.internal.zzy;
import com.google.android.gms.common.api.Api.ApiOptions;
import com.google.android.gms.common.api.Api.ApiOptions.HasAccountOptions;
import com.google.android.gms.common.api.Api.zzb;
import com.google.android.gms.common.api.Api.zze;
import com.google.android.gms.common.api.internal.zzak;
import com.google.android.gms.common.api.internal.zzbp;
import com.google.android.gms.common.api.internal.zzbr;
import com.google.android.gms.common.api.internal.zzbx;
import com.google.android.gms.common.api.internal.zzcw;
import com.google.android.gms.common.api.internal.zzcz;
import com.google.android.gms.common.api.internal.zzdd;
import com.google.android.gms.common.api.internal.zzg;
import com.google.android.gms.common.api.internal.zzh;
import com.google.android.gms.common.api.internal.zzm;
import com.google.android.gms.common.internal.zzr;
import com.google.android.gms.tasks.Task;
import com.google.android.gms.tasks.TaskCompletionSource;

public class GoogleApi<O extends ApiOptions> {
    private final Context mContext;
    private final int mId;
    private final Looper zzakl;
    private final Api<O> zzfda;
    private final O zzfgl;
    private final zzh<O> zzfgm;
    private final GoogleApiClient zzfgn;
    private final zzcz zzfgo;
    protected final zzbp zzfgp;

    public static final class zza {
        public static final zza zzfgq = new zze().zzafm();
        public final zzcz zzfgr;
        public final Looper zzfgs;

        private zza(zzcz zzcz, Account account, Looper looper) {
            this.zzfgr = zzcz;
            this.zzfgs = looper;
        }
    }

    @MainThread
    public GoogleApi(@NonNull Activity activity, Api<O> api, O o, zza zza) {
        com.google.android.gms.common.internal.zzbp.zzb((Object) activity, (Object) "Null activity is not permitted.");
        com.google.android.gms.common.internal.zzbp.zzb((Object) api, (Object) "Api must not be null.");
        com.google.android.gms.common.internal.zzbp.zzb((Object) zza, (Object) "Settings must not be null; use Settings.DEFAULT_SETTINGS instead.");
        this.mContext = activity.getApplicationContext();
        this.zzfda = api;
        this.zzfgl = o;
        this.zzakl = zza.zzfgs;
        this.zzfgm = zzh.zza(this.zzfda, this.zzfgl);
        this.zzfgn = new zzbx(this);
        this.zzfgp = zzbp.zzcb(this.mContext);
        this.mId = this.zzfgp.zzahp();
        this.zzfgo = zza.zzfgr;
        zzak.zza(activity, this.zzfgp, this.zzfgm);
        this.zzfgp.zzb(this);
    }

    @Deprecated
    public GoogleApi(@NonNull Activity activity, Api<O> api, O o, zzcz zzcz) {
        this(activity, (Api) api, (ApiOptions) o, new zze().zza(zzcz).zza(activity.getMainLooper()).zzafm());
    }

    protected GoogleApi(@NonNull Context context, Api<O> api, Looper looper) {
        com.google.android.gms.common.internal.zzbp.zzb((Object) context, (Object) "Null context is not permitted.");
        com.google.android.gms.common.internal.zzbp.zzb((Object) api, (Object) "Api must not be null.");
        com.google.android.gms.common.internal.zzbp.zzb((Object) looper, (Object) "Looper must not be null.");
        this.mContext = context.getApplicationContext();
        this.zzfda = api;
        this.zzfgl = null;
        this.zzakl = looper;
        this.zzfgm = zzh.zzb(api);
        this.zzfgn = new zzbx(this);
        this.zzfgp = zzbp.zzcb(this.mContext);
        this.mId = this.zzfgp.zzahp();
        this.zzfgo = new zzg();
    }

    @Deprecated
    public GoogleApi(@NonNull Context context, Api<O> api, O o, Looper looper, zzcz zzcz) {
        this(context, (Api) api, null, new zze().zza(looper).zza(zzcz).zzafm());
    }

    public GoogleApi(@NonNull Context context, Api<O> api, O o, zza zza) {
        com.google.android.gms.common.internal.zzbp.zzb((Object) context, (Object) "Null context is not permitted.");
        com.google.android.gms.common.internal.zzbp.zzb((Object) api, (Object) "Api must not be null.");
        com.google.android.gms.common.internal.zzbp.zzb((Object) zza, (Object) "Settings must not be null; use Settings.DEFAULT_SETTINGS instead.");
        this.mContext = context.getApplicationContext();
        this.zzfda = api;
        this.zzfgl = o;
        this.zzakl = zza.zzfgs;
        this.zzfgm = zzh.zza(this.zzfda, this.zzfgl);
        this.zzfgn = new zzbx(this);
        this.zzfgp = zzbp.zzcb(this.mContext);
        this.mId = this.zzfgp.zzahp();
        this.zzfgo = zza.zzfgr;
        this.zzfgp.zzb(this);
    }

    @Deprecated
    public GoogleApi(@NonNull Context context, Api<O> api, O o, zzcz zzcz) {
        this(context, (Api) api, (ApiOptions) o, new zze().zza(zzcz).zzafm());
    }

    private final <A extends zzb, T extends zzm<? extends Result, A>> T zza(int i, @NonNull T t) {
        t.zzagf();
        this.zzfgp.zza(this, i, (zzm) t);
        return t;
    }

    private final <TResult, A extends zzb> Task<TResult> zza(int i, @NonNull zzdd<A, TResult> zzdd) {
        TaskCompletionSource taskCompletionSource = new TaskCompletionSource();
        this.zzfgp.zza(this, i, zzdd, taskCompletionSource, this.zzfgo);
        return taskCompletionSource.getTask();
    }

    private final zzr zzafl() {
        return new zzr().zze(this.zzfgl instanceof HasAccountOptions ? ((HasAccountOptions) this.zzfgl).getAccount() : null);
    }

    public final Context getApplicationContext() {
        return this.mContext;
    }

    public final int getInstanceId() {
        return this.mId;
    }

    public final Looper getLooper() {
        return this.zzakl;
    }

    @WorkerThread
    public zze zza(Looper looper, zzbr<O> zzbr) {
        return this.zzfda.zzafc().zza(this.mContext, looper, zzafl().zzfy(this.mContext.getPackageName()).zzfz(this.mContext.getClass().getName()).zzajz(), this.zzfgl, zzbr, zzbr);
    }

    public zzcw zza(Context context, Handler handler) {
        zzr zzafl = zzafl();
        GoogleSignInOptions zzaas = zzy.zzbm(this.mContext).zzaas();
        if (zzaas != null) {
            zzafl.zze(zzaas.zzaae());
        }
        return new zzcw(context, handler, zzafl.zzajz());
    }

    public final <A extends zzb, T extends zzm<? extends Result, A>> T zza(@NonNull T t) {
        return zza(0, (zzm) t);
    }

    public final <TResult, A extends zzb> Task<TResult> zza(zzdd<A, TResult> zzdd) {
        return zza(0, (zzdd) zzdd);
    }

    public final Api<O> zzafi() {
        return this.zzfda;
    }

    public final zzh<O> zzafj() {
        return this.zzfgm;
    }

    public final GoogleApiClient zzafk() {
        return this.zzfgn;
    }

    public final <A extends zzb, T extends zzm<? extends Result, A>> T zzb(@NonNull T t) {
        return zza(1, (zzm) t);
    }

    public final <TResult, A extends zzb> Task<TResult> zzb(zzdd<A, TResult> zzdd) {
        return zza(1, (zzdd) zzdd);
    }

    public final <A extends zzb, T extends zzm<? extends Result, A>> T zzc(@NonNull T t) {
        return zza(2, (zzm) t);
    }
}
