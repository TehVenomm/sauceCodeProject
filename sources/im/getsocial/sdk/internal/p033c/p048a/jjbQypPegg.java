package im.getsocial.sdk.internal.p033c.p048a;

import im.getsocial.sdk.activities.ActivitiesQuery;
import im.getsocial.sdk.activities.ActivityPost;
import im.getsocial.sdk.activities.ReportingReason;
import im.getsocial.sdk.activities.TagsQuery;
import im.getsocial.sdk.internal.p030e.p065a.pdwpUtZXDT;
import im.getsocial.sdk.internal.p033c.IbawHMWljm;
import im.getsocial.sdk.internal.p033c.QhisXzMgay;
import im.getsocial.sdk.internal.p033c.fOrCGNYyfk;
import im.getsocial.sdk.invites.LinkParams;
import im.getsocial.sdk.invites.ReferralData;
import im.getsocial.sdk.invites.ReferredUser;
import im.getsocial.sdk.invites.p092a.p094b.XdbacJlTDQ;
import im.getsocial.sdk.pushnotifications.Notification;
import im.getsocial.sdk.pushnotifications.NotificationsCountQuery;
import im.getsocial.sdk.pushnotifications.NotificationsQuery;
import im.getsocial.sdk.socialgraph.SuggestedFriend;
import im.getsocial.sdk.usermanagement.AuthIdentity;
import im.getsocial.sdk.usermanagement.PrivateUser;
import im.getsocial.sdk.usermanagement.PublicUser;
import im.getsocial.sdk.usermanagement.UserReference;
import im.getsocial.sdk.usermanagement.UsersQuery;
import im.getsocial.sdk.usermanagement.p138a.p139a.upgqDBbsrL;
import java.util.List;
import java.util.Map;

/* renamed from: im.getsocial.sdk.internal.c.a.jjbQypPegg */
public interface jjbQypPegg {
    /* renamed from: a */
    pdwpUtZXDT<List<ReferredUser>> mo4413a();

    /* renamed from: a */
    pdwpUtZXDT<List<PublicUser>> mo4414a(int i, int i2);

    /* renamed from: a */
    pdwpUtZXDT<List<String>> mo4415a(TagsQuery tagsQuery);

    /* renamed from: a */
    pdwpUtZXDT<Void> mo4416a(QhisXzMgay qhisXzMgay);

    /* renamed from: a */
    pdwpUtZXDT<Void> mo4417a(QhisXzMgay qhisXzMgay, List<im.getsocial.sdk.internal.p036a.p038b.jjbQypPegg> list);

    /* renamed from: a */
    pdwpUtZXDT<ReferralData> mo4418a(fOrCGNYyfk forcgnyyfk, boolean z, Map<im.getsocial.sdk.invites.p092a.p093a.pdwpUtZXDT, Map<String, String>> map, Map<String, String> map2);

    /* renamed from: a */
    pdwpUtZXDT<Integer> mo4419a(NotificationsCountQuery notificationsCountQuery);

    /* renamed from: a */
    pdwpUtZXDT<List<Notification>> mo4420a(NotificationsQuery notificationsQuery);

    /* renamed from: a */
    pdwpUtZXDT<PrivateUser> mo4421a(AuthIdentity authIdentity);

    /* renamed from: a */
    pdwpUtZXDT<List<UserReference>> mo4422a(UsersQuery usersQuery);

    /* renamed from: a */
    pdwpUtZXDT<upgqDBbsrL> mo4423a(im.getsocial.sdk.usermanagement.p138a.p139a.jjbQypPegg jjbqyppegg);

    /* renamed from: a */
    pdwpUtZXDT<PrivateUser> mo4424a(im.getsocial.sdk.usermanagement.p138a.p139a.pdwpUtZXDT pdwputzxdt);

    /* renamed from: a */
    pdwpUtZXDT<PrivateUser> mo4425a(String str);

    /* renamed from: a */
    pdwpUtZXDT<List<PublicUser>> mo4426a(String str, int i, int i2);

    /* renamed from: a */
    pdwpUtZXDT<List<ActivityPost>> mo4427a(String str, ActivitiesQuery activitiesQuery);

    /* renamed from: a */
    pdwpUtZXDT<Void> mo4428a(String str, ReportingReason reportingReason);

    /* renamed from: a */
    pdwpUtZXDT<ActivityPost> mo4429a(String str, im.getsocial.sdk.activities.p028a.p029a.jjbQypPegg jjbqyppegg);

    /* renamed from: a */
    pdwpUtZXDT<XdbacJlTDQ> mo4430a(String str, LinkParams linkParams);

    /* renamed from: a */
    pdwpUtZXDT<Void> mo4431a(String str, String str2, IbawHMWljm ibawHMWljm, Boolean bool);

    /* renamed from: a */
    pdwpUtZXDT<Map<String, PublicUser>> mo4432a(String str, List<String> list);

    /* renamed from: a */
    pdwpUtZXDT<ActivityPost> mo4433a(String str, boolean z);

    /* renamed from: a */
    pdwpUtZXDT<Integer> mo4434a(List<String> list);

    /* renamed from: a */
    pdwpUtZXDT<Void> mo4435a(List<String> list, boolean z);

    /* renamed from: a */
    pdwpUtZXDT<Void> mo4436a(boolean z);

    /* renamed from: b */
    pdwpUtZXDT<Integer> mo4437b();

    /* renamed from: b */
    pdwpUtZXDT<List<SuggestedFriend>> mo4438b(int i, int i2);

    /* renamed from: b */
    pdwpUtZXDT<PrivateUser> mo4439b(AuthIdentity authIdentity);

    /* renamed from: b */
    pdwpUtZXDT<PublicUser> mo4440b(String str);

    /* renamed from: b */
    pdwpUtZXDT<List<ActivityPost>> mo4441b(String str, ActivitiesQuery activitiesQuery);

    /* renamed from: b */
    pdwpUtZXDT<ActivityPost> mo4442b(String str, im.getsocial.sdk.activities.p028a.p029a.jjbQypPegg jjbqyppegg);

    /* renamed from: b */
    pdwpUtZXDT<Integer> mo4443b(String str, List<String> list);

    /* renamed from: c */
    pdwpUtZXDT<List<UserReference>> mo4444c();

    /* renamed from: c */
    pdwpUtZXDT<Integer> mo4445c(String str);

    /* renamed from: c */
    pdwpUtZXDT<Integer> mo4446c(String str, List<String> list);

    /* renamed from: d */
    pdwpUtZXDT<im.getsocial.sdk.invites.p092a.p094b.upgqDBbsrL> mo4447d();

    /* renamed from: d */
    pdwpUtZXDT<Integer> mo4448d(String str);

    /* renamed from: d */
    pdwpUtZXDT<Integer> mo4449d(String str, List<String> list);

    /* renamed from: e */
    pdwpUtZXDT<Boolean> mo4450e();

    /* renamed from: e */
    pdwpUtZXDT<Boolean> mo4451e(String str);

    /* renamed from: f */
    pdwpUtZXDT<List<ActivityPost>> mo4452f(String str);

    /* renamed from: g */
    pdwpUtZXDT<ActivityPost> mo4453g(String str);

    /* renamed from: h */
    pdwpUtZXDT<Void> mo4454h(String str);
}
