package com.google.android.gms.games.quest;

import com.google.android.gms.common.data.DataHolder;
import com.google.android.gms.common.data.zzg;

@Deprecated
public final class QuestBuffer extends zzg<Quest> {
    public QuestBuffer(DataHolder dataHolder) {
        super(dataHolder);
    }

    protected final String zzaiw() {
        return "external_quest_id";
    }

    protected final /* synthetic */ Object zzk(int i, int i2) {
        return new QuestRef(this.zzfkz, i, i2);
    }
}
