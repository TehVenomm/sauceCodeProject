package p018jp.colopl.api.docomo;

import java.util.ArrayList;

/* renamed from: jp.colopl.api.docomo.DoCoMoLocationInfo */
public class DoCoMoLocationInfo {
    private ArrayList<Feature> featureList;
    private ResultInfo resultInfo;

    public DoCoMoLocationInfo() {
        this(new ResultInfo(), new ArrayList());
    }

    public DoCoMoLocationInfo(ResultInfo resultInfo2) {
        this(resultInfo2, new ArrayList());
    }

    public DoCoMoLocationInfo(ResultInfo resultInfo2, ArrayList<Feature> arrayList) {
        this.resultInfo = resultInfo2;
        this.featureList = arrayList;
    }

    public Feature getFeature() {
        return getFeature(0);
    }

    public Feature getFeature(int i) {
        if (this.featureList == null || i > this.featureList.size() - 1) {
            return null;
        }
        return (Feature) this.featureList.get(i);
    }

    public ArrayList<Feature> getFeatureList() {
        return this.featureList;
    }

    public ResultInfo getResultInfo() {
        return this.resultInfo;
    }

    public void setResultInfo(ResultInfo resultInfo2) {
        this.resultInfo = resultInfo2;
    }

    public String toString() {
        StringBuffer stringBuffer = new StringBuffer();
        stringBuffer.append("[");
        stringBuffer.append("ResultInfo: " + (this.resultInfo == null ? "null" : this.resultInfo.toString()) + ", ");
        stringBuffer.append("FeatureList: " + (this.featureList == null ? "null" : this.featureList.toString()));
        stringBuffer.append("]");
        return stringBuffer.toString();
    }
}
