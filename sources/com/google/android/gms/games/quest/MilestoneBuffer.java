package com.google.android.gms.games.quest;

import com.google.android.gms.common.data.AbstractDataBuffer;

@Deprecated
public final class MilestoneBuffer extends AbstractDataBuffer<Milestone> {
    public final Milestone get(int i) {
        return new zzb(this.zzfkz, i);
    }

    /* renamed from: get */
    public final /* synthetic */ Object m1013get(int i) {
        throw new NoSuchMethodError();
    }
}
