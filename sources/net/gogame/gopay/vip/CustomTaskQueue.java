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
    private final ConnectivityManager f1332a;

    public static class CustomConverter implements Converter<BaseEvent> {
        public BaseEvent from(byte[] bArr) throws IOException {
            try {
                JSONObject jSONObject = new JSONObject(new String(bArr, "UTF-8"));
                String optString = jSONObject.optString("@eventType", null);
                if (optString == null) {
                    return null;
                }
                char c = 65535;
                switch (optString.hashCode()) {
                    case -548821511:
                        if (optString.equals(PurchaseEvent.EVENT_TYPE)) {
                            c = 0;
                            break;
                        }
                        break;
                }
                switch (c) {
                    case 0:
                        PurchaseEvent purchaseEvent = new PurchaseEvent();
                        purchaseEvent.unmarshal(jSONObject);
                        return purchaseEvent;
                    default:
                        return null;
                }
            } catch (Exception e) {
                throw new IOException(e);
            }
        }

        public void toStream(BaseEvent baseEvent, OutputStream outputStream) throws IOException {
            try {
                outputStream.write(baseEvent.marshal().toString().getBytes("UTF-8"));
            } catch (Exception e) {
                throw new IOException(e);
            }
        }
    }

    public CustomTaskQueue(Context context, File file, Listener listener) throws IOException {
        super(file, new CustomConverter(), listener);
        this.f1332a = (ConnectivityManager) context.getSystemService("connectivity");
    }

    /* access modifiers changed from: protected */
    public boolean shouldProcess() {
        if (this.f1332a == null) {
            return false;
        }
        NetworkInfo activeNetworkInfo = this.f1332a.getActiveNetworkInfo();
        if (activeNetworkInfo == null || !activeNetworkInfo.isConnectedOrConnecting()) {
            return false;
        }
        return true;
    }
}
