package p018jp.colopl.libs;

import android.location.Location;
import android.telephony.CellLocation;
import android.util.Log;
import java.lang.reflect.InvocationTargetException;
import java.lang.reflect.Method;
import java.util.Date;

/* renamed from: jp.colopl.libs.CdmaCellLocationRef */
public class CdmaCellLocationRef {
    private static final String CDMA_CELL_LOCATION_CLASS_NAME = "android.telephony.cdma.CdmaCellLocation";
    private static final int INVALID_LAT_LON = Integer.MAX_VALUE;
    private static final float PSEUDO_ACCURACY = 350.0f;
    public static final float PSEUDO_ACCURACY_FOR_PIN = 1500.0f;
    public static final String PSEUDO_NAME_AS_LOCATION_PROVIDER = "CdmaCellLocationProvider";
    private static final String TAG = "CdmaCellLocationRef";
    static Class<?> cdmaCellLocationClass = null;
    private Method getBaseStationLatitude;
    private Method getBaseStationLongitude;
    public boolean isValid = false;
    private CellLocation location;

    public CdmaCellLocationRef(CellLocation cellLocation) {
        if (isCdmaCellLocationInstance(cellLocation)) {
            Class cdmaCellLocationClass2 = getCdmaCellLocationClass();
            try {
                this.getBaseStationLatitude = cdmaCellLocationClass2.getMethod("getBaseStationLatitude", null);
                this.getBaseStationLongitude = cdmaCellLocationClass2.getMethod("getBaseStationLongitude", null);
                this.location = cellLocation;
                this.isValid = true;
            } catch (NoSuchMethodException e) {
            }
        }
    }

    public static CdmaCellLocationRef getCastedInstance(CellLocation cellLocation) {
        if (!isCdmaCellLocationInstance(cellLocation)) {
            return null;
        }
        CdmaCellLocationRef cdmaCellLocationRef = new CdmaCellLocationRef(cellLocation);
        if (cdmaCellLocationRef.isValid) {
            return cdmaCellLocationRef;
        }
        return null;
    }

    private static Class<?> getCdmaCellLocationClass() {
        if (cdmaCellLocationClass == null) {
            try {
                cdmaCellLocationClass = Class.forName(CDMA_CELL_LOCATION_CLASS_NAME);
            } catch (ClassNotFoundException e) {
            }
        }
        return cdmaCellLocationClass;
    }

    private static boolean isCdmaCellLocationInstance(CellLocation cellLocation) {
        Class cdmaCellLocationClass2 = getCdmaCellLocationClass();
        return cdmaCellLocationClass2 != null && cdmaCellLocationClass2.isInstance(cellLocation);
    }

    public int getBaseStationLatitude() {
        try {
            return ((Integer) this.getBaseStationLatitude.invoke(this.location, null)).intValue();
        } catch (NullPointerException e) {
            return Integer.MAX_VALUE;
        } catch (IllegalAccessException e2) {
            return Integer.MAX_VALUE;
        } catch (IllegalArgumentException e3) {
            return Integer.MAX_VALUE;
        } catch (InvocationTargetException e4) {
            return Integer.MAX_VALUE;
        }
    }

    public int getBaseStationLongitude() {
        try {
            return ((Integer) this.getBaseStationLongitude.invoke(this.location, null)).intValue();
        } catch (NullPointerException e) {
            return Integer.MAX_VALUE;
        } catch (IllegalAccessException e2) {
            return Integer.MAX_VALUE;
        } catch (IllegalArgumentException e3) {
            return Integer.MAX_VALUE;
        } catch (InvocationTargetException e4) {
            return Integer.MAX_VALUE;
        }
    }

    public Location getLocation() {
        int baseStationLatitude = getBaseStationLatitude();
        int baseStationLongitude = getBaseStationLongitude();
        if (baseStationLatitude == Integer.MAX_VALUE || baseStationLongitude == Integer.MAX_VALUE) {
            Log.i(TAG, "Location is invalid. latitude = " + baseStationLatitude + ", longitude = " + baseStationLongitude);
            return null;
        }
        double d = (double) ((((float) baseStationLatitude) / 3600.0f) / 4.0f);
        double d2 = (double) ((((float) baseStationLongitude) / 3600.0f) / 4.0f);
        if (Math.abs(d) < 0.1d || Math.abs(d2) < 0.1d) {
            return null;
        }
        long time = new Date().getTime();
        Location location2 = new Location(PSEUDO_NAME_AS_LOCATION_PROVIDER);
        location2.setLatitude(d);
        location2.setLongitude(d2);
        location2.setTime(time);
        location2.setAccuracy(PSEUDO_ACCURACY);
        return location2;
    }
}
