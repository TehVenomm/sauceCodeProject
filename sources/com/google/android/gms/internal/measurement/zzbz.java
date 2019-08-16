package com.google.android.gms.internal.measurement;

import android.content.ContentResolver;
import android.database.Cursor;
import android.net.Uri;
import java.util.HashMap;
import java.util.Map;
import java.util.TreeMap;
import java.util.concurrent.atomic.AtomicBoolean;
import java.util.regex.Pattern;

public class zzbz {
    public static final Uri CONTENT_URI = Uri.parse("content://com.google.android.gsf.gservices");
    private static final HashMap<String, Boolean> zzaaa = new HashMap<>();
    private static final HashMap<String, Integer> zzaab = new HashMap<>();
    private static final HashMap<String, Long> zzaac = new HashMap<>();
    private static final HashMap<String, Float> zzaad = new HashMap<>();
    private static Object zzaae;
    private static boolean zzaaf;
    private static String[] zzaag = new String[0];
    private static final Uri zzzv = Uri.parse("content://com.google.android.gsf.gservices/prefix");
    public static final Pattern zzzw = Pattern.compile("^(1|true|t|on|yes|y)$", 2);
    public static final Pattern zzzx = Pattern.compile("^(0|false|f|off|no|n)$", 2);
    /* access modifiers changed from: private */
    public static final AtomicBoolean zzzy = new AtomicBoolean();
    private static HashMap<String, String> zzzz;

    /* JADX WARNING: Code restructure failed: missing block: B:16:0x0032, code lost:
        if (zzaaf == false) goto L_0x003c;
     */
    /* JADX WARNING: Code restructure failed: missing block: B:18:0x003a, code lost:
        if (zzzz.isEmpty() == false) goto L_0x0066;
     */
    /* JADX WARNING: Code restructure failed: missing block: B:19:0x003c, code lost:
        zzzz.putAll(zza(r8, zzaag));
        zzaaf = true;
     */
    /* JADX WARNING: Code restructure failed: missing block: B:20:0x0050, code lost:
        if (zzzz.containsKey(r9) == false) goto L_0x0066;
     */
    /* JADX WARNING: Code restructure failed: missing block: B:21:0x0052, code lost:
        r0 = (java.lang.String) zzzz.get(r9);
     */
    /* JADX WARNING: Code restructure failed: missing block: B:22:0x005a, code lost:
        if (r0 == null) goto L_0x005d;
     */
    /* JADX WARNING: Code restructure failed: missing block: B:23:0x005c, code lost:
        r2 = r0;
     */
    /* Code decompiled incorrectly, please refer to instructions dump. */
    public static java.lang.String zza(android.content.ContentResolver r8, java.lang.String r9, java.lang.String r10) {
        /*
            r3 = 0
            r7 = 1
            r2 = 0
            java.lang.Class<com.google.android.gms.internal.measurement.zzbz> r0 = com.google.android.gms.internal.measurement.zzbz.class
            monitor-enter(r0)
            zza(r8)     // Catch:{ all -> 0x0061 }
            java.lang.Object r6 = zzaae     // Catch:{ all -> 0x0061 }
            java.util.HashMap<java.lang.String, java.lang.String> r0 = zzzz     // Catch:{ all -> 0x0061 }
            boolean r0 = r0.containsKey(r9)     // Catch:{ all -> 0x0061 }
            if (r0 == 0) goto L_0x0022
            java.util.HashMap<java.lang.String, java.lang.String> r0 = zzzz     // Catch:{ all -> 0x0061 }
            java.lang.Object r0 = r0.get(r9)     // Catch:{ all -> 0x0061 }
            java.lang.String r0 = (java.lang.String) r0     // Catch:{ all -> 0x0061 }
            if (r0 == 0) goto L_0x001e
            r2 = r0
        L_0x001e:
            java.lang.Class<com.google.android.gms.internal.measurement.zzbz> r0 = com.google.android.gms.internal.measurement.zzbz.class
            monitor-exit(r0)     // Catch:{ all -> 0x0061 }
        L_0x0021:
            return r2
        L_0x0022:
            java.lang.String[] r1 = zzaag     // Catch:{ all -> 0x0061 }
            int r4 = r1.length     // Catch:{ all -> 0x0061 }
            r0 = r3
        L_0x0026:
            if (r0 >= r4) goto L_0x006d
            r5 = r1[r0]     // Catch:{ all -> 0x0061 }
            boolean r5 = r9.startsWith(r5)     // Catch:{ all -> 0x0061 }
            if (r5 == 0) goto L_0x006a
            boolean r0 = zzaaf     // Catch:{ all -> 0x0061 }
            if (r0 == 0) goto L_0x003c
            java.util.HashMap<java.lang.String, java.lang.String> r0 = zzzz     // Catch:{ all -> 0x0061 }
            boolean r0 = r0.isEmpty()     // Catch:{ all -> 0x0061 }
            if (r0 == 0) goto L_0x0066
        L_0x003c:
            java.lang.String[] r0 = zzaag     // Catch:{ all -> 0x0061 }
            java.util.HashMap<java.lang.String, java.lang.String> r1 = zzzz     // Catch:{ all -> 0x0061 }
            java.util.Map r0 = zza(r8, r0)     // Catch:{ all -> 0x0061 }
            r1.putAll(r0)     // Catch:{ all -> 0x0061 }
            r0 = 1
            zzaaf = r0     // Catch:{ all -> 0x0061 }
            java.util.HashMap<java.lang.String, java.lang.String> r0 = zzzz     // Catch:{ all -> 0x0061 }
            boolean r0 = r0.containsKey(r9)     // Catch:{ all -> 0x0061 }
            if (r0 == 0) goto L_0x0066
            java.util.HashMap<java.lang.String, java.lang.String> r0 = zzzz     // Catch:{ all -> 0x0061 }
            java.lang.Object r0 = r0.get(r9)     // Catch:{ all -> 0x0061 }
            java.lang.String r0 = (java.lang.String) r0     // Catch:{ all -> 0x0061 }
            if (r0 == 0) goto L_0x005d
            r2 = r0
        L_0x005d:
            java.lang.Class<com.google.android.gms.internal.measurement.zzbz> r0 = com.google.android.gms.internal.measurement.zzbz.class
            monitor-exit(r0)     // Catch:{ all -> 0x0061 }
            goto L_0x0021
        L_0x0061:
            r0 = move-exception
            java.lang.Class<com.google.android.gms.internal.measurement.zzbz> r1 = com.google.android.gms.internal.measurement.zzbz.class
            monitor-exit(r1)     // Catch:{ all -> 0x0061 }
            throw r0
        L_0x0066:
            java.lang.Class<com.google.android.gms.internal.measurement.zzbz> r0 = com.google.android.gms.internal.measurement.zzbz.class
            monitor-exit(r0)     // Catch:{ all -> 0x0061 }
            goto L_0x0021
        L_0x006a:
            int r0 = r0 + 1
            goto L_0x0026
        L_0x006d:
            java.lang.Class<com.google.android.gms.internal.measurement.zzbz> r0 = com.google.android.gms.internal.measurement.zzbz.class
            monitor-exit(r0)     // Catch:{ all -> 0x0061 }
            android.net.Uri r1 = CONTENT_URI
            java.lang.String[] r4 = new java.lang.String[r7]
            r4[r3] = r9
            r0 = r8
            r3 = r2
            r5 = r2
            android.database.Cursor r1 = r0.query(r1, r2, r3, r4, r5)
            if (r1 != 0) goto L_0x0085
            if (r1 == 0) goto L_0x0021
            r1.close()
            goto L_0x0021
        L_0x0085:
            boolean r0 = r1.moveToFirst()     // Catch:{ all -> 0x00b1 }
            if (r0 != 0) goto L_0x0095
            r0 = 0
            zza(r6, r9, r0)     // Catch:{ all -> 0x00b1 }
            if (r1 == 0) goto L_0x0021
            r1.close()
            goto L_0x0021
        L_0x0095:
            r0 = 1
            java.lang.String r0 = r1.getString(r0)     // Catch:{ all -> 0x00b1 }
            if (r0 == 0) goto L_0x00a4
            r3 = 0
            boolean r3 = r0.equals(r3)     // Catch:{ all -> 0x00b1 }
            if (r3 == 0) goto L_0x00a4
            r0 = r2
        L_0x00a4:
            zza(r6, r9, r0)     // Catch:{ all -> 0x00b1 }
            if (r0 == 0) goto L_0x00aa
            r2 = r0
        L_0x00aa:
            if (r1 == 0) goto L_0x0021
            r1.close()
            goto L_0x0021
        L_0x00b1:
            r0 = move-exception
            if (r1 == 0) goto L_0x00b7
            r1.close()
        L_0x00b7:
            throw r0
        */
        throw new UnsupportedOperationException("Method not decompiled: com.google.android.gms.internal.measurement.zzbz.zza(android.content.ContentResolver, java.lang.String, java.lang.String):java.lang.String");
    }

    private static Map<String, String> zza(ContentResolver contentResolver, String... strArr) {
        Cursor query = contentResolver.query(zzzv, null, null, strArr, null);
        TreeMap treeMap = new TreeMap();
        if (query != null) {
            while (query.moveToNext()) {
                try {
                    treeMap.put(query.getString(0), query.getString(1));
                } finally {
                    query.close();
                }
            }
        }
        return treeMap;
    }

    private static void zza(ContentResolver contentResolver) {
        if (zzzz == null) {
            zzzy.set(false);
            zzzz = new HashMap<>();
            zzaae = new Object();
            zzaaf = false;
            contentResolver.registerContentObserver(CONTENT_URI, true, new zzby(null));
        } else if (zzzy.getAndSet(false)) {
            zzzz.clear();
            zzaaa.clear();
            zzaab.clear();
            zzaac.clear();
            zzaad.clear();
            zzaae = new Object();
            zzaaf = false;
        }
    }

    private static void zza(Object obj, String str, String str2) {
        synchronized (zzbz.class) {
            try {
                if (obj == zzaae) {
                    zzzz.put(str, str2);
                }
            } finally {
                Class<zzbz> cls = zzbz.class;
            }
        }
    }
}
