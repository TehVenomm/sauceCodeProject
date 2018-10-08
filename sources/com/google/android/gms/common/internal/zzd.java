package com.google.android.gms.common.internal;

import android.accounts.Account;
import android.app.PendingIntent;
import android.content.Context;
import android.content.Intent;
import android.os.Bundle;
import android.os.DeadObjectException;
import android.os.Handler;
import android.os.IBinder;
import android.os.IInterface;
import android.os.Looper;
import android.os.RemoteException;
import android.support.annotation.CallSuper;
import android.support.annotation.NonNull;
import android.support.annotation.Nullable;
import android.support.annotation.WorkerThread;
import android.text.TextUtils;
import android.util.Log;
import com.google.android.gms.common.ConnectionResult;
import com.google.android.gms.common.api.CommonStatusCodes;
import com.google.android.gms.common.api.Scope;
import com.google.android.gms.common.zzc;
import com.google.android.gms.common.zze;
import java.io.FileDescriptor;
import java.io.PrintWriter;
import java.text.SimpleDateFormat;
import java.util.ArrayList;
import java.util.Collections;
import java.util.Date;
import java.util.Locale;
import java.util.Set;
import java.util.concurrent.atomic.AtomicInteger;

public abstract class zzd<T extends IInterface> {
    private static String[] zzftd = new String[]{"service_esmobile", "service_googleme"};
    private final Context mContext;
    final Handler mHandler;
    private final Object mLock;
    private final Looper zzakl;
    private final zze zzfki;
    private int zzfsi;
    private long zzfsj;
    private long zzfsk;
    private int zzfsl;
    private long zzfsm;
    private zzal zzfsn;
    private final zzaf zzfso;
    private final Object zzfsp;
    private zzax zzfsq;
    protected zzj zzfsr;
    private T zzfss;
    private final ArrayList<zzi<?>> zzfst;
    private zzl zzfsu;
    private int zzfsv;
    private final zzf zzfsw;
    private final zzg zzfsx;
    private final int zzfsy;
    private final String zzfsz;
    private ConnectionResult zzfta;
    private boolean zzftb;
    protected AtomicInteger zzftc;

    protected zzd(Context context, Looper looper, int i, zzf zzf, zzg zzg, String str) {
        this(context, looper, zzaf.zzcf(context), zze.zzaew(), i, (zzf) zzbp.zzu(zzf), (zzg) zzbp.zzu(zzg), null);
    }

    protected zzd(Context context, Looper looper, zzaf zzaf, zze zze, int i, zzf zzf, zzg zzg, String str) {
        this.mLock = new Object();
        this.zzfsp = new Object();
        this.zzfst = new ArrayList();
        this.zzfsv = 1;
        this.zzfta = null;
        this.zzftb = false;
        this.zzftc = new AtomicInteger(0);
        this.mContext = (Context) zzbp.zzb((Object) context, (Object) "Context must not be null");
        this.zzakl = (Looper) zzbp.zzb((Object) looper, (Object) "Looper must not be null");
        this.zzfso = (zzaf) zzbp.zzb((Object) zzaf, (Object) "Supervisor must not be null");
        this.zzfki = (zze) zzbp.zzb((Object) zze, (Object) "API availability must not be null");
        this.mHandler = new zzh(this, looper);
        this.zzfsy = i;
        this.zzfsw = zzf;
        this.zzfsx = zzg;
        this.zzfsz = str;
    }

    private final void zza(int i, T t) {
        boolean z = false;
        if ((i == 4) == (t != null)) {
            z = true;
        }
        zzbp.zzbh(z);
        synchronized (this.mLock) {
            this.zzfsv = i;
            this.zzfss = t;
            switch (i) {
                case 1:
                    if (this.zzfsu != null) {
                        this.zzfso.zza(zzhc(), zzajd(), 129, this.zzfsu, zzaje());
                        this.zzfsu = null;
                        break;
                    }
                    break;
                case 2:
                case 3:
                    String zzakk;
                    String packageName;
                    if (!(this.zzfsu == null || this.zzfsn == null)) {
                        zzakk = this.zzfsn.zzakk();
                        packageName = this.zzfsn.getPackageName();
                        Log.e("GmsClient", new StringBuilder((String.valueOf(zzakk).length() + 70) + String.valueOf(packageName).length()).append("Calling connect() while still connected, missing disconnect() for ").append(zzakk).append(" on ").append(packageName).toString());
                        this.zzfso.zza(this.zzfsn.zzakk(), this.zzfsn.getPackageName(), this.zzfsn.zzakg(), this.zzfsu, zzaje());
                        this.zzftc.incrementAndGet();
                    }
                    this.zzfsu = new zzl(this, this.zzftc.get());
                    this.zzfsn = new zzal(zzajd(), zzhc(), false, 129);
                    if (!this.zzfso.zza(new zzag(this.zzfsn.zzakk(), this.zzfsn.getPackageName(), this.zzfsn.zzakg()), this.zzfsu, zzaje())) {
                        zzakk = this.zzfsn.zzakk();
                        packageName = this.zzfsn.getPackageName();
                        Log.e("GmsClient", new StringBuilder((String.valueOf(zzakk).length() + 34) + String.valueOf(packageName).length()).append("unable to connect to service: ").append(zzakk).append(" on ").append(packageName).toString());
                        zza(16, null, this.zzftc.get());
                        break;
                    }
                    break;
                case 4:
                    zza((IInterface) t);
                    break;
            }
        }
    }

    private final boolean zza(int i, int i2, T t) {
        boolean z;
        synchronized (this.mLock) {
            if (this.zzfsv != i) {
                z = false;
            } else {
                zza(i2, (IInterface) t);
                z = true;
            }
        }
        return z;
    }

    @Nullable
    private final String zzaje() {
        return this.zzfsz == null ? this.mContext.getClass().getName() : this.zzfsz;
    }

    private final boolean zzajg() {
        boolean z;
        synchronized (this.mLock) {
            z = this.zzfsv == 3;
        }
        return z;
    }

    private final boolean zzajm() {
        if (this.zzftb || TextUtils.isEmpty(zzhd()) || TextUtils.isEmpty(null)) {
            return false;
        }
        try {
            Class.forName(zzhd());
            return true;
        } catch (ClassNotFoundException e) {
            return false;
        }
    }

    private final void zzcc(int i) {
        int i2;
        if (zzajg()) {
            i2 = 5;
            this.zzftb = true;
        } else {
            i2 = 4;
        }
        this.mHandler.sendMessage(this.mHandler.obtainMessage(i2, this.zzftc.get(), 16));
    }

    public void disconnect() {
        this.zzftc.incrementAndGet();
        synchronized (this.zzfst) {
            int size = this.zzfst.size();
            for (int i = 0; i < size; i++) {
                ((zzi) this.zzfst.get(i)).removeListener();
            }
            this.zzfst.clear();
        }
        synchronized (this.zzfsp) {
            this.zzfsq = null;
        }
        zza(1, null);
    }

    public final void dump(String str, FileDescriptor fileDescriptor, PrintWriter printWriter, String[] strArr) {
        synchronized (this.mLock) {
            int i = this.zzfsv;
            IInterface iInterface = this.zzfss;
        }
        synchronized (this.zzfsp) {
            zzax zzax = this.zzfsq;
        }
        printWriter.append(str).append("mConnectState=");
        switch (i) {
            case 1:
                printWriter.print("DISCONNECTED");
                break;
            case 2:
                printWriter.print("REMOTE_CONNECTING");
                break;
            case 3:
                printWriter.print("LOCAL_CONNECTING");
                break;
            case 4:
                printWriter.print("CONNECTED");
                break;
            case 5:
                printWriter.print("DISCONNECTING");
                break;
            default:
                printWriter.print("UNKNOWN");
                break;
        }
        printWriter.append(" mService=");
        if (iInterface == null) {
            printWriter.append("null");
        } else {
            printWriter.append(zzhd()).append("@").append(Integer.toHexString(System.identityHashCode(iInterface.asBinder())));
        }
        printWriter.append(" mServiceBroker=");
        if (zzax == null) {
            printWriter.println("null");
        } else {
            printWriter.append("IGmsServiceBroker@").println(Integer.toHexString(System.identityHashCode(zzax.asBinder())));
        }
        SimpleDateFormat simpleDateFormat = new SimpleDateFormat("yyyy-MM-dd HH:mm:ss.SSS", Locale.US);
        if (this.zzfsk > 0) {
            PrintWriter append = printWriter.append(str).append("lastConnectedTime=");
            long j = this.zzfsk;
            String format = simpleDateFormat.format(new Date(this.zzfsk));
            append.println(new StringBuilder(String.valueOf(format).length() + 21).append(j).append(" ").append(format).toString());
        }
        if (this.zzfsj > 0) {
            printWriter.append(str).append("lastSuspendedCause=");
            switch (this.zzfsi) {
                case 1:
                    printWriter.append("CAUSE_SERVICE_DISCONNECTED");
                    break;
                case 2:
                    printWriter.append("CAUSE_NETWORK_LOST");
                    break;
                default:
                    printWriter.append(String.valueOf(this.zzfsi));
                    break;
            }
            append = printWriter.append(" lastSuspendedTime=");
            j = this.zzfsj;
            format = simpleDateFormat.format(new Date(this.zzfsj));
            append.println(new StringBuilder(String.valueOf(format).length() + 21).append(j).append(" ").append(format).toString());
        }
        if (this.zzfsm > 0) {
            printWriter.append(str).append("lastFailedStatus=").append(CommonStatusCodes.getStatusCodeString(this.zzfsl));
            append = printWriter.append(" lastFailedTime=");
            j = this.zzfsm;
            String format2 = simpleDateFormat.format(new Date(this.zzfsm));
            append.println(new StringBuilder(String.valueOf(format2).length() + 21).append(j).append(" ").append(format2).toString());
        }
    }

    public Account getAccount() {
        return null;
    }

    public final Context getContext() {
        return this.mContext;
    }

    public final Looper getLooper() {
        return this.zzakl;
    }

    public final boolean isConnected() {
        boolean z;
        synchronized (this.mLock) {
            z = this.zzfsv == 4;
        }
        return z;
    }

    public final boolean isConnecting() {
        boolean z;
        synchronized (this.mLock) {
            z = this.zzfsv == 2 || this.zzfsv == 3;
        }
        return z;
    }

    @CallSuper
    protected void onConnectionFailed(ConnectionResult connectionResult) {
        this.zzfsl = connectionResult.getErrorCode();
        this.zzfsm = System.currentTimeMillis();
    }

    @CallSuper
    protected final void onConnectionSuspended(int i) {
        this.zzfsi = i;
        this.zzfsj = System.currentTimeMillis();
    }

    protected final void zza(int i, @Nullable Bundle bundle, int i2) {
        this.mHandler.sendMessage(this.mHandler.obtainMessage(7, i2, -1, new zzo(this, i, null)));
    }

    protected void zza(int i, IBinder iBinder, Bundle bundle, int i2) {
        this.mHandler.sendMessage(this.mHandler.obtainMessage(1, i2, -1, new zzn(this, i, iBinder, bundle)));
    }

    @CallSuper
    protected void zza(@NonNull T t) {
        this.zzfsk = System.currentTimeMillis();
    }

    @WorkerThread
    public final void zza(zzam zzam, Set<Scope> set) {
        Throwable e;
        Bundle zzzs = zzzs();
        zzy zzy = new zzy(this.zzfsy);
        zzy.zzfty = this.mContext.getPackageName();
        zzy.zzfub = zzzs;
        if (set != null) {
            zzy.zzfua = (Scope[]) set.toArray(new Scope[set.size()]);
        }
        if (zzaaa()) {
            zzy.zzfuc = getAccount() != null ? getAccount() : new Account("<<default account>>", "com.google");
            if (zzam != null) {
                zzy.zzftz = zzam.asBinder();
            }
        } else if (zzajk()) {
            zzy.zzfuc = getAccount();
        }
        zzy.zzfud = zzajh();
        try {
            synchronized (this.zzfsp) {
                if (this.zzfsq != null) {
                    this.zzfsq.zza(new zzk(this, this.zzftc.get()), zzy);
                } else {
                    Log.w("GmsClient", "mServiceBroker is null, client disconnected");
                }
            }
        } catch (Throwable e2) {
            Log.w("GmsClient", "IGmsServiceBroker.getService failed", e2);
            zzcb(1);
        } catch (SecurityException e3) {
            throw e3;
        } catch (RemoteException e4) {
            e2 = e4;
            Log.w("GmsClient", "IGmsServiceBroker.getService failed", e2);
            zza(8, null, null, this.zzftc.get());
        } catch (RuntimeException e5) {
            e2 = e5;
            Log.w("GmsClient", "IGmsServiceBroker.getService failed", e2);
            zza(8, null, null, this.zzftc.get());
        }
    }

    public void zza(@NonNull zzj zzj) {
        this.zzfsr = (zzj) zzbp.zzb((Object) zzj, (Object) "Connection progress callbacks cannot be null.");
        zza(2, null);
    }

    protected final void zza(@NonNull zzj zzj, int i, @Nullable PendingIntent pendingIntent) {
        this.zzfsr = (zzj) zzbp.zzb((Object) zzj, (Object) "Connection progress callbacks cannot be null.");
        this.mHandler.sendMessage(this.mHandler.obtainMessage(3, this.zzftc.get(), i, pendingIntent));
    }

    public boolean zzaaa() {
        return false;
    }

    public boolean zzaak() {
        return false;
    }

    public Intent zzaal() {
        throw new UnsupportedOperationException("Not a sign in API");
    }

    public Bundle zzaeg() {
        return null;
    }

    public boolean zzafe() {
        return true;
    }

    @Nullable
    public final IBinder zzaff() {
        IBinder iBinder;
        synchronized (this.zzfsp) {
            if (this.zzfsq == null) {
                iBinder = null;
            } else {
                iBinder = this.zzfsq.asBinder();
            }
        }
        return iBinder;
    }

    protected String zzajd() {
        return "com.google.android.gms";
    }

    public final void zzajf() {
        int isGooglePlayServicesAvailable = this.zzfki.isGooglePlayServicesAvailable(this.mContext);
        if (isGooglePlayServicesAvailable != 0) {
            zza(1, null);
            zza(new zzm(this), isGooglePlayServicesAvailable, null);
            return;
        }
        zza(new zzm(this));
    }

    public zzc[] zzajh() {
        return new zzc[0];
    }

    protected final void zzaji() {
        if (!isConnected()) {
            throw new IllegalStateException("Not connected. Call connect() and wait for onConnected() to be called.");
        }
    }

    public final T zzajj() throws DeadObjectException {
        T t;
        synchronized (this.mLock) {
            if (this.zzfsv == 5) {
                throw new DeadObjectException();
            }
            zzaji();
            zzbp.zza(this.zzfss != null, (Object) "Client is connected but service is null");
            t = this.zzfss;
        }
        return t;
    }

    public boolean zzajk() {
        return false;
    }

    protected Set<Scope> zzajl() {
        return Collections.EMPTY_SET;
    }

    public final void zzcb(int i) {
        this.mHandler.sendMessage(this.mHandler.obtainMessage(6, this.zzftc.get(), i));
    }

    @Nullable
    protected abstract T zze(IBinder iBinder);

    @NonNull
    protected abstract String zzhc();

    @NonNull
    protected abstract String zzhd();

    protected Bundle zzzs() {
        return new Bundle();
    }
}
