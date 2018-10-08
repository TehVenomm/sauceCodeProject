package com.unity3d.player;

import android.app.Dialog;
import android.content.Context;
import android.graphics.drawable.ColorDrawable;
import android.text.Editable;
import android.text.Selection;
import android.text.TextWatcher;
import android.view.KeyEvent;
import android.view.View;
import android.view.View.OnClickListener;
import android.view.View.OnFocusChangeListener;
import android.view.ViewGroup.LayoutParams;
import android.view.inputmethod.InputMethodManager;
import android.widget.Button;
import android.widget.EditText;
import android.widget.RelativeLayout;
import android.widget.TextView;
import android.widget.TextView.OnEditorActionListener;
import io.fabric.sdk.android.services.common.AbstractSpiCall;

/* renamed from: com.unity3d.player.s */
public final class C0778s extends Dialog implements TextWatcher, OnClickListener {
    /* renamed from: c */
    private static int f559c = -570425344;
    /* renamed from: d */
    private static int f560d = 1627389952;
    /* renamed from: e */
    private static int f561e = -1;
    /* renamed from: a */
    private Context f562a = null;
    /* renamed from: b */
    private UnityPlayer f563b = null;

    /* renamed from: com.unity3d.player.s$1 */
    final class C07751 implements OnFocusChangeListener {
        /* renamed from: a */
        final /* synthetic */ C0778s f556a;

        C07751(C0778s c0778s) {
            this.f556a = c0778s;
        }

        public final void onFocusChange(View view, boolean z) {
            if (z) {
                this.f556a.getWindow().setSoftInputMode(5);
            }
        }
    }

    /* renamed from: com.unity3d.player.s$3 */
    final class C07773 implements OnEditorActionListener {
        /* renamed from: a */
        final /* synthetic */ C0778s f558a;

        C07773(C0778s c0778s) {
            this.f558a = c0778s;
        }

        public final boolean onEditorAction(TextView textView, int i, KeyEvent keyEvent) {
            if (i == 6) {
                this.f558a.m532a(this.f558a.m528a(), false);
            }
            return false;
        }
    }

    public C0778s(Context context, UnityPlayer unityPlayer, String str, int i, boolean z, boolean z2, boolean z3, String str2) {
        super(context);
        this.f562a = context;
        this.f563b = unityPlayer;
        getWindow().setGravity(80);
        getWindow().requestFeature(1);
        getWindow().setBackgroundDrawable(new ColorDrawable(0));
        setContentView(createSoftInputView());
        getWindow().setLayout(-1, -2);
        getWindow().clearFlags(2);
        EditText editText = (EditText) findViewById(1057292289);
        Button button = (Button) findViewById(1057292290);
        m530a(editText, str, i, z, z2, z3, str2);
        button.setOnClickListener(this);
        editText.setOnFocusChangeListener(new C07751(this));
    }

    /* renamed from: a */
    private static int m527a(int i, boolean z, boolean z2, boolean z3) {
        int i2 = 0;
        int i3 = z ? 32768 : 0;
        int i4 = z2 ? 131072 : 0;
        if (z3) {
            i2 = 128;
        }
        i2 |= i4 | i3;
        return (i < 0 || i > 7) ? i2 : i2 | new int[]{1, 16385, 12290, 17, 2, 3, 97, 33}[i];
    }

    /* renamed from: a */
    private String m528a() {
        EditText editText = (EditText) findViewById(1057292289);
        return editText == null ? null : editText.getText().toString().trim();
    }

    /* renamed from: a */
    private void m530a(EditText editText, String str, int i, boolean z, boolean z2, boolean z3, String str2) {
        editText.setImeOptions(6);
        editText.setText(str);
        editText.setHint(str2);
        editText.setHintTextColor(f560d);
        editText.setInputType(C0778s.m527a(i, z, z2, z3));
        editText.addTextChangedListener(this);
        editText.setClickable(true);
        if (!z2) {
            editText.selectAll();
        }
    }

    /* renamed from: a */
    private void m532a(String str, boolean z) {
        Selection.removeSelection(((EditText) findViewById(1057292289)).getEditableText());
        this.f563b.reportSoftInputStr(str, 1, z);
    }

    /* renamed from: a */
    public final void m534a(String str) {
        EditText editText = (EditText) findViewById(1057292289);
        if (editText != null) {
            editText.setText(str);
            editText.setSelection(str.length());
        }
    }

    public final void afterTextChanged(Editable editable) {
        this.f563b.reportSoftInputStr(editable.toString(), 0, false);
    }

    public final void beforeTextChanged(CharSequence charSequence, int i, int i2, int i3) {
    }

    protected final View createSoftInputView() {
        View relativeLayout = new RelativeLayout(this.f562a);
        relativeLayout.setLayoutParams(new LayoutParams(-1, -1));
        relativeLayout.setBackgroundColor(f561e);
        View c07762 = new EditText(this, this.f562a) {
            /* renamed from: a */
            final /* synthetic */ C0778s f557a;

            public final boolean onKeyPreIme(int i, KeyEvent keyEvent) {
                if (i != 4) {
                    return i != 84 ? super.onKeyPreIme(i, keyEvent) : true;
                } else {
                    this.f557a.m532a(this.f557a.m528a(), true);
                    return true;
                }
            }

            public final void onWindowFocusChanged(boolean z) {
                super.onWindowFocusChanged(z);
                if (z) {
                    ((InputMethodManager) this.f557a.f562a.getSystemService("input_method")).showSoftInput(this, 0);
                }
            }
        };
        LayoutParams layoutParams = new RelativeLayout.LayoutParams(-1, -2);
        layoutParams.addRule(15);
        layoutParams.addRule(0, 1057292290);
        c07762.setLayoutParams(layoutParams);
        c07762.setTextColor(f559c);
        c07762.setId(1057292289);
        relativeLayout.addView(c07762);
        c07762 = new Button(this.f562a);
        c07762.setText(this.f562a.getResources().getIdentifier("ok", "string", AbstractSpiCall.ANDROID_CLIENT_TYPE));
        layoutParams = new RelativeLayout.LayoutParams(-2, -2);
        layoutParams.addRule(15);
        layoutParams.addRule(11);
        c07762.setLayoutParams(layoutParams);
        c07762.setId(1057292290);
        c07762.setBackgroundColor(0);
        c07762.setTextColor(f559c);
        relativeLayout.addView(c07762);
        ((EditText) relativeLayout.findViewById(1057292289)).setOnEditorActionListener(new C07773(this));
        relativeLayout.setPadding(16, 16, 16, 16);
        return relativeLayout;
    }

    public final void onBackPressed() {
        m532a(m528a(), true);
    }

    public final void onClick(View view) {
        m532a(m528a(), false);
    }

    public final void onTextChanged(CharSequence charSequence, int i, int i2, int i3) {
    }
}
