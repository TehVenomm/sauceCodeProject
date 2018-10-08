package com.google.android.gms.plus.internal.model.moments;

import android.os.Parcel;
import com.google.android.gms.common.data.DataHolder;
import com.google.android.gms.plus.model.moments.ItemScope;
import com.google.android.gms.plus.model.moments.Moment;

public final class zzc extends com.google.android.gms.common.data.zzc implements Moment {
    private MomentEntity zzaAA;

    public zzc(DataHolder dataHolder, int i) {
        super(dataHolder, i);
    }

    private MomentEntity zzvO() {
        synchronized (this) {
            if (this.zzaAA == null) {
                byte[] byteArray = getByteArray("momentImpl");
                Parcel obtain = Parcel.obtain();
                obtain.unmarshall(byteArray, 0, byteArray.length);
                obtain.setDataPosition(0);
                this.zzaAA = MomentEntity.CREATOR.zzeT(obtain);
                obtain.recycle();
            }
        }
        return this.zzaAA;
    }

    public /* synthetic */ Object freeze() {
        return zzvN();
    }

    public String getId() {
        return zzvO().getId();
    }

    public ItemScope getResult() {
        return zzvO().getResult();
    }

    public String getStartDate() {
        return zzvO().getStartDate();
    }

    public ItemScope getTarget() {
        return zzvO().getTarget();
    }

    public String getType() {
        return zzvO().getType();
    }

    public boolean hasId() {
        return zzvO().hasId();
    }

    public boolean hasResult() {
        return zzvO().hasResult();
    }

    public boolean hasStartDate() {
        return zzvO().hasStartDate();
    }

    public boolean hasTarget() {
        return zzvO().hasTarget();
    }

    public boolean hasType() {
        return zzvO().hasType();
    }

    public MomentEntity zzvN() {
        return zzvO();
    }
}
