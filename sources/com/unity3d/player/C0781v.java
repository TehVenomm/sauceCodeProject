package com.unity3d.player;

/* renamed from: com.unity3d.player.v */
final class C0781v {
    /* renamed from: a */
    private static boolean f570a = false;
    /* renamed from: b */
    private boolean f571b;
    /* renamed from: c */
    private boolean f572c;
    /* renamed from: d */
    private boolean f573d;
    /* renamed from: e */
    private boolean f574e;

    C0781v() {
        this.f571b = !C0773q.f542h;
        this.f572c = false;
        this.f573d = false;
        this.f574e = true;
    }

    /* renamed from: a */
    static void m543a() {
        f570a = true;
    }

    /* renamed from: b */
    static void m544b() {
        f570a = false;
    }

    /* renamed from: c */
    static boolean m545c() {
        return f570a;
    }

    /* renamed from: a */
    final void m546a(boolean z) {
        this.f572c = z;
    }

    /* renamed from: b */
    final void m547b(boolean z) {
        this.f574e = z;
    }

    /* renamed from: c */
    final void m548c(boolean z) {
        this.f573d = z;
    }

    /* renamed from: d */
    final void m549d() {
        this.f571b = true;
    }

    /* renamed from: e */
    final boolean m550e() {
        return this.f574e;
    }

    /* renamed from: f */
    final boolean m551f() {
        return f570a && this.f572c && this.f571b && !this.f574e && !this.f573d;
    }

    /* renamed from: g */
    final boolean m552g() {
        return this.f573d;
    }

    public final String toString() {
        return super.toString();
    }
}
