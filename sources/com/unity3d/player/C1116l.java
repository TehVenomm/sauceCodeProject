package com.unity3d.player;

/* renamed from: com.unity3d.player.l */
final class C1116l {

    /* renamed from: a */
    private static boolean f603a = false;

    /* renamed from: b */
    private boolean f604b;

    /* renamed from: c */
    private boolean f605c;

    /* renamed from: d */
    private boolean f606d;

    /* renamed from: e */
    private boolean f607e;

    C1116l() {
        this.f604b = !C1107h.f582c;
        this.f605c = false;
        this.f606d = false;
        this.f607e = true;
    }

    /* renamed from: a */
    static void m548a() {
        f603a = true;
    }

    /* renamed from: b */
    static void m549b() {
        f603a = false;
    }

    /* renamed from: c */
    static boolean m550c() {
        return f603a;
    }

    /* access modifiers changed from: 0000 */
    /* renamed from: a */
    public final void mo20530a(boolean z) {
        this.f605c = z;
    }

    /* access modifiers changed from: 0000 */
    /* renamed from: b */
    public final void mo20531b(boolean z) {
        this.f607e = z;
    }

    /* access modifiers changed from: 0000 */
    /* renamed from: c */
    public final void mo20532c(boolean z) {
        this.f606d = z;
    }

    /* access modifiers changed from: 0000 */
    /* renamed from: d */
    public final void mo20533d() {
        this.f604b = true;
    }

    /* access modifiers changed from: 0000 */
    /* renamed from: e */
    public final boolean mo20534e() {
        return this.f607e;
    }

    /* access modifiers changed from: 0000 */
    /* renamed from: f */
    public final boolean mo20535f() {
        return f603a && this.f605c && this.f604b && !this.f607e && !this.f606d;
    }

    /* access modifiers changed from: 0000 */
    /* renamed from: g */
    public final boolean mo20536g() {
        return this.f606d;
    }

    public final String toString() {
        return super.toString();
    }
}
