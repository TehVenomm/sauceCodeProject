package com.zopim.android.sdk.chatlog;

import android.annotation.TargetApi;
import android.support.v7.widget.RecyclerView.ViewHolder;
import android.util.Log;
import android.view.View;
import android.widget.ImageView;
import com.squareup.picasso.Picasso;
import com.zopim.android.sdk.C0785R;
import com.zopim.android.sdk.chatlog.view.TypingIndicatorView;
import com.zopim.android.sdk.util.CircleTransform;

/* renamed from: com.zopim.android.sdk.chatlog.f */
final class C0839f extends ViewHolder {
    /* renamed from: a */
    private static final String f806a = AgentMessageHolder.class.getSimpleName();
    /* renamed from: b */
    private ImageView f807b;
    /* renamed from: c */
    private TypingIndicatorView f808c;
    /* renamed from: d */
    private boolean f809d = false;

    public C0839f(View view) {
        super(view);
        this.f807b = (ImageView) view.findViewById(C0785R.id.avatar_icon);
        this.f808c = (TypingIndicatorView) view.findViewById(C0785R.id.typing_indicator);
    }

    @TargetApi(16)
    /* renamed from: a */
    private void m679a() {
        this.f808c.setVisibility(0);
        if (this.f809d) {
            this.f807b.setVisibility(0);
        } else {
            this.f807b.setVisibility(4);
        }
        this.f808c.start();
    }

    /* renamed from: b */
    private void m680b() {
        this.f808c.stop();
        this.f808c.setVisibility(4);
        this.f807b.setVisibility(4);
    }

    /* renamed from: a */
    public void m681a(C0840g c0840g) {
        if (c0840g == null) {
            Log.e(f806a, "Item must not be null");
            return;
        }
        if (c0840g.f810a != null) {
            Picasso.with(this.itemView.getContext()).load(c0840g.f810a).error(C0785R.drawable.ic_chat_default_avatar).placeholder(C0785R.drawable.ic_chat_default_avatar).transform(new CircleTransform()).into(this.f807b);
        } else {
            Picasso.with(this.itemView.getContext()).load(C0785R.drawable.ic_chat_default_avatar).transform(new CircleTransform()).into(this.f807b);
        }
        if (c0840g.f811b) {
            m679a();
        } else {
            m680b();
        }
    }

    /* renamed from: a */
    public void m682a(boolean z) {
        this.f809d = z;
    }
}
