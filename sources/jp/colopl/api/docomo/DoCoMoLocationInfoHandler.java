package p018jp.colopl.api.docomo;

import android.sax.Element;
import android.sax.EndTextElementListener;
import android.sax.RootElement;
import android.sax.StartElementListener;
import android.util.Xml;
import android.util.Xml.Encoding;
import java.io.IOException;
import java.io.InputStream;
import java.util.Date;
import org.xml.sax.Attributes;
import org.xml.sax.SAXException;
import org.xml.sax.helpers.DefaultHandler;
import p018jp.colopl.util.Util;

/* renamed from: jp.colopl.api.docomo.DoCoMoLocationInfoHandler */
public class DoCoMoLocationInfoHandler extends DefaultHandler {
    private static String TAG = "LocationInfoHandler";
    /* access modifiers changed from: private */
    public Feature feature = null;
    /* access modifiers changed from: private */
    public DoCoMoLocationInfo result = null;
    /* access modifiers changed from: private */
    public ResultInfo resultInfo = null;

    public DoCoMoLocationInfo parse(InputStream inputStream) {
        RootElement rootElement = new RootElement("DDF");
        Element child = rootElement.getChild("ResultInfo");
        Element child2 = child.getChild("TotalCount");
        Element child3 = child.getChild("ResultCode");
        Element child4 = child.getChild("Error").getChild("Message");
        Element child5 = rootElement.getChild("Feature");
        Element child6 = child5.getChild("Geometry");
        Element child7 = child6.getChild("Lat");
        Element child8 = child6.getChild("Lon");
        Element child9 = child6.getChild("Time");
        Element child10 = child5.getChild("OptionProperty");
        Element child11 = child10.getChild("AreaCode");
        Element child12 = child10.getChild("AreaName");
        Element child13 = child10.getChild("Adr");
        Element child14 = child10.getChild("AdrCode");
        Element child15 = child10.getChild("PostCode");
        rootElement.setStartElementListener(new StartElementListener() {
            public void start(Attributes attributes) {
                DoCoMoLocationInfoHandler.this.result = new DoCoMoLocationInfo();
            }
        });
        child.setStartElementListener(new StartElementListener() {
            public void start(Attributes attributes) {
                DoCoMoLocationInfoHandler.this.resultInfo = DoCoMoLocationInfoHandler.this.result.getResultInfo();
            }
        });
        child2.setEndTextElementListener(new EndTextElementListener() {
            public void end(String str) {
                DoCoMoLocationInfoHandler.this.resultInfo.setTotalCount(Integer.parseInt(str));
            }
        });
        child3.setEndTextElementListener(new EndTextElementListener() {
            public void end(String str) {
                DoCoMoLocationInfoHandler.this.resultInfo.setResultCode(Integer.parseInt(str));
            }
        });
        child4.setEndTextElementListener(new EndTextElementListener() {
            public void end(String str) {
                DoCoMoLocationInfoHandler.this.resultInfo.setErrorMessage(str);
            }
        });
        child5.setStartElementListener(new StartElementListener() {
            public void start(Attributes attributes) {
                DoCoMoLocationInfoHandler.this.feature = new Feature();
                DoCoMoLocationInfoHandler.this.result.getFeatureList().add(DoCoMoLocationInfoHandler.this.feature);
            }
        });
        child7.setEndTextElementListener(new EndTextElementListener() {
            public void end(String str) {
                DoCoMoLocationInfoHandler.this.feature.setLatitude(Double.parseDouble(str.substring(1)) * ((double) (str.charAt(0) == 'S' ? -1 : 1)));
            }
        });
        child8.setEndTextElementListener(new EndTextElementListener() {
            public void end(String str) {
                DoCoMoLocationInfoHandler.this.feature.setLongitude(Double.parseDouble(str.substring(1)) * ((double) (str.charAt(0) == 'W' ? -1 : 1)));
            }
        });
        child9.setEndTextElementListener(new EndTextElementListener() {
            public void end(String str) {
                try {
                    int parseInt = Integer.parseInt(str.substring(0, 4));
                    DoCoMoLocationInfoHandler.this.feature.setTime(new Date(parseInt - 1900, Integer.parseInt(str.substring(5, 7)) - 1, Integer.parseInt(str.substring(8, 10)), Integer.parseInt(str.substring(11, 13)), Integer.parseInt(str.substring(14, 16)), Integer.parseInt(str.substring(17, 19))).getTime());
                    DoCoMoLocationInfoHandler.this.feature.setTimeStr(str);
                } catch (NumberFormatException e) {
                    throw e;
                }
            }
        });
        child11.setEndTextElementListener(new EndTextElementListener() {
            public void end(String str) {
                DoCoMoLocationInfoHandler.this.feature.setAreaCode(str);
            }
        });
        child12.setEndTextElementListener(new EndTextElementListener() {
            public void end(String str) {
                DoCoMoLocationInfoHandler.this.feature.setAreaName(str);
            }
        });
        child13.setEndTextElementListener(new EndTextElementListener() {
            public void end(String str) {
                DoCoMoLocationInfoHandler.this.feature.setAddress(str);
            }
        });
        child14.setEndTextElementListener(new EndTextElementListener() {
            public void end(String str) {
                DoCoMoLocationInfoHandler.this.feature.setAdrCode(str);
            }
        });
        child15.setEndTextElementListener(new EndTextElementListener() {
            public void end(String str) {
                DoCoMoLocationInfoHandler.this.feature.setPostCode(str);
            }
        });
        try {
            Xml.parse(inputStream, Encoding.UTF_8, rootElement.getContentHandler());
            return this.result;
        } catch (SAXException e) {
            Util.dLog(TAG, e.toString());
        } catch (NumberFormatException e2) {
            Util.dLog(TAG, e2.toString());
        } catch (IOException e3) {
            Util.dLog(TAG, e3.toString());
        } catch (AssertionError e4) {
            Util.dLog(TAG, e4.toString());
        } catch (Exception e5) {
            Util.dLog(TAG, e5.toString());
        }
        return null;
    }
}
