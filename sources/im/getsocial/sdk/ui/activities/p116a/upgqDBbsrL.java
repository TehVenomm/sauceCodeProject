package im.getsocial.sdk.ui.activities.p116a;

import com.facebook.internal.NativeProtocol;
import im.getsocial.sdk.GetSocial.User;
import im.getsocial.sdk.GetSocialException;
import im.getsocial.sdk.activities.ActivityPost;
import im.getsocial.sdk.activities.ActivityPostContent;
import im.getsocial.sdk.ui.AvatarClickListener;
import im.getsocial.sdk.ui.MentionClickListener;
import im.getsocial.sdk.ui.TagClickListener;
import im.getsocial.sdk.ui.UiAction.Pending;
import im.getsocial.sdk.ui.activities.ActionButtonListener;
import im.getsocial.sdk.ui.activities.p116a.p120i.pdwpUtZXDT;
import im.getsocial.sdk.ui.activities.p116a.pdwpUtZXDT.cjrhisSQCL;
import im.getsocial.sdk.ui.activities.p116a.pdwpUtZXDT.jjbQypPegg;
import im.getsocial.sdk.ui.internal.p126e.XdbacJlTDQ;
import im.getsocial.sdk.ui.internal.p131d.p132a.zoToeBNOjF;
import im.getsocial.sdk.ui.internal.views.InputContainer.Listener;
import im.getsocial.sdk.usermanagement.PublicUser;
import java.util.ArrayList;
import java.util.Collections;
import java.util.Comparator;
import java.util.HashMap;
import java.util.List;
import java.util.Map;
import java.util.regex.Matcher;
import java.util.regex.Pattern;

/* renamed from: im.getsocial.sdk.ui.activities.a.upgqDBbsrL */
public abstract class upgqDBbsrL<V extends cjrhisSQCL, M extends jjbQypPegg> extends im.getsocial.sdk.ui.activities.p116a.pdwpUtZXDT.upgqDBbsrL<V, M> implements im.getsocial.sdk.ui.activities.p116a.p120i.cjrhisSQCL, im.getsocial.sdk.ui.activities.p116a.p120i.jjbQypPegg, pdwpUtZXDT, im.getsocial.sdk.ui.activities.p116a.p120i.upgqDBbsrL, Listener {
    /* renamed from: a */
    private ActionButtonListener f2600a;
    /* renamed from: f */
    private AvatarClickListener f2601f;
    /* renamed from: g */
    private MentionClickListener f2602g;
    /* renamed from: h */
    private TagClickListener f2603h;
    /* renamed from: i */
    private im.getsocial.sdk.ui.internal.p126e.pdwpUtZXDT<im.getsocial.sdk.ui.activities.p116a.p121d.jjbQypPegg> f2604i;
    /* renamed from: j */
    private im.getsocial.sdk.ui.internal.p126e.pdwpUtZXDT<im.getsocial.sdk.ui.activities.p116a.p122h.jjbQypPegg> f2605j;

    /* renamed from: im.getsocial.sdk.ui.activities.a.upgqDBbsrL$1 */
    class C11281 implements Pending {
        /* renamed from: a */
        final /* synthetic */ upgqDBbsrL f2764a;

        C11281(upgqDBbsrL upgqdbbsrl) {
            this.f2764a = upgqdbbsrl;
        }

        public void proceed() {
            ((cjrhisSQCL) this.f2764a.mo4733t()).mo4678f();
            ((cjrhisSQCL) this.f2764a.mo4733t()).mo4663c();
            ((cjrhisSQCL) this.f2764a.mo4733t()).mo4671a(false);
            ((jjbQypPegg) this.f2764a.m2593y()).mo4653a(ActivityPostContent.createBuilderWithText(this.f2764a.f2604i.m3281a(new C11314(this.f2764a), true)).build());
        }
    }

    /* renamed from: im.getsocial.sdk.ui.activities.a.upgqDBbsrL$2 */
    static final class C11292 implements Comparator<im.getsocial.sdk.ui.activities.p116a.p121d.jjbQypPegg> {
        C11292() {
        }

        public final /* synthetic */ int compare(Object obj, Object obj2) {
            return String.CASE_INSENSITIVE_ORDER.compare(((im.getsocial.sdk.ui.activities.p116a.p121d.jjbQypPegg) obj).m3063c(), ((im.getsocial.sdk.ui.activities.p116a.p121d.jjbQypPegg) obj2).m3063c());
        }
    }

    /* renamed from: im.getsocial.sdk.ui.activities.a.upgqDBbsrL$3 */
    static final class C11303 implements Comparator<im.getsocial.sdk.ui.activities.p116a.p122h.jjbQypPegg> {
        C11303() {
        }

        public final /* synthetic */ int compare(Object obj, Object obj2) {
            return String.CASE_INSENSITIVE_ORDER.compare(((im.getsocial.sdk.ui.activities.p116a.p122h.jjbQypPegg) obj).m3079a(), ((im.getsocial.sdk.ui.activities.p116a.p122h.jjbQypPegg) obj2).m3079a());
        }
    }

    /* renamed from: im.getsocial.sdk.ui.activities.a.upgqDBbsrL$4 */
    class C11314 implements XdbacJlTDQ<im.getsocial.sdk.ui.activities.p116a.p121d.jjbQypPegg> {
        /* renamed from: a */
        final /* synthetic */ upgqDBbsrL f2765a;

        C11314(upgqDBbsrL upgqdbbsrl) {
            this.f2765a = upgqdbbsrl;
        }

        /* renamed from: a */
        public final /* synthetic */ String mo4715a(im.getsocial.sdk.ui.internal.p126e.cjrhisSQCL cjrhissqcl) {
            return String.format("@%s", new Object[]{((im.getsocial.sdk.ui.activities.p116a.p121d.jjbQypPegg) cjrhissqcl).m3064d()});
        }
    }

    /* renamed from: im.getsocial.sdk.ui.activities.a.upgqDBbsrL$cjrhisSQCL */
    private class cjrhisSQCL implements im.getsocial.sdk.ui.internal.p126e.pdwpUtZXDT.upgqDBbsrL {
        /* renamed from: a */
        final Pattern f2766a;
        /* renamed from: b */
        final /* synthetic */ upgqDBbsrL f2767b;

        private cjrhisSQCL(upgqDBbsrL upgqdbbsrl) {
            this.f2767b = upgqdbbsrl;
            this.f2766a = Pattern.compile("([\\w\\d_]*)");
        }

        /* renamed from: a */
        public final void mo4716a() {
            ((im.getsocial.sdk.ui.activities.p116a.pdwpUtZXDT.cjrhisSQCL) this.f2767b.mo4733t()).mo4677e();
        }

        /* renamed from: a */
        public final void mo4717a(String str) {
            Object obj = (str.isEmpty() || (this.f2766a.matcher(str).matches() && str.length() <= 80)) ? 1 : null;
            if (obj == null) {
                mo4716a();
                return;
            }
            ((im.getsocial.sdk.ui.activities.p116a.pdwpUtZXDT.cjrhisSQCL) this.f2767b.mo4733t()).mo4681i();
            ((jjbQypPegg) this.f2767b.m2593y()).mo4645b(str);
        }
    }

    /* renamed from: im.getsocial.sdk.ui.activities.a.upgqDBbsrL$jjbQypPegg */
    private class jjbQypPegg implements im.getsocial.sdk.ui.internal.p126e.pdwpUtZXDT.upgqDBbsrL {
        /* renamed from: a */
        final /* synthetic */ upgqDBbsrL f2768a;

        private jjbQypPegg(upgqDBbsrL upgqdbbsrl) {
            this.f2768a = upgqdbbsrl;
        }

        /* renamed from: a */
        public final void mo4716a() {
            ((cjrhisSQCL) this.f2768a.mo4733t()).mo4676d();
        }

        /* renamed from: a */
        public final void mo4717a(String str) {
            ((cjrhisSQCL) this.f2768a.mo4733t()).mo4681i();
            ((im.getsocial.sdk.ui.activities.p116a.pdwpUtZXDT.jjbQypPegg) this.f2768a.m2593y()).mo4642a(str);
        }
    }

    /* renamed from: im.getsocial.sdk.ui.activities.a.upgqDBbsrL$upgqDBbsrL */
    private class upgqDBbsrL implements im.getsocial.sdk.ui.internal.p126e.pdwpUtZXDT.jjbQypPegg<im.getsocial.sdk.ui.activities.p116a.p122h.jjbQypPegg> {
        /* renamed from: a */
        final /* synthetic */ upgqDBbsrL f2769a;

        private upgqDBbsrL(upgqDBbsrL upgqdbbsrl) {
            this.f2769a = upgqdbbsrl;
        }

        /* renamed from: a */
        public final List<im.getsocial.sdk.ui.internal.p126e.pdwpUtZXDT.cjrhisSQCL<im.getsocial.sdk.ui.activities.p116a.p122h.jjbQypPegg>> mo4718a(String str) {
            Matcher matcher = im.getsocial.sdk.ui.activities.p116a.p122h.jjbQypPegg.f2752a.matcher(str);
            List<im.getsocial.sdk.ui.internal.p126e.pdwpUtZXDT.cjrhisSQCL<im.getsocial.sdk.ui.activities.p116a.p122h.jjbQypPegg>> arrayList = new ArrayList();
            while (matcher.find()) {
                String substring = matcher.group().substring(1);
                if (substring.length() <= 80) {
                    arrayList.add(new im.getsocial.sdk.ui.internal.p126e.pdwpUtZXDT.cjrhisSQCL(new im.getsocial.sdk.ui.activities.p116a.p122h.jjbQypPegg(substring), matcher.start()));
                }
            }
            return arrayList;
        }
    }

    public upgqDBbsrL(V v, M m) {
        super(v, m);
        ((cjrhisSQCL) mo4733t()).mo4666a(mo4636k());
    }

    /* renamed from: a */
    public final void m2684a(GetSocialException getSocialException) {
        m2580a((Throwable) getSocialException);
        ((cjrhisSQCL) mo4733t()).m2541v();
        ((cjrhisSQCL) mo4733t()).mo4671a(true);
    }

    /* renamed from: a */
    protected final void m2685a(ActivityPost activityPost) {
        if (activityPost.hasButton()) {
            String buttonAction = activityPost.getButtonAction();
            String buttonTitle = activityPost.getButtonTitle();
            Map hashMap = new HashMap();
            hashMap.put(NativeProtocol.WEB_DIALOG_ACTION, buttonAction);
            hashMap.put("title", buttonTitle);
            m2579a("ui_user_activity_action_click", hashMap);
            this.f2600a.onButtonClicked(activityPost.getButtonAction(), activityPost);
        }
    }

    /* renamed from: a */
    public final void mo4616a(AvatarClickListener avatarClickListener) {
        this.f2601f = avatarClickListener;
    }

    /* renamed from: a */
    public final void mo4617a(MentionClickListener mentionClickListener) {
        this.f2602g = mentionClickListener;
    }

    /* renamed from: a */
    public final void mo4618a(TagClickListener tagClickListener) {
        this.f2603h = tagClickListener;
    }

    /* renamed from: a */
    protected abstract void mo4628a(Pending pending);

    /* renamed from: a */
    public final void mo4619a(ActionButtonListener actionButtonListener) {
        this.f2600a = actionButtonListener;
    }

    /* renamed from: a */
    public final void m2691a(im.getsocial.sdk.ui.activities.p116a.p121d.jjbQypPegg jjbqyppegg) {
        this.f2604i.m3285a((im.getsocial.sdk.ui.internal.p126e.cjrhisSQCL) jjbqyppegg);
    }

    /* renamed from: a */
    public final void m2692a(im.getsocial.sdk.ui.activities.p116a.p122h.jjbQypPegg jjbqyppegg) {
        this.f2605j.m3285a((im.getsocial.sdk.ui.internal.p126e.cjrhisSQCL) jjbqyppegg);
    }

    /* renamed from: a */
    protected final void m2693a(PublicUser publicUser) {
        if (!new im.getsocial.sdk.usermanagement.pdwpUtZXDT(publicUser).m3730a() && this.f2601f != null) {
            this.f2601f.onAvatarClicked(publicUser);
        }
    }

    /* renamed from: a */
    protected final void m2694a(String str) {
        this.f2602g.onMentionClicked(str);
    }

    /* renamed from: a */
    public final void mo4620a(List<im.getsocial.sdk.ui.activities.p116a.p121d.jjbQypPegg> list) {
        ((cjrhisSQCL) mo4733t()).mo4682j();
        if (list.isEmpty()) {
            ((cjrhisSQCL) mo4733t()).mo4676d();
            return;
        }
        cjrhisSQCL cjrhissqcl = (cjrhisSQCL) mo4733t();
        List arrayList = new ArrayList(list);
        Collections.sort(arrayList, new C11292());
        cjrhissqcl.mo4673b(arrayList);
    }

    /* renamed from: b */
    protected final void m2696b(ActivityPost activityPost) {
        String id = activityPost.getId();
        Map hashMap = new HashMap();
        hashMap.put("activity_id", id);
        m2579a("ui_user_activity_play_video_click", hashMap);
    }

    /* renamed from: b */
    protected final void m2697b(String str) {
        this.f2603h.onTagClicked(str);
    }

    /* renamed from: b */
    public final void mo4621b(List<im.getsocial.sdk.ui.activities.p116a.p122h.jjbQypPegg> list) {
        ((cjrhisSQCL) mo4733t()).mo4682j();
        if (list.isEmpty()) {
            ((cjrhisSQCL) mo4733t()).mo4677e();
            return;
        }
        cjrhisSQCL cjrhissqcl = (cjrhisSQCL) mo4733t();
        List arrayList = new ArrayList(list);
        Collections.sort(arrayList, new C11303());
        cjrhissqcl.mo4675c(arrayList);
    }

    public final void b_() {
        mo4628a(new C11281(this));
    }

    /* renamed from: c */
    protected final void m2699c(ActivityPost activityPost) {
        im.getsocial.sdk.ui.internal.upgqDBbsrL.m3461a();
        im.getsocial.sdk.ui.internal.upgqDBbsrL.m3462a((im.getsocial.sdk.ui.internal.p114i.jjbQypPegg.upgqDBbsrL) this, activityPost, ((jjbQypPegg) m2593y()).mo4649d());
    }

    /* renamed from: d */
    protected final void m2700d(ActivityPost activityPost) {
        im.getsocial.sdk.ui.internal.upgqDBbsrL.m3461a();
        im.getsocial.sdk.ui.internal.upgqDBbsrL.m3465b(this, activityPost, ((jjbQypPegg) m2593y()).mo4649d());
    }

    public void d_() {
        ((cjrhisSQCL) mo4733t()).m2540u();
        ((jjbQypPegg) m2593y()).mo4659h();
        this.f2604i = ((cjrhisSQCL) mo4733t()).mo4679g();
        this.f2604i.m3287a(new jjbQypPegg());
        this.f2605j = ((cjrhisSQCL) mo4733t()).mo4680h();
        this.f2605j.m3287a(new cjrhisSQCL());
        zoToeBNOjF c = im.getsocial.sdk.ui.internal.p131d.upgqDBbsrL.m3237a().m3248a(im.getsocial.sdk.ui.internal.p131d.upgqDBbsrL.m3237a().m3255b().m3212c().m3126j().m3209a()).m3156c();
        zoToeBNOjF a = im.getsocial.sdk.ui.internal.p131d.upgqDBbsrL.m3237a().m3255b().m3212c().m3132p().m3145a();
        this.f2604i.m3283a(a.m3215a());
        this.f2604i.m3289b(c.m3215a());
        this.f2604i.m3288a(true);
        this.f2605j.m3283a(a.m3215a());
        this.f2605j.m3289b(c.m3215a());
        this.f2605j.m3288a(false);
        this.f2605j.m3286a(new upgqDBbsrL());
    }

    /* renamed from: e */
    public final ActionButtonListener mo4623e() {
        return this.f2600a;
    }

    /* renamed from: e */
    protected final void m2702e(ActivityPost activityPost) {
        if (activityPost.getAuthor().getId().equals(User.getId())) {
            mo4635g(activityPost);
        } else {
            mo4634f(activityPost);
        }
    }

    /* renamed from: f */
    public final AvatarClickListener mo4624f() {
        return this.f2601f;
    }

    /* renamed from: f */
    protected abstract void mo4634f(ActivityPost activityPost);

    /* renamed from: g */
    public final MentionClickListener mo4625g() {
        return this.f2602g;
    }

    /* renamed from: g */
    protected abstract void mo4635g(ActivityPost activityPost);

    /* renamed from: h */
    public final TagClickListener mo4626h() {
        return this.f2603h;
    }

    /* renamed from: i */
    public final void m2708i() {
        ((cjrhisSQCL) mo4733t()).m2541v();
    }

    /* renamed from: j */
    public final void mo4627j() {
        ((cjrhisSQCL) mo4733t()).m2541v();
        ((cjrhisSQCL) mo4733t()).m2533a(this.c.strings().ReportNotificationTitle, this.c.strings().ReportNotificationText);
    }

    /* renamed from: k */
    protected abstract im.getsocial.sdk.ui.activities.p116a.pdwpUtZXDT.cjrhisSQCL.upgqDBbsrL mo4636k();
}
