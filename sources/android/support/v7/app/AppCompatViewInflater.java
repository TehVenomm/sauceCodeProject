package android.support.p003v7.app;

import android.content.Context;
import android.content.ContextWrapper;
import android.content.res.TypedArray;
import android.os.Build.VERSION;
import android.support.annotation.NonNull;
import android.support.annotation.Nullable;
import android.support.p000v4.util.ArrayMap;
import android.support.p000v4.view.ViewCompat;
import android.support.p003v7.widget.AppCompatAutoCompleteTextView;
import android.support.p003v7.widget.AppCompatButton;
import android.support.p003v7.widget.AppCompatCheckBox;
import android.support.p003v7.widget.AppCompatCheckedTextView;
import android.support.p003v7.widget.AppCompatEditText;
import android.support.p003v7.widget.AppCompatImageButton;
import android.support.p003v7.widget.AppCompatImageView;
import android.support.p003v7.widget.AppCompatMultiAutoCompleteTextView;
import android.support.p003v7.widget.AppCompatRadioButton;
import android.support.p003v7.widget.AppCompatRatingBar;
import android.support.p003v7.widget.AppCompatSeekBar;
import android.support.p003v7.widget.AppCompatSpinner;
import android.support.p003v7.widget.AppCompatTextView;
import android.support.p003v7.widget.TintContextWrapper;
import android.util.AttributeSet;
import android.view.InflateException;
import android.view.View;
import android.view.View.OnClickListener;
import java.lang.reflect.Constructor;
import java.lang.reflect.InvocationTargetException;
import java.lang.reflect.Method;
import java.util.Map;

/* renamed from: android.support.v7.app.AppCompatViewInflater */
class AppCompatViewInflater {
    private static final String LOG_TAG = "AppCompatViewInflater";
    private static final String[] sClassPrefixList = {"android.widget.", "android.view.", "android.webkit."};
    private static final Map<String, Constructor<? extends View>> sConstructorMap = new ArrayMap();
    private static final Class<?>[] sConstructorSignature = {Context.class, AttributeSet.class};
    private static final int[] sOnClickAttrs = {16843375};
    private final Object[] mConstructorArgs = new Object[2];

    /* renamed from: android.support.v7.app.AppCompatViewInflater$DeclaredOnClickListener */
    private static class DeclaredOnClickListener implements OnClickListener {
        private final View mHostView;
        private final String mMethodName;
        private Context mResolvedContext;
        private Method mResolvedMethod;

        public DeclaredOnClickListener(@NonNull View view, @NonNull String str) {
            this.mHostView = view;
            this.mMethodName = str;
        }

        @NonNull
        private void resolveMethod(@Nullable Context context, @NonNull String str) {
            for (Context context2 = context; context2 != null; context2 = context2 instanceof ContextWrapper ? ((ContextWrapper) context2).getBaseContext() : null) {
                try {
                    if (!context2.isRestricted()) {
                        Method method = context2.getClass().getMethod(this.mMethodName, new Class[]{View.class});
                        if (method != null) {
                            this.mResolvedMethod = method;
                            this.mResolvedContext = context2;
                            return;
                        }
                    }
                } catch (NoSuchMethodException e) {
                }
            }
            int id = this.mHostView.getId();
            throw new IllegalStateException("Could not find method " + this.mMethodName + "(View) in a parent or ancestor Context for android:onClick " + "attribute defined on view " + this.mHostView.getClass() + (id == -1 ? "" : " with id '" + this.mHostView.getContext().getResources().getResourceEntryName(id) + "'"));
        }

        public void onClick(@NonNull View view) {
            if (this.mResolvedMethod == null) {
                resolveMethod(this.mHostView.getContext(), this.mMethodName);
            }
            try {
                this.mResolvedMethod.invoke(this.mResolvedContext, new Object[]{view});
            } catch (IllegalAccessException e) {
                throw new IllegalStateException("Could not execute non-public method for android:onClick", e);
            } catch (InvocationTargetException e2) {
                throw new IllegalStateException("Could not execute method for android:onClick", e2);
            }
        }
    }

    AppCompatViewInflater() {
    }

    private void checkOnClickListener(View view, AttributeSet attributeSet) {
        Context context = view.getContext();
        if (!(context instanceof ContextWrapper)) {
            return;
        }
        if (VERSION.SDK_INT < 15 || ViewCompat.hasOnClickListeners(view)) {
            TypedArray obtainStyledAttributes = context.obtainStyledAttributes(attributeSet, sOnClickAttrs);
            String string = obtainStyledAttributes.getString(0);
            if (string != null) {
                view.setOnClickListener(new DeclaredOnClickListener(view, string));
            }
            obtainStyledAttributes.recycle();
        }
    }

    private View createView(Context context, String str, String str2) throws ClassNotFoundException, InflateException {
        Constructor constructor = (Constructor) sConstructorMap.get(str);
        if (constructor == null) {
            try {
                constructor = context.getClassLoader().loadClass(str2 != null ? str2 + str : str).asSubclass(View.class).getConstructor(sConstructorSignature);
                sConstructorMap.put(str, constructor);
            } catch (Exception e) {
                return null;
            }
        }
        constructor.setAccessible(true);
        return (View) constructor.newInstance(this.mConstructorArgs);
    }

    /* JADX INFO: finally extract failed */
    private View createViewFromTag(Context context, String str, AttributeSet attributeSet) {
        if (str.equals("view")) {
            str = attributeSet.getAttributeValue(null, "class");
        }
        try {
            this.mConstructorArgs[0] = context;
            this.mConstructorArgs[1] = attributeSet;
            if (-1 == str.indexOf(46)) {
                for (String createView : sClassPrefixList) {
                    View createView2 = createView(context, str, createView);
                    if (createView2 != null) {
                        this.mConstructorArgs[0] = null;
                        this.mConstructorArgs[1] = null;
                        return createView2;
                    }
                }
                this.mConstructorArgs[0] = null;
                this.mConstructorArgs[1] = null;
                return null;
            }
            View createView3 = createView(context, str, null);
            this.mConstructorArgs[0] = null;
            this.mConstructorArgs[1] = null;
            return createView3;
        } catch (Exception e) {
            this.mConstructorArgs[0] = null;
            this.mConstructorArgs[1] = null;
            return null;
        } catch (Throwable th) {
            this.mConstructorArgs[0] = null;
            this.mConstructorArgs[1] = null;
            throw th;
        }
    }

    /* JADX WARNING: Removed duplicated region for block: B:10:0x0028  */
    /* JADX WARNING: Removed duplicated region for block: B:18:? A[RETURN, SYNTHETIC] */
    /* Code decompiled incorrectly, please refer to instructions dump. */
    private static android.content.Context themifyContext(android.content.Context r4, android.util.AttributeSet r5, boolean r6, boolean r7) {
        /*
            r1 = 0
            int[] r0 = android.support.p003v7.appcompat.C0260R.styleable.View
            android.content.res.TypedArray r2 = r4.obtainStyledAttributes(r5, r0, r1, r1)
            if (r6 == 0) goto L_0x003e
            int r0 = android.support.p003v7.appcompat.C0260R.styleable.View_android_theme
            int r0 = r2.getResourceId(r0, r1)
        L_0x000f:
            if (r7 == 0) goto L_0x003c
            if (r0 != 0) goto L_0x003c
            int r0 = android.support.p003v7.appcompat.C0260R.styleable.View_theme
            int r0 = r2.getResourceId(r0, r1)
            if (r0 == 0) goto L_0x003c
            java.lang.String r1 = "AppCompatViewInflater"
            java.lang.String r3 = "app:theme is now deprecated. Please move to using android:theme instead."
            android.util.Log.i(r1, r3)
            r1 = r0
        L_0x0023:
            r2.recycle()
            if (r1 == 0) goto L_0x003b
            boolean r0 = r4 instanceof android.support.p003v7.view.ContextThemeWrapper
            if (r0 == 0) goto L_0x0035
            r0 = r4
            android.support.v7.view.ContextThemeWrapper r0 = (android.support.p003v7.view.ContextThemeWrapper) r0
            int r0 = r0.getThemeResId()
            if (r0 == r1) goto L_0x003b
        L_0x0035:
            android.support.v7.view.ContextThemeWrapper r0 = new android.support.v7.view.ContextThemeWrapper
            r0.<init>(r4, r1)
            r4 = r0
        L_0x003b:
            return r4
        L_0x003c:
            r1 = r0
            goto L_0x0023
        L_0x003e:
            r0 = r1
            goto L_0x000f
        */
        throw new UnsupportedOperationException("Method not decompiled: android.support.p003v7.app.AppCompatViewInflater.themifyContext(android.content.Context, android.util.AttributeSet, boolean, boolean):android.content.Context");
    }

    public final View createView(View view, String str, @NonNull Context context, @NonNull AttributeSet attributeSet, boolean z, boolean z2, boolean z3, boolean z4) {
        Context context2 = (!z || view == null) ? context : view.getContext();
        if (z2 || z3) {
            context2 = themifyContext(context2, attributeSet, z2, z3);
        }
        if (z4) {
            context2 = TintContextWrapper.wrap(context2);
        }
        View view2 = null;
        char c = 65535;
        switch (str.hashCode()) {
            case -1946472170:
                if (str.equals("RatingBar")) {
                    c = 11;
                    break;
                }
                break;
            case -1455429095:
                if (str.equals("CheckedTextView")) {
                    c = 8;
                    break;
                }
                break;
            case -1346021293:
                if (str.equals("MultiAutoCompleteTextView")) {
                    c = 10;
                    break;
                }
                break;
            case -938935918:
                if (str.equals("TextView")) {
                    c = 0;
                    break;
                }
                break;
            case -937446323:
                if (str.equals("ImageButton")) {
                    c = 5;
                    break;
                }
                break;
            case -658531749:
                if (str.equals("SeekBar")) {
                    c = 12;
                    break;
                }
                break;
            case -339785223:
                if (str.equals("Spinner")) {
                    c = 4;
                    break;
                }
                break;
            case 776382189:
                if (str.equals("RadioButton")) {
                    c = 7;
                    break;
                }
                break;
            case 1125864064:
                if (str.equals("ImageView")) {
                    c = 1;
                    break;
                }
                break;
            case 1413872058:
                if (str.equals("AutoCompleteTextView")) {
                    c = 9;
                    break;
                }
                break;
            case 1601505219:
                if (str.equals("CheckBox")) {
                    c = 6;
                    break;
                }
                break;
            case 1666676343:
                if (str.equals("EditText")) {
                    c = 3;
                    break;
                }
                break;
            case 2001146706:
                if (str.equals("Button")) {
                    c = 2;
                    break;
                }
                break;
        }
        switch (c) {
            case 0:
                view2 = new AppCompatTextView(context2, attributeSet);
                break;
            case 1:
                view2 = new AppCompatImageView(context2, attributeSet);
                break;
            case 2:
                view2 = new AppCompatButton(context2, attributeSet);
                break;
            case 3:
                view2 = new AppCompatEditText(context2, attributeSet);
                break;
            case 4:
                view2 = new AppCompatSpinner(context2, attributeSet);
                break;
            case 5:
                view2 = new AppCompatImageButton(context2, attributeSet);
                break;
            case 6:
                view2 = new AppCompatCheckBox(context2, attributeSet);
                break;
            case 7:
                view2 = new AppCompatRadioButton(context2, attributeSet);
                break;
            case 8:
                view2 = new AppCompatCheckedTextView(context2, attributeSet);
                break;
            case 9:
                view2 = new AppCompatAutoCompleteTextView(context2, attributeSet);
                break;
            case 10:
                view2 = new AppCompatMultiAutoCompleteTextView(context2, attributeSet);
                break;
            case 11:
                view2 = new AppCompatRatingBar(context2, attributeSet);
                break;
            case 12:
                view2 = new AppCompatSeekBar(context2, attributeSet);
                break;
        }
        View view3 = (view2 != null || context == context2) ? view2 : createViewFromTag(context2, str, attributeSet);
        if (view3 != null) {
            checkOnClickListener(view3, attributeSet);
        }
        return view3;
    }
}
