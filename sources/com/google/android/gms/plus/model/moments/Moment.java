package com.google.android.gms.plus.model.moments;

import com.google.android.gms.common.data.Freezable;
import com.google.android.gms.plus.internal.model.moments.ItemScopeEntity;
import com.google.android.gms.plus.internal.model.moments.MomentEntity;
import java.util.HashSet;
import java.util.Set;

public interface Moment extends Freezable<Moment> {

    public static class Builder {
        private String zzAV;
        private String zzGM;
        private String zzaAq;
        private ItemScopeEntity zzaAy;
        private ItemScopeEntity zzaAz;
        private final Set<Integer> zzazD = new HashSet();

        public Moment build() {
            return new MomentEntity(this.zzazD, this.zzGM, this.zzaAy, this.zzaAq, this.zzaAz, this.zzAV);
        }

        public Builder setId(String str) {
            this.zzGM = str;
            this.zzazD.add(Integer.valueOf(2));
            return this;
        }

        public Builder setResult(ItemScope itemScope) {
            this.zzaAy = (ItemScopeEntity) itemScope;
            this.zzazD.add(Integer.valueOf(4));
            return this;
        }

        public Builder setStartDate(String str) {
            this.zzaAq = str;
            this.zzazD.add(Integer.valueOf(5));
            return this;
        }

        public Builder setTarget(ItemScope itemScope) {
            this.zzaAz = (ItemScopeEntity) itemScope;
            this.zzazD.add(Integer.valueOf(6));
            return this;
        }

        public Builder setType(String str) {
            this.zzAV = str;
            this.zzazD.add(Integer.valueOf(7));
            return this;
        }
    }

    String getId();

    ItemScope getResult();

    String getStartDate();

    ItemScope getTarget();

    String getType();

    boolean hasId();

    boolean hasResult();

    boolean hasStartDate();

    boolean hasTarget();

    boolean hasType();
}
