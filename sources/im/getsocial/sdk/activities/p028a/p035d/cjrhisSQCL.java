package im.getsocial.sdk.activities.p028a.p035d;

import im.getsocial.sdk.Callback;
import im.getsocial.sdk.activities.ActivitiesQuery;
import im.getsocial.sdk.activities.ActivityPost;
import im.getsocial.sdk.activities.ActivityPost.Type;
import im.getsocial.sdk.internal.p030e.p065a.pdwpUtZXDT;
import im.getsocial.sdk.internal.p033c.p034l.jjbQypPegg;
import java.util.List;

/* renamed from: im.getsocial.sdk.activities.a.d.cjrhisSQCL */
public final class cjrhisSQCL extends jjbQypPegg {
    /* renamed from: a */
    public final void m992a(ActivitiesQuery activitiesQuery, Callback<List<ActivityPost>> callback) {
        im.getsocial.sdk.activities.jjbQypPegg a = im.getsocial.sdk.activities.jjbQypPegg.m998a(activitiesQuery);
        Type a2 = a.m999a();
        pdwpUtZXDT a3 = a2 == Type.POST ? m988c().mo4427a(im.getsocial.sdk.activities.p028a.p031b.jjbQypPegg.m973a(a.m1000b()), activitiesQuery) : a2 == Type.COMMENT ? m988c().mo4441b(a.m1001c(), activitiesQuery) : pdwpUtZXDT.m1660a(new RuntimeException("Communication method in not implemented for content type: " + a2));
        m985a(a3, (Callback) callback);
    }
}
