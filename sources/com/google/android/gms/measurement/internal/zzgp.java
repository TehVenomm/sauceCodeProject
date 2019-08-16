package com.google.android.gms.measurement.internal;

import android.app.Application;
import android.content.Context;
import android.os.Build.VERSION;
import android.os.Bundle;
import android.support.annotation.Nullable;
import android.support.annotation.WorkerThread;
import android.support.p000v4.util.ArrayMap;
import android.text.TextUtils;
import com.facebook.internal.ServerProtocol;
import com.google.android.gms.common.api.internal.GoogleServices;
import com.google.android.gms.common.internal.Preconditions;
import com.google.android.gms.common.util.Clock;
import com.google.android.gms.common.util.VisibleForTesting;
import com.google.android.gms.measurement.api.AppMeasurementSdk.ConditionalUserProperty;
import java.util.ArrayList;
import java.util.Collections;
import java.util.List;
import java.util.Map;
import java.util.Set;
import java.util.concurrent.CopyOnWriteArraySet;
import java.util.concurrent.atomic.AtomicReference;
import p017io.fabric.sdk.android.services.settings.SettingsJsonConstants;

public final class zzgp extends zzg {
    @VisibleForTesting
    protected zzhj zzpu;
    private zzgk zzpv;
    private final Set<zzgn> zzpw = new CopyOnWriteArraySet();
    private boolean zzpx;
    private final AtomicReference<String> zzpy = new AtomicReference<>();
    @VisibleForTesting
    protected boolean zzpz = true;

    protected zzgp(zzfj zzfj) {
        super(zzfj);
    }

    private final void zza(Bundle bundle, long j) {
        Preconditions.checkNotNull(bundle);
        zzgg.zza(bundle, "app_id", String.class, null);
        zzgg.zza(bundle, "origin", String.class, null);
        zzgg.zza(bundle, "name", String.class, null);
        zzgg.zza(bundle, "value", Object.class, null);
        zzgg.zza(bundle, ConditionalUserProperty.TRIGGER_EVENT_NAME, String.class, null);
        zzgg.zza(bundle, ConditionalUserProperty.TRIGGER_TIMEOUT, Long.class, Long.valueOf(0));
        zzgg.zza(bundle, ConditionalUserProperty.TIMED_OUT_EVENT_NAME, String.class, null);
        zzgg.zza(bundle, ConditionalUserProperty.TIMED_OUT_EVENT_PARAMS, Bundle.class, null);
        zzgg.zza(bundle, ConditionalUserProperty.TRIGGERED_EVENT_NAME, String.class, null);
        zzgg.zza(bundle, ConditionalUserProperty.TRIGGERED_EVENT_PARAMS, Bundle.class, null);
        zzgg.zza(bundle, ConditionalUserProperty.TIME_TO_LIVE, Long.class, Long.valueOf(0));
        zzgg.zza(bundle, ConditionalUserProperty.EXPIRED_EVENT_NAME, String.class, null);
        zzgg.zza(bundle, ConditionalUserProperty.EXPIRED_EVENT_PARAMS, Bundle.class, null);
        Preconditions.checkNotEmpty(bundle.getString("name"));
        Preconditions.checkNotEmpty(bundle.getString("origin"));
        Preconditions.checkNotNull(bundle.get("value"));
        bundle.putLong(ConditionalUserProperty.CREATION_TIMESTAMP, j);
        String string = bundle.getString("name");
        Object obj = bundle.get("value");
        if (zzz().zzbm(string) != 0) {
            zzab().zzgk().zza("Invalid conditional user property name", zzy().zzal(string));
        } else if (zzz().zzc(string, obj) != 0) {
            zzab().zzgk().zza("Invalid conditional user property value", zzy().zzal(string), obj);
        } else {
            Object zzd = zzz().zzd(string, obj);
            if (zzd == null) {
                zzab().zzgk().zza("Unable to normalize conditional user property value", zzy().zzal(string), obj);
                return;
            }
            zzgg.zza(bundle, zzd);
            long j2 = bundle.getLong(ConditionalUserProperty.TRIGGER_TIMEOUT);
            if (TextUtils.isEmpty(bundle.getString(ConditionalUserProperty.TRIGGER_EVENT_NAME)) || (j2 <= 15552000000L && j2 >= 1)) {
                long j3 = bundle.getLong(ConditionalUserProperty.TIME_TO_LIVE);
                if (j3 > 15552000000L || j3 < 1) {
                    zzab().zzgk().zza("Invalid conditional user property time to live", zzy().zzal(string), Long.valueOf(j3));
                } else {
                    zzaa().zza((Runnable) new zzgx(this, bundle));
                }
            } else {
                zzab().zzgk().zza("Invalid conditional user property timeout", zzy().zzal(string), Long.valueOf(j2));
            }
        }
    }

    /* access modifiers changed from: private */
    /* JADX WARNING: Removed duplicated region for block: B:109:0x0355  */
    /* JADX WARNING: Removed duplicated region for block: B:112:0x037b  */
    /* JADX WARNING: Removed duplicated region for block: B:115:0x039f  */
    /* JADX WARNING: Removed duplicated region for block: B:120:0x03e4  */
    /* JADX WARNING: Removed duplicated region for block: B:130:0x047d  */
    /* JADX WARNING: Removed duplicated region for block: B:134:0x0495  */
    /* JADX WARNING: Removed duplicated region for block: B:158:0x0542  */
    /* JADX WARNING: Removed duplicated region for block: B:163:0x051d A[EDGE_INSN: B:163:0x051d->B:152:0x051d ?: BREAK  
    EDGE_INSN: B:163:0x051d->B:152:0x051d ?: BREAK  , SYNTHETIC] */
    /* JADX WARNING: Removed duplicated region for block: B:89:0x02a4  */
    /* JADX WARNING: Removed duplicated region for block: B:92:0x02b2  */
    @android.support.annotation.WorkerThread
    /* Code decompiled incorrectly, please refer to instructions dump. */
    public final void zza(java.lang.String r26, java.lang.String r27, long r28, android.os.Bundle r30, boolean r31, boolean r32, boolean r33, java.lang.String r34) {
        /*
            r25 = this;
            com.google.android.gms.common.internal.Preconditions.checkNotEmpty(r26)
            com.google.android.gms.measurement.internal.zzs r4 = r25.zzad()
            com.google.android.gms.measurement.internal.zzdu<java.lang.Boolean> r5 = com.google.android.gms.measurement.internal.zzak.zzip
            r0 = r34
            boolean r4 = r4.zze(r0, r5)
            if (r4 != 0) goto L_0x0014
            com.google.android.gms.common.internal.Preconditions.checkNotEmpty(r27)
        L_0x0014:
            com.google.android.gms.common.internal.Preconditions.checkNotNull(r30)
            r25.zzo()
            r25.zzbi()
            r0 = r25
            com.google.android.gms.measurement.internal.zzfj r4 = r0.zzj
            boolean r4 = r4.isEnabled()
            if (r4 != 0) goto L_0x0035
            com.google.android.gms.measurement.internal.zzef r4 = r25.zzab()
            com.google.android.gms.measurement.internal.zzeh r4 = r4.zzgr()
            java.lang.String r5 = "Event not sent since app measurement is disabled"
            r4.zzao(r5)
        L_0x0034:
            return
        L_0x0035:
            com.google.android.gms.measurement.internal.zzs r4 = r25.zzad()
            com.google.android.gms.measurement.internal.zzdy r5 = r25.zzr()
            java.lang.String r5 = r5.zzag()
            com.google.android.gms.measurement.internal.zzdu<java.lang.Boolean> r6 = com.google.android.gms.measurement.internal.zzak.zzix
            boolean r4 = r4.zze(r5, r6)
            if (r4 == 0) goto L_0x006d
            com.google.android.gms.measurement.internal.zzdy r4 = r25.zzr()
            java.util.List r4 = r4.zzbh()
            if (r4 == 0) goto L_0x006d
            r0 = r27
            boolean r4 = r4.contains(r0)
            if (r4 != 0) goto L_0x006d
            com.google.android.gms.measurement.internal.zzef r4 = r25.zzab()
            com.google.android.gms.measurement.internal.zzeh r4 = r4.zzgr()
            java.lang.String r5 = "Dropping non-safelisted event. event name, origin"
            r0 = r27
            r1 = r26
            r4.zza(r5, r0, r1)
            goto L_0x0034
        L_0x006d:
            r0 = r25
            boolean r4 = r0.zzpx
            if (r4 != 0) goto L_0x00ad
            r4 = 1
            r0 = r25
            r0.zzpx = r4
            r0 = r25
            com.google.android.gms.measurement.internal.zzfj r4 = r0.zzj     // Catch:{ ClassNotFoundException -> 0x0167 }
            boolean r4 = r4.zzia()     // Catch:{ ClassNotFoundException -> 0x0167 }
            if (r4 != 0) goto L_0x014f
            java.lang.String r4 = "com.google.android.gms.tagmanager.TagManagerService"
            r5 = 1
            android.content.Context r6 = r25.getContext()     // Catch:{ ClassNotFoundException -> 0x0167 }
            java.lang.ClassLoader r6 = r6.getClassLoader()     // Catch:{ ClassNotFoundException -> 0x0167 }
            java.lang.Class r4 = java.lang.Class.forName(r4, r5, r6)     // Catch:{ ClassNotFoundException -> 0x0167 }
        L_0x0091:
            java.lang.String r5 = "initialize"
            r6 = 1
            java.lang.Class[] r6 = new java.lang.Class[r6]     // Catch:{ Exception -> 0x0157 }
            r7 = 0
            java.lang.Class<android.content.Context> r8 = android.content.Context.class
            r6[r7] = r8     // Catch:{ Exception -> 0x0157 }
            java.lang.reflect.Method r4 = r4.getDeclaredMethod(r5, r6)     // Catch:{ Exception -> 0x0157 }
            r5 = 0
            r6 = 1
            java.lang.Object[] r6 = new java.lang.Object[r6]     // Catch:{ Exception -> 0x0157 }
            r7 = 0
            android.content.Context r8 = r25.getContext()     // Catch:{ Exception -> 0x0157 }
            r6[r7] = r8     // Catch:{ Exception -> 0x0157 }
            r4.invoke(r5, r6)     // Catch:{ Exception -> 0x0157 }
        L_0x00ad:
            com.google.android.gms.measurement.internal.zzs r4 = r25.zzad()
            com.google.android.gms.measurement.internal.zzdy r5 = r25.zzr()
            java.lang.String r5 = r5.zzag()
            com.google.android.gms.measurement.internal.zzdu<java.lang.Boolean> r6 = com.google.android.gms.measurement.internal.zzak.zzje
            boolean r4 = r4.zze(r5, r6)
            if (r4 == 0) goto L_0x00ee
            java.lang.String r4 = "_cmp"
            r0 = r27
            boolean r4 = r4.equals(r0)
            if (r4 == 0) goto L_0x00ee
            java.lang.String r4 = "gclid"
            r0 = r30
            boolean r4 = r0.containsKey(r4)
            if (r4 == 0) goto L_0x00ee
            java.lang.String r5 = "auto"
            java.lang.String r6 = "_lgclid"
            java.lang.String r4 = "gclid"
            r0 = r30
            java.lang.String r7 = r0.getString(r4)
            com.google.android.gms.common.util.Clock r4 = r25.zzx()
            long r8 = r4.currentTimeMillis()
            r4 = r25
            r4.zza(r5, r6, r7, r8)
        L_0x00ee:
            if (r33 == 0) goto L_0x019d
            r25.zzae()
            java.lang.String r4 = "_iap"
            r0 = r27
            boolean r4 = r4.equals(r0)
            if (r4 != 0) goto L_0x019d
            r0 = r25
            com.google.android.gms.measurement.internal.zzfj r4 = r0.zzj
            com.google.android.gms.measurement.internal.zzjs r4 = r4.zzz()
            java.lang.String r5 = "event"
            r0 = r27
            boolean r5 = r4.zzp(r5, r0)
            if (r5 != 0) goto L_0x0177
            r4 = 2
            r5 = r4
        L_0x0111:
            if (r5 == 0) goto L_0x019d
            com.google.android.gms.measurement.internal.zzef r4 = r25.zzab()
            com.google.android.gms.measurement.internal.zzeh r4 = r4.zzgm()
            java.lang.String r6 = "Invalid public event name. Event will not be logged (FE)"
            com.google.android.gms.measurement.internal.zzed r7 = r25.zzy()
            r0 = r27
            java.lang.String r7 = r7.zzaj(r0)
            r4.zza(r6, r7)
            r0 = r25
            com.google.android.gms.measurement.internal.zzfj r4 = r0.zzj
            r4.zzz()
            r4 = 40
            r6 = 1
            r0 = r27
            java.lang.String r6 = com.google.android.gms.measurement.internal.zzjs.zza(r0, r4, r6)
            if (r27 == 0) goto L_0x019b
            int r4 = r27.length()
        L_0x0140:
            r0 = r25
            com.google.android.gms.measurement.internal.zzfj r7 = r0.zzj
            com.google.android.gms.measurement.internal.zzjs r7 = r7.zzz()
            java.lang.String r8 = "_ev"
            r7.zza(r5, r8, r6, r4)
            goto L_0x0034
        L_0x014f:
            java.lang.String r4 = "com.google.android.gms.tagmanager.TagManagerService"
            java.lang.Class r4 = java.lang.Class.forName(r4)     // Catch:{ ClassNotFoundException -> 0x0167 }
            goto L_0x0091
        L_0x0157:
            r4 = move-exception
            com.google.android.gms.measurement.internal.zzef r5 = r25.zzab()     // Catch:{ ClassNotFoundException -> 0x0167 }
            com.google.android.gms.measurement.internal.zzeh r5 = r5.zzgn()     // Catch:{ ClassNotFoundException -> 0x0167 }
            java.lang.String r6 = "Failed to invoke Tag Manager's initialize() method"
            r5.zza(r6, r4)     // Catch:{ ClassNotFoundException -> 0x0167 }
            goto L_0x00ad
        L_0x0167:
            r4 = move-exception
            com.google.android.gms.measurement.internal.zzef r4 = r25.zzab()
            com.google.android.gms.measurement.internal.zzeh r4 = r4.zzgq()
            java.lang.String r5 = "Tag Manager is not found and thus will not be used"
            r4.zzao(r5)
            goto L_0x00ad
        L_0x0177:
            java.lang.String r5 = "event"
            java.lang.String[] r6 = com.google.android.gms.measurement.internal.zzgj.zzpn
            r0 = r27
            boolean r5 = r4.zza(r5, r6, r0)
            if (r5 != 0) goto L_0x0187
            r4 = 13
            r5 = r4
            goto L_0x0111
        L_0x0187:
            java.lang.String r5 = "event"
            r6 = 40
            r0 = r27
            boolean r4 = r4.zza(r5, r6, r0)
            if (r4 != 0) goto L_0x0197
            r4 = 2
            r5 = r4
            goto L_0x0111
        L_0x0197:
            r4 = 0
            r5 = r4
            goto L_0x0111
        L_0x019b:
            r4 = 0
            goto L_0x0140
        L_0x019d:
            r25.zzae()
            com.google.android.gms.measurement.internal.zzhq r4 = r25.zzt()
            com.google.android.gms.measurement.internal.zzhr r11 = r4.zzin()
            if (r11 == 0) goto L_0x01b7
            java.lang.String r4 = "_sc"
            r0 = r30
            boolean r4 = r0.containsKey(r4)
            if (r4 != 0) goto L_0x01b7
            r4 = 1
            r11.zzqx = r4
        L_0x01b7:
            if (r31 == 0) goto L_0x020b
            if (r33 == 0) goto L_0x020b
            r4 = 1
        L_0x01bc:
            r0 = r30
            com.google.android.gms.measurement.internal.zzhq.zza(r11, r0, r4)
            java.lang.String r4 = "am"
            r0 = r26
            boolean r17 = r4.equals(r0)
            boolean r4 = com.google.android.gms.measurement.internal.zzjs.zzbq(r27)
            if (r31 == 0) goto L_0x020d
            r0 = r25
            com.google.android.gms.measurement.internal.zzgk r5 = r0.zzpv
            if (r5 == 0) goto L_0x020d
            if (r4 != 0) goto L_0x020d
            if (r17 != 0) goto L_0x020d
            com.google.android.gms.measurement.internal.zzef r4 = r25.zzab()
            com.google.android.gms.measurement.internal.zzeh r4 = r4.zzgr()
            java.lang.String r5 = "Passing event to registered event handler (FE)"
            com.google.android.gms.measurement.internal.zzed r6 = r25.zzy()
            r0 = r27
            java.lang.String r6 = r6.zzaj(r0)
            com.google.android.gms.measurement.internal.zzed r7 = r25.zzy()
            r0 = r30
            java.lang.String r7 = r7.zzc(r0)
            r4.zza(r5, r6, r7)
            r0 = r25
            com.google.android.gms.measurement.internal.zzgk r4 = r0.zzpv
            r5 = r26
            r6 = r27
            r7 = r30
            r8 = r28
            r4.interceptEvent(r5, r6, r7, r8)
            goto L_0x0034
        L_0x020b:
            r4 = 0
            goto L_0x01bc
        L_0x020d:
            r0 = r25
            com.google.android.gms.measurement.internal.zzfj r4 = r0.zzj
            boolean r4 = r4.zzie()
            if (r4 == 0) goto L_0x0034
            com.google.android.gms.measurement.internal.zzjs r4 = r25.zzz()
            r0 = r27
            int r6 = r4.zzbl(r0)
            if (r6 == 0) goto L_0x025f
            com.google.android.gms.measurement.internal.zzef r4 = r25.zzab()
            com.google.android.gms.measurement.internal.zzeh r4 = r4.zzgm()
            java.lang.String r5 = "Invalid event name. Event will not be logged (FE)"
            com.google.android.gms.measurement.internal.zzed r7 = r25.zzy()
            r0 = r27
            java.lang.String r7 = r7.zzaj(r0)
            r4.zza(r5, r7)
            r25.zzz()
            r4 = 40
            r5 = 1
            r0 = r27
            java.lang.String r8 = com.google.android.gms.measurement.internal.zzjs.zza(r0, r4, r5)
            if (r27 == 0) goto L_0x025d
            int r9 = r27.length()
        L_0x024c:
            r0 = r25
            com.google.android.gms.measurement.internal.zzfj r4 = r0.zzj
            com.google.android.gms.measurement.internal.zzjs r4 = r4.zzz()
            java.lang.String r7 = "_ev"
            r5 = r34
            r4.zza(r5, r6, r7, r8, r9)
            goto L_0x0034
        L_0x025d:
            r9 = 0
            goto L_0x024c
        L_0x025f:
            r4 = 4
            java.lang.String[] r4 = new java.lang.String[r4]
            r5 = 0
            java.lang.String r6 = "_o"
            r4[r5] = r6
            r5 = 1
            java.lang.String r6 = "_sn"
            r4[r5] = r6
            r5 = 2
            java.lang.String r6 = "_sc"
            r4[r5] = r6
            r5 = 3
            java.lang.String r6 = "_si"
            r4[r5] = r6
            java.util.List r8 = com.google.android.gms.common.util.CollectionUtils.listOf((T[]) r4)
            com.google.android.gms.measurement.internal.zzjs r4 = r25.zzz()
            r10 = 1
            r5 = r34
            r6 = r27
            r7 = r30
            r9 = r33
            android.os.Bundle r18 = r4.zza(r5, r6, r7, r8, r9, r10)
            if (r18 == 0) goto L_0x02a1
            java.lang.String r4 = "_sc"
            r0 = r18
            boolean r4 = r0.containsKey(r4)
            if (r4 == 0) goto L_0x02a1
            java.lang.String r4 = "_si"
            r0 = r18
            boolean r4 = r0.containsKey(r4)
            if (r4 != 0) goto L_0x044a
        L_0x02a1:
            r4 = 0
        L_0x02a2:
            if (r4 != 0) goto L_0x0542
            r16 = r11
        L_0x02a6:
            com.google.android.gms.measurement.internal.zzs r4 = r25.zzad()
            r0 = r34
            boolean r4 = r4.zzz(r0)
            if (r4 == 0) goto L_0x02e0
            r25.zzae()
            com.google.android.gms.measurement.internal.zzhq r4 = r25.zzt()
            com.google.android.gms.measurement.internal.zzhr r4 = r4.zzin()
            if (r4 == 0) goto L_0x02e0
            java.lang.String r4 = "_ae"
            r0 = r27
            boolean r4 = r4.equals(r0)
            if (r4 == 0) goto L_0x02e0
            com.google.android.gms.measurement.internal.zziw r4 = r25.zzv()
            long r4 = r4.zzjb()
            r6 = 0
            int r6 = (r4 > r6 ? 1 : (r4 == r6 ? 0 : -1))
            if (r6 <= 0) goto L_0x02e0
            com.google.android.gms.measurement.internal.zzjs r6 = r25.zzz()
            r0 = r18
            r6.zzb(r0, r4)
        L_0x02e0:
            java.util.ArrayList r19 = new java.util.ArrayList
            r19.<init>()
            r0 = r19
            r1 = r18
            r0.add(r1)
            com.google.android.gms.measurement.internal.zzjs r4 = r25.zzz()
            java.security.SecureRandom r4 = r4.zzjw()
            long r20 = r4.nextLong()
            com.google.android.gms.measurement.internal.zzs r4 = r25.zzad()
            com.google.android.gms.measurement.internal.zzdy r5 = r25.zzr()
            java.lang.String r5 = r5.zzag()
            com.google.android.gms.measurement.internal.zzdu<java.lang.Boolean> r6 = com.google.android.gms.measurement.internal.zzak.zzid
            boolean r4 = r4.zze(r5, r6)
            if (r4 == 0) goto L_0x038d
            com.google.android.gms.measurement.internal.zzeo r4 = r25.zzac()
            com.google.android.gms.measurement.internal.zzet r4 = r4.zzma
            long r4 = r4.get()
            r6 = 0
            int r4 = (r4 > r6 ? 1 : (r4 == r6 ? 0 : -1))
            if (r4 <= 0) goto L_0x038d
            com.google.android.gms.measurement.internal.zzeo r4 = r25.zzac()
            r0 = r28
            boolean r4 = r4.zzx(r0)
            if (r4 == 0) goto L_0x038d
            com.google.android.gms.measurement.internal.zzeo r4 = r25.zzac()
            com.google.android.gms.measurement.internal.zzeq r4 = r4.zzmd
            boolean r4 = r4.get()
            if (r4 == 0) goto L_0x038d
            com.google.android.gms.measurement.internal.zzef r4 = r25.zzab()
            com.google.android.gms.measurement.internal.zzeh r4 = r4.zzgs()
            java.lang.String r5 = "Current session is expired, remove the session number and Id"
            r4.zzao(r5)
            com.google.android.gms.measurement.internal.zzs r4 = r25.zzad()
            com.google.android.gms.measurement.internal.zzdy r5 = r25.zzr()
            java.lang.String r5 = r5.zzag()
            com.google.android.gms.measurement.internal.zzdu<java.lang.Boolean> r6 = com.google.android.gms.measurement.internal.zzak.zzhz
            boolean r4 = r4.zze(r5, r6)
            if (r4 == 0) goto L_0x0367
            java.lang.String r11 = "auto"
            java.lang.String r12 = "_sid"
            r13 = 0
            com.google.android.gms.common.util.Clock r4 = r25.zzx()
            long r14 = r4.currentTimeMillis()
            r10 = r25
            r10.zza(r11, r12, r13, r14)
        L_0x0367:
            com.google.android.gms.measurement.internal.zzs r4 = r25.zzad()
            com.google.android.gms.measurement.internal.zzdy r5 = r25.zzr()
            java.lang.String r5 = r5.zzag()
            com.google.android.gms.measurement.internal.zzdu<java.lang.Boolean> r6 = com.google.android.gms.measurement.internal.zzak.zzia
            boolean r4 = r4.zze(r5, r6)
            if (r4 == 0) goto L_0x038d
            java.lang.String r11 = "auto"
            java.lang.String r12 = "_sno"
            r13 = 0
            com.google.android.gms.common.util.Clock r4 = r25.zzx()
            long r14 = r4.currentTimeMillis()
            r10 = r25
            r10.zza(r11, r12, r13, r14)
        L_0x038d:
            com.google.android.gms.measurement.internal.zzs r4 = r25.zzad()
            com.google.android.gms.measurement.internal.zzdy r5 = r25.zzr()
            java.lang.String r5 = r5.zzag()
            boolean r4 = r4.zzy(r5)
            if (r4 == 0) goto L_0x03ca
            java.lang.String r4 = "extend_session"
            r6 = 0
            r0 = r18
            long r4 = r0.getLong(r4, r6)
            r6 = 1
            int r4 = (r4 > r6 ? 1 : (r4 == r6 ? 0 : -1))
            if (r4 != 0) goto L_0x03ca
            com.google.android.gms.measurement.internal.zzef r4 = r25.zzab()
            com.google.android.gms.measurement.internal.zzeh r4 = r4.zzgs()
            java.lang.String r5 = "EXTEND_SESSION param attached: initiate a new session or extend the current active session"
            r4.zzao(r5)
            r0 = r25
            com.google.android.gms.measurement.internal.zzfj r4 = r0.zzj
            com.google.android.gms.measurement.internal.zziw r4 = r4.zzv()
            r5 = 1
            r0 = r28
            r4.zza(r0, r5)
        L_0x03ca:
            r13 = 0
            java.util.Set r4 = r18.keySet()
            int r5 = r30.size()
            java.lang.String[] r5 = new java.lang.String[r5]
            java.lang.Object[] r4 = r4.toArray(r5)
            r11 = r4
            java.lang.String[] r11 = (java.lang.String[]) r11
            java.util.Arrays.sort(r11)
            int r15 = r11.length
            r4 = 0
            r14 = r4
        L_0x03e2:
            if (r14 >= r15) goto L_0x047b
            r22 = r11[r14]
            r0 = r18
            r1 = r22
            java.lang.Object r4 = r0.get(r1)
            r25.zzz()
            android.os.Bundle[] r23 = com.google.android.gms.measurement.internal.zzjs.zzb(r4)
            if (r23 == 0) goto L_0x053f
            r0 = r23
            int r4 = r0.length
            r0 = r18
            r1 = r22
            r0.putInt(r1, r4)
            r4 = 0
            r12 = r4
        L_0x0403:
            r0 = r23
            int r4 = r0.length
            if (r12 >= r4) goto L_0x0471
            r7 = r23[r12]
            r4 = 1
            r0 = r16
            com.google.android.gms.measurement.internal.zzhq.zza(r0, r7, r4)
            com.google.android.gms.measurement.internal.zzjs r4 = r25.zzz()
            java.lang.String r6 = "_ep"
            r10 = 0
            r5 = r34
            r9 = r33
            android.os.Bundle r4 = r4.zza(r5, r6, r7, r8, r9, r10)
            java.lang.String r5 = "_en"
            r0 = r27
            r4.putString(r5, r0)
            java.lang.String r5 = "_eid"
            r0 = r20
            r4.putLong(r5, r0)
            java.lang.String r5 = "_gn"
            r0 = r22
            r4.putString(r5, r0)
            java.lang.String r5 = "_ll"
            r0 = r23
            int r6 = r0.length
            r4.putInt(r5, r6)
            java.lang.String r5 = "_i"
            r4.putInt(r5, r12)
            r0 = r19
            r0.add(r4)
            int r4 = r12 + 1
            r12 = r4
            goto L_0x0403
        L_0x044a:
            com.google.android.gms.measurement.internal.zzhr r4 = new com.google.android.gms.measurement.internal.zzhr
            java.lang.String r5 = "_sn"
            r0 = r18
            java.lang.String r5 = r0.getString(r5)
            java.lang.String r6 = "_sc"
            r0 = r18
            java.lang.String r6 = r0.getString(r6)
            java.lang.String r7 = "_si"
            r0 = r18
            long r12 = r0.getLong(r7)
            java.lang.Long r7 = java.lang.Long.valueOf(r12)
            long r12 = r7.longValue()
            r4.<init>(r5, r6, r12)
            goto L_0x02a2
        L_0x0471:
            r0 = r23
            int r4 = r0.length
            int r4 = r4 + r13
        L_0x0475:
            int r5 = r14 + 1
            r14 = r5
            r13 = r4
            goto L_0x03e2
        L_0x047b:
            if (r13 == 0) goto L_0x048d
            java.lang.String r4 = "_eid"
            r0 = r18
            r1 = r20
            r0.putLong(r4, r1)
            java.lang.String r4 = "_epc"
            r0 = r18
            r0.putInt(r4, r13)
        L_0x048d:
            r4 = 0
            r11 = r4
        L_0x048f:
            int r4 = r19.size()
            if (r11 >= r4) goto L_0x051d
            r0 = r19
            java.lang.Object r4 = r0.get(r11)
            android.os.Bundle r4 = (android.os.Bundle) r4
            if (r11 == 0) goto L_0x0511
            r5 = 1
        L_0x04a0:
            if (r5 == 0) goto L_0x0513
            java.lang.String r5 = "_ep"
        L_0x04a4:
            java.lang.String r6 = "_o"
            r0 = r26
            r4.putString(r6, r0)
            if (r32 == 0) goto L_0x0516
            com.google.android.gms.measurement.internal.zzjs r6 = r25.zzz()
            android.os.Bundle r4 = r6.zzg(r4)
            r10 = r4
        L_0x04b6:
            com.google.android.gms.measurement.internal.zzef r4 = r25.zzab()
            com.google.android.gms.measurement.internal.zzeh r4 = r4.zzgr()
            java.lang.String r6 = "Logging event (FE)"
            com.google.android.gms.measurement.internal.zzed r7 = r25.zzy()
            r0 = r27
            java.lang.String r7 = r7.zzaj(r0)
            com.google.android.gms.measurement.internal.zzed r8 = r25.zzy()
            java.lang.String r8 = r8.zzc(r10)
            r4.zza(r6, r7, r8)
            com.google.android.gms.measurement.internal.zzai r4 = new com.google.android.gms.measurement.internal.zzai
            com.google.android.gms.measurement.internal.zzah r6 = new com.google.android.gms.measurement.internal.zzah
            r6.<init>(r10)
            r7 = r26
            r8 = r28
            r4.<init>(r5, r6, r7, r8)
            com.google.android.gms.measurement.internal.zzhv r5 = r25.zzs()
            r0 = r34
            r5.zzc(r4, r0)
            if (r17 != 0) goto L_0x0518
            r0 = r25
            java.util.Set<com.google.android.gms.measurement.internal.zzgn> r4 = r0.zzpw
            java.util.Iterator r12 = r4.iterator()
        L_0x04f6:
            boolean r4 = r12.hasNext()
            if (r4 == 0) goto L_0x0518
            java.lang.Object r4 = r12.next()
            com.google.android.gms.measurement.internal.zzgn r4 = (com.google.android.gms.measurement.internal.zzgn) r4
            android.os.Bundle r7 = new android.os.Bundle
            r7.<init>(r10)
            r5 = r26
            r6 = r27
            r8 = r28
            r4.onEvent(r5, r6, r7, r8)
            goto L_0x04f6
        L_0x0511:
            r5 = 0
            goto L_0x04a0
        L_0x0513:
            r5 = r27
            goto L_0x04a4
        L_0x0516:
            r10 = r4
            goto L_0x04b6
        L_0x0518:
            int r4 = r11 + 1
            r11 = r4
            goto L_0x048f
        L_0x051d:
            r25.zzae()
            com.google.android.gms.measurement.internal.zzhq r4 = r25.zzt()
            com.google.android.gms.measurement.internal.zzhr r4 = r4.zzin()
            if (r4 == 0) goto L_0x0034
            java.lang.String r4 = "_ae"
            r0 = r27
            boolean r4 = r4.equals(r0)
            if (r4 == 0) goto L_0x0034
            com.google.android.gms.measurement.internal.zziw r4 = r25.zzv()
            r5 = 1
            r6 = 1
            r4.zza(r5, r6)
            goto L_0x0034
        L_0x053f:
            r4 = r13
            goto L_0x0475
        L_0x0542:
            r16 = r4
            goto L_0x02a6
        */
        throw new UnsupportedOperationException("Method not decompiled: com.google.android.gms.measurement.internal.zzgp.zza(java.lang.String, java.lang.String, long, android.os.Bundle, boolean, boolean, boolean, java.lang.String):void");
    }

    private final void zza(String str, String str2, long j, Object obj) {
        zzaa().zza((Runnable) new zzgq(this, str, str2, obj, j));
    }

    private final void zza(String str, String str2, String str3, Bundle bundle) {
        long currentTimeMillis = zzx().currentTimeMillis();
        Preconditions.checkNotEmpty(str2);
        Bundle bundle2 = new Bundle();
        if (str != null) {
            bundle2.putString("app_id", str);
        }
        bundle2.putString("name", str2);
        bundle2.putLong(ConditionalUserProperty.CREATION_TIMESTAMP, currentTimeMillis);
        if (str3 != null) {
            bundle2.putString(ConditionalUserProperty.EXPIRED_EVENT_NAME, str3);
            bundle2.putBundle(ConditionalUserProperty.EXPIRED_EVENT_PARAMS, bundle);
        }
        zzaa().zza((Runnable) new zzgw(this, bundle2));
    }

    @VisibleForTesting
    private final Map<String, Object> zzb(String str, String str2, String str3, boolean z) {
        if (zzaa().zzhp()) {
            zzab().zzgk().zzao("Cannot get user properties from analytics worker thread");
            return Collections.emptyMap();
        } else if (zzr.isMainThread()) {
            zzab().zzgk().zzao("Cannot get user properties from main thread");
            return Collections.emptyMap();
        } else {
            AtomicReference atomicReference = new AtomicReference();
            synchronized (atomicReference) {
                this.zzj.zzaa().zza((Runnable) new zzhb(this, atomicReference, str, str2, str3, z));
                try {
                    atomicReference.wait(5000);
                } catch (InterruptedException e) {
                    zzab().zzgn().zza("Interrupted waiting for get user properties", e);
                }
            }
            List<zzjn> list = (List) atomicReference.get();
            if (list == null) {
                zzab().zzgn().zzao("Timed out waiting for get user properties");
                return Collections.emptyMap();
            }
            ArrayMap arrayMap = new ArrayMap(list.size());
            for (zzjn zzjn : list) {
                arrayMap.put(zzjn.name, zzjn.getValue());
            }
            return arrayMap;
        }
    }

    private final void zzb(String str, String str2, long j, Bundle bundle, boolean z, boolean z2, boolean z3, String str3) {
        zzaa().zza((Runnable) new zzgr(this, str, str2, j, zzjs.zzh(bundle), z, z2, z3, str3));
    }

    @VisibleForTesting
    private final ArrayList<Bundle> zze(String str, String str2, String str3) {
        if (zzaa().zzhp()) {
            zzab().zzgk().zzao("Cannot get conditional user properties from analytics worker thread");
            return new ArrayList<>(0);
        } else if (zzr.isMainThread()) {
            zzab().zzgk().zzao("Cannot get conditional user properties from main thread");
            return new ArrayList<>(0);
        } else {
            AtomicReference atomicReference = new AtomicReference();
            synchronized (atomicReference) {
                this.zzj.zzaa().zza((Runnable) new zzgz(this, atomicReference, str, str2, str3));
                try {
                    atomicReference.wait(5000);
                } catch (InterruptedException e) {
                    zzab().zzgn().zza("Interrupted waiting for get conditional user properties", str, e);
                }
            }
            List list = (List) atomicReference.get();
            if (list != null) {
                return zzjs.zzd(list);
            }
            zzab().zzgn().zza("Timed out waiting for get conditional user properties", str);
            return new ArrayList<>();
        }
    }

    /* access modifiers changed from: private */
    @WorkerThread
    public final void zze(Bundle bundle) {
        zzo();
        zzbi();
        Preconditions.checkNotNull(bundle);
        Preconditions.checkNotEmpty(bundle.getString("name"));
        Preconditions.checkNotEmpty(bundle.getString("origin"));
        Preconditions.checkNotNull(bundle.get("value"));
        if (!this.zzj.isEnabled()) {
            zzab().zzgr().zzao("Conditional property not sent since collection is disabled");
            return;
        }
        zzjn zzjn = new zzjn(bundle.getString("name"), bundle.getLong(ConditionalUserProperty.TRIGGERED_TIMESTAMP), bundle.get("value"), bundle.getString("origin"));
        try {
            zzai zza = zzz().zza(bundle.getString("app_id"), bundle.getString(ConditionalUserProperty.TRIGGERED_EVENT_NAME), bundle.getBundle(ConditionalUserProperty.TRIGGERED_EVENT_PARAMS), bundle.getString("origin"), 0, true, false);
            zzai zza2 = zzz().zza(bundle.getString("app_id"), bundle.getString(ConditionalUserProperty.TIMED_OUT_EVENT_NAME), bundle.getBundle(ConditionalUserProperty.TIMED_OUT_EVENT_PARAMS), bundle.getString("origin"), 0, true, false);
            zzai zza3 = zzz().zza(bundle.getString("app_id"), bundle.getString(ConditionalUserProperty.EXPIRED_EVENT_NAME), bundle.getBundle(ConditionalUserProperty.EXPIRED_EVENT_PARAMS), bundle.getString("origin"), 0, true, false);
            zzjn zzjn2 = zzjn;
            zzs().zzd(new zzq(bundle.getString("app_id"), bundle.getString("origin"), zzjn2, bundle.getLong(ConditionalUserProperty.CREATION_TIMESTAMP), false, bundle.getString(ConditionalUserProperty.TRIGGER_EVENT_NAME), zza2, bundle.getLong(ConditionalUserProperty.TRIGGER_TIMEOUT), zza, bundle.getLong(ConditionalUserProperty.TIME_TO_LIVE), zza3));
        } catch (IllegalArgumentException e) {
        }
    }

    /* access modifiers changed from: private */
    @WorkerThread
    public final void zzf(Bundle bundle) {
        zzo();
        zzbi();
        Preconditions.checkNotNull(bundle);
        Preconditions.checkNotEmpty(bundle.getString("name"));
        if (!this.zzj.isEnabled()) {
            zzab().zzgr().zzao("Conditional property not cleared since collection is disabled");
            return;
        }
        zzjn zzjn = new zzjn(bundle.getString("name"), 0, null, null);
        try {
            zzai zza = zzz().zza(bundle.getString("app_id"), bundle.getString(ConditionalUserProperty.EXPIRED_EVENT_NAME), bundle.getBundle(ConditionalUserProperty.EXPIRED_EVENT_PARAMS), bundle.getString("origin"), bundle.getLong(ConditionalUserProperty.CREATION_TIMESTAMP), true, false);
            zzjn zzjn2 = zzjn;
            zzs().zzd(new zzq(bundle.getString("app_id"), bundle.getString("origin"), zzjn2, bundle.getLong(ConditionalUserProperty.CREATION_TIMESTAMP), bundle.getBoolean(ConditionalUserProperty.ACTIVE), bundle.getString(ConditionalUserProperty.TRIGGER_EVENT_NAME), null, bundle.getLong(ConditionalUserProperty.TRIGGER_TIMEOUT), null, bundle.getLong(ConditionalUserProperty.TIME_TO_LIVE), zza));
        } catch (IllegalArgumentException e) {
        }
    }

    /* access modifiers changed from: private */
    @WorkerThread
    public final void zzg(boolean z) {
        zzo();
        zzm();
        zzbi();
        zzab().zzgr().zza("Setting app measurement enabled (FE)", Boolean.valueOf(z));
        zzac().setMeasurementEnabled(z);
        zzil();
    }

    /* access modifiers changed from: private */
    @WorkerThread
    public final void zzil() {
        if (zzad().zze(zzr().zzag(), zzak.zzik)) {
            zzo();
            String zzho = zzac().zzlx.zzho();
            if (zzho != null) {
                if ("unset".equals(zzho)) {
                    zza(SettingsJsonConstants.APP_KEY, "_npa", (Object) null, zzx().currentTimeMillis());
                } else {
                    zza(SettingsJsonConstants.APP_KEY, "_npa", (Object) Long.valueOf(ServerProtocol.DIALOG_RETURN_SCOPES_TRUE.equals(zzho) ? 1 : 0), zzx().currentTimeMillis());
                }
            }
        }
        if (!this.zzj.isEnabled() || !this.zzpz) {
            zzab().zzgr().zzao("Updating Scion state (FE)");
            zzs().zzip();
            return;
        }
        zzab().zzgr().zzao("Recording app launch after enabling measurement for the first time (FE)");
        zzim();
    }

    @Nullable
    private final String zzz(long j) {
        AtomicReference atomicReference = new AtomicReference();
        synchronized (atomicReference) {
            zzaa().zza((Runnable) new zzgs(this, atomicReference));
            try {
                atomicReference.wait(j);
            } catch (InterruptedException e) {
                zzab().zzgn().zzao("Interrupted waiting for app instance id");
                return null;
            }
        }
        return (String) atomicReference.get();
    }

    public final void clearConditionalUserProperty(String str, String str2, Bundle bundle) {
        zzm();
        zza((String) null, str, str2, bundle);
    }

    public final void clearConditionalUserPropertyAs(String str, String str2, String str3, Bundle bundle) {
        Preconditions.checkNotEmpty(str);
        zzl();
        zza(str, str2, str3, bundle);
    }

    public final /* bridge */ /* synthetic */ Context getContext() {
        return super.getContext();
    }

    @Nullable
    public final String getCurrentScreenClass() {
        zzhr zzio = this.zzj.zzt().zzio();
        if (zzio != null) {
            return zzio.zzqv;
        }
        return null;
    }

    @Nullable
    public final String getCurrentScreenName() {
        zzhr zzio = this.zzj.zzt().zzio();
        if (zzio != null) {
            return zzio.zzqu;
        }
        return null;
    }

    @Nullable
    public final String getGmpAppId() {
        if (this.zzj.zzhx() != null) {
            return this.zzj.zzhx();
        }
        try {
            return GoogleServices.getGoogleAppId();
        } catch (IllegalStateException e) {
            this.zzj.zzab().zzgk().zza("getGoogleAppId failed with exception", e);
            return null;
        }
    }

    public final Map<String, Object> getUserProperties(String str, String str2, boolean z) {
        zzm();
        return zzb((String) null, str, str2, z);
    }

    public final Map<String, Object> getUserPropertiesAs(String str, String str2, String str3, boolean z) {
        Preconditions.checkNotEmpty(str);
        zzl();
        return zzb(str, str2, str3, z);
    }

    public final void logEvent(String str, String str2, Bundle bundle) {
        logEvent(str, str2, bundle, true, true, zzx().currentTimeMillis());
    }

    public final void logEvent(String str, String str2, Bundle bundle, boolean z, boolean z2, long j) {
        zzm();
        zzb(str == null ? SettingsJsonConstants.APP_KEY : str, str2, j, bundle == null ? new Bundle() : bundle, z2, !z2 || this.zzpv == null || zzjs.zzbq(str2), !z, null);
    }

    public final void resetAnalyticsData(long j) {
        zzbg(null);
        zzaa().zza((Runnable) new zzgv(this, j));
    }

    public final void setConditionalUserProperty(Bundle bundle) {
        setConditionalUserProperty(bundle, zzx().currentTimeMillis());
    }

    public final void setConditionalUserProperty(Bundle bundle, long j) {
        Preconditions.checkNotNull(bundle);
        zzm();
        Bundle bundle2 = new Bundle(bundle);
        if (!TextUtils.isEmpty(bundle2.getString("app_id"))) {
            zzab().zzgn().zzao("Package name should be null when calling setConditionalUserProperty");
        }
        bundle2.remove("app_id");
        zza(bundle2, j);
    }

    public final void setMeasurementEnabled(boolean z) {
        zzbi();
        zzm();
        zzaa().zza((Runnable) new zzhf(this, z));
    }

    public final void setMinimumSessionDuration(long j) {
        zzm();
        zzaa().zza((Runnable) new zzhh(this, j));
    }

    public final void setSessionTimeoutDuration(long j) {
        zzm();
        zzaa().zza((Runnable) new zzhg(this, j));
    }

    @WorkerThread
    public final void zza(zzgk zzgk) {
        zzo();
        zzm();
        zzbi();
        if (!(zzgk == null || zzgk == this.zzpv)) {
            Preconditions.checkState(this.zzpv == null, "EventInterceptor already set.");
        }
        this.zzpv = zzgk;
    }

    public final void zza(zzgn zzgn) {
        zzm();
        zzbi();
        Preconditions.checkNotNull(zzgn);
        if (!this.zzpw.add(zzgn)) {
            zzab().zzgn().zzao("OnEventListener already registered");
        }
    }

    /* access modifiers changed from: 0000 */
    @WorkerThread
    public final void zza(String str, String str2, long j, Bundle bundle) {
        zzm();
        zzo();
        zza(str, str2, j, bundle, true, this.zzpv == null || zzjs.zzbq(str2), false, null);
    }

    /* access modifiers changed from: 0000 */
    @WorkerThread
    public final void zza(String str, String str2, Bundle bundle) {
        zzm();
        zzo();
        zza(str, str2, zzx().currentTimeMillis(), bundle);
    }

    public final void zza(String str, String str2, Bundle bundle, boolean z) {
        logEvent(str, str2, bundle, false, true, zzx().currentTimeMillis());
    }

    /* access modifiers changed from: 0000 */
    /* JADX WARNING: Removed duplicated region for block: B:17:0x0070  */
    /* JADX WARNING: Removed duplicated region for block: B:23:0x0096  */
    @android.support.annotation.WorkerThread
    /* Code decompiled incorrectly, please refer to instructions dump. */
    public final void zza(java.lang.String r9, java.lang.String r10, java.lang.Object r11, long r12) {
        /*
            r8 = this;
            r4 = 1
            com.google.android.gms.common.internal.Preconditions.checkNotEmpty(r9)
            com.google.android.gms.common.internal.Preconditions.checkNotEmpty(r10)
            r8.zzo()
            r8.zzm()
            r8.zzbi()
            com.google.android.gms.measurement.internal.zzs r0 = r8.zzad()
            com.google.android.gms.measurement.internal.zzdy r1 = r8.zzr()
            java.lang.String r1 = r1.zzag()
            com.google.android.gms.measurement.internal.zzdu<java.lang.Boolean> r2 = com.google.android.gms.measurement.internal.zzak.zzik
            boolean r0 = r0.zze(r1, r2)
            if (r0 == 0) goto L_0x0093
            java.lang.String r0 = "allow_personalized_ads"
            boolean r0 = r0.equals(r10)
            if (r0 == 0) goto L_0x0093
            boolean r0 = r11 instanceof java.lang.String
            if (r0 == 0) goto L_0x0084
            r0 = r11
            java.lang.String r0 = (java.lang.String) r0
            boolean r0 = android.text.TextUtils.isEmpty(r0)
            if (r0 != 0) goto L_0x0084
            java.lang.String r0 = "false"
            java.lang.String r11 = (java.lang.String) r11
            java.util.Locale r1 = java.util.Locale.ENGLISH
            java.lang.String r1 = r11.toLowerCase(r1)
            boolean r0 = r0.equals(r1)
            if (r0 == 0) goto L_0x007e
            r0 = r4
        L_0x004b:
            java.lang.Long r2 = java.lang.Long.valueOf(r0)
            com.google.android.gms.measurement.internal.zzeo r0 = r8.zzac()
            com.google.android.gms.measurement.internal.zzev r1 = r0.zzlx
            r0 = r2
            java.lang.Long r0 = (java.lang.Long) r0
            long r6 = r0.longValue()
            int r0 = (r6 > r4 ? 1 : (r6 == r4 ? 0 : -1))
            if (r0 != 0) goto L_0x0081
            java.lang.String r0 = "true"
        L_0x0062:
            r1.zzau(r0)
            java.lang.String r1 = "_npa"
            r4 = r2
        L_0x0068:
            com.google.android.gms.measurement.internal.zzfj r0 = r8.zzj
            boolean r0 = r0.isEnabled()
            if (r0 != 0) goto L_0x0096
            com.google.android.gms.measurement.internal.zzef r0 = r8.zzab()
            com.google.android.gms.measurement.internal.zzeh r0 = r0.zzgr()
            java.lang.String r1 = "User property not set since app measurement is disabled"
            r0.zzao(r1)
        L_0x007d:
            return
        L_0x007e:
            r0 = 0
            goto L_0x004b
        L_0x0081:
            java.lang.String r0 = "false"
            goto L_0x0062
        L_0x0084:
            if (r11 != 0) goto L_0x0093
            java.lang.String r10 = "_npa"
            com.google.android.gms.measurement.internal.zzeo r0 = r8.zzac()
            com.google.android.gms.measurement.internal.zzev r0 = r0.zzlx
            java.lang.String r1 = "unset"
            r0.zzau(r1)
        L_0x0093:
            r4 = r11
            r1 = r10
            goto L_0x0068
        L_0x0096:
            com.google.android.gms.measurement.internal.zzfj r0 = r8.zzj
            boolean r0 = r0.zzie()
            if (r0 == 0) goto L_0x007d
            com.google.android.gms.measurement.internal.zzef r0 = r8.zzab()
            com.google.android.gms.measurement.internal.zzeh r0 = r0.zzgr()
            java.lang.String r2 = "Setting user property (FE)"
            com.google.android.gms.measurement.internal.zzed r3 = r8.zzy()
            java.lang.String r3 = r3.zzaj(r1)
            r0.zza(r2, r3, r4)
            com.google.android.gms.measurement.internal.zzjn r0 = new com.google.android.gms.measurement.internal.zzjn
            r2 = r12
            r5 = r9
            r0.<init>(r1, r2, r4, r5)
            com.google.android.gms.measurement.internal.zzhv r1 = r8.zzs()
            r1.zzb(r0)
            goto L_0x007d
        */
        throw new UnsupportedOperationException("Method not decompiled: com.google.android.gms.measurement.internal.zzgp.zza(java.lang.String, java.lang.String, java.lang.Object, long):void");
    }

    public final void zza(String str, String str2, Object obj, boolean z, long j) {
        int i = 0;
        int i2 = 6;
        String str3 = str == null ? SettingsJsonConstants.APP_KEY : str;
        if (z) {
            i2 = zzz().zzbm(str2);
        } else {
            zzjs zzz = zzz();
            if (zzz.zzp("user property", str2)) {
                if (!zzz.zza("user property", zzgl.zzpp, str2)) {
                    i2 = 15;
                } else if (zzz.zza("user property", 24, str2)) {
                    i2 = 0;
                }
            }
        }
        if (i2 != 0) {
            zzz();
            String zza = zzjs.zza(str2, 24, true);
            if (str2 != null) {
                i = str2.length();
            }
            this.zzj.zzz().zza(i2, "_ev", zza, i);
        } else if (obj != null) {
            int zzc = zzz().zzc(str2, obj);
            if (zzc != 0) {
                zzz();
                String zza2 = zzjs.zza(str2, 24, true);
                if ((obj instanceof String) || (obj instanceof CharSequence)) {
                    i = String.valueOf(obj).length();
                }
                this.zzj.zzz().zza(zzc, "_ev", zza2, i);
                return;
            }
            Object zzd = zzz().zzd(str2, obj);
            if (zzd != null) {
                zza(str3, str2, j, zzd);
            }
        } else {
            zza(str3, str2, j, (Object) null);
        }
    }

    public final void zza(boolean z) {
        zzbi();
        zzm();
        zzaa().zza((Runnable) new zzhe(this, z));
    }

    public final /* bridge */ /* synthetic */ zzfc zzaa() {
        return super.zzaa();
    }

    public final /* bridge */ /* synthetic */ zzef zzab() {
        return super.zzab();
    }

    public final /* bridge */ /* synthetic */ zzeo zzac() {
        return super.zzac();
    }

    public final /* bridge */ /* synthetic */ zzs zzad() {
        return super.zzad();
    }

    public final /* bridge */ /* synthetic */ zzr zzae() {
        return super.zzae();
    }

    public final void zzb(zzgn zzgn) {
        zzm();
        zzbi();
        Preconditions.checkNotNull(zzgn);
        if (!this.zzpw.remove(zzgn)) {
            zzab().zzgn().zzao("OnEventListener had not been registered");
        }
    }

    public final void zzb(String str, String str2, Object obj, boolean z) {
        zza(str, str2, obj, z, zzx().currentTimeMillis());
    }

    /* access modifiers changed from: 0000 */
    public final void zzbg(@Nullable String str) {
        this.zzpy.set(str);
    }

    /* access modifiers changed from: protected */
    public final boolean zzbk() {
        return false;
    }

    public final ArrayList<Bundle> zzd(String str, String str2, String str3) {
        Preconditions.checkNotEmpty(str);
        zzl();
        return zze(str, str2, str3);
    }

    public final void zzd(Bundle bundle) {
        Preconditions.checkNotNull(bundle);
        Preconditions.checkNotEmpty(bundle.getString("app_id"));
        zzl();
        zza(new Bundle(bundle), zzx().currentTimeMillis());
    }

    public final List<zzjn> zzh(boolean z) {
        zzm();
        zzbi();
        zzab().zzgr().zzao("Fetching user attributes (FE)");
        if (zzaa().zzhp()) {
            zzab().zzgk().zzao("Cannot get all user properties from analytics worker thread");
            return Collections.emptyList();
        } else if (zzr.isMainThread()) {
            zzab().zzgk().zzao("Cannot get all user properties from main thread");
            return Collections.emptyList();
        } else {
            AtomicReference atomicReference = new AtomicReference();
            synchronized (atomicReference) {
                this.zzj.zzaa().zza((Runnable) new zzgt(this, atomicReference, z));
                try {
                    atomicReference.wait(5000);
                } catch (InterruptedException e) {
                    zzab().zzgn().zza("Interrupted waiting for get user properties", e);
                }
            }
            List<zzjn> list = (List) atomicReference.get();
            if (list != null) {
                return list;
            }
            zzab().zzgn().zzao("Timed out waiting for get user properties");
            return Collections.emptyList();
        }
    }

    @Nullable
    public final String zzi() {
        zzm();
        return (String) this.zzpy.get();
    }

    public final void zzif() {
        if (getContext().getApplicationContext() instanceof Application) {
            ((Application) getContext().getApplicationContext()).unregisterActivityLifecycleCallbacks(this.zzpu);
        }
    }

    public final Boolean zzig() {
        AtomicReference atomicReference = new AtomicReference();
        return (Boolean) zzaa().zza(atomicReference, 15000, "boolean test flag value", new zzgo(this, atomicReference));
    }

    public final String zzih() {
        AtomicReference atomicReference = new AtomicReference();
        return (String) zzaa().zza(atomicReference, 15000, "String test flag value", new zzgy(this, atomicReference));
    }

    public final Long zzii() {
        AtomicReference atomicReference = new AtomicReference();
        return (Long) zzaa().zza(atomicReference, 15000, "long test flag value", new zzha(this, atomicReference));
    }

    public final Integer zzij() {
        AtomicReference atomicReference = new AtomicReference();
        return (Integer) zzaa().zza(atomicReference, 15000, "int test flag value", new zzhd(this, atomicReference));
    }

    public final Double zzik() {
        AtomicReference atomicReference = new AtomicReference();
        return (Double) zzaa().zza(atomicReference, 15000, "double test flag value", new zzhc(this, atomicReference));
    }

    @WorkerThread
    public final void zzim() {
        zzo();
        zzm();
        zzbi();
        if (this.zzj.zzie()) {
            zzs().zzim();
            this.zzpz = false;
            String zzhh = zzac().zzhh();
            if (!TextUtils.isEmpty(zzhh)) {
                zzw().zzbi();
                if (!zzhh.equals(VERSION.RELEASE)) {
                    Bundle bundle = new Bundle();
                    bundle.putString("_po", zzhh);
                    logEvent("auto", "_ou", bundle);
                }
            }
        }
    }

    public final /* bridge */ /* synthetic */ void zzl() {
        super.zzl();
    }

    public final /* bridge */ /* synthetic */ void zzm() {
        super.zzm();
    }

    public final ArrayList<Bundle> zzn(String str, String str2) {
        zzm();
        return zze(null, str, str2);
    }

    public final /* bridge */ /* synthetic */ void zzn() {
        super.zzn();
    }

    public final /* bridge */ /* synthetic */ void zzo() {
        super.zzo();
    }

    public final /* bridge */ /* synthetic */ zza zzp() {
        return super.zzp();
    }

    public final /* bridge */ /* synthetic */ zzgp zzq() {
        return super.zzq();
    }

    public final /* bridge */ /* synthetic */ zzdy zzr() {
        return super.zzr();
    }

    public final /* bridge */ /* synthetic */ zzhv zzs() {
        return super.zzs();
    }

    public final /* bridge */ /* synthetic */ zzhq zzt() {
        return super.zzt();
    }

    public final /* bridge */ /* synthetic */ zzeb zzu() {
        return super.zzu();
    }

    public final /* bridge */ /* synthetic */ zziw zzv() {
        return super.zzv();
    }

    public final /* bridge */ /* synthetic */ zzac zzw() {
        return super.zzw();
    }

    public final /* bridge */ /* synthetic */ Clock zzx() {
        return super.zzx();
    }

    public final /* bridge */ /* synthetic */ zzed zzy() {
        return super.zzy();
    }

    @Nullable
    public final String zzy(long j) {
        if (zzaa().zzhp()) {
            zzab().zzgk().zzao("Cannot retrieve app instance id from analytics worker thread");
            return null;
        } else if (zzr.isMainThread()) {
            zzab().zzgk().zzao("Cannot retrieve app instance id from main thread");
            return null;
        } else {
            long elapsedRealtime = zzx().elapsedRealtime();
            String zzz = zzz(120000);
            long elapsedRealtime2 = zzx().elapsedRealtime() - elapsedRealtime;
            return (zzz != null || elapsedRealtime2 >= 120000) ? zzz : zzz(120000 - elapsedRealtime2);
        }
    }

    public final /* bridge */ /* synthetic */ zzjs zzz() {
        return super.zzz();
    }
}
