package im.getsocial.sdk.ui.activities.p116a.p124c;

import im.getsocial.sdk.GetSocialException;
import im.getsocial.sdk.activities.ActivityPost;
import im.getsocial.sdk.sharedl10n.LocalizationAdapter;
import im.getsocial.sdk.ui.AvatarClickListener;
import im.getsocial.sdk.ui.activities.p116a.p124c.cjrhisSQCL.cjrhisSQCL;
import im.getsocial.sdk.ui.activities.p116a.p124c.cjrhisSQCL.jjbQypPegg;
import im.getsocial.sdk.ui.activities.p116a.p124c.cjrhisSQCL.upgqDBbsrL;
import im.getsocial.sdk.usermanagement.PublicUser;
import java.util.List;

/* renamed from: im.getsocial.sdk.ui.activities.a.c.pdwpUtZXDT */
class pdwpUtZXDT extends upgqDBbsrL implements im.getsocial.sdk.ui.activities.p116a.p120i.upgqDBbsrL {
    /* renamed from: a */
    private final ActivityPost f2712a;
    /* renamed from: f */
    private AvatarClickListener f2713f;

    pdwpUtZXDT(cjrhisSQCL cjrhissqcl, jjbQypPegg jjbqyppegg, ActivityPost activityPost) {
        super(cjrhissqcl, jjbqyppegg);
        this.f2712a = activityPost;
    }

    /* renamed from: a */
    public final String mo4591a() {
        return this.f2712a.getLikesCount() + " " + LocalizationAdapter.likes(this.c, this.f2712a.getLikesCount());
    }

    /* renamed from: a */
    public final void mo4699a(GetSocialException getSocialException) {
        m2580a((Throwable) getSocialException);
        ((cjrhisSQCL) mo4733t()).m2541v();
        ((cjrhisSQCL) mo4733t()).m2534a(this.c.strings().ConnectionLostTitle, this.c.strings().ConnectionLostMessage, im.getsocial.sdk.ui.internal.p131d.upgqDBbsrL.m3237a().m3255b().m3212c().m3142z().m3173a());
    }

    /* renamed from: a */
    public final void mo4616a(AvatarClickListener avatarClickListener) {
        this.f2713f = avatarClickListener;
    }

    /* renamed from: a */
    public final void mo4700a(PublicUser publicUser) {
        if (!new im.getsocial.sdk.usermanagement.pdwpUtZXDT(publicUser).m3730a() && this.f2713f != null) {
            this.f2713f.onAvatarClicked(publicUser);
        }
    }

    /* renamed from: a */
    public final void mo4701a(List<PublicUser> list) {
        ((cjrhisSQCL) mo4733t()).m2541v();
        if (!list.isEmpty()) {
            ((cjrhisSQCL) mo4733t()).m2543x();
        }
        ((cjrhisSQCL) mo4733t()).mo4697a(list);
    }

    /* renamed from: b */
    public String mo4592b() {
        return "activity_likelist";
    }

    /* renamed from: d */
    public final void mo4702d() {
        ((cjrhisSQCL) mo4733t()).m2541v();
    }

    public final void d_() {
        ((cjrhisSQCL) mo4733t()).m2540u();
        ((jjbQypPegg) m2593y()).mo4705b();
    }

    /* renamed from: e */
    public final void mo4703e() {
        ((cjrhisSQCL) mo4733t()).m2535b(this.c.strings().ActivityNotFound);
        m2592x();
    }

    /* renamed from: f */
    public final AvatarClickListener mo4624f() {
        return this.f2713f;
    }

    /* renamed from: g */
    public final void mo4704g() {
        ((cjrhisSQCL) mo4733t()).m2540u();
        ((jjbQypPegg) m2593y()).mo4706c();
    }
}
