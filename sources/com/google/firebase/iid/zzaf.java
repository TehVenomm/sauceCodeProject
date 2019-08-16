package com.google.firebase.iid;

final /* synthetic */ class zzaf implements Runnable {
    private final zzae zzcc;

    zzaf(zzae zzae) {
        this.zzcc = zzae;
    }

    /* JADX WARNING: Code restructure failed: missing block: B:18:0x0043, code lost:
        if (android.util.Log.isLoggable("MessengerIpcClient", 3) == false) goto L_0x006b;
     */
    /* JADX WARNING: Code restructure failed: missing block: B:19:0x0045, code lost:
        r2 = java.lang.String.valueOf(r0);
        android.util.Log.d("MessengerIpcClient", new java.lang.StringBuilder(java.lang.String.valueOf(r2).length() + 8).append("Sending ").append(r2).toString());
     */
    /* JADX WARNING: Code restructure failed: missing block: B:20:0x006b, code lost:
        r2 = r1.zzch.zzag;
        r3 = r1.zzcd;
        r4 = android.os.Message.obtain();
        r4.what = r0.what;
        r4.arg1 = r0.zzck;
        r4.replyTo = r3;
        r3 = new android.os.Bundle();
        r3.putBoolean("oneWay", r0.zzab());
        r3.putString("pkg", r2.getPackageName());
        r3.putBundle(com.facebook.share.internal.ShareConstants.WEB_DIALOG_PARAM_DATA, r0.zzcm);
        r4.setData(r3);
     */
    /* JADX WARNING: Code restructure failed: missing block: B:22:?, code lost:
        r1.zzce.send(r4);
     */
    /* JADX WARNING: Code restructure failed: missing block: B:23:0x00a9, code lost:
        r0 = move-exception;
     */
    /* JADX WARNING: Code restructure failed: missing block: B:24:0x00aa, code lost:
        r1.zza(2, r0.getMessage());
     */
    /* Code decompiled incorrectly, please refer to instructions dump. */
    public final void run() {
        /*
            r8 = this;
            r7 = 2
            com.google.firebase.iid.zzae r1 = r8.zzcc
        L_0x0003:
            monitor-enter(r1)
            int r0 = r1.state     // Catch:{ all -> 0x0017 }
            if (r0 == r7) goto L_0x000a
            monitor-exit(r1)     // Catch:{ all -> 0x0017 }
        L_0x0009:
            return
        L_0x000a:
            java.util.Queue<com.google.firebase.iid.zzaj<?>> r0 = r1.zzcf     // Catch:{ all -> 0x0017 }
            boolean r0 = r0.isEmpty()     // Catch:{ all -> 0x0017 }
            if (r0 == 0) goto L_0x001a
            r1.zzz()     // Catch:{ all -> 0x0017 }
            monitor-exit(r1)     // Catch:{ all -> 0x0017 }
            goto L_0x0009
        L_0x0017:
            r0 = move-exception
            monitor-exit(r1)     // Catch:{ all -> 0x0017 }
            throw r0
        L_0x001a:
            java.util.Queue<com.google.firebase.iid.zzaj<?>> r0 = r1.zzcf     // Catch:{ all -> 0x0017 }
            java.lang.Object r0 = r0.poll()     // Catch:{ all -> 0x0017 }
            com.google.firebase.iid.zzaj r0 = (com.google.firebase.iid.zzaj) r0     // Catch:{ all -> 0x0017 }
            android.util.SparseArray<com.google.firebase.iid.zzaj<?>> r2 = r1.zzcg     // Catch:{ all -> 0x0017 }
            int r3 = r0.zzck     // Catch:{ all -> 0x0017 }
            r2.put(r3, r0)     // Catch:{ all -> 0x0017 }
            com.google.firebase.iid.zzac r2 = r1.zzch     // Catch:{ all -> 0x0017 }
            java.util.concurrent.ScheduledExecutorService r2 = r2.zzbz     // Catch:{ all -> 0x0017 }
            com.google.firebase.iid.zzai r3 = new com.google.firebase.iid.zzai     // Catch:{ all -> 0x0017 }
            r3.<init>(r1, r0)     // Catch:{ all -> 0x0017 }
            r4 = 30
            java.util.concurrent.TimeUnit r6 = java.util.concurrent.TimeUnit.SECONDS     // Catch:{ all -> 0x0017 }
            r2.schedule(r3, r4, r6)     // Catch:{ all -> 0x0017 }
            monitor-exit(r1)     // Catch:{ all -> 0x0017 }
            java.lang.String r2 = "MessengerIpcClient"
            r3 = 3
            boolean r2 = android.util.Log.isLoggable(r2, r3)
            if (r2 == 0) goto L_0x006b
            java.lang.String r2 = java.lang.String.valueOf(r0)
            java.lang.String r3 = "MessengerIpcClient"
            java.lang.StringBuilder r4 = new java.lang.StringBuilder
            java.lang.String r5 = java.lang.String.valueOf(r2)
            int r5 = r5.length()
            int r5 = r5 + 8
            r4.<init>(r5)
            java.lang.String r5 = "Sending "
            java.lang.StringBuilder r4 = r4.append(r5)
            java.lang.StringBuilder r2 = r4.append(r2)
            java.lang.String r2 = r2.toString()
            android.util.Log.d(r3, r2)
        L_0x006b:
            com.google.firebase.iid.zzac r2 = r1.zzch
            android.content.Context r2 = r2.zzag
            android.os.Messenger r3 = r1.zzcd
            android.os.Message r4 = android.os.Message.obtain()
            int r5 = r0.what
            r4.what = r5
            int r5 = r0.zzck
            r4.arg1 = r5
            r4.replyTo = r3
            android.os.Bundle r3 = new android.os.Bundle
            r3.<init>()
            java.lang.String r5 = "oneWay"
            boolean r6 = r0.zzab()
            r3.putBoolean(r5, r6)
            java.lang.String r5 = "pkg"
            java.lang.String r2 = r2.getPackageName()
            r3.putString(r5, r2)
            java.lang.String r2 = "data"
            android.os.Bundle r0 = r0.zzcm
            r3.putBundle(r2, r0)
            r4.setData(r3)
            com.google.firebase.iid.zzah r0 = r1.zzce     // Catch:{ RemoteException -> 0x00a9 }
            r0.send(r4)     // Catch:{ RemoteException -> 0x00a9 }
            goto L_0x0003
        L_0x00a9:
            r0 = move-exception
            java.lang.String r0 = r0.getMessage()
            r1.zza(r7, r0)
            goto L_0x0003
        */
        throw new UnsupportedOperationException("Method not decompiled: com.google.firebase.iid.zzaf.run():void");
    }
}
