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
        aa c0840g = new C0840g();
        c0840g.f811b = agent.isTyping().booleanValue();
        c0840g.f810a = agent.getAvatarUri();
        c0840g.l = Long.valueOf(System.currentTimeMillis());
        c0840g.k = str;
        C0842i c0842i = (C0842i) this.f788a.getListAdapter();
        aa b = c0842i.m692b(c0842i.getItemCount() - 1);
        if (b instanceof C0840g) {
            ((C0840g) b).f811b = agent.isTyping().booleanValue();
        } else {
            c0842i.m691a(c0840g);
        }
        c0842i.notifyItemChanged(c0842i.getItemCount() - 1);
        this.f788a.mRecyclerView.getLayoutManager().scrollToPosition(c0842i.getItemCount() - 1);
    }

    /* renamed from: b */
    private void m678b(String str, Agent agent) {
        C0842i c0842i = (C0842i) this.f788a.getListAdapter();
        for (int i = 0; i < c0842i.getItemCount(); i++) {
            if (c0842i.m692b(i) instanceof C0833a) {
                C0833a c0833a = (C0833a) c0842i.m692b(i);
                if (!str.equals(c0833a.k)) {
                    continue;
                } else if (agent.getAvatarUri() == null) {
                    return;
                } else {
                    if (!agent.getAvatarUri().equals(c0833a.f753e)) {
                        c0833a.f753e = agent.getAvatarUri();
                    }
                }
            }
        }
    }

    public void update(Map<String, Agent> map) {
        this.f788a.mHandler.post(new ap(this, map));
    }
}
