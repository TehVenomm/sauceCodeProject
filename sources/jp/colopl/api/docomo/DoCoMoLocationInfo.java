package jp.colopl.api.docomo;

import java.util.ArrayList;

public class DoCoMoLocationInfo {
    private ArrayList<Feature> featureList;
    private ResultInfo resultInfo;

    public DoCoMoLocationInfo() {
        this(new ResultInfo(), new ArrayList());
    }

    public DoCoMoLocationInfo(ResultInfo resultInfo) {
        this(resultInfo, new ArrayList());
    }

    public DoCoMoLocationInfo(ResultInfo resultInfo, ArrayList<Feature> arrayList) {
        this.resultInfo = resultInfo;
        this.featureList = arrayList;
    }

    public Feature getFeature() {
        return getFeature(0);
    }

    public Feature getFeature(int i) {
        return (this.featureList == null || i > this.featureList.size() - 1) ? null : (Feature) this.featureList.get(i);
    }

    public ArrayList<Feature> getFeatureList() {
        return this.featureList;
    }

    public ResultInfo getResultInfo() {
        return this.resultInfo;
    }

    public void setResultInfo(ResultInfo resultInfo) {
        this.resultInfo = resultInfo;
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
