package p018jp.colopl.api.docomo;

import android.location.Location;

/* renamed from: jp.colopl.api.docomo.Feature */
public class Feature {
    private String address;
    private String adrCode;
    private String areaCode;
    private String areaName;
    private double latitude;
    private double longitude;
    private String postCode;
    private long time;
    private String timeStr;

    public String getAddress() {
        return this.address;
    }

    public String getAdrCode() {
        return this.adrCode;
    }

    public String getAreaCode() {
        return this.areaCode;
    }

    public String getAreaName() {
        return this.areaName;
    }

    public double getLatitude() {
        return this.latitude;
    }

    public Location getLocation() {
        double latitude2 = getLatitude();
        double longitude2 = getLongitude();
        long time2 = getTime();
        Location location = new Location(DoCoMoAPI.PSEUDO_NAME_AS_LOCATION_PROVIDER);
        location.setLatitude(latitude2);
        location.setLongitude(longitude2);
        location.setTime(time2);
        location.setAccuracy(250.0f);
        return location;
    }

    public double getLongitude() {
        return this.longitude;
    }

    public String getPostCode() {
        return this.postCode;
    }

    public long getTime() {
        return this.time;
    }

    public String getTimeStr() {
        return this.timeStr;
    }

    public boolean hasAddress() {
        return this.address != null;
    }

    public boolean hasAdrCode() {
        return this.adrCode != null;
    }

    public boolean hasAreaCode() {
        return this.areaCode != null;
    }

    public boolean hasAreaName() {
        return this.areaName != null;
    }

    public boolean hasPostCode() {
        return this.postCode != null;
    }

    public void setAddress(String str) {
        this.address = str;
    }

    public void setAdrCode(String str) {
        this.adrCode = str;
    }

    public void setAreaCode(String str) {
        this.areaCode = str;
    }

    public void setAreaName(String str) {
        this.areaName = str;
    }

    public void setLatitude(double d) {
        this.latitude = d;
    }

    public void setLongitude(double d) {
        this.longitude = d;
    }

    public void setPostCode(String str) {
        this.postCode = str;
    }

    public void setTime(long j) {
        this.time = j;
    }

    public void setTimeStr(String str) {
        this.timeStr = str;
    }

    public String toString() {
        StringBuffer stringBuffer = new StringBuffer();
        stringBuffer.append("[lat: " + this.latitude);
        stringBuffer.append(", lon: " + this.longitude);
        stringBuffer.append(", time: " + this.time);
        stringBuffer.append(", timeStr: " + this.timeStr);
        stringBuffer.append(", areaCode: " + (this.areaCode == null ? "null" : this.areaCode));
        stringBuffer.append(", areaName: " + (this.areaName == null ? "null" : this.areaName));
        stringBuffer.append(", address: " + (this.address == null ? "null" : this.address));
        stringBuffer.append(", adrCode: " + (this.adrCode == null ? "null" : this.adrCode));
        stringBuffer.append(", postCode: " + (this.postCode == null ? "null" : this.postCode));
        stringBuffer.append("]");
        return stringBuffer.toString();
    }
}
