package android.support.p003v7.view.menu;

import android.content.ComponentName;
import android.content.Context;
import android.content.Intent;
import android.content.pm.PackageManager;
import android.content.pm.ResolveInfo;
import android.content.res.Resources;
import android.graphics.drawable.Drawable;
import android.os.Bundle;
import android.os.Parcelable;
import android.support.annotation.NonNull;
import android.support.annotation.RestrictTo;
import android.support.annotation.RestrictTo.Scope;
import android.support.p000v4.content.ContextCompat;
import android.support.p000v4.internal.view.SupportMenu;
import android.support.p000v4.view.ActionProvider;
import android.support.p003v7.appcompat.C0260R;
import android.util.SparseArray;
import android.view.ContextMenu.ContextMenuInfo;
import android.view.KeyCharacterMap.KeyData;
import android.view.KeyEvent;
import android.view.MenuItem;
import android.view.SubMenu;
import android.view.View;
import java.lang.ref.WeakReference;
import java.util.ArrayList;
import java.util.Iterator;
import java.util.List;
import java.util.concurrent.CopyOnWriteArrayList;

@RestrictTo({Scope.LIBRARY_GROUP})
/* renamed from: android.support.v7.view.menu.MenuBuilder */
public class MenuBuilder implements SupportMenu {
    private static final String ACTION_VIEW_STATES_KEY = "android:menu:actionviewstates";
    private static final String EXPANDED_ACTION_VIEW_ID = "android:menu:expandedactionview";
    private static final String PRESENTER_KEY = "android:menu:presenters";
    private static final String TAG = "MenuBuilder";
    private static final int[] sCategoryToOrder = {1, 4, 5, 3, 2, 0};
    private ArrayList<MenuItemImpl> mActionItems;
    private Callback mCallback;
    private final Context mContext;
    private ContextMenuInfo mCurrentMenuInfo;
    private int mDefaultShowAsAction = 0;
    private MenuItemImpl mExpandedItem;
    private SparseArray<Parcelable> mFrozenViewStates;
    Drawable mHeaderIcon;
    CharSequence mHeaderTitle;
    View mHeaderView;
    private boolean mIsActionItemsStale;
    private boolean mIsClosing = false;
    private boolean mIsVisibleItemsStale;
    private ArrayList<MenuItemImpl> mItems;
    private boolean mItemsChangedWhileDispatchPrevented = false;
    private ArrayList<MenuItemImpl> mNonActionItems;
    private boolean mOptionalIconsVisible = false;
    private boolean mOverrideVisibleItems;
    private CopyOnWriteArrayList<WeakReference<MenuPresenter>> mPresenters = new CopyOnWriteArrayList<>();
    private boolean mPreventDispatchingItemsChanged = false;
    private boolean mQwertyMode;
    private final Resources mResources;
    private boolean mShortcutsVisible;
    private boolean mStructureChangedWhileDispatchPrevented = false;
    private ArrayList<MenuItemImpl> mTempShortcutItemList = new ArrayList<>();
    private ArrayList<MenuItemImpl> mVisibleItems;

    @RestrictTo({Scope.LIBRARY_GROUP})
    /* renamed from: android.support.v7.view.menu.MenuBuilder$Callback */
    public interface Callback {
        boolean onMenuItemSelected(MenuBuilder menuBuilder, MenuItem menuItem);

        void onMenuModeChange(MenuBuilder menuBuilder);
    }

    @RestrictTo({Scope.LIBRARY_GROUP})
    /* renamed from: android.support.v7.view.menu.MenuBuilder$ItemInvoker */
    public interface ItemInvoker {
        boolean invokeItem(MenuItemImpl menuItemImpl);
    }

    public MenuBuilder(Context context) {
        this.mContext = context;
        this.mResources = context.getResources();
        this.mItems = new ArrayList<>();
        this.mVisibleItems = new ArrayList<>();
        this.mIsVisibleItemsStale = true;
        this.mActionItems = new ArrayList<>();
        this.mNonActionItems = new ArrayList<>();
        this.mIsActionItemsStale = true;
        setShortcutsVisibleInner(true);
    }

    private MenuItemImpl createNewMenuItem(int i, int i2, int i3, int i4, CharSequence charSequence, int i5) {
        return new MenuItemImpl(this, i, i2, i3, i4, charSequence, i5);
    }

    private void dispatchPresenterUpdate(boolean z) {
        if (!this.mPresenters.isEmpty()) {
            stopDispatchingItemsChanged();
            Iterator it = this.mPresenters.iterator();
            while (it.hasNext()) {
                WeakReference weakReference = (WeakReference) it.next();
                MenuPresenter menuPresenter = (MenuPresenter) weakReference.get();
                if (menuPresenter == null) {
                    this.mPresenters.remove(weakReference);
                } else {
                    menuPresenter.updateMenuView(z);
                }
            }
            startDispatchingItemsChanged();
        }
    }

    private void dispatchRestoreInstanceState(Bundle bundle) {
        SparseArray sparseParcelableArray = bundle.getSparseParcelableArray(PRESENTER_KEY);
        if (sparseParcelableArray != null && !this.mPresenters.isEmpty()) {
            Iterator it = this.mPresenters.iterator();
            while (it.hasNext()) {
                WeakReference weakReference = (WeakReference) it.next();
                MenuPresenter menuPresenter = (MenuPresenter) weakReference.get();
                if (menuPresenter == null) {
                    this.mPresenters.remove(weakReference);
                } else {
                    int id = menuPresenter.getId();
                    if (id > 0) {
                        Parcelable parcelable = (Parcelable) sparseParcelableArray.get(id);
                        if (parcelable != null) {
                            menuPresenter.onRestoreInstanceState(parcelable);
                        }
                    }
                }
            }
        }
    }

    private void dispatchSaveInstanceState(Bundle bundle) {
        if (!this.mPresenters.isEmpty()) {
            SparseArray sparseArray = new SparseArray();
            Iterator it = this.mPresenters.iterator();
            while (it.hasNext()) {
                WeakReference weakReference = (WeakReference) it.next();
                MenuPresenter menuPresenter = (MenuPresenter) weakReference.get();
                if (menuPresenter == null) {
                    this.mPresenters.remove(weakReference);
                } else {
                    int id = menuPresenter.getId();
                    if (id > 0) {
                        Parcelable onSaveInstanceState = menuPresenter.onSaveInstanceState();
                        if (onSaveInstanceState != null) {
                            sparseArray.put(id, onSaveInstanceState);
                        }
                    }
                }
            }
            bundle.putSparseParcelableArray(PRESENTER_KEY, sparseArray);
        }
    }

    private boolean dispatchSubMenuSelected(SubMenuBuilder subMenuBuilder, MenuPresenter menuPresenter) {
        boolean z = false;
        if (this.mPresenters.isEmpty()) {
            return false;
        }
        if (menuPresenter != null) {
            z = menuPresenter.onSubMenuSelected(subMenuBuilder);
        }
        Iterator it = this.mPresenters.iterator();
        boolean z2 = z;
        while (it.hasNext()) {
            WeakReference weakReference = (WeakReference) it.next();
            MenuPresenter menuPresenter2 = (MenuPresenter) weakReference.get();
            if (menuPresenter2 == null) {
                this.mPresenters.remove(weakReference);
            } else if (!z2) {
                z2 = menuPresenter2.onSubMenuSelected(subMenuBuilder);
            }
        }
        return z2;
    }

    private static int findInsertIndex(ArrayList<MenuItemImpl> arrayList, int i) {
        for (int size = arrayList.size() - 1; size >= 0; size--) {
            if (((MenuItemImpl) arrayList.get(size)).getOrdering() <= i) {
                return size + 1;
            }
        }
        return 0;
    }

    private static int getOrdering(int i) {
        int i2 = (-65536 & i) >> 16;
        if (i2 >= 0 && i2 < sCategoryToOrder.length) {
            return (sCategoryToOrder[i2] << 16) | (65535 & i);
        }
        throw new IllegalArgumentException("order does not contain a valid category.");
    }

    private void removeItemAtInt(int i, boolean z) {
        if (i >= 0 && i < this.mItems.size()) {
            this.mItems.remove(i);
            if (z) {
                onItemsChanged(true);
            }
        }
    }

    private void setHeaderInternal(int i, CharSequence charSequence, int i2, Drawable drawable, View view) {
        Resources resources = getResources();
        if (view != null) {
            this.mHeaderView = view;
            this.mHeaderTitle = null;
            this.mHeaderIcon = null;
        } else {
            if (i > 0) {
                this.mHeaderTitle = resources.getText(i);
            } else if (charSequence != null) {
                this.mHeaderTitle = charSequence;
            }
            if (i2 > 0) {
                this.mHeaderIcon = ContextCompat.getDrawable(getContext(), i2);
            } else if (drawable != null) {
                this.mHeaderIcon = drawable;
            }
            this.mHeaderView = null;
        }
        onItemsChanged(false);
    }

    private void setShortcutsVisibleInner(boolean z) {
        boolean z2 = true;
        if (!z || this.mResources.getConfiguration().keyboard == 1 || !this.mResources.getBoolean(C0260R.bool.abc_config_showMenuShortcutsWhenKeyboardPresent)) {
            z2 = false;
        }
        this.mShortcutsVisible = z2;
    }

    public MenuItem add(int i) {
        return addInternal(0, 0, 0, this.mResources.getString(i));
    }

    public MenuItem add(int i, int i2, int i3, int i4) {
        return addInternal(i, i2, i3, this.mResources.getString(i4));
    }

    public MenuItem add(int i, int i2, int i3, CharSequence charSequence) {
        return addInternal(i, i2, i3, charSequence);
    }

    public MenuItem add(CharSequence charSequence) {
        return addInternal(0, 0, 0, charSequence);
    }

    public int addIntentOptions(int i, int i2, int i3, ComponentName componentName, Intent[] intentArr, Intent intent, int i4, MenuItem[] menuItemArr) {
        PackageManager packageManager = this.mContext.getPackageManager();
        List queryIntentActivityOptions = packageManager.queryIntentActivityOptions(componentName, intentArr, intent, 0);
        int i5 = queryIntentActivityOptions != null ? queryIntentActivityOptions.size() : 0;
        if ((i4 & 1) == 0) {
            removeGroup(i);
        }
        for (int i6 = 0; i6 < i5; i6++) {
            ResolveInfo resolveInfo = (ResolveInfo) queryIntentActivityOptions.get(i6);
            Intent intent2 = new Intent(resolveInfo.specificIndex < 0 ? intent : intentArr[resolveInfo.specificIndex]);
            intent2.setComponent(new ComponentName(resolveInfo.activityInfo.applicationInfo.packageName, resolveInfo.activityInfo.name));
            MenuItem intent3 = add(i, i2, i3, resolveInfo.loadLabel(packageManager)).setIcon(resolveInfo.loadIcon(packageManager)).setIntent(intent2);
            if (menuItemArr != null && resolveInfo.specificIndex >= 0) {
                menuItemArr[resolveInfo.specificIndex] = intent3;
            }
        }
        return i5;
    }

    /* access modifiers changed from: protected */
    public MenuItem addInternal(int i, int i2, int i3, CharSequence charSequence) {
        int ordering = getOrdering(i3);
        MenuItemImpl createNewMenuItem = createNewMenuItem(i, i2, i3, ordering, charSequence, this.mDefaultShowAsAction);
        if (this.mCurrentMenuInfo != null) {
            createNewMenuItem.setMenuInfo(this.mCurrentMenuInfo);
        }
        this.mItems.add(findInsertIndex(this.mItems, ordering), createNewMenuItem);
        onItemsChanged(true);
        return createNewMenuItem;
    }

    public void addMenuPresenter(MenuPresenter menuPresenter) {
        addMenuPresenter(menuPresenter, this.mContext);
    }

    public void addMenuPresenter(MenuPresenter menuPresenter, Context context) {
        this.mPresenters.add(new WeakReference(menuPresenter));
        menuPresenter.initForMenu(context, this);
        this.mIsActionItemsStale = true;
    }

    public SubMenu addSubMenu(int i) {
        return addSubMenu(0, 0, 0, (CharSequence) this.mResources.getString(i));
    }

    public SubMenu addSubMenu(int i, int i2, int i3, int i4) {
        return addSubMenu(i, i2, i3, (CharSequence) this.mResources.getString(i4));
    }

    public SubMenu addSubMenu(int i, int i2, int i3, CharSequence charSequence) {
        MenuItemImpl menuItemImpl = (MenuItemImpl) addInternal(i, i2, i3, charSequence);
        SubMenuBuilder subMenuBuilder = new SubMenuBuilder(this.mContext, this, menuItemImpl);
        menuItemImpl.setSubMenu(subMenuBuilder);
        return subMenuBuilder;
    }

    public SubMenu addSubMenu(CharSequence charSequence) {
        return addSubMenu(0, 0, 0, charSequence);
    }

    public void changeMenuMode() {
        if (this.mCallback != null) {
            this.mCallback.onMenuModeChange(this);
        }
    }

    public void clear() {
        if (this.mExpandedItem != null) {
            collapseItemActionView(this.mExpandedItem);
        }
        this.mItems.clear();
        onItemsChanged(true);
    }

    public void clearAll() {
        this.mPreventDispatchingItemsChanged = true;
        clear();
        clearHeader();
        this.mPreventDispatchingItemsChanged = false;
        this.mItemsChangedWhileDispatchPrevented = false;
        this.mStructureChangedWhileDispatchPrevented = false;
        onItemsChanged(true);
    }

    public void clearHeader() {
        this.mHeaderIcon = null;
        this.mHeaderTitle = null;
        this.mHeaderView = null;
        onItemsChanged(false);
    }

    public void close() {
        close(true);
    }

    public final void close(boolean z) {
        if (!this.mIsClosing) {
            this.mIsClosing = true;
            Iterator it = this.mPresenters.iterator();
            while (it.hasNext()) {
                WeakReference weakReference = (WeakReference) it.next();
                MenuPresenter menuPresenter = (MenuPresenter) weakReference.get();
                if (menuPresenter == null) {
                    this.mPresenters.remove(weakReference);
                } else {
                    menuPresenter.onCloseMenu(this, z);
                }
            }
            this.mIsClosing = false;
        }
    }

    public boolean collapseItemActionView(MenuItemImpl menuItemImpl) {
        boolean z = false;
        if (!this.mPresenters.isEmpty() && this.mExpandedItem == menuItemImpl) {
            stopDispatchingItemsChanged();
            Iterator it = this.mPresenters.iterator();
            boolean z2 = false;
            while (true) {
                if (!it.hasNext()) {
                    z = z2;
                    break;
                }
                WeakReference weakReference = (WeakReference) it.next();
                MenuPresenter menuPresenter = (MenuPresenter) weakReference.get();
                if (menuPresenter == null) {
                    this.mPresenters.remove(weakReference);
                } else {
                    z = menuPresenter.collapseItemActionView(this, menuItemImpl);
                    if (z) {
                        break;
                    }
                    z2 = z;
                }
            }
            startDispatchingItemsChanged();
            if (z) {
                this.mExpandedItem = null;
            }
        }
        return z;
    }

    /* access modifiers changed from: 0000 */
    public boolean dispatchMenuItemSelected(MenuBuilder menuBuilder, MenuItem menuItem) {
        return this.mCallback != null && this.mCallback.onMenuItemSelected(menuBuilder, menuItem);
    }

    public boolean expandItemActionView(MenuItemImpl menuItemImpl) {
        boolean z = false;
        if (!this.mPresenters.isEmpty()) {
            stopDispatchingItemsChanged();
            Iterator it = this.mPresenters.iterator();
            boolean z2 = false;
            while (true) {
                if (!it.hasNext()) {
                    z = z2;
                    break;
                }
                WeakReference weakReference = (WeakReference) it.next();
                MenuPresenter menuPresenter = (MenuPresenter) weakReference.get();
                if (menuPresenter == null) {
                    this.mPresenters.remove(weakReference);
                } else {
                    z = menuPresenter.expandItemActionView(this, menuItemImpl);
                    if (z) {
                        break;
                    }
                    z2 = z;
                }
            }
            startDispatchingItemsChanged();
            if (z) {
                this.mExpandedItem = menuItemImpl;
            }
        }
        return z;
    }

    public int findGroupIndex(int i) {
        return findGroupIndex(i, 0);
    }

    public int findGroupIndex(int i, int i2) {
        int size = size();
        if (i2 < 0) {
            i2 = 0;
        }
        for (int i3 = i2; i3 < size; i3++) {
            if (((MenuItemImpl) this.mItems.get(i3)).getGroupId() == i) {
                return i3;
            }
        }
        return -1;
    }

    public MenuItem findItem(int i) {
        int size = size();
        for (int i2 = 0; i2 < size; i2++) {
            MenuItemImpl menuItemImpl = (MenuItemImpl) this.mItems.get(i2);
            if (menuItemImpl.getItemId() == i) {
                return menuItemImpl;
            }
            if (menuItemImpl.hasSubMenu()) {
                MenuItem findItem = menuItemImpl.getSubMenu().findItem(i);
                if (findItem != null) {
                    return findItem;
                }
            }
        }
        return null;
    }

    public int findItemIndex(int i) {
        int size = size();
        for (int i2 = 0; i2 < size; i2++) {
            if (((MenuItemImpl) this.mItems.get(i2)).getItemId() == i) {
                return i2;
            }
        }
        return -1;
    }

    /* access modifiers changed from: 0000 */
    public MenuItemImpl findItemWithShortcutForKey(int i, KeyEvent keyEvent) {
        ArrayList<MenuItemImpl> arrayList = this.mTempShortcutItemList;
        arrayList.clear();
        findItemsWithShortcutForKey(arrayList, i, keyEvent);
        if (arrayList.isEmpty()) {
            return null;
        }
        int metaState = keyEvent.getMetaState();
        KeyData keyData = new KeyData();
        keyEvent.getKeyData(keyData);
        int size = arrayList.size();
        if (size == 1) {
            return (MenuItemImpl) arrayList.get(0);
        }
        boolean isQwertyMode = isQwertyMode();
        for (int i2 = 0; i2 < size; i2++) {
            MenuItemImpl menuItemImpl = (MenuItemImpl) arrayList.get(i2);
            char numericShortcut = isQwertyMode ? menuItemImpl.getAlphabeticShortcut() : menuItemImpl.getNumericShortcut();
            if (numericShortcut == keyData.meta[0] && (metaState & 2) == 0) {
                return menuItemImpl;
            }
            if (numericShortcut == keyData.meta[2] && (metaState & 2) != 0) {
                return menuItemImpl;
            }
            if (isQwertyMode && numericShortcut == 8 && i == 67) {
                return menuItemImpl;
            }
        }
        return null;
    }

    /* access modifiers changed from: 0000 */
    public void findItemsWithShortcutForKey(List<MenuItemImpl> list, int i, KeyEvent keyEvent) {
        boolean isQwertyMode = isQwertyMode();
        int modifiers = keyEvent.getModifiers();
        KeyData keyData = new KeyData();
        if (keyEvent.getKeyData(keyData) || i == 67) {
            int size = this.mItems.size();
            for (int i2 = 0; i2 < size; i2++) {
                MenuItemImpl menuItemImpl = (MenuItemImpl) this.mItems.get(i2);
                if (menuItemImpl.hasSubMenu()) {
                    ((MenuBuilder) menuItemImpl.getSubMenu()).findItemsWithShortcutForKey(list, i, keyEvent);
                }
                char numericShortcut = isQwertyMode ? menuItemImpl.getAlphabeticShortcut() : menuItemImpl.getNumericShortcut();
                if (((69647 & modifiers) == ((isQwertyMode ? menuItemImpl.getAlphabeticModifiers() : menuItemImpl.getNumericModifiers()) & SupportMenu.SUPPORTED_MODIFIERS_MASK)) && numericShortcut != 0 && ((numericShortcut == keyData.meta[0] || numericShortcut == keyData.meta[2] || (isQwertyMode && numericShortcut == 8 && i == 67)) && menuItemImpl.isEnabled())) {
                    list.add(menuItemImpl);
                }
            }
        }
    }

    public void flagActionItems() {
        ArrayList visibleItems = getVisibleItems();
        if (this.mIsActionItemsStale) {
            Iterator it = this.mPresenters.iterator();
            boolean z = false;
            while (it.hasNext()) {
                WeakReference weakReference = (WeakReference) it.next();
                MenuPresenter menuPresenter = (MenuPresenter) weakReference.get();
                if (menuPresenter == null) {
                    this.mPresenters.remove(weakReference);
                } else {
                    z = menuPresenter.flagActionItems() | z;
                }
            }
            if (z) {
                this.mActionItems.clear();
                this.mNonActionItems.clear();
                int size = visibleItems.size();
                for (int i = 0; i < size; i++) {
                    MenuItemImpl menuItemImpl = (MenuItemImpl) visibleItems.get(i);
                    if (menuItemImpl.isActionButton()) {
                        this.mActionItems.add(menuItemImpl);
                    } else {
                        this.mNonActionItems.add(menuItemImpl);
                    }
                }
            } else {
                this.mActionItems.clear();
                this.mNonActionItems.clear();
                this.mNonActionItems.addAll(getVisibleItems());
            }
            this.mIsActionItemsStale = false;
        }
    }

    public ArrayList<MenuItemImpl> getActionItems() {
        flagActionItems();
        return this.mActionItems;
    }

    /* access modifiers changed from: protected */
    public String getActionViewStatesKey() {
        return ACTION_VIEW_STATES_KEY;
    }

    public Context getContext() {
        return this.mContext;
    }

    public MenuItemImpl getExpandedItem() {
        return this.mExpandedItem;
    }

    public Drawable getHeaderIcon() {
        return this.mHeaderIcon;
    }

    public CharSequence getHeaderTitle() {
        return this.mHeaderTitle;
    }

    public View getHeaderView() {
        return this.mHeaderView;
    }

    public MenuItem getItem(int i) {
        return (MenuItem) this.mItems.get(i);
    }

    public ArrayList<MenuItemImpl> getNonActionItems() {
        flagActionItems();
        return this.mNonActionItems;
    }

    /* access modifiers changed from: 0000 */
    public boolean getOptionalIconsVisible() {
        return this.mOptionalIconsVisible;
    }

    /* access modifiers changed from: 0000 */
    public Resources getResources() {
        return this.mResources;
    }

    public MenuBuilder getRootMenu() {
        return this;
    }

    @NonNull
    public ArrayList<MenuItemImpl> getVisibleItems() {
        if (!this.mIsVisibleItemsStale) {
            return this.mVisibleItems;
        }
        this.mVisibleItems.clear();
        int size = this.mItems.size();
        for (int i = 0; i < size; i++) {
            MenuItemImpl menuItemImpl = (MenuItemImpl) this.mItems.get(i);
            if (menuItemImpl.isVisible()) {
                this.mVisibleItems.add(menuItemImpl);
            }
        }
        this.mIsVisibleItemsStale = false;
        this.mIsActionItemsStale = true;
        return this.mVisibleItems;
    }

    public boolean hasVisibleItems() {
        if (this.mOverrideVisibleItems) {
            return true;
        }
        int size = size();
        for (int i = 0; i < size; i++) {
            if (((MenuItemImpl) this.mItems.get(i)).isVisible()) {
                return true;
            }
        }
        return false;
    }

    /* access modifiers changed from: 0000 */
    public boolean isQwertyMode() {
        return this.mQwertyMode;
    }

    public boolean isShortcutKey(int i, KeyEvent keyEvent) {
        return findItemWithShortcutForKey(i, keyEvent) != null;
    }

    public boolean isShortcutsVisible() {
        return this.mShortcutsVisible;
    }

    /* access modifiers changed from: 0000 */
    public void onItemActionRequestChanged(MenuItemImpl menuItemImpl) {
        this.mIsActionItemsStale = true;
        onItemsChanged(true);
    }

    /* access modifiers changed from: 0000 */
    public void onItemVisibleChanged(MenuItemImpl menuItemImpl) {
        this.mIsVisibleItemsStale = true;
        onItemsChanged(true);
    }

    public void onItemsChanged(boolean z) {
        if (!this.mPreventDispatchingItemsChanged) {
            if (z) {
                this.mIsVisibleItemsStale = true;
                this.mIsActionItemsStale = true;
            }
            dispatchPresenterUpdate(z);
            return;
        }
        this.mItemsChangedWhileDispatchPrevented = true;
        if (z) {
            this.mStructureChangedWhileDispatchPrevented = true;
        }
    }

    public boolean performIdentifierAction(int i, int i2) {
        return performItemAction(findItem(i), i2);
    }

    public boolean performItemAction(MenuItem menuItem, int i) {
        return performItemAction(menuItem, null, i);
    }

    public boolean performItemAction(MenuItem menuItem, MenuPresenter menuPresenter, int i) {
        MenuItemImpl menuItemImpl = (MenuItemImpl) menuItem;
        if (menuItemImpl == null || !menuItemImpl.isEnabled()) {
            return false;
        }
        boolean invoke = menuItemImpl.invoke();
        ActionProvider supportActionProvider = menuItemImpl.getSupportActionProvider();
        boolean z = supportActionProvider != null && supportActionProvider.hasSubMenu();
        if (menuItemImpl.hasCollapsibleActionView()) {
            boolean expandActionView = menuItemImpl.expandActionView() | invoke;
            if (!expandActionView) {
                return expandActionView;
            }
            close(true);
            return expandActionView;
        } else if (menuItemImpl.hasSubMenu() || z) {
            if ((i & 4) == 0) {
                close(false);
            }
            if (!menuItemImpl.hasSubMenu()) {
                menuItemImpl.setSubMenu(new SubMenuBuilder(getContext(), this, menuItemImpl));
            }
            SubMenuBuilder subMenuBuilder = (SubMenuBuilder) menuItemImpl.getSubMenu();
            if (z) {
                supportActionProvider.onPrepareSubMenu(subMenuBuilder);
            }
            boolean dispatchSubMenuSelected = dispatchSubMenuSelected(subMenuBuilder, menuPresenter) | invoke;
            if (dispatchSubMenuSelected) {
                return dispatchSubMenuSelected;
            }
            close(true);
            return dispatchSubMenuSelected;
        } else if ((i & 1) != 0) {
            return invoke;
        } else {
            close(true);
            return invoke;
        }
    }

    public boolean performShortcut(int i, KeyEvent keyEvent, int i2) {
        MenuItemImpl findItemWithShortcutForKey = findItemWithShortcutForKey(i, keyEvent);
        boolean z = false;
        if (findItemWithShortcutForKey != null) {
            z = performItemAction(findItemWithShortcutForKey, i2);
        }
        if ((i2 & 2) != 0) {
            close(true);
        }
        return z;
    }

    public void removeGroup(int i) {
        int findGroupIndex = findGroupIndex(i);
        if (findGroupIndex >= 0) {
            int size = this.mItems.size();
            for (int i2 = 0; i2 < size - findGroupIndex && ((MenuItemImpl) this.mItems.get(findGroupIndex)).getGroupId() == i; i2++) {
                removeItemAtInt(findGroupIndex, false);
            }
            onItemsChanged(true);
        }
    }

    public void removeItem(int i) {
        removeItemAtInt(findItemIndex(i), true);
    }

    public void removeItemAt(int i) {
        removeItemAtInt(i, true);
    }

    public void removeMenuPresenter(MenuPresenter menuPresenter) {
        Iterator it = this.mPresenters.iterator();
        while (it.hasNext()) {
            WeakReference weakReference = (WeakReference) it.next();
            MenuPresenter menuPresenter2 = (MenuPresenter) weakReference.get();
            if (menuPresenter2 == null || menuPresenter2 == menuPresenter) {
                this.mPresenters.remove(weakReference);
            }
        }
    }

    public void restoreActionViewStates(Bundle bundle) {
        if (bundle != null) {
            SparseArray sparseParcelableArray = bundle.getSparseParcelableArray(getActionViewStatesKey());
            int size = size();
            for (int i = 0; i < size; i++) {
                MenuItem item = getItem(i);
                View actionView = item.getActionView();
                if (!(actionView == null || actionView.getId() == -1)) {
                    actionView.restoreHierarchyState(sparseParcelableArray);
                }
                if (item.hasSubMenu()) {
                    ((SubMenuBuilder) item.getSubMenu()).restoreActionViewStates(bundle);
                }
            }
            int i2 = bundle.getInt(EXPANDED_ACTION_VIEW_ID);
            if (i2 > 0) {
                MenuItem findItem = findItem(i2);
                if (findItem != null) {
                    findItem.expandActionView();
                }
            }
        }
    }

    public void restorePresenterStates(Bundle bundle) {
        dispatchRestoreInstanceState(bundle);
    }

    /* JADX WARNING: Removed duplicated region for block: B:13:0x003a  */
    /* JADX WARNING: Removed duplicated region for block: B:20:0x0043 A[SYNTHETIC] */
    /* Code decompiled incorrectly, please refer to instructions dump. */
    public void saveActionViewStates(android.os.Bundle r8) {
        /*
            r7 = this;
            r0 = 0
            int r3 = r7.size()
            r1 = 0
            r2 = r1
        L_0x0007:
            if (r2 >= r3) goto L_0x0047
            android.view.MenuItem r4 = r7.getItem(r2)
            android.view.View r1 = r4.getActionView()
            if (r1 == 0) goto L_0x0051
            int r5 = r1.getId()
            r6 = -1
            if (r5 == r6) goto L_0x0051
            if (r0 != 0) goto L_0x0021
            android.util.SparseArray r0 = new android.util.SparseArray
            r0.<init>()
        L_0x0021:
            r1.saveHierarchyState(r0)
            boolean r1 = r4.isActionViewExpanded()
            if (r1 == 0) goto L_0x0051
            java.lang.String r1 = "android:menu:expandedactionview"
            int r5 = r4.getItemId()
            r8.putInt(r1, r5)
            r1 = r0
        L_0x0034:
            boolean r0 = r4.hasSubMenu()
            if (r0 == 0) goto L_0x0043
            android.view.SubMenu r0 = r4.getSubMenu()
            android.support.v7.view.menu.SubMenuBuilder r0 = (android.support.p003v7.view.menu.SubMenuBuilder) r0
            r0.saveActionViewStates(r8)
        L_0x0043:
            int r2 = r2 + 1
            r0 = r1
            goto L_0x0007
        L_0x0047:
            if (r0 == 0) goto L_0x0050
            java.lang.String r1 = r7.getActionViewStatesKey()
            r8.putSparseParcelableArray(r1, r0)
        L_0x0050:
            return
        L_0x0051:
            r1 = r0
            goto L_0x0034
        */
        throw new UnsupportedOperationException("Method not decompiled: android.support.p003v7.view.menu.MenuBuilder.saveActionViewStates(android.os.Bundle):void");
    }

    public void savePresenterStates(Bundle bundle) {
        dispatchSaveInstanceState(bundle);
    }

    public void setCallback(Callback callback) {
        this.mCallback = callback;
    }

    public void setCurrentMenuInfo(ContextMenuInfo contextMenuInfo) {
        this.mCurrentMenuInfo = contextMenuInfo;
    }

    public MenuBuilder setDefaultShowAsAction(int i) {
        this.mDefaultShowAsAction = i;
        return this;
    }

    /* access modifiers changed from: 0000 */
    public void setExclusiveItemChecked(MenuItem menuItem) {
        int groupId = menuItem.getGroupId();
        int size = this.mItems.size();
        stopDispatchingItemsChanged();
        for (int i = 0; i < size; i++) {
            MenuItemImpl menuItemImpl = (MenuItemImpl) this.mItems.get(i);
            if (menuItemImpl.getGroupId() == groupId && menuItemImpl.isExclusiveCheckable() && menuItemImpl.isCheckable()) {
                menuItemImpl.setCheckedInt(menuItemImpl == menuItem);
            }
        }
        startDispatchingItemsChanged();
    }

    public void setGroupCheckable(int i, boolean z, boolean z2) {
        int size = this.mItems.size();
        for (int i2 = 0; i2 < size; i2++) {
            MenuItemImpl menuItemImpl = (MenuItemImpl) this.mItems.get(i2);
            if (menuItemImpl.getGroupId() == i) {
                menuItemImpl.setExclusiveCheckable(z2);
                menuItemImpl.setCheckable(z);
            }
        }
    }

    public void setGroupEnabled(int i, boolean z) {
        int size = this.mItems.size();
        for (int i2 = 0; i2 < size; i2++) {
            MenuItemImpl menuItemImpl = (MenuItemImpl) this.mItems.get(i2);
            if (menuItemImpl.getGroupId() == i) {
                menuItemImpl.setEnabled(z);
            }
        }
    }

    public void setGroupVisible(int i, boolean z) {
        int size = this.mItems.size();
        boolean z2 = false;
        int i2 = 0;
        while (i2 < size) {
            MenuItemImpl menuItemImpl = (MenuItemImpl) this.mItems.get(i2);
            i2++;
            z2 = (menuItemImpl.getGroupId() != i || !menuItemImpl.setVisibleInt(z)) ? z2 : true;
        }
        if (z2) {
            onItemsChanged(true);
        }
    }

    /* access modifiers changed from: protected */
    public MenuBuilder setHeaderIconInt(int i) {
        setHeaderInternal(0, null, i, null, null);
        return this;
    }

    /* access modifiers changed from: protected */
    public MenuBuilder setHeaderIconInt(Drawable drawable) {
        setHeaderInternal(0, null, 0, drawable, null);
        return this;
    }

    /* access modifiers changed from: protected */
    public MenuBuilder setHeaderTitleInt(int i) {
        setHeaderInternal(i, null, 0, null, null);
        return this;
    }

    /* access modifiers changed from: protected */
    public MenuBuilder setHeaderTitleInt(CharSequence charSequence) {
        setHeaderInternal(0, charSequence, 0, null, null);
        return this;
    }

    /* access modifiers changed from: protected */
    public MenuBuilder setHeaderViewInt(View view) {
        setHeaderInternal(0, null, 0, null, view);
        return this;
    }

    public void setOptionalIconsVisible(boolean z) {
        this.mOptionalIconsVisible = z;
    }

    public void setOverrideVisibleItems(boolean z) {
        this.mOverrideVisibleItems = z;
    }

    public void setQwertyMode(boolean z) {
        this.mQwertyMode = z;
        onItemsChanged(false);
    }

    public void setShortcutsVisible(boolean z) {
        if (this.mShortcutsVisible != z) {
            setShortcutsVisibleInner(z);
            onItemsChanged(false);
        }
    }

    public int size() {
        return this.mItems.size();
    }

    public void startDispatchingItemsChanged() {
        this.mPreventDispatchingItemsChanged = false;
        if (this.mItemsChangedWhileDispatchPrevented) {
            this.mItemsChangedWhileDispatchPrevented = false;
            onItemsChanged(this.mStructureChangedWhileDispatchPrevented);
        }
    }

    public void stopDispatchingItemsChanged() {
        if (!this.mPreventDispatchingItemsChanged) {
            this.mPreventDispatchingItemsChanged = true;
            this.mItemsChangedWhileDispatchPrevented = false;
            this.mStructureChangedWhileDispatchPrevented = false;
        }
    }
}
