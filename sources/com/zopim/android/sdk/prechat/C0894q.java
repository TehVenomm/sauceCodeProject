package com.zopim.android.sdk.prechat;

import android.content.Context;
import android.widget.ArrayAdapter;
import java.util.List;

/* renamed from: com.zopim.android.sdk.prechat.q */
class C0894q extends ArrayAdapter<String> {
    /* renamed from: a */
    final /* synthetic */ ZopimPreChatFragment f912a;

    C0894q(ZopimPreChatFragment zopimPreChatFragment, Context context, int i, List list) {
        this.f912a = zopimPreChatFragment;
        super(context, i, list);
    }

    public int getCount() {
        return super.getCount() - 1;
    }
}
