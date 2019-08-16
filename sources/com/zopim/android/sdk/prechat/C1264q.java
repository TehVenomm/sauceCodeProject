package com.zopim.android.sdk.prechat;

import android.content.Context;
import android.widget.ArrayAdapter;
import java.util.List;

/* renamed from: com.zopim.android.sdk.prechat.q */
class C1264q extends ArrayAdapter<String> {

    /* renamed from: a */
    final /* synthetic */ ZopimPreChatFragment f956a;

    C1264q(ZopimPreChatFragment zopimPreChatFragment, Context context, int i, List list) {
        this.f956a = zopimPreChatFragment;
        super(context, i, list);
    }

    public int getCount() {
        return super.getCount() - 1;
    }
}
