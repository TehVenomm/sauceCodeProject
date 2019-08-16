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
import net.gogame.gowrap.InternalConstants;

/* renamed from: com.unity3d.player.i */
public final class C1108i extends Dialog implements TextWatcher, OnClickListener {

    /* renamed from: c */
    private static int f584c = 1627389952;

    /* renamed from: d */
    private static int f585d = -1;

    /* renamed from: e */
    private static int f586e = 134217728;

    /* renamed from: f */
    private static int f587f = InternalConstants.DISKLRUCACHE_MAXSIZE;
    /* access modifiers changed from: private */

    /* renamed from: a */
    public Context f588a = null;

    /* renamed from: b */
    private UnityPlayer f589b = null;

    public C1108i(Context context, UnityPlayer unityPlayer, String str, int i, boolean z, boolean z2, boolean z3, String str2) {
        super(context);
        this.f588a = context;
        this.f589b = unityPlayer;
        getWindow().setGravity(80);
        getWindow().requestFeature(1);
        getWindow().setBackgroundDrawable(new ColorDrawable(0));
        setContentView(createSoftInputView());
        getWindow().setLayout(-1, -2);
        getWindow().clearFlags(2);
        if (C1107h.f580a) {
            getWindow().clearFlags(f586e);
            getWindow().clearFlags(f587f);
        }
        EditText editText = (EditText) findViewById(1057292289);
        Button button = (Button) findViewById(1057292290);
        m541a(editText, str, i, z, z2, z3, str2);
        button.setOnClickListener(this);
        editText.setOnFocusChangeListener(new OnFocusChangeListener() {
            public final void onFocusChange(View view, boolean z) {
                if (z) {
                    C1108i.this.getWindow().setSoftInputMode(5);
                }
            }
        });
    }

    /* renamed from: a */
    private static int m538a(int i, boolean z, boolean z2, boolean z3) {
        int i2 = 0;
        int i3 = z ? 32768 : 0;
        int i4 = z2 ? 131072 : 0;
        if (z3) {
            i2 = 128;
        }
        int i5 = i2 | i4 | i3;
        return (i < 0 || i > 7) ? i5 : i5 | new int[]{1, 16385, 12290, 17, 2, 3, 8289, 33}[i];
    }

    /* access modifiers changed from: private */
    /* renamed from: a */
    public String m539a() {
        EditText editText = (EditText) findViewById(1057292289);
        if (editText == null) {
            return null;
        }
        return editText.getText().toString().trim();
    }

    /* renamed from: a */
    private void m541a(EditText editText, String str, int i, boolean z, boolean z2, boolean z3, String str2) {
        editText.setImeOptions(6);
        editText.setText(str);
        editText.setHint(str2);
        editText.setHintTextColor(f584c);
        editText.setInputType(m538a(i, z, z2, z3));
        editText.setImeOptions(33554432);
        editText.addTextChangedListener(this);
        editText.setClickable(true);
        if (!z2) {
            editText.selectAll();
        }
    }

    /* access modifiers changed from: private */
    /* renamed from: a */
    public void m543a(String str, boolean z) {
        Selection.removeSelection(((EditText) findViewById(1057292289)).getEditableText());
        this.f589b.reportSoftInputStr(str, 1, z);
    }

    /* renamed from: a */
    public final void mo20515a(String str) {
        EditText editText = (EditText) findViewById(1057292289);
        if (editText != null) {
            editText.setText(str);
            editText.setSelection(str.length());
        }
    }

    public final void afterTextChanged(Editable editable) {
        this.f589b.reportSoftInputStr(editable.toString(), 0, false);
    }

    public final void beforeTextChanged(CharSequence charSequence, int i, int i2, int i3) {
    }

    /* access modifiers changed from: protected */
    public final View createSoftInputView() {
        RelativeLayout relativeLayout = new RelativeLayout(this.f588a);
        relativeLayout.setLayoutParams(new LayoutParams(-1, -1));
        relativeLayout.setBackgroundColor(f585d);
        C11102 r0 = new EditText(this.f588a) {
            public final boolean onKeyPreIme(int i, KeyEvent keyEvent) {
                if (i == 4) {
                    C1108i.this.m543a(C1108i.this.m539a(), true);
                    return true;
                } else if (i != 84) {
                    return super.onKeyPreIme(i, keyEvent);
                } else {
                    return true;
                }
            }

            public final void onWindowFocusChanged(boolean z) {
                super.onWindowFocusChanged(z);
                if (z) {
                    ((InputMethodManager) C1108i.this.f588a.getSystemService("input_method")).showSoftInput(this, 0);
                }
            }
        };
        RelativeLayout.LayoutParams layoutParams = new RelativeLayout.LayoutParams(-1, -2);
        layoutParams.addRule(15);
        layoutParams.addRule(0, 1057292290);
        r0.setLayoutParams(layoutParams);
        r0.setId(1057292289);
        relativeLayout.addView(r0);
        Button button = new Button(this.f588a);
        button.setText(this.f588a.getResources().getIdentifier("ok", "string", "android"));
        RelativeLayout.LayoutParams layoutParams2 = new RelativeLayout.LayoutParams(-2, -2);
        layoutParams2.addRule(15);
        layoutParams2.addRule(11);
        button.setLayoutParams(layoutParams2);
        button.setId(1057292290);
        button.setBackgroundColor(0);
        relativeLayout.addView(button);
        ((EditText) relativeLayout.findViewById(1057292289)).setOnEditorActionListener(new OnEditorActionListener() {
            public final boolean onEditorAction(TextView textView, int i, KeyEvent keyEvent) {
                if (i == 6) {
                    C1108i.this.m543a(C1108i.this.m539a(), false);
                }
                return false;
            }
        });
        relativeLayout.setPadding(16, 16, 16, 16);
        return relativeLayout;
    }

    public final void onBackPressed() {
        m543a(m539a(), true);
    }

    public final void onClick(View view) {
        m543a(m539a(), false);
    }

    public final void onTextChanged(CharSequence charSequence, int i, int i2, int i3) {
    }
}
