package net.gogame.gopay.sdk;

import java.util.List;
import java.util.Map;

public class GetCountryDetailsResponse {
    /* renamed from: a */
    private final String f954a;
    /* renamed from: b */
    private final List f955b;
    /* renamed from: c */
    private final Map f956c;

    public GetCountryDetailsResponse(String str, List list, Map map) {
        this.f954a = str;
        this.f955b = list;
        this.f956c = map;
    }

    public Map getBaseUrls() {
        return this.f956c;
    }

    public List getCountries() {
        return this.f955b;
    }

    public String getCountry() {
        return this.f954a;
    }
}
