package im.getsocial.sdk.ui.internal.p126e;

import android.text.Editable;
import android.view.View;
import android.view.View.OnClickListener;
import android.view.View.OnFocusChangeListener;
import android.widget.EditText;
import java.util.HashSet;
import java.util.Set;

/* renamed from: im.getsocial.sdk.ui.internal.e.jjbQypPegg */
public final class jjbQypPegg implements OnClickListener, OnFocusChangeListener {
    /* renamed from: a */
    private final EditText f2931a;
    /* renamed from: b */
    private final Set<upgqDBbsrL> f2932b = new HashSet();

    public jjbQypPegg(EditText editText) {
        this.f2931a = editText;
        this.f2931a.setOnClickListener(this);
        this.f2931a.setOnFocusChangeListener(this);
    }

    /* renamed from: a */
    private static void m3262a(Set<Class> set, Editable editable) {
        for (Class spans : set) {
            for (Object removeSpan : editable.getSpans(0, editable.length(), spans)) {
                editable.removeSpan(removeSpan);
            }
        }
    }

    /* renamed from: a */
    final void m3263a() {
        Editable text = this.f2931a.getText();
        Set hashSet = new HashSet();
        for (upgqDBbsrL a : this.f2932b) {
            hashSet.addAll(a.mo4728a());
        }
        jjbQypPegg.m3262a(hashSet, text);
        for (upgqDBbsrL a2 : this.f2932b) {
            a2.mo4729a(text);
        }
    }

    /* renamed from: a */
    final void m3264a(upgqDBbsrL upgqdbbsrl) {
        this.f2932b.add(upgqdbbsrl);
    }

    public final void onClick(View view) {
        for (OnClickListener onClick : this.f2932b) {
            onClick.onClick(view);
        }
    }

    public final void onFocusChange(View view, boolean z) {
        for (OnFocusChangeListener onFocusChange : this.f2932b) {
            onFocusChange.onFocusChange(view, z);
        }
    }
}
