package com.zopim.android.sdk.prechat;

import android.content.BroadcastReceiver;
import android.content.Context;
import android.content.Intent;
import android.support.v4.app.Fragment;
import android.support.v4.app.FragmentTransaction;
import com.zopim.android.sdk.C0784R;

/* renamed from: com.zopim.android.sdk.prechat.j */
class C0887j extends BroadcastReceiver {
    /* renamed from: a */
    final /* synthetic */ ZopimChatFragment f905a;

    C0887j(ZopimChatFragment zopimChatFragment) {
        this.f905a = zopimChatFragment;
    }

    public void onReceive(Context context, Intent intent) {
        Fragment zopimOfflineFragment = new ZopimOfflineFragment();
        FragmentTransaction beginTransaction = this.f905a.getFragmentManager().beginTransaction();
        beginTransaction.replace(C0784R.id.chat_fragment_container, zopimOfflineFragment, ZopimPreChatFragment.class.getName());
        beginTransaction.remove(this.f905a);
        beginTransaction.commit();
    }
}
