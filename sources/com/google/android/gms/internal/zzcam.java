package com.google.android.gms.internal;

import android.content.ContentValues;
import android.database.sqlite.SQLiteException;
import android.support.annotation.WorkerThread;
import android.support.v4.util.ArrayMap;
import android.text.TextUtils;
import com.google.android.gms.common.internal.zzbp;
import java.io.IOException;
import java.math.BigDecimal;
import java.util.ArrayList;
import java.util.Arrays;
import java.util.BitSet;
import java.util.HashSet;
import java.util.List;
import java.util.Locale;
import java.util.Map;
import java.util.Set;
import java.util.regex.Pattern;
import java.util.regex.PatternSyntaxException;

final class zzcam extends zzcdm {
    zzcam(zzcco zzcco) {
        super(zzcco);
    }

    private final Boolean zza(double d, zzcfs zzcfs) {
        try {
            return zza(new BigDecimal(d), zzcfs, Math.ulp(d));
        } catch (NumberFormatException e) {
            return null;
        }
    }

    private final Boolean zza(long j, zzcfs zzcfs) {
        try {
            return zza(new BigDecimal(j), zzcfs, 0.0d);
        } catch (NumberFormatException e) {
            return null;
        }
    }

    private final Boolean zza(zzcfq zzcfq, zzcfz zzcfz, long j) {
        Boolean zza;
        if (zzcfq.zzixm != null) {
            zza = zza(j, zzcfq.zzixm);
            if (zza == null) {
                return null;
            }
            if (!zza.booleanValue()) {
                return Boolean.valueOf(false);
            }
        }
        Set hashSet = new HashSet();
        for (zzcfr zzcfr : zzcfq.zzixk) {
            if (TextUtils.isEmpty(zzcfr.zzixr)) {
                zzauk().zzaye().zzj("null or empty param name in filter. event", zzauf().zzjc(zzcfz.name));
                return null;
            }
            hashSet.add(zzcfr.zzixr);
        }
        Map arrayMap = new ArrayMap();
        for (zzcga zzcga : zzcfz.zziys) {
            if (hashSet.contains(zzcga.name)) {
                if (zzcga.zziyw != null) {
                    arrayMap.put(zzcga.name, zzcga.zziyw);
                } else if (zzcga.zziwx != null) {
                    arrayMap.put(zzcga.name, zzcga.zziwx);
                } else if (zzcga.zzfwi != null) {
                    arrayMap.put(zzcga.name, zzcga.zzfwi);
                } else {
                    zzauk().zzaye().zze("Unknown value for param. event, param", zzauf().zzjc(zzcfz.name), zzauf().zzjd(zzcga.name));
                    return null;
                }
            }
        }
        for (zzcfr zzcfr2 : zzcfq.zzixk) {
            boolean equals = Boolean.TRUE.equals(zzcfr2.zzixq);
            String str = zzcfr2.zzixr;
            if (TextUtils.isEmpty(str)) {
                zzauk().zzaye().zzj("Event has empty param name. event", zzauf().zzjc(zzcfz.name));
                return null;
            }
            Object obj = arrayMap.get(str);
            if (obj instanceof Long) {
                if (zzcfr2.zzixp == null) {
                    zzauk().zzaye().zze("No number filter for long param. event, param", zzauf().zzjc(zzcfz.name), zzauf().zzjd(str));
                    return null;
                }
                zza = zza(((Long) obj).longValue(), zzcfr2.zzixp);
                if (zza == null) {
                    return null;
                }
                if (((!zza.booleanValue() ? 1 : 0) ^ equals) != 0) {
                    return Boolean.valueOf(false);
                }
            } else if (obj instanceof Double) {
                if (zzcfr2.zzixp == null) {
                    zzauk().zzaye().zze("No number filter for double param. event, param", zzauf().zzjc(zzcfz.name), zzauf().zzjd(str));
                    return null;
                }
                zza = zza(((Double) obj).doubleValue(), zzcfr2.zzixp);
                if (zza == null) {
                    return null;
                }
                if (((!zza.booleanValue() ? 1 : 0) ^ equals) != 0) {
                    return Boolean.valueOf(false);
                }
            } else if (obj instanceof String) {
                if (zzcfr2.zzixo != null) {
                    zza = zza((String) obj, zzcfr2.zzixo);
                } else if (zzcfr2.zzixp == null) {
                    zzauk().zzaye().zze("No filter for String param. event, param", zzauf().zzjc(zzcfz.name), zzauf().zzjd(str));
                    return null;
                } else if (zzcfo.zzkf((String) obj)) {
                    zza = zza((String) obj, zzcfr2.zzixp);
                } else {
                    zzauk().zzaye().zze("Invalid param value for number filter. event, param", zzauf().zzjc(zzcfz.name), zzauf().zzjd(str));
                    return null;
                }
                if (zza == null) {
                    return null;
                }
                if (((!zza.booleanValue() ? 1 : 0) ^ equals) != 0) {
                    return Boolean.valueOf(false);
                }
            } else if (obj == null) {
                zzauk().zzayi().zze("Missing param for filter. event, param", zzauf().zzjc(zzcfz.name), zzauf().zzjd(str));
                return Boolean.valueOf(false);
            } else {
                zzauk().zzaye().zze("Unknown param type. event, param", zzauf().zzjc(zzcfz.name), zzauf().zzjd(str));
                return null;
            }
        }
        return Boolean.valueOf(true);
    }

    private static Boolean zza(Boolean bool, boolean z) {
        return bool == null ? null : Boolean.valueOf(bool.booleanValue() ^ z);
    }

    private final Boolean zza(String str, int i, boolean z, String str2, List<String> list, String str3) {
        Boolean bool = null;
        if (str == null) {
            return bool;
        }
        if (i == 6) {
            if (list == null || list.size() == 0) {
                return bool;
            }
        } else if (str2 == null) {
            return bool;
        }
        if (!(z || i == 1)) {
            str = str.toUpperCase(Locale.ENGLISH);
        }
        switch (i) {
            case 1:
                try {
                    return Boolean.valueOf(Pattern.compile(str3, z ? 0 : 66).matcher(str).matches());
                } catch (PatternSyntaxException e) {
                    zzauk().zzaye().zzj("Invalid regular expression in REGEXP audience filter. expression", str3);
                    return bool;
                }
            case 2:
                return Boolean.valueOf(str.startsWith(str2));
            case 3:
                return Boolean.valueOf(str.endsWith(str2));
            case 4:
                return Boolean.valueOf(str.contains(str2));
            case 5:
                return Boolean.valueOf(str.equals(str2));
            case 6:
                return Boolean.valueOf(list.contains(str));
            default:
                return bool;
        }
    }

    private final Boolean zza(String str, zzcfs zzcfs) {
        Boolean bool = null;
        if (zzcfo.zzkf(str)) {
            try {
                bool = zza(new BigDecimal(str), zzcfs, 0.0d);
            } catch (NumberFormatException e) {
            }
        }
        return bool;
    }

    private final Boolean zza(String str, zzcfu zzcfu) {
        int i = 0;
        String str2 = null;
        zzbp.zzu(zzcfu);
        if (str == null || zzcfu.zziya == null || zzcfu.zziya.intValue() == 0) {
            return null;
        }
        List list;
        if (zzcfu.zziya.intValue() == 6) {
            if (zzcfu.zziyd == null || zzcfu.zziyd.length == 0) {
                return null;
            }
        } else if (zzcfu.zziyb == null) {
            return null;
        }
        int intValue = zzcfu.zziya.intValue();
        boolean z = zzcfu.zziyc != null && zzcfu.zziyc.booleanValue();
        String toUpperCase = (z || intValue == 1 || intValue == 6) ? zzcfu.zziyb : zzcfu.zziyb.toUpperCase(Locale.ENGLISH);
        if (zzcfu.zziyd == null) {
            list = null;
        } else {
            String[] strArr = zzcfu.zziyd;
            if (z) {
                list = Arrays.asList(strArr);
            } else {
                list = new ArrayList();
                int length = strArr.length;
                while (i < length) {
                    list.add(strArr[i].toUpperCase(Locale.ENGLISH));
                    i++;
                }
            }
        }
        if (intValue == 1) {
            str2 = toUpperCase;
        }
        return zza(str, intValue, z, toUpperCase, list, str2);
    }

    /* JADX WARNING: inconsistent code. */
    /* Code decompiled incorrectly, please refer to instructions dump. */
    private static java.lang.Boolean zza(java.math.BigDecimal r10, com.google.android.gms.internal.zzcfs r11, double r12) {
        /*
        r8 = 4;
        r7 = -1;
        r0 = 0;
        r1 = 1;
        r2 = 0;
        com.google.android.gms.common.internal.zzbp.zzu(r11);
        r3 = r11.zzixs;
        if (r3 == 0) goto L_0x0014;
    L_0x000c:
        r3 = r11.zzixs;
        r3 = r3.intValue();
        if (r3 != 0) goto L_0x0016;
    L_0x0014:
        r0 = r2;
    L_0x0015:
        return r0;
    L_0x0016:
        r3 = r11.zzixs;
        r3 = r3.intValue();
        if (r3 != r8) goto L_0x0028;
    L_0x001e:
        r3 = r11.zzixv;
        if (r3 == 0) goto L_0x0026;
    L_0x0022:
        r3 = r11.zzixw;
        if (r3 != 0) goto L_0x002e;
    L_0x0026:
        r0 = r2;
        goto L_0x0015;
    L_0x0028:
        r3 = r11.zzixu;
        if (r3 != 0) goto L_0x002e;
    L_0x002c:
        r0 = r2;
        goto L_0x0015;
    L_0x002e:
        r3 = r11.zzixs;
        r6 = r3.intValue();
        r3 = r11.zzixs;
        r3 = r3.intValue();
        if (r3 != r8) goto L_0x0067;
    L_0x003c:
        r3 = r11.zzixv;
        r3 = com.google.android.gms.internal.zzcfo.zzkf(r3);
        if (r3 == 0) goto L_0x004c;
    L_0x0044:
        r3 = r11.zzixw;
        r3 = com.google.android.gms.internal.zzcfo.zzkf(r3);
        if (r3 != 0) goto L_0x004e;
    L_0x004c:
        r0 = r2;
        goto L_0x0015;
    L_0x004e:
        r4 = new java.math.BigDecimal;	 Catch:{ NumberFormatException -> 0x0064 }
        r3 = r11.zzixv;	 Catch:{ NumberFormatException -> 0x0064 }
        r4.<init>(r3);	 Catch:{ NumberFormatException -> 0x0064 }
        r3 = new java.math.BigDecimal;	 Catch:{ NumberFormatException -> 0x0064 }
        r5 = r11.zzixw;	 Catch:{ NumberFormatException -> 0x0064 }
        r3.<init>(r5);	 Catch:{ NumberFormatException -> 0x0064 }
        r5 = r4;
        r4 = r2;
    L_0x005e:
        if (r6 != r8) goto L_0x007f;
    L_0x0060:
        if (r5 != 0) goto L_0x0081;
    L_0x0062:
        r0 = r2;
        goto L_0x0015;
    L_0x0064:
        r0 = move-exception;
        r0 = r2;
        goto L_0x0015;
    L_0x0067:
        r3 = r11.zzixu;
        r3 = com.google.android.gms.internal.zzcfo.zzkf(r3);
        if (r3 != 0) goto L_0x0071;
    L_0x006f:
        r0 = r2;
        goto L_0x0015;
    L_0x0071:
        r3 = new java.math.BigDecimal;	 Catch:{ NumberFormatException -> 0x007c }
        r4 = r11.zzixu;	 Catch:{ NumberFormatException -> 0x007c }
        r3.<init>(r4);	 Catch:{ NumberFormatException -> 0x007c }
        r4 = r3;
        r5 = r2;
        r3 = r2;
        goto L_0x005e;
    L_0x007c:
        r0 = move-exception;
        r0 = r2;
        goto L_0x0015;
    L_0x007f:
        if (r4 == 0) goto L_0x0084;
    L_0x0081:
        switch(r6) {
            case 1: goto L_0x0086;
            case 2: goto L_0x0092;
            case 3: goto L_0x00a0;
            case 4: goto L_0x00ee;
            default: goto L_0x0084;
        };
    L_0x0084:
        r0 = r2;
        goto L_0x0015;
    L_0x0086:
        r2 = r10.compareTo(r4);
        if (r2 != r7) goto L_0x008d;
    L_0x008c:
        r0 = r1;
    L_0x008d:
        r0 = java.lang.Boolean.valueOf(r0);
        goto L_0x0015;
    L_0x0092:
        r2 = r10.compareTo(r4);
        if (r2 != r1) goto L_0x009e;
    L_0x0098:
        r0 = java.lang.Boolean.valueOf(r1);
        goto L_0x0015;
    L_0x009e:
        r1 = r0;
        goto L_0x0098;
    L_0x00a0:
        r2 = 0;
        r2 = (r12 > r2 ? 1 : (r12 == r2 ? 0 : -1));
        if (r2 == 0) goto L_0x00e0;
    L_0x00a6:
        r2 = new java.math.BigDecimal;
        r2.<init>(r12);
        r3 = new java.math.BigDecimal;
        r5 = 2;
        r3.<init>(r5);
        r2 = r2.multiply(r3);
        r2 = r4.subtract(r2);
        r2 = r10.compareTo(r2);
        if (r2 != r1) goto L_0x00de;
    L_0x00bf:
        r2 = new java.math.BigDecimal;
        r2.<init>(r12);
        r3 = new java.math.BigDecimal;
        r5 = 2;
        r3.<init>(r5);
        r2 = r2.multiply(r3);
        r2 = r4.add(r2);
        r2 = r10.compareTo(r2);
        if (r2 != r7) goto L_0x00de;
    L_0x00d8:
        r0 = java.lang.Boolean.valueOf(r1);
        goto L_0x0015;
    L_0x00de:
        r1 = r0;
        goto L_0x00d8;
    L_0x00e0:
        r2 = r10.compareTo(r4);
        if (r2 != 0) goto L_0x00ec;
    L_0x00e6:
        r0 = java.lang.Boolean.valueOf(r1);
        goto L_0x0015;
    L_0x00ec:
        r1 = r0;
        goto L_0x00e6;
    L_0x00ee:
        r2 = r10.compareTo(r5);
        if (r2 == r7) goto L_0x0100;
    L_0x00f4:
        r2 = r10.compareTo(r3);
        if (r2 == r1) goto L_0x0100;
    L_0x00fa:
        r0 = java.lang.Boolean.valueOf(r1);
        goto L_0x0015;
    L_0x0100:
        r1 = r0;
        goto L_0x00fa;
        */
        throw new UnsupportedOperationException("Method not decompiled: com.google.android.gms.internal.zzcam.zza(java.math.BigDecimal, com.google.android.gms.internal.zzcfs, double):java.lang.Boolean");
    }

    @WorkerThread
    final zzcfy[] zza(String str, zzcfz[] zzcfzArr, zzcge[] zzcgeArr) {
        int intValue;
        BitSet bitSet;
        BitSet bitSet2;
        Map map;
        Boolean zza;
        zzbp.zzgf(str);
        Set hashSet = new HashSet();
        ArrayMap arrayMap = new ArrayMap();
        Map arrayMap2 = new ArrayMap();
        ArrayMap arrayMap3 = new ArrayMap();
        Map zziz = zzaue().zziz(str);
        if (zziz != null) {
            for (Integer intValue2 : zziz.keySet()) {
                intValue = intValue2.intValue();
                zzcgd zzcgd = (zzcgd) zziz.get(Integer.valueOf(intValue));
                bitSet = (BitSet) arrayMap2.get(Integer.valueOf(intValue));
                bitSet2 = (BitSet) arrayMap3.get(Integer.valueOf(intValue));
                if (bitSet == null) {
                    bitSet = new BitSet();
                    arrayMap2.put(Integer.valueOf(intValue), bitSet);
                    bitSet2 = new BitSet();
                    arrayMap3.put(Integer.valueOf(intValue), bitSet2);
                }
                for (int i = 0; i < (zzcgd.zzjaa.length << 6); i++) {
                    if (zzcfo.zza(zzcgd.zzjaa, i)) {
                        zzauk().zzayi().zze("Filter already evaluated. audience ID, filter ID", Integer.valueOf(intValue), Integer.valueOf(i));
                        bitSet2.set(i);
                        if (zzcfo.zza(zzcgd.zzjab, i)) {
                            bitSet.set(i);
                        }
                    }
                }
                zzcfy zzcfy = new zzcfy();
                arrayMap.put(Integer.valueOf(intValue), zzcfy);
                zzcfy.zziyq = Boolean.valueOf(false);
                zzcfy.zziyp = zzcgd;
                zzcfy.zziyo = new zzcgd();
                zzcfy.zziyo.zzjab = zzcfo.zza(bitSet);
                zzcfy.zziyo.zzjaa = zzcfo.zza(bitSet2);
            }
        }
        if (zzcfzArr != null) {
            ArrayMap arrayMap4 = new ArrayMap();
            for (zzcfz zzcfz : zzcfzArr) {
                zzcay zzcay;
                zzcay zzah = zzaue().zzah(str, zzcfz.name);
                if (zzah == null) {
                    zzauk().zzaye().zze("Event aggregate wasn't created during raw event logging. appId, event", zzcbo.zzjf(str), zzauf().zzjc(zzcfz.name));
                    zzcay = new zzcay(str, zzcfz.name, 1, 1, zzcfz.zziyt.longValue());
                } else {
                    zzcay = zzah.zzaxx();
                }
                zzaue().zza(zzcay);
                long j = zzcay.zzind;
                map = (Map) arrayMap4.get(zzcfz.name);
                Map map2;
                if (map == null) {
                    map = zzaue().zzam(str, zzcfz.name);
                    if (map == null) {
                        map = new ArrayMap();
                    }
                    arrayMap4.put(zzcfz.name, map);
                    map2 = map;
                } else {
                    map2 = map;
                }
                for (Integer intValue22 : r9.keySet()) {
                    int intValue3 = intValue22.intValue();
                    if (hashSet.contains(Integer.valueOf(intValue3))) {
                        zzauk().zzayi().zzj("Skipping failed audience ID", Integer.valueOf(intValue3));
                    } else {
                        bitSet2 = (BitSet) arrayMap2.get(Integer.valueOf(intValue3));
                        BitSet bitSet3 = (BitSet) arrayMap3.get(Integer.valueOf(intValue3));
                        if (((zzcfy) arrayMap.get(Integer.valueOf(intValue3))) == null) {
                            zzcfy zzcfy2 = new zzcfy();
                            arrayMap.put(Integer.valueOf(intValue3), zzcfy2);
                            zzcfy2.zziyq = Boolean.valueOf(true);
                            bitSet2 = new BitSet();
                            arrayMap2.put(Integer.valueOf(intValue3), bitSet2);
                            bitSet3 = new BitSet();
                            arrayMap3.put(Integer.valueOf(intValue3), bitSet3);
                            bitSet = bitSet3;
                        } else {
                            bitSet = bitSet3;
                        }
                        for (zzcfq zzcfq : (List) r9.get(Integer.valueOf(intValue3))) {
                            if (zzauk().zzad(2)) {
                                zzauk().zzayi().zzd("Evaluating filter. audience, filter, event", Integer.valueOf(intValue3), zzcfq.zzixi, zzauf().zzjc(zzcfq.zzixj));
                                zzauk().zzayi().zzj("Filter definition", zzauf().zza(zzcfq));
                            }
                            if (zzcfq.zzixi == null || zzcfq.zzixi.intValue() > 256) {
                                zzauk().zzaye().zze("Invalid event filter ID. appId, id", zzcbo.zzjf(str), String.valueOf(zzcfq.zzixi));
                            } else if (bitSet2.get(zzcfq.zzixi.intValue())) {
                                zzauk().zzayi().zze("Event filter already evaluated true. audience ID, filter ID", Integer.valueOf(intValue3), zzcfq.zzixi);
                            } else {
                                Object obj;
                                zza = zza(zzcfq, zzcfz, j);
                                zzcbq zzayi = zzauk().zzayi();
                                if (zza == null) {
                                    obj = "null";
                                } else {
                                    Boolean bool = zza;
                                }
                                zzayi.zzj("Event filter result", obj);
                                if (zza == null) {
                                    hashSet.add(Integer.valueOf(intValue3));
                                } else {
                                    bitSet.set(zzcfq.zzixi.intValue());
                                    if (zza.booleanValue()) {
                                        bitSet2.set(zzcfq.zzixi.intValue());
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }
        if (zzcgeArr != null) {
            Map arrayMap5 = new ArrayMap();
            for (zzcge zzcge : zzcgeArr) {
                map = (Map) arrayMap5.get(zzcge.name);
                Map map3;
                if (map == null) {
                    map = zzaue().zzan(str, zzcge.name);
                    if (map == null) {
                        map = new ArrayMap();
                    }
                    arrayMap5.put(zzcge.name, map);
                    map3 = map;
                } else {
                    map3 = map;
                }
                for (Integer intValue222 : r7.keySet()) {
                    int intValue4 = intValue222.intValue();
                    if (hashSet.contains(Integer.valueOf(intValue4))) {
                        zzauk().zzayi().zzj("Skipping failed audience ID", Integer.valueOf(intValue4));
                    } else {
                        bitSet = (BitSet) arrayMap2.get(Integer.valueOf(intValue4));
                        bitSet2 = (BitSet) arrayMap3.get(Integer.valueOf(intValue4));
                        if (((zzcfy) arrayMap.get(Integer.valueOf(intValue4))) == null) {
                            zzcfy2 = new zzcfy();
                            arrayMap.put(Integer.valueOf(intValue4), zzcfy2);
                            zzcfy2.zziyq = Boolean.valueOf(true);
                            bitSet = new BitSet();
                            arrayMap2.put(Integer.valueOf(intValue4), bitSet);
                            bitSet2 = new BitSet();
                            arrayMap3.put(Integer.valueOf(intValue4), bitSet2);
                        }
                        for (zzcft zzcft : (List) r7.get(Integer.valueOf(intValue4))) {
                            if (zzauk().zzad(2)) {
                                zzauk().zzayi().zzd("Evaluating filter. audience, filter, property", Integer.valueOf(intValue4), zzcft.zzixi, zzauf().zzje(zzcft.zzixy));
                                zzauk().zzayi().zzj("Filter definition", zzauf().zza(zzcft));
                            }
                            if (zzcft.zzixi == null || zzcft.zzixi.intValue() > 256) {
                                zzauk().zzaye().zze("Invalid property filter ID. appId, id", zzcbo.zzjf(str), String.valueOf(zzcft.zzixi));
                                hashSet.add(Integer.valueOf(intValue4));
                                break;
                            } else if (bitSet.get(zzcft.zzixi.intValue())) {
                                zzauk().zzayi().zze("Property filter already evaluated true. audience ID, filter ID", Integer.valueOf(intValue4), zzcft.zzixi);
                            } else {
                                Object obj2;
                                zzcfr zzcfr = zzcft.zzixz;
                                if (zzcfr == null) {
                                    zzauk().zzaye().zzj("Missing property filter. property", zzauf().zzje(zzcge.name));
                                    zza = null;
                                } else {
                                    boolean equals = Boolean.TRUE.equals(zzcfr.zzixq);
                                    if (zzcge.zziyw != null) {
                                        if (zzcfr.zzixp == null) {
                                            zzauk().zzaye().zzj("No number filter for long property. property", zzauf().zzje(zzcge.name));
                                            zza = null;
                                        } else {
                                            zza = zza(zza(zzcge.zziyw.longValue(), zzcfr.zzixp), equals);
                                        }
                                    } else if (zzcge.zziwx != null) {
                                        if (zzcfr.zzixp == null) {
                                            zzauk().zzaye().zzj("No number filter for double property. property", zzauf().zzje(zzcge.name));
                                            zza = null;
                                        } else {
                                            zza = zza(zza(zzcge.zziwx.doubleValue(), zzcfr.zzixp), equals);
                                        }
                                    } else if (zzcge.zzfwi == null) {
                                        zzauk().zzaye().zzj("User property has no value, property", zzauf().zzje(zzcge.name));
                                        zza = null;
                                    } else if (zzcfr.zzixo == null) {
                                        if (zzcfr.zzixp == null) {
                                            zzauk().zzaye().zzj("No string or number filter defined. property", zzauf().zzje(zzcge.name));
                                        } else if (zzcfo.zzkf(zzcge.zzfwi)) {
                                            zza = zza(zza(zzcge.zzfwi, zzcfr.zzixp), equals);
                                        } else {
                                            zzauk().zzaye().zze("Invalid user property value for Numeric number filter. property, value", zzauf().zzje(zzcge.name), zzcge.zzfwi);
                                        }
                                        zza = null;
                                    } else {
                                        zza = zza(zza(zzcge.zzfwi, zzcfr.zzixo), equals);
                                    }
                                }
                                zzcbq zzayi2 = zzauk().zzayi();
                                if (zza == null) {
                                    obj2 = "null";
                                } else {
                                    Boolean bool2 = zza;
                                }
                                zzayi2.zzj("Property filter result", obj2);
                                if (zza == null) {
                                    hashSet.add(Integer.valueOf(intValue4));
                                } else {
                                    bitSet2.set(zzcft.zzixi.intValue());
                                    if (zza.booleanValue()) {
                                        bitSet.set(zzcft.zzixi.intValue());
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }
        zzcfy[] zzcfyArr = new zzcfy[arrayMap2.size()];
        int i2 = 0;
        for (Integer intValue2222 : arrayMap2.keySet()) {
            intValue = intValue2222.intValue();
            if (!hashSet.contains(Integer.valueOf(intValue))) {
                zzcfy2 = (zzcfy) arrayMap.get(Integer.valueOf(intValue));
                zzcfy = zzcfy2 == null ? new zzcfy() : zzcfy2;
                int i3 = i2 + 1;
                zzcfyArr[i2] = zzcfy;
                zzcfy.zzixe = Integer.valueOf(intValue);
                zzcfy.zziyo = new zzcgd();
                zzcfy.zziyo.zzjab = zzcfo.zza((BitSet) arrayMap2.get(Integer.valueOf(intValue)));
                zzcfy.zziyo.zzjaa = zzcfo.zza((BitSet) arrayMap3.get(Integer.valueOf(intValue)));
                zzcdl zzaue = zzaue();
                zzego zzego = zzcfy.zziyo;
                zzaue.zzwh();
                zzaue.zzug();
                zzbp.zzgf(str);
                zzbp.zzu(zzego);
                try {
                    byte[] bArr = new byte[zzego.zzbjo()];
                    zzegg zzi = zzegg.zzi(bArr, 0, bArr.length);
                    zzego.zza(zzi);
                    zzi.zzccd();
                    ContentValues contentValues = new ContentValues();
                    contentValues.put("app_id", str);
                    contentValues.put("audience_id", Integer.valueOf(intValue));
                    contentValues.put("current_results", bArr);
                    try {
                        if (zzaue.getWritableDatabase().insertWithOnConflict("audience_filter_values", null, contentValues, 5) == -1) {
                            zzaue.zzauk().zzayc().zzj("Failed to insert filter results (got -1). appId", zzcbo.zzjf(str));
                        }
                        i2 = i3;
                    } catch (SQLiteException e) {
                        zzaue.zzauk().zzayc().zze("Error storing filter results. appId", zzcbo.zzjf(str), e);
                        i2 = i3;
                    }
                } catch (IOException e2) {
                    zzaue.zzauk().zzayc().zze("Configuration loss. Failed to serialize filter results. appId", zzcbo.zzjf(str), e2);
                    i2 = i3;
                }
            }
        }
        return (zzcfy[]) Arrays.copyOf(zzcfyArr, i2);
    }

    protected final void zzuh() {
    }
}
