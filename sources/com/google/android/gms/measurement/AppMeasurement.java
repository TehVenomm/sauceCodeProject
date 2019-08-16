package com.google.android.gms.measurement;

import android.content.Context;
import android.os.Bundle;
import android.support.annotation.Keep;
import android.support.annotation.NonNull;
import android.support.annotation.Nullable;
import android.support.annotation.RequiresPermission;
import android.support.annotation.Size;
import android.support.annotation.WorkerThread;
import android.support.p000v4.util.ArrayMap;
import com.google.android.gms.common.annotation.KeepForSdk;
import com.google.android.gms.common.internal.Preconditions;
import com.google.android.gms.common.internal.ShowFirstParty;
import com.google.android.gms.common.util.VisibleForTesting;
import com.google.android.gms.measurement.internal.zzfj;
import com.google.android.gms.measurement.internal.zzgg;
import com.google.android.gms.measurement.internal.zzgi;
import com.google.android.gms.measurement.internal.zzgj;
import com.google.android.gms.measurement.internal.zzgk;
import com.google.android.gms.measurement.internal.zzgl;
import com.google.android.gms.measurement.internal.zzgn;
import com.google.android.gms.measurement.internal.zzhi;
import com.google.android.gms.measurement.internal.zzho;
import com.google.android.gms.measurement.internal.zzjn;
import java.util.ArrayList;
import java.util.List;
import java.util.Map;

@ShowFirstParty
@Deprecated
public class AppMeasurement {
    @ShowFirstParty
    @KeepForSdk
    public static final String CRASH_ORIGIN = "crash";
    @ShowFirstParty
    @KeepForSdk
    public static final String FCM_ORIGIN = "fcm";
    @ShowFirstParty
    @KeepForSdk
    public static final String FIAM_ORIGIN = "fiam";
    private static volatile AppMeasurement zzi;
    private final zzfj zzj;
    private final zzhi zzk;
    private final boolean zzl;

    @ShowFirstParty
    @KeepForSdk
    public static class ConditionalUserProperty {
        @Keep
        @ShowFirstParty
        @KeepForSdk
        public boolean mActive;
        @Keep
        @ShowFirstParty
        @KeepForSdk
        public String mAppId;
        @Keep
        @ShowFirstParty
        @KeepForSdk
        public long mCreationTimestamp;
        @Keep
        public String mExpiredEventName;
        @Keep
        public Bundle mExpiredEventParams;
        @Keep
        @ShowFirstParty
        @KeepForSdk
        public String mName;
        @Keep
        @ShowFirstParty
        @KeepForSdk
        public String mOrigin;
        @Keep
        @ShowFirstParty
        @KeepForSdk
        public long mTimeToLive;
        @Keep
        public String mTimedOutEventName;
        @Keep
        public Bundle mTimedOutEventParams;
        @Keep
        @ShowFirstParty
        @KeepForSdk
        public String mTriggerEventName;
        @Keep
        @ShowFirstParty
        @KeepForSdk
        public long mTriggerTimeout;
        @Keep
        public String mTriggeredEventName;
        @Keep
        public Bundle mTriggeredEventParams;
        @Keep
        @ShowFirstParty
        @KeepForSdk
        public long mTriggeredTimestamp;
        @Keep
        @ShowFirstParty
        @KeepForSdk
        public Object mValue;

        @KeepForSdk
        public ConditionalUserProperty() {
        }

        private ConditionalUserProperty(@NonNull Bundle bundle) {
            Preconditions.checkNotNull(bundle);
            this.mAppId = (String) zzgg.zza(bundle, "app_id", String.class, null);
            this.mOrigin = (String) zzgg.zza(bundle, "origin", String.class, null);
            this.mName = (String) zzgg.zza(bundle, "name", String.class, null);
            this.mValue = zzgg.zza(bundle, "value", Object.class, null);
            this.mTriggerEventName = (String) zzgg.zza(bundle, com.google.android.gms.measurement.api.AppMeasurementSdk.ConditionalUserProperty.TRIGGER_EVENT_NAME, String.class, null);
            this.mTriggerTimeout = ((Long) zzgg.zza(bundle, com.google.android.gms.measurement.api.AppMeasurementSdk.ConditionalUserProperty.TRIGGER_TIMEOUT, Long.class, Long.valueOf(0))).longValue();
            this.mTimedOutEventName = (String) zzgg.zza(bundle, com.google.android.gms.measurement.api.AppMeasurementSdk.ConditionalUserProperty.TIMED_OUT_EVENT_NAME, String.class, null);
            this.mTimedOutEventParams = (Bundle) zzgg.zza(bundle, com.google.android.gms.measurement.api.AppMeasurementSdk.ConditionalUserProperty.TIMED_OUT_EVENT_PARAMS, Bundle.class, null);
            this.mTriggeredEventName = (String) zzgg.zza(bundle, com.google.android.gms.measurement.api.AppMeasurementSdk.ConditionalUserProperty.TRIGGERED_EVENT_NAME, String.class, null);
            this.mTriggeredEventParams = (Bundle) zzgg.zza(bundle, com.google.android.gms.measurement.api.AppMeasurementSdk.ConditionalUserProperty.TRIGGERED_EVENT_PARAMS, Bundle.class, null);
            this.mTimeToLive = ((Long) zzgg.zza(bundle, com.google.android.gms.measurement.api.AppMeasurementSdk.ConditionalUserProperty.TIME_TO_LIVE, Long.class, Long.valueOf(0))).longValue();
            this.mExpiredEventName = (String) zzgg.zza(bundle, com.google.android.gms.measurement.api.AppMeasurementSdk.ConditionalUserProperty.EXPIRED_EVENT_NAME, String.class, null);
            this.mExpiredEventParams = (Bundle) zzgg.zza(bundle, com.google.android.gms.measurement.api.AppMeasurementSdk.ConditionalUserProperty.EXPIRED_EVENT_PARAMS, Bundle.class, null);
        }

        @KeepForSdk
        public ConditionalUserProperty(ConditionalUserProperty conditionalUserProperty) {
            Preconditions.checkNotNull(conditionalUserProperty);
            this.mAppId = conditionalUserProperty.mAppId;
            this.mOrigin = conditionalUserProperty.mOrigin;
            this.mCreationTimestamp = conditionalUserProperty.mCreationTimestamp;
            this.mName = conditionalUserProperty.mName;
            if (conditionalUserProperty.mValue != null) {
                this.mValue = zzho.zza(conditionalUserProperty.mValue);
                if (this.mValue == null) {
                    this.mValue = conditionalUserProperty.mValue;
                }
            }
            this.mActive = conditionalUserProperty.mActive;
            this.mTriggerEventName = conditionalUserProperty.mTriggerEventName;
            this.mTriggerTimeout = conditionalUserProperty.mTriggerTimeout;
            this.mTimedOutEventName = conditionalUserProperty.mTimedOutEventName;
            if (conditionalUserProperty.mTimedOutEventParams != null) {
                this.mTimedOutEventParams = new Bundle(conditionalUserProperty.mTimedOutEventParams);
            }
            this.mTriggeredEventName = conditionalUserProperty.mTriggeredEventName;
            if (conditionalUserProperty.mTriggeredEventParams != null) {
                this.mTriggeredEventParams = new Bundle(conditionalUserProperty.mTriggeredEventParams);
            }
            this.mTriggeredTimestamp = conditionalUserProperty.mTriggeredTimestamp;
            this.mTimeToLive = conditionalUserProperty.mTimeToLive;
            this.mExpiredEventName = conditionalUserProperty.mExpiredEventName;
            if (conditionalUserProperty.mExpiredEventParams != null) {
                this.mExpiredEventParams = new Bundle(conditionalUserProperty.mExpiredEventParams);
            }
        }

        /* access modifiers changed from: private */
        public final Bundle zzd() {
            Bundle bundle = new Bundle();
            if (this.mAppId != null) {
                bundle.putString("app_id", this.mAppId);
            }
            if (this.mOrigin != null) {
                bundle.putString("origin", this.mOrigin);
            }
            if (this.mName != null) {
                bundle.putString("name", this.mName);
            }
            if (this.mValue != null) {
                zzgg.zza(bundle, this.mValue);
            }
            if (this.mTriggerEventName != null) {
                bundle.putString(com.google.android.gms.measurement.api.AppMeasurementSdk.ConditionalUserProperty.TRIGGER_EVENT_NAME, this.mTriggerEventName);
            }
            bundle.putLong(com.google.android.gms.measurement.api.AppMeasurementSdk.ConditionalUserProperty.TRIGGER_TIMEOUT, this.mTriggerTimeout);
            if (this.mTimedOutEventName != null) {
                bundle.putString(com.google.android.gms.measurement.api.AppMeasurementSdk.ConditionalUserProperty.TIMED_OUT_EVENT_NAME, this.mTimedOutEventName);
            }
            if (this.mTimedOutEventParams != null) {
                bundle.putBundle(com.google.android.gms.measurement.api.AppMeasurementSdk.ConditionalUserProperty.TIMED_OUT_EVENT_PARAMS, this.mTimedOutEventParams);
            }
            if (this.mTriggeredEventName != null) {
                bundle.putString(com.google.android.gms.measurement.api.AppMeasurementSdk.ConditionalUserProperty.TRIGGERED_EVENT_NAME, this.mTriggeredEventName);
            }
            if (this.mTriggeredEventParams != null) {
                bundle.putBundle(com.google.android.gms.measurement.api.AppMeasurementSdk.ConditionalUserProperty.TRIGGERED_EVENT_PARAMS, this.mTriggeredEventParams);
            }
            bundle.putLong(com.google.android.gms.measurement.api.AppMeasurementSdk.ConditionalUserProperty.TIME_TO_LIVE, this.mTimeToLive);
            if (this.mExpiredEventName != null) {
                bundle.putString(com.google.android.gms.measurement.api.AppMeasurementSdk.ConditionalUserProperty.EXPIRED_EVENT_NAME, this.mExpiredEventName);
            }
            if (this.mExpiredEventParams != null) {
                bundle.putBundle(com.google.android.gms.measurement.api.AppMeasurementSdk.ConditionalUserProperty.EXPIRED_EVENT_PARAMS, this.mExpiredEventParams);
            }
            bundle.putLong(com.google.android.gms.measurement.api.AppMeasurementSdk.ConditionalUserProperty.CREATION_TIMESTAMP, this.mCreationTimestamp);
            bundle.putBoolean(com.google.android.gms.measurement.api.AppMeasurementSdk.ConditionalUserProperty.ACTIVE, this.mActive);
            bundle.putLong(com.google.android.gms.measurement.api.AppMeasurementSdk.ConditionalUserProperty.TRIGGERED_TIMESTAMP, this.mTriggeredTimestamp);
            return bundle;
        }
    }

    @ShowFirstParty
    @KeepForSdk
    public static final class Event extends zzgj {
        @ShowFirstParty
        @KeepForSdk
        public static final String AD_REWARD = "_ar";
        @ShowFirstParty
        @KeepForSdk
        public static final String APP_EXCEPTION = "_ae";

        private Event() {
        }
    }

    @ShowFirstParty
    @KeepForSdk
    public interface EventInterceptor extends zzgk {
        @WorkerThread
        @ShowFirstParty
        @KeepForSdk
        void interceptEvent(String str, String str2, Bundle bundle, long j);
    }

    @ShowFirstParty
    @KeepForSdk
    public interface OnEventListener extends zzgn {
        @WorkerThread
        @ShowFirstParty
        @KeepForSdk
        void onEvent(String str, String str2, Bundle bundle, long j);
    }

    @ShowFirstParty
    @KeepForSdk
    public static final class Param extends zzgi {
        @ShowFirstParty
        @KeepForSdk
        public static final String FATAL = "fatal";
        @ShowFirstParty
        @KeepForSdk
        public static final String TIMESTAMP = "timestamp";
        @ShowFirstParty
        @KeepForSdk
        public static final String TYPE = "type";

        private Param() {
        }
    }

    @ShowFirstParty
    @KeepForSdk
    public static final class UserProperty extends zzgl {
        @ShowFirstParty
        @KeepForSdk
        public static final String FIREBASE_LAST_NOTIFICATION = "_ln";

        private UserProperty() {
        }
    }

    private AppMeasurement(zzfj zzfj) {
        Preconditions.checkNotNull(zzfj);
        this.zzj = zzfj;
        this.zzk = null;
        this.zzl = false;
    }

    private AppMeasurement(zzhi zzhi) {
        Preconditions.checkNotNull(zzhi);
        this.zzk = zzhi;
        this.zzj = null;
        this.zzl = true;
    }

    @Keep
    @ShowFirstParty
    @RequiresPermission(allOf = {"android.permission.INTERNET", "android.permission.ACCESS_NETWORK_STATE", "android.permission.WAKE_LOCK"})
    @Deprecated
    public static AppMeasurement getInstance(Context context) {
        return zza(context, null, null);
    }

    public static AppMeasurement zza(Context context, Bundle bundle) {
        if (zzi == null) {
            synchronized (AppMeasurement.class) {
                try {
                    if (zzi == null) {
                        zzhi zzb = zzb(context, bundle);
                        if (zzb != null) {
                            zzi = new AppMeasurement(zzb);
                        } else {
                            zzi = new AppMeasurement(zzfj.zza(context, null, null, bundle));
                        }
                    }
                } finally {
                    Class<AppMeasurement> cls = AppMeasurement.class;
                }
            }
        }
        return zzi;
    }

    @VisibleForTesting
    private static AppMeasurement zza(Context context, String str, String str2) {
        if (zzi == null) {
            synchronized (AppMeasurement.class) {
                try {
                    if (zzi == null) {
                        zzhi zzb = zzb(context, null);
                        if (zzb != null) {
                            zzi = new AppMeasurement(zzb);
                        } else {
                            zzi = new AppMeasurement(zzfj.zza(context, null, null, null));
                        }
                    }
                } finally {
                    Class<AppMeasurement> cls = AppMeasurement.class;
                }
            }
        }
        return zzi;
    }

    private static zzhi zzb(Context context, Bundle bundle) {
        try {
            try {
                return (zzhi) Class.forName("com.google.firebase.analytics.FirebaseAnalytics").getDeclaredMethod("getScionFrontendApiImplementation", new Class[]{Context.class, Bundle.class}).invoke(null, new Object[]{context, bundle});
            } catch (Exception e) {
                return null;
            }
        } catch (ClassNotFoundException e2) {
            return null;
        }
    }

    @Keep
    public void beginAdUnitExposure(@Size(min = 1) @NonNull String str) {
        if (this.zzl) {
            this.zzk.beginAdUnitExposure(str);
        } else {
            this.zzj.zzp().beginAdUnitExposure(str, this.zzj.zzx().elapsedRealtime());
        }
    }

    @Keep
    @ShowFirstParty
    @KeepForSdk
    public void clearConditionalUserProperty(@Size(max = 24, min = 1) @NonNull String str, @Nullable String str2, @Nullable Bundle bundle) {
        if (this.zzl) {
            this.zzk.clearConditionalUserProperty(str, str2, bundle);
        } else {
            this.zzj.zzq().clearConditionalUserProperty(str, str2, bundle);
        }
    }

    /* access modifiers changed from: protected */
    @Keep
    @VisibleForTesting
    public void clearConditionalUserPropertyAs(@Size(min = 1) @NonNull String str, @Size(max = 24, min = 1) @NonNull String str2, @Nullable String str3, @Nullable Bundle bundle) {
        if (this.zzl) {
            throw new IllegalStateException("Unexpected call on client side");
        }
        this.zzj.zzq().clearConditionalUserPropertyAs(str, str2, str3, bundle);
    }

    @Keep
    public void endAdUnitExposure(@Size(min = 1) @NonNull String str) {
        if (this.zzl) {
            this.zzk.endAdUnitExposure(str);
        } else {
            this.zzj.zzp().endAdUnitExposure(str, this.zzj.zzx().elapsedRealtime());
        }
    }

    @Keep
    public long generateEventId() {
        return this.zzl ? this.zzk.generateEventId() : this.zzj.zzz().zzjv();
    }

    @Keep
    @Nullable
    public String getAppInstanceId() {
        return this.zzl ? this.zzk.zzi() : this.zzj.zzq().zzi();
    }

    @KeepForSdk
    public Boolean getBoolean() {
        return this.zzl ? (Boolean) this.zzk.zzb(4) : this.zzj.zzq().zzig();
    }

    @Keep
    @ShowFirstParty
    @WorkerThread
    @KeepForSdk
    public List<ConditionalUserProperty> getConditionalUserProperties(@Nullable String str, @Nullable @Size(max = 23, min = 1) String str2) {
        List<Bundle> zzn = this.zzl ? this.zzk.getConditionalUserProperties(str, str2) : this.zzj.zzq().zzn(str, str2);
        ArrayList arrayList = new ArrayList(zzn == null ? 0 : zzn.size());
        for (Bundle conditionalUserProperty : zzn) {
            arrayList.add(new ConditionalUserProperty(conditionalUserProperty));
        }
        return arrayList;
    }

    /* access modifiers changed from: protected */
    @Keep
    @WorkerThread
    @VisibleForTesting
    public List<ConditionalUserProperty> getConditionalUserPropertiesAs(@Size(min = 1) @NonNull String str, @Nullable String str2, @Nullable @Size(max = 23, min = 1) String str3) {
        int i = 0;
        if (this.zzl) {
            throw new IllegalStateException("Unexpected call on client side");
        }
        ArrayList zzd = this.zzj.zzq().zzd(str, str2, str3);
        ArrayList arrayList = new ArrayList(zzd == null ? 0 : zzd.size());
        ArrayList arrayList2 = zzd;
        int size = arrayList2.size();
        while (i < size) {
            Object obj = arrayList2.get(i);
            i++;
            arrayList.add(new ConditionalUserProperty((Bundle) obj));
        }
        return arrayList;
    }

    @Keep
    @Nullable
    public String getCurrentScreenClass() {
        return this.zzl ? this.zzk.getCurrentScreenClass() : this.zzj.zzq().getCurrentScreenClass();
    }

    @Keep
    @Nullable
    public String getCurrentScreenName() {
        return this.zzl ? this.zzk.getCurrentScreenName() : this.zzj.zzq().getCurrentScreenName();
    }

    @KeepForSdk
    public Double getDouble() {
        return this.zzl ? (Double) this.zzk.zzb(2) : this.zzj.zzq().zzik();
    }

    @Keep
    @Nullable
    public String getGmpAppId() {
        return this.zzl ? this.zzk.getGmpAppId() : this.zzj.zzq().getGmpAppId();
    }

    @KeepForSdk
    public Integer getInteger() {
        return this.zzl ? (Integer) this.zzk.zzb(3) : this.zzj.zzq().zzij();
    }

    @KeepForSdk
    public Long getLong() {
        return this.zzl ? (Long) this.zzk.zzb(1) : this.zzj.zzq().zzii();
    }

    @Keep
    @ShowFirstParty
    @WorkerThread
    @KeepForSdk
    public int getMaxUserProperties(@Size(min = 1) @NonNull String str) {
        if (this.zzl) {
            return this.zzk.getMaxUserProperties(str);
        }
        this.zzj.zzq();
        Preconditions.checkNotEmpty(str);
        return 25;
    }

    @KeepForSdk
    public String getString() {
        return this.zzl ? (String) this.zzk.zzb(0) : this.zzj.zzq().zzih();
    }

    /* access modifiers changed from: protected */
    @Keep
    @WorkerThread
    @VisibleForTesting
    public Map<String, Object> getUserProperties(@Nullable String str, @Nullable @Size(max = 24, min = 1) String str2, boolean z) {
        return this.zzl ? this.zzk.getUserProperties(str, str2, z) : this.zzj.zzq().getUserProperties(str, str2, z);
    }

    @ShowFirstParty
    @WorkerThread
    @KeepForSdk
    public Map<String, Object> getUserProperties(boolean z) {
        if (this.zzl) {
            return this.zzk.getUserProperties(null, null, z);
        }
        List<zzjn> zzh = this.zzj.zzq().zzh(z);
        ArrayMap arrayMap = new ArrayMap(zzh.size());
        for (zzjn zzjn : zzh) {
            arrayMap.put(zzjn.name, zzjn.getValue());
        }
        return arrayMap;
    }

    /* access modifiers changed from: protected */
    @Keep
    @WorkerThread
    @VisibleForTesting
    public Map<String, Object> getUserPropertiesAs(@Size(min = 1) @NonNull String str, @Nullable String str2, @Nullable @Size(max = 23, min = 1) String str3, boolean z) {
        if (!this.zzl) {
            return this.zzj.zzq().getUserPropertiesAs(str, str2, str3, z);
        }
        throw new IllegalStateException("Unexpected call on client side");
    }

    @Keep
    @ShowFirstParty
    public void logEventInternal(String str, String str2, Bundle bundle) {
        if (this.zzl) {
            this.zzk.logEventInternal(str, str2, bundle);
        } else {
            this.zzj.zzq().logEvent(str, str2, bundle);
        }
    }

    @ShowFirstParty
    @KeepForSdk
    public void logEventInternalNoInterceptor(String str, String str2, Bundle bundle, long j) {
        if (this.zzl) {
            this.zzk.logEventInternalNoInterceptor(str, str2, bundle, j);
        } else {
            this.zzj.zzq().logEvent(str, str2, bundle, true, false, j);
        }
    }

    @ShowFirstParty
    @KeepForSdk
    public void registerOnMeasurementEventListener(OnEventListener onEventListener) {
        if (this.zzl) {
            this.zzk.zza((zzgn) onEventListener);
        } else {
            this.zzj.zzq().zza((zzgn) onEventListener);
        }
    }

    @Keep
    @ShowFirstParty
    @KeepForSdk
    public void setConditionalUserProperty(@NonNull ConditionalUserProperty conditionalUserProperty) {
        Preconditions.checkNotNull(conditionalUserProperty);
        if (this.zzl) {
            this.zzk.setConditionalUserProperty(conditionalUserProperty.zzd());
        } else {
            this.zzj.zzq().setConditionalUserProperty(conditionalUserProperty.zzd());
        }
    }

    /* access modifiers changed from: protected */
    @Keep
    @VisibleForTesting
    public void setConditionalUserPropertyAs(@NonNull ConditionalUserProperty conditionalUserProperty) {
        Preconditions.checkNotNull(conditionalUserProperty);
        if (this.zzl) {
            throw new IllegalStateException("Unexpected call on client side");
        }
        this.zzj.zzq().zzd(conditionalUserProperty.zzd());
    }

    @WorkerThread
    @ShowFirstParty
    @KeepForSdk
    public void setEventInterceptor(EventInterceptor eventInterceptor) {
        if (this.zzl) {
            this.zzk.zza((zzgk) eventInterceptor);
        } else {
            this.zzj.zzq().zza((zzgk) eventInterceptor);
        }
    }

    @KeepForSdk
    @Deprecated
    public void setMeasurementEnabled(boolean z) {
        if (this.zzl) {
            this.zzk.setMeasurementEnabled(z);
        } else {
            this.zzj.zzq().setMeasurementEnabled(z);
        }
    }

    @ShowFirstParty
    @KeepForSdk
    public void setUserPropertyInternal(String str, String str2, Object obj) {
        Preconditions.checkNotEmpty(str);
        if (this.zzl) {
            this.zzk.setUserPropertyInternal(str, str2, obj);
        } else {
            this.zzj.zzq().zzb(str, str2, obj, true);
        }
    }

    @ShowFirstParty
    @KeepForSdk
    public void unregisterOnMeasurementEventListener(OnEventListener onEventListener) {
        if (this.zzl) {
            this.zzk.zzb((zzgn) onEventListener);
        } else {
            this.zzj.zzq().zzb(onEventListener);
        }
    }

    public final void zza(boolean z) {
        if (this.zzl) {
            this.zzk.setDataCollectionEnabled(z);
        } else {
            this.zzj.zzq().zza(z);
        }
    }
}
