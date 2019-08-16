package net.gogame.gowrap.p019ui.view;

import android.os.Bundle;
import android.widget.ExpandableListView;
import android.widget.ExpandableListView.OnGroupExpandListener;

/* renamed from: net.gogame.gowrap.ui.view.AutoClosingExpandableListViewListener */
public class AutoClosingExpandableListViewListener implements OnGroupExpandListener {
    private int lastExpandedPosition = -1;
    private final ExpandableListView parent;

    public AutoClosingExpandableListViewListener(ExpandableListView expandableListView, Bundle bundle) {
        this.parent = expandableListView;
        if (bundle != null) {
            this.lastExpandedPosition = bundle.getInt("lastExpandedPosition", -1);
        }
    }

    public void onGroupExpand(int i) {
        if (!(this.lastExpandedPosition == -1 || i == this.lastExpandedPosition)) {
            this.parent.collapseGroup(this.lastExpandedPosition);
        }
        this.lastExpandedPosition = i;
    }

    public void saveState(Bundle bundle) {
        bundle.putInt("lastExpandedPosition", this.lastExpandedPosition);
    }
}
