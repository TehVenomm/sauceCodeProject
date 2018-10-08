package com.google.android.gms.common.api;

import android.accounts.Account;
import android.content.Context;
import android.content.Intent;
import android.os.IBinder;
import android.os.IInterface;
import android.os.Looper;
import android.support.annotation.Nullable;
import com.google.android.gms.common.api.GoogleApiClient.ConnectionCallbacks;
import com.google.android.gms.common.api.GoogleApiClient.OnConnectionFailedListener;
import com.google.android.gms.common.internal.zzam;
import com.google.android.gms.common.internal.zzbp;
import com.google.android.gms.common.internal.zzj;
import com.google.android.gms.common.internal.zzq;
import com.google.android.gms.nearby.messages.Strategy;
import java.io.FileDescriptor;
import java.io.PrintWriter;
import java.util.Collections;
import java.util.List;
import java.util.Set;

public final class Api<O extends ApiOptions> {
    private final String mName;
    private final zza<?, O> zzffz;
    private final zzh<?, O> zzfga = null;
    private final zzf<?> zzfgb;
    private final zzi<?> zzfgc;

    public interface zzb {
    }

    public static class zzd<T extends zzb, O> {
        public int getPriority() {
            return Strategy.TTL_SECONDS_INFINITE;
        }

        public List<Scope> zzn(O o) {
            return Collections.emptyList();
        }
    }

    public static abstract class zza<T extends zze, O> extends zzd<T, O> {
        public abstract T zza(Context context, Looper looper, zzq zzq, O o, ConnectionCallbacks connectionCallbacks, OnConnectionFailedListener onConnectionFailedListener);
    }

    public interface ApiOptions {

        public interface HasOptions extends ApiOptions {
        }

        public interface NotRequiredOptions extends ApiOptions {
        }

        public interface Optional extends HasOptions, NotRequiredOptions {
        }

        public interface HasAccountOptions extends HasOptions, NotRequiredOptions {
            Account getAccount();
        }

        public static final class NoOptions implements NotRequiredOptions {
            private NoOptions() {
            }
        }
    }

    public interface zze extends zzb {
        void disconnect();

        void dump(String str, FileDescriptor fileDescriptor, PrintWriter printWriter, String[] strArr);

        boolean isConnected();

        boolean isConnecting();

        void zza(zzam zzam, Set<Scope> set);

        void zza(zzj zzj);

        boolean zzaaa();

        boolean zzaak();

        Intent zzaal();

        boolean zzafe();

        @Nullable
        IBinder zzaff();
    }

    public static class zzc<C extends zzb> {
    }

    public static final class zzf<C extends zze> extends zzc<C> {
    }

    public interface zzg<T extends IInterface> extends zzb {
    }

    public static class zzh<T extends zzg, O> extends zzd<T, O> {
    }

    public static final class zzi<C extends zzg> extends zzc<C> {
    }

    public <C extends zze> Api(String str, zza<C, O> zza, zzf<C> zzf) {
        zzbp.zzb((Object) zza, (Object) "Cannot construct an Api with a null ClientBuilder");
        zzbp.zzb((Object) zzf, (Object) "Cannot construct an Api with a null ClientKey");
        this.mName = str;
        this.zzffz = zza;
        this.zzfgb = zzf;
        this.zzfgc = null;
    }

    public final String getName() {
        return this.mName;
    }

    public final zzd<?, O> zzafb() {
        return this.zzffz;
    }

    public final zza<?, O> zzafc() {
        zzbp.zza(this.zzffz != null, (Object) "This API was constructed with a SimpleClientBuilder. Use getSimpleClientBuilder");
        return this.zzffz;
    }

    public final zzc<?> zzafd() {
        if (this.zzfgb != null) {
            return this.zzfgb;
        }
        throw new IllegalStateException("This API was constructed with null client keys. This should not be possible.");
    }
}
