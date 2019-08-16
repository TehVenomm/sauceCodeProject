package com.github.droidfu.adapters;

import android.app.Activity;
import android.app.ExpandableListActivity;
import android.app.ListActivity;
import android.view.View;
import android.view.ViewGroup;
import android.widget.AbsListView;
import android.widget.BaseAdapter;
import java.util.ArrayList;
import java.util.List;

public abstract class ListAdapterWithProgress<T> extends BaseAdapter {
    private ArrayList<T> data;
    private boolean isLoadingData;
    private AbsListView listView;
    private View progressView;

    public ListAdapterWithProgress(Activity activity, AbsListView absListView, int i) {
        this.data = new ArrayList<>();
        this.listView = absListView;
        this.progressView = activity.getLayoutInflater().inflate(i, absListView, false);
    }

    public ListAdapterWithProgress(ExpandableListActivity expandableListActivity, int i) {
        this(expandableListActivity, expandableListActivity.getExpandableListView(), i);
    }

    public ListAdapterWithProgress(ListActivity listActivity, int i) {
        this(listActivity, listActivity.getListView(), i);
    }

    private boolean isPositionOfProgressElement(int i) {
        return this.isLoadingData && i == this.data.size();
    }

    public void addAll(List<T> list) {
        this.data.addAll(list);
        notifyDataSetChanged();
    }

    public void addAll(List<T> list, boolean z) {
        this.data.addAll(list);
        if (z) {
            notifyDataSetChanged();
        }
    }

    public boolean areAllItemsEnabled() {
        return false;
    }

    public void clear() {
        this.data.clear();
        notifyDataSetChanged();
    }

    /* access modifiers changed from: protected */
    public abstract View doGetView(int i, View view, ViewGroup viewGroup);

    public int getCount() {
        int i = 0;
        if (this.data != null) {
            i = this.data.size() + 0;
        }
        return this.isLoadingData ? i + 1 : i;
    }

    public ArrayList<T> getData() {
        return this.data;
    }

    public T getItem(int i) {
        if (this.data == null) {
            return null;
        }
        return this.data.get(i);
    }

    public int getItemCount() {
        if (this.data != null) {
            return this.data.size();
        }
        return 0;
    }

    public long getItemId(int i) {
        return (long) i;
    }

    public int getItemViewType(int i) {
        return isPositionOfProgressElement(i) ? -1 : 0;
    }

    public AbsListView getListView() {
        return this.listView;
    }

    public View getProgressView() {
        return this.progressView;
    }

    public final View getView(int i, View view, ViewGroup viewGroup) {
        return isPositionOfProgressElement(i) ? this.progressView : doGetView(i, view, viewGroup);
    }

    public int getViewTypeCount() {
        return 2;
    }

    public boolean hasItems() {
        return this.data != null && !this.data.isEmpty();
    }

    public boolean isEmpty() {
        return getCount() == 0 && !this.isLoadingData;
    }

    public boolean isEnabled(int i) {
        return !isPositionOfProgressElement(i);
    }

    public boolean isLoadingData() {
        return this.isLoadingData;
    }

    public void remove(int i) {
        this.data.remove(i);
        notifyDataSetChanged();
    }

    public void setIsLoadingData(boolean z) {
        setIsLoadingData(z, true);
    }

    public void setIsLoadingData(boolean z, boolean z2) {
        this.isLoadingData = z;
        if (z2) {
            notifyDataSetChanged();
        }
    }
}
