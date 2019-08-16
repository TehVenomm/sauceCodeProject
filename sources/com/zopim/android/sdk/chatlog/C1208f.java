package com.zopim.android.sdk.chatlog;

import android.annotation.TargetApi;
import android.support.v7.widget.RecyclerView.ViewHolder;
import android.util.Log;
import android.view.View;
import android.widget.ImageView;
import com.squareup.picasso.Picasso;
import com.squareup.picasso.Transformation;
import com.zopim.android.sdk.C1122R;
import com.zopim.android.sdk.chatlog.view.TypingIndicatorView;
import com.zopim.android.sdk.util.CircleTransform;

/* renamed from: com.zopim.android.sdk.chatlog.f */
final class C1208f extends ViewHolder {

    /* renamed from: a */
    private static final String f850a = AgentMessageHolder.class.getSimpleName();

    /* renamed from: b */
    private ImageView f851b;

    /* renamed from: c */
    private TypingIndicatorView f852c;

    /* renamed from: d */
    private boolean f853d = false;

    public C1208f(View view) {
        super(view);
        this.f851b = (ImageView) view.findViewById(C1122R.C1125id.avatar_icon);
        this.f852c = (TypingIndicatorView) view.findViewById(C1122R.C1125id.typing_indicator);
    }

    @TargetApi(16)
    /* renamed from: a */
    private void m692a() {
        this.f852c.setVisibility(0);
        if (this.f853d) {
            this.f851b.setVisibility(0);
        } else {
            this.f851b.setVisibility(4);
        }
        this.f852c.start();
    }

    /* renamed from: b */
    private void m693b() {
        this.f852c.stop();
        this.f852c.setVisibility(4);
        this.f851b.setVisibility(4);
    }

    /* renamed from: a */
    public void mo20752a(C1209g gVar) {
        if (gVar == null) {
            Log.e(f850a, "Item must not be null");
            return;
        }
        if (gVar.f854a != null) {
            Picasso.with(this.itemView.getContext()).load(gVar.f854a).error(C1122R.C1124drawable.ic_chat_default_avatar).placeholder(C1122R.C1124drawable.ic_chat_default_avatar).transform((Transformation) new CircleTransform()).into(this.f851b);
        } else {
            Picasso.with(this.itemView.getContext()).load(C1122R.C1124drawable.ic_chat_default_avatar).transform((Transformation) new CircleTransform()).into(this.f851b);
        }
        if (gVar.f855b) {
            m692a();
        } else {
            m693b();
        }
    }

    /* renamed from: a */
    public void mo20753a(boolean z) {
        this.f853d = z;
    }
}
