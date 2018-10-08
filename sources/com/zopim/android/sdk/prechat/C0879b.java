package com.zopim.android.sdk.prechat;

import android.content.Intent;
import android.view.View;
import android.view.View.OnClickListener;
import com.zopim.android.sdk.embeddable.Contract;

/* renamed from: com.zopim.android.sdk.prechat.b */
class C0879b implements OnClickListener {
    /* renamed from: a */
    final /* synthetic */ ZopimChatFragment f897a;

    C0879b(ZopimChatFragment zopimChatFragment) {
        this.f897a = zopimChatFragment;
    }

    public void onClick(View view) {
        Intent intent = new Intent();
        intent.setAction(Contract.ACTION_CREATE_REQUEST);
        this.f897a.getActivity().sendOrderedBroadcast(intent, null);
    }
}
