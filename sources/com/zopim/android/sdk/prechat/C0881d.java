package com.zopim.android.sdk.prechat;

import android.support.v4.app.Fragment;
import android.support.v4.app.FragmentTransaction;
import com.zopim.android.sdk.C0784R;
import com.zopim.android.sdk.chatlog.ZopimChatLogFragment;

/* renamed from: com.zopim.android.sdk.prechat.d */
class C0881d implements Runnable {
    /* renamed from: a */
    final /* synthetic */ ZopimChatFragment f899a;

    C0881d(ZopimChatFragment zopimChatFragment) {
        this.f899a = zopimChatFragment;
    }

    public void run() {
        this.f899a.mProgressBar.setVisibility(8);
        if (this.f899a.showPreChat()) {
            Fragment newInstance = ZopimPreChatFragment.newInstance(this.f899a.mChat.getConfig().getPreChatForm());
            FragmentTransaction beginTransaction = this.f899a.getFragmentManager().beginTransaction();
            beginTransaction.replace(C0784R.id.chat_fragment_container, newInstance, ZopimPreChatFragment.class.getName());
            beginTransaction.remove(this.f899a);
            beginTransaction.commit();
            return;
        }
        newInstance = new ZopimChatLogFragment();
        beginTransaction = this.f899a.getFragmentManager().beginTransaction();
        beginTransaction.replace(C0784R.id.chat_fragment_container, newInstance, ZopimChatLogFragment.class.getName());
        beginTransaction.remove(this.f899a);
        beginTransaction.commit();
    }
}
