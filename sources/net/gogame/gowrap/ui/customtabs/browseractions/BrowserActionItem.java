package net.gogame.gowrap.ui.customtabs.browseractions;

import android.app.PendingIntent;
import android.graphics.Bitmap;

public class BrowserActionItem {
    private final PendingIntent mAction;
    private Bitmap mIcon;
    private final String mTitle;

    public BrowserActionItem(String str, PendingIntent pendingIntent, Bitmap bitmap) {
        this.mTitle = str;
        this.mAction = pendingIntent;
        this.mIcon = bitmap;
    }

    public BrowserActionItem(String str, PendingIntent pendingIntent) {
        this(str, pendingIntent, null);
    }

    public void setIcon(Bitmap bitmap) {
        this.mIcon = bitmap;
    }

    public Bitmap getIcon() {
        return this.mIcon;
    }

    public String getTitle() {
        return this.mTitle;
    }

    public PendingIntent getAction() {
        return this.mAction;
    }
}
