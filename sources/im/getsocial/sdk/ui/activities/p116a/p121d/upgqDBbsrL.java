package im.getsocial.sdk.ui.activities.p116a.p121d;

import android.content.Context;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.ImageView;
import android.widget.TextView;
import im.getsocial.sdk.ui.C1067R;
import im.getsocial.sdk.ui.activities.p116a.p123g.jjbQypPegg;
import im.getsocial.sdk.ui.internal.p125h.KluUZYuxme;

/* renamed from: im.getsocial.sdk.ui.activities.a.d.upgqDBbsrL */
public class upgqDBbsrL extends jjbQypPegg<jjbQypPegg> {

    /* renamed from: im.getsocial.sdk.ui.activities.a.d.upgqDBbsrL$jjbQypPegg */
    private static class jjbQypPegg {
        /* renamed from: a */
        ImageView f2741a;
        /* renamed from: b */
        TextView f2742b;

        private jjbQypPegg() {
        }
    }

    public upgqDBbsrL(Context context) {
        super(context);
    }

    public View getView(int i, View view, ViewGroup viewGroup) {
        jjbQypPegg jjbqyppegg;
        if (view == null) {
            view = LayoutInflater.from(viewGroup.getContext()).inflate(C1067R.layout.item_user_suggestion, viewGroup, false);
            jjbqyppegg = new jjbQypPegg();
            jjbqyppegg.f2741a = (ImageView) view.findViewById(C1067R.id.image_view_user_avatar);
            jjbqyppegg.f2742b = (TextView) view.findViewById(C1067R.id.text_view_user_name);
            view.setTag(jjbqyppegg);
        } else {
            jjbqyppegg = (jjbQypPegg) view.getTag();
        }
        jjbQypPegg jjbqyppegg2 = (jjbQypPegg) getItem(i);
        KluUZYuxme a = KluUZYuxme.m3299a(getContext());
        a.m3317a(jjbqyppegg2.m3061a(), jjbqyppegg.f2741a, 24);
        a.m3326d(jjbqyppegg.f2742b);
        jjbqyppegg.f2742b.setText(jjbqyppegg2.m3063c());
        view.setBackgroundColor(0);
        return view;
    }
}
