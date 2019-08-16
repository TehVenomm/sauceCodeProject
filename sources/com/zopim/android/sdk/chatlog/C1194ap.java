package com.zopim.android.sdk.chatlog;

import android.util.Log;
import com.zopim.android.sdk.api.Logger;
import com.zopim.android.sdk.model.Agent;
import java.util.Map;
import java.util.Map.Entry;

/* renamed from: com.zopim.android.sdk.chatlog.ap */
class C1194ap implements Runnable {

    /* renamed from: a */
    final /* synthetic */ Map f833a;

    /* renamed from: b */
    final /* synthetic */ C1193ao f834b;

    C1194ap(C1193ao aoVar, Map map) {
        this.f834b = aoVar;
        this.f833a = map;
    }

    public void run() {
        if (!(this.f834b.f832a.getListAdapter() instanceof C1211i)) {
            Log.w(ZopimChatLogFragment.LOG_TAG, "Aborting update. Adapter must be of type " + C1211i.class);
            return;
        }
        for (Entry entry : this.f833a.entrySet()) {
            String str = (String) entry.getKey();
            Agent agent = (Agent) entry.getValue();
            this.f834b.m689a(str, agent);
            this.f834b.m691b(str, agent);
        }
        Logger.m571d(ZopimChatLogFragment.LOG_TAG, "Agents updated");
    }
}
