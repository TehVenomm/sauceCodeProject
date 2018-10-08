package com.zopim.android.sdk.chatlog;

import android.util.Log;
import com.zopim.android.sdk.api.Logger;
import com.zopim.android.sdk.model.Agent;
import java.util.Map;
import java.util.Map.Entry;

class ap implements Runnable {
    /* renamed from: a */
    final /* synthetic */ Map f789a;
    /* renamed from: b */
    final /* synthetic */ ao f790b;

    ap(ao aoVar, Map map) {
        this.f790b = aoVar;
        this.f789a = map;
    }

    public void run() {
        if (this.f790b.f788a.getListAdapter() instanceof C0842i) {
            for (Entry entry : this.f789a.entrySet()) {
                String str = (String) entry.getKey();
                Agent agent = (Agent) entry.getValue();
                this.f790b.m676a(str, agent);
                this.f790b.m678b(str, agent);
            }
            Logger.m558d(ZopimChatLogFragment.LOG_TAG, "Agents updated");
            return;
        }
        Log.w(ZopimChatLogFragment.LOG_TAG, "Aborting update. Adapter must be of type " + C0842i.class);
    }
}
