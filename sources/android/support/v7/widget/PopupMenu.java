package android.support.p003v7.widget;

import android.content.Context;
import android.support.annotation.AttrRes;
import android.support.annotation.MenuRes;
import android.support.annotation.NonNull;
import android.support.annotation.Nullable;
import android.support.annotation.StyleRes;
import android.support.p003v7.appcompat.C0260R;
import android.support.p003v7.view.SupportMenuInflater;
import android.support.p003v7.view.menu.MenuBuilder;
import android.support.p003v7.view.menu.MenuBuilder.Callback;
import android.support.p003v7.view.menu.MenuPopupHelper;
import android.support.p003v7.view.menu.ShowableListMenu;
import android.view.Menu;
import android.view.MenuInflater;
import android.view.MenuItem;
import android.view.View;
import android.view.View.OnTouchListener;

/* renamed from: android.support.v7.widget.PopupMenu */
public class PopupMenu {
    private final View mAnchor;
    private final Context mContext;
    private OnTouchListener mDragListener;
    private final MenuBuilder mMenu;
    OnMenuItemClickListener mMenuItemClickListener;
    OnDismissListener mOnDismissListener;
    final MenuPopupHelper mPopup;

    /* renamed from: android.support.v7.widget.PopupMenu$OnDismissListener */
    public interface OnDismissListener {
        void onDismiss(PopupMenu popupMenu);
    }

    /* renamed from: android.support.v7.widget.PopupMenu$OnMenuItemClickListener */
    public interface OnMenuItemClickListener {
        boolean onMenuItemClick(MenuItem menuItem);
    }

    public PopupMenu(@NonNull Context context, @NonNull View view) {
        this(context, view, 0);
    }

    public PopupMenu(@NonNull Context context, @NonNull View view, int i) {
        this(context, view, i, C0260R.attr.popupMenuStyle, 0);
    }

    public PopupMenu(@NonNull Context context, @NonNull View view, int i, @AttrRes int i2, @StyleRes int i3) {
        this.mContext = context;
        this.mAnchor = view;
        this.mMenu = new MenuBuilder(context);
        this.mMenu.setCallback(new Callback() {
            public boolean onMenuItemSelected(MenuBuilder menuBuilder, MenuItem menuItem) {
                if (PopupMenu.this.mMenuItemClickListener != null) {
                    return PopupMenu.this.mMenuItemClickListener.onMenuItemClick(menuItem);
                }
                return false;
            }

            public void onMenuModeChange(MenuBuilder menuBuilder) {
            }
        });
        this.mPopup = new MenuPopupHelper(context, this.mMenu, view, false, i2, i3);
        this.mPopup.setGravity(i);
        this.mPopup.setOnDismissListener(new android.widget.PopupWindow.OnDismissListener() {
            public void onDismiss() {
                if (PopupMenu.this.mOnDismissListener != null) {
                    PopupMenu.this.mOnDismissListener.onDismiss(PopupMenu.this);
                }
            }
        });
    }

    public void dismiss() {
        this.mPopup.dismiss();
    }

    @NonNull
    public OnTouchListener getDragToOpenListener() {
        if (this.mDragListener == null) {
            this.mDragListener = new ForwardingListener(this.mAnchor) {
                public ShowableListMenu getPopup() {
                    return PopupMenu.this.mPopup.getPopup();
                }

                /* access modifiers changed from: protected */
                public boolean onForwardingStarted() {
                    PopupMenu.this.show();
                    return true;
                }

                /* access modifiers changed from: protected */
                public boolean onForwardingStopped() {
                    PopupMenu.this.dismiss();
                    return true;
                }
            };
        }
        return this.mDragListener;
    }

    public int getGravity() {
        return this.mPopup.getGravity();
    }

    @NonNull
    public Menu getMenu() {
        return this.mMenu;
    }

    @NonNull
    public MenuInflater getMenuInflater() {
        return new SupportMenuInflater(this.mContext);
    }

    public void inflate(@MenuRes int i) {
        getMenuInflater().inflate(i, this.mMenu);
    }

    public void setGravity(int i) {
        this.mPopup.setGravity(i);
    }

    public void setOnDismissListener(@Nullable OnDismissListener onDismissListener) {
        this.mOnDismissListener = onDismissListener;
    }

    public void setOnMenuItemClickListener(@Nullable OnMenuItemClickListener onMenuItemClickListener) {
        this.mMenuItemClickListener = onMenuItemClickListener;
    }

    public void show() {
        this.mPopup.show();
    }
}
