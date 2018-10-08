package com.google.android.gms.internal;

import java.util.ArrayList;
import java.util.Iterator;
import java.util.List;
import java.util.Map.Entry;

final class zzeea<FieldDescriptorType extends zzeec<FieldDescriptorType>> {
    private static final zzeea zzmyo = new zzeea(true);
    private boolean zzkeu;
    private final zzefe<FieldDescriptorType, Object> zzmym = zzefe.zzgv(16);
    private boolean zzmyn = false;

    private zzeea() {
    }

    private zzeea(boolean z) {
        if (!this.zzkeu) {
            this.zzmym.zzbhq();
            this.zzkeu = true;
        }
    }

    private void zza(FieldDescriptorType fieldDescriptorType, Object obj) {
        Object obj2;
        if (!fieldDescriptorType.zzccl()) {
            zza(fieldDescriptorType.zzcck(), obj);
            obj2 = obj;
        } else if (obj instanceof List) {
            obj2 = new ArrayList();
            obj2.addAll((List) obj);
            ArrayList arrayList = (ArrayList) obj2;
            int size = arrayList.size();
            int i = 0;
            while (i < size) {
                Object obj3 = arrayList.get(i);
                i++;
                zza(fieldDescriptorType.zzcck(), obj3);
            }
        } else {
            throw new IllegalArgumentException("Wrong object type used with protocol message reflection.");
        }
        if (obj2 instanceof zzeet) {
            this.zzmyn = true;
        }
        this.zzmym.zza((Comparable) fieldDescriptorType, obj2);
    }

    private static void zza(zzefz zzefz, Object obj) {
        boolean z = false;
        zzeen.zzu(obj);
        switch (zzeeb.zzmyp[zzefz.zzcdq().ordinal()]) {
            case 1:
                z = obj instanceof Integer;
                break;
            case 2:
                z = obj instanceof Long;
                break;
            case 3:
                z = obj instanceof Float;
                break;
            case 4:
                z = obj instanceof Double;
                break;
            case 5:
                z = obj instanceof Boolean;
                break;
            case 6:
                z = obj instanceof String;
                break;
            case 7:
                if ((obj instanceof zzedk) || (obj instanceof byte[])) {
                    z = true;
                    break;
                }
            case 8:
                if ((obj instanceof Integer) || (obj instanceof zzeeo)) {
                    z = true;
                    break;
                }
            case 9:
                if ((obj instanceof zzeey) || (obj instanceof zzeet)) {
                    z = true;
                    break;
                }
        }
        if (!z) {
            throw new IllegalArgumentException("Wrong object type used with protocol message reflection.");
        }
    }

    public static <T extends zzeec<T>> zzeea<T> zzccj() {
        return new zzeea();
    }

    public final /* synthetic */ Object clone() throws CloneNotSupportedException {
        zzeea zzeea = new zzeea();
        for (int i = 0; i < this.zzmym.zzccz(); i++) {
            Entry zzgw = this.zzmym.zzgw(i);
            zzeea.zza((zzeec) zzgw.getKey(), zzgw.getValue());
        }
        for (Entry entry : this.zzmym.zzcda()) {
            zzeea.zza((zzeec) entry.getKey(), entry.getValue());
        }
        zzeea.zzmyn = this.zzmyn;
        return zzeea;
    }

    public final boolean equals(Object obj) {
        if (this == obj) {
            return true;
        }
        if (!(obj instanceof zzeea)) {
            return false;
        }
        return this.zzmym.equals(((zzeea) obj).zzmym);
    }

    public final int hashCode() {
        return this.zzmym.hashCode();
    }

    public final Iterator<Entry<FieldDescriptorType, Object>> iterator() {
        return this.zzmyn ? new zzeew(this.zzmym.entrySet().iterator()) : this.zzmym.entrySet().iterator();
    }
}
