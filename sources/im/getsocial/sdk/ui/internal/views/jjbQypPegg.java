package im.getsocial.sdk.ui.internal.views;

import android.graphics.drawable.Drawable;
import android.os.Build.VERSION;
import android.view.View;
import android.view.ViewGroup.LayoutParams;
import android.view.ViewGroup.MarginLayoutParams;
import im.getsocial.sdk.ui.internal.p131d.p132a.ztWNWCuZiM;
import im.getsocial.sdk.ui.internal.p131d.upgqDBbsrL;

public final class jjbQypPegg {
    private jjbQypPegg() {
    }

    /* renamed from: a */
    public static void m3594a(View view, int i, int i2) {
        LayoutParams layoutParams = view.getLayoutParams();
        layoutParams.width = i;
        layoutParams.height = i2;
        view.setLayoutParams(layoutParams);
    }

    /* renamed from: a */
    public static void m3595a(View view, int i, int i2, int i3, int i4) {
        LayoutParams layoutParams = view.getLayoutParams();
        if (layoutParams instanceof MarginLayoutParams) {
            ((MarginLayoutParams) layoutParams).setMargins(0, i2, 0, i4);
            return;
        }
        throw new RuntimeException("View must have a parent using MarginLayoutParams : " + view);
    }

    /* renamed from: a */
    public static void m3596a(View view, Drawable drawable) {
        if (VERSION.SDK_INT < 16) {
            view.setBackgroundDrawable(drawable);
        } else {
            view.setBackground(drawable);
        }
    }

    /* renamed from: a */
    public static void m3597a(View view, ztWNWCuZiM ztwnwcuzim) {
        LayoutParams layoutParams = view.getLayoutParams();
        if (layoutParams instanceof MarginLayoutParams) {
            MarginLayoutParams marginLayoutParams = (MarginLayoutParams) layoutParams;
            upgqDBbsrL a = upgqDBbsrL.m3237a();
            marginLayoutParams.setMargins(a.m3246a(ztwnwcuzim.mo4722a()), a.m3246a(ztwnwcuzim.mo4724c()), a.m3246a(ztwnwcuzim.mo4723b()), a.m3246a(ztwnwcuzim.mo4725d()));
            return;
        }
        throw new RuntimeException("View must have a parent using MarginLayoutParams : " + view);
    }
}
