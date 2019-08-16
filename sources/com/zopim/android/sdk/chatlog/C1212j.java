package com.zopim.android.sdk.chatlog;

import com.zopim.android.sdk.chatlog.VisitorMessageHolder.OnClickListener;

/* renamed from: com.zopim.android.sdk.chatlog.j */
class C1212j implements OnClickListener {

    /* renamed from: a */
    final /* synthetic */ C1211i f864a;

    C1212j(C1211i iVar) {
        this.f864a = iVar;
    }

    public void onClick(int i) {
        if (this.f864a.f862e != null) {
            C1178aa b = this.f864a.mo20759b(i);
            if (b instanceof C1180ab) {
                C1180ab abVar = (C1180ab) b;
                if (abVar.f809a != null) {
                    this.f864a.f862e.send(abVar.f809a);
                } else {
                    this.f864a.f862e.resend(b.f793g);
                }
            }
        }
    }
}
