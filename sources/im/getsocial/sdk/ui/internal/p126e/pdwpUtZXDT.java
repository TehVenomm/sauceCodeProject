package im.getsocial.sdk.ui.internal.p126e;

import android.text.Editable;
import android.text.TextWatcher;
import android.text.style.BackgroundColorSpan;
import android.text.style.ForegroundColorSpan;
import android.view.View;
import android.view.View.OnFocusChangeListener;
import android.widget.EditText;
import im.getsocial.sdk.internal.p033c.p066m.cjrhisSQCL;
import java.util.Collection;
import java.util.Comparator;
import java.util.HashMap;
import java.util.Iterator;
import java.util.List;
import java.util.Map;
import java.util.Map.Entry;
import java.util.Set;
import java.util.TreeSet;

/* renamed from: im.getsocial.sdk.ui.internal.e.pdwpUtZXDT */
public class pdwpUtZXDT<M extends cjrhisSQCL> implements TextWatcher, OnFocusChangeListener, upgqDBbsrL {
    /* renamed from: a */
    private final Map<Class, pdwpUtZXDT> f2942a = new HashMap();
    /* renamed from: b */
    private final EditText f2943b;
    /* renamed from: c */
    private final Set<cjrhisSQCL<M>> f2944c;
    /* renamed from: d */
    private final String f2945d;
    /* renamed from: e */
    private final jjbQypPegg f2946e;
    /* renamed from: f */
    private upgqDBbsrL f2947f = ((upgqDBbsrL) cjrhisSQCL.m1509a(upgqDBbsrL.class));
    /* renamed from: g */
    private jjbQypPegg<M> f2948g;
    /* renamed from: h */
    private boolean f2949h;

    /* renamed from: im.getsocial.sdk.ui.internal.e.pdwpUtZXDT$upgqDBbsrL */
    public interface upgqDBbsrL {
        /* renamed from: a */
        void mo4716a();

        /* renamed from: a */
        void mo4717a(String str);
    }

    /* renamed from: im.getsocial.sdk.ui.internal.e.pdwpUtZXDT$jjbQypPegg */
    public interface jjbQypPegg<M extends cjrhisSQCL> {
        /* renamed from: a */
        List<cjrhisSQCL<M>> mo4718a(String str);
    }

    /* renamed from: im.getsocial.sdk.ui.internal.e.pdwpUtZXDT$1 */
    class C11501 implements Comparator<cjrhisSQCL> {
        /* renamed from: a */
        final /* synthetic */ pdwpUtZXDT f2933a;

        C11501(pdwpUtZXDT pdwputzxdt) {
            this.f2933a = pdwputzxdt;
        }

        public /* synthetic */ int compare(Object obj, Object obj2) {
            return -(((cjrhisSQCL) obj).m3270a() - ((cjrhisSQCL) obj2).m3270a());
        }
    }

    /* renamed from: im.getsocial.sdk.ui.internal.e.pdwpUtZXDT$pdwpUtZXDT */
    private interface pdwpUtZXDT {
        /* renamed from: a */
        Object mo4727a();
    }

    /* renamed from: im.getsocial.sdk.ui.internal.e.pdwpUtZXDT$4 */
    static final class C11534 implements jjbQypPegg<T> {
        C11534() {
        }

        /* renamed from: a */
        public final List<cjrhisSQCL<T>> mo4718a(String str) {
            return null;
        }
    }

    /* renamed from: im.getsocial.sdk.ui.internal.e.pdwpUtZXDT$cjrhisSQCL */
    public static class cjrhisSQCL<M extends cjrhisSQCL> {
        /* renamed from: a */
        private final M f2938a;
        /* renamed from: b */
        private int f2939b;
        /* renamed from: c */
        private int f2940c;
        /* renamed from: d */
        private int f2941d;

        public cjrhisSQCL(M m, int i) {
            this.f2938a = m;
            m3269d(i);
        }

        /* renamed from: d */
        private void m3269d(int i) {
            this.f2939b = i;
            this.f2941d = this.f2938a.mo4714b().length();
            this.f2940c = this.f2939b + this.f2941d;
        }

        /* renamed from: a */
        final int m3270a() {
            return this.f2939b;
        }

        /* renamed from: a */
        final boolean m3271a(int i) {
            return this.f2939b >= i;
        }

        /* renamed from: b */
        final int m3272b() {
            return this.f2940c;
        }

        /* renamed from: b */
        final boolean m3273b(int i) {
            return this.f2939b + i >= 0;
        }

        /* renamed from: c */
        final M m3274c() {
            return this.f2938a;
        }

        /* renamed from: c */
        final void m3275c(int i) {
            m3269d(this.f2939b + i);
        }
    }

    public pdwpUtZXDT(EditText editText, jjbQypPegg jjbqyppegg, String str) {
        this.f2943b = editText;
        this.f2945d = str;
        this.f2944c = new TreeSet(new C11501(this));
        this.f2946e = jjbqyppegg;
        this.f2948g = new C11534();
        this.f2949h = true;
        this.f2943b.addTextChangedListener(this);
        this.f2946e.m3264a(this);
    }

    /* renamed from: a */
    private static boolean m3278a(cjrhisSQCL cjrhissqcl, int i, int i2, CharSequence charSequence) {
        if (charSequence.length() < i2) {
            return false;
        }
        return cjrhissqcl.mo4714b().equals(charSequence.subSequence(i, i2).toString());
    }

    /* JADX WARNING: inconsistent code. */
    /* Code decompiled incorrectly, please refer to instructions dump. */
    /* renamed from: b */
    private void m3279b() {
        /*
        r7 = this;
        r1 = 1;
        r2 = 0;
        r0 = 0;
        r3 = r7.f2943b;
        r3 = r3.getText();
        r3 = r3.toString();
        r4 = r7.f2945d;
        r4 = r3.contains(r4);
        if (r4 != 0) goto L_0x001d;
    L_0x0015:
        if (r0 != 0) goto L_0x005c;
    L_0x0017:
        r0 = r7.f2947f;
        r0.mo4716a();
    L_0x001c:
        return;
    L_0x001d:
        r4 = r7.f2943b;
        r4 = r4.getSelectionStart();
        r4 = r3.substring(r2, r4);
        r3 = r7.f2945d;
        r5 = r4.lastIndexOf(r3);
        r3 = -1;
        if (r5 == r3) goto L_0x0015;
    L_0x0030:
        r3 = r7.f2949h;
        if (r3 == 0) goto L_0x0043;
    L_0x0034:
        if (r5 == 0) goto L_0x0051;
    L_0x0036:
        r3 = r5 + -1;
        r3 = r4.charAt(r3);
        r6 = 32;
        if (r3 == r6) goto L_0x0051;
    L_0x0040:
        r3 = r1;
    L_0x0041:
        if (r3 != 0) goto L_0x0015;
    L_0x0043:
        r0 = r4.length();
        r3 = r5 + 1;
        if (r0 != r3) goto L_0x0053;
    L_0x004b:
        r0 = r1;
    L_0x004c:
        if (r0 == 0) goto L_0x0055;
    L_0x004e:
        r0 = "";
        goto L_0x0015;
    L_0x0051:
        r3 = r2;
        goto L_0x0041;
    L_0x0053:
        r0 = r2;
        goto L_0x004c;
    L_0x0055:
        r0 = r5 + 1;
        r0 = r4.substring(r0);
        goto L_0x0015;
    L_0x005c:
        r1 = r7.f2947f;
        r1.mo4717a(r0);
        goto L_0x001c;
        */
        throw new UnsupportedOperationException("Method not decompiled: im.getsocial.sdk.ui.internal.e.pdwpUtZXDT.b():void");
    }

    /* renamed from: c */
    private void m3280c() {
        Collection a = this.f2948g.mo4718a(this.f2943b.getText().toString());
        if (a != null) {
            this.f2944c.clear();
            this.f2944c.addAll(a);
        }
        this.f2946e.m3263a();
    }

    /* renamed from: a */
    public final String m3281a(XdbacJlTDQ<M> xdbacJlTDQ, boolean z) {
        StringBuilder stringBuilder = new StringBuilder(this.f2943b.getText());
        for (cjrhisSQCL cjrhissqcl : this.f2944c) {
            int a = cjrhissqcl.m3270a();
            int b = cjrhissqcl.m3272b();
            if (pdwpUtZXDT.m3278a(cjrhissqcl.m3274c(), a, b, stringBuilder)) {
                if (stringBuilder.length() != b && Character.isDigit(stringBuilder.charAt(b))) {
                    stringBuilder.insert(b, ' ');
                }
                stringBuilder.replace(a, b, xdbacJlTDQ.mo4715a(cjrhissqcl.m3274c()));
            }
        }
        return stringBuilder.toString();
    }

    /* renamed from: a */
    public final Set<Class> mo4728a() {
        return this.f2942a.keySet();
    }

    /* renamed from: a */
    public final void m3283a(final int i) {
        this.f2942a.put(BackgroundColorSpan.class, new pdwpUtZXDT(this) {
            /* renamed from: b */
            final /* synthetic */ pdwpUtZXDT f2935b;

            /* renamed from: a */
            public final Object mo4727a() {
                return new BackgroundColorSpan(i);
            }
        });
    }

    /* renamed from: a */
    public final void mo4729a(Editable editable) {
        if (!this.f2944c.isEmpty()) {
            Iterator it = this.f2944c.iterator();
            while (it.hasNext()) {
                cjrhisSQCL cjrhissqcl = (cjrhisSQCL) it.next();
                int a = cjrhissqcl.m3270a();
                int b = cjrhissqcl.m3272b();
                if (pdwpUtZXDT.m3278a(cjrhissqcl.m3274c(), a, b, editable)) {
                    for (Entry value : this.f2942a.entrySet()) {
                        editable.setSpan(((pdwpUtZXDT) value.getValue()).mo4727a(), a, b, 33);
                    }
                } else {
                    it.remove();
                }
            }
        }
    }

    /* renamed from: a */
    public final void m3285a(M m) {
        int selectionEnd = this.f2943b.getSelectionEnd();
        int lastIndexOf = this.f2943b.getText().toString().substring(0, selectionEnd).lastIndexOf(this.f2945d);
        if (lastIndexOf != -1) {
            CharSequence charSequence = m.mo4714b() + " ";
            int length = charSequence.length();
            this.f2943b.getText().delete(lastIndexOf, selectionEnd);
            this.f2943b.getText().insert(lastIndexOf, charSequence);
            selectionEnd = this.f2943b.getInputType();
            this.f2943b.setInputType(524288);
            this.f2943b.setInputType(selectionEnd);
            this.f2943b.setSelection(length + lastIndexOf);
            this.f2944c.add(new cjrhisSQCL(m, lastIndexOf));
            m3280c();
        }
    }

    /* renamed from: a */
    public final void m3286a(jjbQypPegg<M> jjbqyppegg) {
        this.f2948g = jjbqyppegg;
    }

    /* renamed from: a */
    public final void m3287a(upgqDBbsrL upgqdbbsrl) {
        this.f2947f = (upgqDBbsrL) cjrhisSQCL.m1510a(upgqDBbsrL.class, upgqdbbsrl);
    }

    /* renamed from: a */
    public final void m3288a(boolean z) {
        this.f2949h = z;
    }

    public void afterTextChanged(Editable editable) {
        m3279b();
        m3280c();
    }

    /* renamed from: b */
    public final void m3289b(final int i) {
        this.f2942a.put(ForegroundColorSpan.class, new pdwpUtZXDT(this) {
            /* renamed from: b */
            final /* synthetic */ pdwpUtZXDT f2937b;

            /* renamed from: a */
            public final Object mo4727a() {
                return new ForegroundColorSpan(i);
            }
        });
    }

    public void beforeTextChanged(CharSequence charSequence, int i, int i2, int i3) {
    }

    public void onClick(View view) {
        m3279b();
    }

    public void onFocusChange(View view, boolean z) {
        if (z) {
            m3279b();
        } else {
            this.f2947f.mo4716a();
        }
    }

    public void onTextChanged(CharSequence charSequence, int i, int i2, int i3) {
        int i4 = i3 - i2;
        if (i4 != 0) {
            Iterator it = this.f2944c.iterator();
            while (it.hasNext()) {
                cjrhisSQCL cjrhissqcl = (cjrhisSQCL) it.next();
                if (cjrhissqcl.m3271a(i)) {
                    if (cjrhissqcl.m3273b(i4)) {
                        cjrhissqcl.m3275c(i4);
                    } else {
                        it.remove();
                    }
                }
            }
        }
    }
}
