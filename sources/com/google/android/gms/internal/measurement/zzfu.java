package com.google.android.gms.internal.measurement;

import java.util.ArrayList;
import java.util.Collections;
import java.util.List;

final class zzfu extends zzfs {
    private static final Class<?> zzajv = Collections.unmodifiableList(Collections.emptyList()).getClass();

    private zzfu() {
        super();
    }

    private static <L> List<L> zza(Object obj, long j, int i) {
        List<L> zzd = zzd(obj, j);
        if (zzd.isEmpty()) {
            List<L> arrayList = zzd instanceof zzfp ? new zzfq<>(i) : (!(zzd instanceof zzgu) || !(zzd instanceof zzff)) ? new ArrayList<>(i) : ((zzff) zzd).zzap(i);
            zzhv.zza(obj, j, (Object) arrayList);
            return arrayList;
        } else if (zzajv.isAssignableFrom(zzd.getClass())) {
            ArrayList arrayList2 = new ArrayList(zzd.size() + i);
            arrayList2.addAll(zzd);
            zzhv.zza(obj, j, (Object) arrayList2);
            return arrayList2;
        } else if (zzd instanceof zzhu) {
            zzfq zzfq = new zzfq(zzd.size() + i);
            zzfq.addAll((zzhu) zzd);
            zzhv.zza(obj, j, (Object) zzfq);
            return zzfq;
        } else if (!(zzd instanceof zzgu) || !(zzd instanceof zzff) || ((zzff) zzd).zzrx()) {
            return zzd;
        } else {
            zzff zzap = ((zzff) zzd).zzap(zzd.size() + i);
            zzhv.zza(obj, j, (Object) zzap);
            return zzap;
        }
    }

    private static <E> List<E> zzd(Object obj, long j) {
        return (List) zzhv.zzp(obj, j);
    }

    /* access modifiers changed from: 0000 */
    public final <L> List<L> zza(Object obj, long j) {
        return zza(obj, j, 10);
    }

    /* access modifiers changed from: 0000 */
    public final <E> void zza(Object obj, Object obj2, long j) {
        List zzd = zzd(obj2, j);
        List zza = zza(obj, j, zzd.size());
        int size = zza.size();
        int size2 = zzd.size();
        if (size > 0 && size2 > 0) {
            zza.addAll(zzd);
        }
        if (size > 0) {
            zzd = zza;
        }
        zzhv.zza(obj, j, (Object) zzd);
    }

    /* access modifiers changed from: 0000 */
    public final void zzb(Object obj, long j) {
        Object unmodifiableList;
        List list = (List) zzhv.zzp(obj, j);
        if (list instanceof zzfp) {
            unmodifiableList = ((zzfp) list).zzvg();
        } else if (zzajv.isAssignableFrom(list.getClass())) {
            return;
        } else {
            if (!(list instanceof zzgu) || !(list instanceof zzff)) {
                unmodifiableList = Collections.unmodifiableList(list);
            } else if (((zzff) list).zzrx()) {
                ((zzff) list).zzry();
                return;
            } else {
                return;
            }
        }
        zzhv.zza(obj, j, unmodifiableList);
    }
}
