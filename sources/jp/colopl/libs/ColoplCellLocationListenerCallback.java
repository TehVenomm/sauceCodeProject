package p018jp.colopl.libs;

import android.location.Location;

/* renamed from: jp.colopl.libs.ColoplCellLocationListenerCallback */
public interface ColoplCellLocationListenerCallback {
    void receiveFailedCdmaCellLocation(ColoplCellLocationListener coloplCellLocationListener);

    void receiveSuccessCdmaCellLocation(ColoplCellLocationListener coloplCellLocationListener, Location location);
}
