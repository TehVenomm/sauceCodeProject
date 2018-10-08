package com.zopim.android.sdk.chatlog;

import com.zopim.android.sdk.chatlog.VisitorMessageHolder.OnClickListener;

/* renamed from: com.zopim.android.sdk.chatlog.j */
class C0843j implements OnClickListener {
    /* renamed from: a */
    final /* synthetic */ C0842i f820a;

    C0843j(C0842i c0842i) {
        this.f820a = c0842i;
    }

    public void onClick(int i) {
        if (this.f820a.f818e != null) {
            aa b = this.f820a.m692b(i);
            if (b instanceof ab) {
                ab abVar = (ab) b;
                if ((abVar.f765a != null ? 1 : null) != null) {
                    this.f820a.f818e.send(abVar.f765a);
                } else {
                    this.f820a.f818e.resend(b.f743g);
                }
            }
        }
    }
}
