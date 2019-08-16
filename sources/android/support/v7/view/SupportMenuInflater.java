package android.support.p003v7.view;

import android.app.Activity;
import android.content.Context;
import android.content.ContextWrapper;
import android.content.res.ColorStateList;
import android.content.res.TypedArray;
import android.content.res.XmlResourceParser;
import android.graphics.PorterDuff.Mode;
import android.support.annotation.RestrictTo;
import android.support.annotation.RestrictTo.Scope;
import android.support.p000v4.internal.view.SupportMenu;
import android.support.p000v4.view.ActionProvider;
import android.support.p000v4.view.MenuItemCompat;
import android.support.p003v7.appcompat.C0260R;
import android.support.p003v7.view.menu.MenuItemImpl;
import android.support.p003v7.view.menu.MenuItemWrapperICS;
import android.support.p003v7.widget.DrawableUtils;
import android.util.AttributeSet;
import android.util.Log;
import android.util.Xml;
import android.view.InflateException;
import android.view.Menu;
import android.view.MenuInflater;
import android.view.MenuItem;
import android.view.MenuItem.OnMenuItemClickListener;
import android.view.SubMenu;
import android.view.View;
import java.io.IOException;
import java.lang.reflect.Constructor;
import java.lang.reflect.Method;
import org.xmlpull.v1.XmlPullParserException;

@RestrictTo({Scope.LIBRARY_GROUP})
/* renamed from: android.support.v7.view.SupportMenuInflater */
public class SupportMenuInflater extends MenuInflater {
    static final Class<?>[] ACTION_PROVIDER_CONSTRUCTOR_SIGNATURE = ACTION_VIEW_CONSTRUCTOR_SIGNATURE;
    static final Class<?>[] ACTION_VIEW_CONSTRUCTOR_SIGNATURE = {Context.class};
    static final String LOG_TAG = "SupportMenuInflater";
    static final int NO_ID = 0;
    private static final String XML_GROUP = "group";
    private static final String XML_ITEM = "item";
    private static final String XML_MENU = "menu";
    final Object[] mActionProviderConstructorArguments = this.mActionViewConstructorArguments;
    final Object[] mActionViewConstructorArguments;
    Context mContext;
    private Object mRealOwner;

    /* renamed from: android.support.v7.view.SupportMenuInflater$InflatedOnMenuItemClickListener */
    private static class InflatedOnMenuItemClickListener implements OnMenuItemClickListener {
        private static final Class<?>[] PARAM_TYPES = {MenuItem.class};
        private Method mMethod;
        private Object mRealOwner;

        public InflatedOnMenuItemClickListener(Object obj, String str) {
            this.mRealOwner = obj;
            Class cls = obj.getClass();
            try {
                this.mMethod = cls.getMethod(str, PARAM_TYPES);
            } catch (Exception e) {
                InflateException inflateException = new InflateException("Couldn't resolve menu item onClick handler " + str + " in class " + cls.getName());
                inflateException.initCause(e);
                throw inflateException;
            }
        }

        public boolean onMenuItemClick(MenuItem menuItem) {
            try {
                if (this.mMethod.getReturnType() == Boolean.TYPE) {
                    return ((Boolean) this.mMethod.invoke(this.mRealOwner, new Object[]{menuItem})).booleanValue();
                }
                this.mMethod.invoke(this.mRealOwner, new Object[]{menuItem});
                return true;
            } catch (Exception e) {
                throw new RuntimeException(e);
            }
        }
    }

    /* renamed from: android.support.v7.view.SupportMenuInflater$MenuState */
    private class MenuState {
        private static final int defaultGroupId = 0;
        private static final int defaultItemCategory = 0;
        private static final int defaultItemCheckable = 0;
        private static final boolean defaultItemChecked = false;
        private static final boolean defaultItemEnabled = true;
        private static final int defaultItemId = 0;
        private static final int defaultItemOrder = 0;
        private static final boolean defaultItemVisible = true;
        private int groupCategory;
        private int groupCheckable;
        private boolean groupEnabled;
        private int groupId;
        private int groupOrder;
        private boolean groupVisible;
        ActionProvider itemActionProvider;
        private String itemActionProviderClassName;
        private String itemActionViewClassName;
        private int itemActionViewLayout;
        private boolean itemAdded;
        private int itemAlphabeticModifiers;
        private char itemAlphabeticShortcut;
        private int itemCategoryOrder;
        private int itemCheckable;
        private boolean itemChecked;
        private CharSequence itemContentDescription;
        private boolean itemEnabled;
        private int itemIconResId;
        private ColorStateList itemIconTintList = null;
        private Mode itemIconTintMode = null;
        private int itemId;
        private String itemListenerMethodName;
        private int itemNumericModifiers;
        private char itemNumericShortcut;
        private int itemShowAsAction;
        private CharSequence itemTitle;
        private CharSequence itemTitleCondensed;
        private CharSequence itemTooltipText;
        private boolean itemVisible;
        private Menu menu;

        public MenuState(Menu menu2) {
            this.menu = menu2;
            resetGroup();
        }

        private char getShortcut(String str) {
            if (str == null) {
                return 0;
            }
            return str.charAt(0);
        }

        private <T> T newInstance(String str, Class<?>[] clsArr, Object[] objArr) {
            try {
                Constructor constructor = SupportMenuInflater.this.mContext.getClassLoader().loadClass(str).getConstructor(clsArr);
                constructor.setAccessible(true);
                return constructor.newInstance(objArr);
            } catch (Exception e) {
                Log.w(SupportMenuInflater.LOG_TAG, "Cannot instantiate class: " + str, e);
                return null;
            }
        }

        private void setItem(MenuItem menuItem) {
            boolean z = true;
            menuItem.setChecked(this.itemChecked).setVisible(this.itemVisible).setEnabled(this.itemEnabled).setCheckable(this.itemCheckable >= 1).setTitleCondensed(this.itemTitleCondensed).setIcon(this.itemIconResId);
            if (this.itemShowAsAction >= 0) {
                menuItem.setShowAsAction(this.itemShowAsAction);
            }
            if (this.itemListenerMethodName != null) {
                if (SupportMenuInflater.this.mContext.isRestricted()) {
                    throw new IllegalStateException("The android:onClick attribute cannot be used within a restricted context");
                }
                menuItem.setOnMenuItemClickListener(new InflatedOnMenuItemClickListener(SupportMenuInflater.this.getRealOwner(), this.itemListenerMethodName));
            }
            if (menuItem instanceof MenuItemImpl) {
                MenuItemImpl menuItemImpl = (MenuItemImpl) menuItem;
            }
            if (this.itemCheckable >= 2) {
                if (menuItem instanceof MenuItemImpl) {
                    ((MenuItemImpl) menuItem).setExclusiveCheckable(true);
                } else if (menuItem instanceof MenuItemWrapperICS) {
                    ((MenuItemWrapperICS) menuItem).setExclusiveCheckable(true);
                }
            }
            if (this.itemActionViewClassName != null) {
                menuItem.setActionView((View) newInstance(this.itemActionViewClassName, SupportMenuInflater.ACTION_VIEW_CONSTRUCTOR_SIGNATURE, SupportMenuInflater.this.mActionViewConstructorArguments));
            } else {
                z = false;
            }
            if (this.itemActionViewLayout > 0) {
                if (!z) {
                    menuItem.setActionView(this.itemActionViewLayout);
                } else {
                    Log.w(SupportMenuInflater.LOG_TAG, "Ignoring attribute 'itemActionViewLayout'. Action view already specified.");
                }
            }
            if (this.itemActionProvider != null) {
                MenuItemCompat.setActionProvider(menuItem, this.itemActionProvider);
            }
            MenuItemCompat.setContentDescription(menuItem, this.itemContentDescription);
            MenuItemCompat.setTooltipText(menuItem, this.itemTooltipText);
            MenuItemCompat.setAlphabeticShortcut(menuItem, this.itemAlphabeticShortcut, this.itemAlphabeticModifiers);
            MenuItemCompat.setNumericShortcut(menuItem, this.itemNumericShortcut, this.itemNumericModifiers);
            if (this.itemIconTintMode != null) {
                MenuItemCompat.setIconTintMode(menuItem, this.itemIconTintMode);
            }
            if (this.itemIconTintList != null) {
                MenuItemCompat.setIconTintList(menuItem, this.itemIconTintList);
            }
        }

        public void addItem() {
            this.itemAdded = true;
            setItem(this.menu.add(this.groupId, this.itemId, this.itemCategoryOrder, this.itemTitle));
        }

        public SubMenu addSubMenuItem() {
            this.itemAdded = true;
            SubMenu addSubMenu = this.menu.addSubMenu(this.groupId, this.itemId, this.itemCategoryOrder, this.itemTitle);
            setItem(addSubMenu.getItem());
            return addSubMenu;
        }

        public boolean hasAddedItem() {
            return this.itemAdded;
        }

        public void readGroup(AttributeSet attributeSet) {
            TypedArray obtainStyledAttributes = SupportMenuInflater.this.mContext.obtainStyledAttributes(attributeSet, C0260R.styleable.MenuGroup);
            this.groupId = obtainStyledAttributes.getResourceId(C0260R.styleable.MenuGroup_android_id, 0);
            this.groupCategory = obtainStyledAttributes.getInt(C0260R.styleable.MenuGroup_android_menuCategory, 0);
            this.groupOrder = obtainStyledAttributes.getInt(C0260R.styleable.MenuGroup_android_orderInCategory, 0);
            this.groupCheckable = obtainStyledAttributes.getInt(C0260R.styleable.MenuGroup_android_checkableBehavior, 0);
            this.groupVisible = obtainStyledAttributes.getBoolean(C0260R.styleable.MenuGroup_android_visible, true);
            this.groupEnabled = obtainStyledAttributes.getBoolean(C0260R.styleable.MenuGroup_android_enabled, true);
            obtainStyledAttributes.recycle();
        }

        public void readItem(AttributeSet attributeSet) {
            boolean z = true;
            TypedArray obtainStyledAttributes = SupportMenuInflater.this.mContext.obtainStyledAttributes(attributeSet, C0260R.styleable.MenuItem);
            this.itemId = obtainStyledAttributes.getResourceId(C0260R.styleable.MenuItem_android_id, 0);
            this.itemCategoryOrder = (-65536 & obtainStyledAttributes.getInt(C0260R.styleable.MenuItem_android_menuCategory, this.groupCategory)) | (65535 & obtainStyledAttributes.getInt(C0260R.styleable.MenuItem_android_orderInCategory, this.groupOrder));
            this.itemTitle = obtainStyledAttributes.getText(C0260R.styleable.MenuItem_android_title);
            this.itemTitleCondensed = obtainStyledAttributes.getText(C0260R.styleable.MenuItem_android_titleCondensed);
            this.itemIconResId = obtainStyledAttributes.getResourceId(C0260R.styleable.MenuItem_android_icon, 0);
            this.itemAlphabeticShortcut = getShortcut(obtainStyledAttributes.getString(C0260R.styleable.MenuItem_android_alphabeticShortcut));
            this.itemAlphabeticModifiers = obtainStyledAttributes.getInt(C0260R.styleable.MenuItem_alphabeticModifiers, 4096);
            this.itemNumericShortcut = getShortcut(obtainStyledAttributes.getString(C0260R.styleable.MenuItem_android_numericShortcut));
            this.itemNumericModifiers = obtainStyledAttributes.getInt(C0260R.styleable.MenuItem_numericModifiers, 4096);
            if (obtainStyledAttributes.hasValue(C0260R.styleable.MenuItem_android_checkable)) {
                this.itemCheckable = obtainStyledAttributes.getBoolean(C0260R.styleable.MenuItem_android_checkable, false) ? 1 : 0;
            } else {
                this.itemCheckable = this.groupCheckable;
            }
            this.itemChecked = obtainStyledAttributes.getBoolean(C0260R.styleable.MenuItem_android_checked, false);
            this.itemVisible = obtainStyledAttributes.getBoolean(C0260R.styleable.MenuItem_android_visible, this.groupVisible);
            this.itemEnabled = obtainStyledAttributes.getBoolean(C0260R.styleable.MenuItem_android_enabled, this.groupEnabled);
            this.itemShowAsAction = obtainStyledAttributes.getInt(C0260R.styleable.MenuItem_showAsAction, -1);
            this.itemListenerMethodName = obtainStyledAttributes.getString(C0260R.styleable.MenuItem_android_onClick);
            this.itemActionViewLayout = obtainStyledAttributes.getResourceId(C0260R.styleable.MenuItem_actionLayout, 0);
            this.itemActionViewClassName = obtainStyledAttributes.getString(C0260R.styleable.MenuItem_actionViewClass);
            this.itemActionProviderClassName = obtainStyledAttributes.getString(C0260R.styleable.MenuItem_actionProviderClass);
            if (this.itemActionProviderClassName == null) {
                z = false;
            }
            if (z && this.itemActionViewLayout == 0 && this.itemActionViewClassName == null) {
                this.itemActionProvider = (ActionProvider) newInstance(this.itemActionProviderClassName, SupportMenuInflater.ACTION_PROVIDER_CONSTRUCTOR_SIGNATURE, SupportMenuInflater.this.mActionProviderConstructorArguments);
            } else {
                if (z) {
                    Log.w(SupportMenuInflater.LOG_TAG, "Ignoring attribute 'actionProviderClass'. Action view already specified.");
                }
                this.itemActionProvider = null;
            }
            this.itemContentDescription = obtainStyledAttributes.getText(C0260R.styleable.MenuItem_contentDescription);
            this.itemTooltipText = obtainStyledAttributes.getText(C0260R.styleable.MenuItem_tooltipText);
            if (obtainStyledAttributes.hasValue(C0260R.styleable.MenuItem_iconTintMode)) {
                this.itemIconTintMode = DrawableUtils.parseTintMode(obtainStyledAttributes.getInt(C0260R.styleable.MenuItem_iconTintMode, -1), this.itemIconTintMode);
            } else {
                this.itemIconTintMode = null;
            }
            if (obtainStyledAttributes.hasValue(C0260R.styleable.MenuItem_iconTint)) {
                this.itemIconTintList = obtainStyledAttributes.getColorStateList(C0260R.styleable.MenuItem_iconTint);
            } else {
                this.itemIconTintList = null;
            }
            obtainStyledAttributes.recycle();
            this.itemAdded = false;
        }

        public void resetGroup() {
            this.groupId = 0;
            this.groupCategory = 0;
            this.groupOrder = 0;
            this.groupCheckable = 0;
            this.groupVisible = true;
            this.groupEnabled = true;
        }
    }

    public SupportMenuInflater(Context context) {
        super(context);
        this.mContext = context;
        this.mActionViewConstructorArguments = new Object[]{context};
    }

    private Object findRealOwner(Object obj) {
        return (!(obj instanceof Activity) && (obj instanceof ContextWrapper)) ? findRealOwner(((ContextWrapper) obj).getBaseContext()) : obj;
    }

    /* JADX WARNING: Code restructure failed: missing block: B:50:0x00db, code lost:
        r0 = r1;
     */
    /* Code decompiled incorrectly, please refer to instructions dump. */
    private void parseMenu(org.xmlpull.v1.XmlPullParser r10, android.util.AttributeSet r11, android.view.Menu r12) throws org.xmlpull.v1.XmlPullParserException, java.io.IOException {
        /*
            r9 = this;
            r7 = 0
            r2 = 1
            r4 = 0
            android.support.v7.view.SupportMenuInflater$MenuState r8 = new android.support.v7.view.SupportMenuInflater$MenuState
            r8.<init>(r12)
            int r0 = r10.getEventType()
        L_0x000c:
            r1 = 2
            if (r0 != r1) goto L_0x0048
            java.lang.String r0 = r10.getName()
            java.lang.String r1 = "menu"
            boolean r1 = r0.equals(r1)
            if (r1 == 0) goto L_0x002f
            int r0 = r10.next()
        L_0x001f:
            r5 = r7
            r6 = r0
            r3 = r4
            r1 = r4
        L_0x0023:
            if (r1 != 0) goto L_0x00da
            switch(r6) {
                case 1: goto L_0x00d2;
                case 2: goto L_0x004f;
                case 3: goto L_0x0084;
                default: goto L_0x0028;
            }
        L_0x0028:
            r0 = r1
        L_0x0029:
            int r6 = r10.next()
            r1 = r0
            goto L_0x0023
        L_0x002f:
            java.lang.RuntimeException r1 = new java.lang.RuntimeException
            java.lang.StringBuilder r2 = new java.lang.StringBuilder
            r2.<init>()
            java.lang.String r3 = "Expecting menu, got "
            java.lang.StringBuilder r2 = r2.append(r3)
            java.lang.StringBuilder r0 = r2.append(r0)
            java.lang.String r0 = r0.toString()
            r1.<init>(r0)
            throw r1
        L_0x0048:
            int r0 = r10.next()
            if (r0 != r2) goto L_0x000c
            goto L_0x001f
        L_0x004f:
            if (r3 != 0) goto L_0x00db
            java.lang.String r6 = r10.getName()
            java.lang.String r0 = "group"
            boolean r0 = r6.equals(r0)
            if (r0 == 0) goto L_0x0062
            r8.readGroup(r11)
            r0 = r1
            goto L_0x0029
        L_0x0062:
            java.lang.String r0 = "item"
            boolean r0 = r6.equals(r0)
            if (r0 == 0) goto L_0x006f
            r8.readItem(r11)
            r0 = r1
            goto L_0x0029
        L_0x006f:
            java.lang.String r0 = "menu"
            boolean r0 = r6.equals(r0)
            if (r0 == 0) goto L_0x0080
            android.view.SubMenu r0 = r8.addSubMenuItem()
            r9.parseMenu(r10, r11, r0)
            r0 = r1
            goto L_0x0029
        L_0x0080:
            r0 = r1
            r3 = r2
            r5 = r6
            goto L_0x0029
        L_0x0084:
            java.lang.String r0 = r10.getName()
            if (r3 == 0) goto L_0x0094
            boolean r6 = r0.equals(r5)
            if (r6 == 0) goto L_0x0094
            r0 = r1
            r3 = r4
            r5 = r7
            goto L_0x0029
        L_0x0094:
            java.lang.String r6 = "group"
            boolean r6 = r0.equals(r6)
            if (r6 == 0) goto L_0x00a1
            r8.resetGroup()
            r0 = r1
            goto L_0x0029
        L_0x00a1:
            java.lang.String r6 = "item"
            boolean r6 = r0.equals(r6)
            if (r6 == 0) goto L_0x00c7
            boolean r0 = r8.hasAddedItem()
            if (r0 != 0) goto L_0x00db
            android.support.v4.view.ActionProvider r0 = r8.itemActionProvider
            if (r0 == 0) goto L_0x00c1
            android.support.v4.view.ActionProvider r0 = r8.itemActionProvider
            boolean r0 = r0.hasSubMenu()
            if (r0 == 0) goto L_0x00c1
            r8.addSubMenuItem()
            r0 = r1
            goto L_0x0029
        L_0x00c1:
            r8.addItem()
            r0 = r1
            goto L_0x0029
        L_0x00c7:
            java.lang.String r6 = "menu"
            boolean r0 = r0.equals(r6)
            if (r0 == 0) goto L_0x00db
            r0 = r2
            goto L_0x0029
        L_0x00d2:
            java.lang.RuntimeException r0 = new java.lang.RuntimeException
            java.lang.String r1 = "Unexpected end of document"
            r0.<init>(r1)
            throw r0
        L_0x00da:
            return
        L_0x00db:
            r0 = r1
            goto L_0x0029
        */
        throw new UnsupportedOperationException("Method not decompiled: android.support.p003v7.view.SupportMenuInflater.parseMenu(org.xmlpull.v1.XmlPullParser, android.util.AttributeSet, android.view.Menu):void");
    }

    /* access modifiers changed from: 0000 */
    public Object getRealOwner() {
        if (this.mRealOwner == null) {
            this.mRealOwner = findRealOwner(this.mContext);
        }
        return this.mRealOwner;
    }

    public void inflate(int i, Menu menu) {
        XmlResourceParser xmlResourceParser = null;
        if (!(menu instanceof SupportMenu)) {
            super.inflate(i, menu);
            return;
        }
        try {
            xmlResourceParser = this.mContext.getResources().getLayout(i);
            parseMenu(xmlResourceParser, Xml.asAttributeSet(xmlResourceParser), menu);
            if (xmlResourceParser != null) {
                xmlResourceParser.close();
            }
        } catch (XmlPullParserException e) {
            throw new InflateException("Error inflating menu XML", e);
        } catch (IOException e2) {
            throw new InflateException("Error inflating menu XML", e2);
        } catch (Throwable th) {
            if (xmlResourceParser != null) {
                xmlResourceParser.close();
            }
            throw th;
        }
    }
}
