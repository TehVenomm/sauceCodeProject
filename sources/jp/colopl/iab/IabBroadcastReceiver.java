package p018jp.colopl.iab;

import android.content.BroadcastReceiver;
import android.content.Context;
import android.content.Intent;

/* renamed from: jp.colopl.iab.IabBroadcastReceiver */
public class IabBroadcastReceiver extends BroadcastReceiver {
    private final IabBroadcastListener mListener;

    /* renamed from: jp.colopl.iab.IabBroadcastReceiver$IabBroadcastListener */
    public interface IabBroadcastListener {
        void receivedBroadcast();
    }

    public IabBroadcastReceiver(IabBroadcastListener iabBroadcastListener) {
        this.mListener = iabBroadcastListener;
    }

    public void onReceive(Context context, Intent intent) {
        if (this.mListener != null) {
            this.mListener.receivedBroadcast();
        }
    }
}
