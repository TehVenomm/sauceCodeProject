package com.zopim.android.sdk.chatlog;

import android.support.v7.widget.RecyclerView.ViewHolder;
import android.util.Log;
import android.view.View;
import android.widget.TextView;
import com.zopim.android.sdk.C1122R;

/* renamed from: com.zopim.android.sdk.chatlog.n */
final class C1216n extends ViewHolder {

    /* renamed from: a */
    private static final String f868a = C1216n.class.getSimpleName();

    /* renamed from: b */
    private TextView f869b;

    public C1216n(View view) {
        super(view);
        this.f869b = (TextView) view.findViewById(C1122R.C1125id.message_text);
    }

    /* renamed from: a */
    public void mo20764a(C1178aa aaVar) {
        if (aaVar == null) {
            Log.e(f868a, "Item must not be null");
        } else {
            this.f869b.setText(aaVar.f795i);
        }
    }
}
