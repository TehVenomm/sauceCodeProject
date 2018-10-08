package com.zopim.android.sdk.chatlog;

import android.support.v7.widget.RecyclerView.ViewHolder;
import android.util.Log;
import android.view.View;
import android.widget.TextView;
import com.zopim.android.sdk.C0785R;

/* renamed from: com.zopim.android.sdk.chatlog.n */
final class C0847n extends ViewHolder {
    /* renamed from: a */
    private static final String f824a = C0847n.class.getSimpleName();
    /* renamed from: b */
    private TextView f825b;

    public C0847n(View view) {
        super(view);
        this.f825b = (TextView) view.findViewById(C0785R.id.message_text);
    }

    /* renamed from: a */
    public void m693a(aa aaVar) {
        if (aaVar == null) {
            Log.e(f824a, "Item must not be null");
        } else {
            this.f825b.setText(aaVar.f745i);
        }
    }
}
