package com.zopim.android.sdk.prechat;

import android.content.Intent;
import android.view.View;
import android.view.View.OnClickListener;
import com.zopim.android.sdk.embeddable.Contract;

/* renamed from: com.zopim.android.sdk.prechat.b */
class C1249b implements OnClickListener {

    /* renamed from: a */
    final /* synthetic */ ZopimChatFragment f941a;

    C1249b(ZopimChatFragment zopimChatFragment) {
        this.f941a = zopimChatFragment;
    }

    public void onClick(View view) {
        Intent intent = new Intent();
        intent.setAction(Contract.ACTION_CREATE_REQUEST);
        this.f941a.getActivity().sendOrderedBroadcast(intent, null);
    }
}
