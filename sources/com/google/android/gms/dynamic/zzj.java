package com.google.android.gms.dynamic;

import android.annotation.SuppressLint;
import android.app.Fragment;
import android.content.Intent;
import android.os.Bundle;
import android.view.View;

@SuppressLint({"NewApi"})
public final class zzj extends zzl {
    private Fragment zzgov;

    private zzj(Fragment fragment) {
        this.zzgov = fragment;
    }

    public static zzj zza(Fragment fragment) {
        return fragment != null ? new zzj(fragment) : null;
    }

    public final Bundle getArguments() {
        return this.zzgov.getArguments();
    }

    public final int getId() {
        return this.zzgov.getId();
    }

    public final boolean getRetainInstance() {
        return this.zzgov.getRetainInstance();
    }

    public final String getTag() {
        return this.zzgov.getTag();
    }

    public final int getTargetRequestCode() {
        return this.zzgov.getTargetRequestCode();
    }

    public final boolean getUserVisibleHint() {
        return this.zzgov.getUserVisibleHint();
    }

    public final IObjectWrapper getView() {
        return zzn.zzw(this.zzgov.getView());
    }

    public final boolean isAdded() {
        return this.zzgov.isAdded();
    }

    public final boolean isDetached() {
        return this.zzgov.isDetached();
    }

    public final boolean isHidden() {
        return this.zzgov.isHidden();
    }

    public final boolean isInLayout() {
        return this.zzgov.isInLayout();
    }

    public final boolean isRemoving() {
        return this.zzgov.isRemoving();
    }

    public final boolean isResumed() {
        return this.zzgov.isResumed();
    }

    public final boolean isVisible() {
        return this.zzgov.isVisible();
    }

    public final void setHasOptionsMenu(boolean z) {
        this.zzgov.setHasOptionsMenu(z);
    }

    public final void setMenuVisibility(boolean z) {
        this.zzgov.setMenuVisibility(z);
    }

    public final void setRetainInstance(boolean z) {
        this.zzgov.setRetainInstance(z);
    }

    public final void setUserVisibleHint(boolean z) {
        this.zzgov.setUserVisibleHint(z);
    }

    public final void startActivity(Intent intent) {
        this.zzgov.startActivity(intent);
    }

    public final void startActivityForResult(Intent intent, int i) {
        this.zzgov.startActivityForResult(intent, i);
    }

    public final void zzaa(IObjectWrapper iObjectWrapper) {
        this.zzgov.unregisterForContextMenu((View) zzn.zzab(iObjectWrapper));
    }

    public final IObjectWrapper zzaob() {
        return zzn.zzw(this.zzgov.getActivity());
    }

    public final zzk zzaoc() {
        return zza(this.zzgov.getParentFragment());
    }

    public final IObjectWrapper zzaod() {
        return zzn.zzw(this.zzgov.getResources());
    }

    public final zzk zzaoe() {
        return zza(this.zzgov.getTargetFragment());
    }

    public final void zzz(IObjectWrapper iObjectWrapper) {
        this.zzgov.registerForContextMenu((View) zzn.zzab(iObjectWrapper));
    }
}
