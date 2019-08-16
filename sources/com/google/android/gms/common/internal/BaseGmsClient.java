package com.google.android.gms.common.internal;

import android.accounts.Account;
import android.app.PendingIntent;
import android.content.ComponentName;
import android.content.Context;
import android.content.Intent;
import android.content.ServiceConnection;
import android.os.Bundle;
import android.os.DeadObjectException;
import android.os.Handler;
import android.os.IBinder;
import android.os.IInterface;
import android.os.Looper;
import android.os.Message;
import android.os.RemoteException;
import android.support.annotation.BinderThread;
import android.support.annotation.CallSuper;
import android.support.annotation.NonNull;
import android.support.annotation.Nullable;
import android.support.annotation.WorkerThread;
import android.text.TextUtils;
import android.util.Log;
import com.google.android.gms.common.ConnectionResult;
import com.google.android.gms.common.Feature;
import com.google.android.gms.common.GoogleApiAvailabilityLight;
import com.google.android.gms.common.annotation.KeepForSdk;
import com.google.android.gms.common.api.CommonStatusCodes;
import com.google.android.gms.common.api.Scope;
import com.google.android.gms.common.util.VisibleForTesting;
import java.io.FileDescriptor;
import java.io.PrintWriter;
import java.text.SimpleDateFormat;
import java.util.ArrayList;
import java.util.Collections;
import java.util.Date;
import java.util.Locale;
import java.util.Set;
import java.util.concurrent.atomic.AtomicInteger;
import javax.annotation.concurrent.GuardedBy;

@KeepForSdk
public abstract class BaseGmsClient<T extends IInterface> {
    @KeepForSdk
    public static final int CONNECT_STATE_CONNECTED = 4;
    @KeepForSdk
    public static final int CONNECT_STATE_DISCONNECTED = 1;
    @KeepForSdk
    public static final int CONNECT_STATE_DISCONNECTING = 5;
    @KeepForSdk
    public static final String DEFAULT_ACCOUNT = "<<default account>>";
    @KeepForSdk
    public static final String[] GOOGLE_PLUS_REQUIRED_FEATURES = {"service_esmobile", "service_googleme"};
    @KeepForSdk
    public static final String KEY_PENDING_INTENT = "pendingIntent";
    private static final Feature[] zzbt = new Feature[0];
    private final Context mContext;
    final Handler mHandler;
    private final Object mLock;
    private int zzbu;
    private long zzbv;
    private long zzbw;
    private int zzbx;
    private long zzby;
    @VisibleForTesting
    private zzh zzbz;
    private final Looper zzca;
    private final GmsClientSupervisor zzcb;
    private final GoogleApiAvailabilityLight zzcc;
    /* access modifiers changed from: private */
    public final Object zzcd;
    /* access modifiers changed from: private */
    @GuardedBy("mServiceBrokerLock")
    public IGmsServiceBroker zzce;
    @VisibleForTesting
    protected ConnectionProgressReportCallbacks zzcf;
    @GuardedBy("mLock")
    private T zzcg;
    /* access modifiers changed from: private */
    public final ArrayList<zzc<?>> zzch;
    @GuardedBy("mLock")
    private zze zzci;
    @GuardedBy("mLock")
    private int zzcj;
    /* access modifiers changed from: private */
    public final BaseConnectionCallbacks zzck;
    /* access modifiers changed from: private */
    public final BaseOnConnectionFailedListener zzcl;
    private final int zzcm;
    private final String zzcn;
    /* access modifiers changed from: private */
    public ConnectionResult zzco;
    /* access modifiers changed from: private */
    public boolean zzcp;
    private volatile zzb zzcq;
    @VisibleForTesting
    protected AtomicInteger zzcr;

    @KeepForSdk
    public interface BaseConnectionCallbacks {
        @KeepForSdk
        void onConnected(@Nullable Bundle bundle);

        @KeepForSdk
        void onConnectionSuspended(int i);
    }

    @KeepForSdk
    public interface BaseOnConnectionFailedListener {
        void onConnectionFailed(@NonNull ConnectionResult connectionResult);
    }

    @KeepForSdk
    public interface ConnectionProgressReportCallbacks {
        @KeepForSdk
        void onReportServiceBinding(@NonNull ConnectionResult connectionResult);
    }

    protected class LegacyClientCallbackAdapter implements ConnectionProgressReportCallbacks {
        @KeepForSdk
        public LegacyClientCallbackAdapter() {
        }

        public void onReportServiceBinding(@NonNull ConnectionResult connectionResult) {
            if (connectionResult.isSuccess()) {
                BaseGmsClient.this.getRemoteService(null, BaseGmsClient.this.getScopes());
            } else if (BaseGmsClient.this.zzcl != null) {
                BaseGmsClient.this.zzcl.onConnectionFailed(connectionResult);
            }
        }
    }

    @KeepForSdk
    public interface SignOutCallbacks {
        @KeepForSdk
        void onSignOutComplete();
    }

    private abstract class zza extends zzc<Boolean> {
        private final int statusCode;
        private final Bundle zzcs;

        @BinderThread
        protected zza(int i, Bundle bundle) {
            super(Boolean.valueOf(true));
            this.statusCode = i;
            this.zzcs = bundle;
        }

        /* access modifiers changed from: protected */
        public abstract void zza(ConnectionResult connectionResult);

        /* access modifiers changed from: protected */
        public final /* synthetic */ void zza(Object obj) {
            PendingIntent pendingIntent = null;
            if (((Boolean) obj) == null) {
                BaseGmsClient.this.zza(1, null);
                return;
            }
            switch (this.statusCode) {
                case 0:
                    if (!zzm()) {
                        BaseGmsClient.this.zza(1, null);
                        zza(new ConnectionResult(8, null));
                        return;
                    }
                    return;
                case 10:
                    BaseGmsClient.this.zza(1, null);
                    throw new IllegalStateException(String.format("A fatal developer error has occurred. Class name: %s. Start service action: %s. Service Descriptor: %s. ", new Object[]{getClass().getSimpleName(), BaseGmsClient.this.getStartServiceAction(), BaseGmsClient.this.getServiceDescriptor()}));
                default:
                    BaseGmsClient.this.zza(1, null);
                    if (this.zzcs != null) {
                        pendingIntent = (PendingIntent) this.zzcs.getParcelable(BaseGmsClient.KEY_PENDING_INTENT);
                    }
                    zza(new ConnectionResult(this.statusCode, pendingIntent));
                    return;
            }
        }

        /* access modifiers changed from: protected */
        public abstract boolean zzm();

        /* access modifiers changed from: protected */
        public final void zzn() {
        }
    }

    final class zzb extends com.google.android.gms.internal.common.zze {
        public zzb(Looper looper) {
            super(looper);
        }

        private static void zza(Message message) {
            zzc zzc = (zzc) message.obj;
            zzc.zzn();
            zzc.unregister();
        }

        private static boolean zzb(Message message) {
            return message.what == 2 || message.what == 1 || message.what == 7;
        }

        public final void handleMessage(Message message) {
            PendingIntent pendingIntent = null;
            if (BaseGmsClient.this.zzcr.get() != message.arg1) {
                if (zzb(message)) {
                    zza(message);
                }
            } else if ((message.what == 1 || message.what == 7 || ((message.what == 4 && !BaseGmsClient.this.enableLocalFallback()) || message.what == 5)) && !BaseGmsClient.this.isConnecting()) {
                zza(message);
            } else if (message.what == 4) {
                BaseGmsClient.this.zzco = new ConnectionResult(message.arg2);
                if (!BaseGmsClient.this.zzl() || BaseGmsClient.this.zzcp) {
                    ConnectionResult connectionResult = BaseGmsClient.this.zzco != null ? BaseGmsClient.this.zzco : new ConnectionResult(8);
                    BaseGmsClient.this.zzcf.onReportServiceBinding(connectionResult);
                    BaseGmsClient.this.onConnectionFailed(connectionResult);
                    return;
                }
                BaseGmsClient.this.zza(3, null);
            } else if (message.what == 5) {
                ConnectionResult connectionResult2 = BaseGmsClient.this.zzco != null ? BaseGmsClient.this.zzco : new ConnectionResult(8);
                BaseGmsClient.this.zzcf.onReportServiceBinding(connectionResult2);
                BaseGmsClient.this.onConnectionFailed(connectionResult2);
            } else if (message.what == 3) {
                if (message.obj instanceof PendingIntent) {
                    pendingIntent = (PendingIntent) message.obj;
                }
                ConnectionResult connectionResult3 = new ConnectionResult(message.arg2, pendingIntent);
                BaseGmsClient.this.zzcf.onReportServiceBinding(connectionResult3);
                BaseGmsClient.this.onConnectionFailed(connectionResult3);
            } else if (message.what == 6) {
                BaseGmsClient.this.zza(5, null);
                if (BaseGmsClient.this.zzck != null) {
                    BaseGmsClient.this.zzck.onConnectionSuspended(message.arg2);
                }
                BaseGmsClient.this.onConnectionSuspended(message.arg2);
                BaseGmsClient.this.zza(5, 1, null);
            } else if (message.what == 2 && !BaseGmsClient.this.isConnected()) {
                zza(message);
            } else if (zzb(message)) {
                ((zzc) message.obj).zzo();
            } else {
                Log.wtf("GmsClient", "Don't know how to handle message: " + message.what, new Exception());
            }
        }
    }

    protected abstract class zzc<TListener> {
        private TListener zzcu;
        private boolean zzcv = false;

        public zzc(TListener tlistener) {
            this.zzcu = tlistener;
        }

        public final void removeListener() {
            synchronized (this) {
                this.zzcu = null;
            }
        }

        public final void unregister() {
            removeListener();
            synchronized (BaseGmsClient.this.zzch) {
                BaseGmsClient.this.zzch.remove(this);
            }
        }

        /* access modifiers changed from: protected */
        public abstract void zza(TListener tlistener);

        /* access modifiers changed from: protected */
        public abstract void zzn();

        public final void zzo() {
            TListener tlistener;
            synchronized (this) {
                tlistener = this.zzcu;
                if (this.zzcv) {
                    String valueOf = String.valueOf(this);
                    Log.w("GmsClient", new StringBuilder(String.valueOf(valueOf).length() + 47).append("Callback proxy ").append(valueOf).append(" being reused. This is not safe.").toString());
                }
            }
            if (tlistener != null) {
                try {
                    zza(tlistener);
                } catch (RuntimeException e) {
                    zzn();
                    throw e;
                }
            } else {
                zzn();
            }
            synchronized (this) {
                this.zzcv = true;
            }
            unregister();
        }
    }

    @VisibleForTesting
    public static final class zzd extends com.google.android.gms.common.internal.IGmsCallbacks.zza {
        private BaseGmsClient zzcw;
        private final int zzcx;

        public zzd(@NonNull BaseGmsClient baseGmsClient, int i) {
            this.zzcw = baseGmsClient;
            this.zzcx = i;
        }

        @BinderThread
        public final void onPostInitComplete(int i, @NonNull IBinder iBinder, @Nullable Bundle bundle) {
            Preconditions.checkNotNull(this.zzcw, "onPostInitComplete can be called only once per call to getRemoteService");
            this.zzcw.onPostInitHandler(i, iBinder, bundle, this.zzcx);
            this.zzcw = null;
        }

        @BinderThread
        public final void zza(int i, @Nullable Bundle bundle) {
            Log.wtf("GmsClient", "received deprecated onAccountValidationComplete callback, ignoring", new Exception());
        }

        @BinderThread
        public final void zza(int i, @NonNull IBinder iBinder, @NonNull zzb zzb) {
            Preconditions.checkNotNull(this.zzcw, "onPostInitCompleteWithConnectionInfo can be called only once per call togetRemoteService");
            Preconditions.checkNotNull(zzb);
            this.zzcw.zza(zzb);
            onPostInitComplete(i, iBinder, zzb.zzda);
        }
    }

    @VisibleForTesting
    public final class zze implements ServiceConnection {
        private final int zzcx;

        public zze(int i) {
            this.zzcx = i;
        }

        public final void onServiceConnected(ComponentName componentName, IBinder iBinder) {
            IGmsServiceBroker zza;
            if (iBinder == null) {
                BaseGmsClient.this.zzb(16);
                return;
            }
            synchronized (BaseGmsClient.this.zzcd) {
                BaseGmsClient baseGmsClient = BaseGmsClient.this;
                if (iBinder == null) {
                    zza = null;
                } else {
                    IInterface queryLocalInterface = iBinder.queryLocalInterface("com.google.android.gms.common.internal.IGmsServiceBroker");
                    zza = (queryLocalInterface == null || !(queryLocalInterface instanceof IGmsServiceBroker)) ? new zza(iBinder) : (IGmsServiceBroker) queryLocalInterface;
                }
                baseGmsClient.zzce = zza;
            }
            BaseGmsClient.this.zza(0, (Bundle) null, this.zzcx);
        }

        public final void onServiceDisconnected(ComponentName componentName) {
            synchronized (BaseGmsClient.this.zzcd) {
                BaseGmsClient.this.zzce = null;
            }
            BaseGmsClient.this.mHandler.sendMessage(BaseGmsClient.this.mHandler.obtainMessage(6, this.zzcx, 1));
        }
    }

    protected final class zzf extends zza {
        private final IBinder zzcy;

        @BinderThread
        public zzf(int i, IBinder iBinder, Bundle bundle) {
            super(i, bundle);
            this.zzcy = iBinder;
        }

        /* access modifiers changed from: protected */
        public final void zza(ConnectionResult connectionResult) {
            if (BaseGmsClient.this.zzcl != null) {
                BaseGmsClient.this.zzcl.onConnectionFailed(connectionResult);
            }
            BaseGmsClient.this.onConnectionFailed(connectionResult);
        }

        /* access modifiers changed from: protected */
        public final boolean zzm() {
            try {
                String interfaceDescriptor = this.zzcy.getInterfaceDescriptor();
                if (!BaseGmsClient.this.getServiceDescriptor().equals(interfaceDescriptor)) {
                    String serviceDescriptor = BaseGmsClient.this.getServiceDescriptor();
                    Log.e("GmsClient", new StringBuilder(String.valueOf(serviceDescriptor).length() + 34 + String.valueOf(interfaceDescriptor).length()).append("service descriptor mismatch: ").append(serviceDescriptor).append(" vs. ").append(interfaceDescriptor).toString());
                    return false;
                }
                IInterface createServiceInterface = BaseGmsClient.this.createServiceInterface(this.zzcy);
                if (createServiceInterface == null) {
                    return false;
                }
                if (!BaseGmsClient.this.zza(2, 4, createServiceInterface) && !BaseGmsClient.this.zza(3, 4, createServiceInterface)) {
                    return false;
                }
                BaseGmsClient.this.zzco = null;
                Bundle connectionHint = BaseGmsClient.this.getConnectionHint();
                if (BaseGmsClient.this.zzck != null) {
                    BaseGmsClient.this.zzck.onConnected(connectionHint);
                }
                return true;
            } catch (RemoteException e) {
                Log.w("GmsClient", "service probably died");
                return false;
            }
        }
    }

    protected final class zzg extends zza {
        @BinderThread
        public zzg(int i, @Nullable Bundle bundle) {
            super(i, null);
        }

        /* access modifiers changed from: protected */
        public final void zza(ConnectionResult connectionResult) {
            if (!BaseGmsClient.this.enableLocalFallback() || !BaseGmsClient.this.zzl()) {
                BaseGmsClient.this.zzcf.onReportServiceBinding(connectionResult);
                BaseGmsClient.this.onConnectionFailed(connectionResult);
                return;
            }
            BaseGmsClient.this.zzb(16);
        }

        /* access modifiers changed from: protected */
        public final boolean zzm() {
            BaseGmsClient.this.zzcf.onReportServiceBinding(ConnectionResult.RESULT_SUCCESS);
            return true;
        }
    }

    @KeepForSdk
    @VisibleForTesting
    protected BaseGmsClient(Context context, Handler handler, GmsClientSupervisor gmsClientSupervisor, GoogleApiAvailabilityLight googleApiAvailabilityLight, int i, BaseConnectionCallbacks baseConnectionCallbacks, BaseOnConnectionFailedListener baseOnConnectionFailedListener) {
        this.mLock = new Object();
        this.zzcd = new Object();
        this.zzch = new ArrayList<>();
        this.zzcj = 1;
        this.zzco = null;
        this.zzcp = false;
        this.zzcq = null;
        this.zzcr = new AtomicInteger(0);
        this.mContext = (Context) Preconditions.checkNotNull(context, "Context must not be null");
        this.mHandler = (Handler) Preconditions.checkNotNull(handler, "Handler must not be null");
        this.zzca = handler.getLooper();
        this.zzcb = (GmsClientSupervisor) Preconditions.checkNotNull(gmsClientSupervisor, "Supervisor must not be null");
        this.zzcc = (GoogleApiAvailabilityLight) Preconditions.checkNotNull(googleApiAvailabilityLight, "API availability must not be null");
        this.zzcm = i;
        this.zzck = baseConnectionCallbacks;
        this.zzcl = baseOnConnectionFailedListener;
        this.zzcn = null;
    }

    @KeepForSdk
    protected BaseGmsClient(Context context, Looper looper, int i, BaseConnectionCallbacks baseConnectionCallbacks, BaseOnConnectionFailedListener baseOnConnectionFailedListener, String str) {
        this(context, looper, GmsClientSupervisor.getInstance(context), GoogleApiAvailabilityLight.getInstance(), i, (BaseConnectionCallbacks) Preconditions.checkNotNull(baseConnectionCallbacks), (BaseOnConnectionFailedListener) Preconditions.checkNotNull(baseOnConnectionFailedListener), str);
    }

    @KeepForSdk
    @VisibleForTesting
    protected BaseGmsClient(Context context, Looper looper, GmsClientSupervisor gmsClientSupervisor, GoogleApiAvailabilityLight googleApiAvailabilityLight, int i, BaseConnectionCallbacks baseConnectionCallbacks, BaseOnConnectionFailedListener baseOnConnectionFailedListener, String str) {
        this.mLock = new Object();
        this.zzcd = new Object();
        this.zzch = new ArrayList<>();
        this.zzcj = 1;
        this.zzco = null;
        this.zzcp = false;
        this.zzcq = null;
        this.zzcr = new AtomicInteger(0);
        this.mContext = (Context) Preconditions.checkNotNull(context, "Context must not be null");
        this.zzca = (Looper) Preconditions.checkNotNull(looper, "Looper must not be null");
        this.zzcb = (GmsClientSupervisor) Preconditions.checkNotNull(gmsClientSupervisor, "Supervisor must not be null");
        this.zzcc = (GoogleApiAvailabilityLight) Preconditions.checkNotNull(googleApiAvailabilityLight, "API availability must not be null");
        this.mHandler = new zzb(looper);
        this.zzcm = i;
        this.zzck = baseConnectionCallbacks;
        this.zzcl = baseOnConnectionFailedListener;
        this.zzcn = str;
    }

    /* access modifiers changed from: private */
    public final void zza(int i, T t) {
        boolean z = false;
        if ((i == 4) == (t != null)) {
            z = true;
        }
        Preconditions.checkArgument(z);
        synchronized (this.mLock) {
            this.zzcj = i;
            this.zzcg = t;
            onSetConnectState(i, t);
            switch (i) {
                case 1:
                    if (this.zzci != null) {
                        this.zzcb.zza(this.zzbz.zzt(), this.zzbz.getPackageName(), this.zzbz.zzq(), this.zzci, zzj());
                        this.zzci = null;
                        break;
                    }
                    break;
                case 2:
                case 3:
                    if (!(this.zzci == null || this.zzbz == null)) {
                        String zzt = this.zzbz.zzt();
                        String packageName = this.zzbz.getPackageName();
                        Log.e("GmsClient", new StringBuilder(String.valueOf(zzt).length() + 70 + String.valueOf(packageName).length()).append("Calling connect() while still connected, missing disconnect() for ").append(zzt).append(" on ").append(packageName).toString());
                        this.zzcb.zza(this.zzbz.zzt(), this.zzbz.getPackageName(), this.zzbz.zzq(), this.zzci, zzj());
                        this.zzcr.incrementAndGet();
                    }
                    this.zzci = new zze(this.zzcr.get());
                    this.zzbz = (this.zzcj != 3 || getLocalStartServiceAction() == null) ? new zzh(getStartServicePackage(), getStartServiceAction(), false, 129) : new zzh(getContext().getPackageName(), getLocalStartServiceAction(), true, 129);
                    if (!this.zzcb.zza(new zza(this.zzbz.zzt(), this.zzbz.getPackageName(), this.zzbz.zzq()), this.zzci, zzj())) {
                        String zzt2 = this.zzbz.zzt();
                        String packageName2 = this.zzbz.getPackageName();
                        Log.e("GmsClient", new StringBuilder(String.valueOf(zzt2).length() + 34 + String.valueOf(packageName2).length()).append("unable to connect to service: ").append(zzt2).append(" on ").append(packageName2).toString());
                        zza(16, (Bundle) null, this.zzcr.get());
                        break;
                    }
                    break;
                case 4:
                    onConnectedLocked(t);
                    break;
            }
        }
    }

    /* access modifiers changed from: private */
    public final void zza(zzb zzb2) {
        this.zzcq = zzb2;
    }

    /* access modifiers changed from: private */
    public final boolean zza(int i, int i2, T t) {
        boolean z;
        synchronized (this.mLock) {
            if (this.zzcj != i) {
                z = false;
            } else {
                zza(i2, t);
                z = true;
            }
        }
        return z;
    }

    /* access modifiers changed from: private */
    public final void zzb(int i) {
        int i2;
        if (zzk()) {
            i2 = 5;
            this.zzcp = true;
        } else {
            i2 = 4;
        }
        this.mHandler.sendMessage(this.mHandler.obtainMessage(i2, this.zzcr.get(), 16));
    }

    @Nullable
    private final String zzj() {
        return this.zzcn == null ? this.mContext.getClass().getName() : this.zzcn;
    }

    private final boolean zzk() {
        boolean z;
        synchronized (this.mLock) {
            z = this.zzcj == 3;
        }
        return z;
    }

    /* access modifiers changed from: private */
    public final boolean zzl() {
        if (this.zzcp || TextUtils.isEmpty(getServiceDescriptor()) || TextUtils.isEmpty(getLocalStartServiceAction())) {
            return false;
        }
        try {
            Class.forName(getServiceDescriptor());
            return true;
        } catch (ClassNotFoundException e) {
            return false;
        }
    }

    @KeepForSdk
    public void checkAvailabilityAndConnect() {
        int isGooglePlayServicesAvailable = this.zzcc.isGooglePlayServicesAvailable(this.mContext, getMinApkVersion());
        if (isGooglePlayServicesAvailable != 0) {
            zza(1, (T) null);
            triggerNotAvailable(new LegacyClientCallbackAdapter(), isGooglePlayServicesAvailable, null);
            return;
        }
        connect(new LegacyClientCallbackAdapter());
    }

    /* access modifiers changed from: protected */
    @KeepForSdk
    public final void checkConnected() {
        if (!isConnected()) {
            throw new IllegalStateException("Not connected. Call connect() and wait for onConnected() to be called.");
        }
    }

    @KeepForSdk
    public void connect(@NonNull ConnectionProgressReportCallbacks connectionProgressReportCallbacks) {
        this.zzcf = (ConnectionProgressReportCallbacks) Preconditions.checkNotNull(connectionProgressReportCallbacks, "Connection progress callbacks cannot be null.");
        zza(2, (T) null);
    }

    /* access modifiers changed from: protected */
    @Nullable
    @KeepForSdk
    public abstract T createServiceInterface(IBinder iBinder);

    @KeepForSdk
    public void disconnect() {
        this.zzcr.incrementAndGet();
        synchronized (this.zzch) {
            int size = this.zzch.size();
            for (int i = 0; i < size; i++) {
                ((zzc) this.zzch.get(i)).removeListener();
            }
            this.zzch.clear();
        }
        synchronized (this.zzcd) {
            this.zzce = null;
        }
        zza(1, (T) null);
    }

    @KeepForSdk
    public void dump(String str, FileDescriptor fileDescriptor, PrintWriter printWriter, String[] strArr) {
        int i;
        T t;
        IGmsServiceBroker iGmsServiceBroker;
        synchronized (this.mLock) {
            i = this.zzcj;
            t = this.zzcg;
        }
        synchronized (this.zzcd) {
            iGmsServiceBroker = this.zzce;
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
        if (t == null) {
            printWriter.append("null");
        } else {
            printWriter.append(getServiceDescriptor()).append("@").append(Integer.toHexString(System.identityHashCode(t.asBinder())));
        }
        printWriter.append(" mServiceBroker=");
        if (iGmsServiceBroker == null) {
            printWriter.println("null");
        } else {
            printWriter.append("IGmsServiceBroker@").println(Integer.toHexString(System.identityHashCode(iGmsServiceBroker.asBinder())));
        }
        SimpleDateFormat simpleDateFormat = new SimpleDateFormat("yyyy-MM-dd HH:mm:ss.SSS", Locale.US);
        if (this.zzbw > 0) {
            PrintWriter append = printWriter.append(str).append("lastConnectedTime=");
            long j = this.zzbw;
            String format = simpleDateFormat.format(new Date(this.zzbw));
            append.println(new StringBuilder(String.valueOf(format).length() + 21).append(j).append(" ").append(format).toString());
        }
        if (this.zzbv > 0) {
            printWriter.append(str).append("lastSuspendedCause=");
            switch (this.zzbu) {
                case 1:
                    printWriter.append("CAUSE_SERVICE_DISCONNECTED");
                    break;
                case 2:
                    printWriter.append("CAUSE_NETWORK_LOST");
                    break;
                default:
                    printWriter.append(String.valueOf(this.zzbu));
                    break;
            }
            PrintWriter append2 = printWriter.append(" lastSuspendedTime=");
            long j2 = this.zzbv;
            String format2 = simpleDateFormat.format(new Date(this.zzbv));
            append2.println(new StringBuilder(String.valueOf(format2).length() + 21).append(j2).append(" ").append(format2).toString());
        }
        if (this.zzby > 0) {
            printWriter.append(str).append("lastFailedStatus=").append(CommonStatusCodes.getStatusCodeString(this.zzbx));
            PrintWriter append3 = printWriter.append(" lastFailedTime=");
            long j3 = this.zzby;
            String format3 = simpleDateFormat.format(new Date(this.zzby));
            append3.println(new StringBuilder(String.valueOf(format3).length() + 21).append(j3).append(" ").append(format3).toString());
        }
    }

    /* access modifiers changed from: protected */
    @KeepForSdk
    public boolean enableLocalFallback() {
        return false;
    }

    @KeepForSdk
    public Account getAccount() {
        return null;
    }

    @KeepForSdk
    public Feature[] getApiFeatures() {
        return zzbt;
    }

    @Nullable
    @KeepForSdk
    public final Feature[] getAvailableFeatures() {
        zzb zzb2 = this.zzcq;
        if (zzb2 == null) {
            return null;
        }
        return zzb2.zzdb;
    }

    @KeepForSdk
    public Bundle getConnectionHint() {
        return null;
    }

    @KeepForSdk
    public final Context getContext() {
        return this.mContext;
    }

    @KeepForSdk
    public String getEndpointPackageName() {
        if (isConnected() && this.zzbz != null) {
            return this.zzbz.getPackageName();
        }
        throw new RuntimeException("Failed to connect when checking package");
    }

    /* access modifiers changed from: protected */
    @KeepForSdk
    public Bundle getGetServiceRequestExtraArgs() {
        return new Bundle();
    }

    /* access modifiers changed from: protected */
    @Nullable
    @KeepForSdk
    public String getLocalStartServiceAction() {
        return null;
    }

    @KeepForSdk
    public final Looper getLooper() {
        return this.zzca;
    }

    @KeepForSdk
    public int getMinApkVersion() {
        return GoogleApiAvailabilityLight.GOOGLE_PLAY_SERVICES_VERSION_CODE;
    }

    @WorkerThread
    @KeepForSdk
    public void getRemoteService(IAccountAccessor iAccountAccessor, Set<Scope> set) {
        Bundle getServiceRequestExtraArgs = getGetServiceRequestExtraArgs();
        GetServiceRequest getServiceRequest = new GetServiceRequest(this.zzcm);
        getServiceRequest.zzy = this.mContext.getPackageName();
        getServiceRequest.zzdk = getServiceRequestExtraArgs;
        if (set != null) {
            getServiceRequest.zzdj = (Scope[]) set.toArray(new Scope[set.size()]);
        }
        if (requiresSignIn()) {
            getServiceRequest.zzdl = getAccount() != null ? getAccount() : new Account("<<default account>>", "com.google");
            if (iAccountAccessor != null) {
                getServiceRequest.zzdi = iAccountAccessor.asBinder();
            }
        } else if (requiresAccount()) {
            getServiceRequest.zzdl = getAccount();
        }
        getServiceRequest.zzdm = zzbt;
        getServiceRequest.zzdn = getApiFeatures();
        try {
            synchronized (this.zzcd) {
                if (this.zzce != null) {
                    this.zzce.getService(new zzd(this, this.zzcr.get()), getServiceRequest);
                } else {
                    Log.w("GmsClient", "mServiceBroker is null, client disconnected");
                }
            }
        } catch (DeadObjectException e) {
            Log.w("GmsClient", "IGmsServiceBroker.getService failed", e);
            triggerConnectionSuspended(1);
        } catch (SecurityException e2) {
            throw e2;
        } catch (RemoteException | RuntimeException e3) {
            Log.w("GmsClient", "IGmsServiceBroker.getService failed", e3);
            onPostInitHandler(8, null, null, this.zzcr.get());
        }
    }

    /* access modifiers changed from: protected */
    @KeepForSdk
    public Set<Scope> getScopes() {
        return Collections.EMPTY_SET;
    }

    @KeepForSdk
    public final T getService() throws DeadObjectException {
        T t;
        synchronized (this.mLock) {
            if (this.zzcj == 5) {
                throw new DeadObjectException();
            }
            checkConnected();
            Preconditions.checkState(this.zzcg != null, "Client is connected but service is null");
            t = this.zzcg;
        }
        return t;
    }

    @Nullable
    @KeepForSdk
    public IBinder getServiceBrokerBinder() {
        IBinder asBinder;
        synchronized (this.zzcd) {
            asBinder = this.zzce == null ? null : this.zzce.asBinder();
        }
        return asBinder;
    }

    /* access modifiers changed from: protected */
    @KeepForSdk
    @NonNull
    public abstract String getServiceDescriptor();

    @KeepForSdk
    public Intent getSignInIntent() {
        throw new UnsupportedOperationException("Not a sign in API");
    }

    /* access modifiers changed from: protected */
    @KeepForSdk
    @NonNull
    public abstract String getStartServiceAction();

    /* access modifiers changed from: protected */
    @KeepForSdk
    public String getStartServicePackage() {
        return "com.google.android.gms";
    }

    @KeepForSdk
    public boolean isConnected() {
        boolean z;
        synchronized (this.mLock) {
            z = this.zzcj == 4;
        }
        return z;
    }

    @KeepForSdk
    public boolean isConnecting() {
        boolean z;
        synchronized (this.mLock) {
            z = this.zzcj == 2 || this.zzcj == 3;
        }
        return z;
    }

    /* access modifiers changed from: protected */
    @KeepForSdk
    @CallSuper
    public void onConnectedLocked(@NonNull T t) {
        this.zzbw = System.currentTimeMillis();
    }

    /* access modifiers changed from: protected */
    @KeepForSdk
    @CallSuper
    public void onConnectionFailed(ConnectionResult connectionResult) {
        this.zzbx = connectionResult.getErrorCode();
        this.zzby = System.currentTimeMillis();
    }

    /* access modifiers changed from: protected */
    @KeepForSdk
    @CallSuper
    public void onConnectionSuspended(int i) {
        this.zzbu = i;
        this.zzbv = System.currentTimeMillis();
    }

    /* access modifiers changed from: protected */
    @KeepForSdk
    public void onPostInitHandler(int i, IBinder iBinder, Bundle bundle, int i2) {
        this.mHandler.sendMessage(this.mHandler.obtainMessage(1, i2, -1, new zzf(i, iBinder, bundle)));
    }

    /* access modifiers changed from: 0000 */
    @KeepForSdk
    public void onSetConnectState(int i, T t) {
    }

    @KeepForSdk
    public void onUserSignOut(@NonNull SignOutCallbacks signOutCallbacks) {
        signOutCallbacks.onSignOutComplete();
    }

    @KeepForSdk
    public boolean providesSignIn() {
        return false;
    }

    @KeepForSdk
    public boolean requiresAccount() {
        return false;
    }

    @KeepForSdk
    public boolean requiresGooglePlayServices() {
        return true;
    }

    @KeepForSdk
    public boolean requiresSignIn() {
        return false;
    }

    @KeepForSdk
    public void triggerConnectionSuspended(int i) {
        this.mHandler.sendMessage(this.mHandler.obtainMessage(6, this.zzcr.get(), i));
    }

    /* access modifiers changed from: protected */
    @KeepForSdk
    @VisibleForTesting
    public void triggerNotAvailable(@NonNull ConnectionProgressReportCallbacks connectionProgressReportCallbacks, int i, @Nullable PendingIntent pendingIntent) {
        this.zzcf = (ConnectionProgressReportCallbacks) Preconditions.checkNotNull(connectionProgressReportCallbacks, "Connection progress callbacks cannot be null.");
        this.mHandler.sendMessage(this.mHandler.obtainMessage(3, this.zzcr.get(), i, pendingIntent));
    }

    /* access modifiers changed from: protected */
    public final void zza(int i, @Nullable Bundle bundle, int i2) {
        this.mHandler.sendMessage(this.mHandler.obtainMessage(7, i2, -1, new zzg(i, null)));
    }
}
