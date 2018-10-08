package im.getsocial.sdk.ui.internal.views;

import android.annotation.TargetApi;
import android.content.Context;
import android.text.Editable;
import android.text.TextUtils;
import android.text.TextWatcher;
import android.util.AttributeSet;
import android.view.KeyEvent;
import android.view.View;
import android.view.View.OnClickListener;
import android.widget.EditText;
import android.widget.RelativeLayout;
import android.widget.TextView;
import android.widget.TextView.OnEditorActionListener;
import im.getsocial.sdk.internal.p033c.p066m.cjrhisSQCL;
import im.getsocial.sdk.ui.C1067R;
import im.getsocial.sdk.ui.internal.p125h.KluUZYuxme;
import im.getsocial.sdk.ui.internal.p126e.jjbQypPegg;
import im.getsocial.sdk.ui.internal.p126e.pdwpUtZXDT;

public class InputContainer extends RelativeLayout implements TextWatcher, OnEditorActionListener {
    /* renamed from: a */
    private EditText f3134a;
    /* renamed from: b */
    private AssetButton f3135b;
    /* renamed from: c */
    private Listener f3136c = ((Listener) cjrhisSQCL.m1509a(Listener.class));
    /* renamed from: d */
    private jjbQypPegg f3137d;

    public interface Listener {
        void b_();
    }

    /* renamed from: im.getsocial.sdk.ui.internal.views.InputContainer$1 */
    class C12011 implements OnClickListener {
        /* renamed from: a */
        final /* synthetic */ InputContainer f3133a;

        C12011(InputContainer inputContainer) {
            this.f3133a = inputContainer;
        }

        public void onClick(View view) {
            this.f3133a.m3508e();
        }
    }

    public InputContainer(Context context) {
        super(context);
        m3507d();
    }

    public InputContainer(Context context, AttributeSet attributeSet) {
        super(context, attributeSet);
        m3507d();
    }

    public InputContainer(Context context, AttributeSet attributeSet, int i) {
        super(context, attributeSet, i);
        m3507d();
    }

    @TargetApi(21)
    public InputContainer(Context context, AttributeSet attributeSet, int i, int i2) {
        super(context, attributeSet, i, i2);
        m3507d();
    }

    /* renamed from: d */
    private void m3507d() {
        View inflate = inflate(getContext(), C1067R.layout.post_input_button_container, this);
        this.f3134a = (EditText) findViewById(C1067R.id.edit_text_post_comment);
        this.f3134a.setContentDescription("post_inputfield");
        this.f3135b = (AssetButton) findViewById(C1067R.id.button_post_comment);
        KluUZYuxme a = KluUZYuxme.m3299a(getContext());
        a.m3323b(this.f3135b);
        a.m3326d(this.f3134a);
        a.m3305a(inflate);
        this.f3137d = new jjbQypPegg(this.f3134a);
        this.f3134a.addTextChangedListener(this);
        this.f3134a.setOnEditorActionListener(this);
        this.f3135b.setEnabled(false);
        this.f3135b.setOnClickListener(new C12011(this));
        this.f3135b.setContentDescription("post_button");
    }

    /* renamed from: e */
    private void m3508e() {
        if (this.f3135b.isEnabled()) {
            Listener listener = this.f3136c;
            this.f3134a.getText().toString();
            listener.b_();
        }
    }

    /* renamed from: a */
    public final void m3509a() {
        this.f3134a.setText("");
    }

    /* renamed from: a */
    public final void m3510a(Listener listener) {
        this.f3136c = (Listener) cjrhisSQCL.m1510a(Listener.class, listener);
    }

    /* renamed from: a */
    public final void m3511a(String str) {
        this.f3134a.setHint(str);
    }

    /* renamed from: a */
    public final void m3512a(boolean z) {
        this.f3135b.setEnabled(z);
    }

    public void afterTextChanged(Editable editable) {
        this.f3135b.setEnabled(!TextUtils.isEmpty(this.f3134a.getText().toString().trim()));
    }

    /* renamed from: b */
    public final pdwpUtZXDT<im.getsocial.sdk.ui.activities.p116a.p121d.jjbQypPegg> m3513b() {
        return new pdwpUtZXDT(this.f3134a, this.f3137d, "@");
    }

    public void beforeTextChanged(CharSequence charSequence, int i, int i2, int i3) {
    }

    /* renamed from: c */
    public final pdwpUtZXDT<im.getsocial.sdk.ui.activities.p116a.p122h.jjbQypPegg> m3514c() {
        return new pdwpUtZXDT(this.f3134a, this.f3137d, "#");
    }

    public boolean onEditorAction(TextView textView, int i, KeyEvent keyEvent) {
        if (i != 4) {
            return false;
        }
        m3508e();
        return true;
    }

    public void onTextChanged(CharSequence charSequence, int i, int i2, int i3) {
    }
}
