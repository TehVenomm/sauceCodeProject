package com.zopim.android.sdk.prechat;

import android.content.BroadcastReceiver;
import android.content.Context;
import android.content.Intent;
import android.support.p000v4.app.FragmentTransaction;
import com.zopim.android.sdk.C1122R;

/* renamed from: com.zopim.android.sdk.prechat.j */
class C1257j extends BroadcastReceiver {

    /* renamed from: a */
    final /* synthetic */ ZopimChatFragment f949a;

    C1257j(ZopimChatFragment zopimChatFragment) {
        this.f949a = zopimChatFragment;
    }

    public void onReceive(Context context, Intent intent) {
        ZopimOfflineFragment zopimOfflineFragment = new ZopimOfflineFragment();
        FragmentTransaction beginTransaction = this.f949a.getFragmentManager().beginTransaction();
        beginTransaction.replace(C1122R.C1125id.chat_fragment_container, zopimOfflineFragment, ZopimPreChatFragment.class.getName());
        beginTransaction.remove(this.f949a);
        beginTransaction.commit();
    }
}
