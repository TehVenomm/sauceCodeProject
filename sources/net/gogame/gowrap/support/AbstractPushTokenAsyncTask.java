package net.gogame.gowrap.support;

import android.content.Context;
import android.os.AsyncTask;
import android.util.Log;
import net.gogame.gowrap.Constants;

public abstract class AbstractPushTokenAsyncTask extends AsyncTask<Context, Void, Void> {
    protected abstract void onPushTokenReceived(Context context, String str) throws Exception;

    protected Void doInBackground(Context... contextArr) {
        for (Context context : contextArr) {
            try {
                String token = PushUtils.getToken(context);
                if (token != null) {
                    onPushTokenReceived(context, token);
                }
            } catch (Throwable e) {
                Log.e(Constants.TAG, "Exception", e);
            }
        }
        return null;
    }
}
