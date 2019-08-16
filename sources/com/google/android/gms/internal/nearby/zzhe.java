package com.google.android.gms.internal.nearby;

import android.content.ContentResolver;
import android.database.Cursor;
import android.net.Uri;
import android.util.Log;
import java.util.HashMap;
import java.util.Map;
import java.util.TreeMap;
import java.util.concurrent.atomic.AtomicBoolean;
import java.util.regex.Pattern;

public class zzhe {
    private static final Uri CONTENT_URI = Uri.parse("content://com.google.android.gsf.gservices");
    private static final Uri zzjn = Uri.parse("content://com.google.android.gsf.gservices/prefix");
    private static final Pattern zzjo = Pattern.compile("^(1|true|t|on|yes|y)$", 2);
    private static final Pattern zzjp = Pattern.compile("^(0|false|f|off|no|n)$", 2);
    /* access modifiers changed from: private */
    public static final AtomicBoolean zzjq = new AtomicBoolean();
    private static HashMap<String, String> zzjr;
    private static final HashMap<String, Boolean> zzjs = new HashMap<>();
    private static final HashMap<String, Integer> zzjt = new HashMap<>();
    private static final HashMap<String, Long> zzju = new HashMap<>();
    private static final HashMap<String, Float> zzjv = new HashMap<>();
    private static Object zzjw;
    private static boolean zzjx;
    private static String[] zzjy = new String[0];

    private static <T> T zza(HashMap<String, T> hashMap, String str, T t) {
        synchronized (zzhe.class) {
            try {
                if (!hashMap.containsKey(str)) {
                    return null;
                }
                T t2 = hashMap.get(str);
                if (t2 == null) {
                    t2 = t;
                }
                return t2;
            } finally {
                Class<zzhe> cls = zzhe.class;
            }
        }
    }

    /* JADX WARNING: Code restructure failed: missing block: B:16:0x0032, code lost:
        if (zzjx == false) goto L_0x003c;
     */
    /* JADX WARNING: Code restructure failed: missing block: B:18:0x003a, code lost:
        if (zzjr.isEmpty() == false) goto L_0x0066;
     */
    /* JADX WARNING: Code restructure failed: missing block: B:19:0x003c, code lost:
        zzjr.putAll(zza(r8, zzjy));
        zzjx = true;
     */
    /* JADX WARNING: Code restructure failed: missing block: B:20:0x0050, code lost:
        if (zzjr.containsKey(r9) == false) goto L_0x0066;
     */
    /* JADX WARNING: Code restructure failed: missing block: B:21:0x0052, code lost:
        r0 = (java.lang.String) zzjr.get(r9);
     */
    /* JADX WARNING: Code restructure failed: missing block: B:22:0x005a, code lost:
        if (r0 == null) goto L_0x005d;
     */
    /* JADX WARNING: Code restructure failed: missing block: B:23:0x005c, code lost:
        r2 = r0;
     */
    /* Code decompiled incorrectly, please refer to instructions dump. */
    private static java.lang.String zza(android.content.ContentResolver r8, java.lang.String r9, java.lang.String r10) {
        /*
            r3 = 0
            r7 = 1
            r2 = 0
            java.lang.Class<com.google.android.gms.internal.nearby.zzhe> r0 = com.google.android.gms.internal.nearby.zzhe.class
            monitor-enter(r0)
            zza(r8)     // Catch:{ all -> 0x0061 }
            java.lang.Object r6 = zzjw     // Catch:{ all -> 0x0061 }
            java.util.HashMap<java.lang.String, java.lang.String> r0 = zzjr     // Catch:{ all -> 0x0061 }
            boolean r0 = r0.containsKey(r9)     // Catch:{ all -> 0x0061 }
            if (r0 == 0) goto L_0x0022
            java.util.HashMap<java.lang.String, java.lang.String> r0 = zzjr     // Catch:{ all -> 0x0061 }
            java.lang.Object r0 = r0.get(r9)     // Catch:{ all -> 0x0061 }
            java.lang.String r0 = (java.lang.String) r0     // Catch:{ all -> 0x0061 }
            if (r0 == 0) goto L_0x001e
            r2 = r0
        L_0x001e:
            java.lang.Class<com.google.android.gms.internal.nearby.zzhe> r0 = com.google.android.gms.internal.nearby.zzhe.class
            monitor-exit(r0)     // Catch:{ all -> 0x0061 }
        L_0x0021:
            return r2
        L_0x0022:
            java.lang.String[] r1 = zzjy     // Catch:{ all -> 0x0061 }
            int r4 = r1.length     // Catch:{ all -> 0x0061 }
            r0 = r3
        L_0x0026:
            if (r0 >= r4) goto L_0x006d
            r5 = r1[r0]     // Catch:{ all -> 0x0061 }
            boolean r5 = r9.startsWith(r5)     // Catch:{ all -> 0x0061 }
            if (r5 == 0) goto L_0x006a
            boolean r0 = zzjx     // Catch:{ all -> 0x0061 }
            if (r0 == 0) goto L_0x003c
            java.util.HashMap<java.lang.String, java.lang.String> r0 = zzjr     // Catch:{ all -> 0x0061 }
            boolean r0 = r0.isEmpty()     // Catch:{ all -> 0x0061 }
            if (r0 == 0) goto L_0x0066
        L_0x003c:
            java.lang.String[] r0 = zzjy     // Catch:{ all -> 0x0061 }
            java.util.HashMap<java.lang.String, java.lang.String> r1 = zzjr     // Catch:{ all -> 0x0061 }
            java.util.Map r0 = zza(r8, r0)     // Catch:{ all -> 0x0061 }
            r1.putAll(r0)     // Catch:{ all -> 0x0061 }
            r0 = 1
            zzjx = r0     // Catch:{ all -> 0x0061 }
            java.util.HashMap<java.lang.String, java.lang.String> r0 = zzjr     // Catch:{ all -> 0x0061 }
            boolean r0 = r0.containsKey(r9)     // Catch:{ all -> 0x0061 }
            if (r0 == 0) goto L_0x0066
            java.util.HashMap<java.lang.String, java.lang.String> r0 = zzjr     // Catch:{ all -> 0x0061 }
            java.lang.Object r0 = r0.get(r9)     // Catch:{ all -> 0x0061 }
            java.lang.String r0 = (java.lang.String) r0     // Catch:{ all -> 0x0061 }
            if (r0 == 0) goto L_0x005d
            r2 = r0
        L_0x005d:
            java.lang.Class<com.google.android.gms.internal.nearby.zzhe> r0 = com.google.android.gms.internal.nearby.zzhe.class
            monitor-exit(r0)     // Catch:{ all -> 0x0061 }
            goto L_0x0021
        L_0x0061:
            r0 = move-exception
            java.lang.Class<com.google.android.gms.internal.nearby.zzhe> r1 = com.google.android.gms.internal.nearby.zzhe.class
            monitor-exit(r1)     // Catch:{ all -> 0x0061 }
            throw r0
        L_0x0066:
            java.lang.Class<com.google.android.gms.internal.nearby.zzhe> r0 = com.google.android.gms.internal.nearby.zzhe.class
            monitor-exit(r0)     // Catch:{ all -> 0x0061 }
            goto L_0x0021
        L_0x006a:
            int r0 = r0 + 1
            goto L_0x0026
        L_0x006d:
            java.lang.Class<com.google.android.gms.internal.nearby.zzhe> r0 = com.google.android.gms.internal.nearby.zzhe.class
            monitor-exit(r0)     // Catch:{ all -> 0x0061 }
            android.net.Uri r1 = CONTENT_URI
            java.lang.String[] r4 = new java.lang.String[r7]
            r4[r3] = r9
            r0 = r8
            r3 = r2
            r5 = r2
            android.database.Cursor r1 = r0.query(r1, r2, r3, r4, r5)
            if (r1 == 0) goto L_0x0085
            boolean r0 = r1.moveToFirst()     // Catch:{ all -> 0x00ab }
            if (r0 != 0) goto L_0x008f
        L_0x0085:
            r0 = 0
            zza(r6, r9, r0)     // Catch:{ all -> 0x00ab }
            if (r1 == 0) goto L_0x0021
            r1.close()
            goto L_0x0021
        L_0x008f:
            r0 = 1
            java.lang.String r0 = r1.getString(r0)     // Catch:{ all -> 0x00ab }
            if (r0 == 0) goto L_0x009e
            r3 = 0
            boolean r3 = r0.equals(r3)     // Catch:{ all -> 0x00ab }
            if (r3 == 0) goto L_0x009e
            r0 = r2
        L_0x009e:
            zza(r6, r9, r0)     // Catch:{ all -> 0x00ab }
            if (r0 == 0) goto L_0x00a4
            r2 = r0
        L_0x00a4:
            if (r1 == 0) goto L_0x0021
            r1.close()
            goto L_0x0021
        L_0x00ab:
            r0 = move-exception
            if (r1 == 0) goto L_0x00b1
            r1.close()
        L_0x00b1:
            throw r0
        */
        throw new UnsupportedOperationException("Method not decompiled: com.google.android.gms.internal.nearby.zzhe.zza(android.content.ContentResolver, java.lang.String, java.lang.String):java.lang.String");
    }

    private static Map<String, String> zza(ContentResolver contentResolver, String... strArr) {
        Cursor query = contentResolver.query(zzjn, null, null, strArr, null);
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
        if (zzjr == null) {
            zzjq.set(false);
            zzjr = new HashMap<>();
            zzjw = new Object();
            zzjx = false;
            contentResolver.registerContentObserver(CONTENT_URI, true, new zzhf(null));
        } else if (zzjq.getAndSet(false)) {
            zzjr.clear();
            zzjs.clear();
            zzjt.clear();
            zzju.clear();
            zzjv.clear();
            zzjw = new Object();
            zzjx = false;
        }
    }

    private static void zza(Object obj, String str, String str2) {
        synchronized (zzhe.class) {
            try {
                if (obj == zzjw) {
                    zzjr.put(str, str2);
                }
            } finally {
                Class<zzhe> cls = zzhe.class;
            }
        }
    }

    public static boolean zza(ContentResolver contentResolver, String str, boolean z) {
        boolean z2 = true;
        Object zzb = zzb(contentResolver);
        Boolean bool = (Boolean) zza(zzjs, str, (T) Boolean.valueOf(true));
        if (bool != null) {
            return bool.booleanValue();
        }
        String zza = zza(contentResolver, str, (String) null);
        if (zza != null && !zza.equals("")) {
            if (zzjo.matcher(zza).matches()) {
                bool = Boolean.valueOf(true);
            } else if (zzjp.matcher(zza).matches()) {
                bool = Boolean.valueOf(false);
                z2 = false;
            } else {
                Log.w("Gservices", "attempt to read gservices key " + str + " (value \"" + zza + "\") as boolean");
            }
        }
        HashMap<String, Boolean> hashMap = zzjs;
        synchronized (zzhe.class) {
            try {
                if (zzb == zzjw) {
                    hashMap.put(str, bool);
                    zzjr.remove(str);
                }
            } finally {
                Class<zzhe> cls = zzhe.class;
            }
        }
        return z2;
    }

    private static Object zzb(ContentResolver contentResolver) {
        Object obj;
        synchronized (zzhe.class) {
            try {
                zza(contentResolver);
                obj = zzjw;
            } catch (Throwable th) {
                Class<zzhe> cls = zzhe.class;
                throw th;
            }
        }
        return obj;
    }
}
