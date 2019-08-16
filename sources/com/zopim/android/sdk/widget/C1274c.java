package com.zopim.android.sdk.widget;

import com.zopim.android.sdk.data.observers.AgentsObserver;
import com.zopim.android.sdk.model.Agent;
import java.util.Map;

/* renamed from: com.zopim.android.sdk.widget.c */
class C1274c extends AgentsObserver {

    /* renamed from: a */
    final /* synthetic */ ChatWidgetService f977a;

    C1274c(ChatWidgetService chatWidgetService) {
        this.f977a = chatWidgetService;
    }

    public void update(Map<String, Agent> map) {
        this.f977a.mAnimationHandler.post(new C1275d(this, map));
    }
}
