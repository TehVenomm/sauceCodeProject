package com.zopim.android.sdk.prechat;

import android.support.p000v4.app.FragmentTransaction;
import com.zopim.android.sdk.C1122R;
import com.zopim.android.sdk.chatlog.ZopimChatLogFragment;

/* renamed from: com.zopim.android.sdk.prechat.d */
class C1251d implements Runnable {

    /* renamed from: a */
    final /* synthetic */ ZopimChatFragment f943a;

    C1251d(ZopimChatFragment zopimChatFragment) {
        this.f943a = zopimChatFragment;
    }

    public void run() {
        this.f943a.mProgressBar.setVisibility(8);
        if (this.f943a.showPreChat()) {
            ZopimPreChatFragment newInstance = ZopimPreChatFragment.newInstance(this.f943a.mChat.getConfig().getPreChatForm());
            FragmentTransaction beginTransaction = this.f943a.getFragmentManager().beginTransaction();
            beginTransaction.replace(C1122R.C1125id.chat_fragment_container, newInstance, ZopimPreChatFragment.class.getName());
            beginTransaction.remove(this.f943a);
            beginTransaction.commit();
            return;
        }
        ZopimChatLogFragment zopimChatLogFragment = new ZopimChatLogFragment();
        FragmentTransaction beginTransaction2 = this.f943a.getFragmentManager().beginTransaction();
        beginTransaction2.replace(C1122R.C1125id.chat_fragment_container, zopimChatLogFragment, ZopimChatLogFragment.class.getName());
        beginTransaction2.remove(this.f943a);
        beginTransaction2.commit();
    }
}
