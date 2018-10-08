package jp.colopl.libs;

import android.content.Context;
import android.location.Location;
import android.telephony.CellLocation;
import android.telephony.PhoneStateListener;
import android.telephony.TelephonyManager;

public class ColoplCellLocationListener extends PhoneStateListener {
    public static final int ERROR_TYPE_FAIL_REFLECTION = 1;
    public static final int ERROR_TYPE_NO_ERROR = 0;
    public static final int ERROR_TYPE_NULL_LOCATION = 2;
    private ColoplCellLocationListenerCallback callback = null;
    private int errorType = 0;

    public ColoplCellLocationListener(ColoplCellLocationListenerCallback coloplCellLocationListenerCallback) {
        this.callback = coloplCellLocationListenerCallback;
    }

    public static void startListenCellLocationChange(Context context, ColoplCellLocationListener coloplCellLocationListener) {
        ((TelephonyManager) context.getSystemService("phone")).listen(coloplCellLocationListener, 16);
    }

    public static void stopListenCellLocationChange(Context context, ColoplCellLocationListener coloplCellLocationListener) {
        ((TelephonyManager) context.getSystemService("phone")).listen(coloplCellLocationListener, 0);
    }

    public int getErrorType() {
        return this.errorType;
    }

    public void onCellLocationChanged(CellLocation cellLocation) {
        CdmaCellLocationRef castedInstance = CdmaCellLocationRef.getCastedInstance(cellLocation);
        if (castedInstance != null) {
            Location location = castedInstance.getLocation();
            if (this.callback == null) {
                return;
            }
            if (location == null) {
                this.errorType = 2;
                this.callback.receiveFailedCdmaCellLocation(this);
                return;
            }
            this.callback.receiveSuccessCdmaCellLocation(this, location);
        } else if (this.callback != null) {
            this.errorType = 1;
            this.callback.receiveFailedCdmaCellLocation(this);
        }
    }
}
