package com.google.android.gms.internal.measurement;

import com.google.android.gms.internal.measurement.zzdf;
import com.google.android.gms.internal.measurement.zzdh;
import java.io.IOException;
import java.util.ArrayList;
import java.util.Collection;
import java.util.List;

public abstract class zzdf<MessageType extends zzdf<MessageType, BuilderType>, BuilderType extends zzdh<MessageType, BuilderType>> implements zzgi {
    private static boolean zzacu = false;
    protected int zzact = 0;

    protected static <T> void zza(Iterable<T> iterable, List<? super T> list) {
        zzez.checkNotNull(iterable);
        if (iterable instanceof zzfp) {
            List zzvf = ((zzfp) iterable).zzvf();
            zzfp zzfp = (zzfp) list;
            int size = list.size();
            for (Object next : zzvf) {
                if (next == null) {
                    String str = "Element at index " + (zzfp.size() - size) + " is null.";
                    for (int size2 = zzfp.size() - 1; size2 >= size; size2--) {
                        zzfp.remove(size2);
                    }
                    throw new NullPointerException(str);
                } else if (next instanceof zzdp) {
                    zzfp.zzc((zzdp) next);
                } else {
                    zzfp.add((String) next);
                }
            }
        } else if (iterable instanceof zzgu) {
            list.addAll((Collection) iterable);
        } else {
            if ((list instanceof ArrayList) && (iterable instanceof Collection)) {
                ((ArrayList) list).ensureCapacity(((Collection) iterable).size() + list.size());
            }
            int size3 = list.size();
            for (Object next2 : iterable) {
                if (next2 == null) {
                    String str2 = "Element at index " + (list.size() - size3) + " is null.";
                    for (int size4 = list.size() - 1; size4 >= size3; size4--) {
                        list.remove(size4);
                    }
                    throw new NullPointerException(str2);
                }
                list.add(next2);
            }
        }
    }

    public final byte[] toByteArray() {
        try {
            byte[] bArr = new byte[zzuk()];
            zzee zzf = zzee.zzf(bArr);
            zzb(zzf);
            zzf.zzth();
            return bArr;
        } catch (IOException e) {
            String name = getClass().getName();
            throw new RuntimeException(new StringBuilder(String.valueOf(name).length() + 62 + String.valueOf("byte array").length()).append("Serializing ").append(name).append(" to a ").append("byte array").append(" threw an IOException (should never happen).").toString(), e);
        }
    }

    /* access modifiers changed from: 0000 */
    public void zzam(int i) {
        throw new UnsupportedOperationException();
    }

    public final zzdp zzrs() {
        try {
            zzdx zzas = zzdp.zzas(zzuk());
            zzb(zzas.zzsf());
            return zzas.zzse();
        } catch (IOException e) {
            String name = getClass().getName();
            throw new RuntimeException(new StringBuilder(String.valueOf(name).length() + 62 + String.valueOf("ByteString").length()).append("Serializing ").append(name).append(" to a ").append("ByteString").append(" threw an IOException (should never happen).").toString(), e);
        }
    }

    /* access modifiers changed from: 0000 */
    public int zzrt() {
        throw new UnsupportedOperationException();
    }
}
