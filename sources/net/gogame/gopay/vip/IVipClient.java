package net.gogame.gopay.vip;

import android.content.Context;

public interface IVipClient {

    public interface Listener {
        void onVipStatus(VipStatus vipStatus);
    }

    void addListener(Listener listener);

    void checkVipStatus(String str, boolean z);

    VipStatus getVipStatus();

    void init(Context context, String str, String str2);

    void removeListener(Listener listener);

    void setExtraData(String str, String str2);

    void setExtraHeader(String str, String str2);

    void trackPurchase(PurchaseEvent purchaseEvent);
}
