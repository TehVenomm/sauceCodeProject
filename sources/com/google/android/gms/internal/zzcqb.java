package com.google.android.gms.internal;

import android.annotation.SuppressLint;
import android.content.Context;
import android.os.PowerManager;
import android.os.PowerManager.WakeLock;
import android.os.WorkSource;
import android.text.TextUtils;
import android.util.Log;
import com.google.android.gms.common.internal.zzbp;
import com.google.android.gms.common.util.zzs;
import com.google.android.gms.common.util.zzw;

public final class zzcqb {
    private static boolean DEBUG = false;
    private static String TAG = "WakeLock";
    private static String zzjno = "*gcore*:";
    private final Context mContext;
    private final String zzfxt;
    private final String zzfxv;
    private final WakeLock zzjnp;
    private WorkSource zzjnq;
    private final int zzjnr;
    private final String zzjns;
    private boolean zzjnt;
    private int zzjnu;
    private int zzjnv;

    public zzcqb(Context context, int i, String str) {
        this(context, 1, str, null, context == null ? null : context.getPackageName());
    }

    @SuppressLint({"UnwrappedWakeLock"})
    private zzcqb(Context context, int i, String str, String str2, String str3) {
        this(context, 1, str, null, str3, null);
    }

    @SuppressLint({"UnwrappedWakeLock"})
    private zzcqb(Context context, int i, String str, String str2, String str3, String str4) {
        this.zzjnt = true;
        zzbp.zzh(str, "Wake lock name can NOT be empty");
        this.zzjnr = i;
        this.zzjns = null;
        this.zzfxv = null;
        this.mContext = context.getApplicationContext();
        if ("com.google.android.gms".equals(context.getPackageName())) {
            this.zzfxt = str;
        } else {
            String valueOf = String.valueOf(zzjno);
            String valueOf2 = String.valueOf(str);
            this.zzfxt = valueOf2.length() != 0 ? valueOf.concat(valueOf2) : new String(valueOf);
        }
        this.zzjnp = ((PowerManager) context.getSystemService("power")).newWakeLock(i, str);
        if (zzw.zzcp(this.mContext)) {
            if (zzs.zzgl(str3)) {
                str3 = context.getPackageName();
            }
            this.zzjnq = zzw.zzad(context, str3);
            WorkSource workSource = this.zzjnq;
            if (workSource != null && zzw.zzcp(this.mContext)) {
                if (this.zzjnq != null) {
                    this.zzjnq.add(workSource);
                } else {
                    this.zzjnq = workSource;
                }
                try {
                    this.zzjnp.setWorkSource(this.zzjnq);
                } catch (IllegalArgumentException e) {
                    Log.wtf(TAG, e.toString());
                }
            }
        }
    }

    private final String zzh(String str, boolean z) {
        return this.zzjnt ? z ? null : this.zzjns : this.zzjns;
    }

    private final boolean zzlb(String str) {
        if (TextUtils.isEmpty(null)) {
            return false;
        }
        String str2 = this.zzjns;
        throw new NullPointerException();
    }

    /* JADX WARNING: inconsistent code. */
    /* Code decompiled incorrectly, please refer to instructions dump. */
    public final void acquire(long r13) {
        /*
        r12 = this;
        r10 = 1000; // 0x3e8 float:1.401E-42 double:4.94E-321;
        r1 = 0;
        r0 = r12.zzlb(r1);
        r4 = r12.zzh(r1, r0);
        monitor-enter(r12);
        r1 = r12.zzjnu;	 Catch:{ all -> 0x0061 }
        if (r1 > 0) goto L_0x0014;
    L_0x0010:
        r1 = r12.zzjnv;	 Catch:{ all -> 0x0061 }
        if (r1 <= 0) goto L_0x0022;
    L_0x0014:
        r1 = r12.zzjnp;	 Catch:{ all -> 0x0061 }
        r1 = r1.isHeld();	 Catch:{ all -> 0x0061 }
        if (r1 != 0) goto L_0x0022;
    L_0x001c:
        r1 = 0;
        r12.zzjnu = r1;	 Catch:{ all -> 0x0061 }
        r1 = 0;
        r12.zzjnv = r1;	 Catch:{ all -> 0x0061 }
    L_0x0022:
        r1 = r12.zzjnt;	 Catch:{ all -> 0x0061 }
        if (r1 == 0) goto L_0x0030;
    L_0x0026:
        r1 = r12.zzjnu;	 Catch:{ all -> 0x0061 }
        r2 = r1 + 1;
        r12.zzjnu = r2;	 Catch:{ all -> 0x0061 }
        if (r1 == 0) goto L_0x0038;
    L_0x002e:
        if (r0 != 0) goto L_0x0038;
    L_0x0030:
        r0 = r12.zzjnt;	 Catch:{ all -> 0x0061 }
        if (r0 != 0) goto L_0x005a;
    L_0x0034:
        r0 = r12.zzjnv;	 Catch:{ all -> 0x0061 }
        if (r0 != 0) goto L_0x005a;
    L_0x0038:
        com.google.android.gms.common.stats.zze.zzalb();	 Catch:{ all -> 0x0061 }
        r0 = r12.mContext;	 Catch:{ all -> 0x0061 }
        r1 = r12.zzjnp;	 Catch:{ all -> 0x0061 }
        r1 = com.google.android.gms.common.stats.zzc.zza(r1, r4);	 Catch:{ all -> 0x0061 }
        r2 = 7;
        r3 = r12.zzfxt;	 Catch:{ all -> 0x0061 }
        r5 = 0;
        r6 = r12.zzjnr;	 Catch:{ all -> 0x0061 }
        r7 = r12.zzjnq;	 Catch:{ all -> 0x0061 }
        r7 = com.google.android.gms.common.util.zzw.zzb(r7);	 Catch:{ all -> 0x0061 }
        r8 = 1000; // 0x3e8 float:1.401E-42 double:4.94E-321;
        com.google.android.gms.common.stats.zze.zza(r0, r1, r2, r3, r4, r5, r6, r7, r8);	 Catch:{ all -> 0x0061 }
        r0 = r12.zzjnv;	 Catch:{ all -> 0x0061 }
        r0 = r0 + 1;
        r12.zzjnv = r0;	 Catch:{ all -> 0x0061 }
    L_0x005a:
        monitor-exit(r12);	 Catch:{ all -> 0x0061 }
        r0 = r12.zzjnp;
        r0.acquire(r10);
        return;
    L_0x0061:
        r0 = move-exception;
        monitor-exit(r12);	 Catch:{ all -> 0x0061 }
        throw r0;
        */
        throw new UnsupportedOperationException("Method not decompiled: com.google.android.gms.internal.zzcqb.acquire(long):void");
    }

    public final boolean isHeld() {
        return this.zzjnp.isHeld();
    }

    /* JADX WARNING: inconsistent code. */
    /* Code decompiled incorrectly, please refer to instructions dump. */
    public final void release() {
        /*
        r8 = this;
        r1 = 0;
        r0 = r8.zzlb(r1);
        r4 = r8.zzh(r1, r0);
        monitor-enter(r8);
        r1 = r8.zzjnt;	 Catch:{ all -> 0x0049 }
        if (r1 == 0) goto L_0x0018;
    L_0x000e:
        r1 = r8.zzjnu;	 Catch:{ all -> 0x0049 }
        r1 = r1 + -1;
        r8.zzjnu = r1;	 Catch:{ all -> 0x0049 }
        if (r1 == 0) goto L_0x0021;
    L_0x0016:
        if (r0 != 0) goto L_0x0021;
    L_0x0018:
        r0 = r8.zzjnt;	 Catch:{ all -> 0x0049 }
        if (r0 != 0) goto L_0x0042;
    L_0x001c:
        r0 = r8.zzjnv;	 Catch:{ all -> 0x0049 }
        r1 = 1;
        if (r0 != r1) goto L_0x0042;
    L_0x0021:
        com.google.android.gms.common.stats.zze.zzalb();	 Catch:{ all -> 0x0049 }
        r0 = r8.mContext;	 Catch:{ all -> 0x0049 }
        r1 = r8.zzjnp;	 Catch:{ all -> 0x0049 }
        r1 = com.google.android.gms.common.stats.zzc.zza(r1, r4);	 Catch:{ all -> 0x0049 }
        r2 = 8;
        r3 = r8.zzfxt;	 Catch:{ all -> 0x0049 }
        r5 = 0;
        r6 = r8.zzjnr;	 Catch:{ all -> 0x0049 }
        r7 = r8.zzjnq;	 Catch:{ all -> 0x0049 }
        r7 = com.google.android.gms.common.util.zzw.zzb(r7);	 Catch:{ all -> 0x0049 }
        com.google.android.gms.common.stats.zze.zza(r0, r1, r2, r3, r4, r5, r6, r7);	 Catch:{ all -> 0x0049 }
        r0 = r8.zzjnv;	 Catch:{ all -> 0x0049 }
        r0 = r0 + -1;
        r8.zzjnv = r0;	 Catch:{ all -> 0x0049 }
    L_0x0042:
        monitor-exit(r8);	 Catch:{ all -> 0x0049 }
        r0 = r8.zzjnp;
        r0.release();
        return;
    L_0x0049:
        r0 = move-exception;
        monitor-exit(r8);	 Catch:{ all -> 0x0049 }
        throw r0;
        */
        throw new UnsupportedOperationException("Method not decompiled: com.google.android.gms.internal.zzcqb.release():void");
    }

    public final void setReferenceCounted(boolean z) {
        this.zzjnp.setReferenceCounted(false);
        this.zzjnt = false;
    }
}
