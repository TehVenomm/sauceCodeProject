package net.gogame.gopay.sdk;

import java.util.List;
import java.util.Map;

public class GetCountryDetailsResponse {

    /* renamed from: a */
    private final String f1201a;

    /* renamed from: b */
    private final List f1202b;

    /* renamed from: c */
    private final Map f1203c;

    public GetCountryDetailsResponse(String str, List list, Map map) {
        this.f1201a = str;
        this.f1202b = list;
        this.f1203c = map;
    }

    public Map getBaseUrls() {
        return this.f1203c;
    }

    public List getCountries() {
        return this.f1202b;
    }

    public String getCountry() {
        return this.f1201a;
    }
}
