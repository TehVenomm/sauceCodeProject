package p018jp.colopl.api.docomo;

/* renamed from: jp.colopl.api.docomo.ResultInfo */
public class ResultInfo {
    private String errorMessage = null;
    private int resultCode = 0;
    private int totalCount = 0;

    public ResultInfo() {
    }

    ResultInfo(int i, int i2, String str) {
        this.totalCount = i;
        this.resultCode = i2;
        this.errorMessage = str;
    }

    public String getErrorMessage() {
        return this.errorMessage;
    }

    public int getResultCode() {
        return this.resultCode;
    }

    public int getStatus() {
        if (this.resultCode >= 2000 && this.resultCode <= 2999) {
            return 0;
        }
        if (this.resultCode >= 3000 && this.resultCode <= 3999) {
            return 2;
        }
        if (this.resultCode < 4000 || this.resultCode > 4999) {
            return (this.resultCode < 5000 || this.resultCode > 5999) ? -1 : 2;
        }
        return 3;
    }

    public int getTotalCount() {
        return this.totalCount;
    }

    public String getUrlForAuthorize() {
        if (isNeedToAuthroize()) {
            return this.errorMessage;
        }
        if (isNeedToAuthroizeAll()) {
            return DoCoMoAPI.SETTING_SITE_URL;
        }
        return null;
    }

    public boolean isNeedToAuthroize() {
        return this.resultCode == 4002;
    }

    public boolean isNeedToAuthroizeAll() {
        return this.resultCode == 4100;
    }

    public boolean isResultCodeOK() {
        return this.resultCode == 2000;
    }

    public void setErrorMessage(String str) {
        this.errorMessage = str;
    }

    public void setResultCode(int i) {
        this.resultCode = i;
    }

    public void setTotalCount(int i) {
        this.totalCount = i;
    }

    public String toString() {
        StringBuffer stringBuffer = new StringBuffer();
        stringBuffer.append("[totalCount: " + this.totalCount);
        stringBuffer.append(", resultCode: " + this.resultCode);
        stringBuffer.append(", errorMessage: " + (this.errorMessage == null ? "null" : this.errorMessage));
        stringBuffer.append("]");
        return stringBuffer.toString();
    }
}
