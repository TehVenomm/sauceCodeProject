package im.getsocial.sdk.ui.activities.p116a.p124c;

import android.content.Context;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.ArrayAdapter;
import android.widget.ImageView;
import android.widget.TextView;
import im.getsocial.sdk.ui.C1067R;
import im.getsocial.sdk.ui.internal.p125h.KluUZYuxme;
import im.getsocial.sdk.ui.internal.p131d.upgqDBbsrL;
import im.getsocial.sdk.usermanagement.PublicUser;
import java.util.List;

/* renamed from: im.getsocial.sdk.ui.activities.a.c.jjbQypPegg */
class jjbQypPegg extends ArrayAdapter<PublicUser> {
    /* renamed from: a */
    private final KluUZYuxme f2711a;

    /* renamed from: im.getsocial.sdk.ui.activities.a.c.jjbQypPegg$jjbQypPegg */
    private static class jjbQypPegg {
        /* renamed from: a */
        ImageView f2709a;
        /* renamed from: b */
        TextView f2710b;

        private jjbQypPegg() {
        }
    }

    jjbQypPegg(Context context, List<PublicUser> list, KluUZYuxme kluUZYuxme) {
        super(context, 0, list);
        this.f2711a = kluUZYuxme;
    }

    public View getView(int i, View view, ViewGroup viewGroup) {
        jjbQypPegg jjbqyppegg;
        upgqDBbsrL a = upgqDBbsrL.m3237a();
        if (view == null) {
            view = LayoutInflater.from(viewGroup.getContext()).inflate(C1067R.layout.item_activity_liker, viewGroup, false);
            jjbqyppegg = new jjbQypPegg();
            jjbqyppegg.f2709a = (ImageView) view.findViewById(C1067R.id.image_view_user_avatar);
            jjbqyppegg.f2710b = (TextView) view.findViewById(C1067R.id.text_view_user_name);
            view.setTag(jjbqyppegg);
        } else {
            jjbqyppegg = (jjbQypPegg) view.getTag();
        }
        PublicUser publicUser = (PublicUser) getItem(i);
        this.f2711a.m3316a(publicUser.getAvatarUrl(), jjbqyppegg.f2709a);
        this.f2711a.m3309a(jjbqyppegg.f2710b);
        jjbqyppegg.f2710b.setText(publicUser.getDisplayName());
        if (i % 2 == 0) {
            view.setBackgroundColor(a.m3255b().m3212c().m3114C().m3144b().m3215a());
        } else {
            view.setBackgroundColor(a.m3255b().m3212c().m3114C().m3143a().m3215a());
        }
        return view;
    }
}
