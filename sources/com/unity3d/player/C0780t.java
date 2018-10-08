package com.unity3d.player;

import android.content.Context;
import android.view.View;
import android.view.ViewGroup;
import java.util.HashSet;
import java.util.Set;

/* renamed from: com.unity3d.player.t */
final class C0780t {
    /* renamed from: a */
    public static C0780t f564a;
    /* renamed from: b */
    private final ViewGroup f565b;
    /* renamed from: c */
    private Set f566c = new HashSet();
    /* renamed from: d */
    private View f567d;
    /* renamed from: e */
    private View f568e;

    C0780t(ViewGroup viewGroup) {
        this.f565b = viewGroup;
        f564a = this;
    }

    /* renamed from: e */
    private void m535e(View view) {
        this.f565b.addView(view, this.f565b.getChildCount());
    }

    /* renamed from: f */
    private void m536f(View view) {
        this.f565b.removeView(view);
        this.f565b.requestLayout();
    }

    /* renamed from: a */
    public final Context m537a() {
        return this.f565b.getContext();
    }

    /* renamed from: a */
    public final void m538a(View view) {
        this.f566c.add(view);
        if (this.f567d != null) {
            m535e(view);
        }
    }

    /* renamed from: b */
    public final void m539b(View view) {
        this.f566c.remove(view);
        if (this.f567d != null) {
            m536f(view);
        }
    }

    /* renamed from: c */
    public final void m540c(View view) {
        if (this.f567d != view) {
            this.f567d = view;
            this.f565b.addView(view);
            for (View e : this.f566c) {
                m535e(e);
            }
            if (this.f568e != null) {
                this.f568e.setVisibility(4);
            }
        }
    }

    /* renamed from: d */
    public final void m541d(View view) {
        if (this.f567d == view) {
            for (View f : this.f566c) {
                m536f(f);
            }
            this.f565b.removeView(view);
            this.f567d = null;
            if (this.f568e != null) {
                this.f568e.setVisibility(0);
            }
        }
    }
}
