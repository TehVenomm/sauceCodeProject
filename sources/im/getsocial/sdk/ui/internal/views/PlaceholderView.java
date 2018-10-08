package im.getsocial.sdk.ui.internal.views;

import android.content.Context;
import android.view.View;
import android.view.ViewGroup;
import android.view.ViewGroup.LayoutParams;
import android.widget.ImageView;
import android.widget.LinearLayout;
import android.widget.TextView;
import im.getsocial.sdk.ui.C1067R;
import im.getsocial.sdk.ui.internal.p125h.KluUZYuxme;
import im.getsocial.sdk.ui.internal.p131d.upgqDBbsrL;

public class PlaceholderView extends LinearLayout {
    /* renamed from: a */
    private static final Object f3220a = new Object();
    /* renamed from: b */
    private ImageView f3221b = ((ImageView) findViewById(C1067R.id.placeholder_image));
    /* renamed from: c */
    private TextView f3222c = ((TextView) findViewById(C1067R.id.placeholder_title));
    /* renamed from: d */
    private TextView f3223d = ((TextView) findViewById(C1067R.id.placeholder_text));

    public PlaceholderView(Context context) {
        super(context);
        inflate(getContext(), C1067R.layout.placeholder_view, this);
        setBackgroundColor(upgqDBbsrL.m3237a().m3255b().m3212c().m3117a().m3164d().m3215a());
        KluUZYuxme a = KluUZYuxme.m3299a(getContext());
        a.m3310a(this.f3222c, upgqDBbsrL.m3237a().m3255b().m3212c().m3130n().m3209a());
        a.m3310a(this.f3223d, upgqDBbsrL.m3237a().m3255b().m3212c().m3131o().m3209a());
    }

    /* renamed from: a */
    public static PlaceholderView m3589a(View view, String str, String str2, String str3) {
        return m3590a(view, str, str2, str3, false);
    }

    /* renamed from: a */
    public static PlaceholderView m3590a(View view, String str, String str2, String str3, boolean z) {
        if (!(view instanceof ViewGroup)) {
            return null;
        }
        View childAt;
        PlaceholderView placeholderView;
        ViewGroup viewGroup = (ViewGroup) view;
        if (z) {
            childAt = viewGroup.getChildAt(0);
            if (childAt != null) {
                childAt.setVisibility(8);
            }
        }
        PlaceholderView a = m3591a(viewGroup);
        if (a == null) {
            childAt = new PlaceholderView(view.getContext());
            childAt.setTag(f3220a);
            viewGroup.addView(childAt, new LayoutParams(-1, -1));
            placeholderView = childAt;
        } else {
            placeholderView = a;
        }
        placeholderView.bringToFront();
        placeholderView.f3222c.setText(str);
        placeholderView.f3223d.setText(str2);
        placeholderView.f3221b.setImageBitmap(upgqDBbsrL.m3237a().m3258c(placeholderView.getContext(), str3));
        return placeholderView;
    }

    /* renamed from: a */
    private static PlaceholderView m3591a(ViewGroup viewGroup) {
        return (PlaceholderView) viewGroup.findViewWithTag(f3220a);
    }

    /* renamed from: a */
    public static void m3592a(View view) {
        if (view instanceof ViewGroup) {
            ViewGroup viewGroup = (ViewGroup) view;
            View a = m3591a(viewGroup);
            if (a != null) {
                viewGroup.removeView(a);
            }
            a = viewGroup.getChildAt(0);
            if (a != null) {
                a.setVisibility(0);
            }
        }
    }
}
