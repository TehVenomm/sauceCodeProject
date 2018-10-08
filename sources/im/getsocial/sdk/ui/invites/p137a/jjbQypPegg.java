package im.getsocial.sdk.ui.invites.p137a;

import android.view.LayoutInflater;
import android.view.View;
import android.view.View.OnClickListener;
import android.view.ViewGroup;
import android.widget.BaseAdapter;
import android.widget.ImageView;
import android.widget.TextView;
import im.getsocial.sdk.invites.InviteChannel;
import im.getsocial.sdk.ui.C1067R;
import im.getsocial.sdk.ui.internal.p125h.KluUZYuxme;
import im.getsocial.sdk.ui.internal.p131d.upgqDBbsrL;
import java.util.List;

/* renamed from: im.getsocial.sdk.ui.invites.a.jjbQypPegg */
public class jjbQypPegg extends BaseAdapter {
    /* renamed from: a */
    private final List<InviteChannel> f3240a;
    /* renamed from: b */
    private final upgqDBbsrL f3241b;

    /* renamed from: im.getsocial.sdk.ui.invites.a.jjbQypPegg$upgqDBbsrL */
    public interface upgqDBbsrL {
        /* renamed from: a */
        void mo4748a(InviteChannel inviteChannel);
    }

    /* renamed from: im.getsocial.sdk.ui.invites.a.jjbQypPegg$jjbQypPegg */
    private class jjbQypPegg {
        /* renamed from: a */
        ImageView f3236a;
        /* renamed from: b */
        TextView f3237b;
        /* renamed from: c */
        int f3238c;
        /* renamed from: d */
        final /* synthetic */ jjbQypPegg f3239d;

        jjbQypPegg(final jjbQypPegg jjbqyppegg, View view) {
            this.f3239d = jjbqyppegg;
            this.f3236a = (ImageView) view.findViewById(C1067R.id.imageViewInviteIcon);
            this.f3237b = (TextView) view.findViewById(C1067R.id.textViewInviteName);
            view.setOnClickListener(new OnClickListener(this) {
                /* renamed from: b */
                final /* synthetic */ jjbQypPegg f3235b;

                public void onClick(View view) {
                    this.f3235b.f3239d.f3241b.mo4748a(this.f3235b.f3239d.m3612a(this.f3235b.f3238c));
                }
            });
        }
    }

    public jjbQypPegg(List<InviteChannel> list, upgqDBbsrL upgqdbbsrl) {
        this.f3240a = list;
        this.f3241b = upgqdbbsrl;
    }

    /* renamed from: a */
    public final InviteChannel m3612a(int i) {
        return (InviteChannel) this.f3240a.get(i);
    }

    public int getCount() {
        return this.f3240a == null ? 0 : this.f3240a.size();
    }

    public /* synthetic */ Object getItem(int i) {
        return m3612a(i);
    }

    public long getItemId(int i) {
        return (long) ((InviteChannel) this.f3240a.get(i)).hashCode();
    }

    public View getView(int i, View view, ViewGroup viewGroup) {
        jjbQypPegg jjbqyppegg;
        upgqDBbsrL a = upgqDBbsrL.m3237a();
        if (view == null) {
            view = LayoutInflater.from(viewGroup.getContext()).inflate(C1067R.layout.item_invite, viewGroup, false);
            jjbqyppegg = new jjbQypPegg(this, view);
            view.setTag(jjbqyppegg);
        } else {
            jjbqyppegg = (jjbQypPegg) view.getTag();
        }
        KluUZYuxme.m3299a(viewGroup.getContext()).m3310a(jjbqyppegg.f3237b, a.m3255b().m3212c().m3123g().m3209a());
        view.setBackgroundColor((i % 2 == 0 ? a.m3255b().m3212c().m3114C().m3143a() : a.m3255b().m3212c().m3114C().m3144b()).m3215a());
        InviteChannel a2 = m3612a(i);
        jjbqyppegg.f3238c = i;
        jjbqyppegg.f3237b.setText(a2.getChannelName());
        im.getsocial.sdk.internal.p072g.jjbQypPegg.m1910a(a2.getIconImageUrl()).m1935b(jjbqyppegg.f3236a);
        return view;
    }
}
