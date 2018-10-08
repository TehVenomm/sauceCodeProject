package com.google.android.gms.internal;

import java.util.Collections;
import java.util.List;
import java.util.Map.Entry;

final class zzeff extends zzefe<FieldDescriptorType, Object> {
    zzeff(int i) {
        super(i);
    }

    public final void zzbhq() {
        if (!isImmutable()) {
            for (int i = 0; i < zzccz(); i++) {
                Entry zzgw = zzgw(i);
                if (((zzeec) zzgw.getKey()).zzccl()) {
                    zzgw.setValue(Collections.unmodifiableList((List) zzgw.getValue()));
                }
            }
            for (Entry entry : zzcda()) {
                if (((zzeec) entry.getKey()).zzccl()) {
                    entry.setValue(Collections.unmodifiableList((List) entry.getValue()));
                }
            }
        }
        super.zzbhq();
    }
}
