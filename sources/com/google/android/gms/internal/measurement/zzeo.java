package com.google.android.gms.internal.measurement;

import com.google.android.gms.internal.measurement.zzeq;
import java.io.IOException;
import java.util.ArrayList;
import java.util.Iterator;
import java.util.List;
import java.util.Map.Entry;

final class zzeo<FieldDescriptorType extends zzeq<FieldDescriptorType>> {
    private static final zzeo zzafa = new zzeo(true);
    final zzhc<FieldDescriptorType, Object> zzaex = zzhc.zzce(16);
    private boolean zzaey;
    private boolean zzaez = false;

    private zzeo() {
    }

    private zzeo(boolean z) {
        zzry();
    }

    static int zza(zzig zzig, int i, Object obj) {
        int i2;
        int zzbi = zzee.zzbi(i);
        if (zzig == zzig.GROUP) {
            zzez.zzf((zzgi) obj);
            i2 = zzbi << 1;
        } else {
            i2 = zzbi;
        }
        return i2 + zzb(zzig, obj);
    }

    private final Object zza(FieldDescriptorType fielddescriptortype) {
        Object obj = this.zzaex.get(fielddescriptortype);
        return obj instanceof zzfj ? zzfj.zzvc() : obj;
    }

    static void zza(zzee zzee, zzig zzig, int i, Object obj) throws IOException {
        if (zzig == zzig.GROUP) {
            zzez.zzf((zzgi) obj);
            zzgi zzgi = (zzgi) obj;
            zzee.zzb(i, 3);
            zzgi.zzb(zzee);
            zzee.zzb(i, 4);
            return;
        }
        zzee.zzb(i, zzig.zzxa());
        switch (zzig) {
            case DOUBLE:
                zzee.zzd(((Double) obj).doubleValue());
                return;
            case FLOAT:
                zzee.zza(((Float) obj).floatValue());
                return;
            case INT64:
                zzee.zzbn(((Long) obj).longValue());
                return;
            case UINT64:
                zzee.zzbn(((Long) obj).longValue());
                return;
            case INT32:
                zzee.zzbe(((Integer) obj).intValue());
                return;
            case FIXED64:
                zzee.zzbp(((Long) obj).longValue());
                return;
            case FIXED32:
                zzee.zzbh(((Integer) obj).intValue());
                return;
            case BOOL:
                zzee.zzq(((Boolean) obj).booleanValue());
                return;
            case GROUP:
                ((zzgi) obj).zzb(zzee);
                return;
            case MESSAGE:
                zzee.zzb((zzgi) obj);
                return;
            case STRING:
                if (obj instanceof zzdp) {
                    zzee.zza((zzdp) obj);
                    return;
                } else {
                    zzee.zzdr((String) obj);
                    return;
                }
            case BYTES:
                if (obj instanceof zzdp) {
                    zzee.zza((zzdp) obj);
                    return;
                }
                byte[] bArr = (byte[]) obj;
                zzee.zze(bArr, 0, bArr.length);
                return;
            case UINT32:
                zzee.zzbf(((Integer) obj).intValue());
                return;
            case SFIXED32:
                zzee.zzbh(((Integer) obj).intValue());
                return;
            case SFIXED64:
                zzee.zzbp(((Long) obj).longValue());
                return;
            case SINT32:
                zzee.zzbg(((Integer) obj).intValue());
                return;
            case SINT64:
                zzee.zzbo(((Long) obj).longValue());
                return;
            case ENUM:
                if (obj instanceof zzfc) {
                    zzee.zzbe(((zzfc) obj).zzlg());
                    return;
                } else {
                    zzee.zzbe(((Integer) obj).intValue());
                    return;
                }
            default:
                return;
        }
    }

    /* JADX WARNING: type inference failed for: r1v0, types: [java.lang.Object] */
    /* JADX WARNING: type inference failed for: r1v1 */
    /* JADX WARNING: Multi-variable type inference failed */
    /* Code decompiled incorrectly, please refer to instructions dump. */
    private final void zza(FieldDescriptorType r7, java.lang.Object r8) {
        /*
            r6 = this;
            boolean r0 = r7.zzty()
            if (r0 == 0) goto L_0x0034
            boolean r0 = r8 instanceof java.util.List
            if (r0 != 0) goto L_0x0012
            java.lang.IllegalArgumentException r0 = new java.lang.IllegalArgumentException
            java.lang.String r1 = "Wrong object type used with protocol message reflection."
            r0.<init>(r1)
            throw r0
        L_0x0012:
            java.util.ArrayList r1 = new java.util.ArrayList
            r1.<init>()
            java.util.List r8 = (java.util.List) r8
            r1.addAll(r8)
            r0 = r1
            java.util.ArrayList r0 = (java.util.ArrayList) r0
            int r3 = r0.size()
            r2 = 0
        L_0x0024:
            if (r2 >= r3) goto L_0x003c
            java.lang.Object r4 = r0.get(r2)
            int r2 = r2 + 1
            com.google.android.gms.internal.measurement.zzig r5 = r7.zztw()
            zza(r5, r4)
            goto L_0x0024
        L_0x0034:
            com.google.android.gms.internal.measurement.zzig r0 = r7.zztw()
            zza(r0, r8)
            r1 = r8
        L_0x003c:
            boolean r0 = r1 instanceof com.google.android.gms.internal.measurement.zzfj
            if (r0 == 0) goto L_0x0043
            r0 = 1
            r6.zzaez = r0
        L_0x0043:
            com.google.android.gms.internal.measurement.zzhc<FieldDescriptorType, java.lang.Object> r0 = r6.zzaex
            r0.put(r7, r1)
            return
        */
        throw new UnsupportedOperationException("Method not decompiled: com.google.android.gms.internal.measurement.zzeo.zza(com.google.android.gms.internal.measurement.zzeq, java.lang.Object):void");
    }

    private static void zza(zzig zzig, Object obj) {
        boolean z = false;
        zzez.checkNotNull(obj);
        switch (zzig.zzwz()) {
            case INT:
                z = obj instanceof Integer;
                break;
            case LONG:
                z = obj instanceof Long;
                break;
            case FLOAT:
                z = obj instanceof Float;
                break;
            case DOUBLE:
                z = obj instanceof Double;
                break;
            case BOOLEAN:
                z = obj instanceof Boolean;
                break;
            case STRING:
                z = obj instanceof String;
                break;
            case BYTE_STRING:
                if ((obj instanceof zzdp) || (obj instanceof byte[])) {
                    z = true;
                    break;
                }
            case ENUM:
                if ((obj instanceof Integer) || (obj instanceof zzfc)) {
                    z = true;
                    break;
                }
            case MESSAGE:
                if ((obj instanceof zzgi) || (obj instanceof zzfj)) {
                    z = true;
                    break;
                }
        }
        if (!z) {
            throw new IllegalArgumentException("Wrong object type used with protocol message reflection.");
        }
    }

    public static int zzb(zzeq<?> zzeq, Object obj) {
        int i = 0;
        zzig zztw = zzeq.zztw();
        int zzlg = zzeq.zzlg();
        if (!zzeq.zzty()) {
            return zza(zztw, zzlg, obj);
        }
        if (zzeq.zztz()) {
            for (Object zzb : (List) obj) {
                i += zzb(zztw, zzb);
            }
            int zzbi = zzee.zzbi(zzlg);
            return i + zzbi + zzee.zzbq(i);
        }
        for (Object zza : (List) obj) {
            i += zza(zztw, zzlg, zza);
        }
        return i;
    }

    private static int zzb(zzig zzig, Object obj) {
        switch (zzig) {
            case DOUBLE:
                return zzee.zze(((Double) obj).doubleValue());
            case FLOAT:
                return zzee.zzb(((Float) obj).floatValue());
            case INT64:
                return zzee.zzbq(((Long) obj).longValue());
            case UINT64:
                return zzee.zzbr(((Long) obj).longValue());
            case INT32:
                return zzee.zzbj(((Integer) obj).intValue());
            case FIXED64:
                return zzee.zzbt(((Long) obj).longValue());
            case FIXED32:
                return zzee.zzbm(((Integer) obj).intValue());
            case BOOL:
                return zzee.zzr(((Boolean) obj).booleanValue());
            case GROUP:
                return zzee.zzd((zzgi) obj);
            case MESSAGE:
                return obj instanceof zzfj ? zzee.zza((zzfn) (zzfj) obj) : zzee.zzc((zzgi) obj);
            case STRING:
                return obj instanceof zzdp ? zzee.zzb((zzdp) obj) : zzee.zzds((String) obj);
            case BYTES:
                return obj instanceof zzdp ? zzee.zzb((zzdp) obj) : zzee.zzg((byte[]) obj);
            case UINT32:
                return zzee.zzbk(((Integer) obj).intValue());
            case SFIXED32:
                return zzee.zzbn(((Integer) obj).intValue());
            case SFIXED64:
                return zzee.zzbu(((Long) obj).longValue());
            case SINT32:
                return zzee.zzbl(((Integer) obj).intValue());
            case SINT64:
                return zzee.zzbs(((Long) obj).longValue());
            case ENUM:
                return obj instanceof zzfc ? zzee.zzbo(((zzfc) obj).zzlg()) : zzee.zzbo(((Integer) obj).intValue());
            default:
                throw new RuntimeException("There is no way to get here, but the compiler thinks otherwise.");
        }
    }

    private static boolean zzb(Entry<FieldDescriptorType, Object> entry) {
        zzeq zzeq = (zzeq) entry.getKey();
        if (zzeq.zztx() == zzij.MESSAGE) {
            if (zzeq.zzty()) {
                for (zzgi isInitialized : (List) entry.getValue()) {
                    if (!isInitialized.isInitialized()) {
                        return false;
                    }
                }
            } else {
                Object value = entry.getValue();
                if (value instanceof zzgi) {
                    if (!((zzgi) value).isInitialized()) {
                        return false;
                    }
                } else if (value instanceof zzfj) {
                    return true;
                } else {
                    throw new IllegalArgumentException("Wrong object type used with protocol message reflection.");
                }
            }
        }
        return true;
    }

    private final void zzc(Entry<FieldDescriptorType, Object> entry) {
        zzeq zzeq = (zzeq) entry.getKey();
        Object value = entry.getValue();
        if (value instanceof zzfj) {
            value = zzfj.zzvc();
        }
        if (zzeq.zzty()) {
            Object zza = zza((FieldDescriptorType) zzeq);
            if (zza == null) {
                zza = new ArrayList();
            }
            for (Object zzk : (List) value) {
                ((List) zza).add(zzk(zzk));
            }
            this.zzaex.put(zzeq, zza);
        } else if (zzeq.zztx() == zzij.MESSAGE) {
            Object zza2 = zza((FieldDescriptorType) zzeq);
            if (zza2 == null) {
                this.zzaex.put(zzeq, zzk(value));
            } else {
                this.zzaex.put(zzeq, zza2 instanceof zzgn ? zzeq.zza((zzgn) zza2, (zzgn) value) : zzeq.zza(((zzgi) zza2).zzuo(), (zzgi) value).zzug());
            }
        } else {
            this.zzaex.put(zzeq, zzk(value));
        }
    }

    private static int zzd(Entry<FieldDescriptorType, Object> entry) {
        zzeq zzeq = (zzeq) entry.getKey();
        Object value = entry.getValue();
        return (zzeq.zztx() != zzij.MESSAGE || zzeq.zzty() || zzeq.zztz()) ? zzb(zzeq, value) : value instanceof zzfj ? zzee.zzb(((zzeq) entry.getKey()).zzlg(), (zzfn) (zzfj) value) : zzee.zzd(((zzeq) entry.getKey()).zzlg(), (zzgi) value);
    }

    private static Object zzk(Object obj) {
        if (obj instanceof zzgn) {
            return ((zzgn) obj).zzvu();
        }
        if (!(obj instanceof byte[])) {
            return obj;
        }
        byte[] bArr = (byte[]) obj;
        byte[] bArr2 = new byte[bArr.length];
        System.arraycopy(bArr, 0, bArr2, 0, bArr.length);
        return bArr2;
    }

    public static <T extends zzeq<T>> zzeo<T> zztr() {
        return zzafa;
    }

    public final /* synthetic */ Object clone() throws CloneNotSupportedException {
        zzeo zzeo = new zzeo();
        int i = 0;
        while (true) {
            int i2 = i;
            if (i2 >= this.zzaex.zzwh()) {
                break;
            }
            Entry zzcf = this.zzaex.zzcf(i2);
            zzeo.zza((FieldDescriptorType) (zzeq) zzcf.getKey(), zzcf.getValue());
            i = i2 + 1;
        }
        for (Entry entry : this.zzaex.zzwi()) {
            zzeo.zza((FieldDescriptorType) (zzeq) entry.getKey(), entry.getValue());
        }
        zzeo.zzaez = this.zzaez;
        return zzeo;
    }

    /* access modifiers changed from: 0000 */
    public final Iterator<Entry<FieldDescriptorType, Object>> descendingIterator() {
        return this.zzaez ? new zzfo(this.zzaex.zzwj().iterator()) : this.zzaex.zzwj().iterator();
    }

    public final boolean equals(Object obj) {
        if (this == obj) {
            return true;
        }
        if (!(obj instanceof zzeo)) {
            return false;
        }
        return this.zzaex.equals(((zzeo) obj).zzaex);
    }

    public final int hashCode() {
        return this.zzaex.hashCode();
    }

    public final boolean isImmutable() {
        return this.zzaey;
    }

    public final boolean isInitialized() {
        for (int i = 0; i < this.zzaex.zzwh(); i++) {
            if (!zzb(this.zzaex.zzcf(i))) {
                return false;
            }
        }
        for (Entry zzb : this.zzaex.zzwi()) {
            if (!zzb(zzb)) {
                return false;
            }
        }
        return true;
    }

    public final Iterator<Entry<FieldDescriptorType, Object>> iterator() {
        return this.zzaez ? new zzfo(this.zzaex.entrySet().iterator()) : this.zzaex.entrySet().iterator();
    }

    public final void zza(zzeo<FieldDescriptorType> zzeo) {
        for (int i = 0; i < zzeo.zzaex.zzwh(); i++) {
            zzc(zzeo.zzaex.zzcf(i));
        }
        for (Entry zzc : zzeo.zzaex.zzwi()) {
            zzc(zzc);
        }
    }

    public final void zzry() {
        if (!this.zzaey) {
            this.zzaex.zzry();
            this.zzaey = true;
        }
    }

    public final int zzts() {
        int i;
        int i2 = 0;
        int i3 = 0;
        while (true) {
            i = i2;
            if (i3 >= this.zzaex.zzwh()) {
                break;
            }
            i2 = zzd(this.zzaex.zzcf(i3)) + i;
            i3++;
        }
        for (Entry zzd : this.zzaex.zzwi()) {
            i += zzd(zzd);
        }
        return i;
    }
}
