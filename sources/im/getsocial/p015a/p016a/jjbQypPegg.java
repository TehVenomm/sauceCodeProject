package im.getsocial.p015a.p016a;

import java.util.ArrayList;
import java.util.List;

/* renamed from: im.getsocial.a.a.jjbQypPegg */
public class jjbQypPegg {
    /* renamed from: a */
    List f972a = new ArrayList();
    /* renamed from: b */
    private String f973b = ",";

    public String toString() {
        String str = this.f973b;
        StringBuffer stringBuffer = new StringBuffer();
        for (int i = 0; i < this.f972a.size(); i++) {
            if (i == 0) {
                stringBuffer.append(this.f972a.get(i));
            } else {
                stringBuffer.append(str);
                stringBuffer.append(this.f972a.get(i));
            }
        }
        return stringBuffer.toString();
    }
}
