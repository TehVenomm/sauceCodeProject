package com.google.android.gms.internal.measurement;

import java.io.IOException;
import java.util.Iterator;
import java.util.List;
import java.util.RandomAccess;

final class zzgz {
    private static final Class<?> zzalg = zzwf();
    private static final zzhp<?, ?> zzalh = zzt(false);
    private static final zzhp<?, ?> zzali = zzt(true);
    private static final zzhp<?, ?> zzalj = new zzhr();

    static <UT, UB> UB zza(int i, int i2, UB ub, zzhp<UT, UB> zzhp) {
        if (ub == null) {
            ub = zzhp.zzwp();
        }
        zzhp.zza(ub, i, (long) i2);
        return ub;
    }

    static <UT, UB> UB zza(int i, List<Integer> list, zzfe zzfe, UB ub, zzhp<UT, UB> zzhp) {
        int i2;
        if (zzfe == null) {
            return ub;
        }
        if (list instanceof RandomAccess) {
            int size = list.size();
            int i3 = 0;
            int i4 = 0;
            UB ub2 = ub;
            while (i3 < size) {
                int intValue = ((Integer) list.get(i3)).intValue();
                if (zzfe.zzg(intValue)) {
                    if (i3 != i4) {
                        list.set(i4, Integer.valueOf(intValue));
                    }
                    i2 = i4 + 1;
                } else {
                    ub2 = zza(i, intValue, ub2, zzhp);
                    i2 = i4;
                }
                i3++;
                i4 = i2;
            }
            if (i4 == size) {
                return ub2;
            }
            list.subList(i4, size).clear();
            return ub2;
        }
        Iterator it = list.iterator();
        while (it.hasNext()) {
            int intValue2 = ((Integer) it.next()).intValue();
            if (!zzfe.zzg(intValue2)) {
                ub = zza(i, intValue2, ub, zzhp);
                it.remove();
            }
        }
        return ub;
    }

    public static void zza(int i, List<String> list, zzim zzim) throws IOException {
        if (list != null && !list.isEmpty()) {
            zzim.zza(i, list);
        }
    }

    public static void zza(int i, List<?> list, zzim zzim, zzgx zzgx) throws IOException {
        if (list != null && !list.isEmpty()) {
            zzim.zza(i, list, zzgx);
        }
    }

    public static void zza(int i, List<Double> list, zzim zzim, boolean z) throws IOException {
        if (list != null && !list.isEmpty()) {
            zzim.zzg(i, list, z);
        }
    }

    static <T, FT extends zzeq<FT>> void zza(zzen<FT> zzen, T t, T t2) {
        zzeo zzh = zzen.zzh(t2);
        if (!zzh.zzaex.isEmpty()) {
            zzen.zzi(t).zza(zzh);
        }
    }

    static <T> void zza(zzgb zzgb, T t, T t2, long j) {
        zzhv.zza((Object) t, j, zzgb.zzb(zzhv.zzp(t, j), zzhv.zzp(t2, j)));
    }

    static <T, UT, UB> void zza(zzhp<UT, UB> zzhp, T t, T t2) {
        zzhp.zze(t, zzhp.zzg(zzhp.zzx(t), zzhp.zzx(t2)));
    }

    static int zzaa(List<Integer> list) {
        int size = list.size();
        if (size == 0) {
            return 0;
        }
        if (list instanceof zzfa) {
            zzfa zzfa = (zzfa) list;
            int i = 0;
            for (int i2 = 0; i2 < size; i2++) {
                i += zzee.zzbl(zzfa.getInt(i2));
            }
            return i;
        }
        int i3 = 0;
        for (int i4 = 0; i4 < size; i4++) {
            i3 += zzee.zzbl(((Integer) list.get(i4)).intValue());
        }
        return i3;
    }

    static int zzab(List<?> list) {
        return list.size() << 2;
    }

    static int zzac(List<?> list) {
        return list.size() << 3;
    }

    static int zzad(List<?> list) {
        return list.size();
    }

    public static void zzb(int i, List<zzdp> list, zzim zzim) throws IOException {
        if (list != null && !list.isEmpty()) {
            zzim.zzb(i, list);
        }
    }

    public static void zzb(int i, List<?> list, zzim zzim, zzgx zzgx) throws IOException {
        if (list != null && !list.isEmpty()) {
            zzim.zzb(i, list, zzgx);
        }
    }

    public static void zzb(int i, List<Float> list, zzim zzim, boolean z) throws IOException {
        if (list != null && !list.isEmpty()) {
            zzim.zzf(i, list, z);
        }
    }

    static int zzc(int i, Object obj, zzgx zzgx) {
        return obj instanceof zzfn ? zzee.zza(i, (zzfn) obj) : zzee.zzb(i, (zzgi) obj, zzgx);
    }

    static int zzc(int i, List<?> list) {
        int size = list.size();
        if (size == 0) {
            return 0;
        }
        int zzbi = zzee.zzbi(i) * size;
        if (list instanceof zzfp) {
            zzfp zzfp = (zzfp) list;
            int i2 = 0;
            while (i2 < size) {
                Object zzbw = zzfp.zzbw(i2);
                i2++;
                zzbi = (zzbw instanceof zzdp ? zzee.zzb((zzdp) zzbw) : zzee.zzds((String) zzbw)) + zzbi;
            }
            return zzbi;
        }
        int i3 = 0;
        while (i3 < size) {
            Object obj = list.get(i3);
            i3++;
            zzbi = (obj instanceof zzdp ? zzee.zzb((zzdp) obj) : zzee.zzds((String) obj)) + zzbi;
        }
        return zzbi;
    }

    static int zzc(int i, List<?> list, zzgx zzgx) {
        int size = list.size();
        if (size == 0) {
            return 0;
        }
        int zzbi = zzee.zzbi(i) * size;
        int i2 = 0;
        while (i2 < size) {
            Object obj = list.get(i2);
            i2++;
            zzbi = (obj instanceof zzfn ? zzee.zza((zzfn) obj) : zzee.zzb((zzgi) obj, zzgx)) + zzbi;
        }
        return zzbi;
    }

    public static void zzc(int i, List<Long> list, zzim zzim, boolean z) throws IOException {
        if (list != null && !list.isEmpty()) {
            zzim.zzc(i, list, z);
        }
    }

    static int zzd(int i, List<zzdp> list) {
        int i2 = 0;
        int size = list.size();
        if (size == 0) {
            return 0;
        }
        int zzbi = size * zzee.zzbi(i);
        while (true) {
            int i3 = i2;
            if (i3 >= list.size()) {
                return zzbi;
            }
            zzbi += zzee.zzb((zzdp) list.get(i3));
            i2 = i3 + 1;
        }
    }

    static int zzd(int i, List<zzgi> list, zzgx zzgx) {
        int size = list.size();
        if (size == 0) {
            return 0;
        }
        int i2 = 0;
        for (int i3 = 0; i3 < size; i3++) {
            i2 += zzee.zzc(i, (zzgi) list.get(i3), zzgx);
        }
        return i2;
    }

    public static void zzd(int i, List<Long> list, zzim zzim, boolean z) throws IOException {
        if (list != null && !list.isEmpty()) {
            zzim.zzd(i, list, z);
        }
    }

    static boolean zzd(Object obj, Object obj2) {
        return obj == obj2 || (obj != null && obj.equals(obj2));
    }

    public static void zze(int i, List<Long> list, zzim zzim, boolean z) throws IOException {
        if (list != null && !list.isEmpty()) {
            zzim.zzn(i, list, z);
        }
    }

    public static void zzf(int i, List<Long> list, zzim zzim, boolean z) throws IOException {
        if (list != null && !list.isEmpty()) {
            zzim.zze(i, list, z);
        }
    }

    public static void zzg(int i, List<Long> list, zzim zzim, boolean z) throws IOException {
        if (list != null && !list.isEmpty()) {
            zzim.zzl(i, list, z);
        }
    }

    public static void zzg(Class<?> cls) {
        if (!zzey.class.isAssignableFrom(cls) && zzalg != null && !zzalg.isAssignableFrom(cls)) {
            throw new IllegalArgumentException("Message classes must extend GeneratedMessage or GeneratedMessageLite");
        }
    }

    public static void zzh(int i, List<Integer> list, zzim zzim, boolean z) throws IOException {
        if (list != null && !list.isEmpty()) {
            zzim.zza(i, list, z);
        }
    }

    public static void zzi(int i, List<Integer> list, zzim zzim, boolean z) throws IOException {
        if (list != null && !list.isEmpty()) {
            zzim.zzj(i, list, z);
        }
    }

    public static void zzj(int i, List<Integer> list, zzim zzim, boolean z) throws IOException {
        if (list != null && !list.isEmpty()) {
            zzim.zzm(i, list, z);
        }
    }

    public static void zzk(int i, List<Integer> list, zzim zzim, boolean z) throws IOException {
        if (list != null && !list.isEmpty()) {
            zzim.zzb(i, list, z);
        }
    }

    public static void zzl(int i, List<Integer> list, zzim zzim, boolean z) throws IOException {
        if (list != null && !list.isEmpty()) {
            zzim.zzk(i, list, z);
        }
    }

    public static void zzm(int i, List<Integer> list, zzim zzim, boolean z) throws IOException {
        if (list != null && !list.isEmpty()) {
            zzim.zzh(i, list, z);
        }
    }

    public static void zzn(int i, List<Boolean> list, zzim zzim, boolean z) throws IOException {
        if (list != null && !list.isEmpty()) {
            zzim.zzi(i, list, z);
        }
    }

    static int zzo(int i, List<Long> list, boolean z) {
        if (list.size() == 0) {
            return 0;
        }
        return zzu(list) + (list.size() * zzee.zzbi(i));
    }

    static int zzp(int i, List<Long> list, boolean z) {
        int size = list.size();
        if (size == 0) {
            return 0;
        }
        return (size * zzee.zzbi(i)) + zzv(list);
    }

    static int zzq(int i, List<Long> list, boolean z) {
        int size = list.size();
        if (size == 0) {
            return 0;
        }
        return (size * zzee.zzbi(i)) + zzw(list);
    }

    static int zzr(int i, List<Integer> list, boolean z) {
        int size = list.size();
        if (size == 0) {
            return 0;
        }
        return (size * zzee.zzbi(i)) + zzx(list);
    }

    static int zzs(int i, List<Integer> list, boolean z) {
        int size = list.size();
        if (size == 0) {
            return 0;
        }
        return (size * zzee.zzbi(i)) + zzy(list);
    }

    static int zzt(int i, List<Integer> list, boolean z) {
        int size = list.size();
        if (size == 0) {
            return 0;
        }
        return (size * zzee.zzbi(i)) + zzz(list);
    }

    private static zzhp<?, ?> zzt(boolean z) {
        try {
            Class zzwg = zzwg();
            if (zzwg == null) {
                return null;
            }
            return (zzhp) zzwg.getConstructor(new Class[]{Boolean.TYPE}).newInstance(new Object[]{Boolean.valueOf(z)});
        } catch (Throwable th) {
            return null;
        }
    }

    static int zzu(int i, List<Integer> list, boolean z) {
        int size = list.size();
        if (size == 0) {
            return 0;
        }
        return (size * zzee.zzbi(i)) + zzaa(list);
    }

    static int zzu(List<Long> list) {
        int size = list.size();
        if (size == 0) {
            return 0;
        }
        if (list instanceof zzfw) {
            zzfw zzfw = (zzfw) list;
            int i = 0;
            for (int i2 = 0; i2 < size; i2++) {
                i += zzee.zzbq(zzfw.getLong(i2));
            }
            return i;
        }
        int i3 = 0;
        for (int i4 = 0; i4 < size; i4++) {
            i3 += zzee.zzbq(((Long) list.get(i4)).longValue());
        }
        return i3;
    }

    static int zzv(int i, List<?> list, boolean z) {
        int size = list.size();
        if (size == 0) {
            return 0;
        }
        return zzee.zzj(i, 0) * size;
    }

    static int zzv(List<Long> list) {
        int size = list.size();
        if (size == 0) {
            return 0;
        }
        if (list instanceof zzfw) {
            zzfw zzfw = (zzfw) list;
            int i = 0;
            for (int i2 = 0; i2 < size; i2++) {
                i += zzee.zzbr(zzfw.getLong(i2));
            }
            return i;
        }
        int i3 = 0;
        for (int i4 = 0; i4 < size; i4++) {
            i3 += zzee.zzbr(((Long) list.get(i4)).longValue());
        }
        return i3;
    }

    static int zzw(int i, List<?> list, boolean z) {
        int size = list.size();
        if (size == 0) {
            return 0;
        }
        return size * zzee.zzg(i, 0);
    }

    static int zzw(List<Long> list) {
        int size = list.size();
        if (size == 0) {
            return 0;
        }
        if (list instanceof zzfw) {
            zzfw zzfw = (zzfw) list;
            int i = 0;
            for (int i2 = 0; i2 < size; i2++) {
                i += zzee.zzbs(zzfw.getLong(i2));
            }
            return i;
        }
        int i3 = 0;
        for (int i4 = 0; i4 < size; i4++) {
            i3 += zzee.zzbs(((Long) list.get(i4)).longValue());
        }
        return i3;
    }

    public static zzhp<?, ?> zzwc() {
        return zzalh;
    }

    public static zzhp<?, ?> zzwd() {
        return zzali;
    }

    public static zzhp<?, ?> zzwe() {
        return zzalj;
    }

    private static Class<?> zzwf() {
        try {
            return Class.forName("com.google.protobuf.GeneratedMessage");
        } catch (Throwable th) {
            return null;
        }
    }

    private static Class<?> zzwg() {
        try {
            return Class.forName("com.google.protobuf.UnknownFieldSetSchema");
        } catch (Throwable th) {
            return null;
        }
    }

    static int zzx(int i, List<?> list, boolean z) {
        int size = list.size();
        if (size == 0) {
            return 0;
        }
        return size * zzee.zzc(i, true);
    }

    static int zzx(List<Integer> list) {
        int size = list.size();
        if (size == 0) {
            return 0;
        }
        if (list instanceof zzfa) {
            zzfa zzfa = (zzfa) list;
            int i = 0;
            for (int i2 = 0; i2 < size; i2++) {
                i += zzee.zzbo(zzfa.getInt(i2));
            }
            return i;
        }
        int i3 = 0;
        for (int i4 = 0; i4 < size; i4++) {
            i3 += zzee.zzbo(((Integer) list.get(i4)).intValue());
        }
        return i3;
    }

    static int zzy(List<Integer> list) {
        int size = list.size();
        if (size == 0) {
            return 0;
        }
        if (list instanceof zzfa) {
            zzfa zzfa = (zzfa) list;
            int i = 0;
            for (int i2 = 0; i2 < size; i2++) {
                i += zzee.zzbj(zzfa.getInt(i2));
            }
            return i;
        }
        int i3 = 0;
        for (int i4 = 0; i4 < size; i4++) {
            i3 += zzee.zzbj(((Integer) list.get(i4)).intValue());
        }
        return i3;
    }

    static int zzz(List<Integer> list) {
        int size = list.size();
        if (size == 0) {
            return 0;
        }
        if (list instanceof zzfa) {
            zzfa zzfa = (zzfa) list;
            int i = 0;
            for (int i2 = 0; i2 < size; i2++) {
                i += zzee.zzbk(zzfa.getInt(i2));
            }
            return i;
        }
        int i3 = 0;
        for (int i4 = 0; i4 < size; i4++) {
            i3 += zzee.zzbk(((Integer) list.get(i4)).intValue());
        }
        return i3;
    }
}
