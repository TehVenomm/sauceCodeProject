package jp.colopl.api.docomo;

public interface DoCoMoAsyncTaskDelegate {
    void receiveErrorDoCoMoLocationInfo(DoCoMoLocationInfo doCoMoLocationInfo);

    void receiveSuccessDoCoMoLocationInfo(DoCoMoLocationInfo doCoMoLocationInfo);
}
