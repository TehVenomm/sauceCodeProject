package com.google.android.gms.games.internal.api;

import com.google.android.gms.common.api.Status;
import com.google.android.gms.common.data.DataHolder;
import com.google.android.gms.games.quest.QuestBuffer;
import com.google.android.gms.games.quest.Quests.LoadQuestsResult;

final class zzbr implements LoadQuestsResult {
    private /* synthetic */ Status zzeik;

    zzbr(zzbq zzbq, Status status) {
        this.zzeik = status;
    }

    public final QuestBuffer getQuests() {
        return new QuestBuffer(DataHolder.zzbx(this.zzeik.getStatusCode()));
    }

    public final Status getStatus() {
        return this.zzeik;
    }

    public final void release() {
    }
}
