package com.zopim.android.sdk.chatlog;

import android.content.ActivityNotFoundException;
import android.util.Log;
import android.view.View;
import android.view.View.OnClickListener;
import android.widget.Toast;
import com.zopim.android.sdk.C1122R;

/* renamed from: com.zopim.android.sdk.chatlog.ae */
class C1183ae implements OnClickListener {

    /* renamed from: a */
    final /* synthetic */ VisitorMessageHolder f817a;

    C1183ae(VisitorMessageHolder visitorMessageHolder) {
        this.f817a = visitorMessageHolder;
    }

    public void onClick(View view) {
        try {
            this.f817a.itemView.getContext().startActivity(this.f817a.f784i);
        } catch (ActivityNotFoundException e) {
            Log.i(VisitorMessageHolder.f775k, "Can't open attachment. No application can handle this uri. " + this.f817a.f784i.getData(), e);
            Toast.makeText(this.f817a.itemView.getContext(), C1122R.string.attachment_open_error_message, 0).show();
        }
    }
}
