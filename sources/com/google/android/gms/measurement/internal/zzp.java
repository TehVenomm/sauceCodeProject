package com.google.android.gms.measurement.internal;

import android.support.p000v4.util.ArrayMap;
import com.google.android.gms.common.internal.Preconditions;
import com.google.android.gms.common.util.VisibleForTesting;
import com.google.android.gms.internal.measurement.zzbk;
import com.google.android.gms.internal.measurement.zzbk.zza;
import com.google.android.gms.internal.measurement.zzbk.zzb;
import com.google.android.gms.internal.measurement.zzbk.zzc;
import com.google.android.gms.internal.measurement.zzbk.zzd;
import com.google.android.gms.internal.measurement.zzbs;
import com.google.android.gms.internal.measurement.zzbs.zze;
import com.google.android.gms.internal.measurement.zzbs.zzk;
import com.google.android.gms.internal.measurement.zzey;
import java.math.BigDecimal;
import java.util.ArrayList;
import java.util.Collections;
import java.util.HashSet;
import java.util.List;
import java.util.Locale;
import java.util.Map;
import java.util.regex.Pattern;
import java.util.regex.PatternSyntaxException;

final class zzp extends zzjh {
    zzp(zzjg zzjg) {
        super(zzjg);
    }

    private final Boolean zza(double d, zzc zzc) {
        try {
            return zza(new BigDecimal(d), zzc, Math.ulp(d));
        } catch (NumberFormatException e) {
            return null;
        }
    }

    private final Boolean zza(long j, zzc zzc) {
        try {
            return zza(new BigDecimal(j), zzc, 0.0d);
        } catch (NumberFormatException e) {
            return null;
        }
    }

    private final Boolean zza(zza zza, String str, List<zze> list, long j) {
        Boolean zza2;
        if (zza.zzkd()) {
            Boolean zza3 = zza(j, zza.zzke());
            if (zza3 == null) {
                return null;
            }
            if (!zza3.booleanValue()) {
                return Boolean.valueOf(false);
            }
        }
        HashSet hashSet = new HashSet();
        for (zzb zzb : zza.zzkc()) {
            if (zzb.zzkr().isEmpty()) {
                zzab().zzgn().zza("null or empty param name in filter. event", zzy().zzaj(str));
                return null;
            }
            hashSet.add(zzb.zzkr());
        }
        ArrayMap arrayMap = new ArrayMap();
        for (zze zze : list) {
            if (hashSet.contains(zze.getName())) {
                if (zze.zzna()) {
                    arrayMap.put(zze.getName(), zze.zzna() ? Long.valueOf(zze.zznb()) : null);
                } else if (zze.zznd()) {
                    arrayMap.put(zze.getName(), zze.zznd() ? Double.valueOf(zze.zzne()) : null);
                } else if (zze.zzmx()) {
                    arrayMap.put(zze.getName(), zze.zzmy());
                } else {
                    zzab().zzgn().zza("Unknown value for param. event, param", zzy().zzaj(str), zzy().zzak(zze.getName()));
                    return null;
                }
            }
        }
        for (zzb zzb2 : zza.zzkc()) {
            boolean z = zzb2.zzkp() && zzb2.zzkq();
            String zzkr = zzb2.zzkr();
            if (zzkr.isEmpty()) {
                zzab().zzgn().zza("Event has empty param name. event", zzy().zzaj(str));
                return null;
            }
            Object obj = arrayMap.get(zzkr);
            if (obj instanceof Long) {
                if (!zzb2.zzkn()) {
                    zzab().zzgn().zza("No number filter for long param. event, param", zzy().zzaj(str), zzy().zzak(zzkr));
                    return null;
                }
                Boolean zza4 = zza(((Long) obj).longValue(), zzb2.zzko());
                if (zza4 == null) {
                    return null;
                }
                if (zza4.booleanValue() == z) {
                    return Boolean.valueOf(false);
                }
            } else if (obj instanceof Double) {
                if (!zzb2.zzkn()) {
                    zzab().zzgn().zza("No number filter for double param. event, param", zzy().zzaj(str), zzy().zzak(zzkr));
                    return null;
                }
                Boolean zza5 = zza(((Double) obj).doubleValue(), zzb2.zzko());
                if (zza5 == null) {
                    return null;
                }
                if (zza5.booleanValue() == z) {
                    return Boolean.valueOf(false);
                }
            } else if (obj instanceof String) {
                if (zzb2.zzkl()) {
                    zza2 = zza((String) obj, zzb2.zzkm());
                } else if (!zzb2.zzkn()) {
                    zzab().zzgn().zza("No filter for String param. event, param", zzy().zzaj(str), zzy().zzak(zzkr));
                    return null;
                } else if (zzjo.zzbj((String) obj)) {
                    zza2 = zza((String) obj, zzb2.zzko());
                } else {
                    zzab().zzgn().zza("Invalid param value for number filter. event, param", zzy().zzaj(str), zzy().zzak(zzkr));
                    return null;
                }
                if (zza2 == null) {
                    return null;
                }
                if (zza2.booleanValue() == z) {
                    return Boolean.valueOf(false);
                }
            } else if (obj == null) {
                zzab().zzgs().zza("Missing param for filter. event, param", zzy().zzaj(str), zzy().zzak(zzkr));
                return Boolean.valueOf(false);
            } else {
                zzab().zzgn().zza("Unknown param type. event, param", zzy().zzaj(str), zzy().zzak(zzkr));
                return null;
            }
        }
        return Boolean.valueOf(true);
    }

    private final Boolean zza(zzd zzd, zzk zzk) {
        zzb zzli = zzd.zzli();
        boolean zzkq = zzli.zzkq();
        if (zzk.zzna()) {
            if (zzli.zzkn()) {
                return zza(zza(zzk.zznb(), zzli.zzko()), zzkq);
            }
            zzab().zzgn().zza("No number filter for long property. property", zzy().zzal(zzk.getName()));
            return null;
        } else if (zzk.zznd()) {
            if (zzli.zzkn()) {
                return zza(zza(zzk.zzne(), zzli.zzko()), zzkq);
            }
            zzab().zzgn().zza("No number filter for double property. property", zzy().zzal(zzk.getName()));
            return null;
        } else if (!zzk.zzmx()) {
            zzab().zzgn().zza("User property has no value, property", zzy().zzal(zzk.getName()));
            return null;
        } else if (zzli.zzkl()) {
            return zza(zza(zzk.zzmy(), zzli.zzkm()), zzkq);
        } else {
            if (!zzli.zzkn()) {
                zzab().zzgn().zza("No string or number filter defined. property", zzy().zzal(zzk.getName()));
                return null;
            } else if (zzjo.zzbj(zzk.zzmy())) {
                return zza(zza(zzk.zzmy(), zzli.zzko()), zzkq);
            } else {
                zzab().zzgn().zza("Invalid user property value for Numeric number filter. property, value", zzy().zzal(zzk.getName()), zzk.zzmy());
                return null;
            }
        }
    }

    @VisibleForTesting
    private static Boolean zza(Boolean bool, boolean z) {
        if (bool == null) {
            return null;
        }
        return Boolean.valueOf(bool.booleanValue() != z);
    }

    private final Boolean zza(String str, zzc zzc) {
        Boolean bool = null;
        if (!zzjo.zzbj(str)) {
            return bool;
        }
        try {
            return zza(new BigDecimal(str), zzc, 0.0d);
        } catch (NumberFormatException e) {
            return bool;
        }
    }

    private final Boolean zza(String str, zzbk.zze.zza zza, boolean z, String str2, List<String> list, String str3) {
        Boolean bool = null;
        if (str == null) {
            return bool;
        }
        if (zza == zzbk.zze.zza.IN_LIST) {
            if (list == null || list.size() == 0) {
                return bool;
            }
        } else if (str2 == null) {
            return bool;
        }
        if (!z && zza != zzbk.zze.zza.REGEXP) {
            str = str.toUpperCase(Locale.ENGLISH);
        }
        switch (zzo.zzdu[zza.ordinal()]) {
            case 1:
                try {
                    return Boolean.valueOf(Pattern.compile(str3, z ? 0 : 66).matcher(str).matches());
                } catch (PatternSyntaxException e) {
                    zzab().zzgn().zza("Invalid regular expression in REGEXP audience filter. expression", str3);
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

    @VisibleForTesting
    private final Boolean zza(String str, zzbk.zze zze) {
        List<String> zzlq;
        String str2 = null;
        Preconditions.checkNotNull(zze);
        if (str == null || !zze.zzlk() || zze.zzll() == zzbk.zze.zza.UNKNOWN_MATCH_TYPE) {
            return null;
        }
        if (zze.zzll() == zzbk.zze.zza.IN_LIST) {
            if (zze.zzlr() == 0) {
                return null;
            }
        } else if (!zze.zzlm()) {
            return null;
        }
        zzbk.zze.zza zzll = zze.zzll();
        boolean zzlp = zze.zzlp();
        String zzln = (zzlp || zzll == zzbk.zze.zza.REGEXP || zzll == zzbk.zze.zza.IN_LIST) ? zze.zzln() : zze.zzln().toUpperCase(Locale.ENGLISH);
        if (zze.zzlr() == 0) {
            zzlq = null;
        } else {
            zzlq = zze.zzlq();
            if (!zzlp) {
                ArrayList arrayList = new ArrayList(zzlq.size());
                for (String upperCase : zzlq) {
                    arrayList.add(upperCase.toUpperCase(Locale.ENGLISH));
                }
                zzlq = Collections.unmodifiableList(arrayList);
            }
        }
        if (zzll == zzbk.zze.zza.REGEXP) {
            str2 = zzln;
        }
        return zza(str, zzll, zzlp, zzln, zzlq, str2);
    }

    @VisibleForTesting
    private static Boolean zza(BigDecimal bigDecimal, zzc zzc, double d) {
        BigDecimal bigDecimal2;
        BigDecimal bigDecimal3;
        BigDecimal bigDecimal4;
        boolean z = false;
        boolean z2 = true;
        Preconditions.checkNotNull(zzc);
        if (!zzc.zzku() || zzc.zzkv() == zzc.zzb.UNKNOWN_COMPARISON_TYPE) {
            return null;
        }
        if (zzc.zzkv() == zzc.zzb.BETWEEN) {
            if (!zzc.zzla() || !zzc.zzlc()) {
                return null;
            }
        } else if (!zzc.zzky()) {
            return null;
        }
        zzc.zzb zzkv = zzc.zzkv();
        if (zzc.zzkv() == zzc.zzb.BETWEEN) {
            if (!zzjo.zzbj(zzc.zzlb()) || !zzjo.zzbj(zzc.zzld())) {
                return null;
            }
            try {
                bigDecimal3 = new BigDecimal(zzc.zzlb());
                bigDecimal4 = new BigDecimal(zzc.zzld());
                bigDecimal2 = null;
            } catch (NumberFormatException e) {
                return null;
            }
        } else if (!zzjo.zzbj(zzc.zzkz())) {
            return null;
        } else {
            try {
                bigDecimal2 = new BigDecimal(zzc.zzkz());
                bigDecimal3 = null;
                bigDecimal4 = null;
            } catch (NumberFormatException e2) {
                return null;
            }
        }
        if (zzkv == zzc.zzb.BETWEEN) {
            if (bigDecimal3 == null) {
                return null;
            }
        } else if (bigDecimal2 == null) {
            return null;
        }
        switch (zzo.zzdv[zzkv.ordinal()]) {
            case 1:
                return Boolean.valueOf(bigDecimal.compareTo(bigDecimal2) == -1);
            case 2:
                if (bigDecimal.compareTo(bigDecimal2) == 1) {
                    z = true;
                }
                return Boolean.valueOf(z);
            case 3:
                if (d != 0.0d) {
                    if (!(bigDecimal.compareTo(bigDecimal2.subtract(new BigDecimal(d).multiply(new BigDecimal(2)))) == 1 && bigDecimal.compareTo(bigDecimal2.add(new BigDecimal(d).multiply(new BigDecimal(2)))) == -1)) {
                        z2 = false;
                    }
                    return Boolean.valueOf(z2);
                }
                if (bigDecimal.compareTo(bigDecimal2) != 0) {
                    z2 = false;
                }
                return Boolean.valueOf(z2);
            case 4:
                if (bigDecimal.compareTo(bigDecimal3) == -1 || bigDecimal.compareTo(bigDecimal4) == 1) {
                    z2 = false;
                }
                return Boolean.valueOf(z2);
            default:
                return null;
        }
    }

    private static List<zzbs.zzb> zza(Map<Integer, Long> map) {
        if (map == null) {
            return null;
        }
        ArrayList arrayList = new ArrayList(map.size());
        for (Integer intValue : map.keySet()) {
            int intValue2 = intValue.intValue();
            arrayList.add((zzbs.zzb) ((zzey) zzbs.zzb.zzmh().zzk(intValue2).zzae(((Long) map.get(Integer.valueOf(intValue2))).longValue()).zzug()));
        }
        return arrayList;
    }

    private static void zza(Map<Integer, Long> map, int i, long j) {
        Long l = (Long) map.get(Integer.valueOf(i));
        long j2 = j / 1000;
        if (l == null || j2 > l.longValue()) {
            map.put(Integer.valueOf(i), Long.valueOf(j2));
        }
    }

    private static void zzb(Map<Integer, List<Long>> map, int i, long j) {
        List list = (List) map.get(Integer.valueOf(i));
        if (list == null) {
            list = new ArrayList();
            map.put(Integer.valueOf(i), list);
        }
        list.add(Long.valueOf(j / 1000));
    }

    /* JADX WARNING: type inference failed for: r2v50 */
    /* JADX WARNING: type inference failed for: r2v61, types: [java.util.Collection] */
    /* JADX WARNING: type inference failed for: r2v62, types: [java.lang.Iterable] */
    /* JADX WARNING: type inference failed for: r4v6, types: [java.util.List, java.util.ArrayList] */
    /* JADX WARNING: type inference failed for: r2v69 */
    /* JADX WARNING: type inference failed for: r2v99, types: [java.util.List] */
    /* JADX WARNING: type inference failed for: r8v8 */
    /* JADX WARNING: type inference failed for: r8v12, types: [java.lang.String] */
    /* JADX WARNING: type inference failed for: r9v11 */
    /* JADX WARNING: type inference failed for: r9v21, types: [java.lang.String] */
    /* JADX WARNING: type inference failed for: r2v217 */
    /* JADX WARNING: type inference failed for: r2v223, types: [java.lang.String] */
    /* JADX WARNING: type inference failed for: r2v236 */
    /* JADX WARNING: type inference failed for: r2v245, types: [java.lang.String] */
    /* JADX WARNING: type inference failed for: r2v356 */
    /* JADX WARNING: type inference failed for: r8v60 */
    /* JADX WARNING: type inference failed for: r9v38 */
    /* JADX WARNING: type inference failed for: r2v357 */
    /* JADX WARNING: type inference failed for: r2v358 */
    /* access modifiers changed from: 0000 */
    /* JADX WARNING: Multi-variable type inference failed */
    /* JADX WARNING: Removed duplicated region for block: B:262:0x086b  */
    /* JADX WARNING: Removed duplicated region for block: B:376:0x0b82  */
    /* JADX WARNING: Removed duplicated region for block: B:54:0x01cd  */
    /* JADX WARNING: Removed duplicated region for block: B:96:0x0315  */
    /* JADX WARNING: Unknown variable types count: 7 */
    @android.support.annotation.WorkerThread
    /* Code decompiled incorrectly, please refer to instructions dump. */
    public final java.util.List<com.google.android.gms.internal.measurement.zzbs.zza> zza(java.lang.String r43, java.util.List<com.google.android.gms.internal.measurement.zzbs.zzc> r44, java.util.List<com.google.android.gms.internal.measurement.zzbs.zzk> r45) {
        /*
            r42 = this;
            com.google.android.gms.common.internal.Preconditions.checkNotEmpty(r43)
            com.google.android.gms.common.internal.Preconditions.checkNotNull(r44)
            com.google.android.gms.common.internal.Preconditions.checkNotNull(r45)
            java.util.HashSet r28 = new java.util.HashSet
            r28.<init>()
            android.support.v4.util.ArrayMap r29 = new android.support.v4.util.ArrayMap
            r29.<init>()
            android.support.v4.util.ArrayMap r30 = new android.support.v4.util.ArrayMap
            r30.<init>()
            android.support.v4.util.ArrayMap r31 = new android.support.v4.util.ArrayMap
            r31.<init>()
            android.support.v4.util.ArrayMap r32 = new android.support.v4.util.ArrayMap
            r32.<init>()
            android.support.v4.util.ArrayMap r33 = new android.support.v4.util.ArrayMap
            r33.<init>()
            com.google.android.gms.measurement.internal.zzs r2 = r42.zzad()
            r0 = r43
            boolean r34 = r2.zzq(r0)
            com.google.android.gms.measurement.internal.zzs r2 = r42.zzad()
            com.google.android.gms.measurement.internal.zzdu<java.lang.Boolean> r3 = com.google.android.gms.measurement.internal.zzak.zziq
            r0 = r43
            boolean r35 = r2.zzd(r0, r3)
            com.google.android.gms.measurement.internal.zzs r2 = r42.zzad()
            com.google.android.gms.measurement.internal.zzdu<java.lang.Boolean> r3 = com.google.android.gms.measurement.internal.zzak.zziy
            r0 = r43
            boolean r36 = r2.zzd(r0, r3)
            com.google.android.gms.measurement.internal.zzs r2 = r42.zzad()
            com.google.android.gms.measurement.internal.zzdu<java.lang.Boolean> r3 = com.google.android.gms.measurement.internal.zzak.zziz
            r0 = r43
            boolean r37 = r2.zzd(r0, r3)
            if (r36 != 0) goto L_0x0059
            if (r37 == 0) goto L_0x0e1f
        L_0x0059:
            java.util.Iterator r3 = r44.iterator()
        L_0x005d:
            boolean r2 = r3.hasNext()
            if (r2 == 0) goto L_0x0e1f
            java.lang.Object r2 = r3.next()
            com.google.android.gms.internal.measurement.zzbs$zzc r2 = (com.google.android.gms.internal.measurement.zzbs.zzc) r2
            java.lang.String r4 = "_s"
            java.lang.String r5 = r2.getName()
            boolean r4 = r4.equals(r5)
            if (r4 == 0) goto L_0x005d
            long r2 = r2.getTimestampMillis()
            java.lang.Long r2 = java.lang.Long.valueOf(r2)
            r27 = r2
        L_0x007f:
            if (r27 == 0) goto L_0x00b0
            if (r37 == 0) goto L_0x00b0
            com.google.android.gms.measurement.internal.zzx r3 = r42.zzgy()
            r3.zzbi()
            r3.zzo()
            com.google.android.gms.common.internal.Preconditions.checkNotEmpty(r43)
            android.content.ContentValues r2 = new android.content.ContentValues
            r2.<init>()
            java.lang.String r4 = "current_session_count"
            r5 = 0
            java.lang.Integer r5 = java.lang.Integer.valueOf(r5)
            r2.put(r4, r5)
            android.database.sqlite.SQLiteDatabase r4 = r3.getWritableDatabase()     // Catch:{ SQLiteException -> 0x012a }
            java.lang.String r5 = "events"
            java.lang.String r6 = "app_id = ?"
            r7 = 1
            java.lang.String[] r7 = new java.lang.String[r7]     // Catch:{ SQLiteException -> 0x012a }
            r8 = 0
            r7[r8] = r43     // Catch:{ SQLiteException -> 0x012a }
            r4.update(r5, r2, r6, r7)     // Catch:{ SQLiteException -> 0x012a }
        L_0x00b0:
            com.google.android.gms.measurement.internal.zzx r2 = r42.zzgy()
            r0 = r43
            java.util.Map r7 = r2.zzaf(r0)
            if (r7 == 0) goto L_0x030f
            boolean r2 = r7.isEmpty()
            if (r2 != 0) goto L_0x030f
            java.util.HashSet r8 = new java.util.HashSet
            java.util.Set r2 = r7.keySet()
            r8.<init>(r2)
            if (r36 == 0) goto L_0x01c2
            if (r27 == 0) goto L_0x01c2
            com.google.android.gms.measurement.internal.zzp r6 = r42.zzgx()
            com.google.android.gms.common.internal.Preconditions.checkNotEmpty(r43)
            com.google.android.gms.common.internal.Preconditions.checkNotNull(r7)
            android.support.v4.util.ArrayMap r5 = new android.support.v4.util.ArrayMap
            r5.<init>()
            boolean r2 = r7.isEmpty()
            if (r2 != 0) goto L_0x0e25
            com.google.android.gms.measurement.internal.zzx r2 = r6.zzgy()
            r0 = r43
            java.util.Map r9 = r2.zzae(r0)
            java.util.Set r2 = r7.keySet()
            java.util.Iterator r10 = r2.iterator()
        L_0x00f6:
            boolean r2 = r10.hasNext()
            if (r2 == 0) goto L_0x0e25
            java.lang.Object r2 = r10.next()
            java.lang.Integer r2 = (java.lang.Integer) r2
            int r11 = r2.intValue()
            java.lang.Integer r2 = java.lang.Integer.valueOf(r11)
            java.lang.Object r2 = r7.get(r2)
            com.google.android.gms.internal.measurement.zzbs$zzi r2 = (com.google.android.gms.internal.measurement.zzbs.zzi) r2
            java.lang.Integer r3 = java.lang.Integer.valueOf(r11)
            java.lang.Object r3 = r9.get(r3)
            java.util.List r3 = (java.util.List) r3
            if (r3 == 0) goto L_0x0122
            boolean r4 = r3.isEmpty()
            if (r4 == 0) goto L_0x013e
        L_0x0122:
            java.lang.Integer r3 = java.lang.Integer.valueOf(r11)
            r5.put(r3, r2)
            goto L_0x00f6
        L_0x012a:
            r2 = move-exception
            com.google.android.gms.measurement.internal.zzef r3 = r3.zzab()
            com.google.android.gms.measurement.internal.zzeh r3 = r3.zzgk()
            java.lang.String r4 = "Error resetting session-scoped event counts. appId"
            java.lang.Object r5 = com.google.android.gms.measurement.internal.zzef.zzam(r43)
            r3.zza(r4, r5, r2)
            goto L_0x00b0
        L_0x013e:
            com.google.android.gms.measurement.internal.zzjo r4 = r6.zzgw()
            java.util.List r12 = r2.zzpy()
            java.util.List r12 = r4.zza(r12, r3)
            boolean r4 = r12.isEmpty()
            if (r4 != 0) goto L_0x00f6
            com.google.android.gms.internal.measurement.zzey$zza r4 = r2.zzuj()
            com.google.android.gms.internal.measurement.zzey$zza r4 = (com.google.android.gms.internal.measurement.zzey.zza) r4
            com.google.android.gms.internal.measurement.zzbs$zzi$zza r4 = (com.google.android.gms.internal.measurement.zzbs.zzi.zza) r4
            com.google.android.gms.internal.measurement.zzbs$zzi$zza r4 = r4.zzqr()
            com.google.android.gms.internal.measurement.zzbs$zzi$zza r12 = r4.zzo(r12)
            com.google.android.gms.measurement.internal.zzjo r4 = r6.zzgw()
            java.util.List r13 = r2.zzpv()
            java.util.List r4 = r4.zza(r13, r3)
            com.google.android.gms.internal.measurement.zzbs$zzi$zza r13 = r12.zzqq()
            r13.zzn(r4)
            r4 = 0
        L_0x0174:
            int r13 = r2.zzqc()
            if (r4 >= r13) goto L_0x0192
            com.google.android.gms.internal.measurement.zzbs$zzb r13 = r2.zzae(r4)
            int r13 = r13.getIndex()
            java.lang.Integer r13 = java.lang.Integer.valueOf(r13)
            boolean r13 = r3.contains(r13)
            if (r13 == 0) goto L_0x018f
            r12.zzaj(r4)
        L_0x018f:
            int r4 = r4 + 1
            goto L_0x0174
        L_0x0192:
            r4 = 0
        L_0x0193:
            int r13 = r2.zzqf()
            if (r4 >= r13) goto L_0x01b1
            com.google.android.gms.internal.measurement.zzbs$zzj r13 = r2.zzag(r4)
            int r13 = r13.getIndex()
            java.lang.Integer r13 = java.lang.Integer.valueOf(r13)
            boolean r13 = r3.contains(r13)
            if (r13 == 0) goto L_0x01ae
            r12.zzak(r4)
        L_0x01ae:
            int r4 = r4 + 1
            goto L_0x0193
        L_0x01b1:
            java.lang.Integer r3 = java.lang.Integer.valueOf(r11)
            com.google.android.gms.internal.measurement.zzgi r2 = r12.zzug()
            com.google.android.gms.internal.measurement.zzey r2 = (com.google.android.gms.internal.measurement.zzey) r2
            com.google.android.gms.internal.measurement.zzbs$zzi r2 = (com.google.android.gms.internal.measurement.zzbs.zzi) r2
            r5.put(r3, r2)
            goto L_0x00f6
        L_0x01c2:
            r6 = r7
        L_0x01c3:
            java.util.Iterator r10 = r8.iterator()
        L_0x01c7:
            boolean r2 = r10.hasNext()
            if (r2 == 0) goto L_0x030f
            java.lang.Object r2 = r10.next()
            java.lang.Integer r2 = (java.lang.Integer) r2
            int r11 = r2.intValue()
            java.lang.Integer r2 = java.lang.Integer.valueOf(r11)
            java.lang.Object r2 = r6.get(r2)
            com.google.android.gms.internal.measurement.zzbs$zzi r2 = (com.google.android.gms.internal.measurement.zzbs.zzi) r2
            java.lang.Integer r3 = java.lang.Integer.valueOf(r11)
            r0 = r30
            java.lang.Object r3 = r0.get(r3)
            java.util.BitSet r3 = (java.util.BitSet) r3
            java.lang.Integer r4 = java.lang.Integer.valueOf(r11)
            r0 = r31
            java.lang.Object r4 = r0.get(r4)
            java.util.BitSet r4 = (java.util.BitSet) r4
            if (r34 == 0) goto L_0x0e1b
            android.support.v4.util.ArrayMap r8 = new android.support.v4.util.ArrayMap
            r8.<init>()
            if (r2 == 0) goto L_0x0208
            int r5 = r2.zzqc()
            if (r5 != 0) goto L_0x027c
        L_0x0208:
            java.lang.Integer r5 = java.lang.Integer.valueOf(r11)
            r0 = r32
            r0.put(r5, r8)
            r9 = r8
        L_0x0212:
            if (r3 != 0) goto L_0x0e17
            java.util.BitSet r3 = new java.util.BitSet
            r3.<init>()
            java.lang.Integer r4 = java.lang.Integer.valueOf(r11)
            r0 = r30
            r0.put(r4, r3)
            java.util.BitSet r4 = new java.util.BitSet
            r4.<init>()
            java.lang.Integer r5 = java.lang.Integer.valueOf(r11)
            r0 = r31
            r0.put(r5, r4)
            r5 = r4
            r8 = r3
        L_0x0232:
            if (r2 == 0) goto L_0x02b2
            r3 = 0
        L_0x0235:
            int r4 = r2.zzpw()
            int r4 = r4 << 6
            if (r3 >= r4) goto L_0x02b2
            r4 = 0
            java.util.List r12 = r2.zzpv()
            boolean r12 = com.google.android.gms.measurement.internal.zzjo.zza(r12, r3)
            if (r12 == 0) goto L_0x026e
            com.google.android.gms.measurement.internal.zzef r12 = r42.zzab()
            com.google.android.gms.measurement.internal.zzeh r12 = r12.zzgs()
            java.lang.String r13 = "Filter already evaluated. audience ID, filter ID"
            java.lang.Integer r14 = java.lang.Integer.valueOf(r11)
            java.lang.Integer r15 = java.lang.Integer.valueOf(r3)
            r12.zza(r13, r14, r15)
            r5.set(r3)
            java.util.List r12 = r2.zzpy()
            boolean r12 = com.google.android.gms.measurement.internal.zzjo.zza(r12, r3)
            if (r12 == 0) goto L_0x026e
            r8.set(r3)
            r4 = 1
        L_0x026e:
            if (r9 == 0) goto L_0x0279
            if (r4 != 0) goto L_0x0279
            java.lang.Integer r4 = java.lang.Integer.valueOf(r3)
            r9.remove(r4)
        L_0x0279:
            int r3 = r3 + 1
            goto L_0x0235
        L_0x027c:
            java.util.List r5 = r2.zzqb()
            java.util.Iterator r9 = r5.iterator()
        L_0x0284:
            boolean r5 = r9.hasNext()
            if (r5 == 0) goto L_0x0208
            java.lang.Object r5 = r9.next()
            com.google.android.gms.internal.measurement.zzbs$zzb r5 = (com.google.android.gms.internal.measurement.zzbs.zzb) r5
            boolean r12 = r5.zzme()
            if (r12 == 0) goto L_0x0284
            int r12 = r5.getIndex()
            boolean r13 = r5.zzmf()
            if (r13 == 0) goto L_0x02b0
            long r14 = r5.zzmg()
            java.lang.Long r5 = java.lang.Long.valueOf(r14)
        L_0x02a8:
            java.lang.Integer r12 = java.lang.Integer.valueOf(r12)
            r8.put(r12, r5)
            goto L_0x0284
        L_0x02b0:
            r5 = 0
            goto L_0x02a8
        L_0x02b2:
            com.google.android.gms.internal.measurement.zzbs$zza$zza r3 = com.google.android.gms.internal.measurement.zzbs.zza.zzmc()
            r4 = 0
            com.google.android.gms.internal.measurement.zzbs$zza$zza r3 = r3.zzk(r4)
            if (r36 == 0) goto L_0x030b
            java.lang.Integer r2 = java.lang.Integer.valueOf(r11)
            java.lang.Object r2 = r7.get(r2)
            com.google.android.gms.internal.measurement.zzbs$zzi r2 = (com.google.android.gms.internal.measurement.zzbs.zzi) r2
            r3.zza(r2)
        L_0x02ca:
            com.google.android.gms.internal.measurement.zzbs$zzi$zza r2 = com.google.android.gms.internal.measurement.zzbs.zzi.zzqh()
            java.util.List r4 = com.google.android.gms.measurement.internal.zzjo.zza(r8)
            com.google.android.gms.internal.measurement.zzbs$zzi$zza r2 = r2.zzo(r4)
            java.util.List r4 = com.google.android.gms.measurement.internal.zzjo.zza(r5)
            com.google.android.gms.internal.measurement.zzbs$zzi$zza r2 = r2.zzn(r4)
            if (r34 == 0) goto L_0x02f5
            java.util.List r4 = zza(r9)
            r2.zzp(r4)
            java.lang.Integer r4 = java.lang.Integer.valueOf(r11)
            android.support.v4.util.ArrayMap r5 = new android.support.v4.util.ArrayMap
            r5.<init>()
            r0 = r33
            r0.put(r4, r5)
        L_0x02f5:
            r3.zza(r2)
            java.lang.Integer r4 = java.lang.Integer.valueOf(r11)
            com.google.android.gms.internal.measurement.zzgi r2 = r3.zzug()
            com.google.android.gms.internal.measurement.zzey r2 = (com.google.android.gms.internal.measurement.zzey) r2
            com.google.android.gms.internal.measurement.zzbs$zza r2 = (com.google.android.gms.internal.measurement.zzbs.zza) r2
            r0 = r29
            r0.put(r4, r2)
            goto L_0x01c7
        L_0x030b:
            r3.zza(r2)
            goto L_0x02ca
        L_0x030f:
            boolean r2 = r44.isEmpty()
            if (r2 != 0) goto L_0x0865
            r8 = 0
            r6 = 0
            r3 = 0
            android.support.v4.util.ArrayMap r38 = new android.support.v4.util.ArrayMap
            r38.<init>()
            java.util.Iterator r39 = r44.iterator()
        L_0x0322:
            boolean r2 = r39.hasNext()
            if (r2 == 0) goto L_0x0865
            java.lang.Object r2 = r39.next()
            r20 = r2
            com.google.android.gms.internal.measurement.zzbs$zzc r20 = (com.google.android.gms.internal.measurement.zzbs.zzc) r20
            java.lang.String r9 = r20.getName()
            java.util.List r10 = r20.zzmj()
            r42.zzgw()
            java.lang.String r2 = "_eid"
            r0 = r20
            java.lang.Object r5 = com.google.android.gms.measurement.internal.zzjo.zzb(r0, r2)
            java.lang.Long r5 = (java.lang.Long) r5
            if (r5 == 0) goto L_0x0378
            r2 = 1
            r4 = r2
        L_0x0349:
            if (r4 == 0) goto L_0x037b
            java.lang.String r2 = "_ep"
            boolean r2 = r9.equals(r2)
            if (r2 == 0) goto L_0x037b
            r2 = 1
        L_0x0354:
            if (r2 == 0) goto L_0x0529
            r42.zzgw()
            java.lang.String r2 = "_en"
            r0 = r20
            java.lang.Object r2 = com.google.android.gms.measurement.internal.zzjo.zzb(r0, r2)
            r9 = r2
            java.lang.String r9 = (java.lang.String) r9
            boolean r2 = android.text.TextUtils.isEmpty(r9)
            if (r2 == 0) goto L_0x037d
            com.google.android.gms.measurement.internal.zzef r2 = r42.zzab()
            com.google.android.gms.measurement.internal.zzeh r2 = r2.zzgk()
            java.lang.String r4 = "Extra parameter without an event name. eventId"
            r2.zza(r4, r5)
            goto L_0x0322
        L_0x0378:
            r2 = 0
            r4 = r2
            goto L_0x0349
        L_0x037b:
            r2 = 0
            goto L_0x0354
        L_0x037d:
            if (r8 == 0) goto L_0x038d
            if (r3 == 0) goto L_0x038d
            long r12 = r5.longValue()
            long r14 = r3.longValue()
            int r2 = (r12 > r14 ? 1 : (r12 == r14 ? 0 : -1))
            if (r2 == 0) goto L_0x0dfd
        L_0x038d:
            com.google.android.gms.measurement.internal.zzx r2 = r42.zzgy()
            r0 = r43
            android.util.Pair r4 = r2.zza(r0, r5)
            if (r4 == 0) goto L_0x039d
            java.lang.Object r2 = r4.first
            if (r2 != 0) goto L_0x03ac
        L_0x039d:
            com.google.android.gms.measurement.internal.zzef r2 = r42.zzab()
            com.google.android.gms.measurement.internal.zzeh r2 = r2.zzgk()
            java.lang.String r4 = "Extra parameter without existing main event. eventName, eventId"
            r2.zza(r4, r9, r5)
            goto L_0x0322
        L_0x03ac:
            java.lang.Object r2 = r4.first
            com.google.android.gms.internal.measurement.zzbs$zzc r2 = (com.google.android.gms.internal.measurement.zzbs.zzc) r2
            java.lang.Object r3 = r4.second
            java.lang.Long r3 = (java.lang.Long) r3
            long r6 = r3.longValue()
            r42.zzgw()
            java.lang.String r3 = "_eid"
            java.lang.Object r3 = com.google.android.gms.measurement.internal.zzjo.zzb(r2, r3)
            java.lang.Long r3 = (java.lang.Long) r3
            r11 = r3
            r8 = r2
        L_0x03c5:
            r2 = 1
            long r6 = r6 - r2
            r2 = 0
            int r2 = (r6 > r2 ? 1 : (r6 == r2 ? 0 : -1))
            if (r2 > 0) goto L_0x042e
            com.google.android.gms.measurement.internal.zzx r3 = r42.zzgy()
            r3.zzo()
            com.google.android.gms.measurement.internal.zzef r2 = r3.zzab()
            com.google.android.gms.measurement.internal.zzeh r2 = r2.zzgs()
            java.lang.String r4 = "Clearing complex main event info. appId"
            r0 = r43
            r2.zza(r4, r0)
            android.database.sqlite.SQLiteDatabase r2 = r3.getWritableDatabase()     // Catch:{ SQLiteException -> 0x041f }
            java.lang.String r4 = "delete from main_event_params where app_id=?"
            r5 = 1
            java.lang.String[] r5 = new java.lang.String[r5]     // Catch:{ SQLiteException -> 0x041f }
            r12 = 0
            r5[r12] = r43     // Catch:{ SQLiteException -> 0x041f }
            r2.execSQL(r4, r5)     // Catch:{ SQLiteException -> 0x041f }
        L_0x03f3:
            java.util.ArrayList r3 = new java.util.ArrayList
            r3.<init>()
            java.util.List r2 = r8.zzmj()
            java.util.Iterator r4 = r2.iterator()
        L_0x0400:
            boolean r2 = r4.hasNext()
            if (r2 == 0) goto L_0x0438
            java.lang.Object r2 = r4.next()
            com.google.android.gms.internal.measurement.zzbs$zze r2 = (com.google.android.gms.internal.measurement.zzbs.zze) r2
            r42.zzgw()
            java.lang.String r5 = r2.getName()
            r0 = r20
            com.google.android.gms.internal.measurement.zzbs$zze r5 = com.google.android.gms.measurement.internal.zzjo.zza(r0, r5)
            if (r5 != 0) goto L_0x0400
            r3.add(r2)
            goto L_0x0400
        L_0x041f:
            r2 = move-exception
            com.google.android.gms.measurement.internal.zzef r3 = r3.zzab()
            com.google.android.gms.measurement.internal.zzeh r3 = r3.zzgk()
            java.lang.String r4 = "Error clearing complex main event"
            r3.zza(r4, r2)
            goto L_0x03f3
        L_0x042e:
            com.google.android.gms.measurement.internal.zzx r3 = r42.zzgy()
            r4 = r43
            r3.zza(r4, r5, r6, r8)
            goto L_0x03f3
        L_0x0438:
            boolean r2 = r3.isEmpty()
            if (r2 != 0) goto L_0x0510
            java.util.Iterator r4 = r10.iterator()
        L_0x0442:
            boolean r2 = r4.hasNext()
            if (r2 == 0) goto L_0x0452
            java.lang.Object r2 = r4.next()
            com.google.android.gms.internal.measurement.zzbs$zze r2 = (com.google.android.gms.internal.measurement.zzbs.zze) r2
            r3.add(r2)
            goto L_0x0442
        L_0x0452:
            r21 = r3
            r22 = r9
            r23 = r8
            r24 = r6
            r26 = r11
        L_0x045c:
            com.google.android.gms.measurement.internal.zzx r2 = r42.zzgy()
            java.lang.String r3 = r20.getName()
            r0 = r43
            com.google.android.gms.measurement.internal.zzae r2 = r2.zzc(r0, r3)
            if (r2 != 0) goto L_0x0599
            com.google.android.gms.measurement.internal.zzef r2 = r42.zzab()
            com.google.android.gms.measurement.internal.zzeh r2 = r2.zzgn()
            java.lang.String r3 = "Event aggregate wasn't created during raw event logging. appId, event"
            java.lang.Object r4 = com.google.android.gms.measurement.internal.zzef.zzam(r43)
            com.google.android.gms.measurement.internal.zzed r5 = r42.zzy()
            r0 = r22
            java.lang.String r5 = r5.zzaj(r0)
            r2.zza(r3, r4, r5)
            if (r37 == 0) goto L_0x057b
            com.google.android.gms.measurement.internal.zzae r3 = new com.google.android.gms.measurement.internal.zzae
            java.lang.String r5 = r20.getName()
            r6 = 1
            r8 = 1
            r10 = 1
            long r12 = r20.getTimestampMillis()
            r14 = 0
            r16 = 0
            r17 = 0
            r18 = 0
            r19 = 0
            r4 = r43
            r3.<init>(r4, r5, r6, r8, r10, r12, r14, r16, r17, r18, r19)
            r8 = r3
        L_0x04a9:
            com.google.android.gms.measurement.internal.zzx r2 = r42.zzgy()
            r2.zza(r8)
            long r14 = r8.zzfg
            r0 = r38
            r1 = r22
            java.lang.Object r2 = r0.get(r1)
            java.util.Map r2 = (java.util.Map) r2
            if (r2 != 0) goto L_0x0e13
            com.google.android.gms.measurement.internal.zzx r2 = r42.zzgy()
            r0 = r43
            r1 = r22
            java.util.Map r2 = r2.zzh(r0, r1)
            if (r2 != 0) goto L_0x04d1
            android.support.v4.util.ArrayMap r2 = new android.support.v4.util.ArrayMap
            r2.<init>()
        L_0x04d1:
            r0 = r38
            r1 = r22
            r0.put(r1, r2)
            r16 = r2
        L_0x04da:
            java.util.Set r2 = r16.keySet()
            java.util.Iterator r17 = r2.iterator()
        L_0x04e2:
            boolean r2 = r17.hasNext()
            if (r2 == 0) goto L_0x085d
            java.lang.Object r2 = r17.next()
            java.lang.Integer r2 = (java.lang.Integer) r2
            int r18 = r2.intValue()
            java.lang.Integer r2 = java.lang.Integer.valueOf(r18)
            r0 = r28
            boolean r2 = r0.contains(r2)
            if (r2 == 0) goto L_0x05f6
            com.google.android.gms.measurement.internal.zzef r2 = r42.zzab()
            com.google.android.gms.measurement.internal.zzeh r2 = r2.zzgs()
            java.lang.String r3 = "Skipping failed audience ID"
            java.lang.Integer r4 = java.lang.Integer.valueOf(r18)
            r2.zza(r3, r4)
            goto L_0x04e2
        L_0x0510:
            com.google.android.gms.measurement.internal.zzef r2 = r42.zzab()
            com.google.android.gms.measurement.internal.zzeh r2 = r2.zzgn()
            java.lang.String r3 = "No unique parameters in main event. eventName"
            r2.zza(r3, r9)
            r21 = r10
            r22 = r9
            r23 = r8
            r24 = r6
            r26 = r11
            goto L_0x045c
        L_0x0529:
            if (r4 == 0) goto L_0x0df1
            r42.zzgw()
            r2 = 0
            java.lang.Long r3 = java.lang.Long.valueOf(r2)
            java.lang.String r2 = "_epc"
            r0 = r20
            java.lang.Object r2 = com.google.android.gms.measurement.internal.zzjo.zzb(r0, r2)
            if (r2 != 0) goto L_0x053f
            r2 = r3
        L_0x053f:
            java.lang.Long r2 = (java.lang.Long) r2
            long r6 = r2.longValue()
            r2 = 0
            int r2 = (r6 > r2 ? 1 : (r6 == r2 ? 0 : -1))
            if (r2 > 0) goto L_0x0564
            com.google.android.gms.measurement.internal.zzef r2 = r42.zzab()
            com.google.android.gms.measurement.internal.zzeh r2 = r2.zzgn()
            java.lang.String r3 = "Complex event with zero extra param count. eventName"
            r2.zza(r3, r9)
            r21 = r10
            r22 = r9
            r23 = r20
            r24 = r6
            r26 = r5
            goto L_0x045c
        L_0x0564:
            com.google.android.gms.measurement.internal.zzx r3 = r42.zzgy()
            r4 = r43
            r8 = r20
            r3.zza(r4, r5, r6, r8)
            r21 = r10
            r22 = r9
            r23 = r20
            r24 = r6
            r26 = r5
            goto L_0x045c
        L_0x057b:
            com.google.android.gms.measurement.internal.zzae r3 = new com.google.android.gms.measurement.internal.zzae
            java.lang.String r5 = r20.getName()
            r6 = 1
            r8 = 1
            long r10 = r20.getTimestampMillis()
            r12 = 0
            r14 = 0
            r15 = 0
            r16 = 0
            r17 = 0
            r4 = r43
            r3.<init>(r4, r5, r6, r8, r10, r12, r14, r15, r16, r17)
            r8 = r3
            goto L_0x04a9
        L_0x0599:
            if (r37 == 0) goto L_0x05ca
            com.google.android.gms.measurement.internal.zzae r3 = new com.google.android.gms.measurement.internal.zzae
            java.lang.String r4 = r2.zzce
            java.lang.String r5 = r2.name
            r6 = 1
            long r8 = r2.zzfg
            long r6 = r6 + r8
            r8 = 1
            long r10 = r2.zzfh
            long r8 = r8 + r10
            r10 = 1
            long r12 = r2.zzfi
            long r10 = r10 + r12
            long r12 = r2.zzfj
            long r14 = r2.zzfk
            java.lang.Long r0 = r2.zzfl
            r16 = r0
            java.lang.Long r0 = r2.zzfm
            r17 = r0
            java.lang.Long r0 = r2.zzfn
            r18 = r0
            java.lang.Boolean r0 = r2.zzfo
            r19 = r0
            r3.<init>(r4, r5, r6, r8, r10, r12, r14, r16, r17, r18, r19)
            r8 = r3
            goto L_0x04a9
        L_0x05ca:
            com.google.android.gms.measurement.internal.zzae r3 = new com.google.android.gms.measurement.internal.zzae
            java.lang.String r4 = r2.zzce
            java.lang.String r5 = r2.name
            r6 = 1
            long r8 = r2.zzfg
            long r6 = r6 + r8
            r8 = 1
            long r10 = r2.zzfh
            long r8 = r8 + r10
            long r10 = r2.zzfi
            long r12 = r2.zzfj
            long r14 = r2.zzfk
            java.lang.Long r0 = r2.zzfl
            r16 = r0
            java.lang.Long r0 = r2.zzfm
            r17 = r0
            java.lang.Long r0 = r2.zzfn
            r18 = r0
            java.lang.Boolean r0 = r2.zzfo
            r19 = r0
            r3.<init>(r4, r5, r6, r8, r10, r12, r14, r16, r17, r18, r19)
            r8 = r3
            goto L_0x04a9
        L_0x05f6:
            java.lang.Integer r2 = java.lang.Integer.valueOf(r18)
            r0 = r30
            java.lang.Object r2 = r0.get(r2)
            r3 = r2
            java.util.BitSet r3 = (java.util.BitSet) r3
            java.lang.Integer r2 = java.lang.Integer.valueOf(r18)
            r0 = r31
            java.lang.Object r2 = r0.get(r2)
            r4 = r2
            java.util.BitSet r4 = (java.util.BitSet) r4
            r2 = 0
            if (r34 == 0) goto L_0x0e0f
            java.lang.Integer r2 = java.lang.Integer.valueOf(r18)
            r0 = r32
            java.lang.Object r2 = r0.get(r2)
            r5 = r2
            java.util.Map r5 = (java.util.Map) r5
            java.lang.Integer r2 = java.lang.Integer.valueOf(r18)
            r0 = r33
            java.lang.Object r2 = r0.get(r2)
            java.util.Map r2 = (java.util.Map) r2
            r6 = r2
        L_0x062d:
            java.lang.Integer r2 = java.lang.Integer.valueOf(r18)
            r0 = r29
            java.lang.Object r2 = r0.get(r2)
            com.google.android.gms.internal.measurement.zzbs$zza r2 = (com.google.android.gms.internal.measurement.zzbs.zza) r2
            if (r2 != 0) goto L_0x0e09
            java.lang.Integer r3 = java.lang.Integer.valueOf(r18)
            com.google.android.gms.internal.measurement.zzbs$zza$zza r2 = com.google.android.gms.internal.measurement.zzbs.zza.zzmc()
            r4 = 1
            com.google.android.gms.internal.measurement.zzbs$zza$zza r2 = r2.zzk(r4)
            com.google.android.gms.internal.measurement.zzgi r2 = r2.zzug()
            com.google.android.gms.internal.measurement.zzey r2 = (com.google.android.gms.internal.measurement.zzey) r2
            com.google.android.gms.internal.measurement.zzbs$zza r2 = (com.google.android.gms.internal.measurement.zzbs.zza) r2
            r0 = r29
            r0.put(r3, r2)
            java.util.BitSet r3 = new java.util.BitSet
            r3.<init>()
            java.lang.Integer r2 = java.lang.Integer.valueOf(r18)
            r0 = r30
            r0.put(r2, r3)
            java.util.BitSet r4 = new java.util.BitSet
            r4.<init>()
            java.lang.Integer r2 = java.lang.Integer.valueOf(r18)
            r0 = r31
            r0.put(r2, r4)
            if (r34 == 0) goto L_0x0e03
            android.support.v4.util.ArrayMap r5 = new android.support.v4.util.ArrayMap
            r5.<init>()
            java.lang.Integer r2 = java.lang.Integer.valueOf(r18)
            r0 = r32
            r0.put(r2, r5)
            android.support.v4.util.ArrayMap r6 = new android.support.v4.util.ArrayMap
            r6.<init>()
            java.lang.Integer r2 = java.lang.Integer.valueOf(r18)
            r0 = r33
            r0.put(r2, r6)
            r9 = r3
            r10 = r4
            r11 = r5
            r12 = r6
        L_0x0693:
            java.lang.Integer r2 = java.lang.Integer.valueOf(r18)
            r0 = r16
            java.lang.Object r2 = r0.get(r2)
            java.util.List r2 = (java.util.List) r2
            java.util.Iterator r19 = r2.iterator()
        L_0x06a3:
            boolean r2 = r19.hasNext()
            if (r2 == 0) goto L_0x04e2
            java.lang.Object r3 = r19.next()
            com.google.android.gms.internal.measurement.zzbk$zza r3 = (com.google.android.gms.internal.measurement.zzbk.zza) r3
            if (r37 == 0) goto L_0x0e00
            if (r36 == 0) goto L_0x0e00
            boolean r2 = r3.zzki()
            if (r2 == 0) goto L_0x0e00
            long r6 = r8.zzfi
        L_0x06bb:
            com.google.android.gms.measurement.internal.zzef r2 = r42.zzab()
            r4 = 2
            boolean r2 = r2.isLoggable(r4)
            if (r2 == 0) goto L_0x0708
            com.google.android.gms.measurement.internal.zzef r2 = r42.zzab()
            com.google.android.gms.measurement.internal.zzeh r4 = r2.zzgs()
            boolean r2 = r3.zzkb()
            if (r2 == 0) goto L_0x073b
            int r2 = r3.getId()
            java.lang.Integer r2 = java.lang.Integer.valueOf(r2)
        L_0x06dc:
            java.lang.String r5 = "Evaluating filter. audience, filter, event"
            java.lang.Integer r13 = java.lang.Integer.valueOf(r18)
            com.google.android.gms.measurement.internal.zzed r40 = r42.zzy()
            java.lang.String r41 = r3.zzjz()
            java.lang.String r40 = r40.zzaj(r41)
            r0 = r40
            r4.zza(r5, r13, r2, r0)
            com.google.android.gms.measurement.internal.zzef r2 = r42.zzab()
            com.google.android.gms.measurement.internal.zzeh r2 = r2.zzgs()
            java.lang.String r4 = "Filter definition"
            com.google.android.gms.measurement.internal.zzjo r5 = r42.zzgw()
            java.lang.String r5 = r5.zza(r3)
            r2.zza(r4, r5)
        L_0x0708:
            boolean r2 = r3.zzkb()
            if (r2 == 0) goto L_0x0716
            int r2 = r3.getId()
            r4 = 256(0x100, float:3.59E-43)
            if (r2 <= r4) goto L_0x073f
        L_0x0716:
            com.google.android.gms.measurement.internal.zzef r2 = r42.zzab()
            com.google.android.gms.measurement.internal.zzeh r4 = r2.zzgn()
            java.lang.Object r5 = com.google.android.gms.measurement.internal.zzef.zzam(r43)
            boolean r2 = r3.zzkb()
            if (r2 == 0) goto L_0x073d
            int r2 = r3.getId()
            java.lang.Integer r2 = java.lang.Integer.valueOf(r2)
        L_0x0730:
            java.lang.String r3 = "Invalid event filter ID. appId, id"
            java.lang.String r2 = java.lang.String.valueOf(r2)
            r4.zza(r3, r5, r2)
            goto L_0x06a3
        L_0x073b:
            r2 = 0
            goto L_0x06dc
        L_0x073d:
            r2 = 0
            goto L_0x0730
        L_0x073f:
            if (r34 == 0) goto L_0x07f0
            boolean r4 = r3.zzkf()
            boolean r40 = r3.zzkg()
            if (r36 == 0) goto L_0x0787
            boolean r2 = r3.zzki()
            if (r2 == 0) goto L_0x0787
            r2 = 1
        L_0x0752:
            if (r4 != 0) goto L_0x0758
            if (r40 != 0) goto L_0x0758
            if (r2 == 0) goto L_0x0789
        L_0x0758:
            r2 = 1
            r13 = r2
        L_0x075a:
            int r2 = r3.getId()
            boolean r2 = r9.get(r2)
            if (r2 == 0) goto L_0x078e
            if (r13 != 0) goto L_0x078e
            com.google.android.gms.measurement.internal.zzef r2 = r42.zzab()
            com.google.android.gms.measurement.internal.zzeh r4 = r2.zzgs()
            boolean r2 = r3.zzkb()
            if (r2 == 0) goto L_0x078c
            int r2 = r3.getId()
            java.lang.Integer r2 = java.lang.Integer.valueOf(r2)
        L_0x077c:
            java.lang.String r3 = "Event filter already evaluated true and it is not associated with an enhanced audience. audience ID, filter ID"
            java.lang.Integer r5 = java.lang.Integer.valueOf(r18)
            r4.zza(r3, r5, r2)
            goto L_0x06a3
        L_0x0787:
            r2 = 0
            goto L_0x0752
        L_0x0789:
            r2 = 0
            r13 = r2
            goto L_0x075a
        L_0x078c:
            r2 = 0
            goto L_0x077c
        L_0x078e:
            r2 = r42
            r4 = r22
            r5 = r21
            java.lang.Boolean r4 = r2.zza(r3, r4, r5, r6)
            com.google.android.gms.measurement.internal.zzef r2 = r42.zzab()
            com.google.android.gms.measurement.internal.zzeh r5 = r2.zzgs()
            if (r4 != 0) goto L_0x07b6
            java.lang.String r2 = "null"
        L_0x07a4:
            java.lang.String r6 = "Event filter result"
            r5.zza(r6, r2)
            if (r4 != 0) goto L_0x07b8
            java.lang.Integer r2 = java.lang.Integer.valueOf(r18)
            r0 = r28
            r0.add(r2)
            goto L_0x06a3
        L_0x07b6:
            r2 = r4
            goto L_0x07a4
        L_0x07b8:
            int r2 = r3.getId()
            r10.set(r2)
            boolean r2 = r4.booleanValue()
            if (r2 == 0) goto L_0x06a3
            int r2 = r3.getId()
            r9.set(r2)
            if (r13 == 0) goto L_0x06a3
            boolean r2 = r20.zzml()
            if (r2 == 0) goto L_0x06a3
            if (r40 == 0) goto L_0x07e3
            int r2 = r3.getId()
            long r4 = r20.getTimestampMillis()
            zzb(r12, r2, r4)
            goto L_0x06a3
        L_0x07e3:
            int r2 = r3.getId()
            long r4 = r20.getTimestampMillis()
            zza(r11, r2, r4)
            goto L_0x06a3
        L_0x07f0:
            int r2 = r3.getId()
            boolean r2 = r9.get(r2)
            if (r2 == 0) goto L_0x081d
            com.google.android.gms.measurement.internal.zzef r2 = r42.zzab()
            com.google.android.gms.measurement.internal.zzeh r4 = r2.zzgs()
            boolean r2 = r3.zzkb()
            if (r2 == 0) goto L_0x081b
            int r2 = r3.getId()
            java.lang.Integer r2 = java.lang.Integer.valueOf(r2)
        L_0x0810:
            java.lang.String r3 = "Event filter already evaluated true. audience ID, filter ID"
            java.lang.Integer r5 = java.lang.Integer.valueOf(r18)
            r4.zza(r3, r5, r2)
            goto L_0x06a3
        L_0x081b:
            r2 = 0
            goto L_0x0810
        L_0x081d:
            r2 = r42
            r4 = r22
            r5 = r21
            java.lang.Boolean r4 = r2.zza(r3, r4, r5, r6)
            com.google.android.gms.measurement.internal.zzef r2 = r42.zzab()
            com.google.android.gms.measurement.internal.zzeh r5 = r2.zzgs()
            if (r4 != 0) goto L_0x0845
            java.lang.String r2 = "null"
        L_0x0833:
            java.lang.String r6 = "Event filter result"
            r5.zza(r6, r2)
            if (r4 != 0) goto L_0x0847
            java.lang.Integer r2 = java.lang.Integer.valueOf(r18)
            r0 = r28
            r0.add(r2)
            goto L_0x06a3
        L_0x0845:
            r2 = r4
            goto L_0x0833
        L_0x0847:
            int r2 = r3.getId()
            r10.set(r2)
            boolean r2 = r4.booleanValue()
            if (r2 == 0) goto L_0x06a3
            int r2 = r3.getId()
            r9.set(r2)
            goto L_0x06a3
        L_0x085d:
            r3 = r26
            r8 = r23
            r6 = r24
            goto L_0x0322
        L_0x0865:
            boolean r2 = r45.isEmpty()
            if (r2 != 0) goto L_0x0b6f
            android.support.v4.util.ArrayMap r13 = new android.support.v4.util.ArrayMap
            r13.<init>()
            java.util.Iterator r14 = r45.iterator()
        L_0x0874:
            boolean r2 = r14.hasNext()
            if (r2 == 0) goto L_0x0b6f
            java.lang.Object r2 = r14.next()
            r5 = r2
            com.google.android.gms.internal.measurement.zzbs$zzk r5 = (com.google.android.gms.internal.measurement.zzbs.zzk) r5
            java.lang.String r2 = r5.getName()
            java.lang.Object r2 = r13.get(r2)
            java.util.Map r2 = (java.util.Map) r2
            if (r2 != 0) goto L_0x0dee
            com.google.android.gms.measurement.internal.zzx r2 = r42.zzgy()
            java.lang.String r3 = r5.getName()
            r0 = r43
            java.util.Map r2 = r2.zzi(r0, r3)
            if (r2 != 0) goto L_0x08a2
            android.support.v4.util.ArrayMap r2 = new android.support.v4.util.ArrayMap
            r2.<init>()
        L_0x08a2:
            java.lang.String r3 = r5.getName()
            r13.put(r3, r2)
            r11 = r2
        L_0x08aa:
            java.util.Set r2 = r11.keySet()
            java.util.Iterator r15 = r2.iterator()
        L_0x08b2:
            boolean r2 = r15.hasNext()
            if (r2 == 0) goto L_0x0874
            java.lang.Object r2 = r15.next()
            java.lang.Integer r2 = (java.lang.Integer) r2
            int r16 = r2.intValue()
            java.lang.Integer r2 = java.lang.Integer.valueOf(r16)
            r0 = r28
            boolean r2 = r0.contains(r2)
            if (r2 == 0) goto L_0x08e0
            com.google.android.gms.measurement.internal.zzef r2 = r42.zzab()
            com.google.android.gms.measurement.internal.zzeh r2 = r2.zzgs()
            java.lang.String r3 = "Skipping failed audience ID"
            java.lang.Integer r4 = java.lang.Integer.valueOf(r16)
            r2.zza(r3, r4)
            goto L_0x08b2
        L_0x08e0:
            java.lang.Integer r2 = java.lang.Integer.valueOf(r16)
            r0 = r30
            java.lang.Object r2 = r0.get(r2)
            r6 = r2
            java.util.BitSet r6 = (java.util.BitSet) r6
            java.lang.Integer r2 = java.lang.Integer.valueOf(r16)
            r0 = r31
            java.lang.Object r2 = r0.get(r2)
            r4 = r2
            java.util.BitSet r4 = (java.util.BitSet) r4
            r3 = 0
            if (r34 == 0) goto L_0x0dea
            java.lang.Integer r2 = java.lang.Integer.valueOf(r16)
            r0 = r32
            java.lang.Object r2 = r0.get(r2)
            java.util.Map r2 = (java.util.Map) r2
            java.lang.Integer r3 = java.lang.Integer.valueOf(r16)
            r0 = r33
            java.lang.Object r3 = r0.get(r3)
            java.util.Map r3 = (java.util.Map) r3
            r7 = r2
        L_0x0916:
            java.lang.Integer r2 = java.lang.Integer.valueOf(r16)
            r0 = r29
            java.lang.Object r2 = r0.get(r2)
            com.google.android.gms.internal.measurement.zzbs$zza r2 = (com.google.android.gms.internal.measurement.zzbs.zza) r2
            if (r2 != 0) goto L_0x0978
            java.lang.Integer r4 = java.lang.Integer.valueOf(r16)
            com.google.android.gms.internal.measurement.zzbs$zza$zza r2 = com.google.android.gms.internal.measurement.zzbs.zza.zzmc()
            r6 = 1
            com.google.android.gms.internal.measurement.zzbs$zza$zza r2 = r2.zzk(r6)
            com.google.android.gms.internal.measurement.zzgi r2 = r2.zzug()
            com.google.android.gms.internal.measurement.zzey r2 = (com.google.android.gms.internal.measurement.zzey) r2
            com.google.android.gms.internal.measurement.zzbs$zza r2 = (com.google.android.gms.internal.measurement.zzbs.zza) r2
            r0 = r29
            r0.put(r4, r2)
            java.util.BitSet r6 = new java.util.BitSet
            r6.<init>()
            java.lang.Integer r2 = java.lang.Integer.valueOf(r16)
            r0 = r30
            r0.put(r2, r6)
            java.util.BitSet r4 = new java.util.BitSet
            r4.<init>()
            java.lang.Integer r2 = java.lang.Integer.valueOf(r16)
            r0 = r31
            r0.put(r2, r4)
            if (r34 == 0) goto L_0x0978
            android.support.v4.util.ArrayMap r7 = new android.support.v4.util.ArrayMap
            r7.<init>()
            java.lang.Integer r2 = java.lang.Integer.valueOf(r16)
            r0 = r32
            r0.put(r2, r7)
            android.support.v4.util.ArrayMap r3 = new android.support.v4.util.ArrayMap
            r3.<init>()
            java.lang.Integer r2 = java.lang.Integer.valueOf(r16)
            r0 = r33
            r0.put(r2, r3)
        L_0x0978:
            java.lang.Integer r2 = java.lang.Integer.valueOf(r16)
            java.lang.Object r2 = r11.get(r2)
            java.util.List r2 = (java.util.List) r2
            java.util.Iterator r17 = r2.iterator()
        L_0x0986:
            boolean r2 = r17.hasNext()
            if (r2 == 0) goto L_0x08b2
            java.lang.Object r2 = r17.next()
            com.google.android.gms.internal.measurement.zzbk$zzd r2 = (com.google.android.gms.internal.measurement.zzbk.zzd) r2
            com.google.android.gms.measurement.internal.zzef r8 = r42.zzab()
            r9 = 2
            boolean r8 = r8.isLoggable(r9)
            if (r8 == 0) goto L_0x09df
            com.google.android.gms.measurement.internal.zzef r8 = r42.zzab()
            com.google.android.gms.measurement.internal.zzeh r9 = r8.zzgs()
            boolean r8 = r2.zzkb()
            if (r8 == 0) goto L_0x0a1b
            int r8 = r2.getId()
            java.lang.Integer r8 = java.lang.Integer.valueOf(r8)
        L_0x09b3:
            java.lang.String r10 = "Evaluating filter. audience, filter, property"
            java.lang.Integer r12 = java.lang.Integer.valueOf(r16)
            com.google.android.gms.measurement.internal.zzed r18 = r42.zzy()
            java.lang.String r19 = r2.getPropertyName()
            java.lang.String r18 = r18.zzal(r19)
            r0 = r18
            r9.zza(r10, r12, r8, r0)
            com.google.android.gms.measurement.internal.zzef r8 = r42.zzab()
            com.google.android.gms.measurement.internal.zzeh r8 = r8.zzgs()
            java.lang.String r9 = "Filter definition"
            com.google.android.gms.measurement.internal.zzjo r10 = r42.zzgw()
            java.lang.String r10 = r10.zza(r2)
            r8.zza(r9, r10)
        L_0x09df:
            boolean r8 = r2.zzkb()
            if (r8 == 0) goto L_0x09ed
            int r8 = r2.getId()
            r9 = 256(0x100, float:3.59E-43)
            if (r8 <= r9) goto L_0x0a1f
        L_0x09ed:
            com.google.android.gms.measurement.internal.zzef r3 = r42.zzab()
            com.google.android.gms.measurement.internal.zzeh r3 = r3.zzgn()
            java.lang.Object r4 = com.google.android.gms.measurement.internal.zzef.zzam(r43)
            boolean r6 = r2.zzkb()
            if (r6 == 0) goto L_0x0a1d
            int r2 = r2.getId()
            java.lang.Integer r2 = java.lang.Integer.valueOf(r2)
        L_0x0a07:
            java.lang.String r6 = "Invalid property filter ID. appId, id"
            java.lang.String r2 = java.lang.String.valueOf(r2)
            r3.zza(r6, r4, r2)
            java.lang.Integer r2 = java.lang.Integer.valueOf(r16)
            r0 = r28
            r0.add(r2)
            goto L_0x08b2
        L_0x0a1b:
            r8 = 0
            goto L_0x09b3
        L_0x0a1d:
            r2 = 0
            goto L_0x0a07
        L_0x0a1f:
            if (r34 == 0) goto L_0x0b06
            boolean r9 = r2.zzkf()
            boolean r18 = r2.zzkg()
            if (r36 == 0) goto L_0x0a67
            boolean r8 = r2.zzki()
            if (r8 == 0) goto L_0x0a67
            r8 = 1
            r12 = r8
        L_0x0a33:
            if (r9 != 0) goto L_0x0a39
            if (r18 != 0) goto L_0x0a39
            if (r12 == 0) goto L_0x0a6a
        L_0x0a39:
            r8 = 1
        L_0x0a3a:
            int r9 = r2.getId()
            boolean r9 = r6.get(r9)
            if (r9 == 0) goto L_0x0a6e
            if (r8 != 0) goto L_0x0a6e
            com.google.android.gms.measurement.internal.zzef r8 = r42.zzab()
            com.google.android.gms.measurement.internal.zzeh r8 = r8.zzgs()
            boolean r9 = r2.zzkb()
            if (r9 == 0) goto L_0x0a6c
            int r2 = r2.getId()
            java.lang.Integer r2 = java.lang.Integer.valueOf(r2)
        L_0x0a5c:
            java.lang.String r9 = "Property filter already evaluated true and it is not associated with an enhanced audience. audience ID, filter ID"
            java.lang.Integer r10 = java.lang.Integer.valueOf(r16)
            r8.zza(r9, r10, r2)
            goto L_0x0986
        L_0x0a67:
            r8 = 0
            r12 = r8
            goto L_0x0a33
        L_0x0a6a:
            r8 = 0
            goto L_0x0a3a
        L_0x0a6c:
            r2 = 0
            goto L_0x0a5c
        L_0x0a6e:
            r0 = r42
            java.lang.Boolean r10 = r0.zza(r2, r5)
            com.google.android.gms.measurement.internal.zzef r9 = r42.zzab()
            com.google.android.gms.measurement.internal.zzeh r19 = r9.zzgs()
            if (r10 != 0) goto L_0x0a96
            java.lang.String r9 = "null"
        L_0x0a80:
            java.lang.String r20 = "Property filter result"
            r0 = r19
            r1 = r20
            r0.zza(r1, r9)
            if (r10 != 0) goto L_0x0a98
            java.lang.Integer r2 = java.lang.Integer.valueOf(r16)
            r0 = r28
            r0.add(r2)
            goto L_0x0986
        L_0x0a96:
            r9 = r10
            goto L_0x0a80
        L_0x0a98:
            int r9 = r2.getId()
            r4.set(r9)
            if (r36 == 0) goto L_0x0aa9
            if (r12 == 0) goto L_0x0aa9
            boolean r9 = r10.booleanValue()
            if (r9 == 0) goto L_0x0986
        L_0x0aa9:
            if (r35 == 0) goto L_0x0aef
            int r9 = r2.getId()
            boolean r9 = r6.get(r9)
            if (r9 == 0) goto L_0x0abb
            boolean r9 = r2.zzkf()
            if (r9 == 0) goto L_0x0ac8
        L_0x0abb:
            int r9 = r2.getId()
            boolean r19 = r10.booleanValue()
            r0 = r19
            r6.set(r9, r0)
        L_0x0ac8:
            boolean r9 = r10.booleanValue()
            if (r9 == 0) goto L_0x0986
            if (r8 == 0) goto L_0x0986
            boolean r8 = r5.zzqs()
            if (r8 == 0) goto L_0x0986
            long r8 = r5.zzqt()
            if (r36 == 0) goto L_0x0ae4
            if (r12 == 0) goto L_0x0ae4
            if (r27 == 0) goto L_0x0ae4
            long r8 = r27.longValue()
        L_0x0ae4:
            if (r18 == 0) goto L_0x0afd
            int r2 = r2.getId()
            zzb(r3, r2, r8)
            goto L_0x0986
        L_0x0aef:
            int r9 = r2.getId()
            boolean r19 = r10.booleanValue()
            r0 = r19
            r6.set(r9, r0)
            goto L_0x0ac8
        L_0x0afd:
            int r2 = r2.getId()
            zza(r7, r2, r8)
            goto L_0x0986
        L_0x0b06:
            int r8 = r2.getId()
            boolean r8 = r6.get(r8)
            if (r8 == 0) goto L_0x0b33
            com.google.android.gms.measurement.internal.zzef r8 = r42.zzab()
            com.google.android.gms.measurement.internal.zzeh r8 = r8.zzgs()
            boolean r9 = r2.zzkb()
            if (r9 == 0) goto L_0x0b31
            int r2 = r2.getId()
            java.lang.Integer r2 = java.lang.Integer.valueOf(r2)
        L_0x0b26:
            java.lang.String r9 = "Property filter already evaluated true. audience ID, filter ID"
            java.lang.Integer r10 = java.lang.Integer.valueOf(r16)
            r8.zza(r9, r10, r2)
            goto L_0x0986
        L_0x0b31:
            r2 = 0
            goto L_0x0b26
        L_0x0b33:
            r0 = r42
            java.lang.Boolean r9 = r0.zza(r2, r5)
            com.google.android.gms.measurement.internal.zzef r8 = r42.zzab()
            com.google.android.gms.measurement.internal.zzeh r10 = r8.zzgs()
            if (r9 != 0) goto L_0x0b57
            java.lang.String r8 = "null"
        L_0x0b45:
            java.lang.String r12 = "Property filter result"
            r10.zza(r12, r8)
            if (r9 != 0) goto L_0x0b59
            java.lang.Integer r2 = java.lang.Integer.valueOf(r16)
            r0 = r28
            r0.add(r2)
            goto L_0x0986
        L_0x0b57:
            r8 = r9
            goto L_0x0b45
        L_0x0b59:
            int r8 = r2.getId()
            r4.set(r8)
            boolean r8 = r9.booleanValue()
            if (r8 == 0) goto L_0x0986
            int r2 = r2.getId()
            r6.set(r2)
            goto L_0x0986
        L_0x0b6f:
            java.util.ArrayList r7 = new java.util.ArrayList
            r7.<init>()
            java.util.Set r2 = r30.keySet()
            java.util.Iterator r8 = r2.iterator()
        L_0x0b7c:
            boolean r2 = r8.hasNext()
            if (r2 == 0) goto L_0x0e24
            java.lang.Object r2 = r8.next()
            java.lang.Integer r2 = (java.lang.Integer) r2
            int r9 = r2.intValue()
            java.lang.Integer r2 = java.lang.Integer.valueOf(r9)
            r0 = r28
            boolean r2 = r0.contains(r2)
            if (r2 != 0) goto L_0x0b7c
            java.lang.Integer r2 = java.lang.Integer.valueOf(r9)
            r0 = r29
            java.lang.Object r2 = r0.get(r2)
            com.google.android.gms.internal.measurement.zzbs$zza r2 = (com.google.android.gms.internal.measurement.zzbs.zza) r2
            if (r2 != 0) goto L_0x0ca5
            com.google.android.gms.internal.measurement.zzbs$zza$zza r2 = com.google.android.gms.internal.measurement.zzbs.zza.zzmc()
            r6 = r2
        L_0x0bab:
            r6.zzi(r9)
            com.google.android.gms.internal.measurement.zzbs$zzi$zza r3 = com.google.android.gms.internal.measurement.zzbs.zzi.zzqh()
            java.lang.Integer r2 = java.lang.Integer.valueOf(r9)
            r0 = r30
            java.lang.Object r2 = r0.get(r2)
            java.util.BitSet r2 = (java.util.BitSet) r2
            java.util.List r2 = com.google.android.gms.measurement.internal.zzjo.zza(r2)
            com.google.android.gms.internal.measurement.zzbs$zzi$zza r3 = r3.zzo(r2)
            java.lang.Integer r2 = java.lang.Integer.valueOf(r9)
            r0 = r31
            java.lang.Object r2 = r0.get(r2)
            java.util.BitSet r2 = (java.util.BitSet) r2
            java.util.List r2 = com.google.android.gms.measurement.internal.zzjo.zza(r2)
            com.google.android.gms.internal.measurement.zzbs$zzi$zza r10 = r3.zzn(r2)
            if (r34 == 0) goto L_0x0c1b
            java.lang.Integer r2 = java.lang.Integer.valueOf(r9)
            r0 = r32
            java.lang.Object r2 = r0.get(r2)
            java.util.Map r2 = (java.util.Map) r2
            java.util.List r2 = zza(r2)
            r10.zzp(r2)
            java.lang.Integer r2 = java.lang.Integer.valueOf(r9)
            r0 = r33
            java.lang.Object r2 = r0.get(r2)
            r3 = r2
            java.util.Map r3 = (java.util.Map) r3
            if (r3 != 0) goto L_0x0cb0
            java.util.List r2 = java.util.Collections.emptyList()
        L_0x0c02:
            if (r35 == 0) goto L_0x0c18
            boolean r3 = r6.zzlw()
            if (r3 == 0) goto L_0x0c18
            com.google.android.gms.internal.measurement.zzbs$zzi r3 = r6.zzlx()
            java.util.List r3 = r3.zzqe()
            boolean r4 = r3.isEmpty()
            if (r4 == 0) goto L_0x0d0b
        L_0x0c18:
            r10.zzq(r2)
        L_0x0c1b:
            r6.zza(r10)
            java.lang.Integer r3 = java.lang.Integer.valueOf(r9)
            com.google.android.gms.internal.measurement.zzgi r2 = r6.zzug()
            com.google.android.gms.internal.measurement.zzey r2 = (com.google.android.gms.internal.measurement.zzey) r2
            com.google.android.gms.internal.measurement.zzbs$zza r2 = (com.google.android.gms.internal.measurement.zzbs.zza) r2
            r0 = r29
            r0.put(r3, r2)
            com.google.android.gms.internal.measurement.zzgi r2 = r6.zzug()
            com.google.android.gms.internal.measurement.zzey r2 = (com.google.android.gms.internal.measurement.zzey) r2
            com.google.android.gms.internal.measurement.zzbs$zza r2 = (com.google.android.gms.internal.measurement.zzbs.zza) r2
            r7.add(r2)
            com.google.android.gms.measurement.internal.zzx r3 = r42.zzgy()
            com.google.android.gms.internal.measurement.zzbs$zzi r2 = r6.zzlv()
            r3.zzbi()
            r3.zzo()
            com.google.android.gms.common.internal.Preconditions.checkNotEmpty(r43)
            com.google.android.gms.common.internal.Preconditions.checkNotNull(r2)
            byte[] r2 = r2.toByteArray()
            android.content.ContentValues r4 = new android.content.ContentValues
            r4.<init>()
            java.lang.String r5 = "app_id"
            r0 = r43
            r4.put(r5, r0)
            java.lang.String r5 = "audience_id"
            java.lang.Integer r6 = java.lang.Integer.valueOf(r9)
            r4.put(r5, r6)
            java.lang.String r5 = "current_results"
            r4.put(r5, r2)
            android.database.sqlite.SQLiteDatabase r2 = r3.getWritableDatabase()     // Catch:{ SQLiteException -> 0x0c91 }
            java.lang.String r5 = "audience_filter_values"
            r6 = 0
            r9 = 5
            long r4 = r2.insertWithOnConflict(r5, r6, r4, r9)     // Catch:{ SQLiteException -> 0x0c91 }
            r10 = -1
            int r2 = (r4 > r10 ? 1 : (r4 == r10 ? 0 : -1))
            if (r2 != 0) goto L_0x0b7c
            com.google.android.gms.measurement.internal.zzef r2 = r3.zzab()     // Catch:{ SQLiteException -> 0x0c91 }
            com.google.android.gms.measurement.internal.zzeh r2 = r2.zzgk()     // Catch:{ SQLiteException -> 0x0c91 }
            java.lang.String r4 = "Failed to insert filter results (got -1). appId"
            java.lang.Object r5 = com.google.android.gms.measurement.internal.zzef.zzam(r43)     // Catch:{ SQLiteException -> 0x0c91 }
            r2.zza(r4, r5)     // Catch:{ SQLiteException -> 0x0c91 }
            goto L_0x0b7c
        L_0x0c91:
            r2 = move-exception
            com.google.android.gms.measurement.internal.zzef r3 = r3.zzab()
            com.google.android.gms.measurement.internal.zzeh r3 = r3.zzgk()
            java.lang.String r4 = "Error storing filter results. appId"
            java.lang.Object r5 = com.google.android.gms.measurement.internal.zzef.zzam(r43)
            r3.zza(r4, r5, r2)
            goto L_0x0b7c
        L_0x0ca5:
            com.google.android.gms.internal.measurement.zzey$zza r2 = r2.zzuj()
            com.google.android.gms.internal.measurement.zzey$zza r2 = (com.google.android.gms.internal.measurement.zzey.zza) r2
            com.google.android.gms.internal.measurement.zzbs$zza$zza r2 = (com.google.android.gms.internal.measurement.zzbs.zza.C1833zza) r2
            r6 = r2
            goto L_0x0bab
        L_0x0cb0:
            java.util.ArrayList r4 = new java.util.ArrayList
            int r2 = r3.size()
            r4.<init>(r2)
            java.util.Set r2 = r3.keySet()
            java.util.Iterator r5 = r2.iterator()
        L_0x0cc1:
            boolean r2 = r5.hasNext()
            if (r2 == 0) goto L_0x0d08
            java.lang.Object r2 = r5.next()
            java.lang.Integer r2 = (java.lang.Integer) r2
            com.google.android.gms.internal.measurement.zzbs$zzj$zza r11 = com.google.android.gms.internal.measurement.zzbs.zzj.zzqo()
            int r12 = r2.intValue()
            com.google.android.gms.internal.measurement.zzbs$zzj$zza r11 = r11.zzal(r12)
            java.lang.Object r2 = r3.get(r2)
            java.util.List r2 = (java.util.List) r2
            if (r2 == 0) goto L_0x0cfc
            java.util.Collections.sort(r2)
            java.util.Iterator r12 = r2.iterator()
        L_0x0ce8:
            boolean r2 = r12.hasNext()
            if (r2 == 0) goto L_0x0cfc
            java.lang.Object r2 = r12.next()
            java.lang.Long r2 = (java.lang.Long) r2
            long r14 = r2.longValue()
            r11.zzbj(r14)
            goto L_0x0ce8
        L_0x0cfc:
            com.google.android.gms.internal.measurement.zzgi r2 = r11.zzug()
            com.google.android.gms.internal.measurement.zzey r2 = (com.google.android.gms.internal.measurement.zzey) r2
            com.google.android.gms.internal.measurement.zzbs$zzj r2 = (com.google.android.gms.internal.measurement.zzbs.zzj) r2
            r4.add(r2)
            goto L_0x0cc1
        L_0x0d08:
            r2 = r4
            goto L_0x0c02
        L_0x0d0b:
            java.util.ArrayList r4 = new java.util.ArrayList
            r4.<init>(r2)
            android.support.v4.util.ArrayMap r11 = new android.support.v4.util.ArrayMap
            r11.<init>()
            java.util.Iterator r3 = r3.iterator()
        L_0x0d19:
            boolean r2 = r3.hasNext()
            if (r2 == 0) goto L_0x0d4b
            java.lang.Object r2 = r3.next()
            com.google.android.gms.internal.measurement.zzbs$zzj r2 = (com.google.android.gms.internal.measurement.zzbs.zzj) r2
            boolean r5 = r2.zzme()
            if (r5 == 0) goto L_0x0d19
            int r5 = r2.zzql()
            if (r5 <= 0) goto L_0x0d19
            int r5 = r2.getIndex()
            java.lang.Integer r5 = java.lang.Integer.valueOf(r5)
            int r12 = r2.zzql()
            int r12 = r12 + -1
            long r12 = r2.zzai(r12)
            java.lang.Long r2 = java.lang.Long.valueOf(r12)
            r11.put(r5, r2)
            goto L_0x0d19
        L_0x0d4b:
            r2 = 0
            r5 = r2
        L_0x0d4d:
            int r2 = r4.size()
            if (r5 >= r2) goto L_0x0dad
            java.lang.Object r2 = r4.get(r5)
            r3 = r2
            com.google.android.gms.internal.measurement.zzbs$zzj r3 = (com.google.android.gms.internal.measurement.zzbs.zzj) r3
            boolean r2 = r3.zzme()
            if (r2 == 0) goto L_0x0dab
            int r2 = r3.getIndex()
            java.lang.Integer r2 = java.lang.Integer.valueOf(r2)
        L_0x0d68:
            java.lang.Object r2 = r11.remove(r2)
            java.lang.Long r2 = (java.lang.Long) r2
            if (r2 == 0) goto L_0x0da7
            java.util.ArrayList r12 = new java.util.ArrayList
            r12.<init>()
            long r14 = r2.longValue()
            r13 = 0
            long r16 = r3.zzai(r13)
            int r13 = (r14 > r16 ? 1 : (r14 == r16 ? 0 : -1))
            if (r13 >= 0) goto L_0x0d85
            r12.add(r2)
        L_0x0d85:
            java.util.List r2 = r3.zzqk()
            r12.addAll(r2)
            com.google.android.gms.internal.measurement.zzey$zza r2 = r3.zzuj()
            com.google.android.gms.internal.measurement.zzey$zza r2 = (com.google.android.gms.internal.measurement.zzey.zza) r2
            com.google.android.gms.internal.measurement.zzbs$zzj$zza r2 = (com.google.android.gms.internal.measurement.zzbs.zzj.zza) r2
            com.google.android.gms.internal.measurement.zzbs$zzj$zza r2 = r2.zzqw()
            com.google.android.gms.internal.measurement.zzbs$zzj$zza r2 = r2.zzr(r12)
            com.google.android.gms.internal.measurement.zzgi r2 = r2.zzug()
            com.google.android.gms.internal.measurement.zzey r2 = (com.google.android.gms.internal.measurement.zzey) r2
            com.google.android.gms.internal.measurement.zzbs$zzj r2 = (com.google.android.gms.internal.measurement.zzbs.zzj) r2
            r4.set(r5, r2)
        L_0x0da7:
            int r2 = r5 + 1
            r5 = r2
            goto L_0x0d4d
        L_0x0dab:
            r2 = 0
            goto L_0x0d68
        L_0x0dad:
            java.util.Set r2 = r11.keySet()
            java.util.Iterator r3 = r2.iterator()
        L_0x0db5:
            boolean r2 = r3.hasNext()
            if (r2 == 0) goto L_0x0de7
            java.lang.Object r2 = r3.next()
            java.lang.Integer r2 = (java.lang.Integer) r2
            com.google.android.gms.internal.measurement.zzbs$zzj$zza r5 = com.google.android.gms.internal.measurement.zzbs.zzj.zzqo()
            int r12 = r2.intValue()
            com.google.android.gms.internal.measurement.zzbs$zzj$zza r5 = r5.zzal(r12)
            java.lang.Object r2 = r11.get(r2)
            java.lang.Long r2 = (java.lang.Long) r2
            long r12 = r2.longValue()
            com.google.android.gms.internal.measurement.zzbs$zzj$zza r2 = r5.zzbj(r12)
            com.google.android.gms.internal.measurement.zzgi r2 = r2.zzug()
            com.google.android.gms.internal.measurement.zzey r2 = (com.google.android.gms.internal.measurement.zzey) r2
            com.google.android.gms.internal.measurement.zzbs$zzj r2 = (com.google.android.gms.internal.measurement.zzbs.zzj) r2
            r4.add(r2)
            goto L_0x0db5
        L_0x0de7:
            r2 = r4
            goto L_0x0c18
        L_0x0dea:
            r2 = 0
            r7 = r2
            goto L_0x0916
        L_0x0dee:
            r11 = r2
            goto L_0x08aa
        L_0x0df1:
            r21 = r10
            r22 = r9
            r23 = r8
            r24 = r6
            r26 = r3
            goto L_0x045c
        L_0x0dfd:
            r11 = r3
            goto L_0x03c5
        L_0x0e00:
            r6 = r14
            goto L_0x06bb
        L_0x0e03:
            r9 = r3
            r10 = r4
            r11 = r5
            r12 = r6
            goto L_0x0693
        L_0x0e09:
            r9 = r3
            r10 = r4
            r11 = r5
            r12 = r6
            goto L_0x0693
        L_0x0e0f:
            r5 = 0
            r6 = r2
            goto L_0x062d
        L_0x0e13:
            r16 = r2
            goto L_0x04da
        L_0x0e17:
            r5 = r4
            r8 = r3
            goto L_0x0232
        L_0x0e1b:
            r5 = 0
            r9 = r5
            goto L_0x0212
        L_0x0e1f:
            r2 = 0
            r27 = r2
            goto L_0x007f
        L_0x0e24:
            return r7
        L_0x0e25:
            r6 = r5
            goto L_0x01c3
        */
        throw new UnsupportedOperationException("Method not decompiled: com.google.android.gms.measurement.internal.zzp.zza(java.lang.String, java.util.List, java.util.List):java.util.List");
    }

    /* access modifiers changed from: protected */
    public final boolean zzbk() {
        return false;
    }
}
