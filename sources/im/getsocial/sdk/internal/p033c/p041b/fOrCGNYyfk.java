package im.getsocial.sdk.internal.p033c.p041b;

import java.util.LinkedList;
import java.util.Queue;

/* renamed from: im.getsocial.sdk.internal.c.b.fOrCGNYyfk */
public class fOrCGNYyfk extends zoToeBNOjF {
    /* renamed from: a */
    private final Queue<Object> f1234a = new LinkedList();

    fOrCGNYyfk() {
        super(new pdwpUtZXDT());
    }

    /* renamed from: a */
    public final void m1190a(zoToeBNOjF zotoebnojf) {
        synchronized (this.f1234a) {
            while (!this.f1234a.isEmpty()) {
                zotoebnojf.mo4379a(this.f1234a.poll());
            }
        }
    }

    /* renamed from: a */
    public final void mo4379a(Object obj) {
        synchronized (this.f1234a) {
            this.f1234a.add(obj);
        }
    }
}
