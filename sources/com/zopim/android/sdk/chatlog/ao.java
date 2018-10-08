package com.zopim.android.sdk.chatlog;

import android.util.Log;
import com.zopim.android.sdk.api.Logger;
import com.zopim.android.sdk.data.observers.AgentsObserver;
import com.zopim.android.sdk.model.Agent;
import java.util.Map;

class ao extends AgentsObserver {
    /* renamed from: a */
    final /* synthetic */ ZopimChatLogFragment f788a;

    ao(ZopimChatLogFragment zopimChatLogFragment) {
        this.f788a = zopimChatLogFragment;
    }

    /* renamed from: a */
    private void m676a(String str, Agent agent) {
        if (agent.isTyping() == null) {
            Log.d(ZopimChatLogFragment.LOG_TAG, "Can't update agent typing while typing event is null");
            return;
        }
        Logger.m564v(ZopimChatLogFragment.LOG_TAG, "Agent " + agent.getDisplayName() + " typing " + agent.isTyping());
        aa c0839g = new C0839g();
        c0839g.f811b = agent.isTyping().booleanValue();
        c0839g.f810a = agent.getAvatarUri();
        c0839g.l = Long.valueOf(System.currentTimeMillis());
        c0839g.k = str;
        C0841i c0841i = (C0841i) this.f788a.getListAdapter();
        aa b = c0841i.m692b(c0841i.getItemCount() - 1);
        if (b instanceof C0839g) {
            ((C0839g) b).f811b = agent.isTyping().booleanValue();
        } else {
            c0841i.m691a(c0839g);
        }
        c0841i.notifyItemChanged(c0841i.getItemCount() - 1);
        this.f788a.mRecyclerView.getLayoutManager().scrollToPosition(c0841i.getItemCount() - 1);
    }

    /* renamed from: b */
    private void m678b(String str, Agent agent) {
        C0841i c0841i = (C0841i) this.f788a.getListAdapter();
        for (int i = 0; i < c0841i.getItemCount(); i++) {
            if (c0841i.m692b(i) instanceof C0832a) {
                C0832a c0832a = (C0832a) c0841i.m692b(i);
                if (!str.equals(c0832a.k)) {
                    continue;
                } else if (agent.getAvatarUri() == null) {
                    return;
                } else {
                    if (!agent.getAvatarUri().equals(c0832a.f753e)) {
                        c0832a.f753e = agent.getAvatarUri();
                    }
                }
            }
        }
    }

    public void update(Map<String, Agent> map) {
        this.f788a.mHandler.post(new ap(this, map));
    }
}
