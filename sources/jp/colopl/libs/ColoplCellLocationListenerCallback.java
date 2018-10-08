package jp.colopl.libs;

import android.location.Location;

public interface ColoplCellLocationListenerCallback {
    void receiveFailedCdmaCellLocation(ColoplCellLocationListener coloplCellLocationListener);

    void receiveSuccessCdmaCellLocation(ColoplCellLocationListener coloplCellLocationListener, Location location);
}
