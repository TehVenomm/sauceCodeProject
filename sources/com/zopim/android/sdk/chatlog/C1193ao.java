package com.zopim.android.sdk.chatlog;

import android.util.Log;
import com.zopim.android.sdk.api.Logger;
import com.zopim.android.sdk.data.observers.AgentsObserver;
import com.zopim.android.sdk.model.Agent;
import java.util.Map;

/* renamed from: com.zopim.android.sdk.chatlog.ao */
class C1193ao extends AgentsObserver {

    /* renamed from: a */
    final /* synthetic */ ZopimChatLogFragment f832a;

    C1193ao(ZopimChatLogFragment zopimChatLogFragment) {
        this.f832a = zopimChatLogFragment;
    }

    /* access modifiers changed from: private */
    /* renamed from: a */
    public void m689a(String str, Agent agent) {
        if (agent.isTyping() == null) {
            Log.d(ZopimChatLogFragment.LOG_TAG, "Can't update agent typing while typing event is null");
            return;
        }
        Logger.m577v(ZopimChatLogFragment.LOG_TAG, "Agent " + agent.getDisplayName() + " typing " + agent.isTyping());
        C1209g gVar = new C1209g();
        gVar.f855b = agent.isTyping().booleanValue();
        gVar.f854a = agent.getAvatarUri();
        gVar.f798l = Long.valueOf(System.currentTimeMillis());
        gVar.f797k = str;
        C1211i access$1500 = this.f832a.getListAdapter();
        C1178aa b = access$1500.mo20759b(access$1500.getItemCount() - 1);
        if (b instanceof C1209g) {
            ((C1209g) b).f855b = agent.isTyping().booleanValue();
        } else {
            access$1500.mo20758a((C1178aa) gVar);
        }
        access$1500.notifyItemChanged(access$1500.getItemCount() - 1);
        this.f832a.mRecyclerView.getLayoutManager().scrollToPosition(access$1500.getItemCount() - 1);
    }

    /* access modifiers changed from: private */
    /* renamed from: b */
    public void m691b(String str, Agent agent) {
        C1211i access$1500 = this.f832a.getListAdapter();
        int i = 0;
        while (true) {
            int i2 = i;
            if (i2 < access$1500.getItemCount()) {
                if (access$1500.mo20759b(i2) instanceof C1177a) {
                    C1177a aVar = (C1177a) access$1500.mo20759b(i2);
                    if (!str.equals(aVar.f797k)) {
                        continue;
                    } else if (agent.getAvatarUri() == null) {
                        return;
                    } else {
                        if (!agent.getAvatarUri().equals(aVar.f791e)) {
                            aVar.f791e = agent.getAvatarUri();
                        }
                    }
                }
                i = i2 + 1;
            } else {
                return;
            }
        }
    }

    public void update(Map<String, Agent> map) {
        this.f832a.mHandler.post(new C1194ap(this, map));
    }
}
