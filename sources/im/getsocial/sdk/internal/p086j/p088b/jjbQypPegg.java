package im.getsocial.sdk.internal.p086j.p088b;

/* renamed from: im.getsocial.sdk.internal.j.b.jjbQypPegg */
public final class jjbQypPegg {
    /* renamed from: a */
    private final upgqDBbsrL f1995a;
    /* renamed from: b */
    private final byte[] f1996b;

    private jjbQypPegg(byte[] bArr, upgqDBbsrL upgqdbbsrl) {
        this.f1996b = (byte[]) bArr.clone();
        this.f1995a = upgqdbbsrl;
    }

    /* renamed from: a */
    public static jjbQypPegg m2007a(byte[] bArr) {
        return bArr == null ? null : new jjbQypPegg(bArr, upgqDBbsrL.IMAGE);
    }

    /* renamed from: b */
    public static jjbQypPegg m2008b(byte[] bArr) {
        return bArr == null ? null : new jjbQypPegg(bArr, upgqDBbsrL.VIDEO);
    }

    /* renamed from: a */
    public final byte[] m2009a() {
        return (byte[]) this.f1996b.clone();
    }

    /* renamed from: b */
    public final upgqDBbsrL m2010b() {
        return this.f1995a;
    }

    /* renamed from: c */
    public final boolean m2011c() {
        return (this.f1995a == null || this.f1996b.length == 0) ? false : true;
    }
}
