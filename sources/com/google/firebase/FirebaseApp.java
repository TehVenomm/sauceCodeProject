package com.google.firebase;

import android.annotation.TargetApi;
import android.app.Application;
import android.content.BroadcastReceiver;
import android.content.Context;
import android.content.Intent;
import android.content.IntentFilter;
import android.support.annotation.NonNull;
import android.support.annotation.Nullable;
import android.support.annotation.UiThread;
import android.support.v4.content.ContextCompat;
import android.support.v4.util.ArrayMap;
import android.support.v4.util.ArraySet;
import android.text.TextUtils;
import android.util.Log;
import com.google.android.gms.common.api.internal.zzk;
import com.google.android.gms.common.internal.zzbf;
import com.google.android.gms.common.internal.zzbp;
import com.google.android.gms.common.util.zzq;
import com.google.android.gms.internal.zzeaa;
import com.google.android.gms.internal.zzeab;
import com.google.android.gms.internal.zzeac;
import com.google.android.gms.internal.zzead;
import com.google.android.gms.tasks.Task;
import com.google.android.gms.tasks.Tasks;
import com.google.firebase.auth.GetTokenResult;
import java.lang.reflect.Method;
import java.lang.reflect.Modifier;
import java.util.ArrayList;
import java.util.Arrays;
import java.util.Collection;
import java.util.Collections;
import java.util.List;
import java.util.Map;
import java.util.Set;
import java.util.concurrent.CopyOnWriteArrayList;
import java.util.concurrent.atomic.AtomicBoolean;
import java.util.concurrent.atomic.AtomicReference;

public class FirebaseApp {
    public static final String DEFAULT_APP_NAME = "[DEFAULT]";
    private static final Object zzaqm = new Object();
    static final Map<String, FirebaseApp> zzhtf = new ArrayMap();
    private static final List<String> zzlea = Arrays.asList(new String[]{"com.google.firebase.auth.FirebaseAuth", "com.google.firebase.iid.FirebaseInstanceId"});
    private static final List<String> zzleb = Collections.singletonList("com.google.firebase.crash.FirebaseCrash");
    private static final List<String> zzlec = Arrays.asList(new String[]{"com.google.android.gms.measurement.AppMeasurement"});
    private static final List<String> zzled = Arrays.asList(new String[0]);
    private static final Set<String> zzlee = Collections.emptySet();
    private final Context mApplicationContext;
    private final String mName;
    private final FirebaseOptions zzlef;
    private final AtomicBoolean zzleg = new AtomicBoolean(false);
    private final AtomicBoolean zzleh = new AtomicBoolean();
    private final List<zzb> zzlei = new CopyOnWriteArrayList();
    private final List<zza> zzlej = new CopyOnWriteArrayList();
    private final List<Object> zzlek = new CopyOnWriteArrayList();
    private zzeac zzlel;
    private zzc zzlem;

    public interface zzc {
    }

    public interface zza {
        void zzbe(boolean z);
    }

    public interface zzb {
        void zzb(@NonNull zzead zzead);
    }

    @TargetApi(24)
    static final class zzd extends BroadcastReceiver {
        private static AtomicReference<zzd> zzlen = new AtomicReference();
        private final Context mApplicationContext;

        private zzd(Context context) {
            this.mApplicationContext = context;
        }

        private static void zzee(Context context) {
            if (zzlen.get() == null) {
                BroadcastReceiver zzd = new zzd(context);
                if (zzlen.compareAndSet(null, zzd)) {
                    context.registerReceiver(zzd, new IntentFilter("android.intent.action.USER_UNLOCKED"));
                }
            }
        }

        public final void onReceive(Context context, Intent intent) {
            synchronized (FirebaseApp.zzaqm) {
                for (FirebaseApp zza : FirebaseApp.zzhtf.values()) {
                    zza.zzbnn();
                }
            }
            this.mApplicationContext.unregisterReceiver(this);
        }
    }

    private FirebaseApp(Context context, String str, FirebaseOptions firebaseOptions) {
        this.mApplicationContext = (Context) zzbp.zzu(context);
        this.mName = zzbp.zzgf(str);
        this.zzlef = (FirebaseOptions) zzbp.zzu(firebaseOptions);
        this.zzlem = new zzeaa();
    }

    public static List<FirebaseApp> getApps(Context context) {
        List<FirebaseApp> arrayList;
        zzeab.zzeo(context);
        synchronized (zzaqm) {
            arrayList = new ArrayList(zzhtf.values());
            zzeab.zzbyr();
            Set<String> zzbys = zzeab.zzbys();
            zzbys.removeAll(zzhtf.keySet());
            for (String str : zzbys) {
                zzeab.zzqb(str);
                arrayList.add(initializeApp(context, null, str));
            }
        }
        return arrayList;
    }

    @Nullable
    public static FirebaseApp getInstance() {
        FirebaseApp firebaseApp;
        synchronized (zzaqm) {
            firebaseApp = (FirebaseApp) zzhtf.get(DEFAULT_APP_NAME);
            if (firebaseApp == null) {
                String zzalk = zzq.zzalk();
                throw new IllegalStateException(new StringBuilder(String.valueOf(zzalk).length() + 116).append("Default FirebaseApp is not initialized in this process ").append(zzalk).append(". Make sure to call FirebaseApp.initializeApp(Context) first.").toString());
            }
        }
        return firebaseApp;
    }

    public static FirebaseApp getInstance(@NonNull String str) {
        FirebaseApp firebaseApp;
        synchronized (zzaqm) {
            firebaseApp = (FirebaseApp) zzhtf.get(str.trim());
            if (firebaseApp != null) {
            } else {
                String str2;
                Iterable zzbnm = zzbnm();
                if (zzbnm.isEmpty()) {
                    str2 = "";
                } else {
                    str2 = String.valueOf(TextUtils.join(", ", zzbnm));
                    str2 = str2.length() != 0 ? "Available app names: ".concat(str2) : new String("Available app names: ");
                }
                throw new IllegalStateException(String.format("FirebaseApp with name %s doesn't exist. %s", new Object[]{str, str2}));
            }
        }
        return firebaseApp;
    }

    @Nullable
    public static FirebaseApp initializeApp(Context context) {
        FirebaseApp instance;
        synchronized (zzaqm) {
            if (zzhtf.containsKey(DEFAULT_APP_NAME)) {
                instance = getInstance();
            } else {
                FirebaseOptions fromResource = FirebaseOptions.fromResource(context);
                if (fromResource == null) {
                    instance = null;
                } else {
                    instance = initializeApp(context, fromResource);
                }
            }
        }
        return instance;
    }

    public static FirebaseApp initializeApp(Context context, FirebaseOptions firebaseOptions) {
        return initializeApp(context, firebaseOptions, DEFAULT_APP_NAME);
    }

    public static FirebaseApp initializeApp(Context context, FirebaseOptions firebaseOptions, String str) {
        FirebaseApp firebaseApp;
        zzeab.zzeo(context);
        if (context.getApplicationContext() instanceof Application) {
            zzk.zza((Application) context.getApplicationContext());
            zzk.zzafy().zza(new zza());
        }
        String trim = str.trim();
        if (context.getApplicationContext() != null) {
            Object applicationContext = context.getApplicationContext();
        }
        synchronized (zzaqm) {
            zzbp.zza(!zzhtf.containsKey(trim), new StringBuilder(String.valueOf(trim).length() + 33).append("FirebaseApp name ").append(trim).append(" already exists!").toString());
            zzbp.zzb(applicationContext, (Object) "Application context cannot be null.");
            firebaseApp = new FirebaseApp(applicationContext, trim, firebaseOptions);
            zzhtf.put(trim, firebaseApp);
        }
        zzeab.zze(firebaseApp);
        firebaseApp.zza(FirebaseApp.class, firebaseApp, zzlea);
        if (firebaseApp.zzbnk()) {
            firebaseApp.zza(FirebaseApp.class, firebaseApp, zzleb);
            firebaseApp.zza(Context.class, firebaseApp.getApplicationContext(), zzlec);
        }
        return firebaseApp;
    }

    private final <T> void zza(Class<T> cls, T t, Iterable<String> iterable) {
        String valueOf;
        boolean isDeviceProtectedStorage = ContextCompat.isDeviceProtectedStorage(this.mApplicationContext);
        if (isDeviceProtectedStorage) {
            zzd.zzee(this.mApplicationContext);
        }
        for (String valueOf2 : iterable) {
            if (isDeviceProtectedStorage) {
                try {
                    if (!zzled.contains(valueOf2)) {
                    }
                } catch (ClassNotFoundException e) {
                    if (zzlee.contains(valueOf2)) {
                        throw new IllegalStateException(String.valueOf(valueOf2).concat(" is missing, but is required. Check if it has been removed by Proguard."));
                    }
                    Log.d("FirebaseApp", String.valueOf(valueOf2).concat(" is not linked. Skipping initialization."));
                } catch (NoSuchMethodException e2) {
                    throw new IllegalStateException(String.valueOf(valueOf2).concat("#getInstance has been removed by Proguard. Add keep rule to prevent it."));
                } catch (Throwable e3) {
                    Log.wtf("FirebaseApp", "Firebase API initialization failure.", e3);
                } catch (Throwable e4) {
                    valueOf2 = String.valueOf(valueOf2);
                    Log.wtf("FirebaseApp", valueOf2.length() != 0 ? "Failed to initialize ".concat(valueOf2) : new String("Failed to initialize "), e4);
                }
            }
            Method method = Class.forName(valueOf2).getMethod("getInstance", new Class[]{cls});
            int modifiers = method.getModifiers();
            if (Modifier.isPublic(modifiers) && Modifier.isStatic(modifiers)) {
                method.invoke(null, new Object[]{t});
            }
        }
    }

    public static void zzbe(boolean z) {
        synchronized (zzaqm) {
            ArrayList arrayList = new ArrayList(zzhtf.values());
            int size = arrayList.size();
            int i = 0;
            while (i < size) {
                Object obj = arrayList.get(i);
                i++;
                FirebaseApp firebaseApp = (FirebaseApp) obj;
                if (firebaseApp.zzleg.get()) {
                    firebaseApp.zzbx(z);
                }
            }
        }
    }

    private final void zzbnj() {
        zzbp.zza(!this.zzleh.get(), (Object) "FirebaseApp was deleted");
    }

    private static List<String> zzbnm() {
        Collection arraySet = new ArraySet();
        synchronized (zzaqm) {
            for (FirebaseApp name : zzhtf.values()) {
                arraySet.add(name.getName());
            }
            if (zzeab.zzbyr() != null) {
                arraySet.addAll(zzeab.zzbys());
            }
        }
        List<String> arrayList = new ArrayList(arraySet);
        Collections.sort(arrayList);
        return arrayList;
    }

    private final void zzbnn() {
        zza(FirebaseApp.class, this, zzlea);
        if (zzbnk()) {
            zza(FirebaseApp.class, this, zzleb);
            zza(Context.class, this.mApplicationContext, zzlec);
        }
    }

    private final void zzbx(boolean z) {
        Log.d("FirebaseApp", "Notifying background state change listeners.");
        for (zza zzbe : this.zzlej) {
            zzbe.zzbe(z);
        }
    }

    public boolean equals(Object obj) {
        return !(obj instanceof FirebaseApp) ? false : this.mName.equals(((FirebaseApp) obj).getName());
    }

    @NonNull
    public Context getApplicationContext() {
        zzbnj();
        return this.mApplicationContext;
    }

    @NonNull
    public String getName() {
        zzbnj();
        return this.mName;
    }

    @NonNull
    public FirebaseOptions getOptions() {
        zzbnj();
        return this.zzlef;
    }

    public final Task<GetTokenResult> getToken(boolean z) {
        zzbnj();
        return this.zzlel == null ? Tasks.forException(new FirebaseApiNotAvailableException("firebase-auth is not linked, please fall back to unauthenticated mode.")) : this.zzlel.zzby(z);
    }

    public int hashCode() {
        return this.mName.hashCode();
    }

    public void setAutomaticResourceManagementEnabled(boolean z) {
        zzbnj();
        if (this.zzleg.compareAndSet(!z, z)) {
            boolean zzafz = zzk.zzafy().zzafz();
            if (z && zzafz) {
                zzbx(true);
            } else if (!z && zzafz) {
                zzbx(false);
            }
        }
    }

    public String toString() {
        return zzbf.zzt(this).zzg("name", this.mName).zzg("options", this.zzlef).toString();
    }

    public final void zza(@NonNull zzeac zzeac) {
        this.zzlel = (zzeac) zzbp.zzu(zzeac);
    }

    @UiThread
    public final void zza(@NonNull zzead zzead) {
        Log.d("FirebaseApp", "Notifying auth state listeners.");
        int i = 0;
        for (zzb zzb : this.zzlei) {
            zzb.zzb(zzead);
            i++;
        }
        Log.d("FirebaseApp", String.format("Notified %d auth state listeners.", new Object[]{Integer.valueOf(i)}));
    }

    public final void zza(zza zza) {
        zzbnj();
        if (this.zzleg.get() && zzk.zzafy().zzafz()) {
            zza.zzbe(true);
        }
        this.zzlej.add(zza);
    }

    public final void zza(@NonNull zzb zzb) {
        zzbnj();
        zzbp.zzu(zzb);
        this.zzlei.add(zzb);
        this.zzlei.size();
    }

    public final boolean zzbnk() {
        return DEFAULT_APP_NAME.equals(getName());
    }

    public final String zzbnl() {
        String zzk = com.google.android.gms.common.util.zzb.zzk(getName().getBytes());
        String zzk2 = com.google.android.gms.common.util.zzb.zzk(getOptions().getApplicationId().getBytes());
        return new StringBuilder((String.valueOf(zzk).length() + 1) + String.valueOf(zzk2).length()).append(zzk).append("+").append(zzk2).toString();
    }
}
