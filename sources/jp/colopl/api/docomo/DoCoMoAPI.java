package jp.colopl.api.docomo;

import java.io.InputStream;
import jp.colopl.drapro.ColoplApplication;
import jp.colopl.util.HTTP;

public class DoCoMoAPI {
    public static final String ENTRY_POINT = "https://api.spmode.ne.jp/nwLocation/GetLocation";
    public static final float PSEUDO_ACCURACY = 250.0f;
    public static final String PSEUDO_NAME_AS_LOCATION_PROVIDER = "DoCoMoSPApi";
    private static final String REQUEST_XML = "<?xml version=\"1.0\" encoding=\"UTF-8\"?><DDF ver=\"1.0\"><RequestInfo><RequestParam><APIKey><APIKey1_ID>%s</APIKey1_ID ><APIKey2>%s</APIKey2></APIKey><OptionProperty><AreaCode></AreaCode><AreaName></AreaName><Adr></Adr><AdrCode></AdrCode><PostCode></PostCode></OptionProperty></RequestParam></RequestInfo></DDF>";
    public static final int RESULT_AUTHORIZE_ERROR = 3;
    public static final int RESULT_REQUEST_INVALID_ERROR = -3;
    public static final int RESULT_SERVICE_ERROR = 2;
    public static final int RESULT_SUCCEEDED = 0;
    public static final int RESULT_SYSTEM_ERROR = 4;
    public static final int RESULT_UNKNOWN_ERROR = -1;
    public static final int REULST_POST_ERROR = -2;
    public static final String SETTING_SITE_APP_URL = "https://spmode.ne.jp/setting/apiUsePermitAuthAction.do?apikey=colopl-cOlopg001";
    public static final String SETTING_SITE_URL = "https://spmode.ne.jp/setting/apiControlAuthKeyClean.do";

    static {
        System.loadLibrary("dcapikey");
    }

    public static native String getApiKey1();

    public static native String getApiKey2();

    public static String getEntryPoint() {
        return ENTRY_POINT;
    }

    public static String getRequestXML() {
        return String.format(REQUEST_XML, new Object[]{getApiKey1(), getApiKey2()});
    }

    public static boolean isAvailableDoCoMoAPI(ColoplApplication coloplApplication) {
        return coloplApplication.isCarrierDoCoMo() && coloplApplication.isMobileConnectivity();
    }

    public DoCoMoLocationInfo getLocationInfo() {
        InputStream post = post();
        return post == null ? null : new DoCoMoLocationInfoHandler().parse(post);
    }

    public InputStream post() {
        return HTTP.postXML(ENTRY_POINT, getRequestXML());
    }
}
