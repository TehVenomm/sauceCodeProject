package im.getsocial.sdk.ui.internal.p114i.p136a;

import im.getsocial.sdk.ui.internal.p114i.jjbQypPegg.upgqDBbsrL;
import java.util.Stack;

/* renamed from: im.getsocial.sdk.ui.internal.i.a.jjbQypPegg */
public class jjbQypPegg {
    /* renamed from: a */
    private final Stack<upgqDBbsrL> f3011a = new Stack();
    /* renamed from: b */
    private final Stack<upgqDBbsrL> f3012b = new Stack();

    /* renamed from: im.getsocial.sdk.ui.internal.i.a.jjbQypPegg$jjbQypPegg */
    public interface jjbQypPegg {
        /* renamed from: a */
        void mo4730a(upgqDBbsrL upgqdbbsrl);
    }

    /* renamed from: a */
    public final void m3370a(jjbQypPegg jjbqyppegg) {
        if (!this.f3011a.isEmpty()) {
            if (this.f3011a.isEmpty()) {
                throw new IllegalStateException("Can not get top presenter, use usingTopPresenter method to avoid exceptions");
            }
            jjbqyppegg.mo4730a((upgqDBbsrL) this.f3011a.peek());
        }
    }

    /* renamed from: a */
    public final void m3371a(upgqDBbsrL upgqdbbsrl) {
        if (!this.f3011a.isEmpty()) {
            this.f3011a.pop();
        }
        this.f3011a.push(upgqdbbsrl);
    }

    /* renamed from: a */
    public final boolean m3372a() {
        return this.f3011a.isEmpty();
    }

    /* renamed from: b */
    public final upgqDBbsrL m3373b() {
        if (this.f3011a.isEmpty()) {
            throw new IllegalStateException("Can not go back");
        }
        this.f3011a.pop();
        return (upgqDBbsrL) this.f3011a.peek();
    }

    /* renamed from: b */
    public final void m3374b(jjbQypPegg jjbqyppegg) {
        while (!this.f3011a.isEmpty()) {
            jjbqyppegg.mo4730a((upgqDBbsrL) this.f3011a.pop());
        }
        this.f3012b.clear();
    }

    /* renamed from: b */
    public final void m3375b(upgqDBbsrL upgqdbbsrl) {
        this.f3011a.push(upgqdbbsrl);
    }

    /* renamed from: c */
    public final upgqDBbsrL m3376c() {
        if (this.f3011a.isEmpty()) {
            throw new IllegalStateException("Can not go pop to root");
        }
        while (this.f3011a.size() > 1) {
            this.f3011a.pop();
        }
        return (upgqDBbsrL) this.f3011a.peek();
    }

    /* renamed from: d */
    public final boolean m3377d() {
        return this.f3011a.size() == 1;
    }

    /* renamed from: e */
    public final boolean m3378e() {
        return this.f3011a.size() > 1;
    }

    /* renamed from: f */
    public final void m3379f() {
        this.f3012b.clear();
    }

    /* renamed from: g */
    public final boolean m3380g() {
        return !this.f3012b.empty();
    }

    /* renamed from: h */
    public final void m3381h() {
        this.f3012b.clear();
        this.f3012b.addAll(this.f3011a);
        this.f3011a.clear();
    }

    /* renamed from: i */
    public final void m3382i() {
        this.f3011a.clear();
        this.f3011a.addAll(this.f3012b);
        this.f3012b.clear();
    }
}
