package com.google.firebase.iid;

import android.support.annotation.GuardedBy;
import android.support.annotation.Nullable;
import android.support.p000v4.util.ArrayMap;
import android.text.TextUtils;
import com.google.android.gms.tasks.Task;
import com.google.android.gms.tasks.TaskCompletionSource;
import java.util.Map;

final class zzaz {
    @GuardedBy("itself")
    private final zzav zzar;
    @GuardedBy("this")
    private int zzdp = 0;
    @GuardedBy("this")
    private final Map<Integer, TaskCompletionSource<Void>> zzdq = new ArrayMap();

    zzaz(zzav zzav) {
        this.zzar = zzav;
    }

    /* JADX WARNING: Removed duplicated region for block: B:19:0x0050  */
    /* JADX WARNING: Removed duplicated region for block: B:25:0x006e  */
    @android.support.annotation.WorkerThread
    /* Code decompiled incorrectly, please refer to instructions dump. */
    private static boolean zza(com.google.firebase.iid.FirebaseInstanceId r6, java.lang.String r7) {
        /*
            r0 = 1
            r1 = 0
            java.lang.String r2 = "!"
            java.lang.String[] r2 = r7.split(r2)
            int r3 = r2.length
            r4 = 2
            if (r3 != r4) goto L_0x001b
            r3 = r2[r1]
            r4 = r2[r0]
            r2 = -1
            int r5 = r3.hashCode()     // Catch:{ IOException -> 0x0041 }
            switch(r5) {
                case 83: goto L_0x001c;
                case 84: goto L_0x0018;
                case 85: goto L_0x0026;
                default: goto L_0x0018;
            }     // Catch:{ IOException -> 0x0041 }
        L_0x0018:
            switch(r2) {
                case 0: goto L_0x0030;
                case 1: goto L_0x005d;
                default: goto L_0x001b;
            }     // Catch:{ IOException -> 0x0041 }
        L_0x001b:
            return r0
        L_0x001c:
            java.lang.String r5 = "S"
            boolean r3 = r3.equals(r5)     // Catch:{ IOException -> 0x0041 }
            if (r3 == 0) goto L_0x0018
            r2 = r1
            goto L_0x0018
        L_0x0026:
            java.lang.String r5 = "U"
            boolean r3 = r3.equals(r5)     // Catch:{ IOException -> 0x0041 }
            if (r3 == 0) goto L_0x0018
            r2 = r0
            goto L_0x0018
        L_0x0030:
            r6.zzb(r4)     // Catch:{ IOException -> 0x0041 }
            boolean r2 = com.google.firebase.iid.FirebaseInstanceId.zzm()     // Catch:{ IOException -> 0x0041 }
            if (r2 == 0) goto L_0x001b
            java.lang.String r2 = "FirebaseInstanceId"
            java.lang.String r3 = "subscribe operation succeeded"
            android.util.Log.d(r2, r3)     // Catch:{ IOException -> 0x0041 }
            goto L_0x001b
        L_0x0041:
            r0 = move-exception
            java.lang.String r0 = r0.getMessage()
            java.lang.String r0 = java.lang.String.valueOf(r0)
            int r2 = r0.length()
            if (r2 == 0) goto L_0x006e
            java.lang.String r2 = "Topic sync failed: "
            java.lang.String r0 = r2.concat(r0)
        L_0x0056:
            java.lang.String r2 = "FirebaseInstanceId"
            android.util.Log.e(r2, r0)
            r0 = r1
            goto L_0x001b
        L_0x005d:
            r6.zzc(r4)     // Catch:{ IOException -> 0x0041 }
            boolean r2 = com.google.firebase.iid.FirebaseInstanceId.zzm()     // Catch:{ IOException -> 0x0041 }
            if (r2 == 0) goto L_0x001b
            java.lang.String r2 = "FirebaseInstanceId"
            java.lang.String r3 = "unsubscribe operation succeeded"
            android.util.Log.d(r2, r3)     // Catch:{ IOException -> 0x0041 }
            goto L_0x001b
        L_0x006e:
            java.lang.String r0 = new java.lang.String
            java.lang.String r2 = "Topic sync failed: "
            r0.<init>(r2)
            goto L_0x0056
        */
        throw new UnsupportedOperationException("Method not decompiled: com.google.firebase.iid.zzaz.zza(com.google.firebase.iid.FirebaseInstanceId, java.lang.String):boolean");
    }

    @Nullable
    @GuardedBy("this")
    private final String zzap() {
        String zzai;
        synchronized (this.zzar) {
            zzai = this.zzar.zzai();
        }
        if (!TextUtils.isEmpty(zzai)) {
            String[] split = zzai.split(",");
            if (split.length > 1 && !TextUtils.isEmpty(split[1])) {
                return split[1];
            }
        }
        return null;
    }

    private final boolean zzk(String str) {
        boolean z;
        synchronized (this) {
            synchronized (this.zzar) {
                String zzai = this.zzar.zzai();
                String valueOf = String.valueOf(",");
                String valueOf2 = String.valueOf(str);
                if (zzai.startsWith(valueOf2.length() != 0 ? valueOf.concat(valueOf2) : new String(valueOf))) {
                    String valueOf3 = String.valueOf(",");
                    String valueOf4 = String.valueOf(str);
                    this.zzar.zzf(zzai.substring((valueOf4.length() != 0 ? valueOf3.concat(valueOf4) : new String(valueOf3)).length()));
                    z = true;
                } else {
                    z = false;
                }
            }
        }
        return z;
    }

    /* access modifiers changed from: 0000 */
    public final Task<Void> zza(String str) {
        String zzai;
        Task<Void> task;
        synchronized (this) {
            synchronized (this.zzar) {
                zzai = this.zzar.zzai();
                this.zzar.zzf(new StringBuilder(String.valueOf(zzai).length() + 1 + String.valueOf(str).length()).append(zzai).append(",").append(str).toString());
            }
            TaskCompletionSource taskCompletionSource = new TaskCompletionSource();
            this.zzdq.put(Integer.valueOf((TextUtils.isEmpty(zzai) ? 0 : zzai.split(",").length - 1) + this.zzdp), taskCompletionSource);
            task = taskCompletionSource.getTask();
        }
        return task;
    }

    /* access modifiers changed from: 0000 */
    public final boolean zzao() {
        boolean z;
        synchronized (this) {
            z = zzap() != null;
        }
        return z;
    }

    /* access modifiers changed from: 0000 */
    /* JADX WARNING: Code restructure failed: missing block: B:11:0x001c, code lost:
        if (zza(r4, r1) != false) goto L_0x0023;
     */
    /* JADX WARNING: Code restructure failed: missing block: B:17:0x0023, code lost:
        monitor-enter(r3);
     */
    /* JADX WARNING: Code restructure failed: missing block: B:19:?, code lost:
        r0 = (com.google.android.gms.tasks.TaskCompletionSource) r3.zzdq.remove(java.lang.Integer.valueOf(r3.zzdp));
        zzk(r1);
        r3.zzdp++;
     */
    /* JADX WARNING: Code restructure failed: missing block: B:20:0x003b, code lost:
        monitor-exit(r3);
     */
    /* JADX WARNING: Code restructure failed: missing block: B:21:0x003c, code lost:
        if (r0 == null) goto L_0x0000;
     */
    /* JADX WARNING: Code restructure failed: missing block: B:22:0x003e, code lost:
        r0.setResult(null);
     */
    /* JADX WARNING: Code restructure failed: missing block: B:39:?, code lost:
        return false;
     */
    @android.support.annotation.WorkerThread
    /* Code decompiled incorrectly, please refer to instructions dump. */
    public final boolean zzc(com.google.firebase.iid.FirebaseInstanceId r4) {
        /*
            r3 = this;
        L_0x0000:
            monitor-enter(r3)
            java.lang.String r1 = r3.zzap()     // Catch:{ all -> 0x0020 }
            if (r1 != 0) goto L_0x0017
            boolean r0 = com.google.firebase.iid.FirebaseInstanceId.zzm()     // Catch:{ all -> 0x0020 }
            if (r0 == 0) goto L_0x0014
            java.lang.String r0 = "FirebaseInstanceId"
            java.lang.String r1 = "topic sync succeeded"
            android.util.Log.d(r0, r1)     // Catch:{ all -> 0x0020 }
        L_0x0014:
            r0 = 1
            monitor-exit(r3)     // Catch:{ all -> 0x0020 }
        L_0x0016:
            return r0
        L_0x0017:
            monitor-exit(r3)     // Catch:{ all -> 0x0020 }
            boolean r0 = zza(r4, r1)
            if (r0 != 0) goto L_0x0023
            r0 = 0
            goto L_0x0016
        L_0x0020:
            r0 = move-exception
            monitor-exit(r3)     // Catch:{ all -> 0x0020 }
            throw r0
        L_0x0023:
            monitor-enter(r3)
            java.util.Map<java.lang.Integer, com.google.android.gms.tasks.TaskCompletionSource<java.lang.Void>> r0 = r3.zzdq     // Catch:{ all -> 0x0043 }
            int r2 = r3.zzdp     // Catch:{ all -> 0x0043 }
            java.lang.Integer r2 = java.lang.Integer.valueOf(r2)     // Catch:{ all -> 0x0043 }
            java.lang.Object r0 = r0.remove(r2)     // Catch:{ all -> 0x0043 }
            com.google.android.gms.tasks.TaskCompletionSource r0 = (com.google.android.gms.tasks.TaskCompletionSource) r0     // Catch:{ all -> 0x0043 }
            r3.zzk(r1)     // Catch:{ all -> 0x0043 }
            int r1 = r3.zzdp     // Catch:{ all -> 0x0043 }
            int r1 = r1 + 1
            r3.zzdp = r1     // Catch:{ all -> 0x0043 }
            monitor-exit(r3)     // Catch:{ all -> 0x0043 }
            if (r0 == 0) goto L_0x0000
            r1 = 0
            r0.setResult(r1)
            goto L_0x0000
        L_0x0043:
            r0 = move-exception
            monitor-exit(r3)     // Catch:{ all -> 0x0043 }
            throw r0
        */
        throw new UnsupportedOperationException("Method not decompiled: com.google.firebase.iid.zzaz.zzc(com.google.firebase.iid.FirebaseInstanceId):boolean");
    }
}
