package net.gogame.gopay.sdk;

import java.util.List;
import java.util.Map;

public class GetCountryDetailsResponse {

    /* renamed from: a */
    private final String f1213a;

    /* renamed from: b */
    private final List f1214b;

    /* renamed from: c */
    private final Map f1215c;

    public GetCountryDetailsResponse(String str, List list, Map map) {
        this.f1213a = str;
        this.f1214b = list;
        this.f1215c = map;
    }

    public Map getBaseUrls() {
        return this.f1215c;
    }

    public List getCountries() {
        return this.f1214b;
    }

    public String getCountry() {
        return this.f1213a;
    }
}
