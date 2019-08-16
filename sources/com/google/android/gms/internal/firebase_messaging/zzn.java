package com.google.android.gms.internal.firebase_messaging;

public final class zzn {
    private static final zzm zzk;
    private static final int zzl;

    static final class zza extends zzm {
        zza() {
        }

        public final void zza(Throwable th, Throwable th2) {
        }
    }

    /* JADX WARNING: Removed duplicated region for block: B:10:0x0018  */
    /* JADX WARNING: Removed duplicated region for block: B:25:0x006f  */
    static {
        /*
            r2 = 1
            java.lang.Integer r0 = zzb()     // Catch:{ Throwable -> 0x0074 }
            if (r0 == 0) goto L_0x001c
            int r1 = r0.intValue()     // Catch:{ Throwable -> 0x002d }
            r3 = 19
            if (r1 < r3) goto L_0x001c
            com.google.android.gms.internal.firebase_messaging.zzr r1 = new com.google.android.gms.internal.firebase_messaging.zzr     // Catch:{ Throwable -> 0x002d }
            r1.<init>()     // Catch:{ Throwable -> 0x002d }
        L_0x0014:
            zzk = r1
            if (r0 != 0) goto L_0x006f
            r0 = r2
        L_0x0019:
            zzl = r0
            return
        L_0x001c:
            java.lang.String r1 = "com.google.devtools.build.android.desugar.runtime.twr_disable_mimic"
            boolean r1 = java.lang.Boolean.getBoolean(r1)     // Catch:{ Throwable -> 0x002d }
            if (r1 != 0) goto L_0x0067
            r1 = r2
        L_0x0025:
            if (r1 == 0) goto L_0x0069
            com.google.android.gms.internal.firebase_messaging.zzq r1 = new com.google.android.gms.internal.firebase_messaging.zzq     // Catch:{ Throwable -> 0x002d }
            r1.<init>()     // Catch:{ Throwable -> 0x002d }
            goto L_0x0014
        L_0x002d:
            r1 = move-exception
        L_0x002e:
            java.io.PrintStream r3 = java.lang.System.err
            java.lang.Class<com.google.android.gms.internal.firebase_messaging.zzn$zza> r4 = com.google.android.gms.internal.firebase_messaging.zzn.zza.class
            java.lang.String r4 = r4.getName()
            java.lang.StringBuilder r5 = new java.lang.StringBuilder
            java.lang.String r6 = java.lang.String.valueOf(r4)
            int r6 = r6.length()
            int r6 = r6 + 133
            r5.<init>(r6)
            java.lang.String r6 = "An error has occurred when initializing the try-with-resources desuguring strategy. The default strategy "
            java.lang.StringBuilder r5 = r5.append(r6)
            java.lang.StringBuilder r4 = r5.append(r4)
            java.lang.String r5 = "will be used. The error is: "
            java.lang.StringBuilder r4 = r4.append(r5)
            java.lang.String r4 = r4.toString()
            r3.println(r4)
            java.io.PrintStream r3 = java.lang.System.err
            r1.printStackTrace(r3)
            com.google.android.gms.internal.firebase_messaging.zzn$zza r1 = new com.google.android.gms.internal.firebase_messaging.zzn$zza
            r1.<init>()
            goto L_0x0014
        L_0x0067:
            r1 = 0
            goto L_0x0025
        L_0x0069:
            com.google.android.gms.internal.firebase_messaging.zzn$zza r1 = new com.google.android.gms.internal.firebase_messaging.zzn$zza     // Catch:{ Throwable -> 0x002d }
            r1.<init>()     // Catch:{ Throwable -> 0x002d }
            goto L_0x0014
        L_0x006f:
            int r0 = r0.intValue()
            goto L_0x0019
        L_0x0074:
            r1 = move-exception
            r0 = 0
            goto L_0x002e
        */
        throw new UnsupportedOperationException("Method not decompiled: com.google.android.gms.internal.firebase_messaging.zzn.<clinit>():void");
    }

    public static void zza(Throwable th, Throwable th2) {
        zzk.zza(th, th2);
    }

    private static Integer zzb() {
        try {
            return (Integer) Class.forName("android.os.Build$VERSION").getField("SDK_INT").get(null);
        } catch (Exception e) {
            System.err.println("Failed to retrieve value from android.os.Build$VERSION.SDK_INT due to the following exception.");
            e.printStackTrace(System.err);
            return null;
        }
    }
}
