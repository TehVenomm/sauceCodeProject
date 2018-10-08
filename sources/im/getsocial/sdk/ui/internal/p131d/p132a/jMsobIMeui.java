package im.getsocial.sdk.ui.internal.p131d.p132a;

/* renamed from: im.getsocial.sdk.ui.internal.d.a.jMsobIMeui */
public class jMsobIMeui {
    /* renamed from: a */
    private final String f2892a;

    public jMsobIMeui(String str) {
        this.f2892a = str;
    }

    public boolean equals(Object obj) {
        if (this != obj) {
            if (obj == null || getClass() != obj.getClass()) {
                return false;
            }
            jMsobIMeui jmsobimeui = (jMsobIMeui) obj;
            if (this.f2892a != null) {
                return this.f2892a.equals(jmsobimeui.f2892a);
            }
            if (jmsobimeui.f2892a != null) {
                return false;
            }
        }
        return true;
    }

    public int hashCode() {
        return this.f2892a == null ? 0 : this.f2892a.hashCode();
    }
}
