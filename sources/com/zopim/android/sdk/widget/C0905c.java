package com.zopim.android.sdk.widget;

import com.zopim.android.sdk.data.observers.AgentsObserver;
import com.zopim.android.sdk.model.Agent;
import java.util.Map;

/* renamed from: com.zopim.android.sdk.widget.c */
class C0905c extends AgentsObserver {
    /* renamed from: a */
    final /* synthetic */ ChatWidgetService f933a;

    C0905c(ChatWidgetService chatWidgetService) {
        this.f933a = chatWidgetService;
    }

    public void update(Map<String, Agent> map) {
        this.f933a.mAnimationHandler.post(new C0906d(this, map));
    }
}
