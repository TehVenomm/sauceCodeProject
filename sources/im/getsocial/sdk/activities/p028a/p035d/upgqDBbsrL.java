package im.getsocial.sdk.activities.p028a.p035d;

import im.getsocial.sdk.Callback;
import im.getsocial.sdk.activities.TagsQuery;
import im.getsocial.sdk.internal.p033c.p034l.jjbQypPegg;
import java.util.List;

/* renamed from: im.getsocial.sdk.activities.a.d.upgqDBbsrL */
public final class upgqDBbsrL extends jjbQypPegg {
    /* renamed from: a */
    public final void m995a(TagsQuery tagsQuery, Callback<List<String>> callback) {
        boolean z = true;
        im.getsocial.sdk.internal.p033c.p066m.jjbQypPegg.jjbQypPegg.m1512a(tagsQuery.getLimit() > 0, "Limit can not be less than or equal 0");
        if (tagsQuery.getLimit() > 20) {
            z = false;
        }
        im.getsocial.sdk.internal.p033c.p066m.jjbQypPegg.jjbQypPegg.m1512a(z, "Limit can not be greater that 20");
        im.getsocial.sdk.activities.upgqDBbsrL.m1009a(tagsQuery, im.getsocial.sdk.activities.p028a.p031b.jjbQypPegg.m973a(tagsQuery.getFeedName()));
        m985a(m988c().mo4415a(tagsQuery), (Callback) callback);
    }
}
