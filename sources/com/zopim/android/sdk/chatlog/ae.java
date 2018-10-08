package com.zopim.android.sdk.chatlog;

import android.util.Log;
import android.view.View;
import android.view.View.OnClickListener;
import android.widget.Toast;
import com.zopim.android.sdk.C0785R;

class ae implements OnClickListener {
    /* renamed from: a */
    final /* synthetic */ VisitorMessageHolder f773a;

    ae(VisitorMessageHolder visitorMessageHolder) {
        this.f773a = visitorMessageHolder;
    }

    public void onClick(View view) {
        try {
            this.f773a.itemView.getContext().startActivity(this.f773a.f740i);
        } catch (Throwable e) {
            Log.i(VisitorMessageHolder.f731k, "Can't open attachment. No application can handle this uri. " + this.f773a.f740i.getData(), e);
            Toast.makeText(this.f773a.itemView.getContext(), C0785R.string.attachment_open_error_message, 0).show();
        }
    }
}
