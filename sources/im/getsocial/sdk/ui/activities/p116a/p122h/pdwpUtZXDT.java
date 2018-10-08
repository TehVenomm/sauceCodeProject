package im.getsocial.sdk.ui.activities.p116a.p122h;

import android.content.Context;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.TextView;
import im.getsocial.sdk.ui.C1067R;
import im.getsocial.sdk.ui.activities.p116a.p123g.jjbQypPegg;
import im.getsocial.sdk.ui.internal.p125h.KluUZYuxme;

/* renamed from: im.getsocial.sdk.ui.activities.a.h.pdwpUtZXDT */
public class pdwpUtZXDT extends jjbQypPegg<jjbQypPegg> {

    /* renamed from: im.getsocial.sdk.ui.activities.a.h.pdwpUtZXDT$jjbQypPegg */
    private static class jjbQypPegg {
        /* renamed from: a */
        TextView f2754a;

        private jjbQypPegg() {
        }
    }

    public pdwpUtZXDT(Context context) {
        super(context);
    }

    public View getView(int i, View view, ViewGroup viewGroup) {
        jjbQypPegg jjbqyppegg;
        if (view == null) {
            view = LayoutInflater.from(viewGroup.getContext()).inflate(C1067R.layout.item_tag_suggestion, viewGroup, false);
            jjbQypPegg jjbqyppegg2 = new jjbQypPegg();
            jjbqyppegg2.f2754a = (TextView) view.findViewById(C1067R.id.text_view_tag_name);
            view.setTag(jjbqyppegg2);
            jjbqyppegg = jjbqyppegg2;
        } else {
            jjbqyppegg = (jjbQypPegg) view.getTag();
        }
        KluUZYuxme.m3299a(getContext()).m3326d(jjbqyppegg.f2754a);
        jjbqyppegg.f2754a.setText(((jjbQypPegg) getItem(i)).mo4714b());
        view.setBackgroundColor(0);
        return view;
    }
}
