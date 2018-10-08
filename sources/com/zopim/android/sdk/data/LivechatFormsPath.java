package com.zopim.android.sdk.data;

import android.util.Log;
import com.zopim.android.sdk.model.Forms;

public class LivechatFormsPath extends Path<Forms> {
    private static final LivechatFormsPath INSTANCE = new LivechatFormsPath();
    private static final String TAG = LivechatFormsPath.class.getSimpleName();
    private final Object mLock = new Object();

    private LivechatFormsPath() {
    }

    public static synchronized LivechatFormsPath getInstance() {
        LivechatFormsPath livechatFormsPath;
        synchronized (LivechatFormsPath.class) {
            livechatFormsPath = INSTANCE;
        }
        return livechatFormsPath;
    }

    void clear() {
        this.mData = null;
    }

    public Forms getData() {
        return (Forms) this.mData;
    }

    void update(String str) {
        if (isClearRequired(str)) {
            clear();
        } else if (!str.isEmpty()) {
            synchronized (this.mLock) {
                if (this.mData == null) {
                    this.mData = this.PARSER.parse(str, new C0867f(this));
                } else {
                    try {
                        this.mData = (Forms) this.PARSER.getMapper().readerForUpdating(this.mData).readValue(str);
                    } catch (Throwable e) {
                        Log.w(TAG, "Failed to process json. Forms could not be updated.", e);
                    } catch (Throwable e2) {
                        Log.w(TAG, "IO error. Forms could not be updated.", e2);
                    }
                }
                broadcast(getData());
            }
        }
    }
}
