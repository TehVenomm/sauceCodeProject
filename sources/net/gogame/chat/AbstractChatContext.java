package net.gogame.chat;

import android.database.DataSetObserver;
import android.net.Uri;
import android.util.Log;
import java.util.HashMap;
import java.util.HashSet;
import java.util.Map;
import java.util.Set;

public abstract class AbstractChatContext implements ChatContext {
    private final Map<String, Uri> imageMap = new HashMap();
    private final Set<DataSetObserver> observers = new HashSet();

    public void notifyDataSetChanged() {
        for (DataSetObserver onChanged : this.observers) {
            try {
                onChanged.onChanged();
            } catch (Throwable e) {
                Log.e(Constants.TAG, "Exception", e);
            }
        }
    }

    public void registerDataSetObserver(DataSetObserver dataSetObserver) {
        this.observers.add(dataSetObserver);
    }

    public void unregisterDataSetObserver(DataSetObserver dataSetObserver) {
        this.observers.remove(dataSetObserver);
    }

    public void registerImage(String str, Uri uri) {
        this.imageMap.put(str, uri);
    }

    protected Uri getImageUri(String str) {
        return (Uri) this.imageMap.get(str);
    }
}
