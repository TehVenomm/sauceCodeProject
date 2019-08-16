package net.gogame.gowrap.p019ui.utils;

import android.content.Context;
import android.content.res.TypedArray;
import android.text.Editable;
import android.text.TextWatcher;
import android.widget.EditText;

/* renamed from: net.gogame.gowrap.ui.utils.UIUtils */
public final class UIUtils {
    private UIUtils() {
    }

    public static void setupRightDrawable(Context context, final EditText editText, int i) {
        TypedArray obtainTypedArray = context.getResources().obtainTypedArray(i);
        final int resourceId = obtainTypedArray.getResourceId(0, 0);
        final int resourceId2 = obtainTypedArray.getResourceId(1, 0);
        obtainTypedArray.recycle();
        setupRightDrawable(editText, resourceId, resourceId2, editText.getText());
        editText.addTextChangedListener(new TextWatcher() {
            public void beforeTextChanged(CharSequence charSequence, int i, int i2, int i3) {
            }

            public void onTextChanged(CharSequence charSequence, int i, int i2, int i3) {
                UIUtils.setupRightDrawable(editText, resourceId, resourceId2, charSequence);
            }

            public void afterTextChanged(Editable editable) {
            }
        });
    }

    /* access modifiers changed from: private */
    public static void setupRightDrawable(EditText editText, int i, int i2, CharSequence charSequence) {
        if (charSequence == null || charSequence.length() <= 0) {
            editText.setCompoundDrawablesWithIntrinsicBounds(0, 0, i, 0);
        } else {
            editText.setCompoundDrawablesWithIntrinsicBounds(0, 0, i2, 0);
        }
    }
}
