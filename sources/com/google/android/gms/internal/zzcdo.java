package com.google.android.gms.internal;

import android.content.Context;
import android.os.Bundle;
import android.os.Parcelable;
import android.support.annotation.Nullable;
import android.support.annotation.WorkerThread;
import android.support.v4.util.ArrayMap;
import android.text.TextUtils;
import com.google.android.gms.common.internal.zzbp;
import com.google.android.gms.common.util.zzd;
import com.google.android.gms.measurement.AppMeasurement.ConditionalUserProperty;
import com.google.android.gms.measurement.AppMeasurement.Event;
import com.google.android.gms.measurement.AppMeasurement.EventInterceptor;
import com.google.android.gms.measurement.AppMeasurement.OnEventListener;
import com.google.android.gms.measurement.AppMeasurement.zzb;
import com.google.android.gms.tasks.Task;
import com.google.android.gms.tasks.Tasks;
import java.util.ArrayList;
import java.util.Arrays;
import java.util.Collections;
import java.util.List;
import java.util.Map;
import java.util.Set;
import java.util.concurrent.CopyOnWriteArraySet;
import java.util.concurrent.atomic.AtomicReference;

public final class zzcdo extends zzcdm {
    protected zzceb zziuk;
    private EventInterceptor zziul;
    private final Set<OnEventListener> zzium = new CopyOnWriteArraySet();
    private boolean zziun;
    private final AtomicReference<String> zziuo = new AtomicReference();

    protected zzcdo(zzcco zzcco) {
        super(zzcco);
    }

    public static int getMaxUserProperties(String str) {
        zzbp.zzgf(str);
        return zzcap.zzavz();
    }

    private final void zza(ConditionalUserProperty conditionalUserProperty) {
        long currentTimeMillis = zzvu().currentTimeMillis();
        zzbp.zzu(conditionalUserProperty);
        zzbp.zzgf(conditionalUserProperty.mName);
        zzbp.zzgf(conditionalUserProperty.mOrigin);
        zzbp.zzu(conditionalUserProperty.mValue);
        conditionalUserProperty.mCreationTimestamp = currentTimeMillis;
        String str = conditionalUserProperty.mName;
        Object obj = conditionalUserProperty.mValue;
        if (zzaug().zzjy(str) != 0) {
            zzauk().zzayc().zzj("Invalid conditional user property name", zzauf().zzje(str));
        } else if (zzaug().zzl(str, obj) != 0) {
            zzauk().zzayc().zze("Invalid conditional user property value", zzauf().zzje(str), obj);
        } else {
            Object zzm = zzaug().zzm(str, obj);
            if (zzm == null) {
                zzauk().zzayc().zze("Unable to normalize conditional user property value", zzauf().zzje(str), obj);
                return;
            }
            conditionalUserProperty.mValue = zzm;
            long j = conditionalUserProperty.mTriggerTimeout;
            if (TextUtils.isEmpty(conditionalUserProperty.mTriggerEventName) || (j <= zzcap.zzawb() && j >= 1)) {
                j = conditionalUserProperty.mTimeToLive;
                if (j > zzcap.zzawc() || j < 1) {
                    zzauk().zzayc().zze("Invalid conditional user property time to live", zzauf().zzje(str), Long.valueOf(j));
                    return;
                } else {
                    zzauj().zzg(new zzcdq(this, conditionalUserProperty));
                    return;
                }
            }
            zzauk().zzayc().zze("Invalid conditional user property timeout", zzauf().zzje(str), Long.valueOf(j));
        }
    }

    private final void zza(String str, String str2, long j, Bundle bundle, boolean z, boolean z2, boolean z3, String str3) {
        Bundle bundle2;
        if (bundle == null) {
            bundle2 = new Bundle();
        } else {
            bundle2 = new Bundle(bundle);
            for (String str4 : bundle2.keySet()) {
                Object obj = bundle2.get(str4);
                if (obj instanceof Bundle) {
                    bundle2.putBundle(str4, new Bundle((Bundle) obj));
                } else if (obj instanceof Parcelable[]) {
                    Parcelable[] parcelableArr = (Parcelable[]) obj;
                    for (r4 = 0; r4 < parcelableArr.length; r4++) {
                        if (parcelableArr[r4] instanceof Bundle) {
                            parcelableArr[r4] = new Bundle((Bundle) parcelableArr[r4]);
                        }
                    }
                } else if (obj instanceof ArrayList) {
                    ArrayList arrayList = (ArrayList) obj;
                    for (r4 = 0; r4 < arrayList.size(); r4++) {
                        Object obj2 = arrayList.get(r4);
                        if (obj2 instanceof Bundle) {
                            arrayList.set(r4, new Bundle((Bundle) obj2));
                        }
                    }
                }
            }
        }
        zzauj().zzg(new zzcdw(this, str, str2, j, bundle2, z, z2, z3, str3));
    }

    private final void zza(String str, String str2, long j, Object obj) {
        zzauj().zzg(new zzcdx(this, str, str2, obj, j));
    }

    private final void zza(String str, String str2, Bundle bundle, boolean z, boolean z2, boolean z3, String str3) {
        zza(str, str2, zzvu().currentTimeMillis(), bundle, true, z2, z3, null);
    }

    @WorkerThread
    private final void zza(String str, String str2, Object obj, long j) {
        zzbp.zzgf(str);
        zzbp.zzgf(str2);
        zzug();
        zzatu();
        zzwh();
        if (!this.zzikb.isEnabled()) {
            zzauk().zzayh().log("User property not set since app measurement is disabled");
        } else if (this.zzikb.zzayu()) {
            zzauk().zzayh().zze("Setting user property (FE)", zzauf().zzjc(str2), obj);
            zzaub().zzb(new zzcfl(str2, j, obj, str));
        }
    }

    private final void zza(String str, String str2, String str3, Bundle bundle) {
        long currentTimeMillis = zzvu().currentTimeMillis();
        zzbp.zzgf(str2);
        ConditionalUserProperty conditionalUserProperty = new ConditionalUserProperty();
        conditionalUserProperty.mAppId = str;
        conditionalUserProperty.mName = str2;
        conditionalUserProperty.mCreationTimestamp = currentTimeMillis;
        if (str3 != null) {
            conditionalUserProperty.mExpiredEventName = str3;
            conditionalUserProperty.mExpiredEventParams = bundle;
        }
        zzauj().zzg(new zzcdr(this, conditionalUserProperty));
    }

    private final Map<String, Object> zzb(String str, String str2, String str3, boolean z) {
        if (zzauj().zzayr()) {
            zzauk().zzayc().log("Cannot get user properties from analytics worker thread");
            return Collections.emptyMap();
        }
        zzauj();
        if (zzccj.zzaq()) {
            zzauk().zzayc().log("Cannot get user properties from main thread");
            return Collections.emptyMap();
        }
        AtomicReference atomicReference = new AtomicReference();
        synchronized (atomicReference) {
            this.zzikb.zzauj().zzg(new zzcdt(this, atomicReference, str, str2, str3, z));
            try {
                atomicReference.wait(5000);
            } catch (InterruptedException e) {
                zzauk().zzaye().zzj("Interrupted waiting for get user properties", e);
            }
        }
        List<zzcfl> list = (List) atomicReference.get();
        if (list == null) {
            zzauk().zzaye().log("Timed out waiting for get user properties");
            return Collections.emptyMap();
        }
        Map<String, Object> arrayMap = new ArrayMap(list.size());
        for (zzcfl zzcfl : list) {
            arrayMap.put(zzcfl.name, zzcfl.getValue());
        }
        return arrayMap;
    }

    @WorkerThread
    private final void zzb(ConditionalUserProperty conditionalUserProperty) {
        zzug();
        zzwh();
        zzbp.zzu(conditionalUserProperty);
        zzbp.zzgf(conditionalUserProperty.mName);
        zzbp.zzgf(conditionalUserProperty.mOrigin);
        zzbp.zzu(conditionalUserProperty.mValue);
        if (this.zzikb.isEnabled()) {
            zzcfl zzcfl = new zzcfl(conditionalUserProperty.mName, conditionalUserProperty.mTriggeredTimestamp, conditionalUserProperty.mValue, conditionalUserProperty.mOrigin);
            try {
                zzcbc zza = zzaug().zza(conditionalUserProperty.mTriggeredEventName, conditionalUserProperty.mTriggeredEventParams, conditionalUserProperty.mOrigin, 0, true, false);
                zzaub().zzf(new zzcan(conditionalUserProperty.mAppId, conditionalUserProperty.mOrigin, zzcfl, conditionalUserProperty.mCreationTimestamp, false, conditionalUserProperty.mTriggerEventName, zzaug().zza(conditionalUserProperty.mTimedOutEventName, conditionalUserProperty.mTimedOutEventParams, conditionalUserProperty.mOrigin, 0, true, false), conditionalUserProperty.mTriggerTimeout, zza, conditionalUserProperty.mTimeToLive, zzaug().zza(conditionalUserProperty.mExpiredEventName, conditionalUserProperty.mExpiredEventParams, conditionalUserProperty.mOrigin, 0, true, false)));
                return;
            } catch (IllegalArgumentException e) {
                return;
            }
        }
        zzauk().zzayh().log("Conditional property not sent since Firebase Analytics is disabled");
    }

    @WorkerThread
    private final void zzb(String str, String str2, long j, Bundle bundle, boolean z, boolean z2, boolean z3, String str3) {
        zzbp.zzgf(str);
        zzbp.zzgf(str2);
        zzbp.zzu(bundle);
        zzug();
        zzwh();
        if (this.zzikb.isEnabled()) {
            if (!this.zziun) {
                this.zziun = true;
                try {
                    try {
                        Class.forName("com.google.android.gms.tagmanager.TagManagerService").getDeclaredMethod("initialize", new Class[]{Context.class}).invoke(null, new Object[]{getContext()});
                    } catch (Exception e) {
                        zzauk().zzaye().zzj("Failed to invoke Tag Manager's initialize() method", e);
                    }
                } catch (ClassNotFoundException e2) {
                    zzauk().zzayg().log("Tag Manager is not found and thus will not be used");
                }
            }
            boolean equals = "am".equals(str);
            boolean zzkd = zzcfo.zzkd(str2);
            if (z && this.zziul != null && !zzkd && !equals) {
                zzauk().zzayh().zze("Passing event to registered event handler (FE)", zzauf().zzjc(str2), zzauf().zzw(bundle));
                this.zziul.interceptEvent(str, str2, bundle, j);
                return;
            } else if (this.zzikb.zzayu()) {
                int zzjw = zzaug().zzjw(str2);
                if (zzjw != 0) {
                    zzaug();
                    this.zzikb.zzaug().zza(str3, zzjw, "_ev", zzcfo.zza(str2, zzcap.zzavm(), true), str2 != null ? str2.length() : 0);
                    return;
                }
                Bundle zza;
                List singletonList = Collections.singletonList("_o");
                Bundle zza2 = zzaug().zza(str2, bundle, singletonList, z3, true);
                List arrayList = new ArrayList();
                arrayList.add(zza2);
                long nextLong = zzaug().zzazx().nextLong();
                int i = 0;
                String[] strArr = (String[]) zza2.keySet().toArray(new String[bundle.size()]);
                Arrays.sort(strArr);
                int length = strArr.length;
                int i2 = 0;
                while (i2 < length) {
                    int length2;
                    String str4 = strArr[i2];
                    Object obj = zza2.get(str4);
                    zzaug();
                    Bundle[] zzac = zzcfo.zzac(obj);
                    if (zzac != null) {
                        zza2.putInt(str4, zzac.length);
                        for (int i3 = 0; i3 < zzac.length; i3++) {
                            zza = zzaug().zza("_ep", zzac[i3], singletonList, z3, false);
                            zza.putString("_en", str2);
                            zza.putLong("_eid", nextLong);
                            zza.putString("_gn", str4);
                            zza.putInt("_ll", zzac.length);
                            zza.putInt("_i", i3);
                            arrayList.add(zza);
                        }
                        length2 = zzac.length + i;
                    } else {
                        length2 = i;
                    }
                    i2++;
                    i = length2;
                }
                if (i != 0) {
                    zza2.putLong("_eid", nextLong);
                    zza2.putInt("_epc", i);
                }
                zzcap.zzawj();
                zzb zzazm = zzauc().zzazm();
                if (!(zzazm == null || zza2.containsKey("_sc"))) {
                    zzazm.zzivo = true;
                }
                int i4 = 0;
                while (i4 < arrayList.size()) {
                    zza = (Bundle) arrayList.get(i4);
                    String str5 = (i4 != 0 ? 1 : null) != null ? "_ep" : str2;
                    zza.putString("_o", str);
                    if (!zza.containsKey("_sc")) {
                        zzcec.zza(zzazm, zza);
                    }
                    Bundle zzx = z2 ? zzaug().zzx(zza) : zza;
                    zzauk().zzayh().zze("Logging event (FE)", zzauf().zzjc(str2), zzauf().zzw(zzx));
                    zzaub().zzc(new zzcbc(str5, new zzcaz(zzx), str, j), str3);
                    if (!equals) {
                        for (OnEventListener onEvent : this.zzium) {
                            onEvent.onEvent(str, str2, new Bundle(zzx), j);
                        }
                    }
                    i4++;
                }
                zzcap.zzawj();
                if (zzauc().zzazm() != null && Event.APP_EXCEPTION.equals(str2)) {
                    zzaui().zzbr(true);
                    return;
                }
                return;
            } else {
                return;
            }
        }
        zzauk().zzayh().log("Event not sent since app measurement is disabled");
    }

    @WorkerThread
    private final void zzbp(boolean z) {
        zzug();
        zzatu();
        zzwh();
        zzauk().zzayh().zzj("Setting app measurement enabled (FE)", Boolean.valueOf(z));
        zzaul().setMeasurementEnabled(z);
        zzaub().zzazo();
    }

    @WorkerThread
    private final void zzc(ConditionalUserProperty conditionalUserProperty) {
        zzug();
        zzwh();
        zzbp.zzu(conditionalUserProperty);
        zzbp.zzgf(conditionalUserProperty.mName);
        if (this.zzikb.isEnabled()) {
            zzcfl zzcfl = new zzcfl(conditionalUserProperty.mName, 0, null, null);
            try {
                zzaub().zzf(new zzcan(conditionalUserProperty.mAppId, conditionalUserProperty.mOrigin, zzcfl, conditionalUserProperty.mCreationTimestamp, conditionalUserProperty.mActive, conditionalUserProperty.mTriggerEventName, null, conditionalUserProperty.mTriggerTimeout, null, conditionalUserProperty.mTimeToLive, zzaug().zza(conditionalUserProperty.mExpiredEventName, conditionalUserProperty.mExpiredEventParams, conditionalUserProperty.mOrigin, conditionalUserProperty.mCreationTimestamp, true, false)));
                return;
            } catch (IllegalArgumentException e) {
                return;
            }
        }
        zzauk().zzayh().log("Conditional property not cleared since Firebase Analytics is disabled");
    }

    private final List<ConditionalUserProperty> zzk(String str, String str2, String str3) {
        if (zzauj().zzayr()) {
            zzauk().zzayc().log("Cannot get conditional user properties from analytics worker thread");
            return Collections.emptyList();
        }
        zzauj();
        if (zzccj.zzaq()) {
            zzauk().zzayc().log("Cannot get conditional user properties from main thread");
            return Collections.emptyList();
        }
        AtomicReference atomicReference = new AtomicReference();
        synchronized (atomicReference) {
            this.zzikb.zzauj().zzg(new zzcds(this, atomicReference, str, str2, str3));
            try {
                atomicReference.wait(5000);
            } catch (InterruptedException e) {
                zzauk().zzaye().zze("Interrupted waiting for get conditional user properties", str, e);
            }
        }
        List<zzcan> list = (List) atomicReference.get();
        if (list == null) {
            zzauk().zzaye().zzj("Timed out waiting for get conditional user properties", str);
            return Collections.emptyList();
        }
        List<ConditionalUserProperty> arrayList = new ArrayList(list.size());
        for (zzcan zzcan : list) {
            ConditionalUserProperty conditionalUserProperty = new ConditionalUserProperty();
            conditionalUserProperty.mAppId = str;
            conditionalUserProperty.mOrigin = str2;
            conditionalUserProperty.mCreationTimestamp = zzcan.zzimb;
            conditionalUserProperty.mName = zzcan.zzima.name;
            conditionalUserProperty.mValue = zzcan.zzima.getValue();
            conditionalUserProperty.mActive = zzcan.zzimc;
            conditionalUserProperty.mTriggerEventName = zzcan.zzimd;
            if (zzcan.zzime != null) {
                conditionalUserProperty.mTimedOutEventName = zzcan.zzime.name;
                if (zzcan.zzime.zzinj != null) {
                    conditionalUserProperty.mTimedOutEventParams = zzcan.zzime.zzinj.zzaxy();
                }
            }
            conditionalUserProperty.mTriggerTimeout = zzcan.zzimf;
            if (zzcan.zzimg != null) {
                conditionalUserProperty.mTriggeredEventName = zzcan.zzimg.name;
                if (zzcan.zzimg.zzinj != null) {
                    conditionalUserProperty.mTriggeredEventParams = zzcan.zzimg.zzinj.zzaxy();
                }
            }
            conditionalUserProperty.mTriggeredTimestamp = zzcan.zzima.zziwu;
            conditionalUserProperty.mTimeToLive = zzcan.zzimh;
            if (zzcan.zzimi != null) {
                conditionalUserProperty.mExpiredEventName = zzcan.zzimi.name;
                if (zzcan.zzimi.zzinj != null) {
                    conditionalUserProperty.mExpiredEventParams = zzcan.zzimi.zzinj.zzaxy();
                }
            }
            arrayList.add(conditionalUserProperty);
        }
        return arrayList;
    }

    public final void clearConditionalUserProperty(String str, String str2, Bundle bundle) {
        zzatu();
        zza(null, str, str2, bundle);
    }

    public final void clearConditionalUserPropertyAs(String str, String str2, String str3, Bundle bundle) {
        zzbp.zzgf(str);
        zzatt();
        zza(str, str2, str3, bundle);
    }

    public final Task<String> getAppInstanceId() {
        try {
            String zzaym = zzaul().zzaym();
            return zzaym != null ? Tasks.forResult(zzaym) : Tasks.call(zzauj().zzays(), new zzcdz(this));
        } catch (Exception e) {
            zzauk().zzaye().log("Failed to schedule task for getAppInstanceId");
            return Tasks.forException(e);
        }
    }

    public final List<ConditionalUserProperty> getConditionalUserProperties(String str, String str2) {
        zzatu();
        return zzk(null, str, str2);
    }

    public final List<ConditionalUserProperty> getConditionalUserPropertiesAs(String str, String str2, String str3) {
        zzbp.zzgf(str);
        zzatt();
        return zzk(str, str2, str3);
    }

    public final /* bridge */ /* synthetic */ Context getContext() {
        return super.getContext();
    }

    public final Map<String, Object> getUserProperties(String str, String str2, boolean z) {
        zzatu();
        return zzb(null, str, str2, z);
    }

    public final Map<String, Object> getUserPropertiesAs(String str, String str2, String str3, boolean z) {
        zzbp.zzgf(str);
        zzatt();
        return zzb(str, str2, str3, z);
    }

    public final void registerOnMeasurementEventListener(OnEventListener onEventListener) {
        zzatu();
        zzwh();
        zzbp.zzu(onEventListener);
        if (!this.zzium.add(onEventListener)) {
            zzauk().zzaye().log("OnEventListener already registered");
        }
    }

    public final void setConditionalUserProperty(ConditionalUserProperty conditionalUserProperty) {
        zzbp.zzu(conditionalUserProperty);
        zzatu();
        ConditionalUserProperty conditionalUserProperty2 = new ConditionalUserProperty(conditionalUserProperty);
        if (!TextUtils.isEmpty(conditionalUserProperty2.mAppId)) {
            zzauk().zzaye().log("Package name should be null when calling setConditionalUserProperty");
        }
        conditionalUserProperty2.mAppId = null;
        zza(conditionalUserProperty2);
    }

    public final void setConditionalUserPropertyAs(ConditionalUserProperty conditionalUserProperty) {
        zzbp.zzu(conditionalUserProperty);
        zzbp.zzgf(conditionalUserProperty.mAppId);
        zzatt();
        zza(new ConditionalUserProperty(conditionalUserProperty));
    }

    @WorkerThread
    public final void setEventInterceptor(EventInterceptor eventInterceptor) {
        zzug();
        zzatu();
        zzwh();
        if (!(eventInterceptor == null || eventInterceptor == this.zziul)) {
            zzbp.zza(this.zziul == null, (Object) "EventInterceptor already set.");
        }
        this.zziul = eventInterceptor;
    }

    public final void setMeasurementEnabled(boolean z) {
        zzwh();
        zzatu();
        zzauj().zzg(new zzcdp(this, z));
    }

    public final void setMinimumSessionDuration(long j) {
        zzatu();
        zzauj().zzg(new zzcdu(this, j));
    }

    public final void setSessionTimeoutDuration(long j) {
        zzatu();
        zzauj().zzg(new zzcdv(this, j));
    }

    public final void unregisterOnMeasurementEventListener(OnEventListener onEventListener) {
        zzatu();
        zzwh();
        zzbp.zzu(onEventListener);
        if (!this.zzium.remove(onEventListener)) {
            zzauk().zzaye().log("OnEventListener had not been registered");
        }
    }

    public final void zza(String str, String str2, Bundle bundle, long j) {
        zzatu();
        zza(str, str2, j, bundle, false, true, true, null);
    }

    public final void zza(String str, String str2, Bundle bundle, boolean z) {
        zzatu();
        boolean z2 = this.zziul == null || zzcfo.zzkd(str2);
        zza(str, str2, bundle, true, z2, true, null);
    }

    public final /* bridge */ /* synthetic */ void zzatt() {
        super.zzatt();
    }

    public final /* bridge */ /* synthetic */ void zzatu() {
        super.zzatu();
    }

    public final /* bridge */ /* synthetic */ void zzatv() {
        super.zzatv();
    }

    public final /* bridge */ /* synthetic */ zzcaf zzatw() {
        return super.zzatw();
    }

    public final /* bridge */ /* synthetic */ zzcam zzatx() {
        return super.zzatx();
    }

    public final /* bridge */ /* synthetic */ zzcdo zzaty() {
        return super.zzaty();
    }

    public final /* bridge */ /* synthetic */ zzcbj zzatz() {
        return super.zzatz();
    }

    public final /* bridge */ /* synthetic */ zzcaw zzaua() {
        return super.zzaua();
    }

    public final /* bridge */ /* synthetic */ zzceg zzaub() {
        return super.zzaub();
    }

    public final /* bridge */ /* synthetic */ zzcec zzauc() {
        return super.zzauc();
    }

    public final /* bridge */ /* synthetic */ zzcbk zzaud() {
        return super.zzaud();
    }

    public final /* bridge */ /* synthetic */ zzcaq zzaue() {
        return super.zzaue();
    }

    public final /* bridge */ /* synthetic */ zzcbm zzauf() {
        return super.zzauf();
    }

    public final /* bridge */ /* synthetic */ zzcfo zzaug() {
        return super.zzaug();
    }

    public final /* bridge */ /* synthetic */ zzcci zzauh() {
        return super.zzauh();
    }

    public final /* bridge */ /* synthetic */ zzcfd zzaui() {
        return super.zzaui();
    }

    public final /* bridge */ /* synthetic */ zzccj zzauj() {
        return super.zzauj();
    }

    public final /* bridge */ /* synthetic */ zzcbo zzauk() {
        return super.zzauk();
    }

    public final /* bridge */ /* synthetic */ zzcbz zzaul() {
        return super.zzaul();
    }

    public final /* bridge */ /* synthetic */ zzcap zzaum() {
        return super.zzaum();
    }

    @Nullable
    public final String zzaym() {
        zzatu();
        return (String) this.zziuo.get();
    }

    public final void zzb(String str, String str2, Object obj) {
        int i = 0;
        zzbp.zzgf(str);
        long currentTimeMillis = zzvu().currentTimeMillis();
        int zzjy = zzaug().zzjy(str2);
        String zza;
        if (zzjy != 0) {
            zzaug();
            zza = zzcfo.zza(str2, zzcap.zzavn(), true);
            if (str2 != null) {
                i = str2.length();
            }
            this.zzikb.zzaug().zza(zzjy, "_ev", zza, i);
        } else if (obj != null) {
            zzjy = zzaug().zzl(str2, obj);
            if (zzjy != 0) {
                zzaug();
                zza = zzcfo.zza(str2, zzcap.zzavn(), true);
                if ((obj instanceof String) || (obj instanceof CharSequence)) {
                    i = String.valueOf(obj).length();
                }
                this.zzikb.zzaug().zza(zzjy, "_ev", zza, i);
                return;
            }
            Object zzm = zzaug().zzm(str2, obj);
            if (zzm != null) {
                zza(str, str2, currentTimeMillis, zzm);
            }
        } else {
            zza(str, str2, currentTimeMillis, null);
        }
    }

    @Nullable
    final String zzbc(long j) {
        AtomicReference atomicReference = new AtomicReference();
        synchronized (atomicReference) {
            zzauj().zzg(new zzcea(this, atomicReference));
            try {
                atomicReference.wait(j);
            } catch (InterruptedException e) {
                zzauk().zzaye().log("Interrupted waiting for app instance id");
                return null;
            }
        }
        return (String) atomicReference.get();
    }

    public final List<zzcfl> zzbq(boolean z) {
        zzatu();
        zzwh();
        zzauk().zzayh().log("Fetching user attributes (FE)");
        if (zzauj().zzayr()) {
            zzauk().zzayc().log("Cannot get all user properties from analytics worker thread");
            return Collections.emptyList();
        }
        zzauj();
        if (zzccj.zzaq()) {
            zzauk().zzayc().log("Cannot get all user properties from main thread");
            return Collections.emptyList();
        }
        AtomicReference atomicReference = new AtomicReference();
        synchronized (atomicReference) {
            this.zzikb.zzauj().zzg(new zzcdy(this, atomicReference, z));
            try {
                atomicReference.wait(5000);
            } catch (InterruptedException e) {
                zzauk().zzaye().zzj("Interrupted waiting for get user properties", e);
            }
        }
        List<zzcfl> list = (List) atomicReference.get();
        if (list != null) {
            return list;
        }
        zzauk().zzaye().log("Timed out waiting for get user properties");
        return Collections.emptyList();
    }

    public final void zzc(String str, String str2, Bundle bundle) {
        zzatu();
        boolean z = this.zziul == null || zzcfo.zzkd(str2);
        zza(str, str2, bundle, true, z, false, null);
    }

    final void zzjk(@Nullable String str) {
        this.zziuo.set(str);
    }

    public final /* bridge */ /* synthetic */ void zzug() {
        super.zzug();
    }

    protected final void zzuh() {
    }

    public final /* bridge */ /* synthetic */ zzd zzvu() {
        return super.zzvu();
    }
}
