package com.google.android.gms.stats;

import android.annotation.SuppressLint;
import android.content.Context;
import android.os.PowerManager;
import android.os.WorkSource;
import android.support.annotation.NonNull;
import android.support.annotation.Nullable;
import android.text.TextUtils;
import android.util.Log;
import com.google.android.gms.common.annotation.KeepForSdk;
import com.google.android.gms.common.internal.Preconditions;
import com.google.android.gms.common.internal.ShowFirstParty;
import com.google.android.gms.common.providers.PooledExecutorsProvider;
import com.google.android.gms.common.util.Strings;
import com.google.android.gms.common.util.WorkSourceUtil;
import java.util.Collections;
import java.util.HashMap;
import java.util.HashSet;
import java.util.List;
import java.util.Map;
import java.util.Set;
import java.util.concurrent.Future;
import java.util.concurrent.ScheduledExecutorService;
import java.util.concurrent.atomic.AtomicInteger;
import javax.annotation.concurrent.ThreadSafe;

@ShowFirstParty
@ThreadSafe
@KeepForSdk
public class WakeLock {
    private static ScheduledExecutorService zzn;
    private static volatile zza zzo = new zza();
    private final Object zza;
    private final android.os.PowerManager.WakeLock zzb;
    private WorkSource zzc;
    private final int zzd;
    private final String zze;
    private final String zzf;
    private final String zzg;
    private final Context zzh;
    private boolean zzi;
    private final Map<String, Integer[]> zzj;
    private final Set<Future<?>> zzk;
    private int zzl;
    private AtomicInteger zzm;

    public interface zza {
    }

    @KeepForSdk
    public WakeLock(@NonNull Context context, int i, @NonNull String str) {
        this(context, i, str, null, context == null ? null : context.getPackageName());
    }

    private WakeLock(@NonNull Context context, int i, @NonNull String str, @Nullable String str2, @NonNull String str3) {
        this(context, i, str, null, str3, null);
    }

    @SuppressLint({"UnwrappedWakeLock"})
    private WakeLock(@NonNull Context context, int i, @NonNull String str, @Nullable String str2, @NonNull String str3, @Nullable String str4) {
        this.zza = this;
        this.zzi = true;
        this.zzj = new HashMap();
        this.zzk = Collections.synchronizedSet(new HashSet());
        this.zzm = new AtomicInteger(0);
        Preconditions.checkNotNull(context, "WakeLock: context must not be null");
        Preconditions.checkNotEmpty(str, "WakeLock: wakeLockName must not be empty");
        this.zzd = i;
        this.zzf = null;
        this.zzg = null;
        this.zzh = context.getApplicationContext();
        if (!"com.google.android.gms".equals(context.getPackageName())) {
            String valueOf = String.valueOf("*gcore*:");
            String valueOf2 = String.valueOf(str);
            this.zze = valueOf2.length() != 0 ? valueOf.concat(valueOf2) : new String(valueOf);
        } else {
            this.zze = str;
        }
        this.zzb = ((PowerManager) context.getSystemService("power")).newWakeLock(i, str);
        if (WorkSourceUtil.hasWorkSourcePermission(context)) {
            if (Strings.isEmptyOrWhitespace(str3)) {
                str3 = context.getPackageName();
            }
            this.zzc = WorkSourceUtil.fromPackage(context, str3);
            WorkSource workSource = this.zzc;
            if (workSource != null && WorkSourceUtil.hasWorkSourcePermission(this.zzh)) {
                if (this.zzc != null) {
                    this.zzc.add(workSource);
                } else {
                    this.zzc = workSource;
                }
                try {
                    this.zzb.setWorkSource(this.zzc);
                } catch (ArrayIndexOutOfBoundsException | IllegalArgumentException e) {
                    Log.wtf("WakeLock", e.toString());
                }
            }
        }
        if (zzn == null) {
            zzn = PooledExecutorsProvider.getInstance().newSingleThreadScheduledExecutor();
        }
    }

    private final String zza(String str) {
        return this.zzi ? !TextUtils.isEmpty(str) ? str : this.zzf : this.zzf;
    }

    private final List<String> zza() {
        return WorkSourceUtil.getNames(this.zzc);
    }

    /* access modifiers changed from: private */
    public final void zza(int i) {
        if (this.zzb.isHeld()) {
            try {
                this.zzb.release();
            } catch (RuntimeException e) {
                if (e.getClass().equals(RuntimeException.class)) {
                    Log.e("WakeLock", String.valueOf(this.zze).concat(" was already released!"), e);
                } else {
                    throw e;
                }
            }
            this.zzb.isHeld();
        }
    }

    /* JADX WARNING: Code restructure failed: missing block: B:15:0x004a, code lost:
        if (r0 == false) goto L_0x004c;
     */
    /* JADX WARNING: Code restructure failed: missing block: B:19:0x0052, code lost:
        if (r13.zzl == 0) goto L_0x0054;
     */
    /* JADX WARNING: Code restructure failed: missing block: B:20:0x0054, code lost:
        com.google.android.gms.common.stats.WakeLockTracker.getInstance().registerEvent(r13.zzh, com.google.android.gms.common.stats.StatsUtils.getEventKey(r13.zzb, r6), 7, r13.zze, r6, null, r13.zzd, zza(), r14);
        r13.zzl++;
     */
    @com.google.android.gms.common.annotation.KeepForSdk
    /* Code decompiled incorrectly, please refer to instructions dump. */
    public void acquire(long r14) {
        /*
            r13 = this;
            r3 = 0
            r1 = 1
            r2 = 0
            java.util.concurrent.atomic.AtomicInteger r0 = r13.zzm
            r0.incrementAndGet()
            java.lang.String r6 = r13.zza(r3)
            java.lang.Object r12 = r13.zza
            monitor-enter(r12)
            java.util.Map<java.lang.String, java.lang.Integer[]> r0 = r13.zzj     // Catch:{ all -> 0x009f }
            boolean r0 = r0.isEmpty()     // Catch:{ all -> 0x009f }
            if (r0 == 0) goto L_0x001b
            int r0 = r13.zzl     // Catch:{ all -> 0x009f }
            if (r0 <= 0) goto L_0x002b
        L_0x001b:
            android.os.PowerManager$WakeLock r0 = r13.zzb     // Catch:{ all -> 0x009f }
            boolean r0 = r0.isHeld()     // Catch:{ all -> 0x009f }
            if (r0 != 0) goto L_0x002b
            java.util.Map<java.lang.String, java.lang.Integer[]> r0 = r13.zzj     // Catch:{ all -> 0x009f }
            r0.clear()     // Catch:{ all -> 0x009f }
            r0 = 0
            r13.zzl = r0     // Catch:{ all -> 0x009f }
        L_0x002b:
            boolean r0 = r13.zzi     // Catch:{ all -> 0x009f }
            if (r0 == 0) goto L_0x004c
            java.util.Map<java.lang.String, java.lang.Integer[]> r0 = r13.zzj     // Catch:{ all -> 0x009f }
            java.lang.Object r0 = r0.get(r6)     // Catch:{ all -> 0x009f }
            java.lang.Integer[] r0 = (java.lang.Integer[]) r0     // Catch:{ all -> 0x009f }
            if (r0 != 0) goto L_0x008d
            java.util.Map<java.lang.String, java.lang.Integer[]> r0 = r13.zzj     // Catch:{ all -> 0x009f }
            r2 = 1
            java.lang.Integer[] r2 = new java.lang.Integer[r2]     // Catch:{ all -> 0x009f }
            r3 = 0
            r4 = 1
            java.lang.Integer r4 = java.lang.Integer.valueOf(r4)     // Catch:{ all -> 0x009f }
            r2[r3] = r4     // Catch:{ all -> 0x009f }
            r0.put(r6, r2)     // Catch:{ all -> 0x009f }
            r0 = r1
        L_0x004a:
            if (r0 != 0) goto L_0x0054
        L_0x004c:
            boolean r0 = r13.zzi     // Catch:{ all -> 0x009f }
            if (r0 != 0) goto L_0x0074
            int r0 = r13.zzl     // Catch:{ all -> 0x009f }
            if (r0 != 0) goto L_0x0074
        L_0x0054:
            com.google.android.gms.common.stats.WakeLockTracker r1 = com.google.android.gms.common.stats.WakeLockTracker.getInstance()     // Catch:{ all -> 0x009f }
            android.content.Context r2 = r13.zzh     // Catch:{ all -> 0x009f }
            android.os.PowerManager$WakeLock r0 = r13.zzb     // Catch:{ all -> 0x009f }
            java.lang.String r3 = com.google.android.gms.common.stats.StatsUtils.getEventKey(r0, r6)     // Catch:{ all -> 0x009f }
            r4 = 7
            java.lang.String r5 = r13.zze     // Catch:{ all -> 0x009f }
            r7 = 0
            int r8 = r13.zzd     // Catch:{ all -> 0x009f }
            java.util.List r9 = r13.zza()     // Catch:{ all -> 0x009f }
            r10 = r14
            r1.registerEvent(r2, r3, r4, r5, r6, r7, r8, r9, r10)     // Catch:{ all -> 0x009f }
            int r0 = r13.zzl     // Catch:{ all -> 0x009f }
            int r0 = r0 + 1
            r13.zzl = r0     // Catch:{ all -> 0x009f }
        L_0x0074:
            monitor-exit(r12)     // Catch:{ all -> 0x009f }
            android.os.PowerManager$WakeLock r0 = r13.zzb
            r0.acquire()
            r0 = 0
            int r0 = (r14 > r0 ? 1 : (r14 == r0 ? 0 : -1))
            if (r0 <= 0) goto L_0x008c
            java.util.concurrent.ScheduledExecutorService r0 = zzn
            com.google.android.gms.stats.zzb r1 = new com.google.android.gms.stats.zzb
            r1.<init>(r13)
            java.util.concurrent.TimeUnit r2 = java.util.concurrent.TimeUnit.MILLISECONDS
            r0.schedule(r1, r14, r2)
        L_0x008c:
            return
        L_0x008d:
            r1 = 0
            r3 = 0
            r3 = r0[r3]     // Catch:{ all -> 0x009f }
            int r3 = r3.intValue()     // Catch:{ all -> 0x009f }
            int r3 = r3 + 1
            java.lang.Integer r3 = java.lang.Integer.valueOf(r3)     // Catch:{ all -> 0x009f }
            r0[r1] = r3     // Catch:{ all -> 0x009f }
            r0 = r2
            goto L_0x004a
        L_0x009f:
            r0 = move-exception
            monitor-exit(r12)     // Catch:{ all -> 0x009f }
            throw r0
        */
        throw new UnsupportedOperationException("Method not decompiled: com.google.android.gms.stats.WakeLock.acquire(long):void");
    }

    @KeepForSdk
    public boolean isHeld() {
        return this.zzb.isHeld();
    }

    /* JADX WARNING: Code restructure failed: missing block: B:11:0x0032, code lost:
        if (r0 == false) goto L_0x0034;
     */
    /* JADX WARNING: Code restructure failed: missing block: B:15:0x003a, code lost:
        if (r11.zzl == 1) goto L_0x003c;
     */
    /* JADX WARNING: Code restructure failed: missing block: B:16:0x003c, code lost:
        com.google.android.gms.common.stats.WakeLockTracker.getInstance().registerEvent(r11.zzh, com.google.android.gms.common.stats.StatsUtils.getEventKey(r11.zzb, r5), 8, r11.zze, r5, null, r11.zzd, zza());
        r11.zzl--;
     */
    @com.google.android.gms.common.annotation.KeepForSdk
    /* Code decompiled incorrectly, please refer to instructions dump. */
    public void release() {
        /*
            r11 = this;
            r4 = 0
            r1 = 1
            r9 = 0
            java.util.concurrent.atomic.AtomicInteger r0 = r11.zzm
            int r0 = r0.decrementAndGet()
            if (r0 >= 0) goto L_0x001c
            java.lang.String r0 = "WakeLock"
            java.lang.String r2 = r11.zze
            java.lang.String r2 = java.lang.String.valueOf(r2)
            java.lang.String r3 = " release without a matched acquire!"
            java.lang.String r2 = r2.concat(r3)
            android.util.Log.e(r0, r2)
        L_0x001c:
            java.lang.String r5 = r11.zza(r4)
            java.lang.Object r10 = r11.zza
            monitor-enter(r10)
            boolean r0 = r11.zzi     // Catch:{ all -> 0x0083 }
            if (r0 == 0) goto L_0x0034
            java.util.Map<java.lang.String, java.lang.Integer[]> r0 = r11.zzj     // Catch:{ all -> 0x0083 }
            java.lang.Object r0 = r0.get(r5)     // Catch:{ all -> 0x0083 }
            java.lang.Integer[] r0 = (java.lang.Integer[]) r0     // Catch:{ all -> 0x0083 }
            if (r0 != 0) goto L_0x0061
            r0 = r9
        L_0x0032:
            if (r0 != 0) goto L_0x003c
        L_0x0034:
            boolean r0 = r11.zzi     // Catch:{ all -> 0x0083 }
            if (r0 != 0) goto L_0x005c
            int r0 = r11.zzl     // Catch:{ all -> 0x0083 }
            if (r0 != r1) goto L_0x005c
        L_0x003c:
            com.google.android.gms.common.stats.WakeLockTracker r0 = com.google.android.gms.common.stats.WakeLockTracker.getInstance()     // Catch:{ all -> 0x0083 }
            android.content.Context r1 = r11.zzh     // Catch:{ all -> 0x0083 }
            android.os.PowerManager$WakeLock r2 = r11.zzb     // Catch:{ all -> 0x0083 }
            java.lang.String r2 = com.google.android.gms.common.stats.StatsUtils.getEventKey(r2, r5)     // Catch:{ all -> 0x0083 }
            r3 = 8
            java.lang.String r4 = r11.zze     // Catch:{ all -> 0x0083 }
            r6 = 0
            int r7 = r11.zzd     // Catch:{ all -> 0x0083 }
            java.util.List r8 = r11.zza()     // Catch:{ all -> 0x0083 }
            r0.registerEvent(r1, r2, r3, r4, r5, r6, r7, r8)     // Catch:{ all -> 0x0083 }
            int r0 = r11.zzl     // Catch:{ all -> 0x0083 }
            int r0 = r0 + -1
            r11.zzl = r0     // Catch:{ all -> 0x0083 }
        L_0x005c:
            monitor-exit(r10)     // Catch:{ all -> 0x0083 }
            r11.zza(r9)
            return
        L_0x0061:
            r2 = 0
            r2 = r0[r2]     // Catch:{ all -> 0x0083 }
            int r2 = r2.intValue()     // Catch:{ all -> 0x0083 }
            if (r2 != r1) goto L_0x0071
            java.util.Map<java.lang.String, java.lang.Integer[]> r0 = r11.zzj     // Catch:{ all -> 0x0083 }
            r0.remove(r5)     // Catch:{ all -> 0x0083 }
            r0 = r1
            goto L_0x0032
        L_0x0071:
            r2 = 0
            r3 = 0
            r3 = r0[r3]     // Catch:{ all -> 0x0083 }
            int r3 = r3.intValue()     // Catch:{ all -> 0x0083 }
            int r3 = r3 + -1
            java.lang.Integer r3 = java.lang.Integer.valueOf(r3)     // Catch:{ all -> 0x0083 }
            r0[r2] = r3     // Catch:{ all -> 0x0083 }
            r0 = r9
            goto L_0x0032
        L_0x0083:
            r0 = move-exception
            monitor-exit(r10)     // Catch:{ all -> 0x0083 }
            throw r0
        */
        throw new UnsupportedOperationException("Method not decompiled: com.google.android.gms.stats.WakeLock.release():void");
    }

    @KeepForSdk
    public void setReferenceCounted(boolean z) {
        this.zzb.setReferenceCounted(z);
        this.zzi = z;
    }
}
