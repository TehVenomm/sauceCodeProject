package im.getsocial.sdk.ui.invites.p137a;

import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.ListView;
import im.getsocial.sdk.internal.p033c.p066m.jjbQypPegg.jjbQypPegg;
import im.getsocial.sdk.invites.InviteChannel;
import im.getsocial.sdk.ui.C1067R;
import im.getsocial.sdk.ui.invites.p137a.cjrhisSQCL.cjrhisSQCL;
import im.getsocial.sdk.ui.invites.p137a.jjbQypPegg.upgqDBbsrL;
import java.util.List;

/* renamed from: im.getsocial.sdk.ui.invites.a.XdbacJlTDQ */
public class XdbacJlTDQ extends cjrhisSQCL implements upgqDBbsrL {
    /* renamed from: a */
    jjbQypPegg f3232a;
    /* renamed from: d */
    ListView f3233d;

    /* renamed from: a */
    public final View mo4580a(ViewGroup viewGroup) {
        return LayoutInflater.from(m2526p()).inflate(C1067R.layout.contentview_invite_provider_list, viewGroup);
    }

    /* renamed from: a */
    public final void mo4581a() {
        this.f3233d = (ListView) m2536q().findViewById(C1067R.id.list_view_invite_providers);
    }

    /* renamed from: a */
    public final void mo4748a(InviteChannel inviteChannel) {
        jjbQypPegg.m1512a(im.getsocial.sdk.internal.p033c.p066m.jjbQypPegg.m1514a(m2524n()), "Presenter has not been set");
        ((cjrhisSQCL.upgqDBbsrL) m2524n()).mo4750a(inviteChannel);
    }

    /* renamed from: a */
    public final void mo4749a(List<InviteChannel> list) {
        this.f3232a = new jjbQypPegg(list, this);
        this.f3233d.setAdapter(this.f3232a);
    }
}
