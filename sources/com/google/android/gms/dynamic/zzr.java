package com.google.android.gms.dynamic;

import android.content.Intent;
import android.os.Bundle;
import android.support.v4.app.Fragment;
import android.view.View;

public final class zzr extends zzl {
    private Fragment zzgoy;

    private zzr(Fragment fragment) {
        this.zzgoy = fragment;
    }

    public static zzr zza(Fragment fragment) {
        return fragment != null ? new zzr(fragment) : null;
    }

    public final Bundle getArguments() {
        return this.zzgoy.getArguments();
    }

    public final int getId() {
        return this.zzgoy.getId();
    }

    public final boolean getRetainInstance() {
        return this.zzgoy.getRetainInstance();
    }

    public final String getTag() {
        return this.zzgoy.getTag();
    }

    public final int getTargetRequestCode() {
        return this.zzgoy.getTargetRequestCode();
    }

    public final boolean getUserVisibleHint() {
        return this.zzgoy.getUserVisibleHint();
    }

    public final IObjectWrapper getView() {
        return zzn.zzw(this.zzgoy.getView());
    }

    public final boolean isAdded() {
        return this.zzgoy.isAdded();
    }

    public final boolean isDetached() {
        return this.zzgoy.isDetached();
    }

    public final boolean isHidden() {
        return this.zzgoy.isHidden();
    }

    public final boolean isInLayout() {
        return this.zzgoy.isInLayout();
    }

    public final boolean isRemoving() {
        return this.zzgoy.isRemoving();
    }

    public final boolean isResumed() {
        return this.zzgoy.isResumed();
    }

    public final boolean isVisible() {
        return this.zzgoy.isVisible();
    }

    public final void setHasOptionsMenu(boolean z) {
        this.zzgoy.setHasOptionsMenu(z);
    }

    public final void setMenuVisibility(boolean z) {
        this.zzgoy.setMenuVisibility(z);
    }

    public final void setRetainInstance(boolean z) {
        this.zzgoy.setRetainInstance(z);
    }

    public final void setUserVisibleHint(boolean z) {
        this.zzgoy.setUserVisibleHint(z);
    }

    public final void startActivity(Intent intent) {
        this.zzgoy.startActivity(intent);
    }

    public final void startActivityForResult(Intent intent, int i) {
        this.zzgoy.startActivityForResult(intent, i);
    }

    public final void zzaa(IObjectWrapper iObjectWrapper) {
        this.zzgoy.unregisterForContextMenu((View) zzn.zzab(iObjectWrapper));
    }

    public final IObjectWrapper zzaob() {
        return zzn.zzw(this.zzgoy.getActivity());
    }

    public final zzk zzaoc() {
        return zza(this.zzgoy.getParentFragment());
    }

    public final IObjectWrapper zzaod() {
        return zzn.zzw(this.zzgoy.getResources());
    }

    public final zzk zzaoe() {
        return zza(this.zzgoy.getTargetFragment());
    }

    public final void zzz(IObjectWrapper iObjectWrapper) {
        this.zzgoy.registerForContextMenu((View) zzn.zzab(iObjectWrapper));
    }
}
