package net.gogame.gopay.vip;

import android.content.Context;
import android.net.ConnectivityManager;
import android.net.NetworkInfo;
import java.io.File;
import java.io.IOException;
import java.io.OutputStream;
import net.gogame.gopay.vip.TaskQueue.Listener;
import net.gogame.gopay.vip.tape2.ObjectQueue.Converter;
import org.json.JSONObject;

public class CustomTaskQueue extends TapeTaskQueue<BaseEvent> {
    /* renamed from: a */
    private final ConnectivityManager f3654a;

    public static class CustomConverter implements Converter<BaseEvent> {
        public BaseEvent from(byte[] bArr) throws IOException {
            BaseEvent baseEvent = null;
            try {
                JSONObject jSONObject = new JSONObject(new String(bArr, "UTF-8"));
                String optString = jSONObject.optString("@eventType", null);
                if (optString != null) {
                    Object obj = -1;
                    switch (optString.hashCode()) {
                        case -548821511:
                            if (optString.equals(PurchaseEvent.EVENT_TYPE)) {
                                obj = null;
                                break;
                            }
                            break;
                    }
                    switch (obj) {
                        case null:
                            baseEvent = new PurchaseEvent();
                            baseEvent.unmarshal(jSONObject);
                            break;
                        default:
                            break;
                    }
                }
                return baseEvent;
            } catch (Throwable e) {
                throw new IOException(e);
            }
        }

        public void toStream(BaseEvent baseEvent, OutputStream outputStream) throws IOException {
            try {
                outputStream.write(baseEvent.marshal().toString().getBytes("UTF-8"));
            } catch (Throwable e) {
                throw new IOException(e);
            }
        }
    }

    public CustomTaskQueue(Context context, File file, Listener listener) throws IOException {
        super(file, new CustomConverter(), listener);
        this.f3654a = (ConnectivityManager) context.getSystemService("connectivity");
    }

    protected boolean shouldProcess() {
        if (this.f3654a == null) {
            return false;
        }
        NetworkInfo activeNetworkInfo = this.f3654a.getActiveNetworkInfo();
        if (activeNetworkInfo == null || !activeNetworkInfo.isConnectedOrConnecting()) {
            return false;
        }
        return true;
    }
}
