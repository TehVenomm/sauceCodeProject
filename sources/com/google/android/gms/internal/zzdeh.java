package com.google.android.gms.internal;

import android.content.ContentResolver;
import android.database.Cursor;
import android.net.Uri;
import java.util.ArrayList;
import java.util.Arrays;
import java.util.HashMap;
import java.util.HashSet;
import java.util.Map;
import java.util.TreeMap;
import java.util.concurrent.atomic.AtomicBoolean;
import java.util.regex.Pattern;

public class zzdeh {
    private static Uri CONTENT_URI = Uri.parse("content://com.google.android.gsf.gservices");
    private static Uri zzkvo = Uri.parse("content://com.google.android.gsf.gservices/prefix");
    private static Pattern zzkvp = Pattern.compile("^(1|true|t|on|yes|y)$", 2);
    private static Pattern zzkvq = Pattern.compile("^(0|false|f|off|no|n)$", 2);
    private static final AtomicBoolean zzkvr = new AtomicBoolean();
    private static HashMap<String, String> zzkvs;
    private static HashMap<String, Boolean> zzkvt = new HashMap();
    private static HashMap<String, Integer> zzkvu = new HashMap();
    private static HashMap<String, Long> zzkvv = new HashMap();
    private static HashMap<String, Float> zzkvw = new HashMap();
    private static Object zzkvx;
    private static boolean zzkvy;
    private static String[] zzkvz = new String[0];

    public static long getLong(ContentResolver contentResolver, String str, long j) {
        Object zzb = zzb(contentResolver);
        Long l = (Long) zza(zzkvv, str, Long.valueOf(0));
        if (l != null) {
            return l.longValue();
        }
        Object obj;
        long j2;
        String zza = zza(contentResolver, str, null);
        if (zza == null) {
            obj = l;
            j2 = 0;
        } else {
            try {
                j2 = Long.parseLong(zza);
                obj = Long.valueOf(j2);
            } catch (NumberFormatException e) {
                Long l2 = l;
                j2 = 0;
            }
        }
        HashMap hashMap = zzkvv;
        synchronized (zzdeh.class) {
            try {
                if (zzb == zzkvx) {
                    hashMap.put(str, obj);
                    zzkvs.remove(str);
                }
            } catch (Throwable th) {
                Class cls = zzdeh.class;
            }
        }
        return j2;
    }

    private static <T> T zza(HashMap<String, T> hashMap, String str, T t) {
        synchronized (zzdeh.class) {
            try {
                if (hashMap.containsKey(str)) {
                    T t2 = hashMap.get(str);
                    if (t2 == null) {
                        t2 = t;
                    }
                    return t2;
                }
                return null;
            } catch (Throwable th) {
                Class cls = zzdeh.class;
            }
        }
    }

    public static String zza(ContentResolver contentResolver, String str, String str2) {
        String str3 = null;
        synchronized (zzdeh.class) {
            try {
                zza(contentResolver);
                Object obj = zzkvx;
                String str4;
                if (zzkvs.containsKey(str)) {
                    str4 = (String) zzkvs.get(str);
                    if (str4 != null) {
                        str3 = str4;
                    }
                } else {
                    String[] strArr = zzkvz;
                    int length = strArr.length;
                    int i = 0;
                    while (i < length) {
                        if (str.startsWith(strArr[i])) {
                            if (!zzkvy || zzkvs.isEmpty()) {
                                zzc(contentResolver, zzkvz);
                                if (zzkvs.containsKey(str)) {
                                    str4 = (String) zzkvs.get(str);
                                    if (str4 != null) {
                                        str3 = str4;
                                    }
                                }
                            }
                        } else {
                            i++;
                        }
                    }
                    Cursor query = contentResolver.query(CONTENT_URI, null, null, new String[]{str}, null);
                    if (query != null) {
                        try {
                            if (query.moveToFirst()) {
                                str4 = query.getString(1);
                                if (str4 != null && str4.equals(null)) {
                                    str4 = null;
                                }
                                zza(obj, str, str4);
                                if (str4 != null) {
                                    str3 = str4;
                                }
                                if (query != null) {
                                    query.close();
                                }
                            }
                        } catch (Throwable th) {
                            if (query != null) {
                                query.close();
                            }
                        }
                    }
                    zza(obj, str, null);
                    if (query != null) {
                        query.close();
                    }
                }
            } catch (Throwable th2) {
                Class cls = zzdeh.class;
            }
        }
        return str3;
    }

    private static Map<String, String> zza(ContentResolver contentResolver, String... strArr) {
        Cursor query = contentResolver.query(zzkvo, null, null, strArr, null);
        Map<String, String> treeMap = new TreeMap();
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
        if (zzkvs == null) {
            zzkvr.set(false);
            zzkvs = new HashMap();
            zzkvx = new Object();
            zzkvy = false;
            contentResolver.registerContentObserver(CONTENT_URI, true, new zzdei(null));
        } else if (zzkvr.getAndSet(false)) {
            zzkvs.clear();
            zzkvt.clear();
            zzkvu.clear();
            zzkvv.clear();
            zzkvw.clear();
            zzkvx = new Object();
            zzkvy = false;
        }
    }

    private static void zza(Object obj, String str, String str2) {
        synchronized (zzdeh.class) {
            try {
                if (obj == zzkvx) {
                    zzkvs.put(str, str2);
                }
            } catch (Throwable th) {
                Class cls = zzdeh.class;
            }
        }
    }

    private static Object zzb(ContentResolver contentResolver) {
        Object obj;
        synchronized (zzdeh.class) {
            try {
                zza(contentResolver);
                obj = zzkvx;
            } catch (Throwable th) {
                Class cls = zzdeh.class;
            }
        }
        return obj;
    }

    public static void zzb(ContentResolver contentResolver, String... strArr) {
        if (strArr.length != 0) {
            synchronized (zzdeh.class) {
                try {
                    String[] strArr2;
                    zza(contentResolver);
                    HashSet hashSet = new HashSet((((zzkvz.length + strArr.length) << 2) / 3) + 1);
                    hashSet.addAll(Arrays.asList(zzkvz));
                    ArrayList arrayList = new ArrayList();
                    for (Object obj : strArr) {
                        if (hashSet.add(obj)) {
                            arrayList.add(obj);
                        }
                    }
                    if (arrayList.isEmpty()) {
                        strArr2 = new String[0];
                    } else {
                        zzkvz = (String[]) hashSet.toArray(new String[hashSet.size()]);
                        strArr2 = (String[]) arrayList.toArray(new String[arrayList.size()]);
                    }
                    if (!zzkvy || zzkvs.isEmpty()) {
                        zzc(contentResolver, zzkvz);
                    } else if (strArr2.length != 0) {
                        zzc(contentResolver, strArr2);
                    }
                } catch (Throwable th) {
                    Class cls = zzdeh.class;
                }
            }
        }
    }

    private static void zzc(ContentResolver contentResolver, String[] strArr) {
        zzkvs.putAll(zza(contentResolver, strArr));
        zzkvy = true;
    }
}
