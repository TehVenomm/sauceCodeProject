package com.google.android.gms.internal.measurement;

import java.util.Collections;
import java.util.List;
import java.util.Map.Entry;

final class zzhb extends zzhc<FieldDescriptorType, Object> {
    zzhb(int i) {
        super(i, null);
    }

    public final void zzry() {
        if (!isImmutable()) {
            int i = 0;
            while (true) {
                int i2 = i;
                if (i2 >= zzwh()) {
                    break;
                }
                Entry zzcf = zzcf(i2);
                if (((zzeq) zzcf.getKey()).zzty()) {
                    zzcf.setValue(Collections.unmodifiableList((List) zzcf.getValue()));
                }
                i = i2 + 1;
            }
            for (Entry entry : zzwi()) {
                if (((zzeq) entry.getKey()).zzty()) {
                    entry.setValue(Collections.unmodifiableList((List) entry.getValue()));
                }
            }
        }
        super.zzry();
    }
}
