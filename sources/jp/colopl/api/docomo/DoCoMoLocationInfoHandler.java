package jp.colopl.api.docomo;

import android.sax.Element;
import android.sax.EndTextElementListener;
import android.sax.RootElement;
import android.sax.StartElementListener;
import android.util.Xml;
import android.util.Xml.Encoding;
import java.io.IOException;
import java.io.InputStream;
import java.util.Date;
import jp.colopl.util.Util;
import org.xml.sax.Attributes;
import org.xml.sax.SAXException;
import org.xml.sax.helpers.DefaultHandler;

public class DoCoMoLocationInfoHandler extends DefaultHandler {
    private static String TAG = "LocationInfoHandler";
    private Feature feature = null;
    private DoCoMoLocationInfo result = null;
    private ResultInfo resultInfo = null;

    /* renamed from: jp.colopl.api.docomo.DoCoMoLocationInfoHandler$1 */
    class C12641 implements StartElementListener {
        C12641() {
        }

        public void start(Attributes attributes) {
            DoCoMoLocationInfoHandler.this.result = new DoCoMoLocationInfo();
        }
    }

    /* renamed from: jp.colopl.api.docomo.DoCoMoLocationInfoHandler$2 */
    class C12652 implements StartElementListener {
        C12652() {
        }

        public void start(Attributes attributes) {
            DoCoMoLocationInfoHandler.this.resultInfo = DoCoMoLocationInfoHandler.this.result.getResultInfo();
        }
    }

    /* renamed from: jp.colopl.api.docomo.DoCoMoLocationInfoHandler$3 */
    class C12663 implements EndTextElementListener {
        C12663() {
        }

        public void end(String str) {
            DoCoMoLocationInfoHandler.this.resultInfo.setTotalCount(Integer.parseInt(str));
        }
    }

    /* renamed from: jp.colopl.api.docomo.DoCoMoLocationInfoHandler$4 */
    class C12674 implements EndTextElementListener {
        C12674() {
        }

        public void end(String str) {
            DoCoMoLocationInfoHandler.this.resultInfo.setResultCode(Integer.parseInt(str));
        }
    }

    /* renamed from: jp.colopl.api.docomo.DoCoMoLocationInfoHandler$5 */
    class C12685 implements EndTextElementListener {
        C12685() {
        }

        public void end(String str) {
            DoCoMoLocationInfoHandler.this.resultInfo.setErrorMessage(str);
        }
    }

    /* renamed from: jp.colopl.api.docomo.DoCoMoLocationInfoHandler$6 */
    class C12696 implements StartElementListener {
        C12696() {
        }

        public void start(Attributes attributes) {
            DoCoMoLocationInfoHandler.this.feature = new Feature();
            DoCoMoLocationInfoHandler.this.result.getFeatureList().add(DoCoMoLocationInfoHandler.this.feature);
        }
    }

    /* renamed from: jp.colopl.api.docomo.DoCoMoLocationInfoHandler$7 */
    class C12707 implements EndTextElementListener {
        C12707() {
        }

        public void end(String str) {
            DoCoMoLocationInfoHandler.this.feature.setLatitude(Double.parseDouble(str.substring(1)) * ((double) (str.charAt(0) == 'S' ? -1 : 1)));
        }
    }

    /* renamed from: jp.colopl.api.docomo.DoCoMoLocationInfoHandler$8 */
    class C12718 implements EndTextElementListener {
        C12718() {
        }

        public void end(String str) {
            DoCoMoLocationInfoHandler.this.feature.setLongitude(Double.parseDouble(str.substring(1)) * ((double) (str.charAt(0) == 'W' ? -1 : 1)));
        }
    }

    /* renamed from: jp.colopl.api.docomo.DoCoMoLocationInfoHandler$9 */
    class C12729 implements EndTextElementListener {
        C12729() {
        }

        public void end(String str) {
            try {
                int parseInt = Integer.parseInt(str.substring(0, 4));
                DoCoMoLocationInfoHandler.this.feature.setTime(new Date(parseInt - 1900, Integer.parseInt(str.substring(5, 7)) - 1, Integer.parseInt(str.substring(8, 10)), Integer.parseInt(str.substring(11, 13)), Integer.parseInt(str.substring(14, 16)), Integer.parseInt(str.substring(17, 19))).getTime());
                DoCoMoLocationInfoHandler.this.feature.setTimeStr(str);
            } catch (NumberFormatException e) {
                throw e;
            }
        }
    }

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
        child6 = child6.getChild("Time");
        Element child9 = child5.getChild("OptionProperty");
        Element child10 = child9.getChild("AreaCode");
        Element child11 = child9.getChild("AreaName");
        Element child12 = child9.getChild("Adr");
        Element child13 = child9.getChild("AdrCode");
        child9 = child9.getChild("PostCode");
        rootElement.setStartElementListener(new C12641());
        child.setStartElementListener(new C12652());
        child2.setEndTextElementListener(new C12663());
        child3.setEndTextElementListener(new C12674());
        child4.setEndTextElementListener(new C12685());
        child5.setStartElementListener(new C12696());
        child7.setEndTextElementListener(new C12707());
        child8.setEndTextElementListener(new C12718());
        child6.setEndTextElementListener(new C12729());
        child10.setEndTextElementListener(new EndTextElementListener() {
            public void end(String str) {
                DoCoMoLocationInfoHandler.this.feature.setAreaCode(str);
            }
        });
        child11.setEndTextElementListener(new EndTextElementListener() {
            public void end(String str) {
                DoCoMoLocationInfoHandler.this.feature.setAreaName(str);
            }
        });
        child12.setEndTextElementListener(new EndTextElementListener() {
            public void end(String str) {
                DoCoMoLocationInfoHandler.this.feature.setAddress(str);
            }
        });
        child13.setEndTextElementListener(new EndTextElementListener() {
            public void end(String str) {
                DoCoMoLocationInfoHandler.this.feature.setAdrCode(str);
            }
        });
        child9.setEndTextElementListener(new EndTextElementListener() {
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
